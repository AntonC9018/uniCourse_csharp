> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Controlul fluxului și expresiile booleene în C#

## Scopul lecției și vocabular

[00:00:00](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=0s) Scopul este explicarea sintaxei din spatele deciziilor și repetării, pe care lecțiile anterioare le-au folosit doar în treacăt. Un mic joc de „ghicește numărul” oferă problema: alegem un număr ascuns, citim o încercare, dăm un indiciu și, în cele din urmă, repetăm până când încercarea reușește.

[00:00:11](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=11s) O verificare produce o valoare care poate fi salvată și reutilizată. Înțelegerea acelei valori leagă cele două subiecte ale lecției: expresiile `bool` și controlul fluxului.

Vocabularul pe care îl vei întâlni este:

- **Controlul fluxului** determină ce instrucțiune se execută în continuare. O **instrucțiune** este o comandă de executat; un **bloc** grupează instrucțiuni în interiorul `{ }`. O **ramură** este o cale alternativă de execuție. **Indentarea** reprezintă spațiile de la începutul rândului, folosite pentru a face structura lizibilă.
- O **expresie** calculează o valoare. Un **operator** efectuează o operație asupra **operanzilor** săi, adică expresiile pe care le primește. **Atribuirea** (`=`) memorează o valoare; **egalitatea** (`==`) compară valori; **inegalitatea** (`!=`) verifică dacă diferă. **Operatorii relaționali** `<`, `>`, `<=` și `>=` compară ordinea.
- **`bool`**, abreviat de la *boolean*, este tipul cu cele două valori logice primitive **`true`** și **`false`**. O **expresie bool** calculează una dintre aceste valori. O **condiție** folosește acea valoare pentru a decide dacă se execută cod.
- **Negarea** (`!`) inversează o valoare bool. **ȘI-ul logic** (`&&`) cere ca ambii operanzi să fie adevărați; **SAU-ul logic** (`||`) cere ca cel puțin unul să fie adevărat. Evaluarea lor **scurt-circuit**, sau **leneșă**, sare peste un operand când rezultatul este deja cunoscut. Un **efect secundar**, precum afișarea, este o acțiune observabilă în timpul evaluării.
- **`if`** execută instrucțiunea asociată atunci când condiția sa este adevărată. **`else`** furnizează calea falsă. **`else if`** este un `if` folosit ca instrucțiune asociată unui `else`. **Imbricarea** pune o instrucțiune sau un bloc în interiorul altuia.
- O **buclă** repetă o instrucțiune sau un bloc, numit **corpul** ei. O **iterație** este o trecere. **`while`** verifică înaintea fiecărei treceri; **`while (true)`** nu are condiție de oprire proprie. **`do ... while`** verifică după corp, astfel încât corpul rulează cel puțin o dată. **`for`** este o altă formă de buclă, menționată în deschidere.
- **`break`** părăsește cea mai apropiată buclă care îl conține. **`continue`** sare peste restul iterației curente și trece la următoarea verificare a condiției în buclele folosite aici. **`return`** părăsește întreaga funcție, furnizând opțional rezultatul ei.
- O **variabilă** memorează o valoare de un anumit tip. **Declararea** o introduce; **inițializarea** îi dă o valoare inițială. **Atribuirea definită** înseamnă că compilatorul poate stabili că o variabilă locală are o valoare înainte de a fi citită. **Domeniul de vizibilitate** este regiunea în care numele ei este disponibil. O **santinelă** este o valoare artificială folosită pentru a marca o stare specială.
- O **funcție** este cod apelabil. Un **apel de funcție** își evaluează **argumentele**, valorile furnizate; **parametrii** primesc acele valori în interiorul funcției. **Valoarea de retur** a unei funcții este rezultatul ei.
- **`Random`** furnizează numere pseudorandom prin `Next`. **Parametrul său `maxValue`** este o limită superioară exclusivă. **Operatorul rest** `%` dă restul împărțirii.
- **Intrarea de la consolă** vine ca text. **Parsarea** încearcă să convertească acel text într-un număr. **`int.TryParse`** raportează succesul printr-un bool și scrie numărul printr-un **parametru `out`**, adică un parametru asignat de funcția apelată.

Pentru exerciții înrudite, folosește [laboratorul bool](../../../labs/1_basic/06_bool.md) și [laboratorul de control al fluxului](../../../labs/1_basic/07_flow_control.md) din repository.

**Domeniul:** deschiderea îi numește pe `if`, `while` și `for`, dar lecția desfășurată dezvoltă expresiile bool, `if/else`, `while`, `break`, `continue`, `do ... while` și reluarea citirii. Nu dezvoltă sintaxa lui `for`. Funcțiile, variabilele și intrarea de la consolă sunt unelte de sprijin, nu subiecte redate din nou în întregime. Prima ramură de parsare folosește valori bool înainte de explicația lor detaliată; ieșirea dintr-o buclă infinită este introdusă abia după buclă.

