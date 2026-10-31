> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Implementarea sumei unor numere cu tablouri și bucle

## Scop și vocabular

[00:00:00](https://www.youtube.com/watch?v=VhA2OupAYRc&t=0s) Transformăm algoritmul de adunare a numerelor descris anterior într-o funcție C# care poate fi apelată, modificând programul de bază deja disponibil. Sarcina este să legăm intrările și ieșirea sa de cod concret, apoi să înlocuim salturile explicite cu bucle tot mai expresive. Nu este o parcurgere de proiect sau de instalare.

Termenii folosiți pe parcursul acestei construcții sunt:

- **Algoritm, interfață și implementare:** un algoritm este o succesiune de acțiuni pentru obținerea unui rezultat. Interfața sa precizează intrările și ieșirea; implementarea execută acele acțiuni.
- **Listă, tablou, element și stocare contiguă:** o listă este o succesiune ordonată. Un tablou este o reprezentare concretă cu stocarea consecutivă a elementelor; un element este o valoare stocată. Contiguu înseamnă alăturat, fără goluri între pozițiile elementelor.
- **Celulă de memorie, adresă, indice și indexare:** o celulă este stocarea unei valori în desenul din lecție. O adresă identifică o locație. Un indice numără deplasările elementelor față de început; indexarea selectează elementul de la acea deplasare.
- **Tip și întreg:** un tip descrie valorile permise. `int` stochează numere întregi; `int[]` descrie un tablou de numere întregi.
- **Obiect, referință, dereferențiere și heap:** un obiect este tabloul alocat, împreună cu elementele și lungimea sa. O referință identifică acel obiect; dereferențierea o urmărește până la obiect. Heap-ul este zona de memorie dinamică folosită pentru aceste obiecte în modelul lecției.
- **Lungime, buffer, proprietate și view de felie:** lungimea numără celulele tabloului. Un buffer este stocarea rezervată pentru acele celule. O proprietate este o valoare cu nume, accesată prin intermediul unui obiect, ca în `a.Length`. Un view de felie descrie o porțiune din stocarea existentă; schimbarea întinderii unui view nu redimensionează tabloul de bază.
- **Declarare, inițializare, atribuire și expresie:** declararea introduce o variabilă; inițializarea îi furnizează prima valoare. Atribuirea evaluează expresia din dreapta și scrie rezultatul în stânga. O expresie este cod care produce o valoare.
- **Alocare și `new`:** alocarea creează stocare pentru un obiect. `new` este cuvântul-cheie de creare a obiectelor folosit aici pentru a aloca un tablou.
- **Inițializator de tablou, tip dedus, creare de tablou cu tip implicit și expresie de colecție:** un inițializator listează valorile inițiale ale elementelor în acolade. Deducerea înseamnă că compilatorul determină un tip sau o numărătoare din context sau din valori. Crearea de tablou cu tip implicit folosește `new[]` pentru a deduce tipul elementelor din valori. O expresie de colecție folosește paranteze pătrate, precum `[6, 8, 5]`, și are nevoie de un tip țintă potrivit.
- **Funcție, funcție locală, antet, corp și apel:** o funcție este cod executabil cu nume; o funcție locală este declarată în codul altei funcții. Antetul ei descrie rezultatul și intrările; corpul conține instrucțiunile. Un apel o invocă.
- **Tip de returnare, valoare returnată și `return`:** tipul de returnare precizează felul rezultatului. Valoarea returnată este rezultatul unui apel. `return` încheie acea invocare și furnizează rezultatul ei.
- **Parametru, argument, apelant, apelat și transmitere prin valoare:** un parametru este o variabilă de intrare într-o declarație de funcție. Un argument este expresia furnizată la apel. Apelantul invocă funcția apelată. Transmiterea obișnuită copiază valoarea argumentului în parametru.
- **Variabilă locală, temporar, domeniu și durată de viață:** o variabilă locală aparține unei funcții sau unui bloc; o temporară păstrează date de lucru. Domeniul stabilește unde numele ei poate fi folosit, iar durata de viață privește existența ei în timpul execuției.
- **Bloc și acolade:** un bloc grupează instrucțiuni în `{ }` și delimitează domeniul variabilelor declarate acolo.
- **Modificator și `static`:** un modificator este un cuvânt-cheie atașat unei declarații. `static` este păstrat pe funcția locală; semnificația sa detaliată este amânată explicit în această lecție.
- **Flux de control, etichetă și `goto`:** fluxul de control este ordinea execuției. O etichetă numește o instrucțiune; `goto` transferă execuția la ea.
- **Buclă, iterație, corp și stare:** o buclă repetă un corp de instrucțiuni. O repetare este o iterație. Starea sunt datele care se modifică și determină pasul următor, aici un indice și o sumă parțială.
- **Acumulator și atribuire compusă:** un acumulator păstrează un rezultat parțial. `s += value` adună la suma veche și scrie rezultatul înapoi; `i += 1` incrementează indicele cu unu.
- **Boolean, condiție, gardă și negație:** un boolean este `true` sau `false`; o condiție calculează o astfel de valoare. O gardă verifică dacă lucrul poate continua. Negația inversează `true` și `false`.
- **Limite și eroare de tip „off-by-one”:** limitele identifică indicii valizi. O eroare „off-by-one” alege o graniță cu o poziție prea devreme sau prea târziu.
- **`if`, `break` și cod inaccesibil:** `if` execută blocul asociat doar când condiția sa este îndeplinită. `break` părăsește bucla înconjurătoare. Codul inaccesibil nu are nicio cale de execuție care să ajungă la el.
- **`while`, buclă infinită, `for` și `foreach`:** `while` repetă cât timp o condiție este adevărată; `while (true)` continuă să repete dacă nu se iese explicit din control. `for` grupează inițializarea, condiția și actualizarea în antetul său. `foreach` vizitează direct valorile elementelor, folosind `in` pentru a identifica colecția.
- **`var`:** acesta îi cere compilatorului să deducă tipul unei variabile locale din inițializatorul ei; nu înseamnă „o variabilă limitată la numere”.
- **Doar limitele domeniului — null și depășirea întregilor:** null înseamnă absența unei referințe de obiect; depășirea întregilor înseamnă că un rezultat aritmetic depășește intervalul tipului său întreg. Tratarea oricăruia dintre ele este în afara implementării dezvoltate aici.
- **Compilator, IDE și debugger:** compilatorul verifică și traduce sursa. IDE-ul este mediul de editare care oferă sugestii de sintaxă și diagnostice. Debuggerul permite înregistrării să urmărească apelurile și valorile.

[Laboratorul de algoritmi](../../../labs/1_basic/11_algorithms.md) oferă contextul sarcinii precedente. [Laboratorul de implementare a algoritmului](../../../labs/1_basic/12_algorithms_code.md) este exercițiul asociat; [laboratorul de funcții](../../../labs/1_basic/05_functions.md) și [laboratorul de tehnici de bază](../../../labs/1_basic/10_basic.md) oferă practică suplimentară legată de copierea argumentelor și stocarea rezultatului. Aceste legături indică documente de laborator, nu commituri ale programului înregistrat.

[00:00:11](https://www.youtube.com/watch?v=VhA2OupAYRc&t=11s) O **interfață** descrie ce intră și ce se întoarce. [00:00:17](https://www.youtube.com/watch?v=VhA2OupAYRc&t=17s) Aici intrarea este un tablou de numere, așadar tablourile trebuie înțelese înainte de a declara parametrul. [00:00:26](https://www.youtube.com/watch?v=VhA2OupAYRc&t=26s) Ieșirea este un singur număr. Acest exemplu alege `int[]` pentru intrare și `int` pentru rezultat; tipul de returnare descrie rezultatul, nu întregul tablou de intrare.

[00:00:26](https://www.youtube.com/watch?v=VhA2OupAYRc&t=26s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
// Interface sketch only: an integer array goes in; one integer comes out.
// The later declaration adds a body implementing the algorithm.
static int SumNumbers(int[] r)
{
    // implementation goes here
}
```

Niciun fișier de sursă inspectat din commit nu conține exact aceste programe intermediare. Toate mostrele de mai jos sunt reconstrucții simplificate cu nume lizibile; transcriptul fixat consemnează explicațiile lor, iar legăturile cu marcaje de timp le localizează stările din video. Nu se afirmă că sunt teste compilate sau executate independent.

[recorded-transcript]: https://github.com/AntonC9018/uniCourse_csharp/blob/4814706ae2fafbc797829239a33afc2eeefa29e8/videos/csharp/07_%D0%A0%D0%B5%D0%B0%D0%BB%D0%B8%D0%B7%D0%B0%D1%86%D0%B8%D1%8F_%D0%B0%D0%BB%D0%B3%D0%BE%D1%80%D0%B8%D1%82%D0%BC%D0%B0_%D0%B8%D0%B7_%D0%BF%D1%80%D0%BE%D1%88%D0%BB%D0%BE%D0%B3%D0%BE_%D0%B2%D0%B8%D0%B4%D0%B5%D0%BE_%D0%9C%D0%B0%D1%81%D1%81%D0%B8%D0%B2%D1%8B_%D0%A6%D0%B8%D0%BA%D0%BB%D1%8B/internal/transcript.md

## O listă devine un tablou contiguu

[00:00:43](https://www.youtube.com/watch?v=VhA2OupAYRc&t=43s) O **listă** este ideea abstractă a valorilor aflate într-o ordine. Numerele deseneate pe hârtie și legate prin săgeți pot reprezenta una. Un **tablou** este o reprezentare concretă ai cărei **elemente**, valorile individuale, ocupă stocare consecutivă.

[00:00:56](https://www.youtube.com/watch?v=VhA2OupAYRc&t=56s) **Stocarea contiguă** înseamnă că pozițiile elementelor stau unele lângă altele, fără goluri sau salturi. Desenul folosește adrese consecutive precum 800, 801 și 802. Acestea sunt unități didactice de mărimea unui element, nu adrese de octeți literal pentru întregii C#.

[00:01:11](https://www.youtube.com/watch?v=VhA2OupAYRc&t=71s) O **adresă** identifică unde începe stocarea. Un **indice** numără câte elemente se sar de la acel început. Deoarece pozițiile elementelor sunt alăturate, pozițiile lor pot fi calculate în loc de a fi descoperite urmărind un lanț.

[00:01:21](https://www.youtube.com/watch?v=VhA2OupAYRc&t=81s) Dacă prima poziție se numește adresa 700, al cincilea element este cu patru poziții mai târziu, la 704 în acel model. Aritmetica reală a adreselor scalează deplasarea cu dimensiunea elementului. Indexarea tablourilor C# se ocupă de acea selecție pentru voi.

[00:01:21](https://www.youtube.com/watch?v=VhA2OupAYRc&t=81s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```text
// In the recording's element-slot model:
first element: index 0, position 700
fifth element: index 4, position 700 + 4
// Conceptually: element address = first-element address + index * element size
```

De aceea indicele primului element este zero: pentru a ajunge la el se sar zero elemente.

## Tipul tabloului, lungimea și referința

[00:01:42](https://www.youtube.com/watch?v=VhA2OupAYRc&t=102s) Un **tip** descrie valorile permise. `int[]` înseamnă un tablou ale cărui elemente sunt întregi; nu precizează câte sunt. Tablourile cu trei și cu cinci întregi au același tip de tablou.

[00:01:55](https://www.youtube.com/watch?v=VhA2OupAYRc&t=115s) **Lungimea** este numărul de celule stocat împreună cu obiectul de tablou respectiv. Membrul public C# `Length` este o **proprietate**, o valoare cu nume citită cu punctul după variabila de tablou. Nu face parte din scrierea tipului `int[]`.

[00:02:06](https://www.youtube.com/watch?v=VhA2OupAYRc&t=126s) Un **obiect** este tabloul alocat separat, care conține acele celule. În modelul de memorie dinamică al lecției, adică în **heap**, o variabilă locală nu conține toate elementele încorporate.

[00:02:20](https://www.youtube.com/watch?v=VhA2OupAYRc&t=140s) O **referință** este valoarea care identifică obiectul. Atribuirea tabloului către o variabilă memorează această referință. Înregistrarea o desenează ca pe o adresă; imaginea explică modul în care se ajunge la obiect, fără a promite o adresă brută accesibilă codului C# obișnuit.

[00:02:20](https://www.youtube.com/watch?v=VhA2OupAYRc&t=140s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int[] a = new int[3];
int[] b = new int[5];
// Same type, different object lengths:
Console.WriteLine(a.Length); // 3
Console.WriteLine(b.Length); // 5
// a and b contain references; their elements belong to their array objects.
```

[00:02:30](https://www.youtube.com/watch?v=VhA2OupAYRc&t=150s) Lungimea obiectului trebuie stabilită la crearea sa: alocarea rezervă un număr fix de celule.

[00:02:44](https://www.youtube.com/watch?v=VhA2OupAYRc&t=164s) Celulele existente pot fi suprascrise, dar obiectul de tablou existent nu poate câștiga celule suplimentare. Pentru a obține o altă dimensiune, se creează alt tablou și se copiază valorile. Un **view de felie**, o descriere a unei porțiuni din stocare, poate acoperi un număr diferit de elemente fără a redimensiona obiectul original. Înregistrarea menționează această distincție fără a dezvolta sintaxa de feliere.

## Creează un tablou și inspectează zerourile sale

[00:03:01](https://www.youtube.com/watch?v=VhA2OupAYRc&t=181s) **Declararea** introduce o variabilă cu tipul și numele ei. **Alocarea** creează stocarea obiectului de tablou. Sintaxa alocării este `new ElementType[count]`: `new int[5]` cere cinci celule de întregi.

[00:03:16](https://www.youtube.com/watch?v=VhA2OupAYRc&t=196s) **Expresia** `new`, cod care produce o valoare, creează obiectul și furnizează referința sa. **Atribuirea** scrie acea referință în variabilă. Numărătoarea de la acest loc de creare determină numărul inițial și permanent de celule al obiectului.

[00:03:29](https://www.youtube.com/watch?v=VhA2OupAYRc&t=209s) **`new`** este cuvântul-cheie de creare a obiectelor. Pentru aceste tablouri, ca și pentru obiectele de clasă din comparația din înregistrare, obiectul este creat în memorie dinamică, în **heap**; nu este stocat ca cinci elemente în interiorul variabilei locale.

[00:03:16](https://www.youtube.com/watch?v=VhA2OupAYRc&t=196s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int[] a = new int[5]; // reference in a; five cells in the array object
Console.WriteLine(a.Length); // 5
```

[00:03:39](https://www.youtube.com/watch?v=VhA2OupAYRc&t=219s) **Inițializarea** furnizează valorile de început. Un tablou `int` nou alocat inițializează fiecare element cu zero. Această garanție se aplică elementelor tabloului, nu unei variabile locale neatribuite.

[00:03:52](https://www.youtube.com/watch?v=VhA2OupAYRc&t=232s) Afișarea primelor celule este verificarea propusă în înregistrare pentru acea inițializare cu zero. Verificare reconstruită:

[00:03:52](https://www.youtube.com/watch?v=VhA2OupAYRc&t=232s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
Console.WriteLine(a[0]); // 0 before any write
Console.WriteLine(a[1]); // 0
Console.WriteLine(a[2]); // 0
```

Aceste comentarii indică valorile zero așteptate, explicate în înregistrare; nu se afirmă nicio execuție separată a acestei mostre.

## Scrie și citește celule indexate

[00:04:01](https://www.youtube.com/watch?v=VhA2OupAYRc&t=241s) **Indexarea** selectează o celulă folosind paranteze pătrate după variabila care deține referința tabloului. O **atribuire** scrie apoi valoarea din dreapta în acea celulă.

[00:04:01](https://www.youtube.com/watch?v=VhA2OupAYRc&t=241s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
a[0] = 6;
Console.WriteLine(a[0]); // reads 6
```

[00:04:12](https://www.youtube.com/watch?v=VhA2OupAYRc&t=252s) **Dereferențierea** înseamnă urmărirea referinței până la obiectul ei. Citirea sau scrierea unui element indexat implică pașii conceptuali următori:

1. Citește referința stocată în `a`.
2. [00:04:21](https://www.youtube.com/watch?v=VhA2OupAYRc&t=261s) Urmeaz-o până la obiectul de tablou, apoi localizează începutul stocării elementelor sale.
3. Aplică indicele ca pe o deplasare numărată în elemente. Indicele zero nu sare niciun element și selectează prima celulă.
4. [00:04:58](https://www.youtube.com/watch?v=VhA2OupAYRc&t=298s) Selectează stocarea acelei celule până la începutul celulei următoare. Săgețile din desen identifică locații sau granițe de celule; un element de tablou nu stochează o legătură către succesorul său.
5. [00:05:18](https://www.youtube.com/watch?v=VhA2OupAYRc&t=318s) Pentru o scriere, pune valoarea din dreapta, aici 6, în celula selectată. Atribuirea modifică celula, nu referința din `a`.

[00:05:48](https://www.youtube.com/watch?v=VhA2OupAYRc&t=348s) Pentru o citire, folosește valoarea curentă a celulei ca valoare a expresiei de indexare. Astfel, `a[0]` contribuie temporar cu 6 la expresia de afișare din jur, iar consola primește 6. Expresia din sursă este înlocuită conceptual cu valoarea ei evaluată; elementul stocat nu este consumat sau șters.

[00:05:56](https://www.youtube.com/watch?v=VhA2OupAYRc&t=356s) Indicele 1 sare un element, așadar scrierea lui 8 vizează a doua celulă. [00:06:16](https://www.youtube.com/watch?v=VhA2OupAYRc&t=376s) Indicele 2 sare două elemente, așadar scrierea lui 5 înlocuiește zeroul celei de-a treia celule.

[00:06:16](https://www.youtube.com/watch?v=VhA2OupAYRc&t=376s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
a[0] = 6; // first cell
a[1] = 8; // second cell
a[2] = 5; // third cell
// The original five-cell object now contains: 6, 8, 5, 0, 0
Console.WriteLine(a[0]); // 6
```

<details>
<summary>Atribuirea lui 8 la indicele 1 modifică referința tabloului sau primul element?</summary>

Niciuna. Referința identifică în continuare același tablou. Indicele 1 desemnează a doua celulă, a cărei valoare se schimbă din zero în 8.

</details>

## Inițializează valorile și scurtează sintaxa

[00:06:35](https://www.youtube.com/watch?v=VhA2OupAYRc&t=395s) Scrierile indexate individuale funcționează, dar o succesiune cunoscută poate fi furnizată la creare. Un **inițializator de tablou** este o listă de valori de început între acolade. Demonstrația schimbă exemplul la trei celule pentru cele trei valori 6, 8 și 5.

[00:06:35](https://www.youtube.com/watch?v=VhA2OupAYRc&t=395s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int[] a = new int[3] { 6, 8, 5 };
```

[00:06:55](https://www.youtube.com/watch?v=VhA2OupAYRc&t=415s) Valorile pot apărea pe linii separate; spațiile albe nu le schimbă ordinea sau numărul.

[00:06:55](https://www.youtube.com/watch?v=VhA2OupAYRc&t=415s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int[] a = new int[3]
{
    6,
    8,
    5
};
```

[00:07:02](https://www.youtube.com/watch?v=VhA2OupAYRc&t=422s) Dacă un inițializator explicit de trei celule furnizează o a patra valoare, **compilatorul**, instrumentul care verifică și traduce sursa, raportează nepotrivirea numărului de elemente. Înregistrarea demonstrează diagnosticul; numărul trebuie să corespundă lungimii declarate.

[00:07:02](https://www.youtube.com/watch?v=VhA2OupAYRc&t=422s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
// Deliberately invalid; the recording shows the count mismatch:
int[] a = new int[3] { 6, 8, 5, 1 };
```

[00:07:07](https://www.youtube.com/watch?v=VhA2OupAYRc&t=427s) Pentru a lăsa compilatorul să deducă numărătoarea, omite numărul explicit dintre paranteze. **Deducerea** înseamnă derivarea unei detalii omise din informații deja prezente. Aici trei valori furnizate implică lungimea trei.

[00:07:07](https://www.youtube.com/watch?v=VhA2OupAYRc&t=427s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int[] a = new int[] { 6, 8, 5 };
Console.WriteLine(a.Length); // 3
```

[00:07:11](https://www.youtube.com/watch?v=VhA2OupAYRc&t=431s) Interogarea lungimii raportează trei pentru aceste trei valori. [00:07:23](https://www.youtube.com/watch?v=VhA2OupAYRc&t=443s) Forma cu lungime dedusă alocă în continuare un buffer de dimensiune fixă, adică stocarea de elemente a obiectului, și memorează lungimea împreună cu obiectul. Deducerea numărătorii nu face tabloul redimensionabil.

[00:07:35](https://www.youtube.com/watch?v=VhA2OupAYRc&t=455s) **IDE-ul**, mediul de editare, sugerează eliminarea unui alt tip redundant. O **creare de tablou cu tip implicit** folosește `new[]`; compilatorul deduce tipul elementelor din valorile întregi listate. Aceasta este distinctă de `new()` cu tip țintă, care nu este scurtătura pentru tablouri ilustrată aici.

[00:07:35](https://www.youtube.com/watch?v=VhA2OupAYRc&t=455s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int[] a = new[] { 6, 8, 5 }; // the values infer int as the element type
```

[00:07:54](https://www.youtube.com/watch?v=VhA2OupAYRc&t=474s) O **expresie de colecție** listează valori în paranteze pătrate, fără `new`. Cu tipul țintă dat explicit ca `int[]`, sintaxa modernă are aceleași conținuturi de tablou intenționate:

[00:07:54](https://www.youtube.com/watch?v=VhA2OupAYRc&t=474s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int[] a = [6, 8, 5]; // requires a C# version supporting collection expressions
```

Toate aceste forme valide produc în acest exemplu un tablou de trei întregi. Formele mai vechi cu acolade rămân utile când un proiect nu suportă sintaxa nouă cu paranteze pătrate.

## Declară interfața funcției de însumare

[00:08:06](https://www.youtube.com/watch?v=VhA2OupAYRc&t=486s) O **funcție locală** este cod executabil cu nume, declarat în funcția programului care o înconjoară. **Antetul** precizează rezultatul și intrările sale. Ordinea corectă a declarației aici este `static`, apoi tipul de returnare `int`, apoi numele `SumNumbers`.

[00:08:17](https://www.youtube.com/watch?v=VhA2OupAYRc&t=497s) Un **modificator** este un cuvânt-cheie atașat unei declarații. Păstrează `static` așa cum este arătat; înregistrarea amână explicit semnificația sa detaliată. Această lecție nu dezvoltă comportamentul specific lui static.

[00:08:22](https://www.youtube.com/watch?v=VhA2OupAYRc&t=502s) Un nume util de funcție precizează lucrul pe care îl face: `SumNumbers` adună numerele furnizate. **Tipul său de returnare**, `int`, descrie rezultatul întreg unic.

[00:08:28](https://www.youtube.com/watch?v=VhA2OupAYRc&t=508s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
// Header sketch: the body will be filled with the existing algorithm.
static int SumNumbers(int[] r)
{
    // implementation goes here
}
```

[00:08:28](https://www.youtube.com/watch?v=VhA2OupAYRc&t=508s) Un **parametru** este o variabilă declarată ca intrare între parantezele rotunde. În corp, `r` se comportă ca o variabilă obișnuită de tip `int[]`. Un **argument** este expresia furnizată la apelul acestei funcții.

[00:08:45](https://www.youtube.com/watch?v=VhA2OupAYRc&t=525s) Parametrii de intrare suplimentari ar fi separați prin virgule între acele paranteze; această funcție de însumare are nevoie doar de un parametru de tip tablou.

[00:08:48](https://www.youtube.com/watch?v=VhA2OupAYRc&t=528s) **Interfața** descrie intrarea și rezultatul. **Corpul**, instrucțiunile dintre acolade, este **implementarea** algoritmului. Un **bloc** grupează acele instrucțiuni. [00:08:55](https://www.youtube.com/watch?v=VhA2OupAYRc&t=535s) Corpul poate fi rescris păstrând aceeași interfață, astfel încât apelantul să nu trebuiască modificat în timpul conversiilor ulterioare la bucle.

[00:09:04](https://www.youtube.com/watch?v=VhA2OupAYRc&t=544s) O **declarare** fără inițializator introduce o variabilă locală fără valoare atribuită. Declarația discutată în timp ce mutăm acest corp este un acumulator de întregi, `int s;`, care trebuie apoi să primească zero-ul inițial. Aceeași regulă de atribuire înainte de folosire se aplică și unei referințe de tablou declarate: declararea ei nu creează un obiect de tablou.

[00:09:04](https://www.youtube.com/watch?v=VhA2OupAYRc&t=544s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int s;       // declared, not yet assigned
s = 0;       // initialize the accumulator before reading it

int[] pending;    // declared reference, no array created
pending = new int[3]; // assign before accessing its elements or Length
```

Un **acumulator** este o variabilă care reține rezultatul parțial. Zero-ul său inițial face ca prima adunare să fie semnificativă.

[00:09:11](https://www.youtube.com/watch?v=VhA2OupAYRc&t=551s) O **proprietate** este o valoare cu nume la care se ajunge printr-un obiect. Citește numărul de celule ca `r.Length`, nu doar `r` și nici un nume fără legătură precum `Len`. Variabila în sine evaluează la referință, în timp ce proprietatea sa de lungime evaluează la numărul de elemente.

[00:09:11](https://www.youtube.com/watch?v=VhA2OupAYRc&t=551s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int length = r.Length; // numeric count belonging to the referenced array
```


## Mută algoritmul într-un corp cu goto

[00:09:23](https://www.youtube.com/watch?v=VhA2OupAYRc&t=563s) Codul de bază existent exprimă deja pașii însumării. Lecția îi portă mai întâi acea logică folosind **`goto`**-ul de bază al lui C#, un transfer de control către o **etichetă** cu nume, înainte de a-l înlocui cu bucle. Scopul declarat este de a renunța la goto cât mai curând.

Algoritmul de plecare, reconstruit din procedura anterioară și din transferul descris aici, este:

1. Inițializează un acumulator cu zero și un indice cu zero.
2. Verifică dacă indicele a ajuns la capăt.
3. Dacă da, încheie cu acumulatorul drept răspuns.
4. Altfel, adună elementul indexat, avansează indicele și repetă verificarea.

[00:09:23](https://www.youtube.com/watch?v=VhA2OupAYRc&t=563s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
// Existing algorithm shape, before wrapping it in a function:
int[] r = [6, 8, 5];
int s = 0;
int i = 0;

CheckIndex:
if (i >= r.Length)
    goto Finished;
s += r[i];
i += 1;
goto CheckIndex;

Finished:
Console.WriteLine(s);
```

[00:09:29](https://www.youtube.com/watch?v=VhA2OupAYRc&t=569s) **Fluxul de control** determină ce instrucțiune rulează în continuare. O etichetă se termină cu două puncte; un goto numește destinația. Poate sări înainte, către o verificare, sau înapoi, către lucru repetat. Etichetele nu stochează valori.

Mută instrucțiunile de lucru în funcție, citește lungimea din parametrul ei și înlocuiește „termină și afișează” cu un **`return`**, care încheie apelul și îi furnizează rezultatul. Următorul exemplu păstrează deliberat vizibile atât saltul înainte, cât și pe cel înapoi:

[00:09:29](https://www.youtube.com/watch?v=VhA2OupAYRc&t=569s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
static int SumNumbers(int[] r)
{
    int s;
    s = 0;
    int i = 0;
    goto CheckIndex; // forward jump: check before any element read

AddElement:
    s += r[i];
    i += 1;

CheckIndex:
    if (i >= r.Length)
        return s;
    goto AddElement; // backward jump: repeat the work
}
```

O **atribuire compusă** precum `s += r[i]` calculează suma veche plus acest element și memorează rezultatul înapoi. Similar, `i += 1` incrementează indicele cu unu. Un punct și virgulă încheie fiecare instrucțiune obișnuită.

[00:09:37](https://www.youtube.com/watch?v=VhA2OupAYRc&t=577s) Un **apel** invocă funcția. Declară o variabilă care să păstreze valoarea returnată, trimite-ți numerele ca argument și atribuie rezultatul apelului acelei variabile. Afișarea rămâne în codul apelant.

[00:09:37](https://www.youtube.com/watch?v=VhA2OupAYRc&t=577s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int[] r = [6, 8, 5];
int sum;
sum = SumNumbers(r);
Console.WriteLine(sum);
```


## Urmărește apelul și rezultatul returnat

[00:09:44](https://www.youtube.com/watch?v=VhA2OupAYRc&t=584s) **Debuggerul** permite înregistrării să urmărească modul în care un apel transferă valori. **Apelantul** este codul care invocă `SumNumbers`; **funcția apelată** este chiar acea funcție. Secvența de apel este tabloul anterior cu trei valori, declarația rezultatului și atribuirea.

[00:09:44](https://www.youtube.com/watch?v=VhA2OupAYRc&t=584s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int[] r = [6, 8, 5]; // caller's r
int sum;
sum = SumNumbers(r); // copied reference goes to the parameter named r
Console.WriteLine(sum); // 19 after the call completes
```

Urmărește-i acțiunile în ordine:

1. [00:09:57](https://www.youtube.com/watch?v=VhA2OupAYRc&t=597s) Creează un tablou cu trei celule conținând 6, 8 și 5. Sunt trei celule, nu nouă și nici câte o celulă pentru fiecare unitate a fiecărui număr.
2. [00:10:09](https://www.youtube.com/watch?v=VhA2OupAYRc&t=609s) Memorează referința noului obiect în `r` al apelantului. Referința este ceea ce păstrează variabila; valorile elementelor rămân în obiectul de tablou.
3. [00:10:17](https://www.youtube.com/watch?v=VhA2OupAYRc&t=617s) Declară variabila numerică de rezultat `sum`. Tipul ei este `int`; rămâne neatribuită până când apelul returnează. `r` al apelantului are tipul `int[]`, deci poate păstra o referință către un tablou de întregi. O variabilă `int` nu poate păstra acea referință de tablou.
4. [00:10:32](https://www.youtube.com/watch?v=VhA2OupAYRc&t=632s) Distinge această variabilă numerică de **`var`**, sintaxa pentru deducerea unui tip dintr-un inițializator. Un `var sum;` de sine stător este invalid. Mai târziu, `var sum = SumNumbers(r);` ar deduce `int`; `var` nu este limitat la numere.
5. [00:10:41](https://www.youtube.com/watch?v=VhA2OupAYRc&t=641s) Creează o **variabilă de parametru** nouă pentru apel. Fiecare parametru declarat în antet primește valoarea argumentului său corespunzător.
6. [00:10:52](https://www.youtube.com/watch?v=VhA2OupAYRc&t=652s) Parametrul numit `r` este altă stocare locală decât variabila apelantului, numită tot `r`. Numele identice nu fuzionează variabilele.
7. [00:11:00](https://www.youtube.com/watch?v=VhA2OupAYRc&t=660s) Evaluează expresia argumentului citind `r` al apelantului. În desen, o adresă ilustrativă precum 8000 desemnează acea referință de tablou.
8. [00:11:18](https://www.youtube.com/watch?v=VhA2OupAYRc&t=678s) Copiază acea valoare în primul parametru, pentru că acesta corespunde primei **poziții de argument**. Aceasta este **transmitere prin valoare**: valoarea copiată este referința tabloului, nu primul element, iar elementele nu sunt copiate într-un al doilea tablou.
9. [00:11:37](https://www.youtube.com/watch?v=VhA2OupAYRc&t=697s) Execută corpul funcției apelate. Indicele și acumulatorul său sunt **variabile locale** suplimentare, stocare de lucru privată acelei invocări.
10. [00:11:48](https://www.youtube.com/watch?v=VhA2OupAYRc&t=708s) **Valoarea returnată**, rezultatul furnizat de `return`, devine valoarea expresiei de apel. Înregistrarea spune momentan 14, apoi corectează totalul la 19: `6 + 8 + 5 = 19`.
11. [00:11:57](https://www.youtube.com/watch?v=VhA2OupAYRc&t=717s) Variabila de parametru nu mai este disponibilă când funcția se încheie. [00:12:06](https://www.youtube.com/watch?v=VhA2OupAYRc&t=726s) Și celelalte variabile temporare de lucru își încheie rolul. Aceasta este **durata de viață**, cât timp aceste variabile locale aparțin invocării, nu ștergerea obiectului de tablou partajat.
12. [00:12:17](https://www.youtube.com/watch?v=VhA2OupAYRc&t=737s) Atribuie 19 lui `sum` al apelantului. Apelantul păstrează valoarea returnată, deși variabilele locale ale funcției apelate s-au încheiat.

[00:12:30](https://www.youtube.com/watch?v=VhA2OupAYRc&t=750s) Rezultatul demonstrat este 19. Cele două variabile de referință identificau același tablou de trei celule, iar acumulatorul a furnizat un răspuns numeric separat.

[00:11:57](https://www.youtube.com/watch?v=VhA2OupAYRc&t=717s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
// Conceptual evaluation, not replacement of the source text:
sum = SumNumbers(r);
sum = 19; // the call's returned value is assigned here
```

<details>
<summary>De ce nu copiază 6 în parametru trecerea tabloului?</summary>

Primul argument este expresia r, a cărei valoare este o referință de tablou. Poziția parametrului corespunde poziției argumentului, nu poziției elementului din tablou. Parametrul primește o copie a referinței către întregul tablou.

</details>

## Combină declararea și inițializarea

[00:12:37](https://www.youtube.com/watch?v=VhA2OupAYRc&t=757s) **Inițializarea** îi dă unei variabile prima valoare. Declararea și atribuirea care o urmează pot fi combinate într-o singură instrucțiune obișnuită. Aceasta schimbă scrierea, păstrând același apel și același rezultat.

[00:12:37](https://www.youtube.com/watch?v=VhA2OupAYRc&t=757s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
// Before:
int sum;
sum = SumNumbers(r);

// After:
int sum = SumNumbers(r);
// Also possible: var sum = SumNumbers(r); // inferred type is int
```

Apelul este tot evaluat mai întâi pentru a obține o valoare, iar acea valoare îl inițializează pe `sum`. Deducerea tipului nu amână inițializarea.

## Înlocuiește goto-ul înapoi cu while

[00:12:49](https://www.youtube.com/watch?v=VhA2OupAYRc&t=769s) O **buclă** repetă un corp de instrucțiuni; o **iterație** este o repetare. Tiparul cu goto înapoi poate fi rescris ca **`while (true)`**, o buclă a cărei condiție este întotdeauna adevărată.

Pentru a face rescrierea:

1. Pune `while (true)` înaintea instrucțiunilor repetate.
2. Plasează codul repetat al etichetei de destinație între acoladele sale.
3. Elimină eticheta și goto-ul care sărea înapoi la ea.
4. Păstrează verificarea de oprire înainte de orice acces la tablou.

[00:12:49](https://www.youtube.com/watch?v=VhA2OupAYRc&t=769s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
static int SumNumbers(int[] r)
{
    int s = 0;
    int i = 0;
    while (true)
    {
        if (i >= r.Length)
            return s;
        s += r[i];
        i += 1;
    }
}
```

[00:13:11](https://www.youtube.com/watch?v=VhA2OupAYRc&t=791s) **Corpul** este blocul din interiorul buclei. Dacă controlul ajunge la acolada sa de închidere, revine la capul buclei, în loc să treacă la instrucțiunea următoare de după buclă.

[00:13:34](https://www.youtube.com/watch?v=VhA2OupAYRc&t=814s) O **buclă infinită** nu are niciun punct de oprire automat atunci când condiția sa este întotdeauna adevărată. Fără o ieșire, corpul se repetă la nesfârșit, iar instrucțiunile de după buclă nu sunt atinse niciodată. Această versiune de însumare are un `return` explicit, deci este finită pentru tabloul demonstrat.

[00:13:52](https://www.youtube.com/watch?v=VhA2OupAYRc&t=832s) Înregistrarea recomandă această buclă structurată în locul repetării cu goto și etichetă. Ea face vizibilă regiunea repetată într-un singur bloc.

[00:14:01](https://www.youtube.com/watch?v=VhA2OupAYRc&t=841s) **Codul inaccesibil** nu are nicio cale de execuție. IDE-ul colorează în gri un goto plasat după un return, pentru că returnul a încheiat deja funcția.

[00:14:01](https://www.youtube.com/watch?v=VhA2OupAYRc&t=841s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
if (i >= r.Length)
{
    return s;
    // goto AddElement; // unreachable if written after this return
}
```

[00:14:18](https://www.youtube.com/watch?v=VhA2OupAYRc&t=858s) **`return`** încheie întreaga invocare a funcției și transferă controlul înapoi la locul apelului. Este diferit de încheierea unui bloc obișnuit.

## Alege limita corectă a buclei

[00:14:24](https://www.youtube.com/watch?v=VhA2OupAYRc&t=864s) O **condiție** evaluează la o valoare **booleană**, adevărat sau fals. **Negația** inversează acea valoare. Schimbarea doar a condiției unei bucle nu păstrează comportamentul; testul de oprire și testul de continuare trebuie să fie inversele logice, cu fluxul de control corespunzător.

[00:14:27](https://www.youtube.com/watch?v=VhA2OupAYRc&t=867s) Testul corect de continuare este `i < length`; inversul său, testul de oprire, este `i >= length`. Echivalent, pentru un indice întreg, oprirea la `i > length - 1` înseamnă același lucru. Nu este echivalent cu `i >= length - 1`, care ar opri înainte de procesarea ultimei celule.

[00:14:54](https://www.youtube.com/watch?v=VhA2OupAYRc&t=894s) **Limitele** identifică pozițiile legale. Un tablou de lungime trei are indicii 0, 1 și 2. Indicele 3 este deja dincolo de ultimul său element. Ultimul indice valid este `length - 1`.

[00:15:10](https://www.youtube.com/watch?v=VhA2OupAYRc&t=910s) Comparația strictă `i > length - 1` este echivalentă cu `i >= length` pentru indici întregi, deoarece nu există niciun indice fracționar între acele limite.

[00:15:10](https://www.youtube.com/watch?v=VhA2OupAYRc&t=910s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```text
// For an integer index and length = 3:
last valid index: 2
continue: i < 3       // includes i == 2
stop:     i >= 3      // excludes i == 3
same stop: i > 3 - 1  // i > 2
wrong stop: i >= 3 - 1 // would wrongly stop at i == 2
```

[00:15:25](https://www.youtube.com/watch?v=VhA2OupAYRc&t=925s) „Mai mic decât lungimea” exclude poziția de după capăt, `length`; include ultimul element, la `length - 1`. Dacă ar fi citit ca excludând ultimul element, ar introduce o **eroare off-by-one**, adică o graniță deplasată cu o poziție.

[00:15:51](https://www.youtube.com/watch?v=VhA2OupAYRc&t=951s) Separă oprirea buclei de returnarea rezultatului. **`break`** părăsește bucla; returnul poate urma după el. Mutarea testului pozitiv de continuare în antetul while elimină perechea if-plus-break.

[00:15:51](https://www.youtube.com/watch?v=VhA2OupAYRc&t=951s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
// Intermediate shape:
int s = 0;
int i = 0;
while (true)
{
    if (i >= r.Length)
        break;
    s += r[i];
    i += 1;
}
// s now holds the answer; return it from the enclosing SumNumbers body.

// Clearer shape, replacing that loop:
s = 0;
i = 0;
while (i < r.Length)
{
    s += r[i];
    i += 1;
}
return s;
```

[00:16:00](https://www.youtube.com/watch?v=VhA2OupAYRc&t=960s) În forma cu buclă infinită, verificarea de oprire trebuie să ruleze înaintea lucrului indexat. În forma cu while condițional, aceeași decizie se ia la capul buclei. O condiție de oprire scrisă doar ca expresie nu poate ieși dintr-o buclă; are nevoie de acțiunea de ieșire sau de garda de continuare inversă.

[00:16:22](https://www.youtube.com/watch?v=VhA2OupAYRc&t=982s) O **gardă** este verificarea care permite rularea corpului. Ea este evaluată înaintea primei iterații și din nou de fiecare dată când controlul revine la capul buclei. Prin urmare, un tablou gol nu efectuează nicio citire de elemente și lasă suma zero. Aceasta rezultă din codul reconstruit; testul numeric demonstrat în înregistrare folosește trei elemente.

<details>
<summary>Ce s-ar întâmpla cu i &lt;= r.Length în loc de i &lt; r.Length?</summary>

Garda ar permite indicele Length, adică unul dincolo de capăt. Corpul ar încerca apoi să citească o celulă inexistentă. Cu Length = 3, indicii valizi sunt doar 0, 1 și 2.

</details>

## Adună starea buclei în antetul for

[00:16:59](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1019s) Indicele este **stare de lucru temporară**, date care se modifică doar pentru a parcurge acest tablou. El furnizează garda și poziția următorului element. [00:17:00](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1020s) Declararea, testul și actualizarea sa se află în prezent în locuri diferite în versiunea cu while.

[00:17:00](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1020s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int i = 0;                 // initial state
while (i < r.Length)        // continuation guard
{
    s += r[i];             // useful body work
    i += 1;                // next-state update
}
```

[00:17:13](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1033s) Înregistrarea identifică patru piese structurale: starea inițială, o condiție de încheiere sau de continuare, lucrul util care modifică datele acumulate și tranziția către starea iterației următoare. Aici ele sunt indicele zero, testul limitelor, adunarea la sumă și incrementarea indicelui.

[00:17:24](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1044s) **Starea** nu trebuie să însemne un singur indice. Poate include mai multe valori, precum doi indici ale căror actualizări depind de valorile lor curente. Exemplul de însumare rămâne cu unul.

[00:17:35](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1055s) O **buclă `for`** adună comenzile de parcurgere în antetul său: stare inițială A, gardă de continuare B și actualizare către starea următoare C. Acoladele conțin lucrul util, adică corpul. B decide dacă poate începe altă iterație; C rulează după fiecare corp completat, nu doar o dată la sfârșitul întregii bucle.

[00:17:57](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1077s) Înregistrarea preferă această formă pentru acest tipar, deoarece inițializarea, garda și actualizarea sunt alături, ceea ce face parcurgerea mai ușor de citit.

[00:18:21](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1101s) Conversia este mecanică:

1. Mută `int i = 0` în prima poziție a antetului.
2. Pune `i < r.Length` în a doua poziție.
3. Mută `i += 1` în a treia poziție.
4. Elimină vechea declarație separată și incrementarea.
5. Păstrează `s += r[i]` în corp.

[00:18:21](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1101s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int s = 0;
for (int i = 0; i < r.Length; i += 1)
{
    s += r[i];
}
return s;
```

Cele două puncte și virgule din antet separă cele trei părți ale sale. Execuția este: inițializare o singură dată, gardă, corp, actualizare, apoi din nou gardă. Conversia păstrează suma celor trei elemente, 19.

## Respectă și domeniul variabilelor, nu doar comportamentul

[00:18:46](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1126s) Conversia buclei păstrează aritmetica, dar există o diferență de **domeniu**: domeniul este locul în care un nume declarat poate fi folosit. Un indice declarat în antetul for aparține acelui for.

[00:19:05](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1145s) El este disponibil în condiția și actualizarea din antet și în corp. Nu este disponibil după buclă. Două bucle for separate pot fiecare să-și declare propriul `i` fără ca acele declarații să împartă un domeniu înconjurător.

[00:19:05](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1145s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
for (int i = 0; i < r.Length; i += 1)
{
    Console.WriteLine(i); // valid: inside this loop's scope
}
// Console.WriteLine(i); // compile error if uncommented: i is not in scope

for (int i = 0; i < r.Length; i += 1)
{
    // a different loop-local i
}
```

[00:19:16](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1156s) Încercarea de a afișa după buclă produce eroarea de compilator din înregistrare: acel nume nu este disponibil. **Compilatorul** verifică unde sunt declarate numele, nu doar dacă o buclă s-a terminat în timpul execuției.

[00:19:17](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1157s) Corecția adusă afirmației „100% echivalent” contează. În forma simplă cu while, declararea lui `i` înaintea buclei îl lasă în domeniul înconjurător, unde poate fi încă citit după aceea. Așadar are același comportament de parcurgere, dar o altă limită de vizibilitate a numelui.

[00:19:17](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1157s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int i = 0;
while (i < r.Length)
{
    s += r[i];
    i += 1;
}
Console.WriteLine(i); // valid here; equals r.Length after this traversal
```

[00:19:35](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1175s) Un **bloc**, instrucțiuni cuprinse între acolade, poate restrânge acea declarație. Înfășoară atât inițializarea, cât și bucla while într-un bloc propriu, pentru a corespunde vizibilității indicelui din for.

[00:19:35](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1175s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
{
    int i = 0;
    while (i < r.Length)
    {
        s += r[i];
        i += 1;
    }
}
// Console.WriteLine(i); // invalid here: outside i's block
```

[00:19:42](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1182s) Variabilele declarate într-un bloc nu pot fi numite din exteriorul lui. Pentru acest exemplu, bucla while împachetată corespunde acum atât ordinii de parcurgere, cât și domeniului relevant al indicelui din forma for. Suma poate fi în continuare returnată ulterior, pentru că `s` este declarată în afara acelui bloc interior. Echivalența descrisă aici privește fluxul de control și vizibilitatea codului, nu afirmația că compilatorul trebuie să emită o anumită secvență de cod mașină.

<details>
<summary>De ce afișarea după buclă compilează pentru while-ul neîmpachetat, dar eșuează pentru for?</summary>

Indicele while-ului neîmpachetat a fost declarat în blocul înconjurător. Indicele for-ului a fost declarat în propriul antet și rămâne local acelei bucle. Împachetarea declarației și a buclei while într-un bloc nou îi oferă indicelui o limită comparabilă.

</details>

## Vizitează valorile direct cu foreach

[00:20:00](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1200s) Pasul următor este o parcurgere de nivel mai înalt: folosește valoarea fiecărui element fără a scrie explicit un indice, un indice inițial, o limită finală sau o incrementare. **`foreach`** repetă lucrul pentru elemente succesive.

[00:20:23](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1223s) Pentru un tablou, compilatorul poate genera mecanismele necesare vizitării elementelor sale, inclusiv parcurgerea bazată pe indici. Această explicație se referă la exemplul cu tablou; alte tipuri de colecții pot fi parcurse diferit.

[00:20:34](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1234s) Declară o variabilă pentru **elementul curent**, valoarea vizitată în această iterație, în locul unui indice. Apoi scrie **`in`**, cuvântul-cheie care leagă acea variabilă de tabloul sursă.

[00:20:34](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1234s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
int s = 0;
foreach (int element in r)
{
    s += element;
}
return s;
```

[00:20:55](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1255s) În corp, `element` conține deja valoarea întreagă. Nu este nevoie să citim manual `r[i]`. Suma primește tot 6, apoi 8, apoi 5.

[00:21:01](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1261s) O **variabilă locală** numește date disponibile în această parte a funcției. Conceptual, forma indexată creează mai întâi o variabilă locală care păstrează valoarea curentă, apoi o folosește. Declarația foreach furnizează direct acea variabilă locală.

[00:21:01](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1261s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
// Indexed body:
for (int i = 0; i < r.Length; i += 1)
{
    int element = r[i];
    s += element;
}
// Foreach supplies the current element variable in its header instead.
```

[00:21:13](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1273s) **`var`** cere compilatorului să deducă tipul variabilei din valorile vizitate. Pentru acest `int[]`, tipul dedus al elementului este `int`. Elimină declarația separată a elementului și folosește direct variabila din antet:

[00:21:13](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1273s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
static int SumNumbers(int[] r)
{
    int s = 0;
    foreach (var element in r)
    {
        s += element;
    }
    return s;
}

int[] r = [6, 8, 5];
int sum = SumNumbers(r);
Console.WriteLine(sum); // 19
```

[00:21:21](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1281s) Înregistrarea rulează forma scurtată după această schimbare. Are același comportament de însumare ca versiunile anterioare; transcriptul nu descrie o nouă ieșire numerică separată în acest moment. Rezultatul 19 rezultă din aceleași trei valori de intrare și adunări.

Folosește foreach atunci când corpul are nevoie de valori, și formele indexate atunci când pozițiile explicite ale lecției sunt utile. Progresia din înregistrare elimină contabilitatea fără să schimbe interfața funcției.

## Citește un if și recunoaște codul inaccesibil

[00:21:27](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1287s) Înregistrarea revine pentru a explica **instrucțiunea `if`**, o instrucțiune condițională. Ea evaluează **condiția booleană** din parantezele sale rotunde. În garda tabloului, întrebarea este dacă indicele curent este mai mare sau egal cu lungimea.

[00:21:27](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1287s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
if (i >= r.Length)
{
    return s; // finish before attempting an out-of-bounds read
}
s += r[i];    // reached only when the above test is false
```

[00:21:39](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1299s) **Blocul** asociat rulează doar când condiția este adevărată. Când este falsă, blocul întreg este sărit. Plasarea acțiunii de oprire înaintea accesului indexat protejează împotriva depășirii capătului tabloului.

[00:21:51](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1311s) Un bloc if obișnuit, completat, este urmat de instrucțiunea următoare în ordine. Un if nu încheie singur o funcție:

[00:21:51](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1311s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
if (i >= r.Length)
{
    Console.WriteLine("End reached");
}
Console.WriteLine("Next statement"); // follows either branch if execution continues
```

[00:22:00](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1320s) **`return`** este acțiunea care încheie funcția. În corpul protejat anterior, dacă-i condiția este îndeplinită, returnul împiedică executarea instrucțiunilor ulterioare pe acea cale. Orice instrucțiune plasată imediat după un return în același bloc este **inaccesibilă**, adică nu poate fi executată de nicio cale de flux de control.

[00:22:00](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1320s) Reconstrucție simplificată a videoclipului, susținută de [transcriptul imutabil][recorded-transcript], nu un program exact din commit:

```csharp
if (i >= r.Length)
{
    return s;
    // Console.WriteLine("After return"); // unreachable inside this block
}
// Reaching here means the if condition was false.
```

Această distincție explică atât garda tabloului, cât și instrucțiunea colorată în gri demonstrată anterior: ramificarea condițională decide în ce bloc intrăm, iar returnul decide când se încheie invocarea.

## Domeniul lecției și studierea ei

[00:00:00](https://www.youtube.com/watch?v=VhA2OupAYRc&t=0s) Lecția modifică un program de bază existent pentru a implementa algoritmul anterior de însumare. Nu creează un proiect nou, nu instalează unelte și nu dezvoltă sarcina anterioară, mai amplă, de procesare a fișierelor.

[00:08:17](https://www.youtube.com/watch?v=VhA2OupAYRc&t=497s) Semnificația detaliată a lui static este amânată. [00:09:23](https://www.youtube.com/watch?v=VhA2OupAYRc&t=563s) Goto este o punte temporară de la algoritmul elementar, cu intenția declarată de a fi înlocuit curând. Construcția propriu-zisă acoperă tablourile, citirile și scrierile indexate, intrările și valorile returnate ale funcției, precum și conversiile prin while, for și foreach.

Desenele cu adrese explică referințele, deplasările elementelor și variabilele temporare. Ele nu specifică adrese exacte de octeți sau dispunerea fizică a fiecărei variabile locale. Redimensionarea unui tablou necesită un alt obiect; observația despre view-ul de felie nu dezvoltă o procedură de feliere.

Intrarea demonstrată este un tablou de întregi care conține 6, 8 și 5, cu o referință de obiect validă. Comportamentul pentru un tablou gol poate fi dedus din garda testată în prealabil, dar nu este raportată nicio rulare cu tablou gol. Null înseamnă absența unei referințe de obiect; depășirea întregilor înseamnă un rezultat aritmetic în afara intervalului tipului întreg. Tratarea intrării null și politica de depășire nu sunt dezvoltate în această înregistrare.

Pentru practică opțională dincolo de videoclip, folosește [laboratorul de implementare a algoritmului](../../../labs/1_basic/12_algorithms_code.md). Acesta cere funcții doar cu algoritm, cu afișare pe consolă în codul principal, tablouri create de apelant și versiuni care folosesc while și for. Instrucțiunile sale despre tablouri de ieșire se aplică algoritmilor care au nevoie de un tablou de ieșire; suma din această lecție returnează în schimb un singur număr. Aserțiunile opționale și excepțiile din laborator aparțin materialului ulterior, nu sunt premise pentru urmărirea acestui videoclip.

[Laboratorul de funcții](../../../labs/1_basic/05_functions.md) și [laboratorul de tehnici de bază](../../../labs/1_basic/10_basic.md) oferă întrebări suplimentare despre variabilele de parametru separate, valorile argumentelor copiate și stocarea unui rezultat returnat. Aceste documente oferă practică, nu copii exacte ale sursei înregistrate.

## Istoricul codului înregistrat

Această pistă compactă urmărește stările predate în videoclip, în ordine cronologică. Niciun commit inspectat nu conține textul exact al programelor lor; [transcriptul imutabil][recorded-transcript] susține explicațiile înregistrate, nu o pretindere de recuperare exactă a sursei. Rezumatul stărilor corespunzătoare este:

```text
existing sum steps -> array input and int result
new int[count] -> indexed writes -> initializer -> inferred syntax
goto body -> captured call result 19
separate result declaration -> initialized declaration
backward goto -> while(true) -> while(i < Length)
while traversal -> for header -> scope-matched while block
indexed element reads -> foreach element values
```

- [00:00:00](https://www.youtube.com/watch?v=VhA2OupAYRc&t=0s) — pornește de la procedura de însumare de bază existentă și îi planifică intrarea de tip tablou și ieșirea numerică.
- [00:03:01](https://www.youtube.com/watch?v=VhA2OupAYRc&t=181s) — creează un tablou de întregi de lungime specificată; inspectează elementele sale inițial egale cu zero.
- [00:04:01](https://www.youtube.com/watch?v=VhA2OupAYRc&t=241s) — scrie și citește celule după indice numerotat de la zero; scrierile ulterioare furnizează 6, 8 și 5.
- [00:06:35](https://www.youtube.com/watch?v=VhA2OupAYRc&t=395s) — înlocuiește scrierile individuale cu un inițializator de trei valori.
- [00:07:07](https://www.youtube.com/watch?v=VhA2OupAYRc&t=427s) — deduce lungimea din inițializator.
- [00:07:35](https://www.youtube.com/watch?v=VhA2OupAYRc&t=455s) — omite tipul de element redundant la crearea tabloului.
- [00:07:54](https://www.youtube.com/watch?v=VhA2OupAYRc&t=474s) — arată forma cu paranteze pătrate a expresiei de colecție.
- [00:08:06](https://www.youtube.com/watch?v=VhA2OupAYRc&t=486s) — declară rezultatul întreg și parametrul de tip tablou ai funcției locale de însumare.
- [00:09:23](https://www.youtube.com/watch?v=VhA2OupAYRc&t=563s) — mută algoritmul în funcție folosind etichete și goto.
- [00:09:37](https://www.youtube.com/watch?v=VhA2OupAYRc&t=577s) — captează rezultatul apelului; următoarea urmărire și rulare stabilește 19.
- [00:12:37](https://www.youtube.com/watch?v=VhA2OupAYRc&t=757s) — combină declararea și inițializarea rezultatului.
- [00:12:49](https://www.youtube.com/watch?v=VhA2OupAYRc&t=769s) — înlocuiește repetiția cu salt înapoi cu while(true).
- [00:15:51](https://www.youtube.com/watch?v=VhA2OupAYRc&t=951s) — plasează condiția pozitivă de continuare în antetul while.
- [00:18:21](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1101s) — adună inițializarea, garda și actualizarea într-un antet for.
- [00:19:35](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1175s) — împachetează versiunea echivalentă cu while pentru a corespunde domeniului indicelui.
- [00:20:34](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1234s) — vizitează direct valorile elementelor cu foreach.
- [00:21:13](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1273s) — deduce tipul elementului din foreach și elimină declarația separată a valorii.
- [00:21:27](https://www.youtube.com/watch?v=VhA2OupAYRc&t=1287s) — revine la garda if și identifică codul care nu poate rula după return.