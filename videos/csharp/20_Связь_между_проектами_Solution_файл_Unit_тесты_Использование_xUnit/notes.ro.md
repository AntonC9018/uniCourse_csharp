> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Scop și vocabular

[00:00:00](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=0s) Construim un exemplu care se poate rula, cu un proiect care conține o funcție și un alt proiect care o exercitează. Ideea este să vedem ce ne aduc testele: putem înlocui o implementare, îi păstrăm comportamentul cunoscut și primim o eroare utilă când o modificăm din greșeală. Exemplul convertește vârsta unei pisici într-un echivalent de vârstă umană inventat; este o regulă de programare, nu un model biologic.

Lecția introduce aceste noțiuni în ordinea în care devin utile:

- **Proiect, șablon console și SDK .NET:** un proiect grupează fișierele sursă și setările de compilare; un șablon console oferă un punct de pornire executabil minimal; SDK-ul furnizează unelte precum comanda `dotnet`.
- **CLI:** interfață de linie de comandă, în care comenzile sunt introduse într-un terminal.
- **MSBuild, compilator și ansamblu:** MSBuild coordonează compilarea; compilatorul C# traduce C#; un ansamblu este unitatea compilată rezultată dintr-un proiect.
- **Fișier de proiect și XML:** un `.csproj` este un document de configurare XML; XML exprimă structura prin etichete de deschidere și de închidere cu nume.
- **Proprietăți de compilare, targeturi și importuri partajate:** proprietățile rețin setări, targeturile descriu acțiuni de compilare, iar fișierele importate furnizează configurare. `Directory.Build.props` și `Directory.Build.targets` sunt nume convenționale pentru fișiere partajate.
- **Target framework, importuri implicite, nullabilitate și tip de ieșire:** aceste setări aleg API-ul .NET vizat, spațiile de nume importate automat, analiza legată de null și dacă un proiect produce un executabil sau o bibliotecă.
- **Bibliotecă, referință de proiect și cale relativă:** o bibliotecă furnizează cod apelanților; o referință de proiect leagă un proiect de altul; o cale relativă localizează un fișier pornind de la directorul fișierului care o referă.
- **Soluție, IDE și IntelliSense:** o soluție listează proiectele gestionate împreună; un mediu de dezvoltare integrat precum Rider sau Visual Studio oferă editare și navigare; IntelliSense oferă completare de cod și asistență conexă.
- **Test unitar, valoare așteptată, valoare efectivă, aserțiune și regresie:** un test unitar verifică un comportament izolat; valoarea așteptată este răspunsul de referință, valoarea efectivă este răspunsul calculat, o aserțiune verifică relația dintre ele, iar o regresie este un comportament care funcționa și a fost stricat de o modificare.
- **Caz de limită și eroare off-by-one:** un caz de limită se află la o margine a unui interval sau lângă ea; o eroare off-by-one numără cu un element prea mult sau prea puțin.
- **Refactorizare și mentenabilitate:** refactorizarea schimbă structura păstrând comportamentul; mentenabilitatea este ușurința de a înțelege și de a modifica acea structură.
- **Interval, coeficient, adâncime, acumulator și constantă magică:** un interval este o mulțime de ani, un coeficient este contribuția sa pe an, adâncimea este numărul de ani atinși în interiorul lui, un acumulator adună totalul, iar o constantă magică ascunde motivul unei valori numerice.
- **Tabel de căutare și regulă pe porțiuni:** un tabel de căutare stochează perechi intrare-ieșire precalculate; o regulă pe porțiuni alege o formulă după intervalul intrării.
- **Debug.Assert, șir interpolat, new cu tip țintă și expresie de colecție:** Debug.Assert este o verificare pentru depanare; un șir interpolat inserează valori de expresii în text; `new` cu tip țintă deduce tipul construit din context; o expresie de colecție folosește paranteze drepte pentru a construi o colecție precum un tablou.
- **Constantă cu nume, abstractizare, configurație, tablou și buclă:** o constantă cu nume dă sens unei valori fixe; abstractizarea separă o operație generală de reguli particulare; configurația păstrează aceste reguli; un tablou ține mai multe elemente; o buclă repetă o operație peste ele.
- **Clasă, struct, record struct, metodă, spațiu de nume și atribut:** o clasă sau un struct definește un tip; un record struct este un tip de valoare cu membri generați de record; o metodă este o funcție care aparține unui tip; un spațiu de nume grupează nume; un atribut atașează metadate unei declarații.
- **Referință de pachet, SDK de testare, runner și descoperire:** o referință de pachet aduce o dependență; SDK-ul de testare și runner-ul asigură execuția; descoperirea găsește metodele și seturile de date care pot rula ca teste.
- **xUnit, NUnit, Fact, Theory, InlineData și Assert.Equal:** xUnit și NUnit sunt biblioteci de testare .NET; Fact din xUnit marchează un test, Theory marchează un test parametrizat, InlineData furnizează un rând de argumente, iar Assert.Equal compară valorile așteptată și efectivă.
- **Test de integrare, serializare, parsare, JSON și HTML:** un test de integrare exercită mai multe componente împreună; serializarea transformă datele într-o reprezentare stocată, parsarea citește acea reprezentare, JSON este un format de date structurat, iar HTML descrie documente web.
- **Snapshot, aprobare și diferență:** un snapshot este o ieșire salvată folosită ca referință; aprobarea acceptă o referință revizuită; o diferență afișează nepotrivirile dintre ieșirea stocată și cea nou produsă.

Pentru practică anterioară cu compilarea și cu un [redacted], vezi [laboratorul de proiect](../../../labs/1_basic/02_project.md) din depozit. Este un document de laborator separat, nu o dovadă despre instalarea sau codul intermediar al acestei lecții.

Ordinea construirii este deliberată: creăm și conectăm proiectele, stabilim o funcție de referință, scriem verificări fără o bibliotecă de testare, rescriem funcția verificând rezultatele, apoi mutăm aceleași cazuri în xUnit. Testele de integrare și cele snapshot sunt demonstrate ulterior într-o altă aplicație. Importurile de fișiere de compilare personalizate, validarea configurației de producție, o reprezentare reală a unui interval infinit și optimizarea alocărilor rămân în afara exemplului implementat.

# Creează două proiecte console

[00:00:12](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=12s) Începem cu directoare surori `code` și `tests` sub un singur folder de exemplu. Un **proiect** grupează sursele și setările sale; **șablonul console** creează un proiect executabil minimal de editat, nu garanția că ambele proiecte vor rămâne executabile. **SDK-ul .NET** expune această operațiune prin **CLI-ul** `dotnet`, interfața de terminal.

1. Creează cele două directoare.
2. Rulează șablonul console pentru fiecare, astfel încât fiecare să aibă inițial un fișier de proiect și un program implicit.
3. [00:00:46](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=46s) Lasă configurația detaliată până când devine necesară.
4. [00:00:48](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=48s) Dă proiectelor roluri diferite: proiectul de cod va conține funcția și nu va fi lansat direct; proiectul de teste o va apela. Inițial acel apelant este un executabil cu verificări scrise manual. Mai târziu un runner de teste va executa teste dintr-o bibliotecă de teste.

Urmează o reconstrucție simplificată a comenzilor de creare. [Proiectul de teste executabil commituit](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/tests/tests.csproj) este o **stare ulterioară conexă**, nu o copie salvată a șablonurilor console neatinse.

```sh
dotnet new console -o examples/tests_example/code
dotnet new console -o examples/tests_example/tests
```

Programul principal generat conține inițial o instrucțiune de consolă precum cea de mai jos. Aceasta este o ilustrare a șablonului, nu un fișier inițial commituit exact.

```csharp
Console.WriteLine("Hello, World!");
```

# Partajează setările de compilare

[00:01:01](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=61s) Un **fișier de proiect**, adică `.csproj`, păstrează configurația în **XML**: etichete cu nume conțin proprietăți și liste de referințe. El memorează setările de compilare și dependențele de pachete ale proiectului. [00:01:19](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=79s) Corecția de aici privește responsabilitatea: nu invoci direct `csc`, **compilatorul** C#, pentru a construi aceste proiecte. **MSBuild** este motorul de compilare care coordonează operațiunea. [00:01:31](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=91s) El apelează compilarea ca parte din procesul de construire a surselor într-un **ansamblu**, adică unitatea compilată. Aceasta nu înseamnă că MSBuild înlocuiește compilatorul sau că este necesară o apelare separată a compilatorului pentru fiecare fișier sursă.

[00:01:36](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=96s) MSBuild poate importa documente `.props` și `.targets`, precum și fișierul de proiect principal. **Proprietățile de compilare** stochează valori; **targeturile** descriu lucrul de compilare. [00:01:42](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=102s) Configurația partajată evită repetarea acelorași setări în fiecare proiect copil. [00:02:02](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=122s) **Importul partajat** convențional `Directory.Build.props` furnizează proprietăți proiectelor de cod și de teste aflate sub directorul său. Numele convențional contează: un fișier `.props` arbitrar nu este importat automat doar pentru că există.

[Proprietățile partajate commituite](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/Directory.Build.props) păstrează această configurare. Exemplul alăturat este grupul de proprietăți real, scurtat doar fără a omite secțiuni de proiect suplimentare:

```xml
<Project>
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

Aici **target framework** selectează `net9.0`; **importurile implicite** activează importurile de spații de nume oferite de SDK; **nullabilitatea** activează analiza valorilor potențial null. **Tipul de ieșire** controlează ieșirea executabil față de cea de bibliotecă.

1. [00:02:13](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=133s) Copiază setările comune într-un `PropertyGroup` din elementul `Project` al noului fișier partajat.
2. Păstrează setările `OutputType` specifice fiecărui proiect în fișierele de proiect individuale.
3. [00:02:34](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=154s) Elimină setările copiate din primul proiect.
4. [00:02:54](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=174s) Elimină-le și din celălalt proiect: extragerea este incompletă cât timp rămâne vreo copie duplicată.
5. [00:03:04](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=184s) Ambele proiecte primesc acum setările comune dintr-un singur loc, astfel încât modificările ulterioare rămân sincronizate.

[00:03:19](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=199s) `Directory.Build.props` este importat prin convenție în timpul evaluării proiectului. În această structură de foldere simplă, ambele proiecte găsesc același fișier deasupra lor. [00:03:21](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=201s) Importurile explicite ale unor fișiere de compilare personalizate suplimentare sunt posibile, dar nu sunt demonstrate intenționat.

# Referă proiectul de cod

[00:03:23](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=203s) Prin convenție, testele trăiesc în propriul lor **ansamblu**, o unitate compilată produsă de proiectul de teste separat. Instrumentele le pot apoi găsi și rula folosind configurația lor obișnuită. [00:03:29](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=209s) Tehnic, testele pot fi puse în același proiect, dar lecția evită configurația manuală suplimentară.

[00:03:38](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=218s) Proiectul de teste trebuie să poată apela funcția din proiectul de cod. O **referință de proiect** declară această dependență. [00:03:58](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=238s) Adaugă un element `ProjectReference` cu o cale `Include` către `.csproj`-ul proiectului de cod. [Proiectul de teste manual commituit](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/tests/tests.csproj) conține referința și setarea sa de ieșire executabilă din acel moment:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
  </PropertyGroup>
  <ItemGroup>
    <ProjectReference Include="../code/code.csproj" />
  </ItemGroup>
</Project>
```

Exemplul normalizează slashurile inverse din commit în slashuri obișnuite pentru lizibilitate. `ItemGroup` grupează elementele de referință; `Include` identifică fișierul referit. O **cale relativă** se rezolvă din directorul fișierului de proiect de teste: `..` sau `../` urcă în părinte, apoi `code/code.csproj` coboară în fratele său.

[00:04:05](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=245s) Preferă această variantă unei căi absolute codificate fix. [00:04:10](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=250s) Căile absolute depind de o anumită mașină și de structura de fișiere a unui anumit sistem de operare, deci se pot strica pe Linux, Windows sau macOS când altcineva deschide checkoutul. Ele nu sunt intrinsec invalide pe acele sisteme. [00:04:13](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=253s) Referința face codul accesibil din proiectul de cod disponibil pentru proiectul de teste; regulile normale de vizibilitate din C# se aplică în continuare.

[00:04:23](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=263s) Proiectul de cod ar trebui acum să producă o **bibliotecă**, adică cod consumat de altă parte a programului, nu un executabil lansat separat. [00:04:32](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=272s) Acesta este exact rolul deținătorului funcției. [00:04:38](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=278s) Șterge intrarea `OutputType` a sa, astfel încât valoarea implicită de bibliotecă a SDK-ului să se aplice. [Proiectul de cod commituit](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/code/code.csproj) este corespunzător minimal:

```xml
<Project Sdk="Microsoft.NET.Sdk">
</Project>
```

[00:04:42](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=282s) Înlocuiește programul din șablonul console cu un ajutor apelabil. [Ajutorul commituit](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/code/Helper.cs) este o **implementare ulterioară conexă**, nu o captură exactă a acestui prim stub; confirmă ajutorul public static folosit de apelant.

```csharp
public static class Helper
{
    public static int GetHumanAge(int catAge)
    {
        // The conversion rule is filled in next.
        return 0;
    }
}
```

Acest stub este o reconstrucție simplificată. O **clasă** definește un tip; `static` permite apelanților să folosească funcția fără a crea o instanță; `public` o face accesibilă din celălalt proiect; `int` declară o intrare și un rezultat întregi. Funcția aparține tipului, deci este o **metodă**.

# Adaugă proiectele într-o soluție

[00:04:54](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=294s) O **soluție** este un fișier `.sln` care listează proiecte ce aparțin aceleiași unități de lucru. [00:05:03](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=303s) Ea permite gestionarea împreună a proiectelor de cod și de teste. O compilare la nivel de soluție procesează proiectele incluse, iar o comandă de testare la nivel de soluție rulează proiectele de teste; nu lansează fiecare bibliotecă ca executabil.

[00:05:18](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=318s) Ambele proiecte noi trebuie adăugate în listă. CLI-ul `dotnet sln` gestionează acea listă fără un IDE. [00:05:31](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=331s) O unealtă separată pentru editarea soluțiilor este o altă opțiune; lecția folosește mai întâi comanda SDK-ului, apoi demonstrează un generator.

1. [00:05:43](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=343s) Citește `dotnet sln --help`: subcomandele relevante sunt `add`, `remove` și `list`.
2. [00:06:01](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=361s) Folosește `list` pentru a inspecta elementele de proiect deja înregistrate în soluție.
3. [00:06:12](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=372s) Transmite calea relativă a fiecărui proiect către `add`.
4. [00:06:35](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=395s) Inspectează lista actualizată și vizualizarea din IDE.
5. [00:06:40](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=400s) Confirmă că codul și testele acestui exemplu sunt incluse alături de celelalte proiecte ale cursului.

[Soluția commituită](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/All.sln) este o **stare rezultată conexă**, nu o dovadă a secvenței exacte de comenzi din terminal. Aceste comenzi simplificate exprimă modificările demonstrate:

```sh
dotnet sln All.sln list
dotnet sln All.sln add examples/tests_example/code/code.csproj
dotnet sln All.sln add examples/tests_example/tests/tests.csproj
dotnet sln All.sln list
```

Soluția rezultată conține elemente de proiect de această formă (GUID-uri omise):

```text
Project(...) = "code", "examples\tests_example\code\code.csproj", ...
EndProject
Project(...) = "tests", "examples\tests_example\tests\tests.csproj", ...
EndProject
```

Ierarhia de foldere afișată ajută la organizarea proiectelor. Afișarea folderelor este distinctă de includerea altui proiect: referirea proiectului de cod și listarea ambelor proiecte într-o soluție rezolvă probleme diferite.

# Generează o soluție cu o unealtă

[00:07:05](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=425s) Alternativa este **SlnGen**, un generator de soluții console instalat prin `dotnet tool install`. [00:07:12](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=432s) Furnizează mulțimea proiectelor de inclus, astfel încât unealta să creeze o singură soluție care le cuprinde împreună, în loc de adăugări manuale repetate. [00:07:25](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=445s) Setează opțiunea de lansare pe false, pentru a evita pornirea automată a Visual Studio.

1. Instalează generatorul ca unealtă .NET.
2. Selectează proiectele pentru soluția generată; aici scopul sunt toate proiectele cursului.
3. Dezactivează lansarea automată a IDE-ului.
4. [00:07:34](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=454s) Rulează generatorul și inspectează rezultatul: ambele proiecte, de cod și de teste, sunt listate.

[Soluția commituită](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/All.sln) este **ieșire conexă**, nu o înregistrare de instalare sau o captură exactă a argumentelor generatorului. Un exemplu scurtat al conținutului vizat este:

```text
All.sln
  examples/tests_example/code/code.csproj
  examples/tests_example/tests/tests.csproj
  ...other selected course projects...
```

Rezultatul înregistrării arată că generarea a funcționat. Comanda exactă de instalare și scrierea wildcard-ului nu sunt păstrate în aceste commituri sursă, așa că această notă nu le inventează.

# Deschide soluția pentru navigare

[00:07:48](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=468s) Un **IDE**, mediu de dezvoltare integrat, folosește soluția deschisă pentru a decide ce proiecte aparțin spațiului de lucru. Problema care motivează este navigarea între proiecte: căutările după tip și după simbol au nevoie de cod și teste în același context încărcat. [00:07:54](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=474s) O căutare pornită dintr-un proiect izolat nu ajunge neapărat la cod din celălalt.

[00:08:13](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=493s) Deschide soluția în Rider sau Visual Studio, nu doar proiectul individual, pentru ca navigarea demonstrată și **IntelliSense**, completarea de cod și asistența conexă, să se întindă pe ambele. Acesta este fluxul prezentat, nu o afirmație că fiecare funcție de IDE are nevoie în mod universal de o soluție.

[Soluția commituită conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/All.sln) furnizează cele două elemente necesare pentru acel spațiu de lucru. Structura sa esențială este:

```text
Opened solution
├── code project: Helper
└── tests project: caller of Helper
```

<details>
<summary>Adăugarea unui proiect în soluție înlocuiește ProjectReference?</summary>

Nu. Soluția grupează proiectele pentru operațiuni de spațiu de lucru. ProjectReference declară dependența care permite codului proiectului de teste să apele biblioteca. Lecția are nevoie de ambele relații.

</details>

# Construiește prima conversie a vârstei