Exemplele de cod de mai jos sunt reconstrucții simplificate ale stărilor video cu marcaje de timp, cu mesaje în engleză și nume descriptive. Sursele laboratoarelor examinate sunt citite ca **exemple înrudite**, nu ca stări salvate exact ale jocului de ghicire.

## Alegerea numărului ascuns și citirea primei încercări

[00:00:26](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=26s) Începem alegând numărul ascuns. `Random` este generatorul de numere, iar `Next()` este un apel de funcție care obține un întreg nenegativ. Restul împărțirii lui la 100 limitează rezultatul la intervalul 0–99 inclusiv:

```csharp
var random = new Random();
int r = random.Next() % 100;
```

Aici `%` este operatorul rest. Numărul 100 este împărțitorul; 100 nu poate fi niciodată un rest.

[00:00:45](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=45s) Schimbă expresia de generare în `random.Next(100)`. Argumentul `100` ajunge în parametrul `maxValue` al funcției, o limită superioară exclusivă. Aceasta produce tot un număr de la 0 la 99:

```csharp
int r = random.Next(100);
```

[00:00:55](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=55s) Apoi citim text de la consolă și îl parsăm. Parsarea convertește o reprezentare textuală într-un întreg. `TryParse` oferă două rezultate: valoarea sa de retur bool spune dacă conversia a reușit, iar parametrul `out` primește întregul.

```csharp
string? input = Console.ReadLine();
if (!int.TryParse(input, out int num))
{
    Console.WriteLine("Not a number.");
    return;
}
```

Declarația `out int num` introduce variabila de ieșire. `!` negează valoarea de succes, așadar această ramură tratează eșecul. `return` iese din funcția care o conține. La [00:01:02](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=62s), aceasta este deliberat doar o primă întâlnire cu `bool` și `if`; mecanica lor este explicată după ce jocul poate face o comparație.

## O singură comparație și extragerea funcției de comparare

[00:01:10](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=70s) Prima ramură verifică egalitatea: dacă numărul introdus `num` este egal cu numărul ascuns `r`, afișăm succesul. La [00:01:23](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=83s), adăugăm comparațiile ordonate: o încercare sub numărul ascuns are nevoie de o încercare următoare mai mare; o încercare deasupra lui are nevoie de una mai mică.

```csharp
if (num == r)
{
    Console.WriteLine("Correct!");
}
else if (num < r)
{
    Console.WriteLine("Guess higher.");
}
else if (num > r)
{
    Console.WriteLine("Guess lower.");
}
```

`==` este operatorul de egalitate, iar `<` și `>` sunt operatori relaționali. Fiecare compară două valori și produce un bool. Structura `else if` va fi descompusă mai târziu; deocamdată exprimă cele trei rezultate ale jocului.

[00:01:33](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=93s) Extragem acest bloc de comparație într-o funcție proprie, ca să poată fi examinat separat. O funcție grupează cod apelabil; parametrii primesc valorile furnizate de apelant. Schimbarea structurală este:

```csharp
CompareGuess(num, r);

static void CompareGuess(int num, int r)
{
    if (num == r)
    {
        Console.WriteLine("Correct!");
    }
    else if (num < r)
    {
        Console.WriteLine("Guess higher.");
    }
    else if (num > r)
    {
        Console.WriteLine("Guess lower.");
    }
}
```

[00:01:41](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=101s) Execuția înregistrată așteaptă un număr de la consolă și apoi raportează dacă trebuie ghicit mai sus sau mai jos. Încă tratează doar o încercare. La [00:01:54](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=114s), lecția întrerupe construcția pentru a analiza `if` înainte de a adăuga o buclă infinită.

## Un `if` citește o expresie bool

[00:02:05](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=125s) Condiția unui `if` decide dacă instrucțiunea asociată se execută. În exemplul curent acea instrucțiune este un bloc, adică codul cuprins între acolade:

```csharp
if (num == r)
{
    Console.WriteLine("Correct!");
}
```

[00:02:16](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=136s) Evaluarea obține mai întâi valorile expresiei din stânga `num` și ale expresiei din dreapta `r`, apoi verifică dacă sunt egale.

[00:02:33](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=153s) Păstrează atribuirea și comparația distincte. Un singur `=` scrie o valoare într-o variabilă; dublu `==` întreabă dacă două valori se potrivesc. La [00:02:42](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=162s), egalitatea este identificată ca o operație de comparare a valorilor:

```csharp
num = r;            // atribuire: memorează valoarea lui r în num
bool same = num == r; // comparație: calculează o valoare logică
```

[00:02:49](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=169s) Operanzii operatorului sunt expresii. Citirea unei variabile este o expresie simplă, dar oricare dintre părți ar putea implica și o calculare. La [00:03:05](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=185s), comparația în sine este recunoscută ca expresie, deci rezultatul ei poate fi folosit și în altă parte. La [00:03:10](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=190s), acest rezultat este `true` pentru valori egale și `false` pentru valori diferite.

