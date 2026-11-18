> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Măști de câmpuri

O mască de câmpuri transformă o alegere de câmpuri în date. Această lecție pornește de la un client și un printer, face imprimarea configurabilă, reutilizează măștile pentru a descrie diferențele dintre clienți și, la final, memorează toate flagurile într-un singur întreg. Urmăriți modificările în această ordine: operațiile pe biți rezolvă o problemă de mentenanță pusă în evidență de versiunea booleană anterioară.

Termenii folosiți pe parcurs sunt:

- **Model de domeniu**: un tip care reprezintă o entitate din problemă, cu câmpuri care descriu acea entitate. Un **enum** dă nume unor valori numerice, cum ar fi grupurile de clienți sau identificatorii de câmpuri.
- **`sealed` și `required`**: `sealed` împiedică moștenirea; `required` impune inițializarea membrilor la construirea unui obiect. Inițializarea și validarea diferită de null sunt sarcini distincte.
- **Clasă utilitară statică și funcție locală**: o clasă statică grupează operații fără instanțe; o funcție locală este declarată în interiorul altei funcții.
- **Mască de câmpuri și flag**: o mască conține o alegere da/nu pentru fiecare câmp; un flag este o asemenea alegere. Ea selectează apartenența, nu ordinea.
- **Serializare**: reprezentarea datelor într-o formă care poate fi salvată și citită înapoi, de exemplu într-un fișier de configurare.
- **`struct`, tip valoare și `record struct`**: un struct este un tip valoare, copiat în mod normal la atribuire sau la transmiterea ca argument; un record struct oferă și egalitate după valoare, inclusiv `==`.
- **Alocare pe heap și boxing**: alocarea pe heap creează stocare pentru obiecte gestionate; boxing-ul înfășoară un tip valoare într-un obiect. Folosirea unei valori mici pentru mască evită o alocare separată a unui obiect-mască în apelurile demonstrate, dar nu garantează că fiecare struct trăiește întotdeauna pe stivă.
- **Valoare implicită, valoare nullable și parametru opțional**: valoarea implicită a unei măști conține flaguri false; o mască nullable poate să nu aibă deloc valoare; un parametru opțional are o valoare de rezervă declarată atunci când argumentul său este omis.
- **Operatorul de coalescență `??` și discard-ul `_`**: `??` furnizează o valoare de rezervă pentru o valoare absentă; un discard indică faptul că un rezultat este intenționat nefolosit.
- **Expresie de colecție, switch și nameof**: parantezele pătrate pot construi tabloul de grupuri; un switch selectează un caz, având o ramură implicită pentru valorile nepotrivite; nameof obține numele unui identificator.
- **Funcție de configurare și rolurile măștilor**: o funcție de configurare transformă datele selecției; o mască de construire selectează comparațiile, o mască de verificare selectează rezultatele obligatorii, iar o mască de ștergere selectează flagurile care trebuie golite.
- **Argument denumit**: `parameterName: value` identifică parametrul explicit, în loc de a se baza pe poziția argumentului.
- **Mască de diferențe, sau diff**: un flag pentru fiecare câmp, care reține dacă două obiecte diferă. **Any** întreabă dacă cel puțin un flag selectat este setat; **all** întreabă dacă toate flagurile selectate sunt setate.
- **Index de câmp**: un identificator numeric pentru un câmp, care permite ca o logică comună să parcurgă câmpurile.
- **Contract**: restricții asupra intrărilor și promisiuni despre ieșiri. O **asertiune** înseamnă o diagnosticare pentru dezvoltare; o **excepție** raportează o eroare care poate fi captată. Apelurile `Debug` depind de simbolul de compilare `DEBUG`.
- **Getter, setter, proprietate și câmp de susținere**: un getter citește o valoare, un setter o scrie, o proprietate expune aceste operații cu sintaxă asemănătoare câmpurilor, iar un câmp de susținere memorează starea efectivă.
- **Expresie `with` și transmitere prin referință**: `with` produce o copie modificată; transmiterea prin referință permite unei funcții să actualizeze direct starea apelantului.
- **Iterator și generic**: un iterator furnizează o succesiune de valori; un generic parametrizează codul după un tip. Ambele sunt menționate ca aplicații mai ample, nedezvoltate aici.
- **Mască de biți și operații pe biți**: un singur întreg memorează flaguri în poziții de biți. `<<` deplasează, `|` combină biții setați, `&` păstrează biții comuni, iar `~` inversează biții.
- **Accesor cu expresie**: `=>` dă unui getter sau unui setter un corp compact, cu o singură expresie.
- **Încapsulare și generator de surse**: încapsularea ascunde reprezentarea în spatele unor operații semnificative; un generator de surse poate produce cod sursă repetitiv în timpul compilării.

[Laboratorul despre măști de câmpuri](../../../labs/2_design/01_field_mask.md) oferă exerciții. Baza de date, API-ul REST, copierea, fuzionarea și extensiile de teste unitare sunt exerciții; nu toate sunt implementări predate în această înregistrare.

Linkurile de cod de mai jos sunt instantanee imutabile. Exemplele scurte sunt simplificate pentru explicația din jur. Stările timpurii de editare live care nu sunt comise separat sunt etichetate ca reconstrucții din video, cu o sursă comisă corespunzătoare; corecțiile unui exemplu comis sunt identificate explicit.

## Un model de domeniu pentru client

[00:00:08](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=8s) Pornim de la un client ale cărui câmpuri sunt prenumele, numele de familie și apartenența la grupuri. `sealed` face clasa ne-moștenibilă, ca o clasă finală în Java; împiedică subclazele, nu face imutabile câmpurile clientului. [00:00:16](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=16s) `required` obligă codul de construcție să inițializeze membrii marcați. Intenția lecției este să avem valori utile imediat, dar `required` singur nu face imposibil null-ul: atribuirea lui null unui membru required non-nullable produce o avertizare nullable, nu dovedește lipsa lui null la runtime. Vezi [regulile pentru membrii required](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required).

[00:00:29](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=29s) Apartenența la grupuri ar putea fi codificată ca biți sau ca o colecție de valori enum. Exemplul folosește un tablou de grupuri; internal, highly valued și not important sunt posibile categorii de domeniu. Un enum este o mulțime denumită de valori numerice. Exemplul comis conține ultimele două categorii. [Modelul clientului comis](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs):

```csharp
sealed class Client
{
    public required string FirstName;
    public required string LastName;
    public required Group[] BelongsToGroups;
}
enum Group { HighlyValued, NotImportant }
```

[00:00:54](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=54s) Acesta este un **model de domeniu**: o clasă care reprezintă o entitate din lumea reală, cu caracteristici preluate din descrierea problemei. [00:01:16](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=76s) Maparea acelei descrieri într-o singură clasă este o abordare validă, nu o afirmație că este întotdeauna cea mai bună proiectare.

[00:01:24](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=84s) Alegeți forma în funcție de modul în care programul folosește clienții. [00:01:31](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=91s) Păstrarea tuturor caracteristicilor laolaltă este convenabilă atunci când un client se comportă ca o entitate într-o simulare sau este sincronizat cu o bază de date. Ea oglindește entitatea din lumea reală. [00:01:53](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=113s) Un dicționar de proprietăți sau datele împărțite între alte componente sunt alternative amânate pentru mai târziu. [00:02:09](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=129s) Următoarea întrebare este cum poate o mască de câmp să controleze operațiile asupra acestui model.

## Un printer static și prima lui rulare

[00:02:16](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=136s) Puneți operațiile independente într-o clasă utilitară autonomă. O **clasă utilitară statică** nu poate fi instanțiată și oferă metode statice apelabile prin numele tipului. Aceasta seamănă cu clasele utilitare din Java. [00:02:30](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=150s) Deși o **funcție locală** ar putea fi declarată în interiorul altei funcții, demonstrația alege o clasă și apoi un fișier separat. Instantaneul comis adună exemplul într-un singur fișier; nu păstrează acea dispunere temporară de fișiere.