[00:08:33](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=513s) Codul testat convertește o vârstă de pisică, exprimată în numere întregi, într-un echivalent de vârstă umană. [00:08:45](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=525s) Un **test unitar** va verifica această funcție izolată, astfel încât înlocuirea implementării sale să nu îi schimbe răspunsurile în mod tăcut.

[00:09:05](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=545s) Începe cu cazurile speciale: zero ani dă zero, iar un an dă 15. [00:09:09](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=549s) Coeficientul primului an este deci 15. Un **coeficient** aici este numărul de ani echivalenți umani contribuți de un an de pisică. [00:09:17](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=557s) Prima estimare rostită atribuie 7 celui de-al doilea an, dând aproximativ 22 în total. Acea estimare este provizorie: implementarea și corecția explicită ulterioară folosesc **8**, dând **23**. Nu păstra 22 ca răspuns final așteptat.

[00:09:51](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=591s) Pentru **intervalul** inclusiv, adică mulțimea, de la 3 la 7, pornește de la totalul primilor doi ani și adaugă 3 pentru fiecare an atins în mulțime. La început, scăzând începutul intervalului, rezultă zero, deci numărarea inclusivă are nevoie de `+ 1`. [00:10:16](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=616s) Dincolo de interval, numără primii doi ani și blocul complet 3–7, apoi adaugă contribuția cozii. [00:10:33](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=633s) Coeficientul cozii este corectat de la 3 la **2**.

