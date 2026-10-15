> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Construirea unui meniu care poate evolua

[00:00:00](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=0s) Construim un meniu de consolă care permite utilizatorului să aleagă una dintre trei operații mici. Exemplul construit crește în trei etape: acțiuni numerotate, intrări reordonabile memorate ca date și intrări care memorează chiar funcțiile. Obiectivul este să păstrăm stabil fluxul programului, schimbând ce oferă meniul.

Vocabularul folosit pe parcurs este:

- **Funcție sau metodă**: o porțiune numită de cod executabil. Un **parametru** îi furnizează o intrare; o **valoare de returnare** furnizează rezultatul. Metodele **statice** nu necesită o instanță a clasei care le conține; **void** înseamnă că nu există valoare de returnare.
- **Enum**: un tip cu constante numite care reprezintă alegeri. Un **membru enum** numește o alegere; o **conversie explicită** (*cast*) transformă explicit o valoare în alt tip.
- **Dispecerizare** (*dispatch*): alegerea și apelarea unei implementări dintr-o valoare de acțiune. Un **switch** alege o ramură după acea valoare; **Debug.Fail** raportează o stare neașteptată infrastructurii de depanare.
- **Analiză** (*parsing*): interpretarea unui text ca valoare, de exemplu un întreg. **TryParse** raportează succesul sau eșecul; un **parametru out** primește rezultatul. **Validarea** verifică dacă o valoare analizată cu succes este permisă.
- **Buclă de citire**: repetă o cerere până sosește o intrare acceptabilă. **continue** începe următoarea iterație; **break** iese din buclă. O **funcție ajutătoare** (*helper*) extrage o sarcină repetată într-o funcție reutilizabilă.
- **Listă sau tablou**, împreună o **colecție**: un grup ordonat de intrări. Un **index** este o poziție numerotată de la zero. **Count** al unei liste, sau **Length** al unui tablou, dă numărul de intrări.
- **Clasă, obiect, câmp și proprietate**: o clasă definește un fel de obiect, un obiect este o instanță, un câmp memorează o parte din datele sale, iar o proprietate expune o valoare printr-un membru. Aici un obiect **Option** grupează o etichetă, o descriere și o acțiune.
- **Spațiu de nume și directivă using**: un spațiu de nume grupează numele de tipuri; o directivă using permite codului să se refere la acele tipuri fără a repeta prefixul spațiului de nume.
- **Adnotări nullable și referințe**: null înseamnă o referință absentă. Un semn întrebător în string? permite o valoare de șir absentă. Operatorul „anulează-null” ! suprimă un avertisment nullable fără să modifice valoarea. Un parametru ref oferă unei funcții acces la starea transmisă prin referință.
- **nameof**: un operator de timp de compilare care produce numele unui simbol ca text. **==** compară șirurile după conținut. **Reflexia** inspectează metadatele programului la timp de execuție; este menționată, dar nu implementată.
- **Refactorizare**: schimbarea organizării codului păstrând comportamentul intenționat. **Înregistrarea** completează colecția meniului. **Configurația** decide ce intrări există. Pasarea acestei configurații în meniu separă conținutul de fluxul de control și reduce **cuplajul**, adică dependențele dintre părți.
- **public și private**: modificatori de acces care controlează unde poate fi numit un membru. **Încapsularea** ascunde detaliile de implementare în spatele unei interfețe publice mici.
- **Compilator**, **IL**, **bytecode** și **DLL**: compilatorul traduce sursa în IL (*Intermediate Language*), o reprezentare de instrucțiuni de nivel mai jos; bytecode-ul codifică numeric asemenea instrucțiuni. DLL-ul .NET compilat (*Dynamic Link Library*) conține IL și metadate. **dotnet build** compilează proiectul; **dotnet run** compilează după cum este necesar și pornește rezultatul.
- **Runtime**, **JIT**, **cod de mașină**, **arhitectură de procesor** și **adresă**: runtime-ul execută programele .NET; compilatorul său just-in-time traduce IL-ul în instrucțiuni pentru procesorul real. O arhitectură de procesor definește setul său de instrucțiuni și codificările. O adresă identifică o locație în memoria procesului. **SharpLab** afișează rezultatul compilării, inclusiv IL-ul și listări de asamblare lizibile.
- **Funcție locală**, **captură**, **lowering** și **mangling de nume**: o funcție locală este declarată în interiorul altei funcții; captura îi oferă acces la variabilele din jur. Lowering-ul rescrie constructele din sursă în metode și stare generate. Mangling-ul de nume oferă membrilor generați nume interne distincte.
- **Delegate** și **Action**: un delegate este un obiect invocabil tipat care se referă la o metodă și, când este necesar, la starea țintă. Tipul încorporat Action nu ia argumente și nu întoarce nimic. O **conversie a grupului de metode** transformă un nume de metodă folosit ca valoare într-un delegate compatibil. **Umbrirea** face ca o declarație mai apropiată să aibă prioritate față de alt nume. **Stocarea în cache** (*caching*) reutilizează un obiect creat anterior.
- **Polimorfism**: un singur punct de apel poate executa implementări diferite, alese la timp de execuție. Această lecție îl demonstrează printr-un delegate memorat într-o variabilă.

Pentru practici anterioare de citire și enum, vezi [laboratorul de citire din consolă](../../../labs/1_basic/08_input.md) și [laboratorul de enum](../../../labs/1_basic/09_enum.md) din acest depozit. Acestea sunt exerciții preliminare, nu versiuni salvate ale acestui meniu.

[00:00:05](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=5s) Inițial utilizatorul introduce un număr. [00:00:11](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=11s) Mai târziu, ordinea afișată poate diferi de ordinea în care au fost scrise implementările. [00:00:16](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=16s) În cele din urmă, intrările disponibile pot fi configurate în altă parte. Un enum furnizează mai întâi identificatori de acțiuni; [00:00:26](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=26s) apoi o colecție traduce un index într-un identificator; [00:00:43](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=43s) la final, delegate-urile înlocuiesc identificatorii cu funcții invocabile. [00:01:00](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=60s) O valoare de funcție memorată spune atât ce reprezintă intrarea, cât și cum se execută.