[00:03:35](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=215s) Memorăm rezultatul în loc să punem comparația direct în condiție. La [00:03:41](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=221s), citirea variabilei furnizează condiția:

```csharp
bool guessed = num == r;
if (guessed)
{
    Console.WriteLine("Correct!");
}
```

Valoarea salvată consemnează rezultatul comparației în momentul executării sale. Citirea lui `guessed` folosește decizia stocată; nu reexecută `num == r`.

[00:03:53](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=233s) Mai precis, `if` consumă o expresie bool, nu o categorie specială de „sintaxă de condiție”. La [00:04:01](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=241s), regula unificatoare este că expresia calculează `true` sau `false`: o comparație, o citire a unei variabile bool și un apel care returnează bool pot furniza toate această valoare.

## Evaluarea comparațiilor și memorarea valorilor bool

[00:04:10](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=250s) Egalitatea și inegalitatea se numără printre operatorii folosiți pentru a construi expresii logice. O expresie logică calculează o valoare de adevăr, așa cum este subliniat la [00:04:33](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=273s). La [00:04:36](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=276s), este numit tipul său C#: `bool`, de la *boolean*.

[00:04:48](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=288s) Urmărește atribuirea de la intrări până la rezultat. [Exemplele înrudite din laboratorul bool](https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/06_bool.md) ilustrează și comparații memorate în variabile; acest exemplu simplificat folosește valorile din video:

```csharp
int a = 5;
int b = 6;
bool c = a == b;
```

1. La [00:04:56](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=296s), citește valoarea curentă a fiecărei variabile: `a` furnizează 5, iar `b` furnizează 6.
2. Compară valorile obținute: `5 == 6`.
3. La [00:05:04](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=304s), comparația produce valoarea booleană primitivă `false`.
4. Atribuirea memorează acea valoare în `c`. Ea memorează rezultatul, nu textul sursă al expresiei.

[00:05:15](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=315s) Schimbă comparația în inegalitate. `!=` verifică dacă operanzii diferă; `5 != 6` dă `true`:

```csharp
bool c = a != b;
```

[00:05:30](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=330s) Aceeași verificare poate fi scrisă ca egalitate negată. La [00:05:37](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=337s), `!` este explicat ca negare: schimbă `true` în `false` și `false` în `true`.

```csharp
bool different = !(a == b); // aici echivalent cu a != b
```

Parantezele fac din expresia de egalitate operandul lui `!`. Mai întâi calculăm egalitatea, apoi inversăm rezultatul ei bool.

## Combinarea verificărilor cu operatorii de scurt-circuit

[00:05:56](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=356s) Fiecare comparație este o expresie logică, deci rezultatul ei poate fi combinat cu o altă expresie logică. Următorul exemplu întreabă atât dacă `a` diferă de `b`, cât și dacă `a` este egal cu șapte.

[00:06:18](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=378s) ȘI-ul logic, scris `&&`, cere ca ambele verificări să fie adevărate. Aceasta urmează aceeași operație ca [exemplele înrudite de ȘI](https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/06_bool.md):

```csharp
int a = 5;
int b = 6;
bool c = (a != b) && (a == 7);
```

1. La [00:06:35](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=395s), evaluează mai întâi operandul din stânga: `5 != 6` este `true`.
2. La [00:06:46](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=406s), ia în calcul alternativa: dacă operandul din stânga ar fi fals, ȘI-ul ar fi deja fals, iar operandul din dreapta nu ar mai fi evaluat.
3. Aici operandul din stânga este adevărat, deci la [00:07:00](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=420s) evaluăm operandul din dreapta: `5 == 7` este fals.
4. La [00:07:14](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=434s), `true && false` dă fals. Ambii operanzi ar trebui să fie adevărați pentru ca ȘI-ul să dea adevărat.

[00:07:25](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=445s) Și SAU-ul logic, scris `||`, evaluează tot întâi partea stângă. Schimbă operatorul:

```csharp
bool c = (a != b) || (a == 7);
```

La [00:07:31](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=451s), un operand stâng adevărat stabilește SAU-ul ca adevărat; operandul din dreapta este sărit. La [00:07:42](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=462s), un operand stâng fals impune verificarea din dreapta. Dacă și aceasta este falsă, întregul rezultat este fals.

[00:07:54](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=474s) Aceasta este evaluarea de scurt-circuit, sau leneșă: calculăm al doilea operand doar când primul nu poate decide rezultatul. La [00:08:05](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=485s), este subliniată consecința practică: un efect secundar din operandul sărit nu se întâmplă niciodată.

| Operator | Rezultat stânga | Operandul din dreapta este evaluat? |
| --- | --- | --- |
| `&&` | `false` | Nu; rezultatul este deja fals |
| `&&` | `true` | Da |
| `||` | `true` | Nu; rezultatul este deja adevărat |
| `||` | `false` | Da |

## Evaluarea sărită, făcută vizibilă

[00:08:21](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=501s) Înlocuiește comparația din dreapta cu o funcție care afișează ceva înainte de a o efectua. Astfel, evaluarea devine observabilă. Un apel de funcție execută corpul funcției; valoarea sa de retur devine apoi valoarea operandului.