[Ajutorul bazat pe ramuri, păstrat](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/code/Helper.cs) este o **stare experimentală ulterioară conexă**, nu baza de referință exactă de mai jos: acea metodă veche commituită are un coeficient de 15 pentru al doilea an și un coeficient modificat pentru coadă. [Cazurile de test finale](https://github.com/AntonC9018/uniCourse_csharp/blob/e4b890a75d5e014ab00227c27ff6a35965acb9a1/examples/tests_example/tests/Tests.cs) fixează regula de predare corectată. Această reconstrucție simplificată exprimă explicit regula:

```csharp
static int Baseline(int age)
{
    if (age == 0) return 0;
    if (age == 1) return 15;
    if (age == 2) return 23;
    if (age >= 3 && age <= 7)
        return 23 + (age - 3 + 1) * 3;
    return 38 + (age - 8 + 1) * 2;
}
```

Ramurile `if` aleg un caz; `return` încheie funcția cu rezultatul ei; `&&` cere ca ambele comparații să fie adevărate; `>=` și `<=` includ capetele. Ultima ramură nu are încă validarea vârstelor negative, un defect discutat în curând.

[00:10:36](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=636s) Blocul complet 3–7 trebuie să folosească propriul coeficient, **3**, înainte ca coada să folosească **2**. [00:10:43](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=643s) Anii sunt 3, 4, 5, 6, 7: cinci ani, nu șapte. [00:10:52](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=652s) Aritmetica rostită în înregistrare include 21 și un total provizoriu de 44, iar [00:11:11](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=671s) tratează acel răspuns drept o bază verificabilă manual. Acele observații explică de ce se dorește o bază cu răspuns cunoscut, dar nu constituie o derivare consistentă a regulii finale. Cinci ani la 3 contribuie cu 15; totalul final pentru șapte ani este `15 + 8 + 5 * 3 = 38`. Numărul 44 ar fi răspunsul regulii finale la vârsta 10, nu la 7.

[00:11:26](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=686s) Odată ce o versiune care produce răspunsuri este acceptată ca referință, următoarea sarcină este să înghețăm comportamentul său cunoscut în teste. Coeficienții biologici sunt intenționat aproximativi; această lecție privește comportamentul software și păstrarea lui.

# Folosește testele ca referință de comportament

[00:11:40](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=700s) O funcție poate da răspunsurile corecte și totuși să aibă o structură pe care vrei să o îmbunătățești. [00:11:59](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=719s) Obiectivul este atât comportament corect, cât și o implementare organizată cum se intenționează. [00:12:08](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=728s) Înlocuirea unei funcții stângace cu una mai clară ar trebui să păstreze aceleași ieșiri. [00:12:22](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=742s) Acestea sunt două criterii distincte: testele verifică rezultatele; proiectarea rescrierii privește structura.

**Refactorizarea** schimbă structura păstrând comportamentul observabil. [00:12:47](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=767s) Testele fac acea schimbare mai sigură, reținând răspunsuri cunoscute independent de implementarea editată. [00:13:31](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=811s) Aceasta este scopul principal subliniat aici: detectarea unei **regresii**, un comportament care funcționa și a fost stricat de o modificare de cod. [00:13:32](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=812s) Funcția corectă anterioară oferă o referință față de care cea nouă poate fi judecată.

[00:13:57](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=837s) O a doua utilizare este izolarea unui defect concret cu exact intrarea pe care apare. [00:14:06](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=846s) Un test apelează funcția cu o intrare cunoscută, obține **valoarea efectivă**, adică rezultatul calculat, și o compară cu **valoarea așteptată**, adică rezultatul de referință. O **aserțiune** exprimă relația cerută.

[00:14:21](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=861s) Nu este necesar să încerci exhaustiv toate intrările pentru acest exercițiu. [00:14:50](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=890s) Alege o submulțime mică care reprezintă intervalele relevante, în special marginile lor, în loc să încerci un domeniu nelimitat. Aceasta oferă dovezi despre comportamentul selectat, nu o demonstrație matematică de corectitudine pentru fiecare intrare. Un test generat dintr-o bază defectă poate păstra defectul.

<details>
<summary>Dacă noua funcție trece aceste cazuri, am demonstrat că este corectă pentru orice număr întreg?</summary>

Nu. Am verificat exemplele selectate. Valoarea lor vine din acoperirea ramurilor și a erorilor probabile la margini. Corectitudinea în afara acelor exemple depinde în continuare de reguli și de implementare; o referință defectă poate da și așteptări defecte.

</details>

# Alege cazuri de limită

[00:15:02](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=902s) Un **caz de limită** se află la un capăt de interval sau imediat lângă el. Funcția inițială are un defect specific: o intrare negativă cade în ultima ramură, deși o vârstă negativă ar trebui să provoace o eroare. Lecția identifică restricția de intrare lipsă; cele nouă cazuri finale, pozitive sau zero, nu implementează un test de excepție pentru ea.

[00:15:25](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=925s) Inspectează regiunile distincte: zero, unu, doi, precum și începutul, mijlocul și sfârșitul intervalului 3–7. [00:15:41](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=941s) Un capăt este o sursă comună de greșeli, deci include valori cu unu mai jos și cu unu mai sus. [00:15:59](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=959s) În particular, vârsta 8 exercitează ramura imediat următoare lui 7.

1. Alege 0, 1 și 2 pentru cazurile speciale.
2. Alege 3 și 4, la începutul următorului interval și imediat după el.
3. Alege 6, 7 și 8 în jurul limitei sale superioare.
4. Adaugă 15 drept un caz de coadă mai îndepărtat.
5. Păstrează defectul de intrare negativă vizibil ca o cerință de validare neimplementată.

[00:16:08](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=968s) O **bibliotecă** de testare furnizează de obicei mecanismele de aserțiune și de execuție, dar un test poate fi și o verificare scrisă manual. [00:16:11](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=971s) Amână conectarea bibliotecii pentru a reduce cantitatea de configurare nouă adusă dintr-o dată: mai întâi scrie verificările, înțelege-le și conectează mai târziu xUnit.
# Generează așteptările manual

[00:16:29](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=989s) Apelează ajutorul existent pentru o intrare cunoscută; zero ar trebui să producă zero. Această primă verificare întreabă dacă comportamentul de referință se păstrează. [00:16:46](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1006s) Aranjează ca o verificare de egalitate eșuată să semnaleze o eroare, astfel încât o ramură incorectă să fie observabilă. [00:16:59](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1019s) Cazuri precum vârsta doi sunt ieftine de enunțat direct, dar calcularea manuală a mai multor cazuri mai mari este repetitivă.

[00:17:16](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1036s) Folosește o **buclă**, o operație repetată, ca să rulezi funcția de încredere peste intrările alese și să generezi răspunsurile așteptate. [00:17:25](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1045s) Funcția este deja presupusă corectă, deci poate produce acele constante. [00:17:32](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1052s) Sarcina concretă este să asociezi fiecare intrare cu răspunsul returnat. [00:17:37](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1057s) Alege de la 0 la 4 drept cazurile timpurii alăturate. [00:17:49](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1069s) Omite 5, singurul gol dintre cele două rulări alese; nu este invalid și aparține regulii 3–7. [00:18:01](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1081s) Continuă cu 6, 7, 8 și 15.

[Verificările manuale commituite](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/tests/Tests.cs) sunt o **stare ulterioară care conține constantele generate**, nu programul temporar de afișare exact. O reconstrucție simplificată a acelui pas temporar este:

```csharp
int[] inputs = [0, 1, 2, 3, 4, 6, 7, 8, 15];
foreach (int input in inputs)
    Console.WriteLine($"Test({input}, {Helper.GetHumanAge(input)});");
```

Un **tablou** ține intrările. `foreach` vizitează fiecare element. Forma `$"…"` este un șir interpolat: expresiile din acolade devin text.

1. [00:18:10](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1090s) Scrie o buclă de afișare care emite atât intrarea, cât și răspunsul calculat.
2. [00:18:21](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1101s) Ruleaz-o și obține mulțimea completă de perechi intrare-răspuns.
3. Copiază acele răspunsuri în verificări fixe înainte de a schimba implementarea.
4. [00:18:46](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1126s) Amintește de ce sunt acceptate: funcția originală era presupusă corectă.

Generarea constantelor o singură dată și salvarea lor este crucială. Recalcularea valorilor așteptate și efective prin aceeași implementare schimbătoare la fiecare rulare de test nu ar proteja independent comportamentul anterior.

# Fă o verificare manuală să eșueze

[00:19:01](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1141s) Dacă o condiție de egalitate simplă eșuează, programul scris manual demonstrat se oprește cu o eroare. [00:19:12](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1152s) Schimbă intenționat valoarea cunoscută **de la 23 la 24** pentru cazul vârstei doi, ca să verifici că verificarea observă nepotrivirea. [00:19:25](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1165s) Rularea raportează un răspuns greșit pentru doi și se oprește; verificările sunt secvențiale, deci cazurile ulterioare nu primesc propriul rezultat. [00:19:44](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1184s) Demonstrarea indică linia cinci, dar nu arată clar ambele valori nepotrivite. Descrierea rostită inversează într-un moment valorile așteptată și efectivă; dovada importantă este nepotrivirea dintre 23 și 24.

[00:19:37](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1177s) O bibliotecă de testare poate executa și raporta fiecare caz de date separat, în loc să abandoneze întreaga listă la prima eșec. [00:19:49](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1189s) O **aserțiune** de bibliotecă poate afișa imediat valorile așteptată și efectivă, evitând numărarea liniilor.

[00:20:02](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1202s) Îmbunătățește mai întâi ieșirea scrisă manual: construiește un mesaj care numește intrarea, rezultatul calculat și răspunsul așteptat. [00:20:26](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1226s) Pune toate acele câmpuri cu nume într-un singur șir interpolat. [00:20:32](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1232s) O valoare simplă nu ajunge pentru a identifica ce caz a eșuat. [Ajutorul de teste manual commituit](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/tests/Tests.cs) conține această formă de diagnosticare; exemplul este scurtat doar în jurul creării configurației:

```csharp
void Test(int input, int expected)
{
    var config = CreateConfig();
    var actual = Helper.GetHumanAge_New(config, input);
    Debug.Assert(actual == expected,
        $"Output for {input} is {actual}, expected {expected}");
}
Test(2, 23);
```

`Debug.Assert` este o aserțiune de depanare, o verificare destinată depanării, nu un înlocuitor al unui runner de teste. Înregistrarea îi demonstrează comportamentul de eșec în fluxul activ de depanare; acel comportament nu este garantat în orice configurație de compilare.

[00:20:42](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1242s) Un **tabel de căutare**, o mapare precalculată de la intrare la răspuns, este o altă modalitate posibilă de a organiza datele. [00:20:50](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1250s) Următoarea implementare exprimă în schimb conversia ca mulțimi de intervale. Sunt alegeri distincte: organizarea cazurilor de test nu impune ca funcția de producție să folosească aceeași reprezentare.

# Rescrie suma ca contribuții de intervale

[00:20:56](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1256s) Un **interval** este un grup de ani care împart același coeficient, contribuția lor pe an. Separă grupurile în loc să ascunzi totalurile anterioare în fiecare ramură ulterioară. [00:21:16](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1276s) Vârsta zero nu contribuie cu nimic: deși eticheta „de la 0 la 0” numește o valoare, ea reprezintă **zero ani trăiți**, nu un interval închis matematic gol. Primul grup de ani trăiți este de la 1 la 1 și contribuie cu 15.

[00:21:27](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1287s) Rezolvă al doilea grup de un an. [00:21:39](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1299s) Valoarea rostită 7 este corectată la **8**; [00:21:49](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1309s) ea se adaugă la valoarea acumulată din primul an. [00:21:54](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1314s) Anii de la 3 la 7 contribuie fiecare cu 3, iar anii de la 8 încolo contribuie fiecare cu 2.

[00:22:18](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1338s) Un **acumulator** este o variabilă care pornește de la zero și adună fiecare contribuție. [00:22:31](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1351s) Mulțimea finală adaugă câte 2 pentru fiecare an atins după blocurile anterioare. [00:22:37](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1357s) Această primă rescriere aditivă folosește condiții explicite în locul unei bucle, deși și o buclă ar funcționa.

[Ajutorul generalizat commituit](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/code/Helper.cs) este o **stare ulterioară conexă**, nu o copie exactă a acestei funcții aditive intermediare. Reconstrucția simplificată de mai jos păstrează regula de intervale stabilită de [așteptările finale](https://github.com/AntonC9018/uniCourse_csharp/blob/e4b890a75d5e014ab00227c27ff6a35965acb9a1/examples/tests_example/tests/Tests.cs):

```csharp
static int Additive(int age)
{
    int total = 0;
    if (age >= 1) total += 15;
    if (age >= 2) total += 8;
    if (age >= 3) total += Math.Min(5, age - 3 + 1) * 3;
    if (age >= 8) total += (age - 8 + 1) * 2;
    return total;
}
```

`+=` adună la acumulator. `Math.Min(a, b)` returnează argumentul mai mic, limitând contribuția unui interval.

1. [00:22:42](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1362s) Adaugă primul bloc de un an dacă a fost trăit cel puțin un an.
2. Adaugă al doilea bloc de un an dacă au fost trăiți cel puțin doi ani.
3. [00:22:57](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1377s) Pentru vârsta trei sau mai mare, calculează cât din blocul 3–7 a fost atins.
4. [00:23:12](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1392s) Măsoară **adâncimea** sa, numărul de ani atinși în bloc, ca `age - 3 + 1`. La vârsta 3 adâncimea este 1, nu 0.
5. [00:23:30](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1410s) Lungimea sa inclusivă este `7 - 3 + 1 = 5`.
6. [00:23:42](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1422s) plafonează adâncimea ca o vârstă ulterioară să nu poată adăuga mai mult de cei cinci ani din acel bloc.
7. [00:23:50](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1430s) Cuvântul „maxim” se corectează în **minim**: alege valoarea mai mică dintre lungimea completă și adâncimea atinsă.
8. [00:24:02](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1442s) Înmulțește adâncimea plafonată cu 3.
9. [00:24:08](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1448s) Aplică aceeași idee de contribuție mulțimii rămase.
10. [00:24:18](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1458s) Cu o coadă cu adevărat nemărginită nu există plafon superior; adaugă adâncimea atinsă înmulțită cu 2.
11. [00:24:38](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1478s) Adâncimea cozii este `age - 8 + 1`, adică `age - 7`. La vârsta 8 este **1**, nu 7.
12. [00:24:45](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1485s) Adaugă acea adâncime înmulțită cu coeficientul cozii.

Funcția intermediară schimbă forma calculului: fiecare interval contribuie o singură dată. Ea nu impune încă eroarea intenționată pentru vârstele negative.

<details>
<summary>De ce aparține Math.Min aici și nu Math.Max?</summary>

La vârsta 4 adâncimea atinsă este 2, iar lungimea blocului este 5. Avem nevoie de contribuția a 2 ani, nu de toți cei 5. La vârsta 8 adâncimea atinsă ar fi 6, dar acest bloc poate contribui doar cu 5. Valoarea mai mică este corectă în ambele cazuri.

</details>


# Verifică implementarea rescrisă

[00:24:50](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1490s) Implementarea a fost înlocuită, dar ar trebui să rămână echivalentă pe cazurile salvate. [00:25:02](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1502s) Rulează imediat acele verificări: uitarea lui `+ 1` ar elimina primul an al unui interval inclusiv și ar schimba un răspuns cunoscut. Aceasta este o **eroare off-by-one**, numărarea cu un an prea puțin.

[00:25:13](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1513s) O așteptare eșuată dezvăluie o greșeală în ramura vârstei opt; ramura este corectată. Rezultatul este o dovadă despre cazurile de test existente, nu o demonstrație că orice vârstă posibilă este acoperită.

[Lista commituită de teste manuale](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/tests/Tests.cs) este un **punct de control ulterior conex** care poartă așteptările de limită folosite pentru verificarea rescrierilor. Un exemplu compact arată sfârșitul unui bloc și începutul următorului:

```csharp
Test(7, 38); // all five years in the 3–7 block
Test(8, 40); // plus the first year of the tail
Test(15, 54);
```

[00:25:24](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1524s) O altă abordare este să păstrăm vechea implementare și să îi comparăm rezultatele cu cele ale celei noi. [00:25:37](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1537s) Aceasta reutilizează referința în timp ce noua structură îmbunătățește **mentenabilitatea**, ușurința de a înțelege și de a modifica codul. Nu mărește automat acoperirea: ambele abordări depind în continuare de intrările pe care le încerci și ambele moștenesc orice greșeală din referință.

# Fii explicită constantele magice

[00:25:46](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1546s) Forma inițială cu ramuri este stângace din cauza **constantelor magice**, numere neexplicate care codifică mai multe părți ale unei reguli. [00:25:54](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1554s) Valoarea 23 repetată înseamnă `15 + 8`, iar 38 înseamnă `15 + 8 + 5 * 3`. Sunt totaluri acumulate, nu setări independente.

[00:26:09](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1569s) Dezavantajul este mentenabilitatea, nu consumul de memorie. [00:26:18](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1578s) Dacă coeficientul primului an se schimbă din 15 în 16, fiecare constantă cumulată care îl conține trebuie schimbată la rândul ei. O apariție omisă face ramurile ulterioare inconsistente. [00:26:30](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1590s) Într-o versiune aditivă, valoarea 16 a primei componente are un scop clar. [00:26:42](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1602s) Descompunerea sumei în componente separate face ca o modificare a regulii să fie locală. [00:26:56](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1616s) Descompunerea corectă este **15 pentru primul an plus 8 pentru al doilea**, indiferent de formularea inversată din înregistrare.

[00:27:25](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1645s) O **constantă cu nume** dă unei valori fixe un identificator descriptiv. [Funcția cu ramuri păstrată, commituită](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/code/Helper.cs) este **cod experimental conex** care arată această extragere, dar valorile coeficienților săi diferă de cele de bază. Exemplul simplificat de mai jos folosește valorile de bază pentru a explica structura:

```csharp
const int firstYearCoefficient = 15;
const int secondYearCoefficient = 8;
const int middleStart = 3;
const int middleEnd = 7;
const int middleCoefficient = 3;

int firstTwoYears = firstYearCoefficient + secondYearCoefficient;
int middleLength = middleEnd - middleStart + 1;
```

`const` declară o valoare fixă la timpul compilării. [00:27:37](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1657s) Denumirea componentelor pentru primul an, al doilea an și intervale este intenționată ca să păstreze comportamentul expunând înțelesul. [00:28:02](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1682s) Exprimarea lungimii ca `end - start + 1` explică de ce o mulțime inclusivă are cinci ani. Un nume descriptiv ajută, dar forma cu ramuri poate repeta în continuare întreaga expresie cumulată.

# Adaugă un interval și vezi duplicarea

[00:28:25](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1705s) Extinde regula pe porțiuni cu încă un interval, după cel precedent și până la vârsta 10. O **regulă pe porțiuni** alege o formulă diferită pentru fiecare mulțime; aici este doar regula intervalelor exprimată prin ramuri. [00:28:35](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1715s) Fiecare interval adăugat are nevoie de constantele proprii, iar ramurile următoare trebuie să includă fiecare contribuție anterioară.

[00:28:48](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1728s) Copiază contribuțiile anterioare în noua ramură, apoi adaugă termenul noului interval. [00:29:04](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1744s) Acest termen numără anii atinși de la începutul său după același model `age - start + 1` și înmulțește cu coeficientul său; formula verbală abreviată din înregistrare nu trebuie citită ca o scădere în ordine inversă.

[Funcția cu ramuri păstrată, commituită](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/code/Helper.cs) este o **stare experimentală conexă** cu o ramură pentru vârstele 8–10 și coeficient 4. Ramura sa scurtată demonstrează repetarea:

```csharp
const int intLen = thirdIntervalEnd - thirdIntervalStart + 1;
if (catAge >= 8 && catAge <= 10)
{
    return firstAgeCoef + secondAgeCoef
        + intLen * _3to7intervalCoef
        + (catAge - 8 + 1) * 4;
}
```

Acei identificatori aparțin funcției experimentale citite; nu sunt configurația finală de test. În acel commit ramura sa implicită repetă în continuare aceeași sumă, ilustrând de ce codul este reconsiderat, mai degrabă decât oferind o nouă regulă de business validată.

[00:29:17](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1757s) O ramură implicită dincolo de ultimul interval trebuie să repete din nou toate contribuțiile anterioare. Repetarea pare explosivă la scriere, dar nu este nevoie de nicio afirmație de complexitate exponențială: mai multe ramuri înseamnă mai multe expresii cumulate copiate și mai multe locuri de actualizat.

[00:29:36](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1776s) Revino la un calcul **bazat pe intervale**: inspectează fiecare mulțime o singură dată și adaugă contribuția acoperită de aceasta. [00:29:52](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1792s) Odată ce vârsta atinge începutul unei mulțimi, adaugă partea din acea mulțime efectiv trăită. Prima mulțime contribuie cu 15 înmulțit cu un an acoperit, nu cu valoarea numerică a începutului său ca atare. [00:30:06](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1806s) Fiecare regulă poate apărea apoi o singură dată, eliminând sumele duplicate.

[00:30:21](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1821s) Avansează începutul cu lungimea tocmai procesată, în loc să menții informații redundante despre început și lungime de-a lungul calculului. [Bucla ulterioară din ajutorul commituit](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/code/Helper.cs) folosește această formă:

```csharp
int end = start + interval.Length;
// Add this interval's covered contribution.
start = end;
```

[00:30:42](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1842s) Dacă o constantă nu mai este repetată, extragerea ei doar pentru a evita repetarea nu este necesară. Numele pot ajuta în continuare la comunicarea sensului; ideea este să eviți munca de denumire care nu adaugă valoare.

# Alege abstractizarea după schimbarea așteptată

[00:30:46](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1846s) O îmbunătățire suplimentară este un tablou de intervale și o buclă peste ele. **Abstractizarea** separă mecanismul de calcul de regulile sale particulare; **configurația** este datele care țin aceste reguli. Aceasta face ca adăugarea unei reguli să fie o modificare de date, nu încă o ramură.

[00:31:01](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1861s) Regula din exemplu are puține șanse să se schimbe; detaliul repetitiv rămas ar putea fi generat de o unealtă AI de codare și lăsat așa. Este o observație pragmatică de scop, nu un înlocuitor al verificării corectitudinii. [00:31:09](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1869s) Compromisul de proiectare este timpul cheltuit acum față de beneficiul viitoarelor modificări. Abstractizările lizibile se amortizează atunci când cineva va modifica efectiv codul.

[00:31:25](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1885s) Pentru o funcție de coeficienți stabilă, versiunea explicită simplă poate fi suficientă. [00:31:37](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1897s) O regulă de joc care se schimbă este un exemplu contrast: viața sau atacul unui inamic ar putea crește cu o rată pe patru niveluri și cu altă rată pe următoarele trei. [00:31:50](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1910s) Regulile editate frecvent beneficiază de componente cu nume și de o structură bazată pe intervale, pentru că locurile de editare sunt clare.

[00:32:00](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1920s) Dacă utilizatorii vor furniza intervale și coeficienți într-un fișier de configurare, folosește o buclă generalizată peste datele lor în locul ramurilor codificate fix. [00:32:14](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1934s) Dacă regulile nu se vor schimba, poate fi preferabilă implementarea cea mai puțin elaborată care este suficientă. [00:32:14](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1934s) Randamentul buclei depinde de reguli care se schimbă, nu de faptul că codul abstract e universal mai bun. [00:32:26](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1946s) Lecția continuă totuși cu versiunea generalizată pentru a demonstra acea tranziție; încărcarea unui fișier de configurare real nu este implementată.

[Ajutorul bazat pe intervale, commituit](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/code/Helper.cs) este rezultatul concret ulterior al acestei alegeri. Separarea definitorie a lui este:

```csharp
// Rules are data; the calculation can visit any such sequence.
foreach (var interval in config.Intervals)
{
    // Calculate this interval's contribution.
}
```

<details>
<summary>Ar fi o buclă întotdeauna mai bună decât patru condiții explicite?</summary>

Nu. Patru condiții stabile și lizibile pot cere mai puțin timp de scriere și întreținere. O buclă peste configurare devine utilă când regulile se schimbă des sau provin din afara programului. Testele protejează comportamentul indiferent de structura aleasă.

</details>

# Reprezintă regulile ca date

[00:32:40](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1960s) Introdu un **tip Interval** care poartă lungimea și coeficientul; fie o clasă, fie un struct poate servi ideii. Un **struct** este un tip de valoare, iar o **clasă** este un tip de referință. [00:32:48](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1968s) Configurația devine un tablou al acestor intervale. [00:32:54](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1974s) Transmite-l funcției de calcul împreună cu vârsta, astfel încât datele regulilor să călătorească ca parametri.

[Ajutorul commituit](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/code/Helper.cs) conține aceste declarații. Un **record struct** este o declarație de tip de valoare care generează membri în stil record; `readonly` împiedică modificarea stării instanței sale. Declarația pozițională creează proprietățile cu nume:

```csharp
public readonly record struct Interval(int Length, int Coefficient);
public readonly record struct Config(Interval[] Intervals);
```

`[]` după `Interval` înseamnă un tablou de intervale. `Config` grupează acel tablou într-un singur parametru. Aceasta este o reprezentare compactă, nu un sistem complet de validare a configurației.

[00:33:03](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1983s) Parcurge intervalele configurate și compară vârsta cu poziția intervalului curent. [00:33:10](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=1990s) Mulțimea curentă se termină acolo unde începe următoarea; [00:33:32](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2012s) explicația descrie întâi acest lucru ca preluarea sfârșitului din intervalul următor. În reprezentarea aleasă, contiguă și bazată pe lungimi, nu este nevoie să cauți începutul explicit al altei intrări. [00:33:40](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2020s) Calculează sfârșitul ca începutul curent plus lungimea acestui interval.

[00:33:53](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2033s) Limitează contribuția la marginea mulțimii, în loc să numere dincolo de ea. [00:34:04](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2044s) Pentru versiunea cu început inclusiv, adâncimea atinsă este `age - start + 1`; ea este **plafonată** de lungimea intervalului, nu adăugată acelei lungimi. [00:34:22](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2062s) Înmulțește adâncimea acoperită cu coeficientul, apoi avansează începutul până la sfârșitul calculat.

[Bucla commituită](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/code/Helper.cs) este o **stare ulterioară consistentă cu origine zero**, distinctă de versiunea inițială cu început inclusiv care este corectată în video. Exemplul scurtat de mai jos îi păstrează calculele reale:

```csharp
int ret = 0;
int start = 0;
foreach (var interval in config.Intervals)
{
    int end = start + interval.Length;
    if (catAge > start)
    {
        int depth = catAge - start;
        ret += Math.Min(interval.Length, depth) * interval.Coefficient;
    }
    start = end;
}
return ret;
```

Aici `start` numără anii deja atribuiți mulțimilor anterioare, deci `catAge - start` este adâncimea în următoarea mulțime. O adâncime pozitivă este plafonată de `Math.Min`. Secțiunea următoare urmează configurația din înregistrare, apoi explică de ce schimbarea orbească a acestei origini de numărare provoacă un defect.

# Creează și transmite configurația

[00:34:37](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2077s) Apelanții trebuie să furnizeze regulile. Introdu o funcție care construiește o **configurație**, tabloul de lungimi și coeficienți pe care calculul îl citește.

1. [00:34:50](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2090s) Definește primul grup ca un an trăit, pornind de la anul 1, cu lungimea 1 și coeficientul 15.
2. [00:35:05](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2105s) Definește al doilea grup cu lungimea 1 și coeficientul **8**. El acoperă anul **2**; 8 este rata lui, nu începutul său.
3. [00:35:14](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2114s) Definește al treilea grup ca anii 3–7, cu lungimea 5 și coeficientul 3.
4. [00:35:23](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2123s) Definește grupul rămas cu coeficientul 2. Tipul nu are un marcaj real de infinit, deci folosește o lungime finită foarte mare. Cuvântul rostit „doi” este coeficientul, nu o lungime de doi.
5. [00:35:33](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2133s) Construiește configurația cu `new` și transmite-o funcției de calcul.
6. [00:35:47](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2147s) Lasă faptul că la fiecare apel se creează o configurație nouă drept un detaliu minor, în afara scopului de optimizare al acestui exemplu.

[Fabrica de configurație și apelantul manual, commituite](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/tests/Tests.cs) păstrează această stare; [versiunea finală xUnit](https://github.com/AntonC9018/uniCourse_csharp/blob/e4b890a75d5e014ab00227c27ff6a35965acb9a1/examples/tests_example/tests/Tests.cs) păstrează aceleași reguli:

```csharp
Helper.Config CreateConfig()
{
    return new([
        new(Length: 1, Coefficient: 15),
        new(Length: 1, Coefficient: 8),
        new(Length: 5, Coefficient: 3),
        new(Length: 99999, Coefficient: 2),
    ]);
}

var config = CreateConfig();
var actual = Helper.GetHumanAge_New(config, input);
```

`new` construiește o valoare. Când tipul este cunoscut din context, `new(...)` cu tip țintă îi poate omite numele. Argumentele cu nume `Length:` și `Coefficient:` fac explicit sensul fiecărui număr. Expresia de colecție între paranteze drepte creează tabloul de intervale așteptat de constructorul configurației. Aceste forme de sintaxă compactează datele; preocuparea principală a lecției rămâne comportamentul intervalelor.

O lungime de 99999 acoperă intrările demonstrației, dar rămâne finită. Intrările dincolo de lungimea totală reprezentată ar înceta să mai primească contribuție. Intervalele cu adevărat nemărginite și validarea configurației externe nu sunt implementate aici.

# Corectează originea numărării intervalelor

[00:35:53](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2153s) Prima rulare generalizată este greșită: intrarea zero produce 15. Ea număra primul interval de ani trăiți de la zero. O **eroare off-by-one** este o nepotrivire cu o unitate în privința locului unde începe sau se termină numărarea; acesta este un exemplu concret.

[00:36:07](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2167s) Mută începutul inclusiv al intervalului la 1. [00:36:13](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2173s) Următoarea rulare întoarce valorile așteptate, iar calculul se citește clar. [00:36:15](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2175s) Se ia în considerare altă scriere a inițializării, ca să se evite greșeala. [00:36:18](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2178s) Propunerea este să mute ajustarea originii în altă parte a formulei. [00:36:26](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2186s) `1` trebuie să apară exact o dată, în locul potrivit. [00:36:32](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2192s) Doar mutarea începutului la zero și folosirea unei comparații stricte pare inițial echivalentă. [00:36:40](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2200s) O inspecție mai atentă respinge acea editare provizorie așa cum este scrisă; forma funcțională cu început inclusiv se păstrează în timpul acestei demonstrații.

[Ajutorul commituit](https://github.com/AntonC9018/uniCourse_csharp/blob/b333fe75bfe90c7d2c965c1a57f477497887e5e2/examples/tests_example/code/Helper.cs) este o **variantă ulterioară cu origine zero**, nu o captură exactă a editării parțiale respinse. El schimbă în mod consistent comparația și adâncimea. Următoarele formulări simplificate echivalente arată toate părțile care trebuie să se mute împreună:

```csharp
// Inclusive lived-year start.
int start = 1;
if (age >= start)
    total += Math.Min(length, age - start + 1) * coefficient;

// Boundary after already-counted years, as in the committed helper.
int boundary = 0;
if (age > boundary)
    total += Math.Min(length, age - boundary) * coefficient;
```

Pentru intervale succesive, avansează fie începutul, fie marginea cu lungimea intervalului. Corecția din video privește o combinație intermediară inconsistentă; o formulă consistentă cu origine zero poate funcționa, iar ajutorul commituit o demonstrează.

[00:36:45](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2205s) Testele existente fac ca această înlocuire și corecțiile ei să fie sigure de încercat: ele verifică aceleași ieșiri, deși internele s-au schimbat substanțial. Lecția nu are nevoie ca elevul să se încredă în asemănarea vizuală dintre formule.

<details>
<summary>Ce s-ar întâmpla dacă start ar fi zero, dar adâncimea ar folosi în continuare age - start + 1?</summary>

La vârsta zero adâncimea calculată ar fi unu, dacă condiția ar permite intrarea, numărând greșit primul an trăit. Mutarea originii cere schimbarea comparației și a adâncimii împreună, apoi rularea din nou a cazurilor salvate de zero și de limită.

</details>


# Conectează xUnit

[00:37:13](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2233s) xUnit și NUnit sunt cele două biblioteci de testare .NET larg folosite, menționate în lecție; acest exemplu alege **xUnit**. O bibliotecă de testare furnizează atribute de test și aserțiuni, iar **SDK-ul de testare** și **runner-ul** furnizează mecanismele de execuție și descoperire.

[00:37:24](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2244s) Instructorul descrie o configurare anterioară care folosea importul partajat convențional `Directory.Build.targets` pentru a injecta dependențe de test în proiectele de sub un folder. Aceasta este o alternativă descrisă, nu structura de pachete commituită pentru acest exemplu. [00:37:32](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2252s) Discuția privește și vizibilitatea proiectelor în IDE: prezența în sistemul de fișiere sau descoperirea este distinctă de instalarea pachetelor, iar fluxul fiabil pentru soluție descris mai sus include explicit proiectele. Nici un fișier de proiect din apropiere, nici o intrare din soluție nu descarcă automat xUnit.

[00:37:39](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2259s) Pachetele pot fi adăugate prin consolă cu `dotnet add package` sau prin editarea directă a XML-ului proiectului. O **referință de pachet** declară o dependență în fișierul de proiect. [Proiectul de teste final commituit](https://github.com/AntonC9018/uniCourse_csharp/blob/e4b890a75d5e014ab00227c27ff6a35965acb9a1/examples/tests_example/tests/tests.csproj) înregistrează versiunile exacte folosite în acest punct de control, nu o promisiune a celei mai recente combinații compatibile de pachete:

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.10.0" />
  <PackageReference Include="xunit" Version="2.9.0" />
  <PackageReference Include="xunit.runner.visualstudio" Version="2.8.2" />
</ItemGroup>
```

Acest exemplu omite metadatele de active ale runner-ului și colectorul de acoperire commituit. Biblioteca xUnit furnizează API-ul de testare; adaptorul runner-ului îl conectează la descoperire/execuție; SDK-ul de testare susține infrastructura de testare. Metadatele opționale de colector sau de șablon nu trebuie confundate cu mecanismele necesare pentru a rula aceste teste.

1. [00:37:45](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2265s) Aduce pachetele de testare, prin editări CLI sau prin XML echivalent.
2. [00:37:49](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2269s) Revizuiește elementele suplimentare generate, în loc să păstrezi orbește fiecare intrare din șablon; nu toate setările opționale sunt necesare pentru compilare.
3. [00:37:50](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2270s) Elimină `OutputType` executabil al proiectului de teste: cu xUnit, acesta devine o bibliotecă rulată de infrastructura de testare.
4. [00:37:54](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2274s) Un import generat de spațiu de nume poate face `Xunit` disponibil în fiecare fișier. Este un element `Using`, nu referința de pachet însăși.
5. [00:37:56](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2276s) Acest import automat este opțional: importul explicit al lui `Xunit` în fișierul de test funcționează la fel de bine.
6. [00:38:02](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2282s) Încheie conversia de la un program console de nivel superior la metode într-o clasă de testare. Cât timp pachetele se importă și sursa este reorganizată, proiectul poate fi temporar incomplet.

[Proiectul final](https://github.com/AntonC9018/uniCourse_csharp/blob/e4b890a75d5e014ab00227c27ff6a35965acb9a1/examples/tests_example/tests/tests.csproj) nu are setare de ieșire executabilă, iar [sursa finală de teste](https://github.com/AntonC9018/uniCourse_csharp/blob/e4b890a75d5e014ab00227c27ff6a35965acb9a1/examples/tests_example/tests/Tests.cs) alege un import explicit de spațiu de nume. Distincția este ilustrată de aceste alternative:

```xml
<!-- Optional generated namespace import, not present in the final project. -->
<ItemGroup>
  <Using Include="Xunit" />
</ItemGroup>
```

```csharp
// The final file uses this instead.
using Xunit;
```

Un **spațiu de nume** grupează nume; `using Xunit;` face nume precum `Assert` disponibile fără a le califica. Lipsa unei metode de test nu invalidează, prin ea însăși, o referință de pachet și nu împiedică un proiect să compileze. Eroarea temporară aparține conversiei neterminate, nu unei cerințe generale că fiecare pachet importat trebuie deja folosit de un test.

[00:38:48](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2328s) Cât timp IDE-ul importă încă pachete, începe să remodelezi sursa în metode de testare, în loc să aștepți doar pentru a explica structura testelor.

# Descoperă și rulează un Fact

[00:38:56](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2336s) Importă spațiul de nume `Xunit` și declară o **clasă** de testare, un tip care conține metodele de test. [00:39:05](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2345s) O **metodă** este o funcție declarată în interiorul acelui tip; devine descoperibilă ca test atunci când este marcată cu **atributul** potrivit, metadate scrise între paranteze drepte. [00:39:10](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2350s) `[Fact]` declară un singur test xUnit neparametrizat.

[Fișierul final de teste parametrizate](https://github.com/AntonC9018/uniCourse_csharp/blob/e4b890a75d5e014ab00227c27ff6a35965acb9a1/examples/tests_example/tests/Tests.cs) este o **stare ulterioară conexă**, nu Fact-ul temporar exact. Forma intermediară simplificată păstrează verificarea de depanare anterioară:

```csharp
using System.Diagnostics;
using Xunit;

public class Tests
{
    [Fact]
    public void Test1()
    {
        var config = CreateConfig();
        Debug.Assert(Helper.GetHumanAge_New(config, 0) == 0);
    }

    // CreateConfig is the factory described in the preceding sections.
}
```

Aceasta este o ilustrare de formă, cu fabrica omisă. `public void` declară o metodă accesibilă care nu întoarce nicio valoare. Clasa și atributul oferă structura de test; pentru verificarea comportamentului e nevoie în continuare de un corp cu sens.

1. [00:39:20](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2360s) Deschide exploratorul de teste din IDE.
2. Inspectează testul descoperit.
3. Face clic dreapta pe acel test și rulează-l singur.
4. [00:39:31](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2371s) Înregistrarea demonstrează că runner-ul îl execută.
5. [00:40:08](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2408s) Rularea se încheie cu succes.

**Descoperirea** înseamnă găsirea testelor care pot rula; este distinctă de reușita lor. [00:40:12](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2412s) Lecția arată și o problemă după încercarea unor versiuni mai noi de pachete: IDE-ul ar putea să nu mai vadă testele. Tratează aceasta ca pe o problemă demonstrată de unealtă/versiune, nu ca dovadă că fiecare versiune mai nouă eșuează. Versiunile fixe commituite identifică punctul de control al acestui exemplu; experimentarea cu altă configurare compatibilă rămâne sarcina elevului.

# Înlocuiește duplicarea cu un Theory

[00:40:23](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2423s) Același corp logic de test trebuie să ruleze cu multe seturi de date. Copierea corpului în `Test1`, `Test2` și așa mai departe duplică verificarea de comportament. [00:40:35](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2435s) Un **test parametrizat** reutilizează în schimb o singură metodă cu argumente. Modificarea propriu-zisă este **Fact → Theory**, cu unul sau mai multe atribute `InlineData`; InlineData nu înlocuiește singur Fact.

[00:40:46](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2446s) **Theory** marchează testul bazat pe date. **InlineData** furnizează argumentele pentru o invocare: aici, vârsta de intrare și ieșirea așteptată. [00:41:24](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2484s) Metoda primește acele valori ca parametri, păstrând intrarea și răspunsul explicite pentru fiecare caz.

[Testul xUnit commituit](https://github.com/AntonC9018/uniCourse_csharp/blob/e4b890a75d5e014ab00227c27ff6a35965acb9a1/examples/tests_example/tests/Tests.cs) păstrează această structură. Un exemplu simplificat, cu fabrica de configurație omisă, este:

```csharp
[Theory]
[InlineData(0, 0)]
[InlineData(1, 15)]
[InlineData(2, 23)]
public void Test(int input, int expected)
{
    var config = CreateConfig();
    var actual = Helper.GetHumanAge_New(config, input);
    Assert.Equal(expected, actual);
}
```

Fiecare rând InlineData furnizează valori pentru `input` și `expected` în acea ordine. Corpul calculează `actual` din implementarea testată.

[00:41:34](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2494s) Ajutoarele **Assert** ale xUnit sunt aserțiuni de test, verificări concepute pentru a raporta un test eșuat. [00:41:43](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2503s) **Assert.Equal** primește **prima valoarea așteptată, apoi valoarea efectivă** și raportează o nepotrivire cu ambele valori. Aceasta oferă o ieșire de test mai utilă decât o simplă condiție de depanare.

[00:41:50](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2510s) Testul inițial de învelire nu mai este necesar odată ce metoda parametrizată acoperă aceleași cazuri, deci îl elimină. [00:41:55](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2515s) Elimină importurile nefolosite după trecerea de la aserțiunile de depanare. Fișierul final commituit conține în continuare un import nefolosit `System.Diagnostics`; este o rămășiță inofensivă, nu o dependență a lui Assert.Equal. Exemplul didactic scurtat îl omite.

# Inspectează cele nouă cazuri de date

[00:42:01](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2521s) Adaugă perechile intrare-ieșire alese ca rânduri InlineData. Acestea sunt **nouă cazuri selectate**, nu orice intrare întreagă posibilă. [Rândurile de date commituite](https://github.com/AntonC9018/uniCourse_csharp/blob/e4b890a75d5e014ab00227c27ff6a35965acb9a1/examples/tests_example/tests/Tests.cs) sunt valorile autoritative; exemplul compact următor le include pe toate:

```csharp
[Theory]
[InlineData(0, 0)]
[InlineData(1, 15)]
[InlineData(2, 23)]
[InlineData(3, 26)]
[InlineData(4, 29)]
[InlineData(6, 35)]
[InlineData(7, 38)]
[InlineData(8, 40)]
[InlineData(15, 54)]
// Attached to Test(int input, int expected), shown above.
```

[00:42:10](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2530s) Compilează proiectul și inspectează din nou descoperirea. [00:42:22](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2542s) Exploratorul de teste arată nouă cazuri separate, deși există o singură metodă parametrizată. Un rând înseamnă o mulțime de intrare, o invocare și un rezultat raportat independent.

[00:42:45](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2565s) Perechile includ **0 → 0** și **8 → 40**. Formularea abreviată din înregistrare în jurul lui 40 nu trebuie să devină afirmația că intrarea 40 așteaptă ieșirea 40; nu există un asemenea rând aici. [00:42:52](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2572s) Ordonarea alfabetică din explorator poate părea și ciudată pentru numere. Ordinea textuală și cea numerică sunt diferite: o etichetă de afișare care începe cu „15” se poate ordona înaintea uneia care începe cu „2”. Aceasta schimbă ordinea de afișare, nu ce verifică fiecare test.

<details>
<summary>Cum poate o singură metodă de test să apară ca nouă teste?</summary>

Theory furnizează corpul comun. Fiecare atribut InlineData oferă o mulțime separată de argumente. Descoperirea extinde acele rânduri în cazuri care pot rula și fi raportate independent.

</details>


# Schimbă un coeficient pentru a demonstra că testele eșuează

[00:43:01](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2581s) Schimbă intenționat un coeficient din regulile implementării și rulează suita. Eșecurile arată că testele verifică comportamentul observabil, nu că trec fără să exerseze conversia. Aceasta este o auto-verificare utilă după introducerea unei suite.

[00:43:32](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2612s) Demonstrarea schimbă ultimul coeficient în 8, ca să localizeze care cazuri depind de el. Cuvintele rosite „doar ultimul” sunt imediat calificate în transcriere: **vârstele 8 și 15 ajung ambele la acel interval**. Cu cele nouă rânduri commituite și reguli altfel neschimbate, ambele așteptări ar fi în dezacord; nu este corect să afirmi că exact unul trebuie să eșueze.

[Configurația și așteptările finale commituite](https://github.com/AntonC9018/uniCourse_csharp/blob/e4b890a75d5e014ab00227c27ff6a35965acb9a1/examples/tests_example/tests/Tests.cs) sunt **referința neschimbată**, nu o copie salvată a mutației deliberate. Acest experiment simplificat arată schimbarea:

```csharp
// Reference tail:
new Helper.Interval(Length: 99999, Coefficient: 2);

// Deliberate mutation for the demonstration:
new Helper.Interval(Length: 99999, Coefficient: 8);
```

La vârsta 8 rezultatul mutat ar fi `38 + 1 * 8 = 46` în loc de 40. La vârsta 15 ar fi `38 + 8 * 8 = 102` în loc de 54. Cazurile anterioare rămân sub coadă și nu folosesc coeficientul ei. Raportul de test ajută deci să identifici regula afectată. Restaurează coeficientul de referință după experiment dacă păstrarea comportamentului original este scopul.

# Testează mai multe componente împreună

[00:43:47](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2627s) Un **test de integrare** exercită mai multe componente care lucrează împreună, nu doar o funcție izolată. [00:44:04](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2644s) El poate testa un sistem întreg asamblat, precum și rezultatul imediat al unei interacțiuni.

[00:43:55](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2635s) Exemplul concret descris aici este o călătorie dus-întors a unui program de evenimente:

1. Citește dintr-un fișier un program de evenimente serializat.
2. **Parsează** reprezentarea stocată, interpretând-o ca date structurate.
3. Reconstituie obiectul de program de evenimente.
4. **Serializează** acel obiect, transformându-l înapoi într-o reprezentare stocată.
5. Compară cele două documente **JSON**, reprezentări structurate de date, pentru echivalență.

Aceasta verifică compoziția citirii, parsării, reconstruirii și serializării. Nu impune spații albe sau formatări de proprietăți identice, dacă comparația este o echivalență semantică.

Niciun commit sursă deținut pentru testul exact de program de evenimente al acelei alte aplicații nu a fost disponibil în istoricul lecției inspectat. [Starea video de la 00:43:55](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2635s) este sursa sa; aceasta este o reconstrucție conceptuală, nu cod executabil din acea aplicație:

```text
file JSON → parsed representation → schedule object → JSON
compare original and reconstructed JSON for equivalence
```

Testarea de integrare este introdusă ca un nivel mai larg de verificare; exemplul cu vârsta pisicii rămâne implementarea concretă de testare unitară.

# Automatizează ieșirea așteptată cu snapshot-uri

[00:44:22](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2662s) Mai devreme, răspunsurile așteptate se obțineau rulând funcția de referință, copiind ieșirea și salvând-o. Un **test snapshot** automatizează acest flux: un **snapshot** este o ieșire salvată folosită ca referință de comparație. [00:44:32](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2672s) Furnizează intrarea; la prima rulare, frameworkul generează ieșirea și o scrie într-un fișier separat. Rezultatul generat tot trebuie revizuit înainte de a merita încredere.

[00:44:41](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2681s) Instructorul numește testele snapshot un instrument deosebit de valoros și spune că le folosește în tot proiectul său. Aceasta explică alegerea demonstrației; este judecata lui despre utilitate, nu o clasificare universală a tehnicilor de testare.

[00:45:00](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2700s) Exemplul celeilalte aplicații verifică parsarea pentru un jurnal online. **HTML** este formatul de documente web citit. [00:45:18](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2718s) Inițial exemplul de ieșire așteptată nu există; prima rulare de procesare îl creează, iar o persoană verifică dacă pare rezonabil.

[00:45:31](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2731s) Testul încarcă o pagină de lecție, extrage date prin funcția de parsare, pune datele extrase într-o reprezentare de rezultat/formular și face snapshot-ul ei. [00:45:46](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2746s) Forma salvată este asemănătoare JSON-ului, nu JSON strict, accentuând lizibilitatea pentru revizuire. Un snapshot nu trebuie să respecte el însuși sintaxa JSON ca să fie o referință utilă.

Sursa exactă a jurnalului online nu se află în fișierele de lecție deținute inspectate; aceste stări video sunt dovada. Un flux simplificat alături demonstrației de la [00:45:31](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2731s) este:

```text
lesson HTML
  → extraction function
  → extracted lesson data / result form
  → readable saved snapshot
```

Nota nu substituie un test snapshot fără legătură din depozit pentru codul demonstrat și nu invocă un commit sursă imutabil indisponibil.

# Revizuiește o diferență de snapshot

[00:46:00](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2760s) Dacă ieșirea salvată și cea nou calculată nu coincid, testul snapshot raportează o **diferență**, o afișare a nepotrivirilor dintre ele, în loc să treacă tăcut. [00:46:23](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2783s) Rularea testului demonstrat deschide o fereastră de comparație care arată linia modificată. [00:46:33](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2793s) Nepotrivirea concretă este **ieșire nouă 3 față de ieșire stocată 6**.

[00:46:40](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2800s) Alege care versiune este corectă. **Aprobarea** înseamnă acceptarea explicită a ieșirii revizuite ca referință; nu este o demonstrație automată că programul are dreptate.

1. Inspectează alături ieșirea efectivă și snapshot-ul stocat.
2. [00:46:54](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2814s) Dacă valoarea stocată 6 este corectă, repară codul astfel încât să producă acea valoare.
3. Dacă noua valoare 3 este corectă, confirm-o ca nouă referință.
4. [00:47:03](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2823s) Rulează din nou același test: după confirmarea noii referințe, demonstrația trece.

[Starea video de la 00:46:33](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2793s) este sursa pentru nepotrivirea exactă a acelei alte aplicații. O reconstrucție compactă a comparației afișate este:

```diff
- stored: 6
+ received: 3
```

Cuvintele „stored” și „received” sunt etichete explicative aici, nu o linie exactă de fișier. Aprobarea unei schimbări doar pentru a obține un rezultat verde ar pierde valoarea testului; decizia trebuie să urmeze comportamentul intenționat.

<details>
<summary>Ar trebui acceptat orice snapshot nou generat?</summary>

Nu. Revizuiește-l față de comportamentul intenționat. O ieșire nouă corectă poate înlocui referința stocată; una incorectă înseamnă că trebuie reparat codul. Aprobarea înregistrează acea judecată pentru comparațiile viitoare.

</details>

# Aprobă primul snapshot și repetă testul

[00:47:12](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2832s) Fără un fișier de snapshot, prima rulare raportează că referința lipsește și creează un candidat. [00:47:25](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2845s) Revizuiește candidatul și confirmă-l doar când conținutul său este corect.

1. Rulează testul cu o intrare aleasă.
2. Citește ieșirea nou generată.
3. Aprobă referința corectă.
4. [00:47:33](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2853s) Rulează din nou: compară rezultatul curent cu snapshot-ul stocat.
5. [00:47:38](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2858s) Cu intrare neschimbată și cod neschimbat, ieșirea demonstrată coincide și testul trece.
6. [00:47:51](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2871s) Schimbă intrarea HTML și rulează din nou.
7. [00:48:05](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2885s) Inspectează varianta/diferența rezultată așa cum ai inspectat ieșirea anterioară.

Rularea finală este discutată ca o comparație cu așteptarea existentă a intrării, nu ca o variantă nouă aprobată automat. Rezultatul ei depinde de intrarea HTML aleasă. Dacă schimbarea acelei intrări schimbă ieșirea intenționată, modificarea trebuie să se reflecte în așteptarea revizuită. Nicio așteptare nu ar trebui să se actualizeze singură fără aprobare.

[Stările video de la 00:47:25](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2845s) și [00:48:05](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2885s) sunt sursa pentru acest flux final; codul exact de test al celeilalte aplicații nu a fost disponibil ca fișier imutabil inspectat. O reprezentare simplificată este:

```text
first run: input → candidate output → review → approved snapshot
later run: same input → current output → compare with approved snapshot
changed input: changed output → inspect diff → approve or fix
```

[00:48:25](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2905s) Aceeași metodă de inspecție funcționează când ai schimbat intenționat intrarea de test: examinează ieșirea efectivă și cea stocată, decide care reprezintă comportamentul intenționat și păstrează acea decizie ca referință nouă sau păstrată.

Testarea snapshot este demonstrată aici, în timp ce instalarea ei, sursele complete ale aplicației și politicile de producție pentru date snapshot stabile rămân în afara acestei lecții. Limita esențială este comună cu așteptările numerice generate: o bază aprobată protejează comportamentul cunoscut, inclusiv orice greșeală aprobată din greșeală.

## Istoricul modificărilor de cod

Șabloanele console intermediare, bucla de afișare, aritmetica provizorie, editarea nereușită cu început inclusiv, Fact-ul temporar și ferestrele de snapshot ale celeilalte aplicații sunt stări video, nu capturi exacte de cod în istoricul Git inspectat. Cele două puncte de control imutabile relevante din depozit sunt:

- [b333fe75 — verificări manuale și implementare bazată pe intervale](https://github.com/AntonC9018/uniCourse_csharp/commit/b333fe75bfe90c7d2c965c1a57f477497887e5e2): proprietăți partajate, referință de bibliotecă, intrări de soluție, ajutor configurabil și nouă verificări manuale; ajutorul vechi păstrat este experimental și diferă de regula verificată.
- [e4b890a7 — teste xUnit](https://github.com/AntonC9018/uniCourse_csharp/commit/e4b890a75d5e014ab00227c27ff6a35965acb9a1): proiectul de teste devine bibliotecă cu referințe de pachete, iar un Theory cu nouă rânduri InlineData folosește Assert.Equal pe ajutorul generalizat.

Schimbarea aserțiunii în acele puncte de control, redusă la liniile esențiale:

```csharp
// b333fe75: manual debug check
Debug.Assert(actual == expected, $"Output for {input} is {actual}, expected {expected}");
// e4b890a7: xUnit assertion inside the data-driven test
Assert.Equal(expected, actual);
```

