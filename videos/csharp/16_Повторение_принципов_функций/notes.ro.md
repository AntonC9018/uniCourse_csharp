> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Revizuirea modului în care funcționează funcțiile

[00:00:00](https://www.youtube.com/watch?v=L62Og-Y4poE&t=0s) O funcție grupează instrucțiuni sub un nume. Această revizuire urmărește ce se întâmplă când acel nume este apelat: argumentele devin valori ale parametrilor locali, corpul se execută, iar execuția revine la apelant. Apoi face legătura între funcțiile locale și metodele de clasă și explică modul în care o metodă de instanță primește obiectul său.

Vocabularul folosit pe tot parcursul lecției este:

- **Funcție, declarație, corp și instrucțiune:** o funcție este o lucrare cu nume; declarația ei definește acea lucrare; corpul ei este blocul dintre acolade; o instrucțiune este o acțiune executată de program.
- **Apel, apelant, loc de apel și flux de control:** un apel pornește execuția unei funcții; apelantul este codul care face apelul; locul de apel este poziția sa; fluxul de control este ordinea în care se execută instrucțiunile.
- **Tip de retur, `void`, `int` și `return`:** tipul de retur descrie rezultatul unui apel; `void` înseamnă că nu există valoare de rezultat; `int` este tipul întreg folosit aici; `return` încheie apelul și, pentru o funcție care returnează o valoare, furnizează rezultatul.
- **Parametru, argument și variabilă locală:** un parametru este o intrare cu nume din declarație; un argument este o expresie furnizată la apel; o variabilă locală aparține unui anumit bloc sau unei anumite invocări de funcție.
- **Declarare, inițializare și atribuire a unei variabile:** declararea introduce variabila, inițializarea furnizează valoarea sa inițială, iar atribuirea memorează o valoare în ea.
- **Expresie, operand, operator și evaluarea expresiei:** o expresie calculează o valoare; un operand este o intrare pentru un operator precum `+` sau `*`; evaluarea efectuează calculele necesare obținerii valorii.
- **Abstractizare:** un nume și o interfață permit apelantului să folosească o operație fără să-i inspecteze implementarea.
- **Domeniu de vizibilitate și umbrire (shadowing):** domeniul de vizibilitate este regiunea în care poate fi folosit un nume; umbrire apare când un nume interior ascunde un nume exterior.
- **Argumente poziționale și argumente cu nume:** argumentele poziționale se leagă după poziția parametrului; argumentele cu nume folosesc `parameterName: expression` pentru a identifica parametrul destinație.
- **Funcție locală, captură și atribuire definită:** o funcție locală este declarată în interiorul unui cod executabil care o cuprinde; captura îi oferă acces la o variabilă din jurul ei; atribuirea definită înseamnă că compilatorul poate stabili că o variabilă are o valoare înainte de a fi citită.
- **`static`, intrare prin valoare, acces prin referință și lowering-ul compilatorului:** `static` împiedică o funcție locală să captureze variabilele locale din jurul ei sau starea de instanță; parametrii obișnuiți primesc valori; accesul prin referință ajunge la o variabilă existentă; lowering-ul este transformarea făcută de compilator din sintaxă sursă confortabilă într-o implementare.
- **Clasă, membru, metodă, câmp și modificator de acces:** o clasă descrie un tip; un membru aparține acelui tip; o metodă este o funcție declarată ca membru; un câmp memorează date; un modificator de acces precum `public` controlează unde poate fi folosit un membru.
- **Clasă statică, metodă statică și câmp static:** o clasă statică nu are instanțe; o metodă statică nu are un receptor de instanță; un câmp static aparține tipului, nu unui obiect individual.
- **Instrucțiuni de nivel superior, punct de intrare, `Program`, `Main` și `args`:** instrucțiunile de nivel superior sunt cod executabil scris în afara unei metode explicite; punctul de intrare pornește programul; `Program` și `Main` exprimă clasa și metoda de intrare convenționale; `args` reprezintă argumentele din linia de comandă.
- **Obiect, instanță, receptor, parametru ascuns și `this`:** un obiect este o instanță a unui tip; receptorul este obiectul pe care este apelată metoda; o metodă de instanță primește conceptual acel obiect ca parametru ascuns; `this` denumește receptorul curent.
- **Sintaxa folosită în exemple:** parantezele delimitează listele de parametri sau argumente, acoladele delimitează corpurile, punctul-virgulă încheie o instrucțiune, iar punctul selectează un membru. `Console.WriteLine` afișează un rând; textul între ghilimele este un șir de caractere; `new` creează un obiect.

Pentru exerciții, documentele conexe din curs sunt [laboratorul despre funcții](../../../labs/1_basic/05_functions.md), [laboratorul despre clasa statică](../../../labs/1_basic/15_static_class.md) și [laboratorul despre metodele de instanță](../../../labs/1_basic/16_method.md). Acestea sunt documente de practică, nu instantanee ale demonstrației.

Revizuirea presupune lecțiile anterioare despre variabile, tipuri, obiecte și referințe. [00:00:20](https://www.youtube.com/watch?v=L62Og-Y4poE&t=20s) Sensul lui `static` este amânat în revizuirea introductivă a sintaxei și este tratat mai târziu în acest videoclip. [00:12:59](https://www.youtube.com/watch?v=L62Og-Y4poE&t=779s) Scopul unui rezultat întreg al punctului de intrare și detaliile argumentelor din linia de comandă sunt lăsate pentru studiere ulterioară. Accesul la rețea, citirea fișierelor și citirea de la consolă sunt doar motivări aici, nu implementări predate în această înregistrare.

Exemplele de mai jos sunt reconstrucții scurte ale stărilor din videoclipul legat, cu nume explicative unde este necesar. Nu a fost identificat niciun instantaneu exact de sursă, comise, al demonstrației; traseul istoricului codului înregistrează deci stările din videoclip.

## Grupează două instrucțiuni sub un singur nume

[00:00:14](https://www.youtube.com/watch?v=L62Og-Y4poE&t=14s) Pornește de la două instrucțiuni independente:

```csharp
Console.WriteLine("Hello World");
Console.WriteLine("Hello You");
```

O **instrucțiune** efectuează o acțiune. Fiecare apel `Console.WriteLine` afișează șirul dintre ghilimele pe rândul său. O **funcție** permite ca aceste două acțiuni să fie numite împreună, astfel încât apelantul să poată cere întreaga operație după nume.

[00:00:24](https://www.youtube.com/watch?v=L62Og-Y4poE&t=24s) Mută-le într-o **declarație** de funcție, care definește operația:

```csharp
static void Greet()
{
    Console.WriteLine("Hello World");
    Console.WriteLine("Hello You");
}
```

Citește declarația în ordinea construirii:

1. Lasă deocamdată modificatorul `static` deoparte.
2. Scrie **tipul de retur**, `void`. Această funcție doar execută comenzi; nu produce nicio valoare de rezultat pentru o expresie.
3. Scrie numele, aici `Greet`.
4. [00:00:34](https://www.youtube.com/watch?v=L62Og-Y4poE&t=34s) Adaugă parantezele pentru **lista de parametri**. Lista este goală deoarece această operație nu are nevoie de intrări.
5. Așază instrucțiunile între acolade. Acest bloc este **corpul** funcției.

Afișarea rezultatului este un efect al funcției, dar nu face ca funcția să returneze o valoare. De aceea `void` rămâne potrivit.

## Apelează funcția și revino la apelant

[00:00:41](https://www.youtube.com/watch?v=L62Og-Y4poE&t=41s) Rularea stării care conține doar declarația nu produce nicio ieșire de salut. Definirea lucrului nu îi execută corpul.

[00:00:55](https://www.youtube.com/watch?v=L62Og-Y4poE&t=55s) Adaugă un **apel** scriind numele funcției urmat de paranteze și de punct-virgulă:

```csharp
Greet();

static void Greet()
{
    Console.WriteLine("Hello World");
    Console.WriteLine("Hello You");
}
```

Parantezele trebuie să fie prezente chiar și atunci când nu se pasează argumente. Codul care conține `Greet();` este **apelantul**, iar acel rând este **locul de apel**.

[00:01:04](https://www.youtube.com/watch?v=L62Og-Y4poE&t=64s) **Fluxul de control**, adică ordinea de execuție, este:

1. Ajungi la apel și intri în funcția cu acel nume.
2. Execuți prima ei instrucțiune, apoi a doua.
3. [00:01:12](https://www.youtube.com/watch?v=L62Og-Y4poE&t=72s) Revii la apelant și continui imediat după apel.

[00:01:20](https://www.youtube.com/watch?v=L62Og-Y4poE&t=80s) Adaugă acum un al doilea apel. Aceasta este starea intermediară importantă: există în continuare o singură declarație, dar două cereri de execuție a ei.

```csharp
Greet();
Greet();
// Aceeași declarație Greet urmează mai jos.
```

[00:01:27](https://www.youtube.com/watch?v=L62Og-Y4poE&t=87s) Urmărirea acestor apeluri dă:

```text
Hello World
Hello You
Hello World
Hello You
```

[00:01:32](https://www.youtube.com/watch?v=L62Og-Y4poE&t=92s) Odată ce după ultimul apel nu mai există instrucțiuni ale apelantului, programul se încheie. Declarația nu provoacă o altă execuție a corpului doar pentru că apare mai târziu în fișier.

[00:01:40](https://www.youtube.com/watch?v=L62Og-Y4poE&t=100s) O declarație definește o entitate distinctă, apelabilă; întâlnirea textului ei nu este o cerere de a rula instrucțiunile sale. Mai precis, C# clasifică într-adevăr declarația unei funcții locale drept instrucțiune de declarație, dar nu execută corpul declarat în acea poziție. [Specificația instrucțiunilor C#](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/statements#1364-local-function-declarations).

[00:01:53](https://www.youtube.com/watch?v=L62Og-Y4poE&t=113s) O funcție `void` se poate încheia fără un `return;` explicit. Atingererea acoladei ei de închidere înapoiază controlul apelantului, ca și cum s-ar fi furnizat un `return;` final. Aici **return** înseamnă părăsirea apelului curent; nu există valoare de rezultat de întors.

<details>
<summary>Mutarea declarației deasupra celor două apeluri face ca saluturile să se afișeze de trei ori?</summary>

Nu. Doar cele două apeluri execută corpul. Poziția sa textuală nu adaugă o altă invocare.

</details>

## Adaugă parametri și o valoare de retur

[00:02:19](https://www.youtube.com/watch?v=L62Og-Y4poE&t=139s) O funcție poate atât să primească intrări, cât și să returneze o valoare. Înlocuiește exemplul fără intrări, cu `void`, printr-un calcul întreg:

```csharp
int sum = Compute(1, 2);

static int Compute(int a, int b)
{
    int red = a + b;
    return red;
}
```

[00:02:23](https://www.youtube.com/watch?v=L62Og-Y4poE&t=143s) Un **parametru** este o intrare cu nume, scrisă în parantezele declarației. Aici parametrii sunt `int a` și `int b`; o virgulă îi separă. Pentru fiecare apel, fiecare parametru acționează ca o **variabilă locală** nouă, aparținând acelei invocări.

[00:02:33](https://www.youtube.com/watch?v=L62Og-Y4poE&t=153s) Un **argument** este o expresie furnizată la locul de apel. În `Compute(1, 2)`, argumentele au valorile 1 și 2. Aceste valori inițializează variabilele locale ale parametrilor: `a` primește 1, iar `b` primește 2.

[00:02:44](https://www.youtube.com/watch?v=L62Og-Y4poE&t=164s) **Tipul de retur** este acum `int`, tipul întreg. Prin urmare, apelul poate apărea într-o **expresie**, o calculare care produce o valoare. Expresia `Compute(1, 2)` produce întregul pe care funcția îl returnează. **Instrucțiunea de retur** `return red;` furnizează acea valoare și încheie această invocare.

## Urmărește un apel de la declarație la atribuire

[00:02:49](https://www.youtube.com/watch?v=L62Og-Y4poE&t=169s) Singurul rând `int sum = Compute(1, 2);` combină trei acțiuni conceptuale: declararea unei variabile, evaluarea unei expresii și atribuirea rezultatului ei. Descompunerea de mai jos face vizibile aceste acțiuni; este o transformare explicativă a sursei, nu o afirmație despre instrucțiunile exacte ale mașinii:

```csharp
int sum;                // Declarare: nu s-a atribuit încă nicio valoare.
sum = Compute(1, 2);    // Evaluează apelul, apoi îi atribuie rezultatul.

static int Compute(int a, int b)
{
    int red = a + b;
    return red;
}
```

[00:03:06](https://www.youtube.com/watch?v=L62Og-Y4poE&t=186s) **Declararea** unei variabile introduce o zonă de stocare pentru o valoare. Fără un inițializator, variabila locală este inițial neatribuită; „golul” este un model didactic, nu o valoare întreagă suplimentară și nici un zero automat. C# cere **atribuire definită** înainte ca variabila locală să poată fi citită. [Regulile pentru variabilele locale în C#](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/statements#1362-local-variable-declarations).

[00:03:09](https://www.youtube.com/watch?v=L62Og-Y4poE&t=189s) **Evaluarea expresiei** calculează valoarea din dreapta. Aceste argumente sunt deja constante cunoscute; exemplele ulterioare necesită mai multă muncă înainte de apelarea funcției.

Urmește întregul apel:

1. [00:03:26](https://www.youtube.com/watch?v=L62Og-Y4poE&t=206s) Pregătește valorile argumentelor 1 și 2. Creează variabilele locale ale parametrilor `a` și `b`, inițializate cu copii ale acestor valori.
2. [00:03:33](https://www.youtube.com/watch?v=L62Og-Y4poE&t=213s) Intră în prima instrucțiune a lui `Compute`.
3. [00:03:37](https://www.youtube.com/watch?v=L62Og-Y4poE&t=217s) Declară variabila locală `red` și evaluează `a + b`.
4. [00:03:44](https://www.youtube.com/watch?v=L62Og-Y4poE&t=224s) Identificatorii `a` și `b` se referă la variabilele de parametru ale acestui apel, deci adunarea folosește 1 și 2.
5. [00:03:52](https://www.youtube.com/watch?v=L62Og-Y4poE&t=232s) Înlocuiește adunarea cu rezultatul ei, 3, și memorează 3 în `red`. Aceasta este **inițializarea** lui `red`.
6. [00:04:02](https://www.youtube.com/watch?v=L62Og-Y4poE&t=242s) Execută `return red;`. Evaluarea lui `red` citește valoarea stocată acolo în acel moment, 3; returnarea acestei valori o face rezultatul acestui apel anume.
7. [00:04:27](https://www.youtube.com/watch?v=L62Og-Y4poE&t=267s) Apelantul poate continua acum cu rezultatul expresiei de apel. Conceptual, expresia sa din dreapta a devenit valoarea simplă 3.
8. [00:04:47](https://www.youtube.com/watch?v=L62Og-Y4poE&t=287s) Memorează acea valoare în `sum`. Această stocare finală este **atribuirea**; `sum` conține acum 3.

Variabila locală `red` nu este transferată ca atare apelantului. Este returnată valoarea sa curentă. Dacă corpul ar fi modificat `red` înainte de a ajunge la `return red;`, s-ar returna valoarea din acel moment.

<details>
<summary>De ce faptul că stocăm adunarea în red îi permite în continuare apelului să producă 3?</summary>

Adunarea îl inițializează pe red cu 3, apoi return citește valoarea curentă a lui red și o folosește ca rezultat al apelului. O expresie de retur poate citi o variabilă; nu trebuie să repete operația aritmetică inițială.

</details>

## Combină rezultatele apelurilor într-o expresie mai mare

[00:05:05](https://www.youtube.com/watch?v=L62Og-Y4poE&t=305s) Schimbă apelantul astfel încât să adune rezultatele a două apeluri:

```csharp
int sum = Compute(1, 2) + Compute(2, 3);

static int Compute(int a, int b)
{
    int red = a + b;
    return red;
}
```

Un **operator** precum `+` are nevoie de valorile **operandelor** săi, expresiile din cele două părți ale sale. Prin urmare, fiecare apel trebuie să își furnizeze valoarea înainte ca adunarea exterioară să se poată încheia.

1. Evaluează primul apel: returnează 3.
2. Evaluează al doilea apel folosind propriile sale variabile locale de parametru: returnează 5.
3. [00:05:21](https://www.youtube.com/watch?v=L62Og-Y4poE&t=321s) Evaluează `3 + 5`, obținând 8.
4. Atribuie valoarea finală 8 lui `sum`.

[00:05:35](https://www.youtube.com/watch?v=L62Og-Y4poE&t=335s) Pentru aceste argumente constante și acest corp, atribuirea valorii întregii expresii are același efect final asupra lui `sum` ca atribuirea directă a lui 8. Aceasta nu înseamnă că înlocuirea apelurilor arbitrare cu constante păstrează tot comportamentul programului.

[00:05:40](https://www.youtube.com/watch?v=L62Og-Y4poE&t=340s) Funcțiile devin utile când o valoare nu este fixată în avans: poate veni de la intrarea utilizatorului, dintr-un răspuns de rețea sau dintr-un fișier. [00:05:52](https://www.youtube.com/watch?v=L62Og-Y4poE&t=352s) Corpurile lor pot face, de asemenea, mai mult decât o simplă adunare. Numele oferă o **abstrație**: apelantul cere o operație prin interfața ei fără să-i vadă implementarea. Un nume descriptiv ajută cititorul să înțeleagă acea cerere.

Demonstrația ulterioară numește operația `SumOperation`; următorul exemplu folosește acest nume, păstrând în schimb corpul cu adunarea simplă, ca pașii evaluării să rămână vizibili.

## Evaluează argumentele înainte de a intra în funcție

[00:06:04](https://www.youtube.com/watch?v=L62Og-Y4poE&t=364s) O expresie mai mare este evaluată și ea obținând valorile expresiilor mai mici din ea. Schimbarea unei constante într-o variabilă sau într-un argument aritmetic adaugă muncă înaintea apelului; nu schimbă ce înseamnă un apel.

[00:06:30](https://www.youtube.com/watch?v=L62Og-Y4poE&t=390s) Această reconstrucție simplificată păstrează succesiunea și valorile finale din înregistrare:

```csharp
int a = 9;
int sum = SumOperation(1, a * 2 + 10) + SumOperation(3, a);

static int SumOperation(int a, int b)
{
    int red = a + b;
    return red;
}
```

Un **argument** este o expresie din parantezele apelului. Valoarea lui trebuie obținută înainte de a începe corpul funcției. [00:06:41](https://www.youtube.com/watch?v=L62Og-Y4poE&t=401s) Pentru primul apel, 1 este deja o valoare. Al doilea argument necesită mai multă evaluare.

[00:06:49](https://www.youtube.com/watch?v=L62Og-Y4poE&t=409s) O expresie-variabilă precum `a` produce valoarea stocată în acea variabilă. Nu transmite numele variabilei sau textul sursă neevaluat unui parametru întreg obișnuit.

[00:06:57](https://www.youtube.com/watch?v=L62Og-Y4poE&t=417s) Un argument poate fi și o expresie complexă. O expresie precum `a * 10` este validă; starea de lucru de aici folosește `a * 2 + 10`. [00:07:04](https://www.youtube.com/watch?v=L62Og-Y4poE&t=424s) În ambele cazuri, evaluarea o reduce mai întâi la o singură valoare, care o inițializează variabila locală a parametrului corespunzător.

Urmește în ordine starea de lucru:

1. [00:07:21](https://www.youtube.com/watch?v=L62Og-Y4poE&t=441s) Al doilea argument are nevoie de o înmulțire și o adunare. Citirea lui `a` din apelant dă 9.
2. [00:07:35](https://www.youtube.com/watch?v=L62Og-Y4poE&t=455s) Evaluează `9 * 2` pentru a obține 18, apoi `18 + 10` pentru a obține 28.
3. Apelează `SumOperation(1, 28)`. Propriile sale variabile locale de parametru primesc 1 și 28; corpul returnează 29.
4. [00:07:51](https://www.youtube.com/watch?v=L62Og-Y4poE&t=471s) Evaluează argumentele celui de-al doilea apel: 3 și `a` din apelant, a cărui valoare este tot 9. `SumOperation(3, 9)` returnează 12.
5. Evaluează adunarea exterioară, `29 + 12`, pentru a obține 41. Memorează 41 în `sum`.

Această procedură este **evaluarea expresiei**. Păstrează-i nivelurile separate: 28 este argumentul calculat, 29 este rezultatul primului apel, iar 41 este rezultatul întregii expresii.

<details>
<summary>Funcția primește expresia a * 2 + 10 sau numărul 28?</summary>

Primește 28 în al doilea parametru al său. Apelantul evaluează expresia argumentului înainte de a începe corpul.

</details>

## Separă domeniile de vizibilitate și numește argumentele

[00:08:11](https://www.youtube.com/watch?v=L62Og-Y4poE&t=491s) Un **domeniu de vizibilitate**, numit și zonă de vizibilitate, stabilește unde poate fi folosit un nume. `a` din apelant și un parametru numit `a` sunt variabile distincte. Scrierea lor identică nu le face să fie aceeași locație de stocare.

În această versiune scurtă a stării precedente, apelul furnizează o valoare parametrului, în loc de a partaja variabila întreagă a apelantului:

```csharp
int a = 9;
int result = SumOperation(1, a);

static int SumOperation(int a, int b)
{
    return a + b;
}
```

`a` din apelant furnizează 9 celui de-al doilea parametru, `b`. În interiorul corpului, numele `a` înseamnă primul parametru, a cărui valoare este 1.

[00:08:24](https://www.youtube.com/watch?v=L62Og-Y4poE&t=504s) **Argumentele poziționale** se leagă după poziția din lista declarată a parametrilor. **Argumentele cu nume** identifică destinația printr-un nume de parametru și un două puncte:

```csharp
SumOperation(1, 9);        // Pozițional: a = 1, b = 9.
SumOperation(a: 1, b: 9);  // Cu nume: aceeași legare.
SumOperation(b: 9, a: 1);  // Cu nume: aceeași legare, ordine inversă în sursă.
```

[00:08:38](https://www.youtube.com/watch?v=L62Og-Y4poE&t=518s) Numirea argumentelor elimină nevoia de a reproduce ordinea parametrilor la locul de apel. [00:08:52](https://www.youtube.com/watch?v=L62Og-Y4poE&t=532s) Numele explicit controlează legarea: scriind `b: 9` primul, furnizezi în continuare 9 lui `b`, al doilea parametru declarat. Primul argument scris nu devine parametrul `a` doar pentru că apare primul.

Formulările „primul” și „al doilea” din înregistrare trebuie citite ținând cont de această distincție. Legarea și ordinea evaluării sunt lucruri diferite: expresiile argumentelor sunt evaluate în ordinea în care sunt scrise, în timp ce numele determină parametrii destinație. [Regulile Microsoft pentru argumentele cu nume](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments).

## Fă explicită cu static dependențele locale

[00:09:01](https://www.youtube.com/watch?v=L62Og-Y4poE&t=541s) Până acum, declarația este o **funcție locală**, o funcție imbricată în cod executabil care o cuprinde. O **funcție locală statică** nu poate **capta**, adică folosi direct, variabilele locale ale blocului care o cuprinde sau starea de instanță. Intrările sale locale obișnuite trebuie furnizate explicit.

```csharp
int outside = 9;
int sum = Add(outside, 1);

static int Add(int a, int b)
{
    return a + b;
}
```

Apelul furnizează vizibil valoarea lui `outside`. Încercarea de a citi direct `outside` în interiorul acestei funcții locale statice este respinsă. Prin urmare, `static` ajută la păstrarea vizibilă a dependențelor față de variabilele locale din jur, în lista de argumente; nu le ascunde. [Regulile pentru funcțiile locale statice](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions).

[00:09:13](https://www.youtube.com/watch?v=L62Og-Y4poE&t=553s) Eliminarea lui `static` permite accesul la variabilele locale din jur, dar compilatorul trebuie totuși să stabilească că o variabilă capturată este atribuită înaintea unui apel care o citește. De exemplu, această stare intermediară explicativă este invalidă:

```csharp
int sum;
sum = ReadSum();       // Invalid: ReadSum ar citi un sum neatribuit.

int ReadSum()
{
    return sum;
}
```

Problema este o eroare de **atribuire definită** detectată la compilare, nu permisiunea de a rula o citire a unei variabile locale neinițializate.

[00:09:27](https://www.youtube.com/watch?v=L62Og-Y4poE&t=567s) Inițializează variabila din jur înaintea primului apel care o citește:

```csharp
int sum = 0;
sum = ReadSum();       // Valid: sum capturat are o valoare.

int ReadSum()
{
    return sum;
}
```

Captura este acum permisă: `ReadSum` citește `sum`-ul existent. Atribuirea definită privește dacă citirea are o valoare disponibilă; declarația funcției poate apărea în continuare mai târziu în fișier.

<details>
<summary>De ce declararea lui ReadSum mai devreme nu repară primul exemplu?</summary>

Declarația nu îl inițializează pe sum. Înaintea unui apel care citește sum, trebuie atribuită o valoare lui sum.

</details>

## Rezolvă umbrirea folosind nume distincte

[00:09:42](https://www.youtube.com/watch?v=L62Og-Y4poE&t=582s) Un parametru al unei funcții locale poate avea același nume ca o variabilă locală din jur, rămânând totuși o entitate distinctă. **Umbrirea** înseamnă că numele interior are prioritate la rezolvarea unui identificator:

```csharp
int a = 9;
int result = SumOperation(1, 2);  // Rezultat: 3.

int SumOperation(int a, int b)
{
    return a + b;                // Citește parametrii 1 și 2.
}
```

[00:09:50](https://www.youtube.com/watch?v=L62Og-Y4poE&t=590s) În interiorul acestui corp, numele simplu `a` se referă la parametru, deci nu îl poate selecta pe cel exterior `a`. Variabila exterioară există în continuare; este ascunsă de acel nume.

[00:10:02](https://www.youtube.com/watch?v=L62Og-Y4poE&t=602s) Înregistrarea descrie mesajul editorului ca fiind „a este o variabilă locală cu același nume”. Păstrează distincția dintre ascunderea numelui și o eroare reală de compilator: începând cu C# 8, un parametru al unei funcții locale poate umbri în mod legal o variabilă locală din jur. Descrierea de către un editor a entității cu același nume nu este suficientă pentru a stabili că acest exemplu legal nu compilează. [Regulile de umbrire pentru funcțiile locale](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions).

[00:10:08](https://www.youtube.com/watch?v=L62Og-Y4poE&t=608s) Căutarea numelui alege parametrul. [00:10:23](https://www.youtube.com/watch?v=L62Og-Y4poE&t=623s) Dă unei variabile un nume distinct, precum `a1`, atunci când ambele valori trebuie să fie identificabile:

```csharp
int a = 9;
int result = SumOperation(1, 2);  // Această versiune returnează 12.

int SumOperation(int a1, int b)
{
    return a1 + b + a;           // Parametrul a1, parametrul b, a exterior capturat.
}
```

Această variantă explicativă face distincția explicită: 1 + 2 + 9 este 12. Redenumirea singură nu trebuie să adauge o dependență; folosirea exterioară a lui `a` aici este deliberată.

[00:10:32](https://www.youtube.com/watch?v=L62Og-Y4poE&t=632s) Dacă funcția are nevoie doar de valorile parametrilor săi, păstrează corpul cu adunarea pură și restabilește `static`. Aceasta împiedică capturarea accidentală:

```csharp
static int SumOperation(int a1, int b)
{
    return a1 + b;
}
```

## Înțelege captura ca acces la o variabilă partajată

[00:10:44](https://www.youtube.com/watch?v=L62Og-Y4poE&t=644s) Elimină primul parametru și citește în schimb o variabilă exterioară. Rezultatul funcției depinde acum de starea din jur, chiar dacă acea stare lipsește din lista sa explicită de argumente:

```csharp
int a = 9;
int result = SumOperation(2); // 11.

int SumOperation(int b)
{
    return a + b;
}
```

Aici **captura** înseamnă accesul la variabila `a` din jur; nu înseamnă crearea unui parametru fără legătură, cu un întreg copiat.

[00:10:56](https://www.youtube.com/watch?v=L62Og-Y4poE&t=656s) Înregistrarea explică **lowering-ul compilatorului** folosind un parametru suplimentar prin referință. Lowering-ul este o transformare de implementare efectuată de compilator. Următorul cod este un echivalent conceptual, nu C#-ul generat exact și nicio modificare de sursă obligatorie:

```csharp
int a = 9;
int result = SumOperationLowered(ref a, 2);

static int SumOperationLowered(ref int capturedA, int b)
{
    return capturedA + b;
}
```

În acest model, `ref` furnizează **acces prin referință** la variabila existentă. Un parametru întreg obișnuit primește o valoare; modelul cu referință poate ajunge la stocarea apelantului.

[00:11:07](https://www.youtube.com/watch?v=L62Og-Y4poE&t=667s) Partajarea variabilei contează, fiindcă o funcție care capturează ar putea atribui valori ei. Copierea valorii sale într-un întreg independent ar pierde acea actualizare. Ca să expui diferența, extinde exemplul inițial cu captură:

```csharp
int a = 9;
Increase();
Console.WriteLine(a); // 10.

void Increase()
{
    a = a + 1;
}
```

[00:11:20](https://www.youtube.com/watch?v=L62Og-Y4poE&t=680s) Continui să scrii apelul funcției locale originale; compilatorul aranjează accesul la captură. [00:11:32](https://www.youtube.com/watch?v=L62Og-Y4poE&t=692s) Conceptual, acesta poate așeza corpul într-un ajutor cu acces suplimentar la starea capturată, în loc ca ajutorul să ajungă printr-o magie la un bloc sursă care o cuprinde. Nu este garantat că emite un parametru `ref` literal pentru fiecare variabilă sau o funcție locală statică la nivel de sursă. Comportamentul observabil esențial este accesul partajat la variabila capturată. [Explicația Microsoft a implementării capturii](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions).

Înregistrarea lasă explicit neclară posibilitatea optimizărilor de copiere. Ele nu sunt necesare pentru a raționa rezultatele programului, iar structura exactă a stocării generate este în afara acestei revizuiri.

## Mută funcția într-o clasă

[00:11:45](https://www.youtube.com/watch?v=L62Og-Y4poE&t=705s) O funcție declarată ca **membru** al unei clase se numește **metodă**. Un membru aparține tipului, în loc de a fi local blocului executabil care o cuprinde.

[00:11:57](https://www.youtube.com/watch?v=L62Og-Y4poE&t=717s) Mută funcția de adunare într-o clasă statică și dă metodei modificatorul de acces `public`:

```csharp
int sum = Functions.SumOperation(1, 2);

static class Functions
{
    public static int SumOperation(int a, int b)
    {
        return a + b;
    }
}
```

Urmează schimbările:

1. Așază declarația în interiorul corpului clasei.
2. Păstrează `static`: o **clasă statică** nu are instanțe și poate conține doar metode statice.
3. Adaugă `public`, un **modificator de acces** care permite apelanților din afara acestei clase să folosească metoda. O metodă de clasă fără modificator de acces este privată în mod implicit; `public` este alegerea folosită aici, nu singurul nivel de acces pe care C# îl suportă.
4. [00:12:08](https://www.youtube.com/watch?v=L62Og-Y4poE&t=728s) Actualizează apelantul la `Functions.SumOperation(1, 2)`. Numele clasei, punctul și numele metodei identifică operația.

[00:12:14](https://www.youtube.com/watch?v=L62Og-Y4poE&t=734s) O **metodă statică** nu are receptor de obiect și este adesea numită, în mod colocvial, funcție. [00:12:20](https://www.youtube.com/watch?v=L62Og-Y4poE&t=740s) Totuși, ea poate citi **câmpuri statice**, date care aparțin tipului, deci `static` nu promite că rezultatul ei depinde doar de argumentele explicite. Acesta este un obiectiv de proiectare pentru exemplul acesta.

[00:12:34](https://www.youtube.com/watch?v=L62Og-Y4poE&t=754s) Preferă parametrii în locul variabilelor statice atunci când furnizezi informații unei operații. Parametrii fac fluxul de date vizibil acolo unde este scris apelul, în loc să impună cititorului să caute o stare partajată.

## Recunoaște punctul de intrare din spatele codului de nivel superior

[00:12:43](https://www.youtube.com/watch?v=L62Og-Y4poE&t=763s) Rândurile executabile din afara unei metode scrise explicit sunt **instrucțiuni de nivel superior**. Compilatorul furnizează un tip `Program` care le cuprinde și un **punct de intrare** static, metoda în care începe execuția. Înregistrarea prezintă aceasta prin modelul familiar `Main`.

Pentru apelul simplu de mai sus, structura echivalentă scrisă manual poate fi ilustrată astfel; este un model didactic, nu numele exact al metodei generate și nici nivelul ei de acces:

```csharp
class Program
{
    public static void Main(string[] args)
    {
        int sum = Functions.SumOperation(1, 2);
    }
}
// Clasa Functions din secțiunea precedentă furnizează SumOperation.
```

[00:12:59](https://www.youtube.com/watch?v=L62Og-Y4poE&t=779s) `Main` desemnează convențional o metodă de intrare scrisă explicit, iar `string[] args` reprezintă tabloul de șiruri din linia de comandă. Nu trebuie să scrii acest înveliș când folosești instrucțiuni de nivel superior. Lecția amână sensul unui rezultat întreg al punctului de intrare și nu dezvoltă procesarea argumentelor din linia de comandă.

[00:13:08](https://www.youtube.com/watch?v=L62Og-Y4poE&t=788s) Clasa `Program` generată nu este statică, în timp ce metoda sa de intrare este statică. O clasă nestatică poate conține metode statice; crearea unei instanțe de `Program` nu este necesară pentru execuția punctului de intrare.

Precizia contează: metoda de intrare generată nu trebuie să fie numită literal `Main` sau declarată `public`. Semnătura ei depinde de codul de nivel superior. Pentru exemplele sincrone de aici, fără valoare de retur a punctului de intrare, modelul este o metodă de intrare `void`; un rezultat întreg la nivel superior îi schimbă tipul de retur în `int`. Încapsularea cu întreg din înregistrare este deci o ilustrare, nu o ieșire universală a compilatorului. [Regulile Microsoft pentru punctul de intrare de nivel superior](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements).

## Dă obiectul unei metode de instanță

[00:13:21](https://www.youtube.com/watch?v=L62Og-Y4poE&t=801s) O **metodă de instanță** rulează pentru un anumit obiect. În această revizuire bazată pe clase, metodele de instanță cer o clasă nestatică; clasele statice nu le pot conține. [00:13:25](https://www.youtube.com/watch?v=L62Og-Y4poE&t=805s) Metodele statice pot fi, de asemenea, declarate pe clase nestatice.

[00:13:31](https://www.youtube.com/watch?v=L62Og-Y4poE&t=811s) Spre deosebire de o metodă statică, o metodă de instanță poate citi câmpurile receptorului său. Un **câmp** este o dată stocată într-un obiect; **receptorul** este obiectul concret pe care metoda este invocată. Gândește receptorul ca pe un **parametru ascuns** suplimentar: apelantul îl furnizează fără să îl numere printre argumentele explicite.

[00:13:49](https://www.youtube.com/watch?v=L62Og-Y4poE&t=829s) Construiește un obiect, stabilește câmpul său, apoi apelează metoda sa de instanță. Această reconstrucție scurtă folosește un `Animal` cu un câmp numit `H`:

```csharp
Animal animal = new Animal();
animal.H = 7;
int result = animal.AddToH(2); // 9.

class Animal
{
    public int H;

    public int AddToH(int amount)
    {
        return H + amount;
    }
}
```

1. `new Animal()` creează un **obiect**, o instanță a tipului `Animal`.
2. Stochează referința sa în `animal`.
3. Atribuie 7 câmpului `H` al acelui obiect.
4. [00:14:07](https://www.youtube.com/watch?v=L62Og-Y4poE&t=847s) Scrie `animal.AddToH(2)`: expresia receptorului, punctul, numele metodei și argumentele explicite.
5. Receptorul este transmis conceptual ca parametru ascuns al obiectului. Argumentul explicit 2 îl inițializează pe `amount`; citirea lui `H` de pe acest receptor furnizează 7, deci rezultatul este 9.

[00:13:39](https://www.youtube.com/watch?v=L62Og-Y4poE&t=819s) Parametrul ascuns reprezintă referința la obiect, nu o copie a întregului `Animal`. Diferite apeluri pot primi astfel obiecte diferite, chiar dacă execută același corp de metodă.

## Folosește expresii de receptor și this

[00:14:23](https://www.youtube.com/watch?v=L62Og-Y4poE&t=863s) Receptorul nu trebuie să fie un simplu nume de variabilă. Poate fi orice expresie care produce o referință potrivită la un obiect. [00:14:31](https://www.youtube.com/watch?v=L62Og-Y4poE&t=871s) Un obiect creat pe loc poate fi folosit imediat:

```csharp
int result = new Animal().AddToH(2);
```

Aceasta folosește aceeași declarație `Animal` ca în starea precedentă. [00:14:40](https://www.youtube.com/watch?v=L62Og-Y4poE&t=880s) Este legal să creezi un obiect în interiorul apelului și să nu păstrezi nicio referință după apel. Pentru această clasă simplificată, câmpul întreg `H` al unui obiect nou creat are inițial valoarea 0, deci această expresie returnează 2; o variabilă locală întreagă neinițializată explicit, discutată mai devreme, respectă alte reguli.

[00:14:46](https://www.youtube.com/watch?v=L62Og-Y4poE&t=886s) În interiorul unei metode de instanță, un acces necalificat la un câmp precum `H` citește câmpul care aparține receptorului acelei invocări. Nu înseamnă un câmp arbitrar al unui alt `Animal`.

[00:15:00](https://www.youtube.com/watch?v=L62Og-Y4poE&t=900s) Receptorul ascuns este disponibil sub cuvântul-cheie **`this`**, care înseamnă obiectul curent. Fă această legătură explicită schimbând corpul:

```csharp
public int AddToH(int amount)
{
    return this.H + amount;
}
```

Aici `H` și `this.H` selectează același câmp al receptorului curent. Punctul selectează câmpul din acel obiect.

[00:15:14](https://www.youtube.com/watch?v=L62Og-Y4poE&t=914s) La nivelul superior al apelantului, folosește expresia de obiect pe care o ai efectiv, precum `animal.H`; acolo nu există o instanță curentă care să fie numită cu `this`. În interiorul metodei, `this.H` se referă la obiectul furnizat pentru invocarea respectivă. Nu înseamnă permanent o variabilă a apelantului numită `animal`.

<details>
<summary>Dacă două obiecte Animal au valori H diferite, pe care îl citește AddToH?</summary>

Îl citește pe H de pe receptorul apelului. Pentru first.AddToH(2), this se referă la first; pentru second.AddToH(2), this se referă la second. Același corp rulează cu un alt receptor ascuns.

</details>

## Istoricul modificărilor de cod

Acestea sunt stările relevante de cod înregistrate, în ordinea lecției. Exemplele de mai sus reconstruiesc comportamentul lor esențial; commitul de adunare al laboratorului de practică nu este o dovadă pentru aceste stări ale demonstrației.

- [00:00:14](https://www.youtube.com/watch?v=L62Og-Y4poE&t=14s) Două instrucțiuni de afișare sunt grupate într-o singură funcție `void`, cu nume și fără parametri.
- [00:00:55](https://www.youtube.com/watch?v=L62Og-Y4poE&t=55s) Se adaugă un apel explicit; [00:01:20](https://www.youtube.com/watch?v=L62Og-Y4poE&t=80s) un al doilea apel demonstrează execuția repetată și revenirea la apelant.
- [00:02:19](https://www.youtube.com/watch?v=L62Og-Y4poE&t=139s) Parametrii întregi și o valoare locală returnată transformă un apel într-o expresie care produce o valoare.
- [00:05:05](https://www.youtube.com/watch?v=L62Og-Y4poE&t=305s) Două rezultate de apel sunt combinate prin adunare.
- [00:06:30](https://www.youtube.com/watch?v=L62Og-Y4poE&t=390s) Argumentele de tip variabilă și aritmetic sunt evaluate înaintea apelurilor; expresia de lucru se încheie la 41.
- [00:08:24](https://www.youtube.com/watch?v=L62Og-Y4poE&t=504s) Argumentele cu nume fac legarea parametrilor explicită, chiar și când sunt scrise în altă ordine.
- [00:09:01](https://www.youtube.com/watch?v=L62Og-Y4poE&t=541s) Funcțiile locale statice sunt contrastate cu accesul la variabilele din jur; [00:09:27](https://www.youtube.com/watch?v=L62Og-Y4poE&t=567s) inițializarea înaintea unei citiri capturate face validă starea corectată.
- [00:10:23](https://www.youtube.com/watch?v=L62Og-Y4poE&t=623s) Numele distincte clarifică umbrirea; [00:10:44](https://www.youtube.com/watch?v=L62Og-Y4poE&t=644s) eliminarea unui parametru expune captura, urmată de lowering-ul ei conceptual.
- [00:11:57](https://www.youtube.com/watch?v=L62Og-Y4poE&t=717s) Funcția locală este mutată într-o clasă statică și este apelată prin numele clasei.
- [00:12:43](https://www.youtube.com/watch?v=L62Og-Y4poE&t=763s) Codul de nivel superior este conectat la un punct de intrare furnizat de compilator.
- [00:13:49](https://www.youtube.com/watch?v=L62Og-Y4poE&t=829s) Un obiect este creat și furnizat ca receptor al unui apel de metodă de instanță.
- [00:14:31](https://www.youtube.com/watch?v=L62Og-Y4poE&t=871s) O expresie de obiect nou devine direct receptorul; [00:15:00](https://www.youtube.com/watch?v=L62Og-Y4poE&t=900s) `this.H` explicit identifică câmpul receptorului.