Construiți primul program utilizabil în trei pași:

1. [00:02:50](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=170s) Scrieți un printer primitiv care primește un client și emite toate câmpurile. Păstrați formatarea simplă, ca întrebarea de proiectare să rămână vizibilă.
2. [00:03:10](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=190s) Construiți un client, inițializați câmpurile required și apelați tipul de afișare.
3. [00:03:23](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=203s) Rulați programul. Rezultatul înregistrat afișează câmpurile clientului prin acel singur apel.

[Helperi de afișare și construirea clientului din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); aceasta este o reconstrucție a stării anterioare, nemascate, din video:

```csharp
var client = new Client
{
    FirstName = "Anton",
    LastName = "Curmanschii",
    BelongsToGroups = [Group.HighlyValued]
};
Functions.Print(client);

static class Functions
{
    public static void Print(Client c)
    {
        Console.WriteLine(c.FirstName);
        Console.WriteLine(c.LastName);
        foreach (var group in c.BelongsToGroups)
            Console.WriteLine(group);
    }
}
```

Aici `static` pe clasă interzice instanțele; `static` pe metodă asociază operația tipului. Expresia de colecție din paranteze pătrate creează tabloul de grupuri folosit de acest exemplu.

## De ce selecția câmpurilor trebuie să devină date

[00:03:26](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=206s) Noua cerință este configurarea câmpurilor care se afișează. [00:03:44](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=224s) Eliminarea unor instrucțiuni de afișare sau apelarea unor printere separate pe câmpuri ar compila și ar produce o ieșire parțială. Aceasta este o bază de pornire disponibilă.

[Tipuri de afișare pe câmpuri din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); selecție hardcodată simplificată:

```csharp
Functions.PrintFirstName(client);
Functions.PrintLastName(client);
// Schimbați aceste apeluri pentru a le modifica ordinea.
```

[00:04:00](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=240s) Problema de proiectare este controlul din exterior: un apelant nu poate modifica o selecție fixată în corpul altei funcții. [00:04:24](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=264s) Transmiterea selecției ca parametru este o soluție naturală, cu un compromis.

[00:04:37](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=277s) O succesiune de apeluri hardcodate lasă programatorul să aleagă prenumele înaintea numelui de familie sau invers. [00:04:48](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=288s) Un printer bazat pe flaguri are o ordine de implementare fixă. [00:04:52](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=292s) Ceea ce devine serializabil sunt **datele selecției**: o mulțime de câmpuri poate fi salvată și restaurată din configurație. O succesiune brută de execuție nu este ea însăși acea configurație. S-ar putea memora o descriere ordonată separată a operațiilor, dar această lecție nu construiește una. O mască de câmp reține apartenența și nu poate păstra o ordine aleasă de apelant.

## Un boolean pentru fiecare câmp

[00:05:04](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=304s) O **mască de câmp** înseamnă inițial un flag boolean pentru fiecare câmp al clientului. Adevărat selectează acel câmp; fals îl sare. Masca separă decizia de implementarea printerului.

[Masca booleană și tipul de afișare condiționat din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); exemplu simplificat:

```csharp
record struct ClientFieldMask
{
    public bool FirstName;
    public bool LastName;
    public bool BelongsToGroups;
}

static void Print(Client c, ClientFieldMask m)
{
    if (m.FirstName) Console.WriteLine(c.FirstName);
    if (m.LastName) Console.WriteLine(c.LastName);
    if (m.BelongsToGroups)
        foreach (var group in c.BelongsToGroups)
            Console.WriteLine(group);
}
```

1. [00:05:18](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=318s) Adăugați un al doilea parametru pentru mască, astfel încât apelantul să furnizeze selecția.
2. [00:05:23](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=323s) Protejați ieșirea fiecărui câmp cu flagul său corespunzător. Tipul de afișare își păstrează ordinea fixă: prenume, nume de familie, grupuri.
3. [00:05:59](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=359s) Odată ce apelanții folosesc operația agregată, funcțiile de afișare separate pe câmpuri nu mai sunt necesare ca mecanism de selecție al apelantului. Exemplul salvat le păstrează în continuare ca helperi de implementare; aceasta este compatibil cu delegarea selecției către mască.

Un `record struct` este un tip valoare cu egalitate după valoare generată. Lecția ajunge la motivul acestei alegeri în timp ce lucrează mai departe la valorile implicite.

## Măști omise și măști explicit goale

[00:06:13](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=373s) Faceți masca opțională, astfel încât apelanții existenți să poată cere în continuare toată ieșirea omițând-o. Un **parametru opțional** are o valoare implicită în declarație. [00:06:21](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=381s) Semnul întrebării înseamnă **tip valoare nullable**, nu opționalitate în sine; `= null` face ca acest parametru să fie opțional. **Operatorul de coalescență** `??` alege o valoare de rezervă doar când valoarea nullable este absentă.

[Parametrul nullable Print și rezerva de tip toate-câmpurile din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); rezervă echivalentă reconstruită:

```csharp
static void Print(Client c, ClientFieldMask? mask = null)
{
    ClientFieldMask m = mask ?? new ClientFieldMask
    {
        FirstName = true,
        LastName = true,
        BelongsToGroups = true
    };
    // Afișează doar câmpurile ale căror flaguri din m sunt true.
}
```

[00:06:35](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=395s) Încercați o mască construită explicit, cu prenumele și numele de familie activate. [00:06:52](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=412s) Câmpurile booleene neamintite au **valoarea implicită** fals, deci grupurile rămân dezactivate. [00:07:05](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=425s) Rularea arată doar numele selectate. Aceasta este o mască parțială explicită; omiterea argumentului este un alt caz. [00:07:21](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=441s) Omiterea lui folosește rezerva și afișează tot.

[Reprezentarea măștii din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); experiment simplificat la locul de apel:

```csharp
var names = new ClientFieldMask { FirstName = true, LastName = true };
Functions.Print(client, names);                  // Doar numele.
Functions.Print(client);                         // Toate câmpurile.
Functions.Print(client, new ClientFieldMask());  // Niciun câmp.
_ = names;                                       // Rezultat intenționat nefolosit.
```

[00:07:11](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=431s) **Discard-ul** `_` semnalizează neutilizarea intenționată. În mediul înregistrat, o diagnosticare pentru variabilă nefolosită este tratată ca o eroare care blochează compilarea; aceasta este o politică de proiect/instrumentare, nu semnificația la runtime a discard-ului. Discard-ul nu schimbă ce câmpuri se afișează.

[00:07:32](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=452s) Un **struct** este un tip valoare: transmiterea obișnuită a măștii nu necesită un obiect-mască separat alocat pe heap. Aceasta este motivul de alocare pentru care preferăm struct în locul construirii unei măști-clasă la fiecare apel. Evitați afirmația mai tare că struct-urile nu ajung niciodată pe heap: ele pot fi boxing (împachetate ca obiecte) sau stocate în interiorul unor obiecte de pe heap. [Referința despre struct](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct) explică copierea valorilor.

[00:07:41](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=461s) Un **record struct** generează și `==`, permițând o comparație a măștii întregi cu `default`. [00:07:56](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=476s) O proiectare intermediară folosește valoarea implicită cu toate flagurile false drept semnal pentru „folosește toate câmpurile”; cealaltă ramură nu are lucru suplimentar. [Instantaneul boolean al record struct](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); stare intermediară reconstruită:

```csharp
static void Print(Client c, ClientFieldMask mask = default)
{
    if (mask == default)
        mask = new() { FirstName = true, LastName = true, BelongsToGroups = true };
    // Continuați cu câmpurile selectate.
}
```

Această proiectare intermediară nu poate distinge un argument omis de o mască explicit goală. [00:08:14](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=494s) Struct-ul ei evită totuși o alocare separată a măștii-clasă în apelurile demonstrate.