[00:01:03](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=63s) Există doar trei operații, așa că întregul tipar rămâne vizibil:

- [00:01:12](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=72s) Addition cere două numere întregi și afișează suma lor.
- [00:01:14](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=74s) Greeting cere un nume și salută persoana respectivă.
- [00:01:19](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=79s) Generarea unui număr aleator afișează o valoare aleatoare.

[00:01:23](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=83s) Meniul împachetează trei programe mai mici într-unul singur și decide care rulează. **Domeniu:** [00:01:33](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=93s) toate operațiile sunt funcții din același program în execuție. Meniul nu pornește procese separate.

Sursa meniului nu se găsește printre fișierele de sursă ale acestei clonări, iar căile de sursă candidate inspectate nu au istorie locală. Mostrele de mai jos sunt reconstrucții scurte ale unor stări din videoclipul legat, nu afirmații despre sursa exactă comitată. Traseul final urmează stările înregistrate, în loc să atașeze commituri de exerciții fără legătură.

## Denumirea acțiunilor și difuzarea cu un enum

[00:01:45](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=105s) O selecție trebuie mai întâi transformată într-o valoare a programului. Un **enum** reprezintă o mulțime fixă de alegeri, cu constante numite. [00:01:57](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=117s) Prima stare de cod declară Addition, Greeting și RandomNumber. Fără atribuiri explicite, valorile lor întregi încep de la zero și cresc cu unu:

```csharp
enum Action
{
    Addition,      // 0
    Greeting,      // 1
    RandomNumber   // 2
}
```

Aici, Action este enum-ul propriu al lecției; va fi eliminat mai târziu, pentru a expune delegate-ul încorporat cu același nume.

[00:02:13](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=133s) Separăm programul în trei responsabilități:

1. Numim acțiunile disponibile cu enum-ul.
2. Citim alegerea utilizatorului și o transformăm într-o valoare de acțiune.
3. Executăm funcția asociată acelei acțiuni.

[00:02:24](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=144s) **Difuzarea** înseamnă selectarea implementării care trebuie apelată. Atât o succesiune de instrucțiuni if, cât și un switch pot face acest lucru. [00:02:33](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=153s) Codul introduce ExecuteAction cu acțiunea aleasă drept **parametru**, adică o valoare de intrare transmisă unei funcții. [00:02:45](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=165s) Un switch folosește acea valoare pentru a alege funcția corespunzătoare:

```csharp
static void ExecuteAction(Action action)
{
    switch (action)
    {
        case Action.Addition: Addition(); break;
        case Action.Greeting: Greeting(); break;
        case Action.RandomNumber: RandomNumber(); break;
        default: Debug.Fail("Unknown action"); break;
    }
}
```

[00:03:05](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=185s) O variabilă de tip enum poate conține un număr de bază pentru care nu există niciun membru declarat. Ramura implicită tratează această valoare neașteptată ca pe o eroare. **Debug.Fail** raportează presupunerea eșuată infrastructurii de depanare; nu este validarea intrării utilizatorului și nici o excepție garantată în fiecare versiune compilată. [00:03:15](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=195s) Fiecare ramură numită are nevoie de propria funcție de operație. [00:03:37](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=217s) Deoarece switch-ul face referire la Debug.Fail, mutăm importul spațiului său de nume în partea de sus a fișierului. Un **spațiu de nume** grupează nume; această **directivă using** permite codului să scrie Debug în loc de System.Diagnostics.Debug:

```csharp
using System.Diagnostics;
```

## Extragerea citirii reutilizabile de numere întregi

[00:03:44](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=224s) Implementăm mai întâi Addition. Aceasta solicită un număr, citește text din consolă și încearcă să interpreteze acel text ca un int. **Analiza** (*parsing*) este această conversie din text într-o valoare tipizată; **int.TryParse** întoarce un indicator de succes și scrie numărul întreg prin **parametrul său out**.

[00:03:53](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=233s) Bucla de citire inițială reîncearcă la eșecul analizei. [00:04:04](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=244s) Declarăm rezultatul în afara buclei, ca să rămână în domeniu și după aceasta, și o întrerupem imediat ce analiza reușește. În această stare video simplificată, continue reia bucla, iar break o părăsește:

```csharp
int a;
while (true)
{
    Console.Write("Enter A: ");
    if (!int.TryParse(Console.ReadLine(), out a))
        continue;
    break;
}
// a is available here
```

**Domeniu:** [00:03:56](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=236s) acest tipar de reîncercare a fost predat mai devreme; [00:04:51](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=291s) lecția dedicată citirii oferă explicația detaliată. Aici motivul revizitării este reutilizarea.

[00:04:12](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=252s) Aceeași logică este necesară pentru A și B. Extragem o **funcție ajutătoare** (*helper*), adică o funcție care îndeplinește o sarcină repetată. ReadIntIn primește numele de afișare al variabilei ca **parametru** de tip șir. [00:04:27](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=267s) Afișează acel parametru în locul unui A scris direct și întoarce rezultatul analizat cu succes:

```csharp
static int ReadIntIn(string variableName)
{
    int result;
    while (true)
    {
        Console.Write("Enter " + variableName + ": ");
        if (!int.TryParse(Console.ReadLine(), out result))
            continue;
        break;
    }
    return result;
}
```

Tipul de returnare int spune că funcția furnizează un rezultat întreg. Instrucțiunea return transmite acea valoare apelantului.

[00:04:40](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=280s) Înlocuim ambele blocuri de citire duplicate cu apeluri la funcția ajutătoare. [00:04:56](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=296s) Addition se poate apoi concentra pe a + b și pe afișarea răspunsului:

```csharp
static void Addition()
{
    int a = ReadIntIn("A");
    int b = ReadIntIn("B");
    Console.WriteLine(a + b);
}
```

## Finalizarea acțiunilor Greeting și RandomNumber

