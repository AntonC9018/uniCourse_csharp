> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Construiește un oraș în etape și păstrează neschimbat sensul rezultatului final

[00:00:00](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=0s) Lecția dezvoltă **șablonul builder** printr-un oraș mic, cu locuitori și casele lor. Pornim de la obiecte obișnuite, expunem o eroare de mutare, apoi separăm configurația editabilă de orașul final. Schimbările ulterioare adaugă valori implicite comune, identificatori puternic tipizați, metode de configurare comune și interfețe alternative de builder.

Termenii folosiți pe acest traseu sunt:

- **Model de domeniu, entitate și agregat:** date care reprezintă problema; un cetățean sau o casă este o entitate, iar orașul grupează entitățile înrudite.
- **Referință și graf de obiecte:** o referință indică spre un obiect; referințele dintre obiecte formează un graf. Primul model are legături cetățean–casă, dar omite intenționat o rețea memorată de colocatari.
- **Vector, listă, index, dicționar și cache:** un vector are un număr fix de poziții; o listă poate crește; un index identifică o poziție; un dicționar mapează chei la valori; un cache păstrează un rezultat sau o căutare pregătită anterior.
- **Model mutabil și imutabil:** stare editabilă față de stare care rămâne fixă după construcție. **Protecția superficială** fixează valoarea sau referința unui membru; **imutabilitatea profundă** protejează și tot ce este accesibil prin ea.
- **Constructor, accessor și câmp de susținere:** cod de inițializare, cod de citire sau scriere a unei proprietăți și stocajul aflat la baza unei proprietăți. O **proprietate get-only** expune citirea fără setter; **readonly** restricționează reatribuirea câmpului; **init** permite atribuirea proprietății în timpul inițializării. **required** îi cere apelantului să inițializeze un membru, fără a valida sensul lui.
- **Valoare nullable și operatorul de anulare a null-ului:** o valoare care poate lipsi și operatorul postfix `!` care suprimă un avertisment de nullabilitate al compilatorului, fără să verifice valoarea.
- **Builder și Build terminal:** un obiect care gestionează o configurație incompletă și operația care o validează și creează rezultatul final.
- **API fluent și copie imutabilă:** apeluri de configurare înlănțuite prin valorile returnate; un builder fluent mutabil își returnează propriul obiect, în timp ce o variantă imutabilă produce o valoare de înlocuire.
- **Delegate, lambda și fabrică:** o valoare invocabilă, o funcție inline precum `x => ...` și cod care creează obiecte. Un delegate de configurare permite unei fabrici să controleze când rulează configurația.
- **Domeniu de bloc și domeniu de configurare:** un bloc limitează vizibilitatea unei variabile locale; un domeniu de configurare conține valori implicite copiate în obiectele create prin el.
- **Responsabilitate unică și încapsulare:** separarea unor sarcini precum stocarea datelor și construirea, plus ascunderea detaliilor de implementare în spatele unor operații. Acest exemplu lasă intenționat datele mutabile deschise, iar verificările și conversiile stau în builderi.
- **Validare, excepție și contract:** verificarea faptului că o intrare este acceptabilă, raportarea unei eșecări prin aruncare și specificarea precondițiilor (cerințe înaintea unui apel) și postcondițiilor (garanții după el). **Validarea de prezență** verifică dacă a fost furnizată o valoare; **validarea de interval** verifică ce valori furnizate sunt permise.
- **Sentinel:** o valoare rezervată care înseamnă „nefurnizat”, distinctă de o valoare validă obișnuită.
- **Conversie, serializare și deserializare:** schimbarea unei reprezentări în alta, scrierea datelor într-o reprezentare transferabilă și citirea lor înapoi. Metoda Build din lecție convertește în principal modele în memorie; nu implementează un serializator pentru bază de date sau fișiere.
- **Struct, record struct și identificator puternic tipizat:** un tip valoare, un tip valoare cu egalitate și hash generate, și un înveliș dedicat care identifică un singur tip de entitate în locul unui număr ne-tipizat.
- **Supraîncărcare, siguranță de tip și ID opțional:** metode cu același nume dar cu tipuri diferite de parametri, verificări ale compilatorului împotriva amestecării categoriilor incompatibile și o reprezentare de identificator care permite explicit lipsa.
- **Durată de viață, colectare a gunoiului și referință slabă:** cât timp un obiect rămâne parte din program, recuperarea automată a memoriei și o referință care nu își ține singură ținta vie.
- **Interfață, builder abstract și suprascriere:** un contract implementat de tipuri diferite, o abstractizare de builder care poate ascunde modele sau rezultate diferite și o înlocuire a unei implementări virtuale sau abstracte.
- **new cu tip țintă, delegare și handle:** sintaxă de construcție al cărei tip vine din context, trimiterea unei operații către alt obiect și o valoare care asociază identitatea cu contextul necesar rezolvării ei.
- **Metodă de extensie și this:** o metodă statică apelabilă cu sintaxă de instanță și instanța curentă; `this` pe primul parametru al unei metode de extensie îi identifică receptorul.
- **Dispecerizare pe interfață:** alegerea implementării unui membru de interfață pentru receptorul efectiv; metodele de extensie rămân apeluri statice.
- **Implementare explicită de interfață:** un membru oferit specific prin interfață, alături de o API concretă care poate păstra tipuri mai specifice.
- **Înveliș și builder final:** un obiect în jurul altui obiect și un obiect separat care expune crearea finală din aceeași stare configurată.
- **ImmutableArray și mulțime rară (sparse set):** o opțiune de colecție imutabilă din C# și o structură alternativă de stocare pe chei; ambele sunt menționate, nu implementate în profunzime.

Pentru practică, [laboratorul Builder](../../../labs/2_design/03_builder.md) însoțește această lecție. Exercițiile sale extind demonstrația; SQL, persistența prin ORM, testele automate cuprinzătoare și agregarea avansată a erorilor nu sunt finalizate în înregistrare.

Exemplele de cod de mai jos sunt mostre didactice scurtate, nu un singur program gata de copiat și lipit. Linkurile de commit indică surse istorice inspectate. „Stare asociată” înseamnă că acel commit conține mecanismul relevant, dar nu este o copie exactă a acelui moment din înregistrare. Commit-urile de ID și de builder abstract conțin cod nefinalizat. Orașul funcțional este numit imutabil prin convenție; vectorii publici și câmpurile mutabile ale elementelor nu îl fac profund imutabil.

## Începe cu locuitori, case și un oraș

[00:00:16](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=16s) Un **model de domeniu** înregistrează lucrurile despre care programul vorbește. Declară un Citizen pentru un locuitor și un Home pentru o locuință înainte de a proiecta utilajul de construire. La [00:00:31](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=31s), un locuitor fără adăpost motivează permiterea ca referința la casă să poată lipsi. La [00:00:39](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=39s), adresa unei case este doar un șir; un tip structurat pentru adresă ar distrage atenția de la builder. Numele și vârsta sunt date obișnuite ale locuitorului ([00:00:46](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=46s)).

[Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) păstrează acestea [redactat]. Formă inițială simplificată:

```csharp
sealed class Citizen
{
    public required string Name;
    public required int Age;
    public Home? Home;
}
sealed class Home
{
    public required string Address;
}
sealed class City
{
    public required Citizen[] Citizens { get; init; }
    public required Home[] Homes { get; init; }
}
```

Un **vector** stochează un număr fix de elemente în poziții indexate; orașul păstrează vectori separați pentru locuitori și case. Modificatorul **required** obligă apelanții să inițializeze acești membri, dar nu validează conținutul lor. Aici o **referință** leagă un locuitor de o casă, iar `Home?` exprimă posibilitatea lipsei. Acea permisiune nu face ca orice operație să fie sigură pe un locuitor fără adăpost: o operație care citește casa trebuie să trateze lipsa.

[00:00:55](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=55s) Nu memora un vector de colocatari pe fiecare locuitor. Asemenea legături ar extinde **graful de obiecte**, rețeaua de referințe, și ar duplica o relație deja reprezentată de reședință. La [00:01:19](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=79s), derivă colocatarii prin parcurgerea locuitorilor și gruparea celor cu aceeași casă. La [00:01:41](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=101s), interogarea propusă, dependentă de reședință, presupune că există o casă; aceasta este o precondiție pentru acea operație, nu o inversare permanentă a cazului fără adăpost.

[00:01:59](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=119s) **Agregatul**, City, are nevoie doar de locuitori și case. La [00:02:20](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=140s), un ID cu rol de index în vector este sugerat ca alternativă de tip bază de date la referințele către case. Pentru moment structura este fixă; proiectarea unei baze de date complete este amânată ([00:02:31](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=151s)). Imutabilitatea este descrisă inițial ca un subiect avansat separat ([00:02:33](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=153s)), dar demonstrația următoare explică de ce contează pentru construirea unor obiecte stabile.

## Ce protejează imutabilitatea

[00:02:42](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=162s) Un **accessor init** permite atribuirea unei proprietăți în timpul inițializării obiectului și împiedică o atribuire obișnuită ulterioară. Scopul este de a împiedica modificările accidentale ale unui oraș creat ([00:02:53](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=173s)). **Imutabil** înseamnă că starea rămâne fixă după construcție; **mutabil** înseamnă că se poate schimba ([00:03:04](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=184s), [00:04:08](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=248s)).

[Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) arată proprietățile `init` ale orașului din lecție și referința către oraș păstrată în constructor. Cele două protecții sunt diferite:

```csharp
sealed class City
{
    public required Citizen[] Citizens { get; init; }
    public required Home[] Homes { get; init; }
}
sealed class CityLogic
{
    private readonly City _city;
    public CityLogic(City city) { _city = city; }
}
// city.Citizens = anotherArray; // reatribuire obișnuită ulterioară interzisă
// city.Citizens[0] = anotherCitizen; // conținutul vectorului rămâne scrisibil
```

[00:03:14](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=194s) O componentă care primește orașul dorește ca alt cod să nu modifice în tăc datele de care depinde. Primirea unei referințe obișnuite la obiect nu conferă proprietate exclusivă: altcineva poate ține aceeași referință și poate modifica obiectul în timpul unui apel sau între apeluri ([00:03:24](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=204s), [00:03:44](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=224s)). Imutabilitatea reală permite componentei să se bazeze pe date stabile acum și mai târziu ([00:03:53](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=233s)).

[00:04:12](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=252s) Un oraș mutabil poate fi copiat în schimb într-un stocaj privat, deținut de componentă. O copie stabilă și independentă poate necesita copierea și a datelor mutabile imbricate, ceea ce costă timp și memorie. Copierea doar a referinței exterioare nu oferă nicio protecție.