```csharp
static bool IsSeven(int number)
{
    Console.WriteLine("Check called.");
    return number == 7;
}
```

[00:08:37](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=517s) Folosește acel apel drept al doilea operand și afișează `c` după. Este același tipar de demonstrație ca [exemplele înrudite de evaluare leneșă](https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/06_bool.md):

```csharp
int a = 5;
int b = 6;
bool c = (a != b) && IsSeven(a);
Console.WriteLine(c);
```

[00:08:47](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=527s) În execuția înregistrată cu ȘI, mesajul funcției apare pentru că comparația din stânga este adevărată. La [00:08:57](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=537s), motivul este că operatorul are în continuare nevoie de valoarea operandului din dreapta. La [00:09:05](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=545s), execuția intră în funcție, afișează mesajul și returnează fals, pentru că 5 nu este 7. La [00:09:22](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=562s), ieșirea confirmă rezultatul fals memorat în `c`:

```text
Check called.
False
```

[00:09:27](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=567s) Acum schimbăm ȘI-ul în SAU:

```csharp
bool c = (a != b) || IsSeven(a);
Console.WriteLine(c);
```

Comparația din stânga este deja adevărată, așadar execuția înregistrată cu SAU afișează doar `True`. La [00:09:47](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=587s), ideea-cheie este că sărirea peste expresia din dreapta sare peste apelul însuși. Funcția nu este intrată niciodată, deci afișarea ei nu poate apărea.

Ce ar afișa varianta cu ȘI dacă `b` ar fi schimbat în 5?

<details>
<summary>Răspuns</summary>

Doar <code>False</code>. Comparația din stânga <code>5 != 5</code> este falsă, deci ȘI-ul sare peste <code>IsSeven(a)</code> și peste afișarea acesteia.

</details>

## Compararea directă a valorilor bool

[00:09:55](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=595s) Egalitatea și inegalitatea pot compara direct valori bool, așa cum compară numerele din această lecție. Nu este necesară nicio conversie.

[00:10:02](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=602s) Păstrăm în `c` rezultatul adevărat din demonstrația cu SAU, punem fals în `d` și le comparăm. [Exemplul înrudit de comparare bool](https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/06_bool.md) folosește aceeași idee:

```csharp
bool c = true;
bool d = false;
bool equal = c == d;
Console.WriteLine(equal); // False
```

[00:10:19](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=619s) Adevărat și fals sunt valori diferite, deci egalitatea dă fals.

[00:10:38](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=638s) Egalitatea nu are o regulă de scurt-circuit. Are nevoie de ambele valori pentru a decide dacă se potrivesc. Într-o variantă reconstruită, apelul de funcție s-ar executa chiar dacă operandul din stânga este deja adevărat:

```csharp
bool equal = c == IsSeven(5);
```

Distincția este între operatori: `&&` și `||` pot decide uneori doar din valoarea din stânga; `==` și `!=` au nevoie de ambii operanzi.

## O singură instrucțiune poate fi un bloc întreg

[00:11:00](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=660s) Un `if` se asociază cu o singură instrucțiune următoare. Un bloc între acolade contează ca acea instrucțiune, permițând totuși mai multe instrucțiuni în interior. [Exemplele înrudite de bloc](https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/07_flow_control.md) arată această grupare:

```csharp
bool execute = false;
if (execute)
{
    Console.WriteLine("First");
    Console.WriteLine("Second");
}
Console.WriteLine("After");
```

[00:11:17](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=677s) Dacă expresia bool este adevărată, execuția intră în bloc și rulează instrucțiunile lui. La [00:11:39](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=699s), o valoare falsă sare peste blocul întreg, dar nu și peste codul de după el.

[00:11:54](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=714s) Afișarea de după `if` demonstrează acea limită: apare indiferent de condiție. Cu fals apare doar `After`; cu adevărat, ambele mesaje interioare o precedă. Condiția controlează doar blocul asociat.

## Acoladele determină ce instrucțiuni aparțin unui `if`

[00:12:13](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=733s) Acoladele pot fi omise când instrucțiunea asociată este o singură instrucțiune. Acea instrucțiune poate fi la rândul ei un `if`, cu instrucțiunea dependentă a lui. La [00:12:19](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=739s), formele imbricate cu și fără acolade sunt comparate:

```csharp
// Reconstructed single-statement form:
if (num >= r)
    if (num == r)
        Console.WriteLine("Equal");

// Equivalent explicitly braced form:
if (num >= r)
{
    if (num == r)
    {
        Console.WriteLine("Equal");
    }
}
```

[00:12:37](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=757s) Fără bloc între acolade, fiecărui `if` îi aparține o singură instrucțiune. La [00:12:54](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=774s), indentarea este subliniată ca formatare vizuală, nu ca regulă de apartenență. O a doua instrucțiune indentată identic rămâne în afara `if`-ului. Vezi [exemplul înrudit fără acolade](https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/07_flow_control.md):