[00:08:24](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=504s) O variabilă locală nullable poate distinge absența de o valoare reală a măștii. Ea trebuie atribuită înainte de utilizare; scrierea doar a lui `ClientFieldMask? m;` nu inițializează o variabilă locală cu null. [00:08:39](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=519s) Proiectarea finală cu parametru nullable folosește absența pentru rezerva de toate câmpurile și lasă nemodificată o mască furnizată, inclusiv o mască goală.

<details>
<summary>Anticipați: afișează o mască explicit goală totul?</summary>

În versiunea intermediară cu santinelă default non-nullable, da. În versiunea nullable, nu: o valoare prezentă cu toate flagurile false nu selectează niciun câmp. Doar o valoare absentă selectează rezerva.

</details>

## Configurați o mască înainte de afișare

[00:08:56](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=536s) Selecția este acum date obișnuite pe care o altă funcție le poate transforma. O **funcție de configurare** primește o mască inițială și întoarce valoarea modificată. [00:09:23](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=563s) Deciziile sale pot fi arbitrare: poate consulta intrarea utilizatorului, alte setări sau poate elimina un câmp cerut de apelant. [00:09:33](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=573s) Masca controlează în continuare doar selecția câmpurilor; nu prescrie cum a fost luată decizia.

[Implementarea ConfigureFilter din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); flux simplificat:

```csharp
static ClientFieldMask ConfigureFilter(ClientFieldMask mask)
{
    if (mask.BelongsToGroups)
        mask.FirstName = false;
    return mask;
}

var mask = new ClientFieldMask { FirstName = true, BelongsToGroups = true };
mask = ConfigureFilter(mask);
Functions.Print(client, mask);
```

[00:09:49](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=589s) Păstrați masca într-o variabilă, transmiteți valoarea originală ca punct de pornire și atribuiți masca întoarsă. Deoarece acesta este un tip valoare, valoarea întoarsă aduce modificările înapoi la apelant. [00:10:08](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=608s) Avantajul este schimbarea mulțimii de operații a unui printer terț fără a edita acel printer.

## Scădeți câmpurile interzise

[00:10:18](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=618s) O operație utilă următoare primește o mască, furnizată de utilizator sau implicit, și elimină fiecare câmp numit de o altă mască. A doua mască poate veni din orice sursă de configurare. [00:10:43](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=643s) Numim cele două roluri „de afișat” și „de neafișat”: primul cere câmpuri, iar al doilea le interzice.

[00:11:02](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=662s) Exemplul narat lasă doar numele de familie. Motivul este că câmpul rămas este cerut și **nu interzis**. Un câmp cerut și interzis nu poate supraviețui; această formulare contează când citim explicația. [00:11:16](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=676s) Masca care interzice are prioritate față de masca de afișare pentru câmpurile selectate de ea. [00:11:37](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=697s) Dacă numește un câmp pe care prima mască nu l-a selectat niciodată, golirea acelui flag nu face pur și simplu nimic. Acest comportament fără efect este proiectarea aleasă.

[Implementarea Without din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); tabel de adevăr simplificat pentru un câmp:

| Cerut | Interzis | Rezultat |
| --- | --- | --- |
| false | false | false |
| false | true | false |
| true | false | true |
| true | true | false |

[00:11:47](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=707s) Păstrarea a două măști separat nu le combină în sine. Scrieți o funcție **Without** care primește masca inițială și masca de eliminat. [00:11:50](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=710s) Ea întoarce prima mască cu câmpurile selectate corespunzătoare golite. Implementarea primitivă verifică fiecare flag:

```csharp
static ClientFieldMask Without(ClientFieldMask mask, ClientFieldMask toDelete)
{
    if (toDelete.FirstName) mask.FirstName = false;
    if (toDelete.LastName) mask.LastName = false;
    if (toDelete.BelongsToGroups) mask.BelongsToGroups = false;
    return mask;
}
```

[00:12:08](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=728s) Acest lucru este ușor de înțeles, dar [00:12:17](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=737s) duplică lista de câmpuri deja prezentă în tipul măștii. [00:12:28](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=748s) Un câmp nou necesită încă o ramură; uitarea acelei ramuri lasă noul flag neșters și creează un bug. [00:12:45](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=765s) Obiectivul este mutarea cunoștințelor despre câmpuri în operații de acces partajate, astfel încât funcțiile de combinare să nu mai întrețină manual acea listă.

[00:12:55](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=775s) Un **argument denumit** folosește numele parametrului și semnul două puncte. Numele asociază argumentele cu parametri chiar când ordinea lor este inversată. [Semnătura Without din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); apelant simplificat:

```csharp
var requested = new ClientFieldMask
{
    FirstName = true, LastName = true, BelongsToGroups = true
};
var forbidden = new ClientFieldMask { LastName = true, BelongsToGroups = true };
var effective = Functions.Without(toDelete: forbidden, mask: requested);
Functions.Print(client, effective);
```

[00:13:34](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=814s) Rularea înregistrată ulterior lasă doar prenumele. [00:13:40](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=820s) Adăugarea grupurilor la masca cerută nu le afișează cât timp rămân interzise. Eliminați grupurile din masca care interzice pentru a le permite să treacă.

[00:14:03](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=843s) Aceasta este scăderea de mulțimi, analoagă diferenței dintre mulțimi de hash (`Except`/`ExceptWith`). [00:14:10](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=850s) Pentru biți, echivalentul este AND cu **complementul** măștii de ștergere, `a & ~b`. Un simplu `a & b` este intersecție și păstrează flagurile comune; nu implementează Without. Tabelul de adevăr boolean de mai sus este contractul pe care versiunea bitwise ulterioară trebuie să-l păstreze.

<details>
<summary>Anticipați: ce se întâmplă dacă masca care interzice conține doar grupurile, iar masca cerută conține doar prenumele?</summary>

Prenumele rămâne selectat. Golirea unui flag de grupuri deja fals este o operație fără efect.

</details>

## Calculați și reutilizați o mască de diferențe

[00:14:24](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=864s) O mască poate descrie și rezultatele comparațiilor, nu doar operațiile cerute. O **mască de diferențe** folosește true pentru a însemna „aceste obiecte diferă în acest câmp”. Inițial discuția include o mască de construire care selectează câmpurile pe care comparația ar trebui să le inspecteze.

[00:14:45](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=885s) Porniți rezultatul cu toate flagurile false. Pentru fiecare câmp inspectat, setați flagul rezultatului său doar dacă valorile corespunzătoare diferă. [00:15:11](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=911s) Această operație se numește în mod convențional **diff**, numele folosit în exemplu. [00:15:27](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=927s) Un diff mascat verifică doar câmpurile numite de selecția sa de intrare; nu este neapărat o comparație a tuturor câmpurilor.

[Diff și helperul de comparație din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); versiune reconstruită pe câmpuri selectate, cu polaritatea diferenței intenționată:

```csharp
static ClientFieldMask Diff(Client a, Client b, ClientFieldMask selected)
{
    ClientFieldMask result = new();
    if (selected.FirstName && a.FirstName != b.FirstName)
        result.FirstName = true;
    if (selected.LastName && a.LastName != b.LastName)
        result.LastName = true;
    if (selected.BelongsToGroups &&
        !a.BelongsToGroups.SequenceEqual(b.BelongsToGroups))
        result.BelongsToGroups = true;
    return result;
}
```

[00:15:37](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=937s) În demonstrație, eliminați verificările de selecție și păstrați comparațiile de valori. Aceasta reduce numărul de condiții și produce un diff complet memorabil, care poate fi filtrat ulterior. Acestea sunt două proiectări: selecția în timpul calculului sare comparațiile irelevante; selecția după calcul reutilizează rezultate deja calculate.