[00:04:35](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=275s) Un **constructor** este codul de inițializare executat la crearea unei instanțe. Trecerea orașului prin constructorul său fixează ce oraș folosește logica. O **proprietate get-only** expune citirea fără setter; nu este în mod inerent privată. Modificatorul ei de acces determină cine o poate citi. La [00:04:57](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=297s), restricția importantă este că apelanții nu o pot atribui. Nici un **câmp readonly** nu poate fi reatribuit de o metodă obișnuită; poate fi inițializat la declarare sau în constructorul tipului care îl declară ([00:05:01](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=301s), [00:05:11](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=311s)). Niciuna dintre acestea nu îngheață obiectul referit. Atribuirea în constructor, proprietățile get-only și câmpurile readonly sunt restricții utile la nivelul limbajului, nu dovezi suficiente de imutabilitate profundă.

## Rupe o căutare în cache cu o mutare

[00:05:18](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=318s) O parcurgere nouă poate reflecta locuitorii curenți la fiecare apel. Pentru a evita parcurgerea repetată a vectorului, introducem un **cache**, o căutare pregătită anterior. Un **dicționar** mapează o cheie la o valoare; aici mapează numele unui locuitor la un **index** de vector, nu direct la obiectul Citizen ([00:05:29](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=329s)). Astfel, corectitudinea lui depinde de păstrarea acestor locuitori în aceleași poziții.

[Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) conține căutarea în cache și CityChanger folosite de această demonstrație. Implementare scurtată:

```csharp
// În constructorul lui CityLogic:
for (int i = 0; i < city.Citizens.Length; i++)
    byName[city.Citizens[i].Name] = i;

// În Search:
if (!byName.TryGetValue(name, out int index))
    return null;
return city.Citizens[index];

// În CityChanger.Change:
city.Citizens[0] = new Citizen { Name = "Joe", Age = 18 };
```

Urmează eșecul în ordine:

1. [00:05:59](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=359s) Pregătește maparea în constructor. La [00:06:16](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=376s), adaugă fiecare locuitor și păzește căutarea astfel încât un nume necunoscut să întoarcă null.
2. [00:06:29](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=389s) Adaugă o altă componentă care poate înlocui poziția zero din vector.
3. [00:06:51](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=411s) Construiește un oraș mic care conține John și un locuitor fără casă.
4. [00:07:18](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=438s) Caută John și afișează numele returnat. Prima căutare îl întoarce pe John ([00:07:32](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=452s)).
5. [00:07:56](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=476s) Rulează CityChanger și caută din nou John. Rezultatul observat este locuitorul de înlocuit, Joe, nu John.

[00:07:42](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=462s) **Operatorul de anulare a null-ului**, `!`, doar suprimă un avertisment de nullabilitate, ca în `logic.Search("John")!.Name`. Nu face nicio verificare la runtime și nu garantează un rezultat diferit de null.

[00:08:26](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=506s) Dicționarul spune în continuare „John este în poziția zero”, dar acea poziție conține acum Joe. Codul a păstrat referința către oraș și o poziție în cache, nu o copie înghețată a lui John. Cache-ul unei referințe directe către Citizen ar fi avut alt comportament: înlocuirea doar a poziției din vector nu ar fi înlocuit acea referință în cache. Defectul demonstrat este exact un cache de indici peste stocaj mutabil ([00:08:48](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=528s)). La [00:08:57](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=537s), această modificare permisă este chiar sensul unui obiect mutabil.

<details>
<summary>Prezică a doua căutare: de ce nu garantează cheia John că rezultatul este John?</summary>

Cheia selectează indexul păstrat în cache în timpul construcției. Search citește apoi celula curentă din vector. Înlocuirea acelei celule schimbă obiectul returnat fără să actualizeze relația nume–index din cache.

</details>

## Separă protecția superficială de imutabilitate

[00:09:13](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=553s) Protejarea unei proprietăți împotriva reatribuirii este **protecție superficială**. **Imutabilitatea profundă** ar împiedica și modificarea celulelor vectorului și a câmpurilor obiectelor locuitor și casă. C# are tipuri de colecții imutabile precum ImmutableArray, ale căror elemente nu pot fi suprascrise prin atribuire pe index; totuși, o colecție imutabilă nu îngheață obiectele mutabile stocate în ea.

[Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) păstrează comentariul explicit „nu trebuie schimbat” alături de vectori scrisibili și câmpuri publice de entitate. O ilustrare simplificată:

```csharp
// Acesta este un model imutabil și nu trebuie schimbat.
sealed class City
{
    public required Citizen[] Citizens { get; init; }
    public required Home[] Homes { get; init; }
}
// Convenția poate fi totuși încălcată:
city.Citizens[0] = new Citizen { Name = "Joe", Age = 18 };
city.Citizens[0].Name = "Changed";
```

[00:09:45](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=585s) Corectarea este la ipoteza logicii: aceasta a fost scrisă pentru un oraș neschimbabil, dar i-a fost dat unul mutabil. Siguranța cere o garanție reală de stabilitate ([00:10:01](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=601s)). Fie faci o copie independentă cu o funcție dedicată de copiere, fie ceri întregului program să trateze orașul partajat ca imutabil. Când consumatorii doar îl citesc sau îl înlocuiesc cu o instanță nouă, logica dependentă devine mai simplă ([00:10:17](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=617s)).

[00:10:42](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=642s) În această etapă, celulele vectorului și obiectele elementelor rămân scrisibile. Abordarea aleasă este **imutabilitatea conceptuală**, o convenție exprimată într-un comentariu și respectată de fiecare consumator ([00:10:56](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=656s), [00:11:11](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=671s)). La [00:11:24](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=684s), o proprietate get-only este din nou luată în calcul: împiedică înlocuirea referinței vectorului, dar permite în continuare pătrunderea în vector și schimbarea celulelor. Aceasta este o limitare a acestui model, nu o afirmație că modelele profund imutabile în C# sunt imposibile.

[00:11:44](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=704s) Lecția nu explorează în profunzime ImmutableArray, pentru că vrea ca ideea de design să se transfere între limbaje. Membrii get-only sau readonly pot restricționa atribuirile individuale; un struct stochează valori, nu obiecte de referință alocate separat, dar un struct în sine nu interzice mutația ([00:11:56](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=716s)). Orice programator care lucrează cu convenția trebuie să evite schimbarea modelului final ([00:12:11](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=731s)).

## Mută construcția într-o zonă de pregătire

[00:12:21](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=741s) Un **vector** are lungime fixă și nu are operație de adăugare. O **listă** este colecția mutabilă care poate crește, necesară cât timp numărul de locuitori încă se stabilește. Nici o proprietate de vector inițializabilă nu poate fi înlocuită ulterior. Aceasta creează o problemă de construcție: chiar înainte ca obiectul final să fie predat cuiva, dependențele sale obligatorii trebuie să fie deja gata ([00:12:31](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=751s), [00:12:49](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=769s)).

[00:13:33](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=813s) Înainte de a extinde mecanismele de imutabilitate, examinează inițializarea directă. [Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) conține helperul de construcție directă și builderul funcțional ulterior. Contrast scurtat:

```csharp
var home = new Home { Address = "123" };
var john = new Citizen { Name = "John", Age = 18, Home = home };
var city = new City { Homes = [home], Citizens = [john] };

// Mai târziu, folosind configurația editabilă:
var builder = new CityBuilder();
var citizen = builder.Citizen();
citizen.Name("John");
citizen.Age(18);
var homeBuilder = builder.Home();
homeBuilder.Address("123");
citizen.Home(homeBuilder);
var builtCity = builder.Build();
```

[00:13:41](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=821s) Pornește invers, de la ce are nevoie orașul, apoi creează dependențele înaintea celor care depind de ele:

1. Creează casele, care în acest exemplu nu depind de nimic.
2. Creează locuitorii, care pot face referire la case.
3. Creează orașul la sfârșit, enumerând manual acele case și locuitori în vectorii săi ([00:14:09](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=849s)).

[00:14:26](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=866s) Un **builder** mută configurarea incompletă într-o zonă de pregătire editabilă. Creează întâi CityBuilder, apoi înregistrează locuitorii și casele prin el ([00:14:41](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=881s)). Acesta nu este încă City final. La [00:14:52](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=892s), un locuitor poate fi configurat după înregistrare. La [00:15:04](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=904s), chiar și casa lui poate fi creată mai târziu, cu condiția să existe înainte de atribuire. Acea flexibilitate privește obiectele de configurare mutabile, nu modificarea unui oraș imutabil deja finalizat.

[00:15:21](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=921s) Metoda terminală **Build** creează rezultatul final. Builderul păstrează un model mutabil în curs; orașul produs este intenționat un model imutabil ([00:15:32](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=932s)). Beneficiul este că consumatorii se pot baza pe faptul că orașul final rămâne stabil ([00:15:44](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=944s)), sub rezerva convenției și a limitărilor protecției superficiale tocmai stabilite.## Introdu tipurile builderului înaintea implementării lor

[00:15:54](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=954s) Pentru a face posibilă sintaxa de configurare în etape, introdu întâi tipurile de care are nevoie. CityBuilder este builderul exterior ([00:16:05](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=965s)); operația sa de creare a unui locuitor trebuie să întoarcă un CitizenBuilder, care configurează acel locuitor ([00:16:08](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=968s)).

[Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) este o versiune ulterioară implementată, nu o copie exactă a acestor corpuri de metodă goale. Schiță simplificată a API-ului:

```csharp
sealed class CityBuilder
{
    public CitizenBuilder Citizen() => /* înregistrează modelul */ throw new NotImplementedException();
    public HomeBuilder Home() => /* înregistrează modelul */ throw new NotImplementedException();
}
sealed class CitizenBuilder
{
    public void Name(string name) { /* ulterior: salvează numele */ }
    public void Age(int age) { /* ulterior: salvează vârsta */ }
    public void Home(HomeBuilder home) { /* ulterior: salvează modelul casei */ }
}
sealed class HomeBuilder { }
```

[00:16:22](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=982s) Adaugă tipuri separate CitizenBuilder și HomeBuilder. La [00:16:35](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=995s), adaugă metodele de configurare a locuitorului. Păstrează clasele mici, în loc să amesteci configurarea tuturor obiectelor într-o singură clasă. La [00:16:44](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1004s), acceptă un HomeBuilder astfel încât casa să rămână configurabilă; nu cere prematur Home final. HomeBuilder este intenționat gol în această etapă doar de interfață ([00:16:53](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1013s)).

[00:17:03](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1023s) O consecință a construcției în etape cu obiecte mutabile este o completitudine la timp de compilare mai slabă. Stilul de construcție anterior, cu date `required` sau furnizate prin constructor, poate forța inițializarea la o anumită limită. Un builder permite existența unui locuitor înainte ca numele său să fie stabilit. Uitarea numelui compilează totuși ([00:17:20](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1040s)).

[00:17:26](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1046s) O valoare obligatorie lipsă poate fi depistată la construirea întregului oraș. O **eroare de validare** înseamnă că configurația completată nu poate produce un rezultat acceptabil. Mesajul dorit ar identifica locuitorul fără nume, dar implementarea validării este lăsată pentru un pas ulterior ([00:17:29](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1049s)). În această etapă intermediară există forma API-ului; ea nu stochează toate datele și nu stabilește validitatea.