```csharp
int r = 50;
if (num == r)
    Console.WriteLine("First");
    Console.WriteLine("Second"); // runs always
```

[00:13:11](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=791s) Pentru demonstrație, numărul ascuns este făcut fix, nu generat de fiecare dată, ca aceeași comparație să poată fi testată fiabil. Aceasta înseamnă atribuirea valorii 50; nu introduce cuvântul cheie `fixed` din C#.

[00:13:15](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=795s) Testele înregistrate arată ambele mesaje pentru 50, dar doar al doilea pentru un alt număr. La [00:13:31](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=811s), explicația este că a doua afișare nu este asociată `if`-ului.

[00:13:28](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=808s) Recomandarea de învățare este să păstrăm acoladele chiar și pentru o singură instrucțiune. Ele fac gruparea clară și ne permit să adăugăm în siguranță încă o instrucțiune:

```csharp
if (num == r)
{
    Console.WriteLine("First");
    Console.WriteLine("Second");
}
```

## Atașarea unui `else` la calea falsă

[00:13:43](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=823s) Un `else` furnizează instrucțiunea alternativă pentru un `if`. Ca și `if`, poate avea un bloc sau o singură instrucțiune fără acolade. La [00:13:57](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=837s), se aplică aceeași regulă a indentării: spațiile nu decid ce aparține lui `else`.

[00:14:05](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=845s) `else` trebuie să urmeze direct instrucțiunea `if` asociată. El se execută când acea condiție `if` este falsă. [Exemplele înrudite de if-else](https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/07_flow_control.md) ilustrează cele două căi:

```csharp
int r = 50;
if (num == r)
{
    Console.WriteLine("Equal");
}
else
{
    Console.WriteLine("Different");
}
Console.WriteLine("After");
```

[00:14:23](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=863s) Forma cu o singură instrucțiune permite și ea `else`; instrucțiunea precedentă completează `if`:

```csharp
if (num == r)
    Console.WriteLine("Equal");
else
    Console.WriteLine("Different");
```

[00:14:37](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=877s) În testul înregistrat, 50 ia calea `if`, iar un alt număr ia calea `else`. La [00:14:41](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=881s), execuția este urmărită prin cazul fals: sare peste blocul `if`, execută `else`-ul său, apoi continuă cu instrucțiunile următoare. Cele două alternative nu rulează amândouă.

## Construirea unui lanț `else if` din instrucțiuni imbricate

[00:15:00](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=900s) Îmbricăm altă verificare în interiorul ramurii de încercare greșită. Îmbricarea înseamnă punerea unei instrucțiuni în interiorul altui bloc. Această structură inițială poate afișa un mesaj general de încercare greșită înainte de a decide ce indice se aplică:

```csharp
if (num == r)
{
    Console.WriteLine("Correct!");
}
else
{
    Console.WriteLine("Wrong guess.");
    if (num < r)
    {
        Console.WriteLine("Guess higher.");
    }
}
```

[00:15:33](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=933s) Dacă `else`-ul exterior conține doar `if`-ul interior, acoladele sale pot fi omise, iar cele două cuvinte cheie împart convențional același rând. La [00:15:55](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=955s), asta explică `else if`: un `if` este instrucțiunea unică asociată lui `else`. [Exemplul înrudit de lanț](https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/07_flow_control.md) folosește această structură:

```csharp
if (num == r)
{
    Console.WriteLine("Correct!");
}
else if (num < r)
{
    Console.WriteLine("Guess higher.");
}
else
{
    Console.WriteLine("Guess lower.");
}
```

[00:15:58](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=958s) O extindem înapoi în acolade, păstrând ultima alternativă: acel `else` îi aparține `if`-ului interior, deci trebuie să rămână în interiorul blocului `else` exterior.

```csharp
if (num == r)
{
    Console.WriteLine("Correct!");
}
else
{
    if (num < r)
    {
        Console.WriteLine("Guess higher.");
    }
    else
    {
        Console.WriteLine("Guess lower.");
    }
}
```

[00:16:01](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=961s) O verificare finală explicită cu `num > r` este inutilă pentru aceste întregi. Dacă numărul nu este nici egal cu `r`, nici mai mic decât el, atunci trebuie să fie mai mare.

[00:16:23](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=983s) Un `if` împreună cu `else`-ul asociat contează ca o singură instrucțiune. Așadar, întreaga pereche interioară poate fi asociată `else`-ului exterior. La [00:16:51](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1011s), eliminarea acoladelor opționale comprimă același lanț:

```csharp
if (num == r) Console.WriteLine("Correct!");
else if (num < r) Console.WriteLine("Guess higher.");
else Console.WriteLine("Guess lower.");
```

Ultimul `else` se leagă de cel mai apropiat `if` interior disponibil; pentru lanț nu este necesar un bloc exterior suplimentar. Aceasta explică sintaxa, rămânând ca modalitate recomandată de a afișa conținutul fiecărei ramuri acoladele.

