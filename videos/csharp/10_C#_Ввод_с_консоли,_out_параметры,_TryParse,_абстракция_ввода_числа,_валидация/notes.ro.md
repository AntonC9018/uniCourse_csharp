> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Intrare din consolă, parametri de ieșire și validare

## Scop și termeni

[00:00:00](https://www.youtube.com/watch?v=SSTFFX5heuY&t=0s) Obiectivul este să transformăm textul introdus în consolă în numere utilizabile, fără să repornim programul după fiecare greșeală de tastare. Construcția evoluează de la o citire blocantă, la conversie, la un parametru de ieșire, la o buclă de reluare, la o funcție reutilizabilă de citire și, în final, la validarea mai multor valori împreună.

Vocabularul necesar acestei construcții este:

- **Intrare și ieșire din consolă:** text primit din consolă sau trimis către consolă. **Familia de metode Console** include funcții precum **Console.ReadLine**, care citește o linie; **Console.Write**, care afișează fără a adăuga sfârșit de linie; și **Console.WriteLine**, care afișează și adaugă sfârșit de linie.
- **Execuție secvențială și blocare:** instrucțiunile rulează în mod normal una după alta, de sus în jos. O instrucțiune blocantă așteaptă un eveniment înainte ca instrucțiunea următoare să poată rula.
- **string, int și bool:** un string este text, un int reține un număr întreg, iar un bool reține adevărat sau fals. Tastarea cifrelor nu transformă automat un string într-un int.
- **Intrare nullable, null și intrare redirecționată:** un string nullable, scris **string?**, poate conține text sau null, adică „nicio valoare de tip string”. Intrarea redirecționată vine dintr-o sursă precum un fișier, nu din consola live. **Sfârșitul intrării** înseamnă că acea sursă nu mai are linii.
- **Operatorul de anulare a nulului (null-forgiving operator):** semnul **!** postfix îi spune analizei de nullabilitate din compilator să trateze o expresie ca fiind nenulă. El suprimă o avertizare; nu furnizează o valoare lipsă.
- **Concatenare și aritmetică:** concatenarea unește string-uri; aritmetica efectuează operații numerice. Semnificația lui **+** depinde de tipurile operanzilor.
- **Parsare, int.Parse și excepții:** parsarea transformă o reprezentare textuală într-o valoare. int.Parse returnează un întreg pentru text numeric adecvat. O excepție este un semnal de eroare la runtime; o excepție ne tratată oprește acest program exemplu.
- **Parametru, argument și parametru out (de ieșire):** un parametru este o poziție de intrare sau de ieșire cu nume într-o declarație de funcție; un argument este ceea ce furnizează apelantul. Un parametru out permite unei funcții să inițializeze o variabilă care aparține apelantului.
- **Variabilă locală, stivă, adresă, referință și inițializare:** o variabilă locală aparține unui bloc sau unei funcții. Stiva este modelul lecției pentru stocarea temporară a apelurilor. O adresă identifică o locație de memorie; o referință oferă acces la acea locație. Inițializarea scrie prima valoare utilizabilă a variabilei.
- **Expresie de apel și valoare de retur:** apelarea unei funcții poate produce ea însăși o valoare. **return** încheie funcția și poate preda acea valoare apelantului. O funcție **void** nu are valoare de retur; un ajutor **static** poate fi apelat fără o instanță de obiect.
- **int.TryParse, declarație out inline și indicator de succes (flag):** TryParse încearcă conversia, scrie întregul prin out și returnează un bool care indică succesul. O declarație out inline creează acea variabilă de ieșire la locul apelului. Un indicator este o variabilă care memorează o stare da-sau-nu.
- **Domeniu de vizibilitate și durată de viață (scope și lifetime):** scope-ul stabilește unde poate fi folosit un nume; durata de viață privește cât timp este necesară stocarea sa. Mutarea codului într-un bloc sau într-o funcție schimbă variabilele pe care acesta le poate accesa.
- **Buclă și iterație:** o buclă repetă un corp; o iterație este o singură parcurgere a acestuia. **while** își verifică condiția înaintea corpului; **do-while** o verifică după corp. **while (true)** este o buclă infinită până când o acțiune explicită o părăsește.
- **Operatori de condiție:** **!** în față înseamnă negație logică a unui bool, iar **||** înseamnă „sau”. O condiție este o expresie al cărei rezultat adevărat-sau-fals controlează o ramură sau o buclă.
- **Ramură, etichetă, goto, continue și break:** o ramură **if** rulează când condiția ei este adevărată. O etichetă numește o destinație pentru goto. continue sare peste restul iterației curente; break iese din buclă.
- **Extracția funcției și abstractizare:** extragerea mută logica repetată într-o funcție. Abstractizarea rezultată îi oferă apelantului o operație cu nume, ascunzând pașii care o implementează.
- **Clasă, obiect, câmp, metodă și new:** o clasă descrie obiecte; un obiect reține valori în câmpuri; o metodă este o funcție asociată tipului sau obiectului. Expresia new creează un obiect.
- **Validare și reguli de validitate:** validarea verifică dacă valorile sunt corecte și permise pentru program. Faptul că o valoare poate fi parcă este o cerință; respectarea unor reguli precum „B trebuie să fie cel puțin de două ori A” este alta.
- **Depanare (debugging):** examinarea execuției și a valorilor intermediare pentru a afla de ce comportamentul diferă de cel intenționat.

[Laboratorul conex de citire din consolă](../../../labs/1_basic/08_input.md) oferă exerciții cu parsare, parametri out și citire repetată. Codul său de exercițiu este o referință de studiu, nu o succesiune salvată a editărilor din înregistrare.

[00:00:21](https://www.youtube.com/watch?v=SSTFFX5heuY&t=21s) Aria de validare de aici este un răspuns da-sau-nu returnat, cu mesaje afișate în timpul verificării. [00:00:37](https://www.youtube.com/watch?v=SSTFFX5heuY&t=37s) Numerele de eroare reprezentate prin constante, raportarea bazată pe excepții și returnarea mai multor erori ca date sunt propuse pentru video-uri ulterioare. Afișarea mai multor mesaje până la sfârșitul acestei lecții nu implementează o colecție structurată de erori. Și alte abordări pentru intrarea nullable sunt amânate.

Exemplele de mai jos sunt reconstrucții scurte ale stărilor din video-ul legat, cu nume descriptive acolo unde este util. Nu a fost identificat niciun cod sursă exact pentru această demonstrație în checkoutul și istoricul local de sursă inspectate, astfel că legăturile către stările din video identifică modificările și nu un commit fără legătură care adaugă un exercițiu. Execuțiile descrise aici sunt demonstrațiile înregistrate, nu rulări noi ale acestor reconstrucții.

## ReadLine blochează execuția secvențială

[00:00:46](https://www.youtube.com/watch?v=SSTFFX5heuY&t=46s) Începem cu trei instrucțiuni. Execuția secvențială înseamnă că prima se termină înaintea celei de-a doua, iar a doua înaintea celei de-a treia. [00:00:59](https://www.youtube.com/watch?v=SSTFFX5heuY&t=59s) Console.ReadLine este metoda de citire din consolă care citește linia introdusă de utilizator. Starea inițială simplificată din video este:

```csharp
Console.WriteLine("World");
Console.ReadLine();
Console.WriteLine("After input");
```

[00:01:05](https://www.youtube.com/watch?v=SSTFFX5heuY&t=65s) Rulând această stare, inițial se vede doar prima linie afișată. [00:01:11](https://www.youtube.com/watch?v=SSTFFX5heuY&t=71s) Apoi pare că rămâne blocat la citire. [00:01:17](https://www.youtube.com/watch?v=SSTFFX5heuY&t=77s) Acea instrucțiune așteaptă intrarea, deci ultima afișare nu s-a produs încă.

Urmăriți execuția:

1. Primul WriteLine își afișează textul și se termină.
2. ReadLine așteaptă cât timp utilizatorul nu a trimis o linie. Nicio instrucțiune următoare nu rulează în acea așteptare.
3. [00:01:25](https://www.youtube.com/watch?v=SSTFFX5heuY&t=85s) Trimiterea unei linii îi permite lui ReadLine să se termine și să returneze string-ul introdus.
4. Abia apoi execuția ajunge la ultima afișare.

[00:01:36](https://www.youtube.com/watch?v=SSTFFX5heuY&t=96s) **Blocarea** înseamnă așteptarea la o instrucțiune fără a trece la următoarea. [00:01:43](https://www.youtube.com/watch?v=SSTFFX5heuY&t=103s) Aceeași ideie se poate aplica la așteptarea unui răspuns de la o bază de date sau la citirea unui fișier; nu este specifică intrării din consolă. Acestea sunt comparații, nu implementări de bază de date sau de citire de fișier în această lecție.

[00:01:53](https://www.youtube.com/watch?v=SSTFFX5heuY&t=113s) Programul este blocat la citire. [00:01:58](https://www.youtube.com/watch?v=SSTFFX5heuY&t=118s) Până când nu apare evenimentul de deblocare, instrucțiunea următoare nu poate rula. [00:02:11](https://www.youtube.com/watch?v=SSTFFX5heuY&t=131s) După ce intrarea sosește, execuția obișnuită de sus în jos reia. Așadar, o citire poate opri intenționat acest program secvențial.

<details>
<summary>Predicție: apare ultimul mesaj dacă nu trimiți niciodată o linie?</summary>

Nu. Cu intrarea interactivă încă deschisă și în așteptare, ReadLine nu s-a terminat, deci nu s-a ajuns la ultima instrucțiune.

</details>

## Capturarea intrării nullable

[00:02:24](https://www.youtube.com/watch?v=SSTFFX5heuY&t=144s) Citirea are un rezultat: un **string nullable**, exprimat ca string?. Semnul întrebării spune că valoarea poate fi null, o notație explicată mai pe larg într-un video anterior. [00:02:41](https://www.youtube.com/watch?v=SSTFFX5heuY&t=161s) Atribuirea expresiei de apel unei variabile salvează textul introdus. Starea intermedie din video este echivalentă cu:

```csharp
string? text = Console.ReadLine();
```

[00:02:35](https://www.youtube.com/watch?v=SSTFFX5heuY&t=155s) Această posibilitate există pentru că intrarea nu trebuie să provină neapărat de la consola live. [00:02:50](https://www.youtube.com/watch?v=SSTFFX5heuY&t=170s) Cu **intrare redirecționată**, liniile sunt furnizate de un fișier. Când acesta nu mai are linii, ReadLine poate produce null în locul unui string gol. Un string gol este tot un string; null spune că nu a fost returnată nicio valoare de linie.

[00:03:06](https://www.youtube.com/watch?v=SSTFFX5heuY&t=186s) Pentru demonstrație, presupunerea este că un utilizator interactiv va furniza o linie. Eliminarea semnului întrebării din declarația variabilei produce o avertizare de valoare posibil nulă atunci când analiza de nullabilitate este activată. [00:03:17](https://www.youtube.com/watch?v=SSTFFX5heuY&t=197s) Următoarea modificare de cod suprimă acea avertizare:

```csharp
string text = Console.ReadLine()!;
```

[00:03:30](https://www.youtube.com/watch?v=SSTFFX5heuY&t=210s) **Operatorul de anulare a nulului** este semnul exclamării de după expresie. [00:03:33](https://www.youtube.com/watch?v=SSTFFX5heuY&t=213s) Îi spune compilatorului să trateze acea expresie ca fiind nenulă. Nu schimbă rezultatul citirii la runtime și nu protejează împotriva intrării epuizate. Exemplul se bazează pe presupunerea sa despre intrare; celelalte moduri de a trata null sunt lăsate pentru mai târziu.

<details>
<summary>Auto-explicație: de ce semnul exclamării se află după apel?</summary>

Se aplică valorii produse de întreaga expresie de apel. Suprimă avertizările de nullabilitate despre acea valoare; nu modifică declarația lui ReadLine.

</details>

## Redarea textului introdus

[00:03:49](https://www.youtube.com/watch?v=SSTFFX5heuY&t=229s) Cu rezultatul salvat într-o variabilă de tip string, adăugăm o afișare a acelei variabile. Starea corespunzătoare din video este reprezentată de:

```csharp
Console.WriteLine("World");
string text = Console.ReadLine()!;
Console.WriteLine(text);
```

[00:03:58](https://www.youtube.com/watch?v=SSTFFX5heuY&t=238s) Execuția afișează World, așteaptă intrarea, apoi afișează înapoi ce s-a introdus. Valoarea salvată leagă acum intrarea de munca ulterioară.

[00:04:10](https://www.youtube.com/watch?v=SSTFFX5heuY&t=250s) Chiar dacă utilizatorul tastează cifre, valoarea este un **string**, adică o secvență de caractere. De exemplu, introducerea 1234 produce reprezentarea textuală a acelor cifre; nu creează automat un int.

## Construirea unui calculator din string-uri

[00:04:24](https://www.youtube.com/watch?v=SSTFFX5heuY&t=264s) Un calculator are nevoie de două numere ale căror valori pot fi adunate. Începem cerând două linii. [00:04:43](https://www.youtube.com/watch?v=SSTFFX5heuY&t=283s) Console.Write afișează un mesaj fără a adăuga sfârșit de linie, astfel încât intrarea apare pe aceeași linie cu mesajul. Starea inițială a calculatorului din video este echivalentă cu:

```csharp
Console.Write("Enter A: ");
string aText = Console.ReadLine()!;
Console.Write("Enter B: ");
string bText = Console.ReadLine()!;
Console.WriteLine(aText + bText);
```

[00:04:49](https://www.youtube.com/watch?v=SSTFFX5heuY&t=289s) Acest operator plus efectuează **concatenarea**, alipind textul, deoarece ambii operanzi sunt string-uri. [00:04:51](https://www.youtube.com/watch?v=SSTFFX5heuY&t=291s) Denumirea variabilelor a și b nu ar schimba acel comportament: a + b între două string-uri le unește oricum. Tipurile lor determină operația.

[00:05:00](https://www.youtube.com/watch?v=SSTFFX5heuY&t=300s) Modificarea vizată este să calculăm explicit o sumă numerică și să o afișăm. [00:05:10](https://www.youtube.com/watch?v=SSTFFX5heuY&t=310s) Ambele valori citite trebuie mai întâi convertite în int, tipul de număr întreg folosit aici. [00:05:14](https://www.youtube.com/watch?v=SSTFFX5heuY&t=314s) Păstrarea citirilor originale în variabile de tip string face distincția clară: acele variabile conțin în continuare text, în timp ce variabilele convertite vor conține numere.

<details>
<summary>Predicție: ce produce expresia de tip string "12" + "3"?</summary>

Produce "123", nu 15. Aritmetica necesită operanzi numerici.

</details>

## Parsarea ambilor operanzi și observarea eșecurilor

[00:05:23](https://www.youtube.com/watch?v=SSTFFX5heuY&t=323s) **Parsarea** transformă reprezentarea textuală în valoarea ei numerică. [00:05:30](https://www.youtube.com/watch?v=SSTFFX5heuY&t=330s) Tipul int expune funcția de conversie int.Parse; punctul selectează acea funcție de pe tip. [00:05:40](https://www.youtube.com/watch?v=SSTFFX5heuY&t=340s) Parse acceptă un string și returnează un int atunci când textul reprezintă un întreg zecimal adecvat. Prima stare de conversie din video este:

```csharp
string aText = Console.ReadLine()!;
int a = int.Parse(aText);
```

[00:05:58](https://www.youtube.com/watch?v=SSTFFX5heuY&t=358s) Dacă textul nu este un număr, precum hjkl, Parse ridică o **excepție**, adică un semnal de eroare la runtime. Nimic din această versiune nu tratează excepția, deci programul cedează (crash).

[00:06:07](https://www.youtube.com/watch?v=SSTFFX5heuY&t=367s) Aplicăm aceeași conversie și pentru B. [00:06:16](https://www.youtube.com/watch?v=SSTFFX5heuY&t=376s) Ambii operanzi sunt acum numerici înainte de calcularea sumei. Starea calculatorului complet, simplificată, este:

```csharp
Console.Write("Enter A: ");
string aText = Console.ReadLine()!;
int a = int.Parse(aText);

Console.Write("Enter B: ");
string bText = Console.ReadLine()!;
int b = int.Parse(bText);

int sum = a + b;
Console.WriteLine(sum);
```

Testele înregistrate arată limitele acestei versiuni:

1. [00:06:30](https://www.youtube.com/watch?v=SSTFFX5heuY&t=390s) Cu intrare numerică, programul afișează suma; cu intrare nenumerică pentru A, Parse ridică o excepție.
2. [00:06:38](https://www.youtube.com/watch?v=SSTFFX5heuY&t=398s) Un A corect urmat de un B incorect ridică tot o excepție și programul cedează (crash).
3. [00:06:47](https://www.youtube.com/watch?v=SSTFFX5heuY&t=407s) După repornire, utilizatorul trebuie să reintroducă A, care era deja corect. O greșeală la a doua valoare pierde progresul util.

[00:07:05](https://www.youtube.com/watch?v=SSTFFX5heuY&t=425s) Comportamentul dorit este mai restrâns și mai util: să continuăm să cerem numărul curent până când textul său poate fi parcă. Pentru a construi acest comportament, programul are nevoie de o conversie care raportează eșecul fără această cedare (crash).

## Un parametru out scrie în variabila apelantului

[00:07:21](https://www.youtube.com/watch?v=SSTFFX5heuY&t=441s) Înainte de a înlocui Parse, introducem **parametrii out**. Un parametru out oferă funcției apelate acces la o variabilă a apelantului pe care trebuie să o inițializeze. [00:07:23](https://www.youtube.com/watch?v=SSTFFX5heuY&t=443s) Acesta este mecanismul care va permite conversiei să raporteze un număr separat de succes.

[00:07:29](https://www.youtube.com/watch?v=SSTFFX5heuY&t=449s) Mica demonstrație lasă întregul apelantului cu valoarea 5. Variabila apelantului este discutată inițial ca p și mai târziu ca a; numele nu produc legătura. O versiune simplificată a acelei stări din video este:

```csharp
int p;
OutExample(out p);
Console.WriteLine(p);

static void OutExample(out int output)
{
    output = 5;
}
```

Aici void înseamnă că apelul nu furnizează o valoare de retur separată. Ajutorul static poate fi apelat fără construirea unui obiect. Modificatorul out apare atât în declarația parametrului funcției, cât și în argumentul de la apel.

[00:07:41](https://www.youtube.com/watch?v=SSTFFX5heuY&t=461s) Explicația folosește doar **stiva**, modelul de stocare temporară pentru variabilele locale și parametrii folosiți în acest exemplu. O **adresă** identifică o locație de memorie; transmiterea accesului la acea locație îi permite funcției apelate să afecteze apelantul. Urmăriți traseul:

1. [00:07:50](https://www.youtube.com/watch?v=SSTFFX5heuY&t=470s) Declararea lui p creează o variabilă locală capabilă să conțină un întreg. Inițial nu este inițializată: nu i-a fost atribuită nicio valoare utilizabilă.
2. [00:08:01](https://www.youtube.com/watch?v=SSTFFX5heuY&t=481s) Apelarea lui OutExample stabilește parametrul său numit output în contextul temporar al apelului.
3. [00:08:13](https://www.youtube.com/watch?v=SSTFFX5heuY&t=493s) În modelul cu adrese, un int obișnuit stochează un număr, în timp ce out int oferă o referință la stocarea întregului din apelant.
4. [00:08:22](https://www.youtube.com/watch?v=SSTFFX5heuY&t=502s) Obiectele au locații de memorie, iar [00:08:32](https://www.youtube.com/watch?v=SSTFFX5heuY&t=512s) variabilele locale au și ele locații.
5. [00:08:40](https://www.youtube.com/watch?v=SSTFFX5heuY&t=520s) Desenul folosește locații exemplu precum 10000 și 10008 pentru stocări succesive. [00:08:54](https://www.youtube.com/watch?v=SSTFFX5heuY&t=534s) Locația următoare se schimbă în funcție de spațiul alocat, iar direcția dispunerii este un detaliu de implementare. Aceste numere ilustrează locații distincte, nu dimensiuni garantate ale int sau o dispunere fixă la runtime.
6. [00:09:05](https://www.youtube.com/watch?v=SSTFFX5heuY&t=545s) Transmiterea lui out p leagă output de locația lui p. Aceeași operație este descrisă ulterior cu variabila apelantului a.
7. [00:09:16](https://www.youtube.com/watch?v=SSTFFX5heuY&t=556s) Atribuirea output = 5 scrie în celula referită, nu peste adresa folosită pentru a ajunge la ea.
8. [00:09:28](https://www.youtube.com/watch?v=SSTFFX5heuY&t=568s) Compilatorul știe că output este asociat unui parametru out. [00:09:41](https://www.youtube.com/watch?v=SSTFFX5heuY&t=581s) În acest model rezolvă referința și scrie valoarea din dreapta în stocarea apelantului.
9. [00:09:46](https://www.youtube.com/watch?v=SSTFFX5heuY&t=586s) Așadar, parametrul este local apelului funcției, dar atribuirea prin el modifică variabila apelantului.
10. [00:10:05](https://www.youtube.com/watch?v=SSTFFX5heuY&t=605s) Neavând instrucțiuni rămase, funcția se termină. Variabilele locale temporare ale apelului nu mai sunt necesare, controlul revine la locul apelului, iar instrucțiunea următoare afișează 5.

Ideea esențială este o referință la stocarea unei variabile existente. Potrivirea numelor dintre apelant și funcția apelată nu este necesară.

## Un singur apel poate produce două rezultate

[00:10:25](https://www.youtube.com/watch?v=SSTFFX5heuY&t=625s) Schimbăm ajutorul astfel încât să returneze și un bool, pe lângă scrierea lui 5 prin out. O **valoare de retur** este chiar valoarea expresiei de apel. Starea din video poate fi simplificată la:

```csharp
int a;
bool b = OutExample(out a);
Console.WriteLine(b);
Console.WriteLine(a);

static bool OutExample(out int output)
{
    output = 5;
    return true;
}
```

[00:10:36](https://www.youtube.com/watch?v=SSTFFX5heuY&t=636s) Execuția afișează True și 5. Acestea sunt două căi distincte de obținere a rezultatelor, nu două atribuiri în aceeași variabilă.

Urmăriți atribuirea mai complexă:

1. [00:10:45](https://www.youtube.com/watch?v=SSTFFX5heuY&t=645s) a are propria stocare înainte de a conține o valoare utilizabilă.
2. [00:10:59](https://www.youtube.com/watch?v=SSTFFX5heuY&t=659s) b primește și ea stocare. Desenul folosește o locație descrisă ca 10.4, ilustrând o celulă distinctă; b nu este încă inițializat.
3. [00:11:05](https://www.youtube.com/watch?v=SSTFFX5heuY&t=665s) Pentru a inițializa b din expresia de apel, acea întreagă expresie trebuie mai întâi evaluată. Apelarea lui OutExample leagă parametrul său out de stocarea lui a.
4. [00:11:19](https://www.youtube.com/watch?v=SSTFFX5heuY&t=679s) Ajutorul scrie 5 în a, apoi return true încheie ajutorul și face ca expresia de apel să fie evaluată la true.
5. [00:11:43](https://www.youtube.com/watch?v=SSTFFX5heuY&t=703s) Acel true este stocat în b. Contextul temporar al apelului este eliminat, iar afișările următoare arată b ca True și a ca 5.

[00:12:03](https://www.youtube.com/watch?v=SSTFFX5heuY&t=723s) Modificatorul out îi cere funcției apelate să își scrie ieșirea în variabila furnizată. [00:12:13](https://www.youtube.com/watch?v=SSTFFX5heuY&t=733s) Separat, expresia de apel poate avea propria valoare de retur. Aceasta este exact forma necesară pentru „parsarea a reușit?” plus „ce număr a fost parcă?”.

[00:12:44](https://www.youtube.com/watch?v=SSTFFX5heuY&t=764s) **Inițializarea** este obligatorie pentru un parametru out pe fiecare cale normală de retur. Dacă ajutorul nu atribuie niciodată output, compilatorul raportează o eroare: apelantul nu trebuie să rămână cu o variabilă neinițializată după un retur reușit. out impune o atribuire; nu prescrie ce valoare trebuie să scrie fiecare funcție.

<details>
<summary>Auto-explicație: care instrucțiune îl inițializează pe b și care pe a?</summary>

output = 5 din ajutor îl inițializează pe a prin referință. return true furnizează valoarea apelului, pe care atribuirea din apelant o stochează în b.

</details>

## TryParse separă succesul de număr

[00:12:58](https://www.youtube.com/watch?v=SSTFFX5heuY&t=778s) **int.TryParse** încearcă să parcurcă un string. Scrie un întreg în argumentul său out și returnează un bool care spune dacă parsarea a reușit. Starea de demonstrație simplificată din video este:

```csharp
string text = "1234";
int a;
bool b = int.TryParse(text, out a);
Console.WriteLine(b);
Console.WriteLine(a);
```

Rolurile parametrilor contează: text este intrarea, a primește numărul, iar b primește succesul. Schimbarea numelor lor nu schimbă aceste roluri.

- [00:13:15](https://www.youtube.com/watch?v=SSTFFX5heuY&t=795s) Cu 1234, b este true, iar a este 1234.
- [00:13:26](https://www.youtube.com/watch?v=SSTFFX5heuY&t=806s) Cu text nenumeric, b este false, iar a este 0. Zero este valoarea int implicită folosită de TryParse la eșec. Ieșirea sa tot trebuie atribuită; apelantul nu poate rămâne neinițializat.

[00:13:52](https://www.youtube.com/watch?v=SSTFFX5heuY&t=832s) O **declarație out inline** declară variabila la locul apelului și îi transmite imediat referința. Următoarea modificare de sintaxă este:

```csharp
bool success = int.TryParse("1234", out int value);
Console.WriteLine(value);
```

int-ul din out int value îl declară acolo pe value. Prin contrast, out value se referă la o variabilă deja declarată.

<details>
<summary>Predicție: demonstrează value == 0 că parsarea a eșuat?</summary>

Nu. Intrarea "0" poate fi parcă cu succes și poate produce tot zero. Verifică bool-ul returnat pentru a deosebi parsarea cu succes a lui zero de conversia eșuată.

</details>

## Construirea buclei de reluare

[00:14:15](https://www.youtube.com/watch?v=SSTFFX5heuY&t=855s) Revenim la calculator și înlocuim Parse cu TryParse. [00:14:33](https://www.youtube.com/watch?v=SSTFFX5heuY&t=873s) Aceasta completează întregul prin out și furnizează un bool care poate conduce o ramură if.

[00:14:44](https://www.youtube.com/watch?v=SSTFFX5heuY&t=884s) Conversia eșuată trebuie să revină la cererea de intrare. Prima explicație folosește o buclă infinită cu goto. O **etichetă** numește destinația saltului; **goto** transferă execuția acolo. [00:14:52](https://www.youtube.com/watch?v=SSTFFX5heuY&t=892s) Eșecul sare în sus, iar succesul ajunge la ieșire. [00:15:02](https://www.youtube.com/watch?v=SSTFFX5heuY&t=902s) Așezăm eticheta la cererea de intrare a ciclului. Această stare intermediară simplificată din video păstrează acel flux de control:

```csharp
int a;
while (true)
{
Input:
    Console.Write("Enter A: ");
    string text = Console.ReadLine()!;
    bool success = int.TryParse(text, out a);
    if (!success)
        goto Input;
    break;
}
Console.WriteLine(a);
```

Aici !success este negație logică: ramura rulează când success este false. Acest semn exclamării în față neagă un bool; diferă de semnul null-forgiving postfix de după ReadLine().

[00:15:08](https://www.youtube.com/watch?v=SSTFFX5heuY&t=908s) La eșec, goto sare peste corpul rămas și repetă citirea. [00:15:19](https://www.youtube.com/watch?v=SSTFFX5heuY&t=919s) În consecință, break este atins doar după ce parsarea reușește. Un **break** părăsește bucla în loc să o repete.

[00:15:29](https://www.youtube.com/watch?v=SSTFFX5heuY&t=929s) Există și o problemă de **scope**: un întreg declarat în interiorul blocului buclei nu poate fi numit de afișarea ulterioară, din afara acelui bloc. Îi mutăm declarația în afară, ca în acest exemplu, apoi transmitem out a în interiorul buclei. Scope-ul este locul în care un nume este disponibil, nu doar faptul că variabila a fost atribuită.

## Cererea unui întreg pozitiv

[00:15:41](https://www.youtube.com/watch?v=SSTFFX5heuY&t=941s) Posibilitatea de a fi parcă nu este toată regula. Intrarea trebuie să fie și **pozitivă**, adică mai mare decât zero.

[00:15:49](https://www.youtube.com/watch?v=SSTFFX5heuY&t=949s) Înlocuim saltul de reluare cu **continue**, care sare peste restul acestei iterații și revine la iterația următoare a buclei. [00:16:06](https://www.youtube.com/watch?v=SSTFFX5heuY&t=966s) Apoi adăugăm verificarea numerică. Pragul este considerat pe scurt ca „mai mic decât zero”, apoi [00:16:15](https://www.youtube.com/watch?v=SSTFFX5heuY&t=975s) corectat în „mai mic sau egal cu zero”. Cerința finală respinge și zero.

[00:16:27](https://www.youtube.com/watch?v=SSTFFX5heuY&t=987s) Folosim continue și pentru acest al doilea eșec. [00:16:34](https://www.youtube.com/watch?v=SSTFFX5heuY&t=994s) Doar parsarea cu succes a unei valori mai mari decât zero ajunge la break. Starea reconstruită pentru intrare pozitivă din video este:

```csharp
int a;
while (true)
{
    Console.Write("Enter A: ");
    string text = Console.ReadLine()!;
    bool success = int.TryParse(text, out a);
    if (!success)
        continue;

    if (a <= 0)
    {
        Console.WriteLine("Number must be positive.");
        continue;
    }

    break;
}
```

Cele două verificări răspund la întrebări diferite: „a fost acest text un număr?” și „este permis acel număr?”. Păstrându-le separate, fiecărei i se poate da un mesaj specific.

[00:16:40](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1000s) Execuția încearcă o literă și repetă cererea. Apoi încearcă un număr negativ, afișează eroarea de număr pozitiv și întreabă din nou. Mesajul despre numărul pozitiv aparține eșecului de interval numeric; intrarea nenumerică urmează ramura anterioară de parsare eșuată. [00:16:55](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1015s) Introducerea unui număr pozitiv permite în sfârșit execuției să continue către citirea lui B.

<details>
<summary>Predicție: unde ajunge zero și unde ajunge o literă?</summary>

Zero poate trece de TryParse, dar eșuează la verificarea a <= 0. O literă eșuează la TryParse și continuă imediat, înainte de verificarea numerică. Niciuna nu ajunge la break.

</details>

## Extragerea ajutorului InputInt

[00:16:57](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1017s) B are nevoie de aceeași validare ca A. [00:17:05](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1025s) Modificarea inițială dublează blocul, schimbă mesajul în B, parcurge în b și folosește b în ieșirea ulterioară. Starea intermediară din video are, așadar, două copii ale aceleiași proceduri de citire.

[00:17:18](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1038s) **Extracția funcției** este următoarea modificare: mutăm logica repetată într-un ajutor, astfel încât să existe un singur loc de modificat. [00:17:25](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1045s) Dăm operației comune un nume precum InputInt. [00:17:34](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1054s) Copiile diferă prin numele afișat utilizatorului și prin variabila apelantului care primește răspunsul.

Construiți ajutorul în acești pași:

1. [00:17:41](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1061s) Declarăm un tip de retur int, deoarece apelantul are nevoie de un întreg pozitiv validat.
2. [00:17:48](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1068s) Facem din numele variabilei afișate un parametru de tip string. Un **parametru** este declarat în ajutor; apelantul furnizează un **argument**, precum "A".
3. [00:17:55](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1075s) Mutăm corpul comun în ajutor. Variabila originală a apelantului nu este vizibilă acolo, deci introducem o valoare locală parcă.
4. [00:18:02](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1082s) Acea valoare locală poate fi declarată în interiorul buclei ajutorului, deoarece rezultatul reușit este tratat în aceeași iterație.
5. [00:18:14](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1094s) Înlocuim break cu return value. **return** părăsește întreaga funcție și predă valoarea înapoi, nu doar părăsește bucla.
6. [00:18:20](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1100s) Combinăm declarația cu out la apelul TryParse.
7. [00:18:42](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1122s) Eliminăm ambele bucle de citire din apelant și apelăm o dată ajutorul pentru fiecare variabilă.

Starea rezultată a ajutorului din video este reprezentată de:

```csharp
int a = InputInt("A");
int b = InputInt("B");
Console.WriteLine(a + b);

static int InputInt(string name)
{
    while (true)
    {
        Console.Write("Enter " + name + ": ");
        string text = Console.ReadLine()!;
        bool success = int.TryParse(text, out int value);
        if (!success)
            continue;

        if (value <= 0)
        {
            Console.WriteLine("Number must be positive.");
            continue;
        }

        return value;
    }
}
```

InputInt este o **abstractizare**: numele său îi permite apelantului să ceară un întreg utilizabil fără să repete pașii de citire, parsare, reluare și verificare de interval. Variabila sa out primește numărul parcă; valoarea sa de retur îl inițializează pe a sau b din apelant. Nu este nevoie ca valoarea de retur a acestui ajutor să fie și ea un parametru out.

## Păstrarea citirii și a verificărilor în pași separați

[00:19:05](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1145s) Să luăm în calcul exprimarea aceleiași reluări cu do-while și așezarea parsării și verificărilor în condiția sa. Lecția respinge această formă comprimată. [00:19:16](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1156s) Ar amesteca citirea string-ului, parsarea lui, verificarea succesului conversiei și verificarea regulii numerice în controlul buclei.

[00:19:23](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1163s) O buclă **while** își verifică mai întâi condiția. O condiție care se bazează pe o valoare citită doar în corp nu poate folosi acea valoare produsă de corp înaintea primei parcurgeri. Punând ReadLine direct în condiție, am efectua o citire, dar am crea exact expresia înghesuită criticată.

[00:19:30](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1170s) Un **do-while** își execută o dată corpul înainte de a testa condiția, apoi repetă cât timp condiția este adevărată. [00:19:44](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1184s) În aranjarea comprimată ilustrată, doar afișarea cererii de intrare încape confortabil în corpul do; citirea și validarea ajung în condiție.

[00:19:55](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1195s) Declararea rezultatului lui ReadLine în interiorul blocului do împiedică condiția exterioară să folosească acel nume. [00:20:00](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1200s) Scope-ul numelui se încheie odată cu acel bloc. Aceasta este o problemă a acelui loc de declarare, nu o regulă care interzice citirile în orice corp do-while.

[00:20:08](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1208s) Rezultatul parsării trebuie să rămână disponibil acolo unde este verificat. În versiunea încercată, bazată pe condiție, aceasta impune aranjarea citirii înaintea expresiei de parsare/verificare și o variabilă de ieșire separată. [00:20:12](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1212s) Salvăm rezultatul parsării în loc să îl eliminăm în tăcere. [00:20:15](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1215s) O parsare eșuată trebuie să declanșeze încă o iterație. [00:20:28](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1228s) Și parsarea cu succes a lui zero sau a unei valori negative trebuie să declanșeze încă o iterație.

Starea comprimată propusă în video poate fi ilustrată astfel; este o alternativă respinsă, nu forma finală a ajutorului:

```csharp
int value;
do
{
    Console.Write("Enter a positive integer: ");
}
while (!int.TryParse(Console.ReadLine()!, out value) || value <= 0);
```

Aici || înseamnă „sau”: reluăm la parsare eșuată sau la o valoare parcă inacceptabilă. Citirea are loc înainte ca rezultatul ei să poată fi parcă, dar mai multe operații au fost înghesuite într-o singură condiție.

[00:20:31](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1231s) Acest aranjament nu oferă niciun loc clar pentru afișarea unor erori diferite după fiecare eșec. [00:20:42](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1242s) Corecția emphatică este să nu mai comprimăm operațiile pe un singur rând. [00:20:49](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1249s) Folosim o buclă infinită și o părăsim manual când intrarea este validă.

[00:20:54](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1254s) Structura preferată păstrează fiecare operație pe rândul ei, astfel încât să putem insera mai multă logică după ea. [00:21:06](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1266s) Condițiile de repetare și de ieșire pot fi verificări separate, una după alta. [00:21:12](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1272s) Acest lucru ajută și la **depanare**, adică la examinarea pașilor și valorilor intermediare. Citirea, parsarea, verificarea succesului, verificarea acceptabilității și returul sunt vizibile individual în InputInt.

## Validarea valorilor împreună

[00:21:16](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1276s) Trecem de la citirea unui singur număr utilizabil la **validare**: hotărârea dacă valorile sunt corecte și permise conform regulilor programului.

[00:21:21](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1281s) O clasă ține acum A și B în câmpuri. O **clasă** descrie datele, un **obiect** este o instanță care conține acele date, iar un **câmp** stochează una dintre valorile sale. Citirea unor întregi pozitivi în acele câmpuri nu dovedește în continuare că obiectul respectă fiecare regulă.

[00:21:26](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1286s) „Valid” înseamnă corect și permis. [00:21:37](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1297s) Regula concretă este B >= 2 * A: B trebuie să fie cel puțin de două ori A. [00:21:40](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1300s) Punem verificarea într-o metodă de validare, o funcție asociată obiectului.

[00:21:45](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1305s) Returnăm false pentru B < 2 * A. [00:21:50](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1310s) Altfel returnăm true. Această stare inițială de validare din video este echivalentă cu:

```csharp
class Values
{
    public int A;
    public int B;

    public bool Validate()
    {
        if (B < 2 * A)
            return false;

        return true;
    }
}
```

Cele două ramuri acoperă regula: egalitatea este validă, iar orice valoare sub dublul lui A este invalidă.

[00:21:53](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1313s) Extindem exemplul cu C și cerem C > A + B. O singură metodă poate verifica mai multe reguli. Verificarea trebuie să descrie **încălcarea**, astfel încât [00:22:25](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1345s) condiția este corectată în C <= A + B înainte de a returna false. Starea intermediară corespunzătoare din video este:

```csharp
// Inside Values, after adding a public int C field:
public bool Validate()
{
    if (B < 2 * A)
        return false;

    if (C <= A + B)
        return false;

    return true;
}
```

[00:22:03](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1323s) Tiparul vizat este să validăm imediat după citirea valorilor, să memorăm rezultatul bool și să cerem din nou date când rezultatul este false.

## Reluarea obiectelor invalide

[00:22:28](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1348s) Înfășurăm pasul complet de citire într-o buclă. Datele invalide repetă citirea; datele valide încheie bucla. Folosind ajutorul existent InputInt, starea simplificată de citire a obiectului din video este:

```csharp
Values data = new Values();
while (true)
{
    data.A = InputInt("A");
    data.B = InputInt("B");
    data.C = InputInt("C");

    bool isValid = data.Validate();
    if (isValid)
        break;
}
```

Noua expresie creează obiectul. Accesul prin punct scrie în câmpurile sale sau îi apelează metoda. isValid din apelant este un **indicator**, adică un bool care memorează dacă întregul obiect respectă regulile.

[00:23:02](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1382s) În execuția înregistrată, citirea se repetă până la furnizarea unor date valide. Rezultatul validării controlează direct dacă bucla se oprește.

[00:23:18](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1398s) Primul dezavantaj este feedbackul slab: utilizatorul nu află de ce obiectul nu a trecut validarea. [00:23:26](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1406s) Altă observație: verificările regulilor pot fi examinate independent, în loc de a fi topite într-o singură condiție complicată.

[00:23:36](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1416s) Unele verificări ar putea, în principiu, fi mutate în procedura de citire și efectuate pe măsură ce valorile necesare devin disponibile. Lecția nu face deliberat această schimbare. Forma demonstrată rămâne: întâi citirea, apoi validarea obiectului.

## Explicarea primei reguli încălcate

[00:24:07](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1447s) Îmbunătățim feedbackul afișând exact ce regulă a eșuat. [00:24:17](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1457s) În loc să doar returnăm false, anunțăm regula încălcată înainte de retur. Starea intermediară simplificată de validare din video este:

```csharp
public bool Validate()
{
    if (B < 2 * A)
    {
        Console.WriteLine("B must be at least twice A.");
        return false;
    }

    if (C <= A + B)
    {
        Console.WriteLine("C must be greater than A + B.");
        return false;
    }

    return true;
}
```

[00:24:22](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1462s) Când ambele reguli sunt încălcate, această versiune afișează doar prima eroare. [00:24:34](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1474s) Primul retur încheie funcția de validare; regula rămasă nu este niciodată examinată în acel apel. Bucla de citire din jur poate continua, dar apelul curent de validare s-a încheiat.

<details>
<summary>Predicție: va face inversarea celor două verificări ca această versiune să raporteze toate erorile?</summary>

Nu. Schimbă doar care eroare apare prima. Prima regulă încălcată returnează înainte de verificarea celei de-a doua reguli.

</details>

## Raportarea tuturor regulilor încălcate

[00:24:39](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1479s) Pentru a raporta fiecare regulă încălcată, nu mai returnăm din ramurile de eșec. Folosim în schimb un **indicator isValid**, un bool care își amintește dacă vreo verificare a eșuat. [00:24:44](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1484s) Fiecare ramură stabilește indicatorul, permițând totodată funcției să continue. [00:24:50](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1490s) Returnăm indicatorul doar după ce au rulat toate verificările. Forma funcțională a acestei stări finale de validare din video este:

```csharp
public bool Validate()
{
    bool isValid = true;

    if (B < 2 * A)
    {
        Console.WriteLine("B must be at least twice A.");
        isValid = false;
    }

    if (C <= A + B)
    {
        Console.WriteLine("C must be greater than A + B.");
        isValid = false;
    }

    return isValid;
}
```

Valoarea inițială trebuie să fie true: obiectul rămâne valid dacă nu eșuează nicio regulă. Pornind isValid de la false și atribuind false doar în aceste ramuri, am respinge orice obiect, inclusiv unul care respectă toate regulile. Aceasta este inițializarea cerută de explicație și de rezultatul final cu intrare validă; false aparține ramurilor de eșec.

[00:25:02](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1502s) Intrarea chiar și într-o singură ramură de eșec face ca răspunsul final să fie false. O regulă îndeplinită ulterior nu readuce true. [00:25:06](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1506s) Fiecare condiție încălcată își afișează propriul mesaj, deoarece execuția continuă cu verificările rămase.

Urmăriți procedura:

1. Presupunem validitatea la începutul fiecărui apel.
2. Verificăm o regulă și afișăm un mesaj dacă este încălcată.
3. Memorăm false pentru acea încălcare, apoi continuăm verificările.
4. Repetăm pentru fiecare regulă.
5. Returnăm o singură dată verdictul acumulat.

Apelantul primește tot un singur bool. Utilizatorul poate vedea mai multe erori afișate, dar apelantul nu a primit o listă a acelor erori ca date.

## Interpretarea testelor și limitelor validării

[00:25:26](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1526s) O singură regulă de validitate încălcată este suficientă pentru a face întregul obiect invalid. Metoda verifică totuși fiecare regulă separat, astfel încât fiecare încălcare să poată fi anunțată. Testele finale din video folosesc aceste două reguli, B >= 2 * A și C > A + B; ambele trebuie să fie îndeplinite pentru ca obiectul complet să fie valid.

Testele înregistrate și semnificațiile lor sunt:

| Test din video | A, B, C | B >= 2 * A | C > A + B | Rezultat |
| --- | --- | --- | --- | --- |
| [00:25:32](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1532s) | 2, 3, 1 | False: 3 < 4 | False: 1 <= 5 | Două mesaje de eroare; validarea este false |
| [00:25:39](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1539s) | 2, 5, 4 | True: 5 >= 4 | False: 4 <= 7 | Un mesaj de eroare; validarea este tot false |
| [00:25:45](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1545s) | Valori care respectă ambele reguli | True | True | Niciun mesaj de eroare; validarea este true |

Cele două mesaje din prima rulare înseamnă două **încălcări**, nu două clauze îndeplinite. O regulă îndeplinită în a doua rulare nu anulează eșecul celeilalte reguli. Rularea cu intrare validă continuă fără a anunța nicio încălcare; această metodă de validare returnează un bool și nu aruncă o excepție.

Pentru o reconstrucție compactă a comportamentului final din video, conectați acea metodă la următorul apelant:

```csharp
bool isValid = data.Validate();
if (isValid)
    break; // inside the surrounding input loop
```

[00:25:52](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1552s) Când validarea returnează true, apelantul ajunge la break și părăsește bucla de citire. Acest break este ieșirea reușită, nu oprirea cauzată de o regulă încălcată. În versiunea anterioară, return false încheia prematur metoda de validare; în versiunea finală, ramurile de eșec continuă verificările.

[00:26:00](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1560s) Această abordare se potrivește unui apelant care are nevoie doar de un semnal de validitate da-sau-nu, în timp ce utilizatorul primește explicația afișată. [00:26:03](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1563s) Dacă codul apelant trebuie să știe exact ce reguli au eșuat, sunt necesare tehnici de raportare mai avansate, amânate deliberat.

<details>
<summary>Auto-explicație: de ce A = 2, B = 5, C = 4 rămâne invalid?</summary>

B respectă prima regulă, dar C trebuie să depășească A + B, adică 7. C = 4 încalcă acea a doua regulă. Orice regulă nereușită face ca rezultatul validării întregului obiect să fie false.

</details>

## Traseul istoricului codului

Acestea sunt legături stabile către stările relevante din această încărcare, în ordinea construcției. Exemplele de mai sus reconstruiesc acele stări; niciun commit fără legătură din depozit nu este prezentat ca sursă a lor.

- [00:00:46](https://www.youtube.com/watch?v=SSTFFX5heuY&t=46s) — Programul secvențial inițial se oprește la ReadLine.
- [00:03:17](https://www.youtube.com/watch?v=SSTFFX5heuY&t=197s) — Capturarea intrării cu o presupunere de non-nul și cu ! postfix.
- [00:05:30](https://www.youtube.com/watch?v=SSTFFX5heuY&t=330s) — Parsarea primului operand textual într-un int.
- [00:06:07](https://www.youtube.com/watch?v=SSTFFX5heuY&t=367s) — Conversia celui de-al doilea operand și calcularea unei sume numerice.
- [00:07:29](https://www.youtube.com/watch?v=SSTFFX5heuY&t=449s) — Demonstrarea scrierii lui 5 printr-un parametru out.
- [00:10:25](https://www.youtube.com/watch?v=SSTFFX5heuY&t=625s) — Adăugarea unei valori de retur bool separate.
- [00:12:58](https://www.youtube.com/watch?v=SSTFFX5heuY&t=778s) — Folosirea lui TryParse pentru succes plus ieșirea parcă.
- [00:14:44](https://www.youtube.com/watch?v=SSTFFX5heuY&t=884s) — Reluarea citirilor eșuate cu o buclă și o etichetă.
- [00:15:49](https://www.youtube.com/watch?v=SSTFFX5heuY&t=949s) — Înlocuirea saltului de reluare cu continue.
- [00:16:15](https://www.youtube.com/watch?v=SSTFFX5heuY&t=975s) — Respingerea lui zero și a valorilor negative înainte de părăsirea buclei.
- [00:17:41](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1061s) — Extragerea lui InputInt și returnarea valorii validate.
- [00:19:05](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1145s) — Explorarea, apoi respingerea controlului comprimat cu do-while.
- [00:21:45](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1305s) — Adăugarea validării booleene pentru relațiile dintre câmpuri.
- [00:24:07](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1447s) — Afișarea primei reguli încălcate înainte de retur.
- [00:24:39](https://www.youtube.com/watch?v=SSTFFX5heuY&t=1479s) — Acumularea invalidității și raportarea tuturor regulilor încălcate.