[00:05:03](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=303s) Greeting este o metodă **statică**, deci apelarea ei nu necesită o instanță a clasei care o conține. Tipul său de returnare **void** înseamnă că nu produce valoare de returnare: munca ei este interacțiunea cu consola. Parantezele goale precizează că nu ia argumente.

```csharp
static void Greeting()
{
    Console.Write("Enter your name: ");
    string? name = Console.ReadLine();
    Console.WriteLine("Hello, " + name);
}
```

Această stare video scurtată folosește o **adnotare nullable**, adică ? din string?, pentru a permite null, o referință absentă întoarsă când nu există nicio linie de intrare. Numele introduse normal sunt șiruri.

[00:05:17](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=317s) Generarea unui număr aleator are nevoie de un obiect care produce valori aleatoare. [00:05:33](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=333s) Random.Shared este o **proprietate** statică, adică un membru care expune o valoare, și oferă acces la un generator partajat. Apelăm metoda sa Next pentru a obține întregul:

```csharp
static void RandomNumber()
{
    Console.WriteLine(Random.Shared.Next());
}
```

Proprietatea furnizează generatorul; Next face generarea. Toate cele trei funcții de acțiune încap acum în ramurile de difuzare.

## Citirea și validarea unei alegeri numerice

[00:05:42](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=342s) Revenim la fluxul principal și citim numărul opțiunii. [00:05:56](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=356s) Prima mapare este fixă: 0 înseamnă Addition, 1 înseamnă Greeting și 2 înseamnă RandomNumber.

[00:06:09](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=369s) Citirea unui întreg este doar prima verificare. **Validarea** întreabă apoi dacă acel număr este permis. [00:06:11](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=371s) Dacă este mai mic decât zero sau mai mare decât doi, afișăm o eroare și repetăm blocul de citire.

[00:06:28](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=388s) În această etapă intermediară, numărul și acțiunea selectată sunt variabile separate; variabila de acțiune este declarată lângă începutul metodei. [00:06:40](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=400s) Interpretăm numărul ca enum abia după validare. [00:06:54](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=414s) Repetarea citirii până când valoarea este între 0 și 2 garantează că orice valoare care ajunge la conversie este validă.

[00:07:02](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=422s) O **conversie explicită** (*cast*), scrisă cu tipul între paranteze, transformă explicit întregul în enum. Aceasta este sigură pentru acest enum anume, deoarece cei trei membri ai săi sunt exact 0, 1 și 2. O conversie explicită nu dovedește singură că există un membru enum.

[00:07:11](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=431s) Extragem bucla de validare într-o metodă statică care întoarce acțiunea selectată. Starea video rezultată, scurtată, este:

```csharp
static Action ReadAction()
{
    while (true)
    {
        Console.WriteLine("0: Addition, 1: Greeting, 2: RandomNumber");
        int number = ReadIntIn("option");
        if (number < 0 || number > 2)
        {
            Console.WriteLine("Invalid option");
            continue;
        }
        return (Action)number;
    }
}
```

Condiția || înseamnă că oricare dintre cele două limite invalide este suficientă pentru a respinge numărul.

[00:07:23](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=443s) Main este acum lizibil: citește o acțiune și o difuzează. [00:07:31](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=451s) ExecuteAction apelează operația pe care utilizatorul a ales-o.

```csharp
Action action = ReadAction();
ExecuteAction(action);
```

## Acceptarea unui nume de opțiune înainte de analiza unui număr

[00:07:39](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=459s) Un utilizator nu poate tasta un obiect funcție în consolă. El furnizează text, pe care programul îl interpretează ca o selecție. [00:07:47](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=467s) Acel text ar putea desemna o funcție prin numărul sau prin numele ei. **Domeniu:** [00:07:54](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=474s) versiunea curentă acceptă intenționat doar un număr.

[00:07:58](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=478s) Un apel direct la nivel de sursă este rezolvat prin programul compilat; un șir de intrare arbitrar nu devine automat un apel de metodă C#. Avem nevoie de o mapare explicită. Această afirmație se aplică designului în curs de construire, nu oricărui mecanism posibil de invocare dinamică.

[00:08:19](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=499s) Extindem selecția ca să accepte și un nume de opțiune. [00:08:32](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=512s) Copiem funcția de citire într-o variantă care citește chiar ea șirul brut: un nume ar eșua la analiza imediată a întregului. [00:08:40](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=520s) Verificăm numele permise înainte de a încerca o conversie numerică. [00:08:51](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=531s) Fiecare șir care se potrivește întoarce acțiunea enum corespunzătoare:

```csharp
string? input = Console.ReadLine();
if (input == "Addition") return Action.Addition;
if (input == "Greeting") return Action.Greeting;
if (input == "RandomNumber") return Action.RandomNumber;
// No name matched: try the numeric path next.
```

Operatorul **==** compară conținutul șirurilor. Aceste comparații sunt sensibile la majuscule, așa că ortografia introdusă trebuie să corespundă etichetei acceptate.

**Domeniu:** [00:09:06](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=546s) **reflexia**, inspecția metadatelor programului la timp de execuție, ar putea ajuta la generalizarea unei căutări după nume. Videoclipul intenționat nu o implementează; comparațiile explicite fac acest pas comprehensibil.

## Păstrarea numelor sincronizate cu nameof

[00:09:21](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=561s) Șirurile de nume scrise direct pot diverge atunci când o metodă este redenumită. **nameof** este un operator de timp de compilare care furnizează numele unui simbol ca șir. [00:09:41](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=581s) Funcționează și pe numele metodelor. Înlocuim etichetele scrise cu numele metodelor corespunzătoare:

```csharp
if (input == nameof(Addition)) return Action.Addition;
if (input == nameof(Greeting)) return Action.Greeting;
if (input == nameof(RandomNumber)) return Action.RandomNumber;
```

Această stare video simplificată întoarce în continuare identificatori enum. nameof(Addition) produce "Addition"; nu invocă nici Addition și nu memorează funcția.