[00:17:05](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1025s) Execuția înregistrată pornește în continuare o singură dată și furnizează un indice „mai sus/mai jos”. Alegerea corectă a ramurii nu a creat încă repetiție.

## Repetarea citirii și a comparației

[00:17:07](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1027s) Pentru a întreaba mereu până când încercarea reușește, identificăm mai întâi porțiunea care se repetă: citirea unui număr și compararea lui cu numărul ascuns. La [00:17:09](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1029s), structura propusă este o buclă infinită cu o ieșire explicită adăugată în interior.

1. Păstrăm alegerea numărului ascuns în afara corpului care se repetă, astfel încât să rămână același pe tot parcursul jocului.
2. Punem citirea și comparația în interiorul corpului.
3. La [00:17:17](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1037s), identificăm o încercare corectă drept evenimentul care trebuie să părăsească bucla.

[00:17:22](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1042s) O buclă `while` își evaluează condiția bool înaintea fiecărei iterații. O iterație este o trecere prin corp. Literalul `true` permite întotdeauna o nouă trecere:

```csharp
int r = new Random().Next(100);
while (true)
{
    int num = ReadNumber();
    CompareGuess(num, r);
}
```

Aceasta este o stare structurală intermediară; funcția ajutătoare de citire este completată mai târziu. La [00:17:30](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1050s), oprirea este amânată deliberat, deci această versiune nu se oprește nici după o încercare corectă.

[00:17:33](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1053s) Și o buclă se asociază cu o singură instrucțiune. Un bloc grupează mai multe instrucțiuni; acoladele pot fi omise pentru o singură instrucțiune, folosind aceeași regulă ca la `if`.

[00:18:04](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1084s) Finalizarea corpului revine la condiția buclei, nu direct la instrucțiunea de după buclă. Cât timp condiția rămâne adevărată, corpul se repetă.

## Folosirea lui `break` și `continue` în bucla de ghicire

[00:18:20](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1100s) `break` iese din cea mai apropiată buclă care îl conține. La [00:18:40](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1120s), destinația este clarificată: prima instrucțiune de după acea buclă, nu o altă instrucțiune din corpul ei.