[00:15:56](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=956s) Odată memorat diff-ul, întrebarea dacă prenumele coincide înseamnă inspectarea flag-ului său, nu compararea din nou a șirurilor. [00:16:16](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=976s) Inegalitatea dintre șiruri compară succesiunea de caractere a șirului, astfel încât accentele sau alte semne diacritice fac nume altfel asemănătoare să fie diferite. [00:16:35](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=995s) Ignorarea lor necesită reguli explicite de comparație; inegalitatea obișnuită nu le normalizează în mod tacit.

[00:16:41](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1001s) Centralizați asemenea reguli în implementarea comparației. [00:16:51](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1011s) O inegalitate inline fără legătură ar urma în continuare regula obișnuită, deci toate locurile de apel care au nevoie de regula personalizată trebuie să reutilizeze aceeași comparație. [00:17:05](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1025s) Extragerea acelei comparații evită duplicarea.

[Helperul FirstNameEquals și apelul său Diff din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); helper și apelant simplificate, corectate:

```csharp
static bool FirstNameEquals(Client a, Client b) =>
    a.FirstName == b.FirstName; // Adăugați aici orice normalizare intenționată.

static ClientFieldMask Diff(Client a, Client b)
{
    ClientFieldMask result = new();
    result.FirstName = !FirstNameEquals(a, b);
    result.LastName = a.LastName != b.LastName;
    result.BelongsToGroups =
        !a.BelongsToGroups.SequenceEqual(b.BelongsToGroups);
    return result;
}
```

Instantanele inspectate din sursă apelează `FirstNameEquals` fără negație la stabilirea flagului de diff pentru prenumele. Aceasta marchează egalitatea ca diferență și contrazice contractul de diff intenționat; exemplul de mai sus corectează acea polaritate. Helperul comis folosește `SequenceEqual`, care efectuează totuși o comparație obișnuită de succesiuni și nu implementează nicio regulă insensibilă la semne diacritice.

[00:17:16](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1036s) Un helper numit `CompareFirstName(a, b)` sau cel salvat `FirstNameEquals(a, b)` elimină comparația inline de la locul verificării. [00:17:24](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1044s) Locul de apel devine o verificare scurtă delegată. [00:17:37](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1057s) Abordarea cu masca mai mare adună întreaga diferență dintr-o dată. [00:17:39](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1059s) Apelanții pot ignora câmpurile selectate și [00:18:03](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1083s) pot reutiliza rezultatele memorate pentru prenumele și numele de familie fără a recalcula comparațiile.

## Programați comparații și egalitate selectivă

[00:18:21](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1101s) Un exemplu practic compară două documente de orare Word: un program mai vechi, valabil până pe nouă, și unul mai nou care conține modificări după acea dată. [00:18:35](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1115s) Citiți ambele documente și comparați celulele corespunzătoare pentru aceeași zi și oră, folosind diff pentru a vedea dacă disciplina sau alte atribute relevante s-au schimbat. Lecția descrie acest caz de utilizare; nu implementează aici parsarea documentelor.

[Diff și helperii AreSet de uz general din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); utilizare conceptuală simplificată, nu un parser de orar din repository:

```csharp
var differences = Functions.Diff(oldItem, newItem);
var relevant = new ClientFieldMask { FirstName = true, LastName = true };
bool allSelectedDiffer = Functions.AreSet(differences, relevant);
```

[00:18:44](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1124s) O selecție de comparație stabilește ce câmpuri contează. Ea poate cere ca toate câmpurile selectate să difere, dar aceasta este o condiție a apelantului, nu definiția diff-ului. [00:18:47](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1127s) După ce diff raportează fiecare comparație, apelantul își alege condiția. „Cel puțin un câmp relevant diferă”, „toate câmpurile relevante diferă” și „toate câmpurile relevante coincid” sunt întrebări distincte.

[00:18:56](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1136s) Altă utilizare este fuzionarea unor intrări de orar într-o singură celulă. Intrările care au același sala, aceeași zi a săptămânii și aceeași oră pot fi combinate chiar dacă grupul diferă. Selectați acele trei atribute și ignorați grupul. Egalitatea pe atributele selectate înseamnă că niciun flag de diff al lor nu este true.

[00:19:16](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1156s) Eliminarea unui câmp din selecție îl elimină din criteriile de comparație. [00:19:26](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1166s) Un helper de diff complet poate calcula în continuare toate rezultatele; interogarea ulterioară a măștii ignoră flagurile irelevante. În proiectarea anterioară cu diff selectat, comparațiile irelevante sunt chiar sărite. Ambele servesc egalității selective, dar costurile lor de calcul diferă.

## Interogări any și all asupra unei diferențe

[00:19:47](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1187s) Un exemplu afișează „Might be siblings” când prenumele diferă, dar numele de familie coincid. Acesta este un mesaj semnificativ pentru aplicația dată, nu o dovadă a unei relații de familie. [Condiția de frați din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); verificare simplificată folosind un diff cu polaritatea corectată:

```csharp
var differences = Functions.Diff(a, b);
if (differences.FirstName && !differences.LastName)
    Console.WriteLine("Might be siblings");
```

[00:19:54](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1194s) Memorați câmpurile de inspectat într-o variabilă, ca aceeași funcție de interogare să poată servi reguli diferite. [00:19:57](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1197s) Denumirea acelei selecții face vizibilă politica apelantului, în loc să îngroape verificările de câmpuri în helper. [00:20:09](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1209s) O **mască de verificare** este doar o altă valoare de mască de câmp, de exemplu care selectează prenumele și numele de familie; nu trebuie să introducă un tip separat.

[00:20:26](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1226s) O operație de tip Without poate exprima dacă este prezentă **orice** diferență selectată: ștergerea câmpurilor selectate modifică masca de diferențe inițială doar dacă a existat o suprapunere. [00:20:31](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1231s) Cerința ca **toate** câmpurile selectate să fie îndeplinită necesită o altă interogare. [00:20:37](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1237s) Pentru fiecare flag obligatoriu, raportați eșecul când acesta nu este setat în masca rezultatului. Un câmp neobligatoriu nu influențează răspunsul.

[AreSet boolean și Without din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs); interogări de mască simplificate, echivalente:

```csharp
var required = new ClientFieldMask { FirstName = true, LastName = true };
bool any = Functions.Without(differences, required) != differences;
bool all = Functions.Without(required, differences) == default;
// AreSet(differences, required) exprimă și ea "all".
```

[00:20:58](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1258s) Helperul inițial pentru toate flagurile duplică în mod conștient verificările pe câmpuri. Reprezentările ulterioare, indexate și bitwise le vor restrânge. [00:21:14](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1274s) Extragerea câmpurilor verificate într-o variabilă transformă regula în date, nu într-o structură de condiții inline. [00:21:23](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1283s) O regulă care caută diferențe nu va identifica o dublură exactă când ambele nume sunt egale: nu există flaguri de diferență de nume care să o satisfacă. [00:21:35](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1295s) Variabila de selecție este punctul de pornire pentru optimizarea interogării fără a schimba apelanții.

Mesajul comis „Either first or last name are different” este înșelător atunci când este protejat de `AreSet` cu ambele flaguri selectate: acel helper cere **ambele**, nu oricare. Mesajul demonstrației și predicatul său de toate flagurile nu trebuie confundate.

[00:21:40](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1300s) Următoarea tehnică se aplică și unor bool-uri simple sau unor valori de același tip din altă clasă; măștile sunt doar o aplicare. Aceste variații mai ample sunt lăsate pentru mai târziu.

<details>
<summary>Dacă diferă doar prenumele și ambele nume sunt selectate, ce returnează any și all?</summary>

Any returnează true; all retornează false. Egalitatea exactă a ambelor nume ar face ca ambele interogări de diferență să fie false. O verificare a egalității întreabă în schimb dacă nu există diferențe selectate.

</details>

## Dă fiecărui câmp un indice

