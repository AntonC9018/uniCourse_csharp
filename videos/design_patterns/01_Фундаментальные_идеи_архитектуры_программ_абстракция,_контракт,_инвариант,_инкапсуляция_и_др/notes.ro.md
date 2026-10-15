> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Scop și vocabular

[00:00:00](https://www.youtube.com/watch?v=mj771Sd298c&t=0s) Învățați să citiți și să proiectați o funcție pornind de la ce trebuie să știe apelantul, ce promite funcția și ce ascunde. Laboratoarele avansate presupun [redacted] competențe și familiarizare anterioară cu idei arhitecturale precum abstracția, cuplarea, invarianții și interfețele. Această lecție aplică aceste idei printr-o verificare de interval, eliminarea duplicatelor și validarea unui senzor; nu este o introducere nouă în programarea C#.

Vocabularul folosit mai jos este:

- **Interfață, semnătură și prototip:** numele vizibil apelantului, tipul de returnare și numele și tipurile parametrilor unei operații. Aici „interfață” nu desemnează în mod specific o declarație `interface` din C#.
- **Implementare:** instrucțiunile care execută o operație. Un **parametru** este o intrare cu nume dintr-o declarație; un **argument** este valoarea furnizată la apel.
- **Interval și limite:** un interval între un capăt inferior și unul superior. Un capăt **inclusiv** aparține intervalului; unul **exclusiv** nu. `[a,b]` include ambele capete; `[a,b)` îl exclude pe cel superior.
- **Argument cu nume:** sintaxa C# `parameterName: value` care identifică parametrul primitor. O **constantă cu nume** atribuie unei valori fixe un nume cu semnificație; o **constantă magică** este un literal a cărui scop nu este explicat.
- **Contract:** ce trebuie să furnizeze apelantul și pe ce poate să se bazeze. **Contractul de intrare**, sau **precondiția**, enunță obligațiile apelantului; **contractul de ieșire** enunță garanțiile operațiilor, inclusiv efectele permise.
- **Stare externă și efect secundar:** date sau activitate observabilă dincolo de valoarea returnată; modificarea lor, inclusiv afișarea, este un efect secundar.
- **Păză (guard):** o verificare care protejează o precondiție. O **aserțiune**, precum `Debug.Assert`, verifică o ipoteză de dezvoltare. **Simbolul de compilare DEBUG** controlează includerea acestor apeluri de aserțiune; o **build Release** convențională le omite.
- **API public și excepție de argument:** un API este o interfață oferită altor coduri; un API public de bibliotecă trebuie să gestioneze apelanți din afara controlului autorului. O excepție de argument raportează argumente invalide în timpul execuției. **NuGet** distribuie pachete de biblioteci .NET.
- **Eliminarea duplicatelor, sau deduplicarea:** păstrarea unei singure apariții a fiecărei valori distincte după o regulă de egalitate specificată.
- **Cuplare:** dependența unei părți a programului față de alta; este numită aici drept context preliminar, nu dezvoltată ca temă separată.
- **Abstracție:** o operație sau un model de date cu semnificație care îi permite utilizatorului să nu țină cont de detaliile implementării. Un **nivel de abstracție** descrie cât de mult din mecanismul subiacent gestionează o bucată de cod.
- **HashSet, List și comparator:** o mulțime urmărește membri distincți, o listă acumulează o secvență ordonată, iar un comparator definește egalitatea sau ordonarea. Compararea **ordinală** a șirurilor folosește valorile caracterelor și distinge majusculele de minuscule; **OrdinalIgnoreCase** ignoră diferența de caz.
- **Denumire semantică:** numirea unei valori sau a unei operații după semnificația sa în program.
- **Normalizare:** transformarea unor reprezentări diferite ale aceleiași semnificații într-o singură formă internă.
- **Invariant:** o condiție stabilită la construire și păstrată de fiecare operație pe durata existenței unui obiect.
- **Fabrică, constructor privat și proprietate de tip get-only:** o fabrică creează obiecte validate; un constructor privat restricționează crearea directă; o proprietate de tip get-only expune citirea fără a expune un setter.
- **Încapsulare:** păstrarea operațiilor dependente de reprezentare și a construcției sub controlul tipului, expunând doar accesul de care au nevoie apelanții.
- **Raționament local:** înțelegerea unei funcții din propria sa logică, din intrările sale explicite și din contractele operațiilor pe care le apelează.
- **Indirectare:** accesul la o acțiune printr-un alt nume sau alt apel, adăugând un nivel pe care cititorul trebuie să-l urmărească.
- **Dependență, configurare și stare globală:** o dependență este ceva de care o operație are nevoie; configurarea furnizează setări; starea globală poate fi accesată fără a fi transmisă ca argument.
- **Mutație și operație pe loc:** mutația modifică date existente; o operație pe loc scrie în obiectul furnizat, în loc să producă o înlocuire independentă.
- **Vedere sau interval numai pentru citire:** o interfață de intrare care oferă citirea fără a oferi scrieri prin acea vedere. Nu îngheață un obiect pe care o altă referință îl poate încă modifica.
- **Referință și alias:** o referință identifică un obiect; un alias este o altă referință către același obiect. Copierea unei referințe nu copiază conținutul obiectului.

[Sursa de prezentare conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md) oferă exemple ulterioare și explicații corectate. Este material de sprijin, nu o înregistrare a fiecărei stări exacte prezentate în video. Exemplele de mai jos sunt reconstrucții sau adaptări scurte; fiecare secțiune relevantă identifică sursa conexă imutabilă. În particular, prima funcție de interval din înregistrare cere limite strict ordonate, în timp ce prezentarea ulterioară permite limite egale.

[00:00:26](https://www.youtube.com/watch?v=mj771Sd298c&t=26s) O versiune simplificată a apelurilor inițiale din acea sursă conexă:

```csharp
Between(15, 10, 20); // true: clar în interior
Between(5, 10, 20);  // false: sub interval
Between(25, 10, 20); // false: peste interval
```

## Citiți interfața inițială a funcției Between

[00:00:26](https://www.youtube.com/watch?v=mj771Sd298c&t=26s) Începeți cu o funcție numită `Between` care ia trei parametri. **Interfața** sa este informația folosită pentru a o apela: numele, tipul de returnare și numele și tipurile parametrilor. **Implementarea** sa este corpul care verifică efectiv valoarea. Declarația sugerează că primul parametru este valoarea de verificat, iar următoarele două sunt începutul și sfârșitul intervalului.

Codul conex ulterior, care face explicită alegerea capetelor, se găsește în [prezentarea cazurilor obișnuite](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#1-start-with-the-ordinary-cases). Aceste apeluri reconstruiesc exemplele inițiale fără a presupune vreo politică a capetelor:

```csharp
Between(15, 10, 20); // true
Between(5, 10, 20);  // false
Between(25, 10, 20); // false
// Forma vizibilă de apelant:
// bool Between(int value, int min, int max)
```

[00:00:50](https://www.youtube.com/watch?v=mj771Sd298c&t=50s) Întregul scop observabil este returnarea unui verdict boolean de apartenență: `true` pentru o valoare din intervalul furnizat și `false` pentru o valoare din afara lui. [00:00:58](https://www.youtube.com/watch?v=mj771Sd298c&t=58s) Rezultatele prezentate sunt `true` pentru 15 și `false` pentru 5 și 25, cu limitele 10 și 20. Aceste cazuri obișnuite nu stabilesc ce se întâmplă la margini.

[00:01:03](https://www.youtube.com/watch?v=mj771Sd298c&t=63s) La început se pare posibil să citiți doar apelurile: valoarea întâi, începutul intervalului al doilea, sfârșitul intervalului al treilea. [00:01:18](https://www.youtube.com/watch?v=mj771Sd298c&t=78s) Acea încredere vine din faptul că aveți definiția sau prototipul la îndemână ca să-l consultați. Fără acel context, trei numere nu își explică rolurile. [00:02:11](https://www.youtube.com/watch?v=mj771Sd298c&t=131s) Chiar dacă definiția este disponibilă, cititorul trebuie să o caute și să potrivească pozițiile; un apel izolat ar putea însemna limita inferioară, limita superioară, apoi valoare.

[00:01:25](https://www.youtube.com/watch?v=mj771Sd298c&t=85s) **Limitele** sunt capetele intervalului. O limită inclusivă acceptă egalitatea; o limită exclusivă o respinge. Numele inițial și numele parametrilor nu spun ce politică se aplică. [00:01:32](https://www.youtube.com/watch?v=mj771Sd298c&t=92s) Pentru un capăt inferior de 10, discuția întreabă dacă 10 este acceptat sau dacă primul întreg acceptat este 11. Formularea despre un „element inclus” ilustrează incertitudinea, nu stabilește un nou rezultat de test.

[00:01:42](https://www.youtube.com/watch?v=mj771Sd298c&t=102s) Cunoscând numele funcției, tipul de returnare și numele și tipurile parametrilor, aveți interfața. [00:01:59](https://www.youtube.com/watch?v=mj771Sd298c&t=119s) Numele și tipurile respective nu spun totuși dacă vreun capăt este inclus.

Cele trei rezultate obișnuite vă pot spune dacă `Between(10, 10, 20)` întoarce `true`?

<details>
<summary>Răspuns</summary>

Nu. Atât o implementare inclusivă, cât și una exclusivă produc cele trei rezultate prezentate. Este nevoie de un exemplu la margine sau de un contract clar.

</details>

## Faceți vizibile rolurile argumentelor

[00:02:27](https://www.youtube.com/watch?v=mj771Sd298c&t=147s) Un **argument cu nume** furnizează o valoare împreună cu numele parametru declarat. În C#, douopunctele leagă acel nume de expresia furnizată. El elimină ghicirea pozițională; nu alege o politică a capetelor.

[Prezentarea conexă despre argumente și constante](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#2-make-argument-roles-and-endpoint-behavior-clear) folosește aceeași progresie. Exemple simplificate:

```csharp
Between(value: 10, min: 10, max: 20);

const int MinimumAllowedTemperature = 10;
const int MaximumAllowedTemperature = 20;
int measuredTemperature = 10;

Between(
    value: measuredTemperature,
    min: MinimumAllowedTemperature,
    max: MaximumAllowedTemperature);
```

Citiți modificările în ordine:

1. [00:02:27](https://www.youtube.com/watch?v=mj771Sd298c&t=147s) Etichetați fiecare argument. Cele două apariții ale lui 10 au acum roluri vizibil diferite: valoarea verificată și minimul.
2. [00:02:46](https://www.youtube.com/watch?v=mj771Sd298c&t=166s) Introduceți constante cu nume pentru limite și transmiteți acele nume. O **constantă cu nume** are o valoare fixă și explică de ce contează acea valoare în această aplicație. `const int` din C# declară o constantă întreagă cu nume.
3. [00:02:54](https://www.youtube.com/watch?v=mj771Sd298c&t=174s) Recunoașteți literalele originale ca fiind potențiale **constante magice**: numere scrise direct în cod, fără a le explica contextul.
4. [00:03:05](https://www.youtube.com/watch?v=mj771Sd298c&t=185s) Folosiți contextul din jur pentru a distinge minimul 10 de maximul 20. Contextul poate furniza semnificație, dar numele explicite permit apelului să o poarte cu el.

Nici `min`, nici `MinimumAllowedTemperature` nu spun dacă egalitatea este acceptată. Lizibilitatea argumentelor și semantica marginilor sunt probleme distincte.

## Enunțați contractul de intrare și de ieșire

[00:03:18](https://www.youtube.com/watch?v=mj771Sd298c&t=198s) **Interfața** este ceea ce utilizatorii știu fără a citi instrucțiunile corpului; **implementarea** sunt acele instrucțiuni. [00:03:42](https://www.youtube.com/watch?v=mj771Sd298c&t=222s) Un **contract** este tot ceea ce apelantul poate avea în vedere când invocă operația. [00:03:55](https://www.youtube.com/watch?v=mj771Sd298c&t=235s) În apelul curent, caracterul inclusiv al minimului și al maximului rămâne o parte neenunțată a acelui contract.

[00:04:07](https://www.youtube.com/watch?v=mj771Sd298c&t=247s) Schimbați numele funcției și al parametrilor astfel încât semantica marginilor să fie vizibilă. [Prezentarea conexă a contractului inclusiv](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#3-state-the-contract-and-make-the-name-help) permite limite egale; reconstrucția de aici păstrează precondiția mai strictă din înregistrare, `minInclusive < maxInclusive`:

```csharp
static bool BetweenInclusive(
    int value, int minInclusive, int maxInclusive)
{
    // Contract de intrare: minInclusive < maxInclusive.
    return value >= minInclusive && value <= maxInclusive;
}

BetweenInclusive(value: 10, minInclusive: 10, maxInclusive: 20);
```

`>=` și `<=` acceptă egalitatea. `&&` cere ca ambele comparații să fie adevărate. Numele `BetweenInclusive` și sufixul `Inclusive` fac vizibile ambele garanții despre capete la locul apelului.

[00:04:18](https://www.youtube.com/watch?v=mj771Sd298c&t=258s) Un contract spune și ce cere funcția de la apelant. [00:04:32](https://www.youtube.com/watch?v=mj771Sd298c&t=272s) Pentru funcția acestei înregistrări, limita inferioară trebuie să fie mai mică decât cea superioară. O pereche neordonată sau egală încalcă acel contract de intrare ales; este o eroare a apelantului. Aceasta este o alegere de API, nu o afirmație matematică că un interval inclusiv cu un singur punct ar fi imposibil. [00:04:47](https://www.youtube.com/watch?v=mj771Sd298c&t=287s) Ambele capete sunt acum garantate ca fiind inclusive.

[00:04:56](https://www.youtube.com/watch?v=mj771Sd298c&t=296s) Garanția de ieșire este un răspuns boolean corect la întrebarea „valoarea aceasta este în interval?”. [00:05:09](https://www.youtube.com/watch?v=mj771Sd298c&t=309s) Promite și nicio modificare a **stării externe**: nicio afișare pe consolă și nicio modificare a datelor fără legătură. Acestea ar fi **efecte secundare**, acțiuni observabile dincolo de returnarea verdictului.

[00:05:26](https://www.youtube.com/watch?v=mj771Sd298c&t=326s) Separați contractul în:

- **Contract de intrare:** ipotezele pe care funcția are dreptul să le facă despre argumente, aici `minInclusive < maxInclusive`.
- **Contract de ieșire:** ipotezele pe care apelantul are dreptul să le facă despre operație, aici apartenența corectă, ambele limite inclusive și nicio modificare a stării externe.

[00:05:34](https://www.youtube.com/watch?v=mj771Sd298c&t=334s) Încălcarea cerinței de intrare este greșeala apelantului. [00:05:36](https://www.youtube.com/watch?v=mj771Sd298c&t=336s) Returnarea verdictului greșit, excluderea unui capet promisat sau modificarea stării fără legătură încalcă contractul de ieșire al funcției.

## Protejați contractul

[00:05:53](https://www.youtube.com/watch?v=mj771Sd298c&t=353s) O **păză** protejează contractul de intrare înainte de a începe munca propriu-zisă. Poate fi o aserțiune pentru o ipoteză de dezvoltare bine stabilită sau o verificare obișnuită la începutul corpului.

[Prezentarea conexă de validare](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#4-validate-an-input-or-assert-an-established-assumption) separă aserțiunile de respingerea în timpul execuției. Exemplele de mai jos păstrează ordonarea strictă a limitelor din înregistrare:

```csharp
// Ipoteză internă în timpul dezvoltării:
Debug.Assert(minInclusive < maxInclusive);

// Validare publică în timpul execuției:
if (minInclusive >= maxInclusive)
    throw new ArgumentException(
        "The minimum must be smaller than the maximum.");

bool result = value >= minInclusive && value <= maxInclusive;
```

Acestea sunt două mecanisme de comparat; nu trebuie să apară amândouă în fiecare funcție.

[00:06:02](https://www.youtube.com/watch?v=mj771Sd298c&t=362s) **Corectare:** exemplul vorbit spune că minimul trebuie să fie mai mare decât maximul. Aceasta inversează contractul enunțat anterior. Aserțiunea corectă este `minInclusive < maxInclusive`; verificarea de intrare invalidă respinge `minInclusive >= maxInclusive`.

[00:06:09](https://www.youtube.com/watch?v=mj771Sd298c&t=369s) O **aserțiune** precum `Debug.Assert(condition)` verifică o ipoteză și raportează o aserțiune eșuată în timpul dezvoltării; nu corectează argumentele. [00:06:16](https://www.youtube.com/watch?v=mj771Sd298c&t=376s) Utilizarea intenționată este de a expune o eroare de programare în timpul dezvoltării și al testării programului.

[00:06:33](https://www.youtube.com/watch?v=mj771Sd298c&t=393s) Înregistrarea descrie eliminarea aserțiunilor după ce programul este scris și testele trec. Mai precis, includerea depinde de **simbolul de compilare DEBUG**, nu de faptul că testele au trecut. Apelurile către `Debug.Assert` sunt omise când simbolul lipsește, ca în build-urile Release convenționale.

[00:06:45](https://www.youtube.com/watch?v=mj771Sd298c&t=405s) Aceasta face aserțiunile nepotrivite ca unică protecție a unui **API public**, o interfață consumată de cod din afara controlului vostru. [00:06:55](https://www.youtube.com/watch?v=mj771Sd298c&t=415s) Când publicați o bibliotecă, utilizatorii descarcă un pachet și îi apelează funcțiile cu propriile argumente. Nu puteți presupune că apelurile lor au fost deja verificate. [00:07:14](https://www.youtube.com/watch?v=mj771Sd298c&t=434s) **NuGet** distribuie pachete de biblioteci .NET. Un pachet NuGet Release optimizat tipic omite acele apeluri de aserțiuni de depanare, lăsând fără nicio pază publică în timpul execuției.

[00:07:23](https://www.youtube.com/watch?v=mj771Sd298c&t=443s) O **excepție de argument** raportează că argumentele furnizate sunt invalide. Un `if` explicit urmat de `throw` supraviețuiește compilării Release normale și raportează utilizarea greșită lângă cauza sa, în loc să lase date invalide să producă o eșecă ulterioară confuză. Un contract public care promite respingerea are nevoie de validare în timpul execuției; o aserțiune doar pentru dezvoltare nu o poate oferi.

Ce se întâmplă dacă un apelant trimite limite inversate unei biblioteci Release protejate doar de `Debug.Assert`?

<details>
<summary>Răspuns</summary>

Într-o build Release convențională fără DEBUG, apelul de aserțiune lipsește. El nu poate impune respingerea. Dacă contractul public promite o eroare clară, este nevoie de o pază în timpul execuției.

</details>

## Ascundeți eliminarea duplicatelor în spatele unui contract

[00:07:43](https://www.youtube.com/watch?v=mj771Sd298c&t=463s) O **abstracție** expune o acțiune cu semnificație, ascunzând cum este realizată. Un nume util de funcție îi permite apelantului să ceară acea acțiune fără să-i reconstruiască mecanismul.

[00:07:52](https://www.youtube.com/watch?v=mj771Sd298c&t=472s) `RemoveDuplicates` ia nume și întoarce un tablou nou cu duplicatele eliminate, păstrând ordinea în care fiecare nume distinct a apărut prima dată. [Prezentarea conexă a abstracției](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#5-a-useful-abstraction-can-hide-a-substantial-operation) enunță același comportament, folosind o vedere numai pentru citire a unei colecții. Acest apel simplificat cu tablouri ilustrează contractul din înregistrare:

```csharp
string[] names = { "Bob", "Ann", "Bob" };
string[] uniqueNames = RemoveDuplicates(names);
// uniqueNames: ["Bob", "Ann"]
// names:       ["Bob", "Ann", "Bob"]
```

[00:08:05](https://www.youtube.com/watch?v=mj771Sd298c&t=485s) Promisiunea vizibilă include ordinea, nu doar unicitatea. Sortarea alfabetică a rezultatului ar încălca acea promisiune pentru aceste date.

[00:08:07](https://www.youtube.com/watch?v=mj771Sd298c&t=487s) Apelantul are nevoie de contract, nu de strategia concretă de eliminare a duplicatelor. [00:08:19](https://www.youtube.com/watch?v=mj771Sd298c&t=499s) Contractul de intrare presupune obiecte de șiruri reale, nenule, nu referințe nule. „Referință nulă” înseamnă `null`, nu un șir de lungime zero; această discuție nu introduce o regulă pentru eliminarea șirurilor goale.

[00:08:29](https://www.youtube.com/watch?v=mj771Sd298c&t=509s) Contractul de ieșire promite trei lucruri:

- Valorile duplicate sunt eliminate după regula de comparare aleasă.
- Se întoarce un tablou nou; datele originale rămân neschimbate.
- Numele păstrate își păstrează ordinea primei apariții.

[00:08:49](https://www.youtube.com/watch?v=mj771Sd298c&t=529s) Abstracția îl scutește pe apelant de la implementarea detectării duplicatelor, a săririi intrărilor repetate și a construirii rezultatului. [00:09:03](https://www.youtube.com/watch?v=mj771Sd298c&t=543s) Acesta este un **nivel de abstracție** mai ridicat: un singur nume descrie rezultatul cerut, iar pașii de nivel mai jos îl realizează. [00:09:13](https://www.youtube.com/watch?v=mj771Sd298c&t=553s) Un nume clar de metodă spune apelantului ce contează, ascunzând mecanismul. [00:09:27](https://www.youtube.com/watch?v=mj771Sd298c&t=567s) În exemplu, filtrarea nu este o singură instrucțiune primitivă; implementarea trebuie să o construiască din mai multe [redacted].

## Implementați eliminarea duplicatelor

[00:09:48](https://www.youtube.com/watch?v=mj771Sd298c&t=588s) Pentru a implementa acțiunea de nivel superior, urmăriți numele deja întâlnite într-un **HashSet**, o colecție de membri distincți, și acumulați primele apariții într-un **List**, o secvență ordonată care crește.

[Implementarea conexă cu mulțime și buclă](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#6-the-caller-and-implementation-work-at-different-levels) întoarce o vedere numai pentru citire a listei. Acest exemplu adaptat întoarce tabloul nou discutat în înregistrare:

```csharp
static string[] RemoveDuplicates(string[] names)
{
    var seen = new HashSet<string>(StringComparer.Ordinal);
    var result = new List<string>();

    foreach (string name in names)
    {
        if (seen.Add(name))
            result.Add(name);
    }

    return result.ToArray();
}
```

Urmăriți construcția și rezultatele sale:

1. [00:09:48](https://www.youtube.com/watch?v=mj771Sd298c&t=588s) Creați mulțimea goală și lista de rezultat. Parcurgeți numele de intrare în ordinea lor inițială cu `foreach`.
2. [00:10:06](https://www.youtube.com/watch?v=mj771Sd298c&t=606s) Apelați `seen.Add(name)`. Întoarce `true` când numele lipsea și tocmai a fost inserat. Abia apoi adăugați numele la rezultat.
3. [00:10:30](https://www.youtube.com/watch?v=mj771Sd298c&t=630s) Pentru un nume repetat, `Add` întoarce `false` adăugarea protejată nu se execută, deci rezultatul nu primește a doua copie.
4. Întoarceți un tablou nou care conține numele acumulate.

Pentru `["Bob", "Ann", "Bob"]`, rezultatele succesive ale mulțimii sunt `true`, `true` și `false`; secvența păstrată este `["Bob", "Ann"]`. Aceasta este o parcurgere a algoritmului, nu o nouă rulare de test raportată.

[00:10:37](https://www.youtube.com/watch?v=mj771Sd298c&t=637s) Un **comparator** definește ce valori contează ca egale. `StringComparer.Ordinal` compară șirurile după valorile caracterelor și distinge cazul: `"Ann"` și `"ann"` rămân nume diferite. [00:10:55](https://www.youtube.com/watch?v=mj771Sd298c&t=655s) `StringComparer.OrdinalIgnoreCase` este alternativa când diferențele de caz trebuie ignorate. Schimbarea comparatorului ar schimba contractul de eliminare a duplicatelor.

[00:11:02](https://www.youtube.com/watch?v=mj771Sd298c&t=662s) Contractul din înregistrare folosește comparație ordinală, sensibilă la caz. [00:11:14](https://www.youtube.com/watch?v=mj771Sd298c&t=674s) Operația de nivel superior este deci implementată prin parcurgere, apartenență la o mulțime și acumulare într-o listă, toate de nivel mai jos. Un apelant se poate baza pe acel contract fără a depinde de reprezentarea cu mulțime și listă.

## Numiți valorile după semnificația lor

[00:11:32](https://www.youtube.com/watch?v=mj771Sd298c&t=692s) **Denumirea semantică** dă variabilelor nume care spun ce înseamnă, nu doar că le descriu ca pe un indicator generic. [Exemplele conexe de denumire semantică](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#7a-names-can-express-meaning-in-the-system) ilustrează aceeași îmbunătățire; această progresie scurtă folosește terminologia din înregistrare:

```csharp
bool indicator = true;

// Mai multă semnificație:
bool temperatureWithinIntervalIndicator = true;
int rightSensorTemperature = 18;
```

[00:11:45](https://www.youtube.com/watch?v=mj771Sd298c&t=705s) Un nume vag poate fi rezonabil atunci când codul din jur într-adevăr nu cunoaște semnificația de domeniu a valorii și doar o transmite. Dacă semnificația este cunoscută, păstrarea ei în nume îl ajută pe cititor.

[00:11:59](https://www.youtube.com/watch?v=mj771Sd298c&t=719s) Alegeți un nume pentru faptul reprezentat. [00:12:04](https://www.youtube.com/watch?v=mj771Sd298c&t=724s) `temperatureWithinIntervalIndicator` enunță atât măsurătoarea cât și verificarea care determină valoarea booleană. [00:12:14](https://www.youtube.com/watch?v=mj771Sd298c&t=734s) `rightSensorTemperature` identifică în plus dispozitivul care a furnizat măsurătoarea. Numele comunică intenția; calculul trebuie totuși să stabilească corect faptul numit.

[00:12:23](https://www.youtube.com/watch?v=mj771Sd298c&t=743s) Un tip dedicat pentru datele senzorului ar putea codifica și mai multă semnificație. Acest pas este deliberat amânat aici; aceste nume îmbunătățesc codul prezent fără a cere acel proiect suplimentar.

## Grupați și normalizați intervalele de numere întregi

[00:12:30](https://www.youtube.com/watch?v=mj771Sd298c&t=750s) Înlocuiți argumentele independente de început și sfârșit cu o **abstracție de interval** care poartă ambele limite împreună. [00:12:51](https://www.youtube.com/watch?v=mj771Sd298c&t=771s) Apelantul se poate gândi acum la un singur interval, în loc să memoreze două valori poziționale fără legătură. [00:13:05](https://www.youtube.com/watch?v=mj771Sd298c&t=785s) Stocați-l o dată și refolosiți-l pentru verificări ulterioare.

[Prezentarea conexă a normalizării intervalelor](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#8-normalize-the-intervals-representation) folosește alegeri de margini la construire. Apelurile simplificate de mai jos arată tranziția:

```csharp
// Mai devreme: limite separate repetate la fiecare apel.
BetweenInclusive(value: temperature, minInclusive: 10, maxInclusive: 19);

// Mai târziu: construim și refolosim un interval.
var interval = Interval.Create(
    min: 10, max: 19,
    minBoundary: Boundary.Inclusive,
    maxBoundary: Boundary.Inclusive);

bool allowed = interval.Contains(temperature);
```

`Boundary` este o alegere între comportament inclusiv și exclusiv al capetelor. `Create` este o **fabrică**, o operație care construiește intervalul printr-o logică controlată. `Contains` cere abstracției verdictul de apartenență.

[00:13:19](https://www.youtube.com/watch?v=mj771Sd298c&t=799s) Apelanții nu trebuie să știe dacă stocarea folosește patru câmpuri, două câmpuri normalizate sau două elemente de tablou. [00:13:41](https://www.youtube.com/watch?v=mj771Sd298c&t=821s) **Normalizarea** schimbă reprezentarea păstrând semnificația. [00:13:57](https://www.youtube.com/watch?v=mj771Sd298c&t=837s) Aici toate intrările devin un început inclusiv și un sfârșit exclusiv.

[00:14:04](https://www.youtube.com/watch?v=mj771Sd298c&t=844s) O limită superioară întreagă inclusivă de 19 devine o limită superioară exclusivă de 20. Fiecare reprezentare conține exact numerele întregi de la 10 la 19:

| Interval întreg furnizat | Interval normalizat |
| --- | --- |
| `[10, 19]` | `[10, 20)` |
| `[10, 20)` | `[10, 20)` |
| `(9, 19]` | `[10, 20)` |

Parantezele pătrate includ un capăt; parantezele rotunde îl exclud. O limită inferioară exclusă este mutată la următorul număr întreg, făcând limita inferioară stocată inclusivă. O limită superioară inclusă este mutată la următorul număr întreg, făcând limita superioară stocată exclusivă.

[00:14:35](https://www.youtube.com/watch?v=mj771Sd298c&t=875s) Această conversie depinde de domeniul numerelor întregi. Adăugarea lui unu nu păstrează semnificația unui interval de valori în virgulă mobilă. Înregistrarea lasă proiectarea intervalelor în virgulă mobilă în afara acestui exemplu; nu predă un algoritm general de normalizare pentru virgulă mobilă.

## Stabiliți și păstrați invariantul

[00:14:37](https://www.youtube.com/watch?v=mj771Sd298c&t=877s) Un **invariant** este o condiție pe care tipul o stabilește la construire și pe care toate operațiile sale trebuie să o păstreze. O metodă publică nu trebuie niciodată să lase obiectul într-o stare care o încalcă. [00:15:00](https://www.youtube.com/watch?v=mj771Sd298c&t=900s) Pentru intervalul normalizat, invariantul este `StartInclusive <= EndExclusive`. Limitele normalizate egale descriu un interval vid, deoarece niciun număr întreg nu poate fi simultan cel puțin egal cu acel început și mai mic decât acel sfârșit.

[00:15:16](https://www.youtube.com/watch?v=mj771Sd298c&t=916s) Folosiți un constructor privat și o fabrică. Un **constructor privat** împiedică apelanții din exterior să construiască direct un obiect neverificat. **Fabrica** validează cererea, normalizează limitele și creează obiectul abia după ce invariantul este stabilit.

[Exemplul conex de invariant și implementarea completă](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#9-an-invariant-keeps-the-normalized-bounds-valid) folosesc o struct read-only și normalizează intervalele vide la o valoare implicită. Această adaptare simplificată de clasă respinge în schimb o pereche normalizată invalidă; nu este o transcriere exactă a acelei implementări sau a videoclipului:

```csharp
public sealed class Interval
{
    public long StartInclusive { get; }
    public long EndExclusive { get; }

    private Interval(long startInclusive, long endExclusive)
    {
        StartInclusive = startInclusive;
        EndExclusive = endExclusive;
    }

    public static Interval Create(
        int min, int max,
        Boundary minBoundary, Boundary maxBoundary)
    {
        if (min > max)
            throw new ArgumentException("Bounds are reversed.");

        long start = (long)min
            + (minBoundary == Boundary.Exclusive ? 1 : 0);
        long end = (long)max
            + (maxBoundary == Boundary.Inclusive ? 1 : 0);

        if (start > end)
            throw new ArgumentException("Invalid normalized interval.");

        return new Interval(start, end);
    }

    // Contains este adăugată în secțiunea următoare.
}
```

Extrasul presupune valori `Boundary` valide. Implementarea completă conexă verifică și acele argumente de enumerare. Limitele sale interne de tip `long` evită depășirea când un `int.MaxValue` inclusiv trebuie să devină un capăt exclusiv cu o unitate mai mare; conversia are loc înaintea adunării. Acesta este un detaliu de implementare furnizat de sursa de sprijin ulterioară, nu o afirmație suplimentară despre ce demonstrează înregistrarea.

Urmăriți pașii fabricii:

1. [00:15:16](https://www.youtube.com/watch?v=mj771Sd298c&t=916s) Validați limitele cerute înainte de a construi obiectul.
2. [00:15:34](https://www.youtube.com/watch?v=mj771Sd298c&t=934s) Normalizați limita inferioară: lăsați o limită inclusivă neatinsă; adăugați unu unei limite inferioare întregi exclusive.
3. [00:15:34](https://www.youtube.com/watch?v=mj771Sd298c&t=934s) Normalizați limita superioară: lăsați o limită exclusivă neatinsă; adăugați unu unei limite superioare întregi inclusive.
4. [00:15:48](https://www.youtube.com/watch?v=mj771Sd298c&t=948s) Verificați că începutul normalizat nu depășește sfârșitul normalizat. Nu creați un obiect care încalcă invariantul.
5. [00:15:55](https://www.youtube.com/watch?v=mj771Sd298c&t=955s) Întoarceți un obiect ale cărui limite au fost deja validate și sunt expuse doar pentru citire.

[00:15:53](https://www.youtube.com/watch?v=mj771Sd298c&t=953s) Înregistrarea lasă deliberat deschisă problema răspunsului la un invariant invalid. Aruncarea unei excepții, raportarea eșecului construcției sau alt răspuns trebuie să urmeze cerințele proiectului. Excepția de mai sus este o alegere ilustrativă, nu un rezultat impus de lecție.

[00:15:55](https://www.youtube.com/watch?v=mj771Sd298c&t=955s) O **proprietate de tip get-only**, scrisă `{ get; }`, expune un getter dar niciun setter public. Odată ce fabrica întoarce, clienții nu pot înlocui independent o limită. [00:16:08](https://www.youtube.com/watch?v=mj771Sd298c&t=968s) Deoarece limitele rămân neschimbate, iar ordonarea lor a fost stabilită la construire, invariantul este valabil pe durata existenței obiectului.

De ce validăm în fabrică, în loc să repetăm aceeași verificare la fiecare apelant?

<details>
<summary>Răspuns</summary>

Fabrica deține construcția. Fiecare interval creat cu succes poartă atunci aceeași garanție, deci apelanții și operațiile ulterioare se pot baza pe invariantul lui, în loc să-l restabilească. Restrângerea scrierilor ulterioare face parte din păstrarea acelei garanții.

</details>

## Încapsulați operațiile pentru raționament local

[00:16:22](https://www.youtube.com/watch?v=mj771Sd298c&t=982s) **Încapsularea** păstrează construcția concretă și operațiile dependente de reprezentare sub controlul tipului. Apelanții primesc operațiile și accesul de citire de care au nevoie, fără posibilitatea de a strica invariantul. [00:16:34](https://www.youtube.com/watch?v=mj771Sd298c&t=994s) Constructorul privat face din `Create` singura cale de creare pentru exemplul de clasă, deci validarea nu poate fi sărită prin apelarea directă a acelui constructor.

[00:16:52](https://www.youtube.com/watch?v=mj771Sd298c&t=1012s) Adăugați `Contains`, care ascunde modul în care sunt interpretate limitele stocate. [Prezentarea conexă a apartenenței](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#10-encapsulation-keeps-bound-interpretation-inside-the-interval) furnizează această operație. Adăugare simplificată la tipul anterior:

```csharp
public bool Contains(int value)
{
    bool result = value >= StartInclusive && value < EndExclusive;
    return result;
}

// Apelant:
bool allowed = interval.Contains(rightSensorTemperature);
```

Comparația folosește `>=` la începutul inclusiv și `<` la sfârșitul exclusiv. Deoarece construcția a normalizat deja limitele, metoda nu are nevoie de marcajele originale de margine.

[00:17:10](https://www.youtube.com/watch?v=mj771Sd298c&t=1030s) În această reprezentare cele două comparații par evidente. O altă reprezentare ar putea cere alte operații. [00:17:19](https://www.youtube.com/watch?v=mj771Sd298c&t=1039s) Întrebarea apelantului rămâne aceeași: valoarea este în interior? El nu trebuie să știe ce câmpuri stocate sau ce verificări produc răspunsul.

[00:17:30](https://www.youtube.com/watch?v=mj771Sd298c&t=1050s) **Raționamentul local** înseamnă înțelegerea codului din fața voastră folosind logica sa, intrările explicite și contractele funcțiilor pe care le apelează. Lecția păstrează deliberat acest raționament local; nu vă cere să reconstruiți sistemul din jur.

[Prezentarea conexă a raționamentului local](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#11-local-reasoning-means-using-the-contracts-of-your-dependencies) oferă același flux de lucru compact:

```csharp
var uniqueNames = RemoveDuplicates(names);
ShowNames(uniqueNames);
```

[00:17:34](https://www.youtube.com/watch?v=mj771Sd298c&t=1054s) Fiecare linie are un rol comprehensibil: produce nume distincte, apoi afișează rezultatul. Trebuie să cunoașteți contractele de eliminare a duplicatelor și de afișare; nu și algoritmele lor sau restul aplicației. Promisiunea anterioară de absență a efectelor secundare aparține contractului de verificare a intervalului și de eliminare a duplicatelor, nu lui `ShowNames`, a cărui sarcină intenționată include afișarea rezultatului.

## Centralizați verificarea tuturor senzorilor

[00:18:01](https://www.youtube.com/watch?v=mj771Sd298c&t=1081s) Considerați cod care repetă fiecare câmp de senzor în fiecare funcție de verificare. Adăugarea unui senzor nou impune atunci modificări oriunde unde sunt listate acele câmpuri. [00:18:09](https://www.youtube.com/watch?v=mj771Sd298c&t=1089s) Mai există o problemă: un cititor nu poate distinge dacă câmpurile listate sunt mulțimea completă sau o submulțime aleasă intenționat.

[Exemplele conexe de câmpuri de senzori](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#12-a-caller-should-not-have-to-list-all-the-fields) arată cunoașterea repetată a reprezentării. Iată o stare intermediară simplificată:

```csharp
bool allowed = interval.Contains(readings.Left)
    && interval.Contains(readings.Center)
    && interval.Contains(readings.Right);
SetIndicator(allowed);

// Altă funcție repetă aceeași verificare pe trei câmpuri
// înainte de a decide dacă trebuie declanșată o alarmă.
```

[00:18:39](https://www.youtube.com/watch?v=mj771Sd298c&t=1119s) Scrierea acelei enumerări în linie nu îi exprimă completitudinea. [00:18:57](https://www.youtube.com/watch?v=mj771Sd298c&t=1137s) Ea amestecă și detalii de nivelul reprezentării cu scopul de nivel mai ridicat al funcției, precum setarea unui indicator sau luarea unei decizii de alarmă.

[00:19:07](https://www.youtube.com/watch?v=mj771Sd298c&t=1147s) Extrageți operația care deține atât enumerarea, cât și verificările. [Extragerea conexă AllWithin](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#13-put-the-field-by-field-operation-in-one-place) centralizează aceeași regulă:

```csharp
static bool AllWithin(Readings readings, Interval interval)
{
    return interval.Contains(readings.Left)
        && interval.Contains(readings.Center)
        && interval.Contains(readings.Right);
}

// Actualizarea ecranului:
bool allowed = AllWithin(readings, interval);
SetIndicator(allowed);
```

`Readings` este tipul de date al senzorului ale cărui câmpuri le verifică operația. `AllWithin` trebuie să promită că este verificată **fiecare** măsurătoare relevantă; numele său ar fi înșelător dacă implementarea ar omite tacit una.

[00:19:23](https://www.youtube.com/watch?v=mj771Sd298c&t=1163s) Apelantul raționează acum despre validarea tuturor măsurătorilor, fără să decidă ce câmpuri există. Parcurgerea câmpurilor și verificarea apartenenței trăiesc împreună în ajutorul de nivel mai jos; codul de la nivelul ecranului se ocupă de verdict și de indicator.

Rămâne muncă de întreținut: adăugarea unui senzor cere actualizarea ajutorului centralizat. Beneficiul este că fiecare funcție de ecran sau de alarmă nu mai repetă acea cunoaștință despre câmpuri. Pentru această extragere nu este necesară niciun design nou de colecție sau iterator.

## Cântăriți costul indirectării

[00:19:44](https://www.youtube.com/watch?v=mj771Sd298c&t=1184s) Încercați aceeași tehnică de extragere și pentru paza de intrare: mutați validarea în `CheckArguments`, dați-i parametrii funcției și lăsați-o să arunce excepția. [Comparația conexă a extragerii pazei](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#14-another-call-can-help-or-just-make-you-look-elsewhere) arată de ce acel nivel suplimentar ar putea aduce puțină valoare. Exemplul păstrează contractul de intrare strict din înregistrare:

```csharp
// Înainte: paza este vizibilă direct.
if (minInclusive >= maxInclusive)
    throw new ArgumentException("Invalid bounds.");

// După: alt apel ascunde aceeași pază.
CheckArguments(value, minInclusive, maxInclusive);

// Ajutor separat:
static void CheckArguments(
    int value, int minInclusive, int maxInclusive)
{
    if (minInclusive >= maxInclusive)
        throw new ArgumentException("Invalid bounds.");
}
```

Aceste fragmente înainte și după sunt alternative, nu instrucțiuni de a executa ambele paze. [00:19:46](https://www.youtube.com/watch?v=mj771Sd298c&t=1186s) Ajutorul ține validarea asociată operației, dar acum înțelegerea ei necesită încă o căutare; `value` nici măcar nu este necesar pentru această verificare de limite.

[00:20:00](https://www.youtube.com/watch?v=mj771Sd298c&t=1200s) Extragerea poate uneori alinia validarea cu restul nivelului de abstracție al unei funcții mai mari. [00:20:22](https://www.youtube.com/watch?v=mj771Sd298c&t=1222s) În acest exemplu minuscule, ea ascunde esențialmente o singură acțiune deja comprehensibilă. Cititorul câștigă un nume și un strat nou de parcurs, fără o reducere substanțială a complexității.

[00:20:36](https://www.youtube.com/watch?v=mj771Sd298c&t=1236s) O abstracție poate ajuta sau rău. [00:20:45](https://www.youtube.com/watch?v=mj771Sd298c&t=1245s) Operațiile care erau anterior în linie sunt acum vizibile doar urmărind noul nume. [00:21:04](https://www.youtube.com/watch?v=mj771Sd298c&t=1264s) Aceasta este **indirectarea**: accesul la operația reală printr-un alt apel sau alt nume. [00:21:26](https://www.youtube.com/watch?v=mj771Sd298c&t=1286s) Aici probabil nu aduce niciun beneficiu, dar aceasta este o apreciere despre acest exemplu, nu o interdicție a ajutoarelor de validare.

Comparați modificările:

- `AllWithin` elimină din apelanți cunoașterea repetată a structurii senzorilor și enunță o garanție de completitudine.
- `CheckArguments` doar mută o pază scurtă în exemplu. Cititorul trebuie s-o urmărească ca să afle ce este verificat.

Întrebați ce cunoștințe economisește extragerea, ce contract stabilește și ce navigare suplimentară adaugă. Un ajutor își câștigă locul prin aceste beneficii, nu doar pentru că extragerea este posibilă.

## Păstrați fluxul de lucru la un singur nivel de abstracție

[00:21:29](https://www.youtube.com/watch?v=mj771Sd298c&t=1289s) Un bloc care citește nume, gestionează direct o mulțime și o listă, apoi afișează numele trece de la intenția de nivel ridicat într-un algoritm și înapoi.

[Fluxul de lucru conex înainte și după](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#15-show-the-workflow-without-its-subordinate-algorithm) extrage eliminarea duplicatelor. Această comparație simplificată folosește afișarea în locul scrierii în fișier:

```csharp
// Înainte:
var names = ReadNames();
var seen = new HashSet<string>(StringComparer.Ordinal);
var unique = new List<string>();
foreach (var name in names)
{
    if (seen.Add(name))
        unique.Add(name);
}
ShowNames(unique);

// După:
var inputNames = ReadNames();
var uniqueNames = RemoveDuplicates(inputNames);
ShowNames(uniqueNames);
```

[00:21:50](https://www.youtube.com/watch?v=mj771Sd298c&t=1310s) Extrageți acțiunile concrete de **deduplicare** în abstracția deja introdusă `RemoveDuplicates`. Acțiunea de afișare rămâne ultimul pas al fluxului de lucru; `RemoveDuplicates` nu este o funcție de afișare. Aceasta corectează o lectură imprecisă a explicației vorbite.

[00:22:08](https://www.youtube.com/watch?v=mj771Sd298c&t=1328s) Mulțimea, parcurgerea și acumularea rezultatului sunt ascunse sub numele eliminării duplicatelor. [00:22:16](https://www.youtube.com/watch?v=mj771Sd298c&t=1336s) Primul bloc amestecă niveluri de abstracție înalte, joase și înalte; al doilea enunță trei acțiuni de nivel de flux de lucru. Un **nivel de abstracție** este relativ: o operație este mai ridicată față de straturile și conceptele de dedesubt, nu pentru că ar primi un rang numeric universal.

Această restructurare nu schimbă ordinea primei apariții, politica de comparare sau garanția unui rezultat nou. Ea schimbă cât de mult detaliu trebuie tratat pentru a înțelege fluxul de lucru din jur.

## Faceți explicite dependențele de configurare

[00:22:33](https://www.youtube.com/watch?v=mj771Sd298c&t=1353s) O funcție poate ascunde date de intrare, la fel cum ascunde un algoritm. **Configurarea** furnizează setări; o **dependență** este ceva de care funcția are nevoie pentru a-și face treaba. Citirea configurării din **stare globală** adaugă o dependență absentă din lista de parametri.

[00:22:41](https://www.youtube.com/watch?v=mj771Sd298c&t=1361s) Transmiteți explicit configurarea necesară. [Prezentarea conexă a intrărilor explicite](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#16-make-the-necessary-input-visible) ilustrează această diferență. Reconstrucție scurtă:

```csharp
// Intrare ascunsă:
bool IsAllowed(int temperature)
    => temperature <= Settings.MaximumTemperature;

// Intrare vizibilă:
bool IsAllowed(int temperature, int maximumTemperature)
    => temperature <= maximumTemperature;
```

Aceste declarații sunt alternative. Rezultatul primei funcții depinde atât de argumentul vizibil, cât și de orice setare globală prezentă în acel moment. A doua enunță acea setare drept intrare. Doar citirea stării globale nu trebuie să o modifice: o dependență ascunsă și un efect secundar sunt concepte diferite.

[00:22:50](https://www.youtube.com/watch?v=mj771Sd298c&t=1370s) Un alt design sugerat grupează setările într-o abstracție și pune acolo o operație `IsValid`. Propunerea înregistrată este în legătură cu [sfatul prezentării despre abstracții de configurare cu semnificație](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#16-make-the-necessary-input-visible); următoarea este o adaptare schematică, nu cod exact angajat:

```csharp
// settings este furnizat acestei operații, nu obținut global.
bool IsAllowed(int temperature, TemperatureSettings settings)
    => settings.IsValid(temperature);
```

`TemperatureSettings` reprezintă un tip care deține regula de temperatură; `IsValid` încapsulează verificările. Implementarea sa poate ascunde exact ce setări individuale participă, în timp ce semnătura funcției arată în continuare că este necesar un obiect de setări.

[00:23:06](https://www.youtube.com/watch?v=mj771Sd298c&t=1386s) Această abstracție poate ascunde atât logica de validare, cât și dependența ei de anumite setări. [00:23:09](https://www.youtube.com/watch?v=mj771Sd298c&t=1389s) Asta înseamnă că un cititor nu mai vede fiecare setare specifică în funcția exterioară. Dependența de obiectul de setări rămâne importantă și trebuie să rămână explicită.

[00:23:22](https://www.youtube.com/watch?v=mj771Sd298c&t=1402s) Înfășurarea unei citiri globale într-o metodă nu elimină dependența de datele globale. O funcție autonomă primește în schimb configurația printr-un parametru. [00:23:31](https://www.youtube.com/watch?v=mj771Sd298c&t=1411s) Aceasta susține **raționamentul local**: tot ce are nevoie operația este furnizat local, deci înțelegerea ei nu cere reconstruirea întregului mediu global al apelantului.

[00:23:51](https://www.youtube.com/watch?v=mj771Sd298c&t=1431s) Înregistrarea favorizează o abstracție de setări la care sunt furnizate setările necesare, în locul unei căutări implicite globale. Folosiți abstracția pentru a încapsula o regulă cu semnificație; folosiți semnătura pentru a face dependența vizibilă. O abstracție poate reduce detaliul fără să ascundă faptul că este necesară o intrare.

## Exprimați mutarea și observați datele partajate

[00:24:03](https://www.youtube.com/watch?v=mj771Sd298c&t=1443s) Tipurile parametrilor pot comunica părți ale unui contract. O **vedere numai pentru citire** oferă citirea fără operații de scriere prin acea vedere. Înregistrarea compară o astfel de intrare, inclusiv un interval numai pentru citire, cu o listă mutabilă. Ulteriorul [prezentare a accesului prin parametri](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#17-use-a-parameter-view-that-matches-the-intended-access) folosește `IReadOnlyList` pentru aceeași distincție; este o alegere de tip conexă, nu declarația exactă din înregistrare.

```csharp
// Forme de declarație, nu implementări complete:
IReadOnlyList<string> RemoveDuplicates(IReadOnlyList<string> names);

void SortInPlace(List<string> names);
```

`IReadOnlyList` oferă citire și numărare fără scrieri în colecție prin acea interfață. `List` oferă mutația. Un **interval numai pentru citire** oferă în mod similar o vedere care nu oferă scrieri de elemente prin acel interval; nu trebuie să fie interschimbabil cu orice interfață de colecție.

[00:24:16](https://www.youtube.com/watch?v=mj771Sd298c&t=1456s) Când `RemoveDuplicates` promite că nu își modifică intrarea, duplicatele rămân în datele originale și dispar doar din rezultatul nou întors. Un exemplu scurt asociat [contractului conex de deduplicare](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#5-a-useful-abstraction-can-hide-a-substantial-operation):

```csharp
var original = new List<string> { "Ann", "Ann" };
var unique = RemoveDuplicates(original);
// original conține în continuare două intrări.
// unique conține o singură intrare.
```

[00:24:41](https://www.youtube.com/watch?v=mj771Sd298c&t=1481s) `SortInPlace` este o **operație pe loc**: sortează rescriind conținutul propriu al listei furnizate. Ea are deci nevoie de acces scriibil, nu de intrarea numai pentru citire din înregistrare. Numele și contractul ar trebui să dezvăluie această **mutare**, o modificare a unui obiect existent.

[00:24:53](https://www.youtube.com/watch?v=mj771Sd298c&t=1493s) Un parametru mutabil înseamnă că apelantul trebuie să fie pregătit pentru o posibilă mutație, dacă contractul nu spune altfel. [00:25:11](https://www.youtube.com/watch?v=mj771Sd298c&t=1511s) **Corectare:** discuția vorbită încearcă și să deducă lipsa mutării din tipul de returnare. Nici returnarea unei valori, nici returnarea lui `void` nu stabilește dacă intrarea este modificată. O funcție poate returna o valoare nouă și să mute intrarea, sau poate returna `void` și să lase intrarea neatinsă. Folosiți împreună contractul enunțat al operației, numele său cu semnificație și accesul oferit de tipul parametrului.

[00:25:18](https://www.youtube.com/watch?v=mj771Sd298c&t=1518s) O **referință** identifică un obiect. Trecerea unei liste ca parametru obișnuit copiază valoarea referinței, nu conținutul listei; acesta nu este mecanismul de parametri `ref` din C#. Un **alias** este o a doua referință către același obiect.

[Demonstrația conexă a referințelor partajate](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#18-two-references-can-observe-the-same-change) arată consecința. Acest exemplu simplificat include operația de sortare:

```csharp
static void SortInPlace(List<string> names)
    => names.Sort(StringComparer.Ordinal);

var original = new List<string> { "Bob", "Ann" };
IReadOnlyList<string> secondReference = original;

SortInPlace(original);
Console.WriteLine(secondReference[0]); // Ann
```

[00:25:32](https://www.youtube.com/watch?v=mj771Sd298c&t=1532s) Afișarea prin referința originală sau prin a doua observă conținutul sortat: ambele identifică același obiect, deci primul element este acum `"Ann"`. Aceasta este o reconstrucție ilustrativă a rezultatului de aliasing din înregistrare, nu o rulare de test efectuată pentru aceste note.

O vedere numai pentru citire restricționează ce poate face posesorul ei prin acea vedere. Ea nu face obiectul partajat de bază imutabil și nu împiedică un alt proprietar să-l modifice. Această limită precisă face parte din înțelegerea contractului de intrare.

Prezicți lista originală după eliminarea duplicatelor fără mutare și după o sortare pe loc.

<details>
<summary>Răspuns</summary>

Eliminarea duplicatelor lasă intrările originale, inclusiv duplicatele, neschimbate și întoarce un rezultat separat. O sortare pe loc modifică lista furnizată; fiecare alias al acelei liste observă ordinea schimbată.

</details>

## Decideți ce abstracții își justifică locul

[00:25:47](https://www.youtube.com/watch?v=mj771Sd298c&t=1547s) Reveniți la exemplul senzorului. Abstracția peste date permite apelanților să nu mai raționeze despre câmpurile sale concrete. [00:25:54](https://www.youtube.com/watch?v=mj771Sd298c&t=1554s) Contractul său trebuie să promită că toate câmpurile relevante sunt luate în calcul.

[Comparația finală conexă a apelanților](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#19-compare-the-knowledge-required-by-two-callers) întreabă ce cunoștințe elimină ajutorul. O versiune compactă:

```csharp
// Apelant dependent de reprezentare:
bool allowed = interval.Contains(readings.Left)
    && interval.Contains(readings.Center)
    && interval.Contains(readings.Right);

// Apelant care se bazează pe contractul tuturor măsurătorilor:
bool allAllowed = AllWithin(readings, interval);
SetIndicator(allAllowed);
```

[00:26:06](https://www.youtube.com/watch?v=mj771Sd298c&t=1566s) Fără abstracție, un al patrulea senzor impune schimbarea fiecărui apelant care enumeră câmpurile. [00:26:19](https://www.youtube.com/watch?v=mj771Sd298c&t=1579s) Cu ea, enumerarea dependentă de câmpuri se schimbă într-o singură operație centralizată. Apelanții continuă să se bazeze pe garanția sa de completitudine. Acea schimbare concretă explică valoarea abstracției.

[00:26:24](https://www.youtube.com/watch?v=mj771Sd298c&t=1584s) Adăugarea unui alt înveliș peste `SetIndicator` poate fi inutilă dacă doar oferă un alt nume pentru aceeași acțiune. [00:26:38](https://www.youtube.com/watch?v=mj771Sd298c&t=1598s) Un înveliș devine cu semnificație când indicatorul are un scop de domeniu precis, de exemplu indicarea faptului că o setare de temperatură maximă este validă.

[Discuția conexă despre denumire semantică](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md#7a-names-can-express-meaning-in-the-system) susține această distincție; acest înveliș specific înregistrării este o ilustrare simplificată, nu o stare exactă din acea sursă:

```csharp
void SetMaxSetTemperatureIndicatorValue(bool value)
{
    SetIndicator(value);
}
```

[00:26:49](https://www.youtube.com/watch?v=mj771Sd298c&t=1609s) Acest înveliș este justificat doar dacă numele și amplasarea lui codifică o intenție reală de domeniu. Un înveliș generic care doar redenumește o acțiune deja clară adaugă indirectare fără acel beneficiu. Semnificația semantică, cunoașterea centralizată a reprezentării și contractele utile sunt motive concrete pentru a introduce o abstracție; introducerea unui strat în plus, prin ea însăși, nu este un asemenea motiv.

Ce trebuie să promită `AllWithin` și de ce învelișul de indicator semantic este o decizie separată?

<details>
<summary>Răspuns</summary>

AllWithin trebuie să verifice fiecare măsurătoare relevantă față de interval, nu o submulțime ascunsă. El centralizează cunoașterea câmpurilor senzorilor. Învelișul de indicator își justifică locul doar dacă numește o semnificație reală a aplicației; centralizarea enumerării câmpurilor nu justifică automat învelirea fiecărui apel ulterior.

</details>

Lecția rămâne la raționamentul local asupra funcțiilor și la aceste exemple mici. Un tip dedicat pentru senzor este amânat la prima propunere; proiectarea intervalelor în virgulă mobilă este în afara exemplului de normalizare a numerelor întregi; politica de eșec a fabricii depinde de cerințele proiectului. Validarea pentru biblioteci publice este discutată pentru a explica de ce aserțiunile de dezvoltare nu sunt suficiente, nu pentru a preda ambalarea bibliotecilor. Nu se stabilește nici o arhitectură de sistem complet, nici o regulă universală despre când ar trebui extras fiecare ajutor.

## Istoricul modificărilor de cod

Nu se afirmă că vreun commit inspectat conține stările intermediare exacte ale înregistrării. Stările stabile de mai jos de pe video urmează ordinea construcției; ultima intrare trăinde către cod sursă conex ulterior.

- [00:00:26](https://www.youtube.com/watch?v=mj771Sd298c&t=26s) Forma inițială a lui `Between` și apelurile obișnuite din interior/din exterior.
- [00:02:27](https://www.youtube.com/watch?v=mj771Sd298c&t=147s) Argumentele cu nume clarifică rolurile argumentelor; [00:02:46](https://www.youtube.com/watch?v=mj771Sd298c&t=166s) constantele cu nume furnizează semnificația aplicației.
- [00:04:07](https://www.youtube.com/watch?v=mj771Sd298c&t=247s) Funcția inclusivă și numele parametrilor expun semantica capetelor.
- [00:05:53](https://www.youtube.com/watch?v=mj771Sd298c&t=353s) Păzile de intrare și aserțiunile; [00:07:23](https://www.youtube.com/watch?v=mj771Sd298c&t=443s) respingerea argumentelor în timpul execuției pentru contractele publice.
- [00:07:52](https://www.youtube.com/watch?v=mj771Sd298c&t=472s) Contractul de eliminare a duplicatelor; [00:09:48](https://www.youtube.com/watch?v=mj771Sd298c&t=588s) implementarea cu mulțime și listă.
- [00:12:30](https://www.youtube.com/watch?v=mj771Sd298c&t=750s) Abstracția de interval; [00:15:34](https://www.youtube.com/watch?v=mj771Sd298c&t=934s) limitele normalizate, urmate de validarea invariantului.
- [00:16:52](https://www.youtube.com/watch?v=mj771Sd298c&t=1012s) Apartenență încapsulată.
- [00:19:07](https://www.youtube.com/watch?v=mj771Sd298c&t=1147s) Validarea tuturor senzorilor extrasă; [00:19:44](https://www.youtube.com/watch?v=mj771Sd298c&t=1184s) extragerea pazei luată în considerare.
- [00:21:29](https://www.youtube.com/watch?v=mj771Sd298c&t=1289s) Flux de lucru cu niveluri mixte simplificat prin eliminarea duplicatelor.
- [00:22:41](https://www.youtube.com/watch?v=mj771Sd298c&t=1361s) Configurare făcută explicită; [00:22:50](https://www.youtube.com/watch?v=mj771Sd298c&t=1370s) validare deținută de obiectul de setări propusă.
- [00:24:41](https://www.youtube.com/watch?v=mj771Sd298c&t=1481s) Sortare pe loc și [00:25:18](https://www.youtube.com/watch?v=mj771Sd298c&t=1518s) comportamentul referințelor partajate.
- [00:26:38](https://www.youtube.com/watch?v=mj771Sd298c&t=1598s) Înveliș de indicator cu nume semantic luat în considerare.
- [Sursa de prezentare conexă la referința imutabilă](https://github.com/AntonC9018/uniCourse_csharp/blob/7e53c72e223132cbd1278019f2b05d3654e658f5/presentations/source-notes.md) — exemple ulterioare de sprijin care disting validarea în timpul execuției de aserțiuni și accesul numai pentru citire de mutarea obiectului partajat; aceasta nu este o captură exactă a înregistrării.

O mostră compactă a acestor corecții conexe, alături de referința lor imutabilă:

```csharp
if (minInclusive > maxInclusive)
    throw new ArgumentException("Bounds are reversed.");

IReadOnlyList<string> view = owner;
SortInPlace(owner); // The same object's contents change for view too.
```

Această pază de interval de sprijin permite limite inclusive egale, spre deosebire de precondiția strictă inițială aleasă în înregistrare.