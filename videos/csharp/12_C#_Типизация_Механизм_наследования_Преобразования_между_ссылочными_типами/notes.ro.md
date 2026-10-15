> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Scop și vocabular

De ce poate o variabilă `Base` să se refere la un obiect `Derived` și de ce poate eșua la execuție o conversie care pare validă? Construiește răspunsul în ordinea acestei lecții: inspectează tipul unui obiect, construiește moștenirea, copiază referințe prin tipuri mai generale, verifică o conversie și, în final, examinează o încercare invalidă de a avea două clase de bază. [00:00:02](https://www.youtube.com/watch?v=t2T88IiiP48&t=2s)

Păstrează acești termeni la îndemână cât timp citești; fiecare este explicat din nou când devine util:

- **Structură și tip valoare** (`struct`): o valoare își conține datele, iar copierea ei copiază acele date. **Clasă și tip referință** (`class`): o variabilă se referă la un obiect alocat, iar copierea referinței păstrează același obiect. Un **câmp** este o dată care aparține unei valori sau unei instanțe.
- **Obiect și instanță**: o instanță este un anumit obiect creat dintr-un tip. O **referință** identifică un obiect; înregistrarea o desenează ca **adresă** numerică în **memoria dinamică**, memoria folosită pentru obiectele alocate.
- **Tip static sau tip declarat**: tipul pe care compilatorul îl folosește pentru o variabilă sau o expresie. **Tip la execuție**: tipul efectiv al instanței referite. „Tipizare dinamică” în această lecție se referă la această informație de execuție, nu la funcția separată `dynamic` din C#.
- **Informații de tip sau metadate**: o descriere a unui tip, inclusiv numele, câmpurile și relațiile sale. Un **obiect de tip**, disponibil prin `System.Type`, expune acea descriere. **Reflexia** este inspecția metadelor prin API-uri gestionate. **Legătura ascunsă de tip** sau **antetul de tip** din înregistrare modelează asocierea dintre o instanță și tipul ei.
- **`GetType()`, `Name`, `BaseType` și `typeof`**: obțin, respectiv, tipul la execuție al unei instanțe, numele unui tip descris, tipul de bază imediat al acestuia și descrierea unui tip dintr-un tip denumit. Un **șir** (`string`) este un tip referință care reprezintă text; un câmp de tip șir păstrează o referință către obiectul de text.
- **Împachetare (boxing)**: reprezentarea unei valori de tip valoare în interiorul unui obiect. Este menționată aici doar pentru a delimita lecția și este amânată.
- **Moștenire, clasă de bază și clasă derivată**: moștenirea extinde o clasă de bază cu o clasă derivată. **Clasele surori** împart o bază, dar sunt tipuri concrete distincte. **Moștenirea multiplă de clase** ar da unei clase mai multe baze de clasă directe. O **interfață** este un contract implementat; mecanismele de interfață sunt în afara lecției.
- **Compatibilitate și polimorfism**: compatibilitatea permite folosirea prin tipul cerut; polimorfismul permite o vedere tipizată comună, cu un comportament care depinde de obiectul efectiv. Comportamentul polimorf al metodelor este menționat, nu dezvoltat aici. Un **deplasament de câmp** este deplasarea folosită pentru a localiza un câmp față de începutul modelat al datelor obiectului.
- **`object` (`System.Object`)**: clasa rădăcină încorporată, utilizabilă ca tip referință comun. O **conversie** produce o valoare utilizabilă ca alt tip. O **conversie implicită** nu necesită sintaxă de conversie; o **conversie explicită** scrie `(TargetType)expression`. O **conversie ascendentă (upcast)** folosește o referință derivată printr-un tip de bază.
- **Bloc, variabilă locală și domeniu**: acoladele delimitează un bloc, iar un nume local este disponibil doar în domeniul său. O **valoare implicită** este valoarea inițială furnizată unui câmp fără alt inițializator. **atribuirea definită** cere ca o variabilă locală să fie atribuită înainte de a fi citită. **Suprascrierea** înlocuiește valoarea păstrată într-o variabilă.
- **Nul, referință nullable și analiză de nul**: null reprezintă absența unei referințe de obiect; o referință nullable precum `Type?` permite null, iar analiza de nul verifică folosirea posibilă a lui null. **Operatorul de anulare a avertismentului de nul** (`!`), postfix, suprimă un avertisment de nul; nu efectuează nicio verificare la execuție. **Egalitatea de tipuri**, `==` între valori `Type`, compară tipurile reprezentate.
- **Eroare la compilare, eroare la execuție, verificare la execuție și `InvalidCastException`**: o eroare la compilare împiedică construirea; o eroare la execuție apare în timpul rulării, iar o verificare la execuție examinează obiectul efectiv. O conversie incompatibilă de referințe aruncă `InvalidCastException`; o excepție ne tratată oprește programul demonstrat.
- **Sintaxă de exemplu**: `new` creează o instanță; `var` cere compilatorului să deducă tipul static al unei variabile locale; `int` memorează un întreg; `public` permite accesul la câmpul demonstrat. `=` atribuie, `.` selectează un membru, iar `Console.WriteLine` afișează o valoare. Un **inițializator** furnizează valoarea de pornire a unui câmp.

Limita este moștenirea între clase și conversiile între referințe. Câmpurile unei structuri aparțin valorii în sine; o variabilă de clasă se referă, în schimb, la un obiect care conține câmpurile sale de instanță. Aceasta explică efectele diferite ale copierii și ale modificării ulterioare; o structură nu trebuie să fie imutabilă. Structurile nu pot declara moștenirea de clase prezentată aici, deși pot implementa interfețe. [00:00:02](https://www.youtube.com/watch?v=t2T88IiiP48&t=2s) [00:00:20](https://www.youtube.com/watch?v=t2T88IiiP48&t=20s) [00:00:23](https://www.youtube.com/watch?v=t2T88IiiP48&t=23s)

O instanță de clasă duce o asociere cu informațiile de tip, alături de datele sale. Lecția reprezintă acele informații ca pe un alt obiect în memoria dinamică. Structurile neîmpachetate nu au antetul per-instanță folosit în acest desen, dar tipurile lor sunt cunoscute în continuare; valorile împachetate au informații de tip asociate obiectului. Tiparea detaliată a structurilor și împachetarea sunt amânate. [00:00:30](https://www.youtube.com/watch?v=t2T88IiiP48&t=30s) [00:00:41](https://www.youtube.com/watch?v=t2T88IiiP48&t=41s) [00:00:47](https://www.youtube.com/watch?v=t2T88IiiP48&t=47s) [00:00:57](https://www.youtube.com/watch?v=t2T88IiiP48&t=57s)

**Citirea exemplelor.** Adresele, o referință care indică începutul zonei de câmpuri, o legătură ascunsă direct către un „obiect de tip” și ordinea cu câmpurile de bază primele sunt modelul explicativ al înregistrării. Ele nu constituie o specificație portabilă a dispunerii în .NET. Referințele obișnuite din C# nu sunt pointeri numerici expuși, iar codul aplicației nu poate rescrie aceste metadate ascunse. Alte date din antet și implementarea dispatcherului polimorf sunt amânate.

Nu a fost găsit în checkoutul verificat niciun program exact, corespunzător acestor stări succesive din clasă. Fiecare exemplu de mai jos este o **reconstrucție simplificată a stării sale din videoclipul asociat**, nu o revizie exactă din repo. Fragmentele presupun că `System` este în domeniu, iar fiecare exemplu complet înlocuiește declarațiile anterioare cu aceleași nume de clase. Commitul de achiziție a transcrierii consemnează dovezi vorbite, nu o stare de instalare sau de cod executabil.

## Obține tipul la execuție

Pornim de la o clasă goală și întrebăm o instanță alocată ce tip are. Un **obiect de tip**, reprezentat de `System.Type`, descrie clasa; este diferit de instanța clasei. **`GetType()`** întoarce tipul efectiv la execuție al obiectului nenul pe care este apelat. Starea inițială din videoclip: [00:01:03](https://www.youtube.com/watch?v=t2T88IiiP48&t=63s) [00:01:12](https://www.youtube.com/watch?v=t2T88IiiP48&t=72s)

```csharp
using System;

var c = new Class();
Type t = c.GetType();
Console.WriteLine(t);

class Class { }
```

Urmează cele trei operații:

1. `new Class()` creează o **instanță**. `var c` declară o **variabilă locală** al cărei tip static dedus este `Class`; `=` păstrează referința instanței.
2. `c.GetType()` selectează și apelează metoda obiectului. Rezultatul este o referință către informațiile de tip, păstrată în `Type t`.
3. `Console.WriteLine(t)` afișează reprezentarea textuală a acelui tip. Rularea afișează `Class`. Aceasta confirmă că descrierea tipului la execuție poate furniza numele său. [00:01:55](https://www.youtube.com/watch?v=t2T88IiiP48&t=115s)

Informațiile de tip permit verificarea dacă un obiect este instanța unui tip așteptat. **Compatibilitatea** înseamnă că poate fi folosit prin acel tip, inclusiv printr-un tip de bază și nu doar prin tipul său exact; lecția revine la tipurile de bază după această inspecție. [00:01:21](https://www.youtube.com/watch?v=t2T88IiiP48&t=81s) [00:01:27](https://www.youtube.com/watch?v=t2T88IiiP48&t=87s)

Aceleași informații permit și **polimorfismul**, adică folosirea tipului efectiv al unui obiect pentru a determina comportamentul, deși aici nu este construită nicio metodă polimorfă. Ele pot expune și nume și descrieri de câmpuri. **`Name`** este proprietatea pentru numele unui tip descris; programul inițial afișează chiar `t`. [00:01:35](https://www.youtube.com/watch?v=t2T88IiiP48&t=95s) [00:01:44](https://www.youtube.com/watch?v=t2T88IiiP48&t=104s) Documentația [GetType API](https://learn.microsoft.com/en-us/dotnet/api/system.object.gettype) confirmă că raportează tipul la execuție al instanței chiar și printr-o variabilă de tip de bază.

## Desenează instanța și legătura sa de tip

Un **câmp de instanță** aparține unei anumite instanțe. O **legătură ascunsă de tip** este informația suplimentară de execuție asociată acelei instanțe în desen. Chiar și o clasă fără câmpuri declarate are tot un obiect și o asociere de tip. [00:02:02](https://www.youtube.com/watch?v=t2T88IiiP48&t=122s) [00:02:17](https://www.youtube.com/watch?v=t2T88IiiP48&t=137s)

Păstrează starea din videoclip cu clasa goală în timp ce urmărești alocarea ei: [00:02:23](https://www.youtube.com/watch?v=t2T88IiiP48&t=143s)

```csharp
var c = new Class();
class Class { } // fără câmpuri de instanță declarate
```

1. Locala `c` primește rezultatul lui `new`, desenat ca adresa obiectului. Fără câmpuri ale utilizatorului, desenul plasează acea adresă la sfârșitul obiectului, pentru că convenția sa indică începutul zonei de câmpuri. Adăugarea unor câmpuri ar face ca aceeași referință să indice începutul lor. [00:02:23](https://www.youtube.com/watch?v=t2T88IiiP48&t=143s) [00:02:31](https://www.youtube.com/watch?v=t2T88IiiP48&t=151s) [00:02:32](https://www.youtube.com/watch?v=t2T88IiiP48&t=152s) [00:02:38](https://www.youtube.com/watch?v=t2T88IiiP48&t=158s)
2. Imediat înaintea zonei de câmpuri, adaugă celula ascunsă pentru informațiile de tip. Ea este furnizată de compilator/runtime, nu declarată de programator. [00:02:45](https://www.youtube.com/watch?v=t2T88IiiP48&t=165s) [00:02:52](https://www.youtube.com/watch?v=t2T88IiiP48&t=172s)
3. Desenează valoarea celulei ca pe o referință către un alt obiect care descrie tipul inițial. Acea descriere are propria adresă, plasată în acest model tot la începutul zonei sale de câmpuri. [00:03:01](https://www.youtube.com/watch?v=t2T88IiiP48&t=181s) [00:03:07](https://www.youtube.com/watch?v=t2T88IiiP48&t=187s)
4. Unul dintre câmpurile descrise este numele tipului, reprezentat printr-o referință către un obiect de text. Un **șir** este un tip referință: câmpul de nume păstrează o referință, nu caracterele textului în sine. [00:03:16](https://www.youtube.com/watch?v=t2T88IiiP48&t=196s) [00:03:26](https://www.youtube.com/watch?v=t2T88IiiP48&t=206s)
5. Șirul de nume este el însuși un obiect, cu propria asociere de tip care conduce la descrierea lui `String`. Așadar, lanțul include atât „instanță → descrierea tipului → șirul de nume”, cât și „șirul de nume → descrierea tipului șir”. [00:03:39](https://www.youtube.com/watch?v=t2T88IiiP48&t=219s) [00:03:45](https://www.youtube.com/watch?v=t2T88IiiP48&t=225s)

Descrierea tipului conține și alte informații: absența câmpurilor utilizatorului în această clasă și relații cu subclasele, de exemplu. Este un desen conceptual de metadate, nu un registru public, scrisibil, de subclase. [00:03:54](https://www.youtube.com/watch?v=t2T88IiiP48&t=234s) Compoziția sa este controlată de compilator și runtime, astfel că declararea unor câmpuri de aplicație nu poate redefini formatul metadelatelor ascunse. Legătura ascunsă a instanței inițiale descrie în continuare clasa alocată în prima instrucțiune. [00:04:03](https://www.youtube.com/watch?v=t2T88IiiP48&t=243s) [00:04:14](https://www.youtube.com/watch?v=t2T88IiiP48&t=254s)

## Dă descrierilor de tip un tip propriu

Un **obiect de tip** este și el un obiect, deci are la rândul lui un tip la execuție. Extinde desenul pentru a răspunde la întrebarea: ce descrie descrierea lui `Class`? [00:04:26](https://www.youtube.com/watch?v=t2T88IiiP48&t=266s)

Descrierea suplimentară ocupă o altă adresă ilustrativă. Numele ei se află din nou într-un **obiect șir** separat, la care se ajunge printr-o referință din câmpul de nume. Adresa `5000` din această parte reprezintă descrierea lui `Type`; descrierea clasei utilizatorului este desenată ca un obiect de tip `Type`. Și descrierea de la `5000` are tipul `Type`. [00:04:41](https://www.youtube.com/watch?v=t2T88IiiP48&t=281s) [00:04:43](https://www.youtube.com/watch?v=t2T88IiiP48&t=283s) [00:05:07](https://www.youtube.com/watch?v=t2T88IiiP48&t=307s)

Aceasta închide lanțul conceptual fără a necesita o infinitate de tipuri diferite. **Reflexia** este API-ul gestionat prin care sunt inspectate metadatele de tip. `System.Type` este abstracția sa publică; obiectele întoarse efectiv pot avea un tip de implementare la execuție derivat din ea. Nu citi diagrama ca pe o garanție că fiecare obiect de reflexie are tipul exact de execuție `System.Type`.

Runtime-ul furnizează automat descrierile și datele de suport inaccesibile. Apelarea lui `GetType()` nu dă tip unui obiect care altfel nu ar fi tipat. Informațiile de tip rămân disponibile chiar dacă programul nu le cere niciodată. Afirmația înregistrării că sistemul de tipuri există deja în memoria dinamică exprimă tocmai această disponibilitate, nu o garanție că fiecare tip posibil este materializat din timp la pornirea programului. [00:05:21](https://www.youtube.com/watch?v=t2T88IiiP48&t=321s) [00:05:32](https://www.youtube.com/watch?v=t2T88IiiP48&t=332s)

## Separă descrierea unui tip de datele instanței

Revino la primul apel și urmărește cele două referințe diferite. Starea relevantă din videoclip este: [00:05:46](https://www.youtube.com/watch?v=t2T88IiiP48&t=346s)

```csharp
var c = new Class();
Type t = c.GetType();
Console.WriteLine(t); // Class

class Class { }
```

1. `c` identifică instanța clasei utilizatorului.
2. În desen, legătura sa ascunsă de tip conține `6000`.
3. `GetType()` urmează acea legătură și întoarce referința către descrierea tipului; `t` primește `6000`. Nu primește o copie a instanței. [00:05:46](https://www.youtube.com/watch?v=t2T88IiiP48&t=346s)
4. Afișarea lui `t` urmează descrierea tipului până la șirul său de nume, apoi afișează numele clasei. Aceasta abreviază operația gestionată: afișarea unui `Type` folosește reprezentarea sa textuală. [00:06:04](https://www.youtube.com/watch?v=t2T88IiiP48&t=364s)

Adaugă acum un câmp cu un **inițializator**, adică o expresie care furnizează valoarea sa de pornire. `int` este un tip întreg, iar `public` face ca acest câmp să fie accesibil în exemplul înconjurător. Modificare video corelată: [00:06:32](https://www.youtube.com/watch?v=t2T88IiiP48&t=392s)

```csharp
var c = new Class();
Type t = c.GetType();
Console.WriteLine(c.Field); // 10
// Console.WriteLine(t.Field); // nu ar compila

class Class
{
    public int Field = 10;
}
```

Noul câmp apare după începutul modelat al instanței și conține `10`. El aparține acelei singure instanțe de `Class`; nu devine câmp al obiectului de tip `Type`. `t` are tipul static `Type`, deci nu expune acest câmp de instanță. Descrierea câmpurilor unei clase și păstrarea valorilor câmpurilor unei instanțe anumite sunt operații separate. [00:06:48](https://www.youtube.com/watch?v=t2T88IiiP48&t=408s) [00:07:15](https://www.youtube.com/watch?v=t2T88IiiP48&t=435s)

<details>
<summary>Dacă t descrie Class, de ce poate reuși c.Field în timp ce t.Field eșuează?</summary>

c se referă la o instanță de Class al cărei Field conține 10. t se referă la o descriere de tip, iar Type nu declară acel câmp de instanță. GetType furnizează metadate, nu copiază datele lui c.

</details>

## Construiește instanțe Base și Derived

Pasul următor este **moștenirea**, adică extinderea unei clase existente. O **clasă de bază** furnizează membrii moșteniți; o **clasă derivată** o extinde cu membrii săi proprii. „De bază” descrie o relație, nu o interdicție de a crea instanțe ale bazei: acest exemplu creează una direct. [00:07:12](https://www.youtube.com/watch?v=t2T88IiiP48&t=432s) [00:07:21](https://www.youtube.com/watch?v=t2T88IiiP48&t=441s)

În `class Derived : Base`, clasa declarată este `Derived`, înaintea douipunctului; baza denumită este `Base`, după el. Câmpul public moștenit este disponibil pe o instanță derivată, alături de câmpul său nou. Starea video inițială a moștenirii, cu scrierea câmpurilor standardizată: [00:07:35](https://www.youtube.com/watch?v=t2T88IiiP48&t=455s) [00:07:56](https://www.youtube.com/watch?v=t2T88IiiP48&t=476s)

```csharp
var d = new Derived();
d.derField = 10;

var b = new Base();
b.baseField = 11;

Console.WriteLine(d.derField); // 10
Console.WriteLine(b.baseField); // 11

class Base
{
    public int baseField;
}
class Derived : Base
{
    public int derField;
}
```

Construiește obiectele în ordine:

1. Declară `d` și alocă o instanță `Derived` cu `new`. Relația dată de douipunct înseamnă că această instanță conține și `baseField` moștenit, pe lângă `derField`. [00:08:25](https://www.youtube.com/watch?v=t2T88IiiP48&t=505s) [00:08:37](https://www.youtube.com/watch?v=t2T88IiiP48&t=517s)
2. Desenează legătura sa ascunsă de tip, apoi datele: `baseField` urmat de `derField`. Această ordine fixă, bază întâi, explică vederile de referință ulterioare din modelul lecției; nu este o promisiune generală de dispunere în octeți pentru clase C# arbitrare. [00:08:46](https://www.youtube.com/watch?v=t2T88IiiP48&t=526s)
3. Setează `d.derField` la `10`. Câmpul întreg moștenit nu a fost scris și își păstrează **valoarea implicită**, zero. [00:09:13](https://www.youtube.com/watch?v=t2T88IiiP48&t=553s)
4. Alocă un obiect `Base` separat. Desenul său are legătura ascunsă și un singur câmp declarat, `baseField`. Atribuie `11` acelui câmp. [00:09:30](https://www.youtube.com/watch?v=t2T88IiiP48&t=570s)

Ambele instanțe au informații de tip, deci imaginea conține și descrierile lui `Base` și `Derived`. Și aceste descrieri au tipul `Type` în modelul conceptual. Legăturile lor către descrierea lui `Type` sunt omise intenționat aici, pentru a menține atenția asupra moștenirii. [00:08:16](https://www.youtube.com/watch?v=t2T88IiiP48&t=496s) [00:08:23](https://www.youtube.com/watch?v=t2T88IiiP48&t=503s)

Câmpul moștenit din `d` face parte din acea instanță derivată. Este independent de câmpul din obiectul `b` alocat separat; niciun obiect de bază existent nu a fost copiat în `d`. [Documentația despre moștenire](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance) descrie această relație bază/derivată.

## Observă scrierile în câmpuri și deplasamentele

Un **acces la câmp** selectează date de pe o anumită instanță. Pornim de la câmpul instanței de bază alocate separat, care conține `11`, îl citim, apoi atribuim o valoare nouă. Modificare video și observație: [00:09:42](https://www.youtube.com/watch?v=t2T88IiiP48&t=582s) [00:09:44](https://www.youtube.com/watch?v=t2T88IiiP48&t=584s)

```csharp
var b = new Base();
b.baseField = 11;
Console.WriteLine(b.baseField); // 11

b.baseField = 12;
Console.WriteLine(b.baseField); // 12

class Base { public int baseField; }
```

Prima citire produce `11`. După atribuire, atât citirea câmpului, cât și inspecția demonstrată a celulei de memorie produc `12`. Scrierea a modificat datele acelei instanțe. Acestea sunt rezultate din înregistrare, nu o nouă execuție a acestui fragment reconstruit. [00:09:57](https://www.youtube.com/watch?v=t2T88IiiP48&t=597s)

Un **deplasament de câmp** este o deplasare față de începutul modelat al zonei de câmpuri a unui obiect. Compilatorul identifică câmpul din definiția tipului și poate aranja accesul la locația sa. Nu parcurge memoria căutând șirul „baseField” la fiecare citire a unui membru. [00:10:10](https://www.youtube.com/watch?v=t2T88IiiP48&t=610s) [00:10:37](https://www.youtube.com/watch?v=t2T88IiiP48&t=637s)

Desenul stabilește adresa de început a zonei de câmpuri pentru `b` la `0x6000`. `baseField` este primul câmp al utilizatorului, deci deplasamentul său este zero în acea convenție. Accesul pornește de la adresa păstrată și nu sare peste nicio celulă de câmp anterioară. Un câmp ulterior ar necesita o deplasare peste datele precedente. [00:10:54](https://www.youtube.com/watch?v=t2T88IiiP48&t=654s) [00:11:09](https://www.youtube.com/watch?v=t2T88IiiP48&t=669s)

Aceasta este explicația simplificată a lecției pentru compilarea unui acces fix la câmp. Ea arată de ce contează definiția tipului, fără să predea calculul unor adrese reale de obiecte .NET pornind de la ordinea declarațiilor.

## Copiază o referință în două variabile tipizate

**Conversia ascendentă (upcast)** folosește o referință derivată printr-un tip de bază. Deoarece moștenirea face o instanță derivată utilizabilă ca bază a sa, o variabilă de tip bază poate păstra aceeași referință. [00:11:27](https://www.youtube.com/watch?v=t2T88IiiP48&t=687s) [00:11:41](https://www.youtube.com/watch?v=t2T88IiiP48&t=701s)

La 11:49, atribuirea suplimentară redundantă este ștearsă, iar exemplul păstrează operația esențială `Base b = d`. Descrierea verbală anterioară a atribuirilor legale nu trebuie citită ca permițând atribuirea implicită a unei referințe de bază arbitrare către o variabilă derivată. Conversia derivat-la-bază este implicită; inversul necesită o conversie explicită și un obiect efectiv compatibil. Starea video rezultată: [00:11:49](https://www.youtube.com/watch?v=t2T88IiiP48&t=709s)

```csharp
var d = new Derived();
d.derField = 10;
Base b = d;

d.baseField = 13;
b.baseField = 12;
Console.WriteLine(d.baseField); // 12
// b.derField = 10; // nu ar compila

class Base { public int baseField; }
class Derived : Base { public int derField; }
```

Urmărește valorile intermediare și vederile:

1. `d` este o locală de tip derivat care identifică o instanță derivată. Setează câmpul său propriu la `10`. [00:12:11](https://www.youtube.com/watch?v=t2T88IiiP48&t=731s) [00:12:17](https://www.youtube.com/watch?v=t2T88IiiP48&t=737s)
2. `Base b = d` declară o altă locală și copiază referința deja aflată în `d`. Ambele locale identifică același obiect; acest pas nu face nicio alocare. Relația `Derived : Base` permite atribuirea. [00:11:53](https://www.youtube.com/watch?v=t2T88IiiP48&t=713s) [00:12:22](https://www.youtube.com/watch?v=t2T88IiiP48&t=742s) [00:12:31](https://www.youtube.com/watch?v=t2T88IiiP48&t=751s)
3. **Tipul static** al lui `d` este `Derived`, deci expune atât câmpul moștenit, cât și câmpul derivat. [00:12:49](https://www.youtube.com/watch?v=t2T88IiiP48&t=769s)
4. Tipul static al lui `b` este `Base`, deci numai membrii de bază accesibili sunt disponibili prin el. Instanța efectivă rămâne `Derived`, inclusiv partea sa derivată. `b.derField` nu este disponibil, dar câmpul nu a fost eliminat din obiect. [00:13:02](https://www.youtube.com/watch?v=t2T88IiiP48&t=782s) [00:13:22](https://www.youtube.com/watch?v=t2T88IiiP48&t=802s) [00:13:32](https://www.youtube.com/watch?v=t2T88IiiP48&t=812s)
5. `d.baseField = 13` scrie câmpul moștenit prin vederea derivată completă. `b.baseField = 12` scrie același câmp prin vederea de bază. A doua atribuire câștigă; oricare dintre referințe citește `12`. [00:13:45](https://www.youtube.com/watch?v=t2T88IiiP48&t=825s) [00:14:06](https://www.youtube.com/watch?v=t2T88IiiP48&t=846s)

Desenul explică ambele accesări cu aceeași referință și cu deplasamentul primului câmp. În modelul său, câmpurile moștenite ocupă porțiunea inițială, astfel că tratarea adresei ca pe o referință de bază ajunge la acea porțiune. Restul este partea derivată. Aceasta este motivul pentru care modelul pune câmpurile de bază primele. Comportamentul limbajului de reținut este: o instanță, mai multe vederi tipizate și date moștenite partajate. [00:14:29](https://www.youtube.com/watch?v=t2T88IiiP48&t=869s) [00:14:39](https://www.youtube.com/watch?v=t2T88IiiP48&t=879s)

`Base b = d` este o **conversie implicită**, fără sintaxă suplimentară. O conversie ascendentă explicită poate numi ținta păstrând totuși aceeași instanță; [regulile conversiilor de referință](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions) disting aceste direcții și arată că o conversie de referință nu schimbă tipul la execuție al obiectului.

<details>
<summary>După cele două atribuiri, ce conține d.baseField? De ce este b.derField indisponibil?</summary>

baseField partajat conține 12. Ambele locale se referă la aceeași instanță de Derived. b are tipul declarat Base, care nu expune derField, deși instanța îl conține în continuare.

</details>

## Cere o relație de moștenire

O **conversie explicită** pune tipul țintă între paranteze înaintea unei expresii. Aici compilatorul cunoaște deja relația de moștenire, astfel că scrierea explicită a conversiei ascendente ajunge în continuare la câmpul moștenit și afișează `12`. Parantezele exterioare grupează referința convertită înainte de selectarea câmpului său. Starea video: [00:14:54](https://www.youtube.com/watch?v=t2T88IiiP48&t=894s)

```csharp
var d = new Derived();
d.baseField = 12;
Console.WriteLine(((Base)d).baseField); // 12

class Base { public int baseField; }
class Derived : Base { public int derField; }
```

Introducem apoi o clasă fără legătură, `A`, cu aceleași două câmpuri. **Compatibilitatea** urmează relațiile dintre tipuri, nu potrivirea numelor sau dimensiunilor câmpurilor. Modificare video corelată: [00:15:07](https://www.youtube.com/watch?v=t2T88IiiP48&t=907s) [00:15:22](https://www.youtube.com/watch?v=t2T88IiiP48&t=922s)

```csharp
var a = new A();
// Base b = (Base)a;       // eroare la compilare
// Derived d = (Derived)a; // eroare la compilare

class Base { public int baseField; }
class Derived : Base { public int derField; }
class A
{
    public int baseField;
    public int derField;
}
```

Datele identice nu îi dau lui `A` o conversie către niciuna dintre clase. Eliminarea tuturor câmpurilor lui `A` nu ar ajuta. Compilatorul privește la ce moștenește `A`, iar `A` nu moștenește acest `Base` sau `Derived`. [00:15:49](https://www.youtube.com/watch?v=t2T88IiiP48&t=949s) [00:16:03](https://www.youtube.com/watch?v=t2T88IiiP48&t=963s) [00:16:15](https://www.youtube.com/watch?v=t2T88IiiP48&t=975s)

Conversia anterioară a funcționat pentru că douipunctul declara explicit relația. Dacă două clase împart o bază definită de utilizator, oricare poate fi furnizată prin acea bază, dar asta nu le face interschimbabile ca tipuri concrete surori. „Fără bază comună” înseamnă aici lipsa unei baze definite de utilizator care să furnizeze membrii doriți; aceste clase împart în continuare rădăcina încorporată `object`. [00:16:24](https://www.youtube.com/watch?v=t2T88IiiP48&t=984s) [00:16:27](https://www.youtube.com/watch?v=t2T88IiiP48&t=987s)

## Păstrează instanțe fără legătură în object

Înlocuiește exemplul de moștenire cu `A` și `B`, fiecare cu un singur câmp declarat. Problema motivantă este păstrarea referinței oricărui obiect într-o singură variabilă. **`object`**, denumirea C# pentru `System.Object`, este tipul referință rădăcină comun care permite acest lucru. Starea video inițială și reatribuirea ulterioară, simplificate împreună: [00:16:39](https://www.youtube.com/watch?v=t2T88IiiP48&t=999s) [00:16:57](https://www.youtube.com/watch?v=t2T88IiiP48&t=1017s) [00:17:06](https://www.youtube.com/watch?v=t2T88IiiP48&t=1026s)

```csharp
var a = new A();
var b = new B();

object ob = a; // prima stare
ob = b;        // starea ulterioară

class A { public int FieldA; }
class B { public int FieldB; }
```

Fiecare `new` creează un obiect distinct, care conține propriul câmp întreg și propria asociere de tip. Ambele câmpuri nescrise au **valoarea implicită** zero. Alte mecanisme ale obiectului sunt amânate intenționat. [00:17:14](https://www.youtube.com/watch?v=t2T88IiiP48&t=1034s) [00:17:21](https://www.youtube.com/watch?v=t2T88IiiP48&t=1041s) [00:17:34](https://www.youtube.com/watch?v=t2T88IiiP48&t=1054s) [00:17:58](https://www.youtube.com/watch?v=t2T88IiiP48&t=1078s)

Alături de instanțe și descrierile lor de tip, desenul adaugă descrierea tipului încorporat special `object`. Aceasta este disponibilă automat, fără o declarație a utilizatorului. Nu este o a treia instanță de utilizator creată printr-o operație invizibilă `new object()`. [00:17:36](https://www.youtube.com/watch?v=t2T88IiiP48&t=1056s) [00:17:43](https://www.youtube.com/watch?v=t2T88IiiP48&t=1063s)

Urmește referința înainte de a o schimba:

1. Desenul plasează zonele de câmpuri ale celor două instanțe la adresele ilustrative `4000` și `5000`. `a` și `b` primesc referințele corespunzătoare. Ca înainte, referințele din diagramă sunt situate dincolo de antetul de tip. [00:18:13](https://www.youtube.com/watch?v=t2T88IiiP48&t=1093s)
2. `object ob = a` copiază referința lui `a`, desenată ca `4000`, în noua locală. Diagrama plasează locala în memorie temporară; stocarea sa fizică efectivă nu face parte din lecție. [00:18:51](https://www.youtube.com/watch?v=t2T88IiiP48&t=1131s)
3. Referința păstrată identifică **întregul obiect**. Privirea ei prin `object` restrânge membrii vizibili prin referință; nu creează o alocare separată doar cu antet și nu trunchiază instanța. Afirmația înregistrării „se ia în considerare doar partea de tip” descrie această vedere mai îngustă. [00:18:23](https://www.youtube.com/watch?v=t2T88IiiP48&t=1103s) [00:18:35](https://www.youtube.com/watch?v=t2T88IiiP48&t=1115s) [00:18:44](https://www.youtube.com/watch?v=t2T88IiiP48&t=1124s)
4. **Tipul declarat** al lui `ob` rămâne `object`, în timp ce **tipul la execuție** al instanței referite este `A`. Alocarea sa poate conține mai multe date decât expune tipul declarat. [00:19:02](https://www.youtube.com/watch?v=t2T88IiiP48&t=1142s) [00:19:10](https://www.youtube.com/watch?v=t2T88IiiP48&t=1150s)

Păstrează separate adresele de următoarea corectură verbală. `10000` este adresa descrierii tipului `object` din desen, iar `99000` reprezintă descrierea unui alt tip concret. Clarificarea spune că **`ob` conține referința de instanță `4000`**; instanța de la adresa respectivă înregistrează tipul concret, nu tipul declarat al localei. `99000` nu este adresa instanței nou salvate în `ob`. [00:19:21](https://www.youtube.com/watch?v=t2T88IiiP48&t=1161s) [00:19:39](https://www.youtube.com/watch?v=t2T88IiiP48&t=1179s)

În sfârșit, **suprascrierea** înlocuiește referința păstrată: `ob = b` face ca `ob` să identifice a doua instanță. Nu schimbă tipul primului obiect și nu îi convertește câmpul în câmpul lui `B`. A doua instanță poate fi folosită printr-o referință `B`, cu câmpul său, sau printr-o referință `object`, fără ca acel câmp să fie expus. Ambele vederi îi păstrează identitatea. [00:19:52](https://www.youtube.com/watch?v=t2T88IiiP48&t=1192s) [00:19:58](https://www.youtube.com/watch?v=t2T88IiiP48&t=1198s)

## Recuperează o referință B cu o conversie

O variabilă de tip `object` nu poate expune un câmp specific lui `B` doar pentru că se întâmplă să se refere la un `B`. Folosește o **conversie explicită de referință**, care cere o vedere țintă și verifică că obiectul efectiv o acceptă. Noua sintaxă din starea video este: [00:20:23](https://www.youtube.com/watch?v=t2T88IiiP48&t=1223s) [00:20:31](https://www.youtube.com/watch?v=t2T88IiiP48&t=1231s) [00:20:43](https://www.youtube.com/watch?v=t2T88IiiP48&t=1243s)

```csharp
object ob = new B();
B b1 = (B)ob;
Console.WriteLine(b1.FieldB);

class B { public int FieldB; }
```

`(B)ob` se citește „convertește valoarea lui `ob` într-o referință utilizabilă ca `B`”. După o verificare reușită, `b1` păstrează o referință `B`, iar accesul la câmp este permis. Conversia nu creează niciun obiect de înlocuire, nici o copie a câmpurilor sale. [00:20:43](https://www.youtube.com/watch?v=t2T88IiiP48&t=1243s) [00:20:54](https://www.youtube.com/watch?v=t2T88IiiP48&t=1254s)

Tipul declarat `object` promite doar vederea generală de obiect. Nu promite că există vreun câmp concret anumit, așa că vederea mai specifică necesită validare. [00:20:59](https://www.youtube.com/watch?v=t2T88IiiP48&t=1259s)

## Păstrează obiectul după ce numele locale ies din domeniu

De ce să convertești, dacă o variabilă concretă `b` era deja disponibilă? Mută alocarea ei într-un **bloc**, delimitat de acolade. **Domeniul** unei variabile locale este regiunea în care numele ei poate fi folosit. În afara blocului, numele `b` nu este disponibil, dar o locală exterioară își poate păstra obiectul accesibil. Prima stare video bazată pe bloc este reconstruită aici: [00:21:11](https://www.youtube.com/watch?v=t2T88IiiP48&t=1271s) [00:21:27](https://www.youtube.com/watch?v=t2T88IiiP48&t=1287s)

```csharp
object ob;
{
    var b = new B();
    b.FieldB = 18;
    ob = b;
}

B b1 = (B)ob;
Console.WriteLine(b1.FieldB); // 18

class B { public int FieldB; }
```

Referința păstrată în `ob` este citită, verificată ca compatibilă cu `B` și copiată în `b1`. Apoi câmpul este citit prin acea referință concretă. Valoarea demonstrată a câmpului este `18`, urmată de `20` după schimbarea exemplului. Acestea sunt valori succesive, nu conversii reușite dintr-un `A` fără legătură către `B`. [00:21:39](https://www.youtube.com/watch?v=t2T88IiiP48&t=1299s)

Parcurgerea de memorie următoare introduce două obiecte și o suprascriere intermediară. Stare video corelată: [00:21:47](https://www.youtube.com/watch?v=t2T88IiiP48&t=1307s) [00:21:57](https://www.youtube.com/watch?v=t2T88IiiP48&t=1317s) [00:22:16](https://www.youtube.com/watch?v=t2T88IiiP48&t=1336s)

```csharp
object ob; // atribuit înainte de orice citire
{
    var a = new A();
    var b = new B();
    a.FieldA = 18;
    b.FieldB = 20;
    ob = a; // intermediar: instanță A
    ob = b; // final: instanță B
}

B b1 = (B)ob;
Console.WriteLine(b1.FieldB); // 20

class A { public int FieldA; }
class B { public int FieldB; }
```

Urmează fiecare stare:

1. `object ob;` declară o locală neinițializată. **atribuirea definită** cere ca ea să primească o valoare înainte de a fi citită; aceasta diferă de inițializarea automată cu zero a unui câmp întreg nou. [00:21:47](https://www.youtube.com/watch?v=t2T88IiiP48&t=1307s)
2. Alocă `A`, apoi `B`. Fiecare pornește cu câmpul său propriu la zero și cu tipul la execuție numit de alocarea sa. Păstrează referințele în localele de bloc `a` și `b`. [00:21:57](https://www.youtube.com/watch?v=t2T88IiiP48&t=1317s) [00:22:12](https://www.youtube.com/watch?v=t2T88IiiP48&t=1332s)
3. Scrie valorile întregi `18` și `20` în câmpuri. Aceste valori sunt date, nu referințe de obiect. Copierea referințelor are loc separat, în atribuirile către `ob`. [00:22:13](https://www.youtube.com/watch?v=t2T88IiiP48&t=1333s)
4. Copiază mai întâi `a` în `ob`, apoi suprascrie cu `b`. Referința final păstrată identifică `B`. [00:22:16](https://www.youtube.com/watch?v=t2T88IiiP48&t=1336s)
5. Se termină blocul. Numele locale ies din domeniu, dar obiectul `B` accesibil nu dispare. `ob` nu mai păstrează accesibilă instanța `A` suprascrisă. Ieșirea din domeniu nu distruge prin ea însăși ambele obiecte alocate. [00:22:22](https://www.youtube.com/watch?v=t2T88IiiP48&t=1342s)
6. Recuperează vederea concretă cu o verificare la execuție. `object` ca tip static nu promite `B`, iar pierderea numelui local nu face valoarea păstrată necunoscută pentru programul care rulează. Conversia folosește instanța efectiv referită atunci când se execută. [00:22:31](https://www.youtube.com/watch?v=t2T88IiiP48&t=1351s) [00:22:41](https://www.youtube.com/watch?v=t2T88IiiP48&t=1361s)

O **expresie de conversie** este tipul țintă între paranteze urmat de expresia convertită. Rezultatul ei are tipul static `B`, deci poate fi atribuit unei locale `B` și folosit pentru accesul ulterior la câmp. Parantezele suplimentare de grupare păstrează acest sens. Explicație video a sintaxei: [00:22:45](https://www.youtube.com/watch?v=t2T88IiiP48&t=1365s) [00:22:55](https://www.youtube.com/watch?v=t2T88IiiP48&t=1375s)

```csharp
B b1 = (B)ob;
// Grupare echivalentă, ca instrucțiune alternativă:
// B b1 = ((B)(ob));
```

Exemplul cu domeniul motivează verificarea; un compilator nu primește voie să acceseze `FieldB` prin `object` doar pentru că un programator poate vedea o atribuire anterioară în sursă.

## Urmează verificarea de compatibilitate la execuție

O **verificare la execuție** consultă obiectul efectiv în timpul rulării. Extinde conversia în procedura de model a adreselor din lecție. Starea video relevantă, simplificată pentru a păstra valoarea finală: [00:23:08](https://www.youtube.com/watch?v=t2T88IiiP48&t=1388s)

```csharp
object ob = new B();
B b1 = (B)ob;
Console.WriteLine(b1.FieldB); // 20

class B { public int FieldB = 20; }
```

1. Evaluează expresia sursă: citește referința păstrată în `ob`, desenată ca `5000`, și identifică instanța respectivă. [00:23:08](https://www.youtube.com/watch?v=t2T88IiiP48&t=1388s)
2. Inspectează tipul la execuție asociat ei. Diagrama urmează celula ascunsă de tip până la descrierea tipului. [00:23:16](https://www.youtube.com/watch?v=t2T88IiiP48&t=1396s) [00:23:31](https://www.youtube.com/watch?v=t2T88IiiP48&t=1411s)
3. Compară tipul efectiv cu ținta cerută `B`. Compilatorul știe ce țintă este scrisă în conversie; runtime-ul verifică ce obiect este prezent. Egalitatea exactă este suficientă, iar un obiect derivat din tipul cerut este de asemenea compatibil. Înregistrarea compară adresa descrierii tipului efectiv cu adresa așteptată, ilustrată ca `8000` pentru `B`. [00:23:39](https://www.youtube.com/watch?v=t2T88IiiP48&t=1419s)
4. Dacă este compatibil, aceeași referință de instanță devine rezultatul conversiei și este salvată în `b1`. Tipurile efectiv și țintă nu trebuie să fie exact egale atunci când moștenirea susține conversia. [00:24:06](https://www.youtube.com/watch?v=t2T88IiiP48&t=1446s) [00:24:26](https://www.youtube.com/watch?v=t2T88IiiP48&t=1466s) [00:24:36](https://www.youtube.com/watch?v=t2T88IiiP48&t=1476s)
5. Dacă este incompatibil, aruncă **`InvalidCastException`**, excepția pentru această conversie de referință nereușită. În înregistrare nu este tratată, deci programul se oprește. Adrese diferite ale descrierilor de tip, luate singure, nu dovedesc eșecul; trebuie luate în calcul și relațiile de bază. [00:23:55](https://www.youtube.com/watch?v=t2T88IiiP48&t=1435s) [00:24:26](https://www.youtube.com/watch?v=t2T88IiiP48&t=1466s)

Această verificare schimbă tipul static al expresiei de referință, nu tipul obiectului la execuție. Cu `b1` static tipizat ca `B`, accesul la câmp folosește locația cunoscută a câmpului în același mod ca în exemplul anterior cu deplasamentul. Valoarea este citită și afișată, fără căutare după numele câmpului și fără acceptarea unui câmp coincident pe o clasă fără legătură. [00:24:48](https://www.youtube.com/watch?v=t2T88IiiP48&t=1488s) [00:25:06](https://www.youtube.com/watch?v=t2T88IiiP48&t=1506s)

[Documentația conversiilor](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions) confirmă atât identitatea partajată a instanței după o conversie de referință, cât și posibilitatea unui `InvalidCastException` în timpul execuției.

<details>
<summary>Dacă conversia reușește, poate o scriere prin b1 să schimbe obiectul atins și prin ob?</summary>

Da. b1 păstrează o altă referință către obiectul existent. Verificarea autorizează o vedere de tip B; nu alocă un obiect independent.

</details>

## Rulează conversia incompatibilă

O **eroare la compilare** împiedică construirea programului; o **eroare la execuție** apare în timpul executării lui. Schimbă ținta în clasa fără legătură `A`, lăsând sursa declarată ca `object`. Compilatorul permite conversia pentru că o referință `object` poate, în general, referi un `A`, dar această rulare furnizează `B`. Starea video schimbată: [00:25:13](https://www.youtube.com/watch?v=t2T88IiiP48&t=1513s)

```csharp
object ob = new B();
A b1 = (A)ob; // compilează, apoi aruncă InvalidCastException

class A { }
class B { }
```

Rularea proiectului raportează o eroare la conversie. Runtime-ul urmează referința de instanță păstrată, îi descoperă tipul `B` și îl verifică față de `A` așteptat. Relația de moștenire nu permite această conversie. Nici câmpurile asemănătoare, nici faptul că împart `object` nu fac clasele interschimbabile. [00:25:22](https://www.youtube.com/watch?v=t2T88IiiP48&t=1522s) [00:25:32](https://www.youtube.com/watch?v=t2T88IiiP48&t=1532s) [00:25:42](https://www.youtube.com/watch?v=t2T88IiiP48&t=1542s) [00:26:02](https://www.youtube.com/watch?v=t2T88IiiP48&t=1562s)

Aceasta contrastează cu conversia directă anterioară, a cărei expresie sursă era deja static tipizată ca `A` fără legătură: acea conversie a fost respinsă la compilare. Aici, declarația sursă mai generală lasă compatibilitatea obiectului efectiv în seama execuției.

## Distinge surorii de baza lor comună

Introducem acum o **clasă de bază** cu două **clase derivate surori**: fiecare extinde aceeași bază, dar niciuna nu este cealaltă suroră. Alocă `Derived` și cere o vedere concretă de soră, apoi consideră separat vederea bazei comune. Modificări video: [00:26:12](https://www.youtube.com/watch?v=t2T88IiiP48&t=1572s) [00:26:24](https://www.youtube.com/watch?v=t2T88IiiP48&t=1584s)

```csharp
object ob = new Derived();

// Testează aceste alternative în rulări separate:
A b1 = (A)ob;             // eșuează: Derived nu este A
// Base b1 = (Base)ob;    // reușește

class Base { }
class Derived : Base { }
class A : Base { }
```

Înregistrarea dă instanței o adresă, aproximativ `5000`, și distinge descrierile lui `Derived` și `A` la adresele ilustrative `10000` și `9000`. Convertirea instanței la sora `A` eșuează; convertirea ei la `Base` reușește, deși tipul său exact la execuție este `Derived`. O conversie nereușită netratată oprește execuția, deci alternativa reușită nu trebuie pusă după ea și așteptată să ruleze. [00:26:35](https://www.youtube.com/watch?v=t2T88IiiP48&t=1595s) [00:26:51](https://www.youtube.com/watch?v=t2T88IiiP48&t=1611s)

**Compatibilitatea** este relația de tip care permite o vedere cerută. Modelul desenează o listă de tipuri compatibile, inclusiv o referință către descrierea lui `Base`. Aceasta este o explicație a relațiilor de execuție, nu o listă ascunsă, scrisibilă, controlată de codul aplicației. Runtime-ul consultă asemenea relații pentru a stabili dacă o conversie este posibilă. [00:27:08](https://www.youtube.com/watch?v=t2T88IiiP48&t=1628s) [00:27:34](https://www.youtube.com/watch?v=t2T88IiiP48&t=1654s)

Proprietatea publică **`Type.BaseType`** expune descrierea bazei imediate fără a necesita o conversie a instanței. Stare video corelată: [00:27:20](https://www.youtube.com/watch?v=t2T88IiiP48&t=1640s)

```csharp
object ob = new Derived();
Type actual = ob.GetType();
Type? parent = actual.BaseType; // descrie Base

class Base { }
class Derived : Base { }
```

`actual` descrie `Derived`; `parent` descrie `Base`. O **referință nullable**, marcată cu `?`, permite null, deoarece unele tipuri nu au bază. Obținerea metadelor nu convertește `ob` și nu înlocuiește instanța sa. face relația inspectabilă, iar acesta este tipul de relație pe care runtime-ul îl folosește pentru a valida conversia compatibilă către bază.

## Ajunge la același tip de bază pe două căi

**`typeof`** obține o descriere de tip dintr-un nume de tip, nu dintr-o instanță. Compară această cale directă cu urmărirea lui **`BaseType`**, descrierea bazei imediate a tipului efectiv. Comparație video corelată: [00:27:40](https://www.youtube.com/watch?v=t2T88IiiP48&t=1660s) [00:27:54](https://www.youtube.com/watch?v=t2T88IiiP48&t=1674s)

```csharp
object ob = new Derived();
Type? viaInstance = ob.GetType().BaseType;
Type direct = typeof(Base);
// viaInstance și direct descriu același tip Base.

class Base { }
class Derived : Base { }
```

O cale este „`ob` → instanță `Derived` → descrierea tipului `Derived` → descrierea tipului `Base`”. Cealaltă cere `Base` direct, după nume. Ambele ajung la același tip descris, de aceea referințele pot fi comparate. [00:28:04](https://www.youtube.com/watch?v=t2T88IiiP48&t=1684s) [00:28:13](https://www.youtube.com/watch?v=t2T88IiiP48&t=1693s)

Variabila rezultată descrie **tipul de bază** al instanței, nu tipul său exact la execuție. Instanța rămâne `Derived`. Această comparație este între descrieri de tip, nu între o referință de instanță și o referință de metadate. [00:28:22](https://www.youtube.com/watch?v=t2T88IiiP48&t=1702s)

## Aplică BaseType și typeof

Păstrează descrierea bazei într-o variabilă `Type` care nu permite null, afișează numele ei, cere direct același tip și compară. **Operatorul de anulare a avertismentului de nul** este `!` postfix pe o expresie; nu este semn de punctuație în numele variabilei. Modificare video: [00:28:29](https://www.youtube.com/watch?v=t2T88IiiP48&t=1709s) [00:28:38](https://www.youtube.com/watch?v=t2T88IiiP48&t=1718s)

```csharp
object ob = new Derived();

Type b = ob.GetType().BaseType!;
Console.WriteLine(b.Name); // Base

Type direct = typeof(Base);
Console.WriteLine(b == direct); // True

class Base { }
class Derived : Base { }
```

1. Obține tipul lui `Derived`, apoi baza sa imediată. În acest exemplu cunoscut baza există. `!` îi spune **analizei de nul** să accepte expresia ca nenulă; nu adaugă nicio garanție la execuție și ar lăsa o valoare null neschimbată. **Null** înseamnă că nu este referit niciun obiect. [00:28:38](https://www.youtube.com/watch?v=t2T88IiiP48&t=1718s) Acesta este [contractul exact al operatorului de anulare a avertismentului de nul](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving).
2. **`Name`** citește numele tipului descris, `Base`. În desen, programul urmează referința descrierii de tip până la câmpul său de șir și transmite acea referință de șir către `Console.WriteLine`. Un **șir** reprezintă textul într-un obiect de referință separat. [00:28:48](https://www.youtube.com/watch?v=t2T88IiiP48&t=1728s)
3. **`typeof(Base)`** cere descrierea tipului denumit și o păstrează într-o altă variabilă `Type`. Nu construiește o instanță `Base`. [00:29:00](https://www.youtube.com/watch?v=t2T88IiiP48&t=1740s)
4. **Egalitatea de tipuri**, exprimată cu `==`, compară cele două descrieri. Desenul reprezintă ambele căi ca ajungând la `8000`, făcând ca modelul numeric comparația `8000 == 8000`. Codul aplicației compară valori `Type`, nu adrese de memorie expuse. [00:29:17](https://www.youtube.com/watch?v=t2T88IiiP48&t=1757s) [00:29:28](https://www.youtube.com/watch?v=t2T88IiiP48&t=1768s)
5. Înregistrarea afișează `True`, confirmând că ambele căi dau același tip `Base`. Comparația afișată încheie acest experiment înaintea următorului exemplu de moștenire. [00:29:25](https://www.youtube.com/watch?v=t2T88IiiP48&t=1765s) [00:29:40](https://www.youtube.com/watch?v=t2T88IiiP48&t=1780s)

Descrierea găsită prin `BaseType` explică de ce o conversie compatibilă către bază poate reuși fără egalitate de tip exact. Ea nu spune că tipul exact al unei instanțe `Derived` a devenit `Base`.

<details>
<summary>De ce este b egal cu typeof(Base) și ce protecție adaugă semnul exclamării?</summary>

GetType descrie mai întâi Derived, apoi BaseType îi selectează descrierea Base. typeof(Base) cere direct aceeași descriere. Semnul exclamării suprimă avertismentele de nul; nu oferă nicio protecție la execuție.

</details>

## Încearcă două baze de clasă

**Moștenirea multiplă de clase** ar însemna derivarea directă din mai multe clase. C# nu o permite. Experimentul final imaginează provizoriu combinarea lui `Base1` și `Base2`, apoi explică de ce aceasta intră în conflict cu modelul simplu de referințe din înregistrare. Stare video ipotetică, intenționat invalidă în C#: [00:29:43](https://www.youtube.com/watch?v=t2T88IiiP48&t=1783s) [00:29:58](https://www.youtube.com/watch?v=t2T88IiiP48&t=1798s)

```csharp
class Base1 { public int Field1; }
class Base2 { public int Field2; }

// Nevalid: Base1 și Base2 sunt ambele clase.
class Derived : Base1, Base2
{
    public int Field3;
}
```

Problema ar fi să folosești un singur obiect prin oricare dintre cele două tipuri de bază, păstrând accesul corect la câmpuri și descoperirea tipului. Desenează câmpurile în această ordine propusă: `Field1`, `Field2`, `Field3`, inițial toate zero, cu un marcaj de tip înaintea lor. Este un experiment mental, nu o alocare compilată cu succes a clasei invalide. [00:30:20](https://www.youtube.com/watch?v=t2T88IiiP48&t=1820s) [00:30:34](https://www.youtube.com/watch?v=t2T88IiiP48&t=1834s)

Urmează cele două vederi propuse:

1. Imaginează alocarea obiectului combinat și păstrarea referinței sale, ilustrată ca `5000`, într-o variabilă de tip derivat. Primul său câmp vine din `Base1`, următorul din `Base2`, iar ultimul din `Derived`. [00:30:20](https://www.youtube.com/watch?v=t2T88IiiP48&t=1820s)
2. Consideră acea referință ca fiind `Base1`. În relația de moștenire imaginară, conversia ar copia aceeași adresă în variabila de tip bază. Primul câmp așteptat de ea ocupă poziția modelată a primului câmp. [00:30:45](https://www.youtube.com/watch?v=t2T88IiiP48&t=1845s) [00:30:59](https://www.youtube.com/watch?v=t2T88IiiP48&t=1859s)
3. Scrierea lui `Field1` prin acea vedere ajunge deci la câmpul intenționat. Prima vedere de bază pare consecventă până aici. [00:31:08](https://www.youtube.com/watch?v=t2T88IiiP48&t=1868s)
4. Imaginează acum că și vederea `Base2` primește `5000`. Nu a fost introdus niciun obiect nou și nicio adresă deplasată. Dar primul câmp așteptat de `Base2` nu este câmpul aflat în acel punct de referință din desenul combinat. [00:31:14](https://www.youtube.com/watch?v=t2T88IiiP48&t=1874s) [00:31:28](https://www.youtube.com/watch?v=t2T88IiiP48&t=1888s)

Aceste operații sunt raționamentul ipotetic al lecției. Declarația însăși nu oferă niciodată unui program C# real conversii legale către două baze de clasă directe.

## Verifică a doua vedere de bază

Un **deplasament de câmp** localizează un câmp față de poziția modelată a referinței. În schița combinată, câmpul propriu al lui `Base2` începe după datele altei baze. Așadar, asocierea sa de tip și câmpul său propriu nu pot fi tratate ca aceeași porțiune independentă, neîntreruptă, așteptată de modelul simplu. Moștenirea urmărește să facă o instanță derivată utilizabilă drept instanță de bază; păstrarea acestei promisiuni este dificultatea. [00:31:31](https://www.youtube.com/watch?v=t2T88IiiP48&t=1891s) [00:31:46](https://www.youtube.com/watch?v=t2T88IiiP48&t=1906s)

O imagine a lui `Base2` de sine stătător are marcajul de tip urmat imediat de `Field2`. În obiectul combinat, acel câmp este deplasat. Conversiile de referință obișnuite din C# nu oferă niciun deplasament reglabil al subobiectului de bază care să codifice vederea ipotetică a doua. [00:31:58](https://www.youtube.com/watch?v=t2T88IiiP48&t=1918s) [00:32:10](https://www.youtube.com/watch?v=t2T88IiiP48&t=1930s)

Parcurge soluțiile propuse:

1. **Păstrează adresa inițială.** O vedere `Base2` își așteaptă primul câmp la poziția standard, în timp ce desenul combinat pune acolo câmpul lui `Base1`. Copierea simplă a referinței lasă o nepotrivire de dispunere. [00:31:58](https://www.youtube.com/watch?v=t2T88IiiP48&t=1918s) [00:32:10](https://www.youtube.com/watch?v=t2T88IiiP48&t=1930s)
2. **Păstrează adresa celei de-a doua porțiuni.** Înregistrarea propune o referință care indică în interiorul obiectului și abreviază acea alternativă ca „5” în loc de `5000`. Tratează acest număr ca pe o poziție deplasată ilustrativă, nu ca pe un rezultat numeric al unei conversii C# legale. [00:32:16](https://www.youtube.com/watch?v=t2T88IiiP48&t=1936s)
3. **Încearcă să convertești înapoi referința deplasată.** Modelul găsește informații de tip imediat înaintea referinței păstrate. Cu deplasarea, marcajul real este mai departe în urmă, separat de datele primei baze. Conversia printr-o vedere generală de obiect și înapoi nu ar mai recupera întreaga instanță după acea regulă. [00:32:28](https://www.youtube.com/watch?v=t2T88IiiP48&t=1948s) [00:32:39](https://www.youtube.com/watch?v=t2T88IiiP48&t=1959s)
4. **Dublează un marcaj de tip înaintea fiecărei porțiuni de bază.** Aceasta ar schimba reprezentarea. Parcurgerea câmpurilor utilizatorului ar trebui acum să sară peste o celulă de metadate intercalară, în loc să avanseze prin secvența originală, contiguă, de date ale utilizatorului. [00:32:50](https://www.youtube.com/watch?v=t2T88IiiP48&t=1970s) [00:33:01](https://www.youtube.com/watch?v=t2T88IiiP48&t=1981s) [00:33:11](https://www.youtube.com/watch?v=t2T88IiiP48&t=1991s)
5. **Alege ce înseamnă un marcaj dublat.** Dacă acesta conține `Derived`, referința păstrată indică tot în interior, deși modelul tratează o referință ca început al obiectului. Doar repetarea tipului la execuție nu spune cum se recuperează începutul complet al obiectului din acea porțiune `Base2`. [00:33:17](https://www.youtube.com/watch?v=t2T88IiiP48&t=1997s) [00:33:25](https://www.youtube.com/watch?v=t2T88IiiP48&t=2005s)

Fiecare soluție sugerată cere reguli suplimentare pentru identitate, acces la câmpuri sau conversii de dus-întors. Ele explică o problemă a acestei reprezentări alese, nu o teoremă de imposibilitate pentru orice limbaj sau runtime. Lecția nu implementează un sistem alternativ de obiecte cu moștenire multiplă.

<details>
<summary>De ce nu rezolvă deplasarea unei referințe Base2 la câmpul său propriu întreaga problemă?</summary>

Ea corectează poziția aparentă a câmpului, dar separă referința de marcajul de tip inițial. Căutarea tipului din desen și conversia înapoi către obiectul complet au apoi nevoie de reguli suplimentare de reprezentare.

</details>

## Observă limita de compilare

O **eroare la compilare** împiedică programul să fie construit. Înregistrarea își încheie explorarea dispunerii spunând că întrebările deschise și abordările concurente posibile au condus la interzicerea acestei construcții. Citește asta drept explicația oferită de desenul lecției; compilatorul aplică o regulă a limbajului, nu testează efectiv fiecare ordine posibilă a câmpurilor. [00:33:37](https://www.youtube.com/watch?v=t2T88IiiP48&t=2017s)

Starea video finală încercată este în continuare o declarație de clasă invalidă: [00:34:01](https://www.youtube.com/watch?v=t2T88IiiP48&t=2041s)

```csharp
class Base1 { }
class Base2 { }
class Derived : Base1, Base2 { } // eroare de compilare
```

Respingerea demonstrată de compilator contrastează cu conversia incompatibilă `object` anterioară: acea conversie a compilat și apoi a eșuat la execuție, în timp ce două baze de clasă directe nu pot compila. Rezumatul verbal final leagă această restricție de incapacitatea modelului simplu de a aranja ambele mulțimi de câmpuri de bază și de a păstra comportamentul de căutare a referinței și tipului. [00:34:03](https://www.youtube.com/watch?v=t2T88IiiP48&t=2043s) [00:34:12](https://www.youtube.com/watch?v=t2T88IiiP48&t=2052s)

Limita limbajului este **o singură bază de clasă directă**, nu „doar un singur nume poate apărea după douipunct”. O **interfață** este un contract implementat, nu o a doua bază de clasă; mecanismele de interfață sunt în afara acestei lecții. [Documentația C# despre moștenire](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance) enunță regula bazei unice de clasă și face distincția între implementarea interfețelor.

Lecția se oprește la conversiile de referință și la această limită a moștenirii de clase. Ea amână tiparea detaliată a structurilor și împachetarea, dispatcherul polimorf al metodelor, date suplimentare în antetul obiectului, implementarea interfețelor și o descriere completă a dispunerii la execuție. Schițele sale numerice de memorie explică exemplele fără a oferi operații pe adrese brute pentru codul aplicației.

## Istoricul modificărilor de cod

Aceste stări video stabile identifică secvența de construire pentru încărcarea `t2T88IiiP48`, cea mai recentă la final. Exemplele sunt reconstrucții simplificate; nu a fost găsită nicio stare executabilă exactă, angajată. O revizie de achiziție a transcrierii nu este înlocuită cu istoricul codului predat.

- [00:01:03](https://www.youtube.com/watch?v=t2T88IiiP48&t=63s) — Alocă instanța goală de `Class`, obține `Type t` și afișează numele clasei.
- [00:06:32](https://www.youtube.com/watch?v=t2T88IiiP48&t=392s) — Adaugă un câmp de instanță inițializat cu `10`; valoarea îi aparține lui `c`, nu lui `t`.
- [00:07:35](https://www.youtube.com/watch?v=t2T88IiiP48&t=455s) — Declară `Derived : Base`, creează instanțe separate și își inițializează câmpurile.
- [00:09:57](https://www.youtube.com/watch?v=t2T88IiiP48&t=597s) — Schimbă câmpul instanței de bază separate de la `11` la `12`; recitirea și inspecția memoriei coincid.
- [00:11:49](https://www.youtube.com/watch?v=t2T88IiiP48&t=709s) — Șterge atribuirea redundantă și păstrează `Base b = d`, obținând două referințe tipizate către un singur obiect.
- [00:14:54](https://www.youtube.com/watch?v=t2T88IiiP48&t=894s) — Scrie explicit conversia ascendentă și afișează valoarea partajată a câmpului, `12`.
- [00:15:07](https://www.youtube.com/watch?v=t2T88IiiP48&t=907s) — Introduce clasa fără legătură `A`; declarațiile identice de câmpuri nu dau conversia.
- [00:16:39](https://www.youtube.com/watch?v=t2T88IiiP48&t=999s) — Lărgește variabila la `object`, ca să poată păstra o referință la o instanță de `A` sau de `B`.
- [00:19:52](https://www.youtube.com/watch?v=t2T88IiiP48&t=1192s) — Suprascrie referința păstrată cu referința celui de-al doilea obiect.
- [00:21:11](https://www.youtube.com/watch?v=t2T88IiiP48&t=1271s) — Mută localele concrete într-un bloc și păstrează o referință `object` exterioară pentru o conversie `B` verificată.
- [00:21:39](https://www.youtube.com/watch?v=t2T88IiiP48&t=1299s) — Observă `18` și apoi `20` în stări succesive ale valorilor câmpului.
- [00:22:16](https://www.youtube.com/watch?v=t2T88IiiP48&t=1336s) — Urmărește referința intermediară `A` și referința finală `B` după scrieri separate în câmpuri.
- [00:25:13](https://www.youtube.com/watch?v=t2T88IiiP48&t=1513s) - Redirecționează conversia către `A`; compilarea o permite, iar execuția raportează o conversie incompatibilă.
- [00:26:12](https://www.youtube.com/watch?v=t2T88IiiP48&t=1572s) — Dă două surori o bază `Base` comună; vederea de soră eșuează, iar vederea bazei comune este compatibilă.
- [00:28:29](https://www.youtube.com/watch?v=t2T88IiiP48&t=1709s) — Păstrează `BaseType!`, compară cu `typeof(Base)` și obține rezultatul afișat `True`.
- [00:29:43](https://www.youtube.com/watch?v=t2T88IiiP48&t=1783s) — Încearcă două baze de clasă; discuția ipotetică despre dispunere se încheie în respingerea de către compilator.