[00:22:15](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1335s) Atribuiți fiecărui câmp comparabil un număr: **indicele său de câmp**. O intrare denumită de enum identifică fiecare indice, permițând o parcurgere generică în locul ramurilor scrise manual cu numele câmpurilor în tot programul. [00:22:25](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1345s) Enumerați câmpurile într-un enum de câmpuri, cu pozițiile lor. [00:22:40](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1360s) Includeți un contor, ca o buclă să știe câte câmpuri există.

[Enumul ClientField și getterul indexat din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/80eddb6786130b733401932cadfb35f5c939d8f4/examples/field_masks/Program.cs); declarație simplificată a indicilor:

```csharp
enum ClientField
{
    FirstName = 0,
    LastName = 1,
    BelongToGroups = 2,
    Count,
    FirstField = FirstName
}
```

`Count` este un marcaj de limită, nu un câmp de client utilizabil. `FirstField` este un alias pentru membrul de început. Bucla introdusă mai târziu se bazează pe indici consecutivi începând de la zero; dacă acea reprezentare se schimbă, limitele ei trebuie reconsiderate și ele.

[00:22:47](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1367s) Adăugați un helper care citește sau scrie un boolean folosind identificatorul de câmp. [00:23:03](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1383s) Masca rămâne o colecție neordonată de alegeri da/nu; indicii oferă adresare, nu ordinea ieșirii. [00:23:09](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1389s) Fiecare identificator utilizabil se referă la exact un câmp concret.

[Maparea Get din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/80eddb6786130b733401932cadfb35f5c939d8f4/examples/field_masks/Program.cs); cazuri valide simplificate:

```csharp
static bool Get(ClientFieldMask m, ClientField field)
{
    switch (field)
    {
        case ClientField.FirstName: return m.FirstName;
        case ClientField.LastName: return m.LastName;
        case ClientField.BelongToGroups: return m.BelongsToGroups;
        default: throw new ArgumentException("Invalid value", nameof(field));
    }
}
```

[00:23:15](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1395s) Scrieți o singură dată maparea de la identificator la câmpul concret în această operație de acces. [00:23:24](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1404s) Funcțiile de combinare pot apoi să o reutilizeze, în loc să repete legarea. Scrierea are propria mapare corespunzătoare cât timp starea este în continuare bool-uri separate; reprezentarea întreagă ulterioară înlocuiește acele mapări cu aritmetică pe indici.

## Impuneți contractul indicelui de câmp

[00:23:42](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1422s) Un **contract** enunță ce intrări sunt permise și ce ieșiri sunt promise. Tipurile exprimă o parte din el: acest getter acceptă un identificator de câmp și întoarce un boolean. [00:23:57](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1437s) Dar un enum nu garantează un membru denumit. Un apelant poate converti un întreg arbitrar, precum `(ClientField)10`, și să îl paseze helperului.

[00:24:22](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1462s) Detectați imediat un identificator ilicit în funcția care îl primește. [00:24:34](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1474s) Două mecanisme discutate sunt o asertiune și o excepție:

- [00:24:41](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1481s) O **asertiune** prinde încălcarea unei ipoteze în timpul dezvoltării.
- [00:24:57](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1497s) Intenția este găsirea greșelilor de programare, precum un indice de câmp invalid, și repararea lor în timpul dezvoltării.
- [00:25:13](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1513s) Apelurile la `Debug.Assert` și `Debug.Fail` lipsesc în mod normal dintr-o compilare release, deoarece sunt condiționate de `DEBUG`. Mai precis, simbolul de compilare controlează eliminarea; „producția nu are niciun fel de asertiuni” ar fi prea general.
- [00:25:26](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1526s) O **excepție** rămâne o cale de eroare în compilările release și poate fi captată și tratată. O excepție netratată poate termina programul.

[00:25:39](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1539s) **Ramura implicită** a switch-ului gestionează identificatorii care nu sunt potriviți de niciun caz explicit. [00:25:45](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1545s) Ea poate arunca `ArgumentException` sau `ArgumentOutOfRangeException`. [00:25:58](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1558s) Construiți excepția cu `nameof(field)` drept nume de parametru și un mesaj precum „Invalid value”. `nameof` produce numele identificatorului fără a hardcoda un șir. [00:26:05](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1565s) Înregistrarea consideră excepția de index în afara domeniului mai precisă, dar nu decide; getterul comis păstrează `ArgumentException`.

[Ramura implicită a getterului din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/80eddb6786130b733401932cadfb35f5c939d8f4/examples/field_masks/Program.cs); cale de excepție păstrată, simplificată:

```csharp
default:
    throw new ArgumentException(
        paramName: nameof(field),
        message: "Invalid value");
```

[00:26:18](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1578s) Varianta alternativă folosește `Debug.Assert(condition, message)`. [00:26:36](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1596s) Pentru o ramură despre care se știe deja că indică eșec, `Debug.Fail(message)` este diagnosticul necondiționat; API-ul nu este `Debug.Assert.Fail`. [Documentația Debug.Fail](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.debug.fail?view=net-9.0) confirmă atât numele său, cât și compilarea condiționată.