[00:09:59](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=599s) Demonstrația folosește intenționat un nume de simbol greșit, pentru ca nepotrivirea să devină o eroare de compilare. Un identificator nedefinit în nameof face compilarea să eșueze; un literal de șir arbitrar precum "WrongName" se compilează în continuare. Operația de redenumire a simbolurilor din editor actualizează împreună metoda și referințele sale nameof.

[00:10:17](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=617s) Programul compară textul introdus cu numele permise, în ordine. Verificarea conținutului șirului are loc în timp ce programul rulează. Înlocuirea efectuată de nameof are loc mai devreme, în timpul executării **dotnet build**, comanda care compilează proiectul.

[00:10:32](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=632s) După compilare, folosirea lui nameof este echivalentă cu scrierea literalului de șir rezultat. Leagă aceste etichete de simbolul redenumit, evitând copiile scrise manual în repetate ale numelui său. Nu impune ca orice etichetă posibilă de meniu din program să urmeze numele metodelor.

## Verificarea ambelor stiluri de intrare

[00:10:38](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=638s) Rulăm programul revizuit și verificăm traseul numeric. Alegerea lui 0 ajunge la adunare. O literă introdusă acolo unde se așteaptă un operand este respinsă; un întreg valid este acceptat și suma este afișată. Aceasta distinge suportul meniului pentru nume de intrarea exclusiv numerică cerută de Addition.

[00:10:56](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=656s) Rulăm din nou și introducem opțiunea de salut ca litere. Ea difuzează Greeting, cere un nume și salută numele introdus. Alegerea intrării cu număr aleator după numărul ei produce în continuare o valoare aleatoare. Ambele stiluri de intrare funcționează după schimbările legate de nume.

Acestea sunt rezultate observate în înregistrare, nu afirmația că fragmentele reconstruite au fost compilate sau rulate în această clonare.

## Identificarea listei de opțiuni dublate

[00:11:15](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=675s) Programul care funcționează are o problemă de întreținere: lista sa completă de opțiuni apare de două ori. [00:11:24](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=684s) Numele și acțiunile apar în codul de selecție, în timp ce mesajul afișat enumeră separat aceleași alegeri.

[00:11:42](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=702s) Adăugarea unei noi operații înseamnă adăugarea logicii sale de difuzare și selecție, apoi editarea șirului afișat, a punctuației și a numărului. Nimic din acel mesaj scris manual nu leagă semantic numărul afișat de acțiune.

[00:12:02](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=722s) Introducem Option, astfel încât informațiile care aparțin unei singure intrări de meniu să trăiască împreună. Aceasta este **refactorizare**: reorganizarea implementării pentru a păstra comportamentul de selecție intenționat și a face modificările viitoare mai ușoare.

## Reprezentarea intrărilor de meniu ca obiecte Option

[00:12:14](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=734s) O **clasă Option** definește datele pentru o intrare. Un **obiect** este o intrare concretă; **câmpurile** sale memorează numele și acțiunea. [00:12:26](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=746s) Adăugăm și o descriere, ca intrarea să se poată explica singură.

În această etapă, câmpul de acțiune conține în continuare enum-ul, nu o funcție. Starea înregistrată simplificată este:

```csharp
class Option
{
    public string Name = "";
    public string Description = "";
    public Action Action;
}
```

[00:12:35](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=755s) O singură buclă poate afișa indexul, numele și descrierea fiecărei intrări. [00:12:50](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=770s) Intrarea poate apoi potrivi fie numele, fie indexul, iar acea intrare furnizează acțiunea.

[00:12:57](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=777s) Creăm o singură colecție de obiecte Option, în loc să împrăștiem intrările în funcția de selecție. O **listă** este o colecție ordonată care poate crește; un **index** este o poziție care începe de la zero. [00:13:11](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=791s) O completăm cu numele, descrierea și acțiunea enum ale fiecărei intrări:

```csharp
List<Option> options = new()
{
    new() { Name = nameof(Greeting),
            Description = "Greet a user", Action = Action.Greeting },
    new() { Name = nameof(Addition),
            Description = "Add two integers", Action = Action.Addition },
    new() { Name = nameof(RandomNumber),
            Description = "Print a random number", Action = Action.RandomNumber }
};
```

List<Option> înseamnă o listă ale cărei intrări sunt obiecte Option. Expresiile new construiesc obiectele, iar acoladele le atribuie câmpurile. Mostra reordonată evidențiază următorul rezultat al lecției.

[00:13:26](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=806s) Indexul aparține acum poziției curente a intrării, nu numărului său enum. Reordonarea intrărilor schimbă numerele afișate, păstrând împreună fiecare etichetă și acțiune.

[00:13:36](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=816s) Trecem această colecție în funcția de citire. [00:13:44](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=824s) Parametrul ei furnizează opțiunile disponibile efective; selecția trebuie să rezulte din ele.

[00:13:51](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=831s) Înlocuim lista fixă din mesaj cu „Enter an option”. [00:14:03](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=843s) Parcurgem intrările și afișăm indexul, numele și descrierea:

```csharp
Console.WriteLine("Enter an option:");
for (int i = 0; i < options.Count; i++)
{
    Option option = options[i];
    Console.WriteLine(i + " or " + option.Name + ": "
                      + option.Description + ".");
}
```

Aici, Count este numărul de intrări; options[i] preia obiectul de la indexul i. **Domeniu:** [00:14:17](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=857s) punctul final afișat este intenționat. Punctuația și formatarea mai elaborate ale listei sunt amânate.

## Selectarea din colecția de opțiuni

[00:14:28](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=868s) Înlocuim comparațiile scrise manual între nume cu o buclă peste intrări. Un nume care se potrivește întoarce imediat acțiunea acelei intrări. [00:14:46](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=886s) Dacă bucla se termină fără potrivire, continuăm cu analiza întregului.

Folosim această procedură pentru fiecare alegere încercată:

1. Afișăm opțiunile disponibile și citim intrarea brută.
2. Căutăm un nume care se potrivește; dacă există, întoarcem acțiunea memorată.
3. Altfel, încercăm să analizăm intrarea ca întreg; reîncercăm dacă analiza eșuează.
4. Validăm acel întreg ca index de colecție; reîncercăm dacă este în afara limitelor.
5. Întoarcem acțiunea memorată în intrarea de la acel index.