[00:18:55](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1135s) `continue` încheie doar iterația curentă. Într-un `while`, trece la verificarea condiției înaintea următoarei treceri. Pentru `while (true)`, aceasta permite imediat o nouă trecere. Este un transfer de control anticipat, distinct de `return`, care iese dintr-o funcție. Vezi [exemplele înrudite de break și continue](https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/07_flow_control.md) și [referința C# pentru instrucțiunile de salt](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements).

[00:19:06](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1146s) Punem cazurile nereușite la început și acțiunea reușită la final:

```csharp
while (true)
{
    int num = ReadNumber();

    if (num > r)
    {
        Console.WriteLine("Guess lower.");
        continue;
    }
    if (num < r)
    {
        Console.WriteLine("Guess higher.");
        continue;
    }

    Console.WriteLine("Correct!");
    break;
}
Console.WriteLine("Game finished.");
```

[00:19:35](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1175s) Verificările „mai mare decât” și „mai mic decât” aleg direcția indicelui. La [00:19:40](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1180s), potrivirea cu cazul „mai mic decât” exclude atât egalitatea, cât și „mai mare decât”; are nevoie doar de indicele său.

[00:19:52](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1192s) Fiecare `continue` împiedică rularea verificărilor rămase și a mesajului de succes pentru o încercare greșită. La [00:20:06](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1206s), egalitatea nu trebuie testată explicit: un întreg care nici nu este mai mic, nici mai mare decât `r` trebuie să fie egal cu el.

[00:20:15](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1215s) Urmărește cele trei căi:

1. O încercare prea mare afișează „Guess lower”, apoi `continue` sare peste restul corpului.
2. O încercare prea mică afișează „Guess higher”, apoi `continue` sare peste restul.
3. O încercare egală ajunge la succes și `break`; execuția continuă după buclă.

[00:20:38](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1238s) Condiția buclei este reevaluată la fiecare trecere, inclusiv la următoarea trecere cerută de `continue`. Aici se întâmplă să fie mereu adevărată.

Dacă s-ar adăuga o buclă interioară, din ce buclă ar ieși un `break` aflat în ea?

<details>
<summary>Răspuns</summary>

Din cea mai apropiată buclă care o conține: cea interioară. Execuția s-ar relua după acea buclă interioară, tot în interiorul buclei exterioare, dacă acolo se află instrucțiunea următoare.

</details>

## Mutarea testului de ieșire în condiția buclei

[00:20:41](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1241s) Explorăm alt design. Mutăm declarația numărului în afara buclei și eliminăm `break`. O declarație introduce variabila; declararea ei în afara buclei face ca numele să fie disponibil unei condiții aflate în afara corpului.

```csharp
int num;
while (true)
{
    num = ReadNumber();
    if (num > r)
    {
        Console.WriteLine("Guess lower.");
        continue;
    }
    if (num < r)
    {
        Console.WriteLine("Guess higher.");
        continue;
    }
    Console.WriteLine("Correct!");
    // break removed
}
```

[00:20:54](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1254s) Fără ieșire, o încercare corectă ajunge la sfârșitul corpului și cere din nou input. Doar succesul nu oprește o buclă infinită.

[00:21:08](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1268s) Alternativa este o condiție de buclă care înseamnă „numărul nu a fost ghicit”. Mutăm afișarea succesului după buclă:

```csharp
// Intermediate attempt: does not compile.
int num;
while (num != r)
{
    num = ReadNumber();
    if (num > r)
    {
        Console.WriteLine("Guess lower.");
    }
    else if (num < r)
    {
        Console.WriteLine("Guess higher.");
    }
}
Console.WriteLine("Correct!");
```

[00:21:34](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1294s) Comportamentul dorit este să întrebăm din nou cât timp încercarea e greșită și să ne oprim când este corectă. La [00:21:37](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1297s), verificarea este descrisă ca mutată din interiorul corpului în expresia evaluată înaintea fiecărei treceri. La [00:21:44](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1304s), fals este ceea ce oprește bucla.

[00:21:59](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1319s) Prin urmare, un test de ieșire din corp și un test de continuare a buclei folosesc valori de adevăr opuse: ieși când `num == r`; continuă să bucleleze când `num != r`.

[00:22:08](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1328s) Această versiune expune atribuirea definită: compilatorul respinge citirea variabilei locale neinițializate `num` în prima verificare a condiției. Declararea unei variabile locale nu îi atribuie o valoare utilizabilă.

[00:22:22](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1342s) Corpul trebuie să ruleze cel puțin o dată pentru a obține input înainte ca orice test de ieșire să citească acel input. Această cerință motivează mutarea verificării la final.

## Încercarea lui `do while` și compararea designurilor

[00:22:32](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1352s) O buclă `do ... while` își evaluează condiția după corp. Înlocuim `while`-ul de la deschidere cu `do`, punem `while (condition)` după acolada de închidere și adăugăm punctul și virgulă obligatoriu:

```csharp
int num;
do
{
    num = ReadNumber();
    if (num > r)
    {
        Console.WriteLine("Guess lower.");
    }
    else if (num < r)
    {
        Console.WriteLine("Guess higher.");
    }
}
while (num != r);
Console.WriteLine("Correct!");
```

Prima trecere îi atribuie lui `num` înainte ca condiția să o citească. Trecerile următoare au loc doar cât timp condiția este adevărată. În această formă de buclă, un `continue`, dacă ar fi folosit, ar merge la condiția de la final înaintea unei alte treceri; nu ar ocoli acea verificare.

[00:22:46](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1366s) Păstrarea unui `while` verificat deasupra ar necesita în schimb inițializarea cu o santinelă: o valoare de început artificială, aleasă astfel încât să nu reprezinte input real. Lecția consideră aceasta mai slab decât exprimarea cerinței reale că inputul trebuie să se întâmple primul.

[00:23:02](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1382s) Comparam domeniul de vizibilitate, regiunea în care numele unei variabile poate fi folosit. Această formă `do ... while` trebuie să declare variabila verificată în afara corpului, ca și condiția să o poată accesa. La [00:23:14](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1394s), aceasta este contrastată cu `while (true)` plus `break`, unde încercarea poate fi declarată în interiorul corpului, lângă folosirile sale. Aceasta este o proprietate a acestei rescrieri, nu o regulă care să impună ca orice variabilă din orice buclă do-while să fie în afara ei.

[00:23:22](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1402s) Ieșirea este și mai greu vizibilă în acest design. La [00:23:31](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1411s), logica de comparație are două roluri: ramurile interioare decid dacă se afișează indici, în timp ce condiția buclei decide separat dacă mai este nevoie de o încercare. Când nu se aplică nicio ramură de indice, nu se afișează nimic în corp; succesul este tratat după buclă.

[00:23:43](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1423s) Cu `break`, ajungerea la sfârșitul verificărilor nereușite însemna firesc succes și ieșire. Forma bazată pe condiție restabilește acea logică ca inversul ei în condiția de continuare. Chiar dacă se folosește un flag bool salvat, acel flag tot trebuie actualizat în corp și citit în condiție.

[00:24:05](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1445s) Pentru acest exemplu, instructorul recomandă `while (true)` cu un `break` explicit când se atinge succesul. Păstrează încercarea locală și pune motivul ieșirii lângă ieșire. Este o recomandare de stil ilustrată de joc; lecția explică în continuare alternativele valide, bazate pe condiție și cu do-while.

## O parsare nereușită nu este o încercare de zero

[00:24:17](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1457s) Apare o problemă rămasă de intrare: când funcția ajutătoare de citire returnează zero după o conversie nereușită, apelantul tratează acel zero drept o încercare reală. Această funcție ajutătoare intermediară ilustrează defectul:

```csharp
static int ReadNumber()
{
    string? input = Console.ReadLine();
    if (!int.TryParse(input, out int num))
    {
        Console.WriteLine("Not a number.");
        return 0;
    }
    return num;
}
```

[00:24:28](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1468s) Păstrăm distincția dintre eșecul parsării și valoarea parametrului de ieșire. Pentru suprasarcina `int.TryParse(string, out int)` folosită aici, eșecul returnează `false` și scrie zero în variabila `out`; **nu** păstrează valoarea anterioară a acelei variabile. [Contractul oficial al parametrilor TryParse](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-string-system-int32@)) confirmă această corectare.