[Instantaneul getterului cu alternativa Debug.Fail comentată](https://github.com/AntonC9018/uniCourse_csharp/blob/80eddb6786130b733401932cadfb35f5c939d8f4/examples/field_masks/Program.cs); ramură reconstruită, doar cu asertiune:

```csharp
default:
    Debug.Assert(false, "Invalid value"); // Sau Debug.Fail("Invalid value").
    return false;
```

[00:26:38](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1598s) O funcție care întoarce bool are în continuare nevoie de un return sau de un throw după diagnostic. Apelul poate fi eliminat, iar prezența lui nu reprezintă o ieșire garantată din fluxul de control. [00:26:54](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1614s) O ramură care nu este atinsă în timpul dezvoltării poate totuși rula în programul livrat. [00:26:57](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1617s) Testele reușite nu dovedesc că un cod este inaccesibil: un apelant neanticipat poate furniza o valoare invalidă.

[00:27:06](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1626s) Ambele mecanisme pot fi combinate: diagnosticați eroarea de dezvoltare, apoi aruncați pentru o eșec la runtime. Înregistrarea descrie o prăbușire imediată; tratamentul efectiv al asertiunilor depinde de runtime și de depanator, deci o asertiune nu este un substitut pentru excepție. [Documentația asertiunilor](https://learn.microsoft.com/en-us/visualstudio/debugger/assertions-in-managed-code?view=visualstudio) descrie tratarea diagnostică; referința Debug.Fail precizează comportamentul ei condiționat. Alegerea finală aici este păstrarea excepției.

<details>
<summary>De ce este necesară o ramură implicită când tipul parametrului este un enum?</summary>

Enumul poate conține valori numerice fără nume. Ramura implicită impune contractul de intrări permise și respinge totodată marcajul de limită Count atunci când acesta nu este un câmp concret.

</details>

## Adăugați scrierea indexată

[00:27:22](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1642s) După ce citirea este completă, adăugați un **setter**, o operație care atribuie booleanul furnizat câmpului identificat. [00:27:27](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1647s) Întoarcerea unei referințe către un câmp ar putea combina citirea și scrierea printr-o singură facilitate avansată a limbajului C#, dar sintaxa de întoarcere prin referință este intenționat în afara acestei lecții.

[00:27:28](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1648s) Setterul inițial face atribuirea și nu întoarce nimic. [00:27:58](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1678s) Eliminați tipul de returnare bool copiat: un setter nu întoarce flagul pe care tocmai l-a scris. Un tip de returnare `void` exprimă acest lucru.

[Maparea indexată Set din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/80eddb6786130b733401932cadfb35f5c939d8f4/examples/field_masks/Program.cs); reconstrucția versiunii mutante anterioare:

```csharp
static void Set(ref ClientFieldMask mask, ClientField field, bool value)
{
    switch (field)
    {
        case ClientField.FirstName: mask.FirstName = value; break;
        case ClientField.LastName: mask.LastName = value; break;
        case ClientField.BelongToGroups: mask.BelongsToGroups = value; break;
        default: throw new ArgumentException("Invalid value", nameof(field));
    }
}
```

Aici **transmiterea prin referință** (`ref`) permite operației să actualizeze starea apelantului; atribuirea unui parametru obișnuit de tip valoare ar modifica doar copia sa. Aceasta este o reconstrucție explicativă, nu semnătura salvată. În proiectarea salvată ulterioară, `Set` întoarce o mască actualizată în loc să folosească `ref`. Întoarcerea unei măști este o API de transformare a valorilor, distinctă de întoarcerea unui flag boolean.

## Înlocuiți verificările repetate cu bucle

[00:28:07](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1687s) Refaceți locurile de apel prin acces indexat. [00:28:09](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1689s) Înlocuiți verificările scrise manual pe câmpuri cu o buclă peste fiecare câmp utilizabil. Funcțiile de combinare partajate nu mai au nevoie de o ramură suplimentară pentru fiecare câmp nou; definițiile câmpurilor și mapările indexate trebuie totuși păstrate consistente.

1. [00:28:16](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1696s) Porniți de la `ClientField.FirstField`. Denumirea marcajului de prim câmp evită împrăștierea membrului de început prin bucle.
2. [00:28:32](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1712s) Opriți-vă la `ClientField.Count`. Un marcaj separat de ultim câmp este inutil în această proiectare cu indici consecutivi, numerotați de la zero.
3. [00:28:54](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1734s) Cât timp indicele este sub contor, el identifică un câmp utilizabil. Citiți-i flagul cu getterul.
4. [00:29:04](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1744s) Efecuați verificarea sau scrierea necesară folosind acel indice în locul unui nume de câmp hardcodat.

[Buclele indexate AreSet și Without din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/80eddb6786130b733401932cadfb35f5c939d8f4/examples/field_masks/Program.cs); buclă corectată pentru toate flagurile, simplificată:

```csharp
static bool AreSet(ClientFieldMask mask, ClientFieldMask required)
{
    for (var field = ClientField.FirstField; field < ClientField.Count; field++)
        if (Get(required, field) && !Get(mask, field))
            return false;
    return true;
}
```

Instantaneul indexat întoarce false când `Get(mask, i)` este true în interiorul unei ramuri de câmp obligatoriu. Lipsa negației inversează verificarea intenționată; exemplul folosește `!Get(mask, field)`. Helperul boolean anterior și helperul bitwise ulterior exprimă amândouă contractul tuturor flagurilor obligatorii.

[00:29:22](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1762s) Un **iterator**, care furnizează o succesiune de câmpuri utilizabile, ar putea face enumerarea mai curată și ar elimina tratamentul special al primului câmp. [00:29:27](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1767s) Nu este implementat, deoarece iteratorii sunt un subiect separat al limbajului.

[00:29:33](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1773s) Operația de ștergere folosește aceeași enumerare, dar masca ei actualizată trebuie să ajungă la apelant. Deoarece un parametru struct este copiat, întoarceți noua valoare sau transmiteți starea prin referință. [00:29:38](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1778s) Este aleasă întoarcerea măștii ca abordare mai simplă, evitând dependența de sintaxa `with` din C#.

[Set care întoarce valoare și Without din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/80eddb6786130b733401932cadfb35f5c939d8f4/examples/field_masks/Program.cs); buclă de scădere simplificată:

```csharp
static ClientFieldMask Without(ClientFieldMask mask, ClientFieldMask toDelete)
{
    for (var field = ClientField.FirstField; field < ClientField.Count; field++)
    {
        if (Get(toDelete, field))
            mask = Set(mask: mask, field: field, value: false);
    }
    return mask;
}
```

[00:29:47](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1787s) Fiecare apel de setter produce masca cu un flag indexat schimbat, în true sau false, după cum se cere. [00:29:55](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1795s) Argumentele denumite fac rolurile explicite: care mască, care câmp și ce valoare. [00:30:01](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1801s) Golirea unui flag cunoscut are același efect ca o **expresie with**, adică o copie modificată precum `mask with { FirstName = false }`.

[00:30:06](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1806s) Limitarea importantă este selectarea unui membru printr-un indice calculat la runtime: un inițializator de membru denumit nu permite unei variabile de tip enum să stea în locul numelui unei proprietăți. O expresie `with` este legală în interiorul unei funcții; poziționarea funcției nu este o interdicție a limbajului. Vezi [referința despre expresii with](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/with-expression). Acțiunile de afișare specifice câmpurilor ale printerului rămân, de asemenea, diferite de simpla citire sau scriere a unui flag boolean.

[00:30:12](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1812s) [00:30:24](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1824s) Parcurgerea câmpurilor printr-o abstractizare identificator-la-valoare este un tipar general folosit și în afara măștilor, cu alte exemple amânate.

## Tiparul depășește măștile

[00:30:31](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1831s) O setare frecventă este o clasă cu mai multe câmpuri de același tip și un enum care denumește fiecare câmp. O singură buclă poate procesa acele valori prin acces indexat comun.

[00:30:42](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1842s) acel tip valoare comun nu trebuie să fie bool. Un **tip generic** exprimă cod parametrizat după un tip, astfel încât aceeași idee poate opera pe valori de alt tip. [00:30:58](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1858s) Genericele și parcurgerea câmpurilor sunt mai ample decât măștile de câmpuri; această înregistrare nu dezvoltă o implementare generică. Exemplul lucrat rămâne masca booleană.

## Proprietăți ascund o altă reprezentare

[00:31:09](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1869s) Obiectivul este acum să memorăm toate flagurile booleene ca biți ai unui singur număr, păstrând un acces confortabil. **Getterii** citesc flagurile logice; **setterii** le scriu. O **proprietate** expune cele două operații cu sintaxă asemănătoare câmpurilor, în timp ce un **câmp de susținere** păstrează reprezentarea efectivă.

[00:31:21](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1881s) În Java s-ar scrie în mod obișnuit metode precum `getFirstName` și `setFirstName`. [00:31:34](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1894s) C# combină accesorii de citire și scriere sub un singur nume de proprietate. Setterul său primește valoarea atribuită prin parametrul implicit `value`. [00:31:34](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1894s) Acea valoare este un bool aici, pentru că proprietatea este un flag boolean.

[Proprietatea FirstName și câmpul de susținere întreg din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/6b312db7c3e158e27048d8921f426054c4a1b2b0/examples/field_masks/Program.cs); forma simplificată a proprietății:

```csharp
internal int _value;

public bool FirstName
{
    get => Get(ClientField.FirstName);
    set => Set(ClientField.FirstName, value);
}
```

[00:31:44](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1904s) Atribuirea lui `mask.FirstName = true` invocă setterul implicit; [00:31:54](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1914s) citirea proprietății invocă getterul ei. Aceasta înlocuiește apelurile explicite de metode în stil Java cu sintaxă obișnuită de proprietate. [00:32:01](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1921s) Corpurile accesorilor determină în continuare ce se întâmplă efectiv.

[00:32:12](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1932s) Memorați flagurile în `_value` ca pe o **mască de biți**: un bit setat înseamnă true, iar un bit golit înseamnă false. [00:32:21](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1941s) Pozițiile succesive reprezintă câmpuri succesive. [00:32:28](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1948s) Indicele fiecărui câmp furnizează poziția bitului său, deci operațiile indexate pot adresa fiecare flag. În enumul comis, prenumele este poziția 0, numele de familie poziția 1, iar grupurile poziția 2.

## Setați și ștergeți un bit

[00:32:43](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1963s) Folosiți indicele de câmp drept poziția bitului său. [00:32:49](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1969s) Alte atribuiri de indici sunt posibile, dar o mapare consistentă unu-la-unu evită coliziunile dintre flaguri. [00:32:58](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1978s) Setterul alege o ramură de setare a bitului pentru true și o ramură de golire a bitului pentru false.

[Implementarea indexată Set din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/6b312db7c3e158e27048d8921f426054c4a1b2b0/examples/field_masks/Program.cs); setter simplificat:

```csharp
public void Set(ClientField field, bool value)
{
    int bit = 1 << (int)field;
    if (value)
        _value |= bit;
    else
        _value &= ~bit;
}
```

[00:33:02](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1982s) **Deplasarea la stânga** (`<<`) mută unicul 1 pe poziția dorită. **OR pe biți** (`|`) setează acel bit și lasă fiecare alt bit neschimbat; `|=` atribuie înapoi valoarea combinată. [00:33:09](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=1989s) Pentru false, folosiți aceeași valoare de un singur bit pentru a goli doar poziția țintă.

[00:33:59](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2039s) [00:34:02](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2042s) Golirea folosește **AND pe biți** (`&`) cu **complementul** (`~`) măștii de un singur bit. Complementul are zero pe țintă și unu peste tot în rest, deci toți biții care nu sunt ținta supraviețuiesc.

[00:34:09](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2049s) Lucrați cu o valoare scrisă ca `101`. [00:34:23](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2063s) Construiți `v` deplasând 1 pe poziția flagului, apoi complementați-l. [00:34:25](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2065s) Diagrama de patru biți din video arată `0111` ca complement; aceasta corespunde unei măști de un singur bit `1000`, cu bitul selectat desenat în stânga. Nu este complementul poziției zero `0001`. Folosiți o convenție constantă pentru biți într-un calcul concret.

[Logica de setare și golire a biților din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/6b312db7c3e158e27048d8921f426054c4a1b2b0/examples/field_masks/Program.cs); cei mai puțin patru biți, prenumele la poziția comisă 0:

```text
value:       0101
v = 1 << 0: 0001
~v:          1110
value & ~v:  0100
```

[00:34:33](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2073s) AND păstrează un 1 doar acolo unde ambii operanzi au 1. [00:34:50](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2090s) Dacă oricare operand are zero, bitul rezultatului este zero. Astfel ținta devine zero, iar fiecare altă poziție copiază valoarea inițială. Diagramele arată doar câțiva biți; complementul real al întregului inversează și ceilalți biți ai întregului.

[00:34:57](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2097s) Pentru că această logică stă în spatele unei proprietăți, apelanții pot atribui proprietatea. [00:35:05](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2105s) [00:35:21](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2121s) Scrierea lui `FirstName = true` invocă accesorul de scriere al proprietății cu `value` egal cu true, deci rulează ramura true. [00:35:32](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2132s) Aceasta este zahăr sintactic pentru invocarea accesorului, nu o operație de stocare independentă. Formularea în stil Java „SetFirstName” din înregistrare descrie operația echivalentă; proprietatea C# comisă deleagă către metoda indexată `Set`.

<details>
<summary>Anticipați: golirea prenumelui golește și grupurile?</summary>

Nu. Cu prenumele la bitul 0 și grupurile la bitul 2, 0101 devine 0100. Complementul are un 1 la bitul 2, deci flagul de grupuri este păstrat.

</details>

## Citiți un bit

[00:35:40](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2140s) **Getterul** are nevoie de aceeași mască de un singur bit ca setterul. [00:35:45](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2145s) Aplicați AND între valoarea de susținere și acea mască, eliminând toate pozițiile în afară de flagul selectat.

[Implementarea indexată Get din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/6b312db7c3e158e27048d8921f426054c4a1b2b0/examples/field_masks/Program.cs); getter simplificat:

```csharp
public bool Get(ClientField field)
{
    int bit = 1 << (int)field;
    return (_value & bit) != 0;
}
```

[00:35:54](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2154s) Dacă flagul lipsește, rezultatul AND este zero. [00:36:03](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2163s) Dacă este prezent, rezultatul este valoarea nenulă de un singur bit. Getterul transformă acel rezultat numeric într-un boolean.

[00:36:09](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2169s) Cu valoarea `101` și prenumele la poziția 0, rezultatul selectat este 1. [00:36:21](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2181s) Zerourile măștii impun zerouri rezultatului, iar singura poziție permisă copiază bitul valorii:

```text
value:       0101
bit:         0001
value & bit: 0001  -> true

value:       0100
bit:         0001
value & bit: 0000  -> false
```

[00:36:30](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2190s) Un rezultat nenul înseamnă că flagul inițial era setat; zero înseamnă că nu era. [00:36:36](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2196s) Motivul este că un zero în valoare, pe poziția selectată, nu poate deveni unu prin AND. [00:36:42](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2202s) O singură expresie acoperă ambele cazuri, deci getterul funcționează pentru orice configurație validă a măștii.

## Partajați logica de biți între proprietăți

[00:36:46](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2206s) Fiecare flag are nevoie de aceleași operații de citire/scriere pe biți; doar indicele său de câmp se schimbă. Duplicarea acelor operații în fiecare proprietate ar recrea problema de mentenanță.

1. [00:37:01](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2221s) Extrageți helperi indexați pentru setarea și citirea biților.
2. [00:37:10](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2230s) Dați setterului un identificator de câmp și un boolean atribuit.
3. [00:37:20](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2240s) Transmiteți câmpul denumit pentru a reutiliza logica de manipulare.
4. [00:37:28](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2248s) Reduceți setterul fiecărei proprietăți la un singur apel de helper.
5. [00:37:30](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2250s) Folosiți un **accesor cu expresie**, scris cu `=>`, când o singură expresie este suficientă. Getterii acceptă aceeași formă.

[Helperii indexați și cele trei wrapper-uri de proprietăți din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/6b312db7c3e158e27048d8921f426054c4a1b2b0/examples/field_masks/Program.cs); wrapper-uri de proprietăți simplificate:

```csharp
public bool FirstName
{
    get => Get(ClientField.FirstName);
    set => Set(ClientField.FirstName, value);
}
public bool LastName
{
    get => Get(ClientField.LastName);
    set => Set(ClientField.LastName, value);
}
public bool BelongsToGroups
{
    get => Get(ClientField.BelongToGroups);
    set => Set(ClientField.BelongToGroups, value);
}
```

[00:37:41](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2261s) Getterul/helperul poate fi exprimat și el compact. [00:37:46](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2266s) [00:37:50](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2270s) Citiți din câmpul de susținere curent, care rămâne modificabil prin setteri; sintaxa compactă nu îngheață valoarea. [00:37:54](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2274s) Getterul primește doar identificatorul de câmp, deoarece citirea nu are nevoie de un boolean atribuit.

[00:38:03](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2283s) Wrapper-urile de proprietăți repetă în continuare numele câmp-uri-indice. Un **generator de surse** ar putea genera această API repetitivă în timpul compilării. Lecția lasă această tehnică avansată în afara domeniului; faptul nu dovedește că nicio altă proiectare de API n-ar putea evita wrapper-urile denumite. [00:38:17](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2297s) Pentru moment wrapper-urile rămân scrise manual, în timp ce logica lor partajată de biți este scrisă o singură dată.

## Expuneți accesul indexat pe mască

[00:38:24](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2304s) Toate flagurile sunt acum într-un singur întreg. [00:38:26](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2306s) Setarea unui flag nu depinde de setterul altei proprietăți; helperul indexat poate exista de sine stătător. [00:38:32](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2312s) Faceți setterul public, alături de citirea indexată, astfel încât atât codul intern, cât și apelanții să poată lucra cu un identificator de câmp.

[Membrii publici finali Get și Set din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/6b312db7c3e158e27048d8921f426054c4a1b2b0/examples/field_masks/Program.cs); apelant simplificat:

```csharp
var mask = new ClientFieldMask();
mask.Set(ClientField.FirstName, true);
bool selected = mask.Get(ClientField.FirstName);
```

[00:38:44](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2324s) `Set` actualizează variabila de mască existentă în loc să întoarcă o mască de înlocuire. Această metodă de instanță mută starea unei variabile struct. Transmiterea măștii după valoare altei funcții o copiază în continuare; mutarea logicii pe mască nu elimină semantica valorilor.

[00:38:51](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2331s) Vechiul getter extern de dispecerizare pe câmpuri nu mai este necesar, acum că masca poate citi direct biții. [00:39:00](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2340s) Ștergeți acea logică de mapare rămasă. [00:39:06](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2346s) Rescrieți locurile de apel pentru a invoca metodele indexate ale măștii. Proprietățile denumite rămân wrapper-uri utile; dispare doar stratul de acces separat, bazat pe switch.

Un detaliu de contract se schimbă în instantaneul bitwise salvat: `Get` și `Set` nu își validează identificatorii de câmp înainte de deplasare. Ei presupun un indice de câmp utilizabil. Discuția anterioară despre intrările invalide rămâne relevantă; o proiectare care promite același comportament de excepție trebuie să păstreze validarea, în loc să presupună că enumul face valorile imposibile.

## Înlocuiți scăderea măștii cu o singură operație

[00:39:13](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2353s) Cu toate flagurile împachetate într-un întreg, scăderea nu mai are nevoie de o buclă. [00:39:20](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2360s) Aplicați AND între masca inițială și inversul măștii de ștergere. [00:39:33](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2373s) Eliminați bucla indexată: o singură expresie pe biți efectuează aceeași golire.

[Implementarea finală Without din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/6b312db7c3e158e27048d8921f426054c4a1b2b0/examples/field_masks/Program.cs); versiune simplificată:

```csharp
static ClientFieldMask Without(ClientFieldMask mask, ClientFieldMask toDelete)
{
    mask._value &= ~toDelete._value;
    return mask;
}
```

Operația întoarce în continuare masca modificată, deoarece parametrul ei este o valoare copiată. Semnificația ei externă rămâne „păstrează câmpurile cerute, cu excepția celor numite pentru ștergere”.

[00:39:41](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2381s) Primul exemplu: cereți prenumele și numele de familie, apoi ștergeți prenumele și grupurile. În diagramele următoare, coloanele merg de la stânga la dreapta în ordinea prun nume, nume de familie, grupuri, ca rolurile selecției să fie ușor de citit. Acestea sunt coloane logice de câmpuri, nu afișarea convențională de la bitul cel mai înalt la cel mai jos a întregului memorat.

```text
requested:  1 1 0
delete:     1 0 1
~delete:    0 1 0
result:     0 1 0
```

[00:39:52](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2392s) Tilda marchează fiecare poziție care nu se șterge ca fiind disponibilă. [00:40:02](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2402s) Poziția finală a grupurilor devine și ea zero; ea nu a fost cerută niciodată, deci ștergerea acolo nu schimbă nimic. Supraviețuiește doar numele de familie.

[00:40:07](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2407s) Al doilea exemplu: cereți tot și ștergeți doar prenumele. [00:40:13](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2413s) Copiați fiecare altă poziție neschimbată:

```text
requested:  1 1 1
delete:     1 0 0
~delete:    0 1 1
result:     0 1 1
```

[00:40:21](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2421s) Puneți munca brută pe biți în metoda denumită. [00:40:28](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2428s) Operatorii bruti sunt mai greu de citit pentru apelanți decât `Without`. [00:40:37](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2437s) **Încapsularea** ascunde atât reprezentarea întreagă, cât și operația de implementare, în spatele acelui nume semnificativ. [00:40:49](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2449s) Ea face și mai puțin lucru repetat decât parcurgerea câmpurilor unul câte unu; lecția face acest argument de număr de operații, nu o afirmație de benchmark.

## Verificați dintr-o dată toate flagurile obligatorii

[00:40:59](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2459s) Optimizarea finală este predicatul de toate flagurile. Luați biții comuni dintre masca efectivă și masca obligatorie. [00:41:08](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2468s) Dacă acei biți diferă de masca obligatorie, cel puțin o cerință lipsește. [00:41:19](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2479s) Dacă sunt egali cu ea, fiecare flag obligatoriu este prezent.

[Implementarea finală AreSet din commitul corespunzător](https://github.com/AntonC9018/uniCourse_csharp/blob/6b312db7c3e158e27048d8921f426054c4a1b2b0/examples/field_masks/Program.cs); predicat simplificat:

```csharp
static bool AreSet(ClientFieldMask mask, ClientFieldMask required) =>
    (mask._value & required._value) == required._value;
```

[00:41:26](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2486s) Diagrama lucrată cere două poziții și le ignoră pe celelalte. Ca în diagrama din video și din sursă, acestea sunt coloane de flaguri afișate:

```text
actual:      1 1 0 1
required:    1 1 0 0
common:      1 1 0 0  -> equals required, true

actual:      1 0 0 1
required:    1 1 0 0
common:      1 0 0 0  -> differs from required, false
```

[00:41:39](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2499s) Pozițiile a treia și a patra sunt aduse la zero de masca obligatorie, chiar dacă masca efectivă are un flag acolo. Doar pozițiile obligatorii contribuie. [00:41:46](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2506s) Când fiecare poziție obligatorie a fost 1, fiecare copiază un 1, iar rezultatul coincide exact cu cerința.

[00:41:53](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2513s) Dacă chiar și o singură poziție obligatorie a fost 0, acel zero supraviețuiește în masca comună, iar egalitatea eșuează. [00:42:01](https://www.youtube.com/watch?v=BmdWiUEgwNw&t=2521s) Acea egalitate este regula de decizie completă: „toate flagurile obligatorii sunt setate”. Nu este un test de tip „any”.

<details>
<summary>De ce comparăm biții comuni cu masca obligatorie și nu cu masca efectivă?</summary>

Masca efectivă poate conține flaguri suplimentare pe care apelantul le ignoră explicit. Masca obligatorie conține exact obligațiile care trebuie îndeplinite. Egalitatea cu ea dovedește că niciuna dintre acele obligații nu lipsește.

</details>

Lecția se încheie cu o selecție configurabilă, rezultate de diferență reutilizabile și operații denumite peste o reprezentare compactă. Dispuneri alternative de domeniu, sintaxa de întoarcere prin referință, iteratorii, un container generic generalizat de câmpuri, generarea de surse și parsarea documentelor rămân în afara exemplului implementat.

## Istoricul modificărilor de cod

Acestea sunt etapele relevante salvate, în ordinea construirii. Ele păstrează grupuri de editări live, nu câte un commit separat pentru fiecare timestamp:

- [ef4630d — exemplu cu mască booleană](https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs): memorează câte un bool pentru fiecare câmp și include afișare configurabilă, scădere, diff și interogări de toate flagurile pe câmpuri; polaritatea diff-ului pentru prenumele și mesajul „either” al apelantului necesită corecțiile explicate mai sus.
- [80eddb6 — acces indexat la câmpuri](https://github.com/AntonC9018/uniCourse_csharp/blob/80eddb6786130b733401932cadfb35f5c939d8f4/examples/field_masks/Program.cs): adaugă enumul de câmpuri, getterul validat, setterul care întoarce valoare și buclele pe câmpuri; bucla salvată pentru toate flagurile are inconsistența negației lipsă, menționată mai sus.
- [6b312db — reprezentare bitwise](https://github.com/AntonC9018/uniCourse_csharp/blob/6b312db7c3e158e27048d8921f426054c4a1b2b0/examples/field_masks/Program.cs): împachetează flagurile într-un singur întreg, oferă acces indexat și wrapper-uri de proprietăți și înlocuiește scăderea și iterarea pentru toate flagurile cu operații pe biți.

Exerciții reprezentative simplificate din acele etape legate:

```csharp
// Etapa booleană: un flag memorat pentru fiecare câmp.
public bool FirstName;

// Etapa indexată: leagă identificatorul de booleanul său concret.
case ClientField.FirstName: return m.FirstName;

// Etapa bitwise: selectează bitul indexat și îl testează.
return (_value & (1 << (int)field)) != 0;
```