[00:14:51](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=891s) Zero este cel mai mic index valid. [00:14:55](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=895s) Validarea protejează accesul la colecție față de o alegere invalidă. [00:14:57](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=897s) Respingem atât un index negativ, cât și unul mai mare sau egal cu dimensiunea colecției. Un **tablou** este o colecție ordonată de dimensiune fixă și numește acea dimensiune Length; o **listă** poate crește și folosește Count în această reconstrucție echivalentă.

[00:15:15](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=915s) Preluăm direct acțiunea memorată, eliminând conversia explicită de la index la enum. Starea video rezultată, scurtată, este:

```csharp
static Action ReadAction(List<Option> options)
{
    while (true)
    {
        for (int i = 0; i < options.Count; i++)
            Console.WriteLine(i + " or " + options[i].Name
                              + ": " + options[i].Description + ".");

        string? input = Console.ReadLine();
        foreach (Option option in options)
            if (option.Name == input)
                return option.Action;

        if (!int.TryParse(input, out int index))
        {
            Console.WriteLine("Invalid option");
            continue;
        }
        if (index < 0 || index >= options.Count)
        {
            Console.WriteLine("Invalid option");
            continue;
        }
        return options[index].Action;
    }
}
```

foreach vizitează fiecare obiect din colecție. return încheie imediat funcția de citire; nu execută acțiunea însăși.

[00:15:17](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=917s) După reordonare, adunarea se află pe poziția unu, nu zero. Indexul 1 preia intrarea Addition, chiar dacă valoarea sa enum rămâne 0. Exact de aceea o conversie explicită de la index la enum nu mai este potrivită.

[00:15:34](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=934s) Înregistrarea rulează din nou programul cu opțiunea de număr aleator introdusă ca litere. Potrivirea după nume întoarce imediat, așa că traseul de analiză numerică este sărit. Acțiunea aleasă este executată doar după ce selecția se încheie.

Ce se întâmplă dacă există trei intrări și utilizatorul introduce 3?

<details>
<summary>Răspuns</summary>

Indexurile valide sunt 0, 1 și 2. Deoarece 3 este egal cu Count, validarea îl respinge înainte de a accesa options[3]. Funcția de citire afișează o eroare și întreabă din nou.

</details>

## Mutarea înregistrării în afara lui Main

[00:15:48](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=948s) Colecția poate fi construită acum în afara lui Main. **Înregistrarea** înseamnă adăugarea intrărilor pe care programul le va oferi; **configurația** determină ce intrări sunt înregistrate. O clasă statică dedicată poate primi lista și să o completeze.

[00:16:12](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=972s) Mutăm adăugările în AddOptions, adăugând intrările una câte una. [00:16:35](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=995s) Main apelează acum această funcție de configurare. O stare video scurtată este:

```csharp
List<Option> options = new();
MenuOptions.AddOptions(options);
Action action = ReadAction(options);
ExecuteAction(action);

// In the separate configuration class:
public static void AddOptions(List<Option> options)
{
    options.Add(new Option {
        Name = "Greeting", Description = "Greet a user",
        Action = Action.Greeting
    });
    // Register the other entries in the same way.
}
```

Eticheta literală de aici ilustrează versiunea intermediară cu enum; etichetele nu trebuie să fie nume de metode.

[00:16:41](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1001s) Main nici nu decide ce intrări există, nici nu implementează căutarea unei intrări anume. [00:16:53](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1013s) De asemenea, deleagă execuția către ExecuteAction.

[00:17:05](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1025s) **Cuplajul** său, adică numărul detaliilor de care depinde, este mic: inițializarea colecției, citirea acțiunii selectate și executarea ei. Configurația este furnizată prin colecție, nu repetată în Main.

[00:17:17](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1037s) Main nu apelează niciodată direct implementările individuale ale acțiunilor. Aceasta sprijină **încapsularea**, ascunderea detaliilor de implementare în spatele unei interfețe mici. **public** permite codului exterior să numească un membru; **private** restrânge acel acces la tipul care îl declară. În această etapă, un difuzor separat ar putea avea în continuare nevoie de acces la implementări; delegate-urile vor elimina această dependență mai târziu.

[00:17:26](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1046s) Ca test, eliminăm o intrare înregistrată fără a edita Main. Ea dispare din meniul afișat. [00:17:34](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1054s) Doar configurația s-a schimbat; fluxul principal a rămas identic. Același design de selecție gestionează dimensiuni diferite ale colecției. O colecție goală nu oferă nicio alegere validă, așa că această funcție de citire cu reîncercări nu se poate încheia până când există o opțiune.

## Înlocuirea identificatorilor de acțiune cu funcții

[00:17:42](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1062s) Acțiunile enum au în continuare valori numerice de bază. După ce funcția de citire preia una, difuzorul este singura parte care o interpretează pentru a alege o funcție.

[00:18:00](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1080s) Ce-ar fi dacă fiecare intrare ar memora chiar acțiunea? Atunci numerele enum și maparea manuală de la enum la funcție ar putea dispărea. **Domeniu:** [00:18:22](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1102s) aceasta deschide întrebarea unei reprezentări diferite de identificatorii numerici actuali; nu implementează încă reprezentări noi arbitrare.

[00:18:34](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1114s) Extinderea meniului ar necesita atunci doar scrierea unei funcții și înregistrarea ei, fără a schimba un switch central. [00:18:45](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1125s) Chiar și o metodă private ar putea fi înregistrată de cod din interiorul clasei sale.

[00:19:11](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1151s) În aranjamentul curent, pentru a face o metodă nouă acțiune sunt necesare un membru enum și o ramură de difuzare. Dacă difuzorul se află în afara clasei metodei, acea metodă nu poate fi private și totuși apelată acolo după nume.

[00:19:49](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1189s) Trecerea unei referințe directe invocabile din locul în care este creată opțiunea elimină apelul exterior după nume. [00:19:54](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1194s) Un **delegate** este obiectul invocabil tipat care face acest lucru posibil: codul exterior invocă delegate-ul, în loc să numească metoda sa privată țintă. Regulile de acces se aplică și la crearea referinței.

[00:20:04](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1204s) Codul extern ar putea, la fel, să își înregistreze propriile acțiuni compatibile. Meniul central le-ar selecta și invoca fără a fi editat ca să le cunoască numele.

## Urmărirea codului sursă până la IL

**Domeniu:** [00:20:17](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1217s) înainte de implementarea valorilor de funcție, lecția face o incursiune la nivel mai jos, pentru a explica ce sunt funcțiile executabile.

[00:20:23](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1223s) **Compilatorul** traduce C# în **IL**, Intermediate Language: o reprezentare de instrucțiuni de nivel mai jos, codificată ca **bytecode**. [00:20:55](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1255s) Artefactul compilat conține instrucțiunile traduse, nu textul sursă original.

[00:21:02](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1262s) În această versiune .NET, codul se află într-o **DLL**, o Dynamic Link Library care conține IL și metadate. [00:21:11](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1271s) Opcodurile instrucțiunilor au codificări numerice; o listare IL este o prezentare lizibilă a acestei reprezentări de nivel mai jos.

[00:21:26](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1286s) **SharpLab** poate afișa IL-ul produs din sursă C# lipită. [00:21:31](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1291s) Înregistrarea inspectează clasa Program, metoda Main, apelurile la metodele programului și apelurile de afișare în consolă. Această intrare mică reconstruită ilustrează ce trebuie căutat în acea stare video:

```csharp
class Program
{
    static void Main()
    {
        SamOperations();
    }

    static void SamOperations()
    {
        Console.WriteLine("Hello");
    }
}
```

Relația importantă este o metodă ce conține un apel la altă metodă, nu o anumită ortografie a unui opcod generat.

**Domeniu:** [00:21:39](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1299s) memorarea instrucțiunilor IL nu este necesară pentru această lecție. [00:21:43](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1303s) Aceeași traducere din sursă în IL are loc când dotnet build compilează proiectul.

## Urmărirea IL-ului până la codul de mașină

[00:22:16](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1336s) Pornirea programului îl încarcă prin **runtime-ul** .NET, mecanismul responsabil de execuția codului .NET. **JIT**-ul său, adică compilatorul just-in-time, transformă IL-ul în **cod de mașină** pentru procesorul real. [00:22:21](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1341s) Codul de mașină este reprezentarea de instrucțiuni executabile a procesorului.

**Domeniu:** [00:22:30](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1350s) sintaxa asemănătoare asamblării, afișată de SharpLab, nu trebuie învățată. [00:22:37](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1357s) Urmărim în schimb traseul: sursă C# → IL în artefactul compilat → instrucțiuni specifice procesorului pentru execuție. Înregistrarea descrie acest lucru în jurul pornirii; în execuția JIT obișnuită, metodele sunt compilate de regulă când sunt necesare, nu neapărat toate dintr-o dată la pornire.

[00:22:46](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1366s) Mnemonicele de asamblare afișate sunt o vedere lizibilă pentru om a instrucțiunilor cu codificări numerice. [00:23:28](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1408s) **Corecție:** numerele de exemplu folosite în explicație sunt ilustrative, nu codificări reale de instrucțiuni.

[00:23:35](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1415s) O **arhitectură de procesor** definește ce instrucțiuni există și cum sunt ele codificate. [00:23:41](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1421s) .NET poate traduce același IL pentru diferite procesoare acceptate. Această portabilitate presupune un runtime și dependențe compatibile ale programului; nu este o garanție că orice program rulează pe orice mașină.

[00:23:51](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1431s) **dotnet run** vă permite să treceți de la sursă la un program în execuție fără să gestionați manual artefactul intermediar. [00:24:00](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1440s) Etapa IL există în continuare, chiar dacă comanda ascunde mare parte din acest flux.

[00:24:08](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1448s) Simplificarea utilă este că instrucțiunile executabile au codificări numerice și se află în memorie. [00:24:38](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1478s) Înainte ca o funcție să se execute, instrucțiunile sale trebuie să existe într-o formă pe care mașina să o poată executa. [00:24:47](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1487s) Procesorul preia acele instrucțiuni din memorie, nu din textul sursă C#.

## Înțelegerea adreselor funcțiilor

[00:25:00](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1500s) Într-un **proces** în execuție, adică o instanță a programului care rulează, funcțiile compilate au instrucțiuni executabile în memorie. O **adresă** identifică o locație acolo. [00:25:17](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1517s) Funcții diferite au propriile locații de cod.

[00:25:34](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1534s) Un nume de funcție de la nivelul sursei permite compilatorului să rezolve un apel. La nivelul mașinii, apelul transferă controlul către cod; nu caută în intrarea utilizatorului acel nume din sursă. [00:25:48](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1548s) Exemplul din înregistrare, „apelează funcția de la adresa 900”, ilustrează intrarea într-alt bloc de instrucțiuni. Adresa este ipotetică.

[00:25:59](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1559s) Și Console.WriteLine are cod de implementare furnizat de biblioteca standard. [00:26:08](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1568s) Un apel ajunge la acea implementare prin mecanismul de apel executabil, nu căutând textul „Console.WriteLine”.

[00:26:19](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1579s) Exemplul cu funcție locală plasează Main și SamOperations la adrese de cod conceptuale distincte. În această stare video reconstruită, funcția interioară folosește variabilele din jur:

```csharp
static void Main()
{
    int a = 2;
    int b = 3;
    void SamOperations()
    {
        Console.WriteLine(a + b);
    }
    SamOperations();
}
```

Păstrați conceptual imaginea adreselor. Un delegate este un obiect invocabil gestionat, nu un pointer întreg brut; apelurile compilate reale pot folosi indirectare sau optimizări. Lecția are nevoie de ideea de a ajunge indirect la cod, nu de garanția că fiecare apel este codificat identic.

## Separarea codului funcției locale de starea capturată