Atât zero-ul parsării nereușite, cât și un `return 0` explicit de pe calea de eroare nu trebuie confundate cu input reușit al utilizatorului. Zero se află în intervalul numărului ascuns, deci jocul nu se poate folosi de el singur pentru a semnala eșecul. Valoarea bool de succes este cea care distinge conversia reușită de eșec.

Ce se întâmplă dacă numărul ascuns este zero, iar această funcție ajutătoare primește `hello`?

<details>
<summary>Răspuns</summary>

Funcția ajutătoare raportează o eroare de parsare, dar returnează zero. Un apelant care compară doar întregul returnat poate declara în mod greșit încercarea corectă. Reluarea citirii în interiorul funcției împiedică textul invalid să devină o încercare.

</details>

## Reluarea citirii în propria sa funcție

[00:24:44](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1484s) Identificăm unitatea care se repetă în operația de citire: citirea unui șir nou, încercarea conversiei și tratarea eșecului sau returnarea unui număr. Înfășurăm întreaga operație într-o buclă. Dacă am repeta doar parsarea, am continua să reîncercăm același text greșit.

[00:25:02](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1502s) La eșecul parsării, afișăm eroarea și folosim `continue` în loc să returnăm zero:

```csharp
static int ReadNumber()
{
    while (true)
    {
        Console.Write("Enter a number: ");
        string? input = Console.ReadLine();
        if (!int.TryParse(input, out int num))
        {
            Console.WriteLine("Not a number. Try again.");
            continue;
        }
        return num;
    }
}
```

1. Citim text nou de la consolă.
2. Îl parsăm. Valoarea bool de retur indică succesul; variabila `out` păstrează rezultatul numeric.
3. La eșec, raportăm problema și reluăm iterația.
4. La succes, returnăm întregul către jocul de ghicire.

[00:25:11](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1511s) Aici `continue` revine la verificarea `while (true)` și apoi la prima instrucțiune a corpului. Sari peste `return num` ulterior; nu înseamnă „execută rândul următor”.

[00:25:24](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1524s) O parsare reușită nu are nevoie de `break`. `return` iese din întreaga funcție și oprește astfel și executarea buclei sale. [Exemplul înrudit de return în while](https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/07_flow_control.md) demonstrează același transfer de control:

```csharp
// Successful path inside ReadNumber's loop:
return num; // leaves the function, not just the loop
```

[00:25:35](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1535s) Structura completată cere input până când se introduce un întreg valid. Împreună cu bucla de ghicire, aceasta separă două repetări: funcția ajutătoare reîncearcă textul invalid, iar jocul reîncearcă încercările valide, dar incorecte.

Lecția validează conversia într-un întreg. Reguli suplimentare ale jocului, precum respingerea încercărilor întregi din afara intervalului 0–99 și tratarea sfârșitului fluxului de intrare de la consolă, depășesc implementarea demonstrată.

## Istoricul modificărilor de cod

Traseul urmărește stările video înregistrate în ordinea construirii. Link-urile imutabile către laboratoare de mai sus identifică exemple înrudite, verificate în repository; commit-urile lor nu înregistrează editările intermediare ale jocului.

- [00:00:26](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=26s): generarea unui număr ascuns cu un plafon dat de rest.
- [00:00:45](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=45s): exprimarea limitei superioare prin `Next(100)`.
- [00:01:33](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=93s): extragerea comparației pentru o singură încercare într-o funcție.
- [00:03:35](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=215s): memorarea rezultatului unei comparații și folosirea bool-ului în `if`.
- [00:08:21](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=501s): adăugarea unei funcții care afișează și returnează bool, pentru a expune evaluarea de scurt-circuit.
- [00:13:11](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=791s): fixarea valorii ascunse pentru testele de apartenență la bloc.
- [00:15:33](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=933s): exprimarea alternativelor imbricate ca un lanț `else if`.
- [00:17:22](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1042s): împachetarea citirii și comparației într-un `while (true)`.
- [00:18:20](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1100s): introducerea ieșirii din buclă cu `break`, apoi reluarea încercărilor greșite cu `continue`.
- [00:20:41](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1241s): mutarea încercării în afara buclei și eliminarea lui `break` pentru a explora ieșirea bazată pe condiție.
- [00:22:32](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1352s): mutarea condiției la final cu `do ... while`.
- [00:25:02](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1502s): reluarea parsării nereușite cu `continue` și returnarea doar a unui întreg parsat cu succes.