## Alege configurare fluentă sau copii imutabile

[00:17:44](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1064s) Un **API fluent** unește apelurile de configurare într-o singură expresie. Fiecare apel întoarce o valoare pe care se poate opera cu următorul apel. În builderul fluent mutabil, acea valoare este chiar același builder ([00:17:52](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1072s), [00:18:06](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1086s)).

[Stare builder fluent](https://github.com/AntonC9018/uniCourse_csharp/blob/1b854ac5ffdf93de5bd0410f39849f7889a1d280/examples/builder/Program.cs) conține metode care returnează `this`. Exemplu scurtat:

```csharp
public CitizenBuilder Name(string name)
{
    Model.Name = name;
    return this;
}
// city.Citizen().Name("John").Age(18).Home(home);
```

[00:18:17](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1097s) Returnarea lui CitizenBuilder din Name ține lanțul viu. Înlănțuirea nu impune prin ea însăși mutație: o alternativă funcționează cu **copii imutabile**, adică valori noi care păstrează toate câmpurile neschimbate ([00:18:24](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1104s)). La [00:18:30](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1110s), alocarea și consumul de memorie sunt compromisul. WithHome poate crea un locuitor complet nou, cu altă casă, și poate copia celelalte câmpuri ([00:18:35](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1115s)); WithName poate apoi crea o altă înlocuire, cu alt nume ([00:18:43](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1123s)). Această alternativă este descrisă, nu implementată ca builderul de oraș ales. Stilul fluent descrie sintaxa apelurilor; faptul că apelurile mută sau copiază este o decizie de design separată.

## Ia în calcul un delegate de configurare

[00:18:50](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1130s) Un alt stil de configurare acceptă un **delegate**, o valoare invocabilă, și execută configurarea în interiorul lui. O **lambda** este o funcție inline precum `john => ...`. [Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) folosește în schimb apeluri separate; este utilaj de builder înrudit, nu o implementare comisă a alternativei cu delegate. Formă ilustrativă de apel:

```csharp
// Schiță alternativă de API, absentă din CityBuilder-ul comis:
city.Citizen(john =>
{
    john.Name("John");
    john.Age(18);
    john.Home(home);
});
```

[00:18:58](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1138s) Metoda de creare primește callbackul și îi controlează invocarea. În interior, stabilește numele și, dacă este potrivit, casa ([00:19:04](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1144s)). Avantajul intenționat este că variabila locală de locuitor nu este disponibilă codului ulterior ca obiect semiconfigurat ([00:19:13](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1153s), [00:19:16](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1156s)).

[00:19:37](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1177s) John este un parametru local a cărui vizibilitate se încheie odată cu blocul callbackului. Tipurile parametrilor pot fi inferate din delegate-ul așteptat de API ([00:19:47](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1187s)). O **fabrică**, cod responsabil de crearea obiectului, poate de asemenea alege să ruleze callbackul mai târziu ([00:19:50](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1190s)). Acestea sunt alegeri de design de API: C# nu interzice capturarea builderului într-o variabilă exterioară, deci un callback în sine nu este o garanție de runtime că obiectul nu va evada.

[00:19:58](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1198s) Compromisul este că variabila locală John nu este disponibilă după configurare. Creează casa înaintea callbackului sau în interiorul lui ([00:20:06](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1206s)); fără a păstra sau returna o referință, codul ulterior nu poate configura acel obiect local ([00:20:12](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1212s)). O variantă poate returna obiectul de configurare creat, astfel încât pregătirea să poată continua în exterior ([00:20:19](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1219s)).

[00:20:30](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1230s) Implementarea continuă cu cea mai simplă schemă: configurează un model mutabil, apoi îl convertește în rezultatul imutabil intenționat. Nu implementează fiecare variantă de callback sau de copie imutabilă.

## Stochează configurația în modele mutabile

[00:20:46](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1246s) La început metodele de configurare există, dar nu fac nimic. Trecerea unui nume trebuie în cele din urmă să facă acel nume lizibil din orașul construit, de exemplu la afișarea lui John ([00:20:51](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1251s)). Stub-urile actuale elimină argumentele ([00:21:10](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1270s)). Introdu un **model mutabil**, un obiect de date ai cărui câmpuri pot fi completate după creare, pentru a le păstra ([00:21:16](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1276s)).

[Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) conține modelele mutabile implementate. Formă simplificată:

```csharp
sealed class MutableCity
{
    public List<MutableCitizen> Citizens = new();
    public List<MutableHome> Homes = new();
}
sealed class MutableCitizen
{
    public const int InvalidAge = -1;
    public int Age = InvalidAge;
    public string? Name;
    public MutableHome? Home;
}
sealed class MutableHome
{
    public string? Address;
}
```

[00:21:27](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1287s) **Responsabilitatea unică** separă stocarea datelor de comportamentul de construire. La [00:21:34](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1294s), adaugă clasa de oraș mutabil, care conține doar date. Configureaz-o după creare, apoi convertește-o în modelul final ([00:21:40](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1300s)). Un MutableCitizen poartă valorile introduse în timpul configurării orașului ([00:21:47](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1307s), [00:21:57](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1317s)). Vârsta lui poate fi atribuită mai târziu; tocmai aceasta este ideea pregătirii mutabile ([00:22:01](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1321s), [00:22:09](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1329s)).

[00:22:16](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1336s) Zero este ambiguu pentru un câmp întreg: poate fi o vârstă furnizată sau valoarea implicită lăsată de o configurare uitată. Un **sentinel** este o valoare rezervată care înseamnă „nefurnizat”. La [00:22:24](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1344s), folosește o vârstă specială, nesetată; sursa comisă o numește InvalidAge și folosește -1. La [00:22:30](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1350s), folosește null pentru un nume nesetat, în loc să tratezi șirul gol ca absent automat. O referință nullable oferă această reprezentare distinctă a absenței ([00:22:34](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1354s)); dacă un nume gol ar trebui permis este o politică de validare separată.

[00:22:40](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1360s) Adaugă MutableHome cu o adresă inițial absentă, furnizată în timpul pregătirii ([00:22:47](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1367s)). Builderul păstrează referința către modelul mutabil ([00:22:59](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1379s)). Sarcina lui este să configureze aceste modele și să producă reprezentarea finală ([00:23:03](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1383s)). Un avantaj suplimentar al expunerii modelelor este amânat pentru scurt timp ([00:23:10](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1390s)).

## Păstrează datele deschise și pune contractele pe configurare

[00:23:25](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1405s) Modelul mutabil este util și în afara builderului. Codul de configurare de nivel mai înalt poate accesa câmpurile și își poate implementa propriile operații ([00:23:35](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1415s)). **Încapsularea** înseamnă controlul accesului printr-o abstractizare; aici modelul de date este intenționat deschis, iar încapsularea verificărilor și a construcției stă în builder.

[Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) arată referința publică către model și metodele de configurare. Exemplu scurtat:

```csharp
sealed class CitizenBuilder
{
    public readonly MutableCitizen Model;
    public CitizenBuilder(MutableCitizen model) { Model = model; }

    public void Age(int age)
    {
        if (age < 0) throw new InvalidOperationException("Invalid age");
        Model.Age = age;
    }
}
public void Address(string address) { Home.Address = address; }
```

[00:23:46](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1426s) Adaugă un **înveliș**, un obiect care oferă operații în jurul modelului furnizat, expune public referința sa ([00:23:55](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1435s)) și păstrează acea referință în timpul construcției ([00:24:00](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1440s)). La [00:24:10](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1450s), membrul de configurare a adresei scrie în modelul casei. Inițial aceste operații sunt aproape atribuiri directe de câmp ([00:24:15](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1455s)). Metodele pot comunica intenția mai clar decât atribuirile brute, dar acea alegere este parțial de stil ([00:24:24](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1464s)).

[00:24:29](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1469s) Vârsta ar putea fi o proprietate sau o metodă. O metodă poate întoarce o valoare utilă pentru înlănțuire sau pentru construcția finală ([00:24:54](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1494s)); un getter de proprietate întoarce și el o valoare, în timp ce un setter nu întoarce receptorul pentru un lanț fluent. Pentru citiri și scrieri simple, cele două forme pot exprima comportamente asemănătoare ([00:25:10](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1510s)). Atât metodele, cât și setterii de proprietate pot face validări; forma cu metodă nu este singura capabilă să respingă vârstele negative ([00:25:22](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1522s)).

[00:25:40](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1540s) Un **contract** descrie precondiții și postcondiții: ce intrări sunt permise și ce garantează operația. Paza pentru vârstă negativă exprimă o precondiție, nu o dovadă că toate câmpurile obligatorii au fost furnizate. Modelul deschis permite operații externe de nivel mai înalt ([00:25:46](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1546s), [00:25:52](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1552s)), dar editările directe de câmp ocolesc verificările de metodă ale builderului. Build trebuie totuși să valideze modelul final.

## Înregistrează modelele și începe conversia

[00:26:02](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1562s) Build are nevoie de locuitori și case înregistrate înainte de a-i putea converti. Adaugă două liste inițial goale. O **listă** crește pe măsură ce se creează obiectele de configurare; fiecare operație de creare face un model, îl înregistrează și întoarce builderul său ([00:26:11](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1571s)).

[Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) conține codul de înregistrare și de conversie. Înregistrarea locuitorului și prima trecere de conversie, scurtate:

```csharp
public CitizenBuilder Citizen()
{
    var m = new MutableCitizen();
    Model.Citizens.Add(m);
    return new(m);
}
// În Build, înainte de adăugarea validării complete:
var result = new Citizen[Model.Citizens.Count];
for (int i = 0; i < result.Length; i++)
{
    var mut = Model.Citizens[i];
    result[i] = new Citizen { Name = mut.Name!, Age = mut.Age };
}
```

Metoda de creare întoarce CitizenBuilder, nu un City final. La [00:26:22](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1582s), **new cu tip țintă**, `new(m)`, omite numele tipului pentru că contextul de return furnizează CitizenBuilder. La [00:26:23](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1583s), un nume local scurt precum `m` este acceptabil în această metodă de trei rânduri, pentru că rolul său este imediat vizibil; aceasta nu este o regulă care favorizează API-urile publice cu o singură literă.

[00:27:00](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1620s) Metoda de creare a caselor urmează același tipar de înregistrare. Orașul mutabil rămâne accesibil public; alternativ, un model pregătit separat poate fi furnizat ca parametru ([00:27:11](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1631s)).

[00:27:19](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1639s) **Conversia** schimbă modelele editabile în modele finale: listele devin vectori și modelele dependente sunt convertite la rândul lor. La [00:27:33](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1653s), alocă vectorul de locuitori ca să corespundă numărului din listă; la [00:28:19](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1699s), parcurge-l; la [00:28:30](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1710s), citește fiecare locuitor mutabil și completează poziția corespunzătoare din rezultat.

[00:28:50](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1730s) Vârsta poate fi încă nesetată, deci are nevoie de o pază. Verificările de interval sunt amânate la acest pas ([00:29:02](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1742s)). La [00:29:04](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1744s), copiază numele final configurat; editările ulterioare făcute înainte de Build trebuie să se reflecte în rezultat ([00:29:44](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1784s)). Validarea numelui este de asemenea amânată aici. Această primă trecere stabilește maparea, nu încă o conversie completă și sigură.

## Convertește casele întâi și păstrează referințele comune

[00:29:45](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1785s) Casa în pregătire a unui locuitor este o referință către un MutableHome, nu un index și nu Home final. Rezolvă acea relație păstrând un **dicționar** de la fiecare casă mutabilă la perechea sa imutabilă convertită ([00:29:46](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1786s)). [Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) păstrează această abordare funcțională bazată pe referințe. Secvență scurtată:

```csharp
var homes = new Home[Model.Homes.Count];
for (int i = 0; i < homes.Length; i++)
    homes[i] = ConvertHome(Model.Homes[i]); // validate, then copy

var map = new Dictionary<MutableHome, Home>(homes.Length);
for (int i = 0; i < homes.Length; i++)
    map.Add(Model.Homes[i], homes[i]);

// While converting each citizen:
Home? home = mut.Home is null ? null : map[mut.Home];
return new City { Homes = homes, Citizens = citizens };
```

Numele helperului ConvertHome abreviază conversia inline din sursă. Execută acești pași în ordine:

1. [00:29:47](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1787s) Finalizează configurația și creează casele finale înainte de a converti locuitorii care au nevoie de ele.
2. [00:29:48](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1788s) Păstrează referința la casa mutabilă drept cheie de căutare. Obiectul final nu există în timpul configurării anterioare ([00:31:04](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1864s), [00:31:17](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1877s)).
3. [00:31:18](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1878s) Alocă dicționarul mutabil–imutabil cu o capacitate care corespunde numărului de case ([00:31:20](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1880s)).
4. [00:31:21](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1881s) Convertește fiecare casă și stocheaz-o în vectorul de rezultat. La [00:32:27](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1947s), păstrează fiecare casă produsă; la [00:32:28](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1948s), o a doua buclă populează dicționarul folosind exact aceleași elemente de vector.
5. [00:33:43](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2023s) Pentru un locuitor cu casă, recuperează casa deja convertită; pentru o casă absentă, lasă null.
6. [00:33:46](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2026s) Pune ambii vectori în City și întoarce-l.

[00:31:24](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1884s) MutableHome și Home par aproape dublate: fiecare are o adresă, cu intrare nullable, respectiv required, jucând roluri diferite. La [00:32:22](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1942s), această asemănare este revizuită. Corectarea este că acestea sunt abstractizări independente, care pot evolua separat ([00:32:23](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1943s)); codul repetitiv de conversie reprezintă costul real ([00:32:25](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1945s)).

[00:32:29](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1949s) Reutilizează Home final, deja aflat în vectorul de rezultat. Astfel se păstrează partajarea: doi locuitori care indică spre o casă în pregătire indică spre o singură casă finală, nu spre două copii reconstruite independent. Sensul dicționarului este **de la mutabil la imutabil**, în ciuda formulării ambigue din narare ([00:32:30](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=1950s), [00:33:42](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2022s)).

[00:33:44](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2024s) ID-urile ar putea elimina acest dicționar de relații dacă colecțiile în pregătire și cele finale păstrează aceiași identificatori și poziții ([00:33:45](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2025s)). O casă în pregătire absentă este permisă. O casă în pregătire nenulă care nu a fost niciodată înregistrată în acest oraș este altceva: indexerul din dicționarul comis aruncă pentru o cheie lipsă; nu transformă în tăcere orice asociere lipsă în lipsă de adăpost.

## Validează configurația completă a caselor

[00:33:48](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2028s) Înregistrarea ajunge la o conversie de la un capăt la altul, apoi trece la **validare**, adică la verificarea faptului că intrarea completată respectă regulile. Succesul anterior este o verificare de dezvoltare înregistrată, cu probleme rămase deschise, nu o dovadă că fiecare commit ulterior compilează.

[00:33:49](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2029s) Cere o adresă pentru fiecare casă. Această verificare a obiectului complet aparține lui Build, după ce configurarea s-a încheiat, pentru că o etapă anterioară este legitim incompletă ([00:33:50](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2030s)). [Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) conține verificarea implementată:

```csharp
if (mut.Address is not { } address)
    throw new InvalidOperationException("Validation error: address not specified");

var home = new Home { Address = address };
```

Modelul `is not { } address` ia ramura de eșec pentru null; pe traseul de succes leagă valoarea adresei nenule. La [00:33:51](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2031s), adresele absente sau goale sunt discutate ca nespecificate; la [00:35:11](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2111s), eroarea ar trebui să explice că lipsea configurarea. **Verificarea comisă de mai sus respinge doar null: șirul gol trece.** Pentru a respinge și șirurile goale ar fi nevoie de o condiție suplimentară. Nu confunda regula mai bogată intenționată cu implementarea ei minimală.

[00:35:35](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2135s) O diagnoză mai utilă ar putea numi indexul casei și alte valori cunoscute. O altă variantă caută locuitorii care fac referire la casă, astfel încât casa vinovată să fie identificabilă chiar și fără adresă ([00:35:50](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2150s)). O metodă de creare ar putea captura rândul sursă al apelantului și să îl păstreze pentru raportare ([00:35:59](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2159s)).

[00:36:15](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2175s) Aceste diagnostici mai bogate sunt alternative, nu funcții finalizate. Alege o **excepție** simplă, o eșecare care întrerupe execuția normală, imediat la primul obiect rău ([00:36:20](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2180s)). Aceasta păstrează demonstrația scurtă. La [00:36:28](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2188s), o variantă avansată ar aduna fiecare eroare și rândul său sursă, ar raporta împreună toate casele vinovate ([00:36:38](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2198s)) și ar pune acel utilaj de validare în spatele abstractizării de builder ([00:36:42](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2202s)). Build implementat aruncă la prima eroare; nu acumulează un raport complet.

## Validează locuitorii și distinge vârsta nesetată

[00:36:51](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2211s) În timp ce convertești un locuitor, traducă-i casa opțională în pregătire prin dicționarul de relații. Fără referință la o casă nu există casă în rezultat. [Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) conține rezolvarea casei, verificarea numelui și verificarea sentinelului de vârstă. Conversia locuitorului, scurtată:

```csharp
Home? home = mut.Home != null ? mutToImmutHome[mut.Home] : null;

if (mut.Name is not { } name)
    throw new InvalidOperationException("Name not given for citizen");
if (mut.Age == MutableCitizen.InvalidAge)
    throw new InvalidOperationException("Name not given"); // source's mismatched message

var result = new Citizen { Name = name, Age = mut.Age, Home = home };
```

[00:37:08](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2228s) Numele este obligatoriu în politica aleasă. Dacă ramura pentru nume lipsă aruncă, celălalt traseu poate atribui numele furnizat efectiv fără a lăsa să treacă o valoare neinițializată ([00:37:18](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2238s)). O altă politică de builder ar putea tolera nume lipsă ([00:37:26](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2246s)) și ar furniza „Unknown” ca valoare de rezervă ([00:37:31](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2251s)). Aceasta este o alternativă, nu comportamentul ales.

[00:37:47](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2267s) Ascunde validarea și transformarea în spatele abstractizării de conversie. Apelanții primesc rezultatul final fără să cunoască fiecare ramură ([00:38:02](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2282s)). Acea metodă ascunsă este un loc natural pentru a adăuga verificări pe măsură ce modelele cresc ([00:38:08](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2288s)).

[00:38:16](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2296s) Revino la marcajul valorii nesetate. Marcajul numeric important aparține **vârstei**; sursa inspectată îl numește MutableCitizen.InvalidAge. Numele folosește deja null. La [00:38:25](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2305s), detectează dacă vârsta a fost furnizată comparând cu sentinelul, nu ținând un indicator separat.

[00:38:32](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2312s) Corectarea este să testezi egalitatea cu marcajul de nesetat, nu doar `age < 0`. **Validarea de prezență** întreabă „a fost configurat?”; **validarea de interval** întreabă „este permisă această valoare?”. Sunt distincte ([00:38:41](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2321s)). Metoda Age respinge argumentele negative, dar editările publice ale modelului ocolesc acea metodă, iar verificarea curentă din Build respinge doar sentinelul. Mesajul „Name not given” din ramura vârstei este un defect de copiere-lipire, nu o dovadă că eșecul privește numele.

[00:38:53](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2333s) Revizuiește verificările copiate și asigură-te că locuitorul validat este introdus în răspuns. O casă nullable este permisă; numele lipsă sau vârstele nesetate nu sunt.

## Finalizează buildul și verifică succesul și eșecul

[00:38:58](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2338s) Consolidează designul: **MutableCity** conține liste de modele mutabile dependente; **City** final conține vectori de obiecte Citizen și Home convertite. Această distincție corectează orice descriere a orașului imutabil ca conținând liste de modele mutabile.

[Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) conține conversia funcțională și demonstrația inițială a domeniului. Formă de construcție scurtată:

```csharp
var city = new CityBuilder();
var home = city.Home();
home.Address("123");
var john = city.Citizen();
john.Name("John");
john.Age(18);
john.Home(home);
var result = city.Build();
Console.WriteLine(result.Citizens[0].Name); // John
```

[00:39:07](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2347s) CityBuilder creează builderii dependenți și păstrează orașul mutabil ca câmp, astfel încât conversia să poată citi toată intrarea în pregătire ([00:39:13](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2353s)). La [00:39:24](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2364s), Build preia acel model memorat și produce un City final. El validează fiecare intrare înainte de a o copia într-un obiect de rezultat ([00:39:34](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2374s)). Aceeași secvență validează-apeoi-convertește se repetă pentru obiectele și colecțiile imbricate ([00:39:43](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2383s)). Nu este neapărat o singură trecere globală de validare înaintea oricărei alocări: casele sunt validate și convertite înaintea locuitorilor.

[00:39:48](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2388s) CitizenBuilder își păstrează propriul model mutabil și expune operații de configurare. El nu are intenționat un Build independent: un locuitor fără contextul de oraș este de puțin folos în acest exemplu ([00:40:00](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2400s)). HomeBuilder urmează același design de înveliș mic ([00:40:16](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2416s)).

Verifică rezultatul în ordine:

1. [00:40:21](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2421s) Rulează exemplul configurat. Înregistrarea afișează John, arătând că refactorizarea a păstrat acest traseu.
2. [00:40:27](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2427s) Inspectează obiectele construite în debugger. Orașul conține locuitorul și casa configurate ([00:40:31](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2431s)), inclusiv John și adresa „123” ([00:40:35](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2435s)).
3. [00:40:46](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2446s) Elimină configurarea numelui și construiește din nou. Validarea eșuează cu mesajul „Name not given”.
4. [00:40:58](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2458s) Rulează cazul care eșuează sub debugger, pentru a identifica rândul care aruncă și a inspecta starea din jur.

Acestea sunt verificările din video, nu teste nou executate. Programul ulterior cu vârsta implicită din sursa citată afișează vârstele, nu această ieșire anterioară cu doar John.

## Introdu domenii pentru valori implicite comune

[00:41:13](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2473s) Adaugă încă un locuitor, inițial tot un John în vârstă de 15 ani, care împarte casa existentă. Repetarea atribuirii casei pentru fiecare locuitor sugerează o valoare implicită reutilizabilă.

[00:41:32](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2492s) Un **domeniu de bloc** limitează vizibilitatea localelor temporare. Builderii și variabilele de casă necesare doar în timpul configurării pot fi ținute în interiorul blocurilor ([00:41:36](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2496s)). Un **domeniu de configurare** merge mai departe: este un obiect care conține setări comune de aplicat obiectelor create prin el ([00:41:43](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2503s)).

[Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) conține mecanismul de domeniu completat. Utilizare simplificată:

```csharp
var scope = city.Scope();
scope.Home(home);
scope.Age(32);

var john = scope.Citizen();
john.Name("John");
john.Age(18);
var joe = scope.Citizen();
joe.Name("Joe");
```

[00:41:51](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2511s) În loc să treci aceeași casă fiecărui locuitor, configureaz-o o singură dată pe domeniu. Acel domeniu devine un bloc de construcție reutilizabil ([00:42:07](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2527s)). Locuitorii creați în raport cu el moștenesc configurația comună fără argumente repetate ([00:42:13](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2533s)), indiferent câți sunt creați ([00:42:21](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2541s)).

[00:42:26](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2546s) Adaugă Scope la CityBuilder, întoarcând un obiect cu operații de creare Citizen și Home. Inițial aceste operații există fără API-ul complet de configurare ([00:42:51](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2571s)). El mai are nevoie și de metodele de configurare Name, Age și Home ([00:43:11](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2591s)).

[00:43:22](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2602s) Un domeniu general poate susține mai multe tipuri de creere decât un domeniu doar pentru locuitori; setările implicite ale lecției rămân setări pentru locuitori. La [00:43:33](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2613s), un model de valori implicite pentru locuitor furnizează acele câmpuri. Dacă s-ar păstra doar acel model și s-ar rescrie fiecare setter, logica de configurare s-ar duplica. Păstrează în schimb un CitizenBuilder pentru valorile implicite ([00:43:44](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2624s)), apoi delegează la metodele sale ([00:43:59](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2639s)). **Delegarea** înseamnă aici trimiterea unei operații către acel builder conținut; este distinctă de delegate-ul de configurare invocabilă de mai devreme.

[00:44:06](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2646s) Helperi statici externi ar putea configura și ei modelul deschis. Această versiune păstrează inițial logica împreună în builder, în loc să o împrăștie printre helperi ([00:44:13](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2653s)); mai târziu, metodele de extensie centralizează intenționat helperii comuni.

## Creează locuitori din valorile implicite ale domeniului

[00:44:31](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2671s) Fiecare domeniu primește un model de valori implicite nou și propriul builder în jurul acelui model. Creerea poate avea loc într-un constructor sau într-o fabrică, în funcție de designul înconjurător ([00:44:42](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2682s)); discuția despre metoda fabrică este amânată explicit ([00:44:50](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2690s)).

[Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) conține constructorul și comportamentul de copiere la creare. Implementare scurtată:

```csharp
public CityScopeBuilder(CityBuilder city)
{
    CityBuilder = city;
    CitizenBuilder = new CitizenBuilder(new MutableCitizen());
}
public CitizenBuilder Citizen()
{
    var citizen = CityBuilder.Citizen();
    var defaults = CitizenBuilder.Model;
    if (defaults.Age != MutableCitizen.InvalidAge) citizen.Model.Age = defaults.Age;
    if (defaults.Name is { } name) citizen.Model.Name = name;
    if (defaults.Home is { } home) citizen.Model.Home = home;
    return citizen;
}
// CityBuilder.Scope(): return new(this);
```

[00:45:00](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2700s) Domeniul trebuie să păstreze accesul la builderul de oraș, deoarece locuitorii creați prin el trebuie înregistrați în lista aceluiași oraș. La [00:45:14](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2714s), creează locuitorul prin acel builder părinte, copiază valorile implicite configurate și întoarce propriul builder al locuitorului ([00:45:20](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2720s)). Aceasta este creare de model în etape, nu construcție terminală a unui locuitor imutabil.

[00:45:28](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2728s) Extinderea aceluiași mecanism de copiere a valorilor implicite la case este posibilă, dar demonstrația se limitează la setările de locuitor. La [00:45:35](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2735s), transmite `this`, CityBuilder-ul curent, în domeniu, ca să știe contextul de construcție proprietar.

[00:45:45](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2745s) O **interfață** specifică operații pe care obiecte concrete diferite le pot furniza. O interfață comună de creare ar permite configurației de nivel mai înalt să lucreze fie cu builderul de oraș, fie cu domeniul, fără să știe ce tip concret a primit ([00:45:52](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2752s)). Operațiile comune sunt crearea de locuitori și de case. Domeniul suportă suplimentar configurarea valorilor implicite pentru locuitori ([00:46:06](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2766s), [00:46:14](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2774s)); CityBuilder-ul concret care funcționează nu expune el însuși fiecare setter de locuitor.

[00:46:24](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2784s) Identifică acele părți comune, în loc să asimilezi toate tipurile de builder. Apelanții pot folosi interfața relevantă și pot lăsa stocajul concret în sarcina fiecărei implementări ([00:46:32](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2792s)). Commitul funcțional de mai sus implementează încă aceste trasee prin clase concrete; refactorizarea ulterioară cu interfețe face contractele explicite.## Verifică casele comune și suprascrierile de vârstă

[00:46:36](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2796s) Inspectează cei doi locuitori: referința la casă s-a propagat la amândoi. Pasul de copiere aplică valorile implicite la fiecare creare prin domeniu ([00:46:48](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2808s)). Câmpurile nespecificate își păstrează valorile implicite din jur ([00:46:55](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2815s)), iar configurarea individuală le poate suprascrie ([00:47:02](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2822s)).

[Stare de sursă asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) dă câmpul exact afișat în demonstrația valorilor implicite: **Age**, nu un ID de casă. Configurare și rezultat, scurtate:

```csharp
scope.Home(home);
scope.Age(32);
var john = scope.Citizen();
john.Name("John");
john.Age(18);
var joe = scope.Citizen();
joe.Name("Joe");
// Built John.Age: 18; Joe.Age: 32
```

La [00:47:11](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2831s), John afișează 18 pentru că vârsta lui a fost suprascrisă, iar Joe afișează vârsta implicită a domeniului, 32. Amândoi pot încă împărți aceeași casă. La [00:47:21](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2841s), Joe este al doilea locuitor creat, deci se află la indexul de locuitor 1 numerotat de la zero; aceasta nu este prima poziție de casă.

[00:47:44](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2864s) Creează încă un locuitor fără suprascriere individuală de vârstă. El moștenește tot 32 ([00:47:51](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2871s)). Sursa funcțională îl numește pe al treilea locuitor Chell și afișează vârstele 18, 32, 32. Acestea sunt valori copiate la apelul Citizen, nu legături active care s-ar actualiza automat când se schimbă valorile implicite.

<details>
<summary>Dacă vârsta din domeniu se schimbă după ce John a fost creat, se schimbă și John automat?</summary>

Nu. Crearea copiază valoarea implicită curentă în modelul mutabil separat al lui John. Editările ulterioare ale acelui model de valori implicite afectează creările viitoare, nu câmpurile numerice deja copiate. O referință la casă este copiată ca referință, deci mai mulți locuitori în pregătire pot împărți în continuare același MutableHome.

</details>

## Înlocuiește referințele la casă cu un identificator puternic tipizat

[00:48:06](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2886s) Un locuitor ține acum o referință directă la o casă. Înlocuiește-o cu un **identificator puternic tipizat**, un mic înveliș care poartă cheia casei ([00:48:12](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2892s)). Un **struct** este un tip valoare; mecanismul său de stocare la nivel jos nu este punctul aici ([00:48:19](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2899s)). Învelișul elimină dependența locuitorului față de un anumit obiect Home ([00:48:25](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2905s)).

[00:48:35](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2915s) Un **record struct** oferă egalitate și hashing orientate pe valoare, evitând codul de comparare scris manual ([00:48:42](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2922s)). Analogia cu un record din Java privește acea facilitate; un record din Java nu este un tip valoare C#.

[Stare de ID nefinalizată asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/dcfd6225dc8e41c7cbdc4e86a79a7bf0b5c1f869/examples/builder/Program.cs) conține ulteriorul înveliș cu valoare int și căutarea bazată pe ID, dar refactorizarea în ansamblu este incompletă. Formă comisă simplificată:

```csharp
readonly record struct HomeId
{
    public readonly int Value;
    public HomeId(int value) { Value = value; }
}
// Direct-initialization site after the change:
var john = new Citizen { Name = "John", Age = 18, Home = new(0) };
```

[00:48:48](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2928s) Introdu HomeId cu un singur câmp numeric. Înregistrarea discută întâi un constructor care primește un long ([00:48:59](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2939s), [00:49:09](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2949s)); commitul ulterior inspectat folosește int și numește câmpul Value. Constructorul atribuie câmpul o singură dată ([00:49:16](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2956s)). Getterele și setterele suplimentare aduc puțin beneficiu pentru acest înveliș transparent ([00:49:21](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2961s)). Pentru a face un câmp din Java nereatribuibil, `final` este restricția relevantă; `private` controlează accesul, nu împiedică reatribuirea ([00:49:29](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2969s)). În această versiune C#, `readonly` oferă restricția de atribuire ([00:49:39](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2979s)); nu orice record struct este automat readonly.

Urmează refactorizarea:

1. [00:49:46](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2986s) Schimbă reprezentarea casei locuitorului dintr-o referință la obiect într-un HomeId, făcut ulterior opțional.
2. [00:49:51](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=2991s) Observă eșecul de compilare acolo unde vechiul cod de creare încă atribuie un obiect Home.
3. [00:50:03](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3003s) Folosește acele erori pentru a găsi locurile de construcție afectate.
4. [00:50:08](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3008s) Furnizează poziția casei în vectorul de case al orașului, împachetată ca ID ([00:50:20](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3020s)).

Erorile de compilare sunt consecințe așteptate ale schimbării reprezentării. Commitul istoric are încă apeluri de builder învechite și atribuiri incompatibile, deci este dovadă a tranziției de design, nu a unui builder de ID complet și executabil.

## Rezolvă identificatorii prin oraș

[00:50:40](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3040s) Un ID singur nu expune adresa unei case. **Rezolvarea** înseamnă folosirea acelei chei în orașul proprietar pentru a recupera Home ([00:50:48](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3048s)). Apelantul obține temporar o referință la Home după rezolvare; locuitorul nu trebuie să o stocheze ([00:51:14](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3074s)).

[Stare de ID nefinalizată asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/dcfd6225dc8e41c7cbdc4e86a79a7bf0b5c1f869/examples/builder/Program.cs) arată căutarea pe **City**, nu pe Citizen. Operații comis, scurtate:

```csharp
public Home Get(HomeId id) => Homes[id.Value];
public Citizen Get(CitizenId id) => Citizens[id.Value];

// At the call site:
Home? home = city.Get(city.Citizens[0].Home);
```

[00:51:02](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3062s) Returnarea lui null pentru un ID inexistent este discutată ca politică de căutare. Căutarea neopțională în vector de mai sus nu face nicio verificare de limite: un index invalid aruncă. Supraîncărcarea sa opțională întoarce null pentru marcajul absenței, nu pentru orice index în afara intervalului. Păstrează separate „nefurnizat” și „furnizat dar inexistent”.

[00:51:21](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3081s) Anterior, referința la casă era disponibilă imediat din locuitor. Refactorizarea mută acea responsabilitate de căutare în City ([00:51:34](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3094s)), încercând totuși să păstreze confortul folosirii ([00:51:39](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3099s)). La [00:51:52](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3112s), adaugă Get(HomeId). Această metodă **încapsulează** stocajul: apelanții furnizează o cheie tipizată fără a depinde de modul în care este stocată ([00:52:11](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3131s)).

[00:52:22](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3142s) Un dicționar cu chei de tip HomeId ar putea înlocui vectorul. Get ar ascunde acea alegere ([00:52:34](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3154s)), deci apelanții nu ar trebui să se schimbe dacă implementarea se schimbă, atât timp cât același contract de căutare este păstrat. La [00:52:56](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3176s), folosește getterul la locul de apel, transmițând ID-ul casei memorat de locuitor către oraș. Descrierea repetată „citizen.Get” nu trebuie citită ca mutarea efectivă a metodei pe clasa Citizen; sursa inspectată apelează `c.Get(c.Citizens[0].Home)`.

## Folosește supraîncărcări pentru a păstra tipul entității

[00:53:10](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3190s) Un int gol nu spune dacă valoarea sa identifică o casă sau un locuitor. Două metode Get(int) nu pot fi deosebite doar după tipul de return, astfel că un design ne-tipizat are nevoie adesea de nume precum GetHome și GetCitizen ([00:53:24](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3204s)).

[Stare de ID nefinalizată asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/dcfd6225dc8e41c7cbdc4e86a79a7bf0b5c1f869/examples/builder/Program.cs) introduce învelișuri distincte HomeId și CitizenId și supraîncărcări. API scurtat:

```csharp
readonly record struct CitizenId(int Value);
readonly record struct HomeId(int Value);

public Citizen Get(CitizenId id) => Citizens[id.Value];
public Home Get(HomeId id) => Homes[id.Value];

// Get(new CitizenId(0)) selects Citizen.
// Get(new HomeId(0)) selects Home.
```

[00:53:39](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3219s) Un alt înveliș de identificator face posibil același nume de metodă pentru altă entitate. Pentru căutarea unui locuitor, tipul inspectat este **CitizenId**, nu CityId. Un CityId ar identifica un oraș și ar fi un concept separat.

[00:54:03](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3243s) O **supraîncărcare** partajează un nume de metodă, dar are alt tip de parametru. La [00:54:10](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3250s), acel tip de parametru selectează operația pe entitate. Cu CitizenId, orașul rezolvă un locuitor prin Get ([00:54:29](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3269s)); fără tipuri de înveliș distincte, apelantul trebuie să aleagă o operație cu nume separat ([00:54:45](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3285s)).

[00:54:59](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3299s) Cu numere goale, apelarea accidentală a lui GetHome cu un index de locuitor poate compila. Poate aduce în tăcere o casă în locul locuitorului intenționat ([00:55:17](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3317s)). Tipurile de ID distincte împiedică această confuzie: HomeId nu este CitizenId, iar supraîncărcarea corectă de Get urmează tipul argumentului ([00:55:34](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3334s)). Cele două câștige sunt mai puține repetări ale numelui entității la locurile de apel și protecție la timp de compilare împotriva amestecării categoriilor de chei ([00:55:37](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3337s)).

Această **siguranță de tip** nu dovedește că cheia există, că este în interval sau că aparține acestui oraș anume. Acestea sunt verificări separate, de runtime sau de proprietate.

<details>
<summary>Știe Get(new HomeId(0)) că poziția zero este o casă validă?</summary>

Tipul său spune compilatorului să aleagă operația pentru case. Validitatea și proprietatea cheii numerice au în continuare nevoie de contractul de căutare al orașului și de orice verificări de runtime.

</details>

## Înțelege stocajul plat și controlul duratei de viață

[00:55:55](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3355s) ID-urile adaugă muncă de implementare, dar referințele directe impun și ele muncă de construcție și conversie ([00:56:03](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3363s)). Builderul bazat pe referințe avea nevoie de un dicționar mutabil–imutabil. Un ID poate rămâne aceeași cheie peste reprezentări, ceea ce face acel dicționar de relații inutil.

[Stare de ID nefinalizată asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/dcfd6225dc8e41c7cbdc4e86a79a7bf0b5c1f869/examples/builder/Program.cs) captează această intenție, inclusiv eliminarea dicționarului de case din build. Atribuire finală simplificată:

```csharp
// Once the whole ID migration is completed:
var citizen = new Citizen
{
    Name = name,
    Age = mut.Age,
    Home = mut.Home // same optional ID, not a converted Home reference
};
```

[00:56:13](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3373s) Un ID este o **cheie**, nu neapărat un index de vector pentru totdeauna. În stocajul susținut de dicționar, orașul poate întreba dacă entitatea cu acea cheie mai există. Aceasta separă **durata de viață** logică, perioada în care entitatea aparține sistemului, de durata de viață a unui anumit obiect în memorie.

[00:56:22](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3382s) O referință puternică la obiect își ține ținta accesibilă. O **referință slabă** evită să țină ținta vie prin ea însăși, dar dispariția țintei urmează apoi **colectarea gunoiului**, recuperarea automată a memoriei, nu regulile programului de eliminare a entităților.

Avantajele descrise sunt:

- [00:56:51](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3411s) **Deserializare** mai simplă, refacerea datelor dintr-o reprezentare stocată: citești chei, în loc să reconstruiești și să reconectezi un graf de referințe între obiecte.
- [00:57:00](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3420s) Control explicit al duratei de viață: o cheie poate fi testată față de stocajul curent chiar și după ce o entitate a fost eliminată.
- [00:57:14](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3434s) Stocaj plat al relațiilor: locuitorii și casele se referă la entități prin ID-uri, nu prin referințe directe între obiecte din entități diferite.
- [00:57:27](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3447s) Construire mai simplă: dicționarele de relații pot dispărea când ID-urile se păstrează peste conversie.

„Plat” se referă la legăturile dintre entități; orașul deține în continuare colecții de obiecte. Lecția nu implementează persistență completă, ștergere sau un sistem de producție pentru durata de viață.

[00:57:35](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3455s) Lucrul cu un ID permite codului să afirme identitatea sau existența fără a inspecta fiecare proprietate a casei. Un identificator îngustează deci operațiile disponibile până când apelantul îl rezolvă ([00:58:00](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3480s)). Asta poate reduce cuplarea dintre entități.

## Reprezintă explicit o casă absentă

[00:58:06](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3486s) Un locuitor poate să nu aibă casă, deci reprezentarea prin ID trebuie să exprime absența. Un **sentinel**, o valoare rezervată precum -1, poate codifica „niciuna” ([00:58:12](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3492s)). Un **ID opțional** împachetează acea posibilitate într-un tip distinct, în loc s-o lase drept convenție numerică ascunsă ([00:58:25](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3505s)).

[Stare de ID nefinalizată asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/dcfd6225dc8e41c7cbdc4e86a79a7bf0b5c1f869/examples/builder/Program.cs) conține OptionalHomeId și supraîncărcarea opțională de Get. Formă din sursă, scurtată:

```csharp
readonly record struct OptionalHomeId
{
    public static readonly HomeId NoHomeId = new(-1);
    public readonly HomeId Id;
    public bool IsNull => Id == NoHomeId;
    public OptionalHomeId(HomeId id) { Id = id; }
    public OptionalHomeId() : this(NoHomeId) { }
}
public Home? Get(OptionalHomeId id)
{
    if (id.IsNull) return null;
    return Get(id.Id);
}
```

Implementează în pași:

1. [00:58:39](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3519s) Adaugă un constructor fără parametri care instalează valoarea de absență.
2. [00:58:47](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3527s) Adoptă -1 ca convenție pentru lipsa casei.
3. [00:58:56](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3536s) Numește acea valoare cu un câmp static readonly, în loc de a repeta literalul. Nu este un `const` la timp de compilare al acestui tip record struct.
4. [00:59:07](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3547s) Folosește sintaxa de constructor pentru a inițializa marcajul sau valoarea opțională. În codul inspectat, marcajul static apelează constructorul numeric HomeId; constructorul fără parametri al lui OptionalHomeId delegează către el.
5. [00:59:15](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3555s) Adaugă un predicat de absență, numit IsNull în sursă.
6. [00:59:26](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3566s) Schimbă membrul locuitorului în OptionalHomeId.
7. [00:59:32](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3572s) Adaugă Get(OptionalHomeId). La [00:59:40](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3580s), întoarce null pentru absență și altfel delegează către Get(HomeId).

[00:59:46](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3586s) Absența este acum vizibilă în tip și în rezultatul `Home?`. Adnotările de referință nullable pot exprima dacă o referință este menită să fie nullable, dar nu adaugă aplicare la runtime care să garanteze că o referință arbitrară nu poate fi niciodată null ([01:00:00](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3600s)). OptionalHomeId este un struct personalizat, nu `Nullable<HomeId>`.

Există și un caz limită de tip valoare: `new OptionalHomeId()` invocă constructorul fără parametri declarat, pe când `default(OptionalHomeId)` și elementele de vector inițializate cu zero nu îl invocă. Valoarea implicită are un ID de zero, nu -1, în acest design. Așadar, această reprezentare cu sentinel nu face automat ca fiecare valoare opțională implicită să însemne „niciuna”.

[01:00:10](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3610s) Costul constă în tipuri de înveliș și supraîncărcări suplimentare. Accesul bazat pe ID adaugă și o trecere prin contextul orașului partajat, în locul accesului imediat printr-o referință Home memorată ([01:00:20](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3620s), [01:00:40](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3640s)). Căutarea opțională poate să nu producă niciun Home, deci apelanții trebuie să trateze un rezultat nullable ([01:00:56](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3656s)).

## Poartă identificatorii prin builderi

[01:01:05](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3665s) Aplică aceeași idee de ID și builderilor. În loc să stocheze direct o referință la MutableCitizen, un builder de locuitor își poate stoca CitizenId și CityBuilder-ul proprietar. Un **getter** rezolvă modelul mutabil curent din acel context comun ([01:01:17](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3677s)).

[Stare de ID nefinalizată asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/dcfd6225dc8e41c7cbdc4e86a79a7bf0b5c1f869/examples/builder/Program.cs) are următoarea formă de builder, alături de apelanți învechiți care încă necesită migrare:

```csharp
public readonly CitizenId Id;
public readonly CityBuilder Builder;
public MutableCitizen Model => Builder.Model.Citizens[Id.Value];

public CitizenBuilder(CityBuilder builder, CitizenId citizenId)
{
    Builder = builder;
    Id = citizenId;
}
```

În această stare intermediară, schimbarea constructorului fără schimbarea fiecărui apel de fabrică produce erori de compilare. Ea demonstrează forma intenționată a handle-ului, nu o implementare completă de la un capăt la altul.

[01:01:27](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3687s) Repetarea perechii „ID plus builder proprietar” în fiecare builder de entitate motivează un **înveliș** comun sau o structură de handle. Acesta ar înlocui două câmpuri și două argumente cu o singură valoare reutilizabilă ([01:01:55](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3715s)). Învelișul încapsulează logica de rezolvare repetată; lecția schițează această îmbunătățire în loc să ofere un cadru generic de handle complet.

[01:02:07](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3727s) Simplificarea importantă a lui Build este că ID-urile relațiilor există deja. Nu trebuie să construiască dicționare separate de asociere a caselor ([01:02:30](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3750s)). Pozițiile entităților pot purta direct acele ID-uri. La [01:02:41](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3761s), configurarea existentă a câmpurilor poate folosi în continuare un getter Model, deși builderul nu mai stochează o a doua referință către model. Getterul trebuie să rezolve prin contextul proprietar corect; „sintaxa de configurare neschimbată” nu înseamnă că fiecare fabrică sau atribuire învechită compilează deja.

## Partajează configurația prin metode de extensie

[01:02:55](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3775s) Builderul de locuitor și domeniul repetă configurarea Name, Age și Home. Elimină acea logică de atribuire duplicată ([01:03:07](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3787s)). O **metodă de extensie** este o metodă statică definită în afara clasei receptorului, dar apelabilă cu sintaxă obișnuită de instanță ([01:03:18](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3798s)).

[Stare de configurare comună](https://github.com/AntonC9018/uniCourse_csharp/blob/abda83011ba294a41288fa5aa2469ba0b51c1f8d/examples/builder/Program.cs) conține interfața și extensiile. Implementare scurtată:

```csharp
interface ICitizenBuilder
{
    MutableCitizen Model { get; }
}
sealed class CitizenBuilder : ICitizenBuilder
{
    public MutableCitizen Model { get; }
    public CitizenBuilder(MutableCitizen model) { Model = model; }
}
static class CitizenBuilderExtensions
{
    public static void Name(this ICitizenBuilder builder, string name)
    {
        builder.Model.Name = name;
    }
}
// john.Name("John");
```

[01:03:27](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3807s) Introdu ICitizenBuilder cu doar un getter Model. La [01:03:50](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3830s), mută metodele comune în CitizenBuilderExtensions. La [01:04:06](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3846s), aceste metode statice devin unica implementare a operațiilor de configurare. La [01:04:33](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3873s), pune `this` înaintea tipului primului parametru, ca compilatorul să recunoască receptorul extensiei.

Afirmația largă din înregistrare că C# nu are implementări implicite de interfață trebuie nuanțată: C# modern le are. Această refactorizare alege metodele de extensie; exemplul nu folosește corpuri implicite de interfață.

[01:04:55](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3895s) Getterul furnizează modelul mutabil care este configurat. O **proprietate auto get-only** într-o clasă are un stocaj generat de compilator, un **câmp de susținere**, și un getter; poate fi atribuită în timpul construcției. O proprietate implementată manual poate în schimb întoarce un câmp existent ([01:05:19](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3919s)). O declarație de interfață `Model { get; }` specifică contractul de getter; nu generează singură un câmp de stocaj în clasa care implementează.

[01:05:46](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3946s) Domeniul expune `Model => CitizenBuilder.Model`, trimitând către builderul său de valori implicite. Extensiile comune pot apoi configura fiecare receptor fără a duplica atribuirile de câmp. La [01:05:55](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3955s), receptorul își face modelul disponibil prin acel getter. Returnarea unui builder este o schimbare separată de API fluent: extensiile din acest commit returnează void.

[01:06:02](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3962s) Asigură-te că clasa declară că implementează **ICitizenBuilder**. Menționarea aparentă a IEnumerable din transcriere nu corespunde implementării inspectate; enumerarea nu este cea care permite aceste metode de configurare. Apelurile existente își păstrează forma odată ce interfața corectă este implementată. Accesul prin getterul de interfață poate implica difuziune pe interfață, în timp ce metodele de extensie rămân statice ([01:06:18](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3978s)). La [01:06:35](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=3995s), logica de setare a câmpurilor a fost mutată într-un singur loc comun, evitând repetarea în domeniu și în builderul de locuitor.

## Întoarce același builder pentru apeluri fluente

[01:06:55](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4015s) Un nume de builder nu face singur un API fluent. Implementarea trebuie să întoarcă ceva pe care se poate apela următoarea metodă de configurare. Pentru versiunea fluentă mutabilă, întoarce același obiect ([01:07:07](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4027s)).

[Stare builder fluent](https://github.com/AntonC9018/uniCourse_csharp/blob/1b854ac5ffdf93de5bd0410f39849f7889a1d280/examples/builder/Program.cs) conține această implementare concretă, care întoarce obiectul:

```csharp
public CitizenBuilder Age(int age)
{
    if (age < 0) throw new InvalidOperationException("Invalid age");
    Model.Age = age;
    return this;
}
public CitizenBuilder Name(string name)
{
    Model.Name = name;
    return this;
}
// city.Citizen().Age(18).Name("John");
```

[01:07:31](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4051s) Un locuitor nou creat poate fi acum configurat într-o singură instrucțiune, fără o locală intermediară. Acest commit explorează metode fluente concrete, în loc să facă doar fiecare extensie din commitul precedent fluentă; acestea sunt variante succesive ale API-ului.

[01:07:42](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4062s) Comprimarea are un cost la depanare. Mai multe operații pe un singur rând sursă fac mai greu oprirea între apeluri cu puncte de oprire obișnuite pe rând. Când un apel aruncă, un număr de rând poate identifica expresia combinată, nu pasul de configurare care a eșuat ([01:08:06](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4086s)). Depanatoarele pot totuși păși în metode sau pot folosi puncte de oprire pe metodă; îngrijorarea lecției este pierderea unor rânduri sursă separate și comode.

[01:08:22](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4102s) Beneficiul aici este în principal sintaxă concisă și mai puține variabile temporare. Pentru o pregătire complicată, instrucțiunile separate pot fi mai ușor de inspectat.

[01:08:44](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4124s) Builderii pot înveli și mai multe obiecte și pot expune o vedere utilă a stării curente. **Învelișuri** imbricate, obiecte în jurul altor obiecte, pot prezenta valoarea în curs la mai multe niveluri ([01:08:57](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4137s)). Această utilizare mai largă este o observație de design, nu o afirmație că configurația incompletă a acestui oraș este deja validă sau că Build poate fi sărit.

## Ascunde modele diferite în spatele contractelor de builder

[01:09:21](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4161s) Un **builder abstract** își poate ascunde propriul model sau poate produce o reprezentare finală diferită ([01:09:35](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4175s)). „Abstract” descrie aici rolul API-ului; nu trebuie să fie literal o clasă abstractă C#. Dacă implementările păstrează modele diferite, o interfață comună ar trebui să expună operații de configurare, nu să forțeze fiecare builder să expună MutableCitizen ([01:09:40](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4180s)).

[Stare de builder abstract nefinalizată asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/ff2cc72527a8a97cf7b756d006ce5d6d8ac9724f/examples/builder/Program.cs) conține interfețele operaționale, dar multe corpuri și membri de interfață sunt nefinalizați. O variație de interfață fluentă, discutată în timpul tranziției, simplificată:

```csharp
interface ICitizenBuilder
{
    ICitizenBuilder Name(string name);
    ICitizenBuilder Age(int age);
    ICitizenBuilder Home(IHomeBuilder home);
}
sealed class CitizenBuilder : ICitizenBuilder
{
    public CitizenBuilder Name(string name)
    {
        Model.Name = name;
        return this;
    }
    ICitizenBuilder ICitizenBuilder.Name(string name) => Name(name);
    // Age and Home implementations omitted in this teaching sketch.
}
```

[01:09:53](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4193s) Un asemenea contract poate întoarce interfața, astfel încât clienții generici să poată continua configurarea fără a depinde de un model concret. Commitul de builder abstract inspectat folosește în schimb metode de configurare care returnează void; nu confunda această variație fluentă scurtată cu codul său complet exact.

[01:10:19](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4219s) Declară că implementarea concretă implementează interfața și furnizează membrii necesari. **Implementarea explicită de interfață** definește un membru în mod specific pentru apelurile prin interfață. Poate trimite către o metodă publică al cărei tip de return este builderul concret ([01:10:31](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4231s)).

[01:10:56](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4256s) Apelurile concrete directe pot păstra tipul de builder mai specific, în timp ce apelurile cu tip de interfață expun doar contractul comun. Acestea sunt membri separați, nu supraîncărcări obișnuite deosebite doar după tipul de return. Identitatea concretă a builderului contează când codul client folosește operații concrete suplimentare ([01:11:05](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4265s)); interfața generală nu le expune intenționat.

## Adaptează builderii concreți la interfețe comune

[01:11:22](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4282s) O interfață comună creează o graniță incomodă pentru Home. Metoda concretă își așteaptă propriul HomeBuilder, dar interfața acceptă IHomeBuilder. Un adaptor trebuie să verifice builderul furnizat înainte de a-l transmite mai departe.

[Stare de builder abstract nefinalizată asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/ff2cc72527a8a97cf7b756d006ce5d6d8ac9724f/examples/builder/Program.cs) folosește o aserțiune și o conversie în implementările explicite de interfață. Adaptor comis, scurtat:

```csharp
void ICitizenBuilder.Home(IHomeBuilder home)
{
    Debug.Assert(home is HomeBuilder);
    Home((HomeBuilder)home);
}
public void Home(HomeBuilder home)
{
    Model.Home = home.Home;
}
```

[01:11:49](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4309s) O interfață permite o altă implementare a lui IHomeBuilder, în timp ce această metodă presupune una anumită implementare concretă. Utilizarea intenționată presupune și că casa vine din același context de construcție. O simplă verificare a tipului concret nu stabilește proprietatea.

[01:12:14](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4334s) Pune tratamentul parametrului mai puțin specific în membrul explicit de interfață. Apelurile directe folosesc metoda tipizată Home(HomeBuilder); apelurile prin interfață folosesc adaptorul. La [01:12:45](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4365s), utilizarea concretă își păstrează comportamentul anterior. La [01:12:54](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4374s), codul client generic poate lucra prin interfața comună. Aserțiunea prezentată este o diagnostică de dezvoltare, nu o validare de producție fiabilă a tipului sau a proprietății; o conversie eșuată poate totuși arunca.

[01:13:03](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4383s) Adaugă operația de scope lipsă în ICityBuilder. Factorizează operațiile de creere repetate într-o interfață comună. În sursa inspectată, membrii sunt **Citizen() and Home()**, nu WithCity și WithHome ([01:13:25](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4405s), [01:13:33](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4413s)).

Forma contractului rezultată este:

```csharp
interface ICityScopeCommon
{
    ICitizenBuilder Citizen();
    IHomeBuilder Home();
}
interface ICityBuilder : ICityScopeCommon
{
    ICityScopeBuilder Scope();
}
interface ICityScopeBuilder : ICitizenBuilder, ICityScopeCommon { }
```

[01:13:42](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4422s) Membrii comuni de creare rămân; adaptorul de configurare Home gestionează dependența de model concret. La [01:13:57](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4437s), folosirea interfețelor potrivite pentru dependențe poate reduce unele conversii concrete. **Interfețele C# moștenesc alte interfețe, nu clase concrete de builder.** Nici înlocuirea unui parametru concret cu o interfață nu elimină cerințele de proprietate. Această parte a înregistrării este exploratorie, iar starea comisă de builder abstract rămâne incompletă.

## Plătește pentru abstractizare doar când este necesară

[01:14:16](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4456s) Construcția bazată pe interfețe poate lăsa o implementare să păstreze un singur model, câmpuri separate, datele în altă parte sau o reprezentare complet diferită. Aceasta este utilă când apelanții trebuie să suporte builderi genuin diferiți.

[Stare de abstractizare nefinalizată asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/ff2cc72527a8a97cf7b756d006ce5d6d8ac9724f/examples/builder/Program.cs) ilustrează separarea contractelor operaționale de expunerea modelului. Ideea sa minimală este:

```csharp
interface IHomeBuilder
{
    void Address(string address);
}
// Each implementation decides where Address stores the value.
```

[01:14:46](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4486s) Nu introduce interfața doar pentru că acea flexibilitate ar putea fi utilă cândva. Ea adaugă cod, adaptori și contracte care trebuie întreținute ([01:14:55](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4495s)). Pregătirea prematură a fiecărei componente pentru un model de înlocuire imaginar poate costa mai mult decât economisește ([01:15:02](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4502s)).

[01:15:19](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4519s) Când apare o cerință nouă reală, poate fi mai ușor să faci clasa existentă mai generală decât să menții de la început o mare ierarhie abstractă. Acest sfat nu interzice interfețele; cere o nevoie concretă care să le justifice. Deturul lecției prin builderul abstract expune atât flexibilitatea, cât și munca suplimentară nefinalizată.

## Lasă builderii să producă implementări diferite de oraș

[01:15:42](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4542s) Dacă builderi diferiți produc implementări diferite de oraș, tipul de return al lui Build trebuie să exprime contractul comun de rezultat. O clasă de bază abstractă City este o opțiune ([01:15:54](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4554s)); o **interfață ICity** care expune căutarea entităților este alta ([01:16:00](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4560s)).

[Stare nefinalizată cu interfață de rezultat asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/ff2cc72527a8a97cf7b756d006ce5d6d8ac9724f/examples/builder/Program.cs) conține ICity, o implementare City, o schiță DynamicCity și interfețe de builder separate. Are corpuri lipsă și membri incompatibili, deci nu este o implementare completă demonstrată. Contractul intenționat, scurtat:

```csharp
interface ICity
{
    Citizen Citizen(CitizenId id);
    Home Home(HomeId id);
}
// Possible array-backed member:
public Home Home(HomeId id) => Homes[id.Value];
// Possible dictionary-backed member:
// public Home Home(HomeId id) => Homes[id.Value];
```

[01:16:05](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4565s) Orașul implicit poate folosi vectori; un oraș dinamic poate folosi dicționare sau o **mulțime rară**, o alternativă de stocare pe chei menționată doar aici. Fiecare implementare trebuie să satisfacă aceleași operații, recuperând datele din colecțiile proprii ([01:16:16](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4576s)). Identitatea pe aceeași cheie și contractul de căutare contează mai mult pentru clienți decât colecția de bază.

[01:16:24](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4584s) Diferite implementări de construire finală pot produce acele orașe diferite. O **suprascriere** înlocuiește un membru de bază virtual sau abstract într-o subclasă; implementări separate ale unei interfețe pot oferi și ele comportări diferite de Build, fără moștenire. Clasele concrete inspectate sunt sealed și nu conțin nicio ierarhie de suprascrieri finalizată. La [01:17:26](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4646s), traseul implicit este intenționat să producă City-ul susținut de vectori.

[01:17:34](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4654s) Acel City trebuie să implementeze efectiv operațiile comune de căutare. O implementare înrudă ar putea construi din propriul model și ar putea furniza alte operații bazate pe alt stocaj ([01:17:39](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4659s)). Înregistrarea schițează această dispunere; corpurile istorice care aruncă NotImplementedException sau nu au return nu demonstrează că a fost finalizat sau testat.

## Separă configurarea de crearea finală

[01:17:40](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4660s) La final, separă configurația și crearea finală în două obiecte. Builderul de configurare expune pregătirea; un **builder final** expune doar Build terminal. Fabricile pot crea și conecta perechea cu tipurile concrete potrivite.

[Stare nefinalizată de builder împărțit asociată](https://github.com/AntonC9018/uniCourse_csharp/blob/ff2cc72527a8a97cf7b756d006ce5d6d8ac9724f/examples/builder/Program.cs) conține ICityFinalBuilder și un deținător pentru referințele de configurare și de builder final. O ilustrare scurtată a relației de stare comună necesare:

```csharp
interface ICityFinalBuilder
{
    ICity Build();
}
sealed class CityFinalBuilder : ICityFinalBuilder
{
    private readonly CityBuilder _configured;
    public CityFinalBuilder(CityBuilder configured) { _configured = configured; }
    public ICity Build() => _configured.Build();
}
// Both roles must refer to the same configured construction state.
```

Aceasta este o completare ilustrativă a rolului propus, nu o clasă completă prezentă în commitul citat. Ea presupune un CityBuilder al cărui rezultat implementează ICity.

[01:17:45](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4665s) CityFinalBuilder trebuie să se refere la **același** CityBuilder folosit pentru configurare. La [01:17:53](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4673s), apelantul invocă crearea finală prin acel rol de builder final. Rezultatul său trebuie să provină din starea configurată, nu dintr-un builder nou și gol. Transcrierea continuă această explicație până la [01:18:50](https://www.youtube.com/watch?v=lIKqnGtzQrA&t=4730s); intervalele mai ample furnizate grupează această discuție finală.

Lecția se încheie cu o graniță de design: pregătirea mutabilă permite configurarea treptată, Build validează și convertește, iar consumatorii se bazează pe stabilitatea modelului final. Exemplul funcțional rămâne imutabil prin convenție, nu impus profund. Designul complet cu ImmutableArray, diagnosticile avansate, detaliile metodei fabrică, valorile implicite generalizate pentru case, persistența, stocajul dinamic și implementările finalizate ale builderilor alternativi rămân în afara demonstrației finalizate.

<details>
<summary>De ce ar fi greșit să creezi un CityBuilder nou în interiorul lui CityFinalBuilder.Build?</summary>

Arunca configurația acumulată prin builderul original. Separarea rolurilor API nu trebuie să separe starea configurată de bază.

</details>

## Istoricul modificărilor de cod

Aceste stări de sursă inspectate sunt listate de la cea mai veche la cea mai nouă. Intrările nefinalizate sunt stări de dezvoltare, nu build-uri reușite.

- [c79a352](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/Program.cs) — Builder 1: conversia funcțională bazată pe referințe și valorile implicite de domeniu.
- [dcfd622](https://github.com/AntonC9018/uniCourse_csharp/blob/dcfd6225dc8e41c7cbdc4e86a79a7bf0b5c1f869/examples/builder/Program.cs) — Cod de ID nefuncțional: ID-uri tipizate și căutare opțională, cu migrarea nefinalizată.
- [abda830](https://github.com/AntonC9018/uniCourse_csharp/blob/abda83011ba294a41288fa5aa2469ba0b51c1f8d/examples/builder/Program.cs) — Interfață pentru metodele de configurare: un contract Model și extensiuni comune.
- [1b854ac](https://github.com/AntonC9018/uniCourse_csharp/blob/1b854ac5ffdf93de5bd0410f39849f7889a1d280/examples/builder/Program.cs) — Builder fluent: metodele concrete de configurare returnează receptorul.
- [ff2cc72](https://github.com/AntonC9018/uniCourse_csharp/blob/ff2cc72527a8a97cf7b756d006ce5d6d8ac9724f/examples/builder/Program.cs) — Builder abstract: contracte alternative și roluri de creare finală, nefinalizat.
- [c6d7b18](https://github.com/AntonC9018/uniCourse_csharp/blob/c6d7b18cfbd1b0b8607b9434feb38dce0aae681f/examples/builder/Program.cs) — Revenire la builderul funcțional: starea finală a depozitului restaurează exemplul bazat pe referințe.

Mostre compacte ale schimbărilor de mai sus, în aceeași ordine:

```csharp
// c79a352: mutable home -> existing final home
var home = mut.Home != null ? mutToImmutHome[mut.Home] : null;
// dcfd622: optional typed ID -> city lookup
Home? resolved = c.Get(c.Citizens[0].Home);
// abda830: shared configuration through the model contract
public static void Name(this ICitizenBuilder b, string name) { b.Model.Name = name; }
// 1b854a: fluent configuration returns the receiver
public CitizenBuilder Name(string name) { Model.Name = name; return this; }
// ff2cc72: split final-creation contract
interface ICityFinalBuilder { ICity Build(); }
// c6d7b18: restore working direct-reference build and age demonstration
var c = city.Build(); // configured ages: 18, 32, 32
```