[00:26:48](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1608s) O **funcție locală** este declarată în interiorul altei funcții. Ea poate **captura** variabilele din jur, adică le folosește prin acces generat de compilator la starea lor. Distincția importantă este între codul ei executabil și starea care aparține unei invocații anumite a funcției din jur.

[00:26:56](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1616s) **Lowering-ul** rescrie constructele din sursă în metode și stare generate. În cazul ilustrat de apel direct, compilatorul poate transmite variabilele locale capturate printr-o structură generată, prin referință. O reconstrucție schematică, nu ieșirea exactă a compilatorului, este:

```csharp
struct CapturedValues
{
    public int a;
    public int b;
}

static void GeneratedSamOperations(ref CapturedValues values)
{
    Console.WriteLine(values.a + values.b);
}
```

ref înseamnă aici că metoda generată accesează starea transmisă prin referință, ceea ce îi permite să se refere la aceleași variabile capturate, nu la copii independente.

[00:27:11](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1631s) Implementarea generată este ascunsă de codul sursă exterior obișnuit. [00:27:21](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1641s) **Mangling-ul de nume** furnizează un nume intern generat, astfel încât două funcții locale cu același nume din sursă, aflate în metode diferite care le cuprind, să nu intre în coliziune.

[00:27:37](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1657s) Apelarea din nou a funcției care cuprinde nu necesită o copie nouă a codului executabil al funcției locale. [00:27:49](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1669s) Invocația ei primește sau accesează în schimb starea capturată corespunzătoare. [00:28:00](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1680s) Apelurile generate ajung în cele din urmă la cod executabil; [00:28:09](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1689s) locațiile de cod permit ca funcțiile să fie referite indirect.

Descrierea din videoclip, „devine statică”, corespunde transformării ilustrate, nu oricărei funcții locale. În funcție de capturi și de conversia în delegate, lowering-ul poate folosi altă stare generată și alte metode de instanță. Starea poate diferi de la un apel la altul chiar dacă implementarea metodei este reutilizată. Lecția nu cere învățarea fiecărei strategii de lowering.

## Pregătirea apelurilor indirecte

[00:28:23](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1703s) Dacă codul executabil poate fi atins indirect, o variabilă poate determina ce funcție va atinge un apel. Aceasta este temelia următoarei reprezentări a meniului.

**Domeniu:** [00:28:35](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1715s) termenul „polimorfism” este amânat intenționat până când exemplul funcțional cu delegate arată de ce contează.

## Folosirea delegate-ului încorporat Action

[00:28:39](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1719s) Ștergem enum-ul Action declarat local. [00:28:44](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1724s) Acea declarație îl **umbra** pe Action din biblioteca standard: avea prioritate în căutarea numelor din cod. [00:29:03](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1743s) După eliminare, Action se rezolvă la tipul de delegate încorporat.

[00:29:09](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1749s) Un **delegate** este un obiect invocabil tipat. Declarația lui Action încorporat este, conceptual:

```csharp
// Standard-library type, shown for explanation; do not redeclare it.
public delegate void Action();
```

[00:29:10](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1750s) **void** înseamnă că funcția referită nu întoarce nimic. Lista goală de parametri () înseamnă că nu acceptă argumente. Sunt proprietăți distincte; void nu înseamnă „fără parametri”.

Tipul câmpului se referă acum la o valoare de funcție, nu la un identificator enum. Schimbarea reconstruită este:

```csharp
class Option
{
    public string Name = "";
    public string Description = "";
    public Action Action = null!;
}
```

Substituentul null! folosește **operatorul „anulează-null”** ! pentru a suprima un avertisment nullable pentru această mostră scurtată bazată pe câmpuri; valoarea rămâne null. Fiecare intrare înregistrată trebuie să atribuie un delegate real înainte de a putea fi selectată și invocată.

[00:29:28](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1768s) Invocarea unui Action transferă execuția către metoda referită și revine la apelant când acea metodă se încheie. Modelul adreselor explică transferul, în timp ce C# gestionează obiectul invocabil:

```csharp
static void ExecuteAction(Action action)
{
    action();
}
```

Tipul parametrului stabilește ce semnătură de funcție este permisă. Nu mai există niciun switch care să transforme o valoare enum într-o funcție.

## Înregistrarea metodelor private ca delegate

[00:29:43](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1783s) Extragem implementările acțiunilor în MenuOptions. [00:29:57](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1797s) Le facem **private**, vizibile după nume doar din clasa lor, deoarece codul exterior al meniului invocă acum un delegate memorat.

[00:30:05](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1805s) Înlocuim fiecare atribuire cu valoare enum cu metoda însăși. Aceasta este o **conversie a grupului de metode**: un nume de metodă folosit ca valoare este transformat într-un delegate cu semnătură compatibilă. O stare video rezultată, scurtată, este:

```csharp
static class MenuOptions
{
    public static void AddOptions(List<Option> options)
    {
        options.Add(new Option {
            Name = nameof(Addition),
            Description = "Add two integers",
            Action = Addition
        });
        // Greeting and RandomNumber are registered in the same way.
    }

    private static void Addition()
    {
        int a = ReadIntIn("A");
        int b = ReadIntIn("B");
        Console.WriteLine(a + b);
    }

    // ReadIntIn and the other private operations live here too.
}
```

Observați că Action = Addition nu are paranteze. Ea memorează o referință invocabilă. Addition() ar executa imediat metoda, iar rezultatul ei void nu ar putea fi atribuit câmpului.

[00:30:11](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1811s) Obiectul delegate se referă la metoda țintă și, unde este cazul, la un obiect țintă. [00:30:20](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1820s) Aici țintele sunt metode statice, așa că nu este nevoie de o instanță a lui MenuOptions. Câmpul memorează o referință la un obiect delegate, nu un număr enum și nici o adresă brută expusă.

[00:30:26](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1826s) Înregistrarea discută reutilizarea obiectelor delegate pentru metode statice. [00:30:50](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1850s) **Cache-ul** înseamnă păstrarea și reutilizarea unui obiect deja creat. Compilatoarele moderne de C# pot pune în cache conversiile grupurilor de metode statice, evitând alocări repetate la un loc de conversie. Aceasta este o optimizare, nu o garanție la nivel de limbaj că există exact un obiect delegate pentru fiecare metodă în întregul program. Locuri de conversie diferite și alte forme de construcție pot produce obiecte distincte.

## Păstrarea etichetelor separate de acțiunile invocabile

[00:31:10](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1870s) Eticheta poate folosi în continuare **nameof**, operatorul de timp de compilare care produce nume:

```csharp
Name = nameof(Greeting),
Action = Greeting
```

[00:31:16](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1876s) nameof nu este o funcție de timp de execuție care primește un delegate. Rolul său de tip macro în această explicație este înlocuirea în timpul compilării: eticheta devine "Greeting", iar câmpul de acțiune primește referința invocabilă.

[00:31:29](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1889s) Chiar și o metodă statică care nu face nimic poate fi înregistrată dacă semnătura sa se potrivește cu Action. [00:31:36](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1896s) Numele din meniu nu trebuie să fie deloc numele metodei. În această alternativă reconstruită, el este o etichetă independentă, orientată către utilizator:

```csharp
options.Add(new Option {
    Name = "Do nothing",
    Description = "Return immediately",
    Action = DoesNothing
});

static void DoesNothing() { }
```

Eticheta controlează potrivirea intrării și afișarea. Delegate-ul controlează execuția. Schimbarea doar a etichetei nu schimbă metoda apelată.

## Invocarea acțiunii selectate prin polimorfism

[00:31:42](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1902s) Funcția de citire caută în continuare numele și validează indexele exact ca înainte. Ea întoarce câmpul de acțiune al intrării; doar reprezentarea acelui câmp și pasul de invocare s-au schimbat.

[00:31:49](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1909s) Rulăm versiunea cu delegate. Aceleași intrări de meniu apar, iar alegerea primei ajunge la operația referită de ea. Action însuși nu primește argumente; o operație precum Addition poate cere în continuare intrare și își poate apela propriii ajutori cu argumente.

[00:31:55](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1915s) Main păstrează aceeași succesiune. [00:32:02](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1922s) Valoarea selectată conține acum o referință invocabilă, nu un număr care așteaptă traducerea. Fluxul final reconstruit din videoclip este:

```csharp
List<Option> options = new();
MenuOptions.AddOptions(options);
Action selected = ReadAction(options);
ExecuteAction(selected);

static void ExecuteAction(Action action)
{
    action();
}
```

[00:32:11](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1931s) Această reprezentare oferă flexibilitate: acțiunile pot fi definite în altă parte și înregistrate fără a adăuga ramuri de difuzare. [00:32:22](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1942s) Clasa de configurare expune doar AddOptions apelantului său. [00:32:39](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1959s) Ar putea fi mutată într-un fișier separat, în timp ce implementările sale rămân private.

[00:32:44](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1964s) Conținutul variabilei selectate determină ce rulează. [00:32:53](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1973s) Locul de invocare nu știe care funcție concretă a fost înregistrată și apoi aleasă de utilizator.

[00:33:01](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1981s) Acesta este **polimorfismul**: un singur loc de apel poate rula implementări diferite, alese la timp de execuție. [00:33:05](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1985s) Aici, action() invocă obiectul invocabil memorat în parametrul său. El poate ajunge la Addition, Greeting, RandomNumber sau la o metodă compatibilă nou înregistrată. Acesta este exemplul lecției bazat pe delegate pentru ideea mai largă.

Dacă introduceți o nouă operație private static void în MenuOptions și o înregistrați, ce părți ale meniului trebuie schimbate?

<details>
<summary>Răspuns</summary>

Scriem metoda și adăugăm un Option care conține eticheta, descrierea și delegate-ul. Bucla de afișare, căutarea după nume, validarea indicilor, succesiunea din Main și invocarea delegate-ului nu necesită nicio ramură nouă. Clasa creează referința acolo unde metoda private este accesibilă; codul exterior folosește acea referință.

</details>

**Rezumat al domeniului:** lecția rămâne într-un singur proces de consolă, cu acțiuni de meniu void și fără parametri. Mecanica anterioară a buclei de citire este reutilizată, nu repredată. Reflexia, memorarea opcodurilor IL, sintaxa detaliată a asamblării, formatarea elaborată a meniului și toate transformările posibile ale funcțiilor locale sunt în afara domeniului său de implementare.

## Istoricul modificărilor de cod

Nu a fost găsit niciun commit exact al sursei meniului în clonarea deținută. Următoarele linkuri video stabile identifică stările construcției, de la cea mai veche la cea mai recentă; mostrele lor simplificate apar în secțiile corespunzătoare de mai sus.

- [00:01:57 — identificatori enum](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=117s): declarăm Addition, Greeting și RandomNumber, apoi le difuzăm prin ExecuteAction.
- [00:04:12 — citire reutilizabilă de întregi](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=252s): extragem ReadIntIn și îl folosim pentru ambii operanzi.
- [00:07:11 — funcție de citire numerică](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=431s): izolăm validarea intervalului și întoarcem valoarea enum selectată.
- [00:08:19 — intrare după nume](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=499s): verificăm numele acceptate înainte de a recurge la analiza întregului.
- [00:09:21 — etichete derivate din simboluri](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=561s): folosim nameof pentru a sincroniza șirurile de nume cu simbolurile.
- [00:12:14 — intrări ca date](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=734s): introducem Option și derivăm afișarea și selecția dintr-o colecție.
- [00:16:12 — înregistrare separată](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=972s): mutăm intrările disponibile în AddOptions, păstrând succesiunea lui Main.
- [00:28:39 — reprezentare cu delegate](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1719s): eliminăm enum-ul și folosim tipul încorporat Action.
- [00:30:05 — înregistrarea funcțiilor private](https://www.youtube.com/watch?v=nKGsQISMrbQ&t=1805s): memorăm referințe la metode și invocăm delegate-ul selectat în loc de a comuta pe identificatori.
