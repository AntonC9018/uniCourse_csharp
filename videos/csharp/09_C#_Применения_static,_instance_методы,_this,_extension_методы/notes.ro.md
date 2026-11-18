> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Organizarea unui joc cu metode statice și de instanță

[00:00:04](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=4s) Această lecție continuă programul de luptă din videoclipul anterior. Primul obiectiv este să împărțim fișierul sursă unic în mai multe fișiere; obiectivul mai larg este să clarificăm dacă datele și comportamentul aparțin unui tip, unui singur obiect sau unui anumit joc. Urmăriți schimbările în ordine: mutăm declarații, introducem un container utilitar static, încercăm și refuzăm un contor global, adăugăm fabrici, transformăm operații în metode de instanță, mutăm o operație într-o metodă de extensie și, în final, explicăm codul de nivel superior și funcțiile locale.

Termenii necesari pentru acest parcurs sunt:

- **Fișier sursă, proiect, compilator și diagnostic:** un fișier sursă conține text C#; un proiect grupează fișierele compilate împreună; compilatorul traduce acel text și îi verifică regulile; un diagnostic raportează o eroare sau un avertisment.
- **Instrucțiuni de nivel superior și punct de intrare:** instrucțiunile de nivel superior sunt instrucțiuni executabile scrise în afara unei metode explicite. Punctul de intrare este locul unde începe execuția programului.
- **Tip, clasă, structură, obiect, instanță și referință:** un tip descrie valori și operațiile lor. O clasă descrie obiecte de tip referință; o structură descrie instanțe de tip valoare. Un obiect este o instanță a unui tip, iar o referință identifică un anumit obiect.
- **Câmp, metodă și membru:** un câmp stochează date; o metodă este o funcție declarată într-un tip; membru este denumirea generală folosită aici pentru oricare dintre ele. „Funcție membru” înseamnă metodă.
- **Clasă statică, membru static și membru de instanță:** o clasă statică este un container la nivel de tip care nu poate fi instanțiat. Un membru static aparține tipului; un membru de instanță aparține unei anumite instanțe sau acționează asupra ei.
- **Calificare, punct și `public`:** calificarea numește proprietarul unui membru; punctul din `Owner.Member` selectează acel membru; `public` permite altui cod să îl acceseze.
- **Parametru, argument, semnătură, receptor și `this`:** un parametru declară o intrare a funcției; un argument o furnizează la apel; o semnătură identifică metoda și tipurile de parametri declarate. Receptorul este obiectul pe care este invocată o metodă de instanță; `this` se referă la acel obiect curent. „Primul parametru ascuns” este modelul lecției pentru receptor, nu un parametru suplimentar scris în C#.
- **Stare, statistici, stare globală, mutabil și imutabil:** starea sunt datele curente ale jocului; statisticile sunt valorile de viață și atac. Starea globală este comună dincolo de un anumit obiect de joc. Datele mutabile se pot schimba; datele imutabile nu se mai schimbă după inițializare.
- **Fabrică, inițializare, inițializator de obiect și `new`:** o fabrică creează și returnează un obiect gata de folosire. Inițializarea furnizează valorile de început; un inițializator de obiect furnizează valorile câmpurilor în timpul construcției; `new` creează o instanță.
- **Contor, incrementare, întreg și constantă:** un contor înregistrează un număr de evenimente; incrementarea adaugă unu; `int` stochează un întreg. O constantă este o valoare fixă, nu o stare de joc care se schimbă.
- **Boolean, expresie, evaluare, atribuire, condiție și `return`:** `bool` reține `true` sau `false`; o expresie calculează o valoare; evaluarea calculează acel rezultat; atribuirea îl stochează; o condiție verifică o valoare booleană; `return` trimite rezultatul înapoi și iese din funcția curentă. `void` înseamnă că nu se returnează niciun rezultat.
- **Listă și buclă de joc:** `List<Enemy>` este o colecție extensibilă de obiecte inamic; bucla de joc repetă lupta până când este atinsă o condiție de oprire. Acestea sunt părți existente ale programului, nu algoritmi de colecție noi predați aici.
- **Serializare:** conversia stării într-o reprezentare care poate fi stocată sau transmisă și apoi reconstruită.
- **Metodă de extensie:** o metodă statică al cărei prim parametru este marcat cu `this`, ceea ce permite un apel în stil de obiect, păstrând declarația într-o clasă statică separată.
- **Domeniu, funcție locală, captură și ascundere:** domeniul spune unde poate fi folosit un nume; o funcție locală este declarată în interiorul altei funcții; captura îi permite să folosească datele locale din jur; ascunderea este o coliziune de nume în care o declarație o maschează pe alta.
- **Argumente de linie de comandă, pattern Builder și încapsulare:** argumentele de linie de comandă sunt intrări transmise la pornirea unui program; un builder asamblează sau configurează ceva prin operații succesive; încapsularea controlează modul în care sunt accesate datele unui obiect. Aceste subiecte sunt numite pentru a marca material ulterior din curs.

Laboratorul [clasă statică](../../../labs/1_basic/15_static_class.md) cere să mutați funcțiile existente într-o clasă `Functions` într-un fișier nou. Laboratorul [metodă de instanță](../../../labs/1_basic/16_method.md) aplică conversia la receptor unei abstracții de bibliotecă. Sunt exerciții înrudite, nu copii angajate ale programului de luptă.

Nu a fost disponibil niciun fișier sursă de luptă angajat exact în checkoutul verificat. Toate exemplele de mai jos sunt reconstrucții simplificate ale stării legate din videoclip, nu un program angajat exact. Transcrierea imutabilă este o dovadă de sprijin pentru înregistrare, nu un commit de sursă de luptă.

## Împărțirea programului în fișiere sursă

[00:00:04](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=4s) Inițial, jocul are un singur **fișier sursă**, `Program.cs`. [00:00:09](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=9s) În acest **proiect**, fișierele sursă puse lângă fișierul de proiect sau în foldere imbricate sunt preluate automat. Aceasta descrie comportamentul implicit de includere al proiectului demonstrat, nu o garanție pentru orice proiect personalizat.

[00:00:33](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=33s) **Instrucțiunile de nivel superior** sunt instrucțiuni scrise direct în fișierul sursă principal, în afara unei metode declarate explicit. Ele se execută din **punctul de intrare** generat la pornirea programului. Numele de fișier `Program.cs` este convențional; important este codul său de nivel superior.

[Transcrierea imutabilă corespunzătoare](https://github.com/AntonC9018/uniCourse_csharp/blob/4814706ae2fafbc797829239a33afc2eeefa29e8/videos/csharp/09_C%23_Применения_static,_instance_методы,_this,_extension_методы/internal/transcript.md) consemnează construcția inițială. Această stare de început scurtată combină o instrucțiune executabilă cu o declarație de clasă:

```csharp
// Program.cs — before splitting
Console.WriteLine("Game starts");

class Stats
{
    public int Health;
    public int Attack;
}
```

[00:00:45](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=45s) Adăugarea altei instrucțiuni de nivel superior într-un al doilea fișier produce o eroare de compilare: într-un proiect, doar un fișier poate conține instrucțiuni de nivel superior. Un **diagnostic** este raportul compilatorului despre această structură invalidă; un al doilea fișier este util, dar nu poate furniza încă un punct de intrare de nivel superior.

[00:01:13](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=73s) În schimb, mutați **declarările de tipuri**, cum ar fi clasele:

1. [00:01:20](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=80s) Tăiați declarațiile de clase din `Program.cs`.
2. Lipiți-le în noul `Logic.cs` din același proiect.
3. [00:01:38](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=98s) Continuați să folosiți acele tipuri din fișierul principal. Saltul la definiție duce acum la declarația din celălalt fișier.

O **clasă** definește câmpurile și operațiile obiectelor sale; un **câmp** stochează date. Starea video după împărțire poate fi reprezentată astfel:

```csharp
// Program.cs
Stats player = new Stats { Health = 10, Attack = 1 };

// Logic.cs
class Stats
{
    public int Health;
    public int Attack;
}
```

[00:01:20](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=80s) Nu există aici nicio legătură de tip fișier-la-fișier de adăugat: tipurile mutate rămân disponibile după aceleași reguli de denumire și accesibilitate. „Totul este disponibil” din înregistrare se referă la aceste declarații. O variabilă locală de nivel superior precum `player` nu devine o variabilă globală accesibilă din metode arbitrare din alt fișier.

## Mutarea funcțiilor într-o clasă statică

[00:01:56](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=116s) O funcție nu poate fi mutată pur și simplu într-un alt fișier obișnuit ca funcție liberă în stil C/C++. Funcțiile scrise alături de instrucțiunile de nivel superior sunt locale corpului punctului de intrare generat, nu funcții declarate global.

[00:02:10](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=130s) Numele au nevoie de un container. [00:02:26](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=146s) Declarațiile luate în calcul aici sunt **tipuri**, în special clase și structuri; aceasta nu este o catalogare exhaustivă a tuturor declarațiilor din C#. [00:02:36](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=156s) Puneți funcțiile mutate în interiorul unei clase. [00:02:38](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=158s) O **clasă statică** oferă un container convenabil pentru operații care nu au nevoie de un obiect container.

Urmați această reconstrucție a schimbării:

1. Mutați funcția din fișierul de nivel superior în `Logic`.
2. Marcați clasa și metodele sale cu `static`.
3. [00:02:46](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=166s) Schimbați apelurile din exterior astfel încât să includă numele clasei.
4. [00:03:01](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=181s) Folosiți punctul pentru a selecta metoda din acea clasă.

```csharp
// Program.cs — changed call
Logic.TakeDamage(player, 2);

// Logic.cs — moved operation
static class Logic
{
    public static void TakeDamage(Stats target, int amount)
    {
        target.Health -= amount;
    }
}
```

**Calificarea** numește proprietarul: `Logic.TakeDamage` selectează metoda din tipul `Logic`. `public` face metoda accesibilă apelanților săi; `void` spune că nu returnează nicio valoare; `target` și `amount` sunt **parametri** declarați, iar `player` și `2` sunt **argumente** furnizate. Atribuirea cu scădere modifică viața cu valoarea furnizată.

[00:02:54](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=174s) Sensul mai detaliat al prefixului cu numele clasei este inițial amânat. În această etapă, folosiți forma apelului și observați că operația primește în continuare datele explicit.

[00:03:18](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=198s) Încercarea unui câmp obișnuit, non-static, în această clasă statică produce o eroare de compilare. Regula exactă este că ea nu poate conține câmpuri de **instanță**; câmpurile statice sunt permise, așa cum stabilește demonstrația următoare. [00:03:33](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=213s) Încercarea de a crea un obiect `Logic` eșuează și ea: o clasă statică nu poate fi instanțiată. Experimentele respinse corespunzătoare din starea video sunt:

```csharp
// Invalid experiments:
static class Logic
{
    public int Count; // instance field in a static class
}
// new Logic();        // a static class cannot be instantiated
```

Aceste restricții sunt documentate în [referința Microsoft despre clasele statice](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members).

[00:04:05](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=245s) O funcție declarată într-o clasă se numește **metodă**; acestea sunt metode statice. [00:04:14](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=254s) Sensul mai profund al lui `static` este amânat pentru scurt timp, apoi dezvoltat prin câmpuri și obiecte.

## Denumirea membrilor unei clase

[00:04:20](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=260s) O **metodă statică** aparține unui tip. O metodă fără `static` este o **metodă de instanță**, asociată obiectului pe care este apelată. Schița acestei stări video arată diferența la nivelul declarațiilor, înainte de a modifica implementarea damage-ului:

```csharp
class Stats
{
    public int Health;                   // instance field
    public static int SharedCount;       // static field
    public static void Utility() { }      // static method
    public void ActOnThisObject() { }     // instance method
}
```

[00:04:35](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=275s) **Membru** este cuvântul general folosit aici pentru un câmp sau o metodă. [00:04:48](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=288s) „Funcție membru” este alt nume pentru metodă. C# are și alte categorii de membri, dar câmpurile și metodele sunt cele pe care lecția le organizează.

## Încercarea unui contor static partajat

[00:05:01](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=301s) **Starea globală mutabilă** sunt date pe care operații fără legătură le pot citi și modifica. Lecția descurajează folosirea câmpurilor statice pentru date de joc care se schimbă, pentru că astfel dependențele devin greu de observat.

[00:05:12](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=312s) Un câmp non-static aparține unui obiect. [00:05:21](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=321s) O clasă statică poate totuși să conțină un **câmp static**: acesta aparține tipului, nu vreunui obiect. Aceasta corectează explicația abreviată anterioară, „nu poate avea câmpuri”.

[00:05:31](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=331s) Adăugați temporar un contor în clasa utilitară și incrementați-l când se adaugă un inamic. Un **contor** înregistrează câte evenimente au avut loc; `++` îl **incrementează** cu unu. Starea intermediară reconstruită este:

```csharp
static class Logic
{
    public static int AddedEnemies;

    public static void AddEnemy(GameState state, Enemy enemy)
    {
        state.Enemies.Add(enemy);
        AddedEnemies++;
    }
}
```

[00:05:43](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=343s) Contorul aparține lui `Logic` în ansamblu. [00:05:54](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=354s) În acest exemplu obișnuit, non-generic, există o singură valoare stocată partajată, nu câte una pentru fiecare joc sau inamic. Se comportă ca o variabilă globală pe tot parcursul rulării.

[00:06:05](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=365s) În interiorul clasei care îl declară, `AddedEnemies` este suficient pentru a numi câmpul. [00:06:22](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=382s) `Logic.AddedEnemies` se referă la același câmp. [00:06:32](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=392s) În afara acelei clase, calificați-l cu numele tipului, de exemplu când îl afișați din codul principal:

```csharp
// Inside Logic:
AddedEnemies++;
// Equivalent qualification, shown separately:
Logic.AddedEnemies++;

// Outside Logic:
Console.WriteLine(Logic.AddedEnemies);
```

Cele două incrementări de mai sus ilustrează scrieri alternative; dacă le scrieți pe amândouă, contorul se mărește de două ori.

[00:06:39](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=399s) Pentru că acest exemplu folosește un câmp public, alt cod din program îl poate accesa prin `Logic.AddedEnemies`. `static` nu face singur un câmp public. [00:06:48](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=408s) Această accesibilitate largă este problema de proiectare: orice funcție poate depinde în secret de contor sau îl poate modifica.

[00:07:05](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=425s) Este preferabil să faceți datele necesare explicite prin parametri. Tehnic, o funcție poate primi o valoare obținută dintr-un câmp static, dar accesul direct la starea globală nu-i obligă pe apelanți să dezvăluie acea dependență. Formularea „imposibil” din lecție este o avertizare despre dependențe ascunse, nu o interdicție a limbajului de a transmite date.

## Mutarea contorului în starea jocului

[00:07:14](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=434s) **Starea jocului** sunt datele care descriu o singură rulare a jocului. Clasa existentă `GameState` grupează aceste date, astfel încât o singură variabilă identifică contextul curent al jocului.

[00:07:30](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=450s) Această apartenență permite jocurilor separate să aibă date separate și face posibilă **serializarea** unei stări: stocarea sau transmiterea unei reprezentări și reconstruirea ei ulterioară. [00:07:44](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=464s) Obiectul face atât dependența, cât și locația datelor explicite. [00:07:52](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=472s) Restaurarea unui joc îi restaurează propriul obiect; câmpurile globale ar permite jocurilor altfel independente să partajeze accidental date, inclusiv exemplul cu meniul din explicație. Serializarea și rețeaua sunt aici motivări, nu funcționalități implementate.

[00:08:08](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=488s) Folosirile adecvate ale câmpurilor statice sunt lăsate pentru mai târziu. [00:08:15](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=495s) Recomandarea din această etapă este să rezervați valorile partajate pentru informații fixe, imutabile, stabilite la inițializare, niciodată pentru starea de joc care se schimbă. **Imutabil** înseamnă că valoarea nu mai este modificată ulterior; o **constantă** este o informație fixă. Aceasta este o recomandare de proiectare, nu o afirmație că C# interzice câmpurile statice mutabile sau că orice valoare fixă trebuie inițializată la pornirea procesului.

Înlocuiți contorul partajat cu date deținute de stare:

1. [00:08:35](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=515s) Adăugați un câmp întreg în `GameState` și schimbați operația de adăugare să incrementeze `state.AddedEnemies`.
2. [00:08:46](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=526s) Rulați jocul. Ieșirea finală înregistrată este **6**, ceea ce confirmă șase adăugări de inamici în acea rulare.
3. [00:08:55](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=535s) Observați că contorul fiecărui joc pornește de la valoarea implicită obișnuită a întregului, zero.
4. [00:09:04](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=544s) Păstrați modificarea pe același obiect de stare transmis operației. [00:09:06](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=546s) Incrementarea aparține acum instanței de joc.
5. [00:09:16](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=556s) Afișați contorul acelei stări după terminarea buclei de joc.

O reconstrucție scurtată a câmpului modificat, a scrierii și a citirii finale este:

```csharp
class GameState
{
    public List<Enemy> Enemies = new List<Enemy>();
    public int AddedEnemies; // initially 0 in each state object
}

static class Logic
{
    public static void AddEnemy(GameState state, Enemy enemy)
    {
        state.Enemies.Add(enemy);
        state.AddedEnemies++;
    }
}

// In Program.cs, after the existing combat loop:
Console.WriteLine(state.AddedEnemies); // recorded run: 6
```

O **listă** este colecția extensibilă de inamici deja folosită de joc; **bucla de joc** repetă lupta. Exemplul omite celelalte câmpuri de stare existente și corpul buclei. Valoarea raportată, șase, este un rezultat de test din video, nu un test executat asupra acestui fragment scurtat.

<details>
<summary>Dacă două jocuri adaugă fiecare trei inamici, ce ar trebui să arate fiecare contor?</summary>

Fiecare stare ar trebui să arate trei. Un contor static ar combina în schimb adăugările ambelor jocuri în aceeași valoare, șase.

</details>

## Folosirea metodelor statice ca fabrici

[00:09:30](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=570s) O **metodă statică** este atașată unui tip, deci o clasă utilitară statică este un adăpost potrivit pentru funcții care operează pe intrări fără comportament de instanță. [00:09:37](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=577s) Este legal și să puneți o metodă statică într-o clasă obișnuită. [00:09:49](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=589s) Orice fel de clasă poate apela o metodă statică publică din altă clasă.

[00:10:02](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=602s) Apelul din exterior rămâne `ClassName.MethodName(...)`: clasa deține metoda și nu este necesară nicio instanță a ei pentru a o invoca. Abordarea cu clasă statică este o preferință de organizare pentru cod utilitar, nu o restricție asupra apelanților care o pot folosi.

[00:10:16](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=616s) O operație statică deosebit de utilă este o **fabrică**: o funcție care creează și returnează un obiect inițializat. [00:10:28](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=628s) De exemplu, o fabrică pe `Stats` poate returna un obiect `Stats`. [00:10:33](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=633s) Ea poate preinițializa câmpurile, astfel încât fiecare apelant să nu repete atribuirile.

Această reconstrucție a stării video arată împreună motivația și fabrica extrasă:

```csharp
// Repeated construction before introducing the factory:
Stats ordinary = new Stats { Attack = 1, Health = 10 };

// Factory placed in the type it produces:
class Stats
{
    public int Attack;
    public int Health;

    public static Stats CreateDefault()
    {
        return new Stats { Attack = 1, Health = 10 };
    }
}

// Changed call:
Stats ordinary = Stats.CreateDefault();
```

Prima și ultima declarație sunt etape alternative, nu două declarații de lipit în același domeniu. `new` creează obiectul; **inițializatorul de obiect** furnizează valorile câmpurilor sale; `return` îi înapoiază acelă referință apelantului. **Inițializarea** stabilește starea de început. O fabrică este statică pentru că nu există un obiect țintă deja creat pentru acest apel.

[00:10:56](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=656s) Fabricile sunt adesea atașate tipului pe care îl produc. Dați-le nume care dezvăluie ce va fi creat; nume precum `CreateDefault` sau `CreateDefaultStats` fac pasul de creare lizibil.

[00:11:41](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=701s) Valorile implicite demonstrate sunt **atac 1 și viață 10**. Fără acele atribuiri, câmpurile întregi obișnuite ar avea valoarea implicită zero; o fabrică furnizează valori implicite utile pentru joc, nu face să apară câmpuri care altfel n-ar exista.

[00:12:00](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=720s) Adăugați o a doua fabrică pentru statistici mai puternice: **atac 5 și viață 10**. O continuare simplificată a acestei stări video este:

```csharp
// Inside Stats:
public static Stats CreateStrong()
{
    return new Stats { Attack = 5, Health = 10 };
}
```

[00:12:10](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=730s) Fabrica nu trebuie să trăiască în `Stats`; poate trăi altundeva în logica jocului, dacă numele ei identifică clar rezultatul. [00:12:17](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=737s) Apelați fabrica implicită pentru fiecare inamic obișnuit, ca toți să primească valorile intenționate. [00:12:40](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=760s) Înregistrarea experimentează cu un nume prefixat cu `Create`, precum `CreateDef`; redenumirea declarației și a apelurilor corespunzătoare păstrează comportamentul.

```csharp
Stats enemyStats = Stats.CreateDefault();
Stats strongStats = Stats.CreateStrong();
```

Fiecare apel creează propriul obiect de statistici. [00:12:55](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=775s) Această operație de creare și inițializare este patternul **funcție fabrică**. Lecția demonstrează patternul, nu o regulă conform căreia orice obiect trebuie construit printr-o fabrică.

## Separarea datelor de tip de datele obiectului

[00:13:05](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=785s) O clasă obișnuită precum `Enemy` poate conține și ea un câmp static. Citirea sau incrementarea lui din programul principal tot folosește numele tipului. [00:13:19](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=799s) Fiecare instanță de inamic partajează acea singură valoare; crearea mai multor inamici nu creează mai multe copii ale câmpului static.

Ideea intermediară din starea video poate fi simplificată astfel:

```csharp
class Enemy
{
    public static int Count;
}

// In the main code:
Enemy.Count++;
Console.WriteLine(Enemy.Count);
```

[00:13:26](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=806s) Atașați datele care se schimbă lucrului pe care îl descriu: unui joc, unei comenzi sau unui anumit inamic. Punerea unui contor în `Enemy` nu rezolvă problema apartenenței globale dacă câmpul rămâne static.

[00:13:45](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=825s) Eliminați `static` pentru a face un câmp deținut de instanță. O **referință** identifică un obiect, iar fiecare **instanță** are propria copie a acelui câmp:

```csharp
// Alternative declaration for the ownership demonstration:
class Enemy
{
    public int Count;
}

Enemy first = new Enemy();
Enemy second = new Enemy();
first.Count++;
// first.Count is 1; second.Count is still 0.
```

Aceasta demonstrează stocare separată; contorul real de inamici adăugați aparține în continuare lui `GameState`, așa cum s-a stabilit mai devreme.

[00:14:05](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=845s) O clasă poate deci combina membri atașați tipului cu membri atașați obiectelor. Aceeași distincție se aplică și metodelor: o operație la nivel de tip este statică; o operație la nivel de obiect are un receptor.

## Transformarea gestionării damage-ului în metodă de instanță

[00:14:32](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=872s) Începeți prin a extrage scăderea inline a vieții într-o metodă statică. [00:14:35](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=875s) Aceasta este în continuare apelul cunoscut, calificat cu tipul și cu intrări explicite:

```csharp
// Static version inside Stats:
public static void TakeDamage(Stats target, int amount)
{
    target.Health -= amount;
}

// Attack logic calls it:
Stats.TakeDamage(enemyStats, playerStats.Attack);
```

Aici **semnătura** declară obiectul de intrare și intrarea de damage. Apelul furnizează referința către statisticile țintă și valoarea atacului.

[00:14:42](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=882s) Transformați operația într-o **metodă de instanță**, o metodă care acționează asupra unui anumit obiect:

1. Păstrați declarația metodei în interiorul tipului ale cărui date le modifică, `Stats`.
2. Eliminați `static`.
3. Eliminați parametrul `Stats target` declarat explicit.
4. [00:15:00](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=900s) Înlocuiți `target` din corp cu `this`, obiectul curent.
5. [00:15:15](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=915s) Schimbați apelul astfel încât să înceapă cu acel obiect, nu cu tipul.

Starea video modificată este:

```csharp
// Instance version inside Stats:
public void TakeDamage(int amount)
{
    this.Health -= amount;
}

// Changed attack call:
enemyStats.TakeDamage(playerStats.Attack);
```

[00:15:31](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=931s) Obiectul dinaintea punctului este **receptorul**. Gândirea lui ca „prim parametru ascuns” explică de unde își ia metoda obiectul, dar nu este un parametru pe care să îl declarați sau să îl transmiteți între paranteze. Compilatorul furnizează contextul de instanță.

[00:15:53](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=953s) Apelantul are nevoie de o referință către obiectul vizat. [00:15:59](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=959s) În noul apel, `enemyStats` este acel receptor. [00:16:02](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=962s) Nu îl transmiteți în plus ca argument; `playerStats.Attack` furnizează parametrul declarat `amount`.

[00:16:14](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=974s) Compilatorul rezolvă metoda prin tipul receptorului, aici `Stats`. [00:16:29](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=989s) În numerotarea conceptuală a lecției, receptorul este „primul”, iar damage-ul „al doilea”. În declarația C# efectivă, `amount` este singurul parametru declarat și singurul argument scris între paranteze. Nu devine obiectul și nu există un parametru de compensare literal de adăugat în semnătură.

Un apel de instanță nu poate fi înlocuit cu `Stats.TakeDamage(amount)`: operația are nevoie acum de obiectul particular de statistici a cărui viață se va modifica.

<details>
<summary>Al cărui obiect se schimbă viața în enemyStats.TakeDamage(playerStats.Attack)?</summary>

Al receptorului, enemyStats. playerStats.Attack furnizează doar valoarea damage-ului. Nu îl face pe playerStats receptor.

</details>

## Reducerea atacului fără un argument de obiect explicit

[00:16:38](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=998s) Un al doilea exemplu elimină complet intrarea de valoare a damage-ului. Porniți de la o funcție statică care primește doar obiectul țintă și îi reduce atacul cu unu:

```csharp
// Static version inside Stats:
public static void ReduceDamage(Stats target)
{
    target.Attack--;
}

// Main or attack logic:
Stats.ReduceDamage(playerStats);
```

[00:17:01](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1021s) Acest apel static are un singur argument explicit obișnuit, `playerStats`, care corespunde parametrului declarat `target`. Nu există o a doua intrare și niciun receptor ascuns pentru această metodă statică.

[00:17:23](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1043s) Transformați-o în metodă de instanță folosind aceiași pași: eliminați `static`, eliminați parametrul explicit de obiect și referiți-vă la obiectul curent. Numele ilustrativ al metodei este `ReduceDamage`; s-ar putea alege alt nume semnificativ.

```csharp
// Instance version inside Stats:
public void ReduceDamage()
{
    this.Attack--;
}

// Changed call:
playerStats.ReduceDamage();
```

[00:17:36](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1056s) Parantezele goale înseamnă că nu există argumente declarate explicit. Apelul își scrie totuși receptorul înaintea punctului. [00:17:51](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1071s) `this` îi oferă corpului acces la datele acelui obiect. [00:17:51](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1071s) Invocați cu obiect, punct și nume de metodă; nu inserați un argument suplimentar pentru receptor.

[00:18:06](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1086s) Căutarea metodei folosește tipul obiectului, iar receptorul furnizează contextul de instanță. Numirea acestui lucru prim parametru ascuns este o comparație conceptuală cu vechea versiune statică, nu o afirmație că un apel static ar primi legare automată de instanță. Cele două etape au efecte intenționate echivalente asupra atacului, în ciuda sintaxei diferite de apel și declarație.

## Citirea câmpurilor de instanță prin this

[00:18:19](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1099s) În interiorul unei metode de instanță, **`this`** înseamnă obiectul care primește apelul. Prefixul este adesea opțional atunci când un nume de câmp necalificat este neechivoc.

[00:18:32](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1112s) Comparați cu regula anterioară despre câmpurile statice: în interiorul clasei care declară, calificatorul de tip putea fi omis. Aici calificatorul opțional este **calificatorul de instanță** `this`, nu numele clasei. [00:18:52](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1132s) Câmpul poate fi adresat direct; scrierea lui `Stats.Attack` ar încerca greșit să acceseze câmpul de instanță prin tip.

Corpurile echivalente scurtate din această explicație a stării video sunt:

```csharp
// With an explicit receiver:
public void ReduceDamage()
{
    this.Attack--;
}

// Alternative spelling, when no local name hides Attack:
public void ReduceDamage()
{
    Attack--;
}
```

Alegeți o singură declarație. Ambele modifică în acest exemplu câmpul obiectului receptor. Dacă un parametru sau o variabilă locală ascunde un nume de câmp, calificarea le poate deosebi.

[00:18:59](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1139s) **Instanță** înseamnă un obiect al tipului, de aceea acestea se numesc metode de instanță. Termenul rus folosit în explicație este „экземпляр”, adică instanță sau obiect. [00:19:21](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1161s) Înregistrarea lasă versiunea non-statică în loc: apelul ei furnizează ținta prin receptor, iar corpul modifică datele acelei ținte.

## Returnarea și stocarea unei decizii de moarte

[00:19:55](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1195s) Pauză pentru exercițiu: scrieți o funcție care primește statistici și returnează dacă viața este mai mică sau egală cu zero. Un rezultat **boolean** este fie `true`, fie `false`. Forma statică inițială a exercițiului se poate reconstrui astfel:

```csharp
public static bool IsDead(Stats target)
{
    return target.Health <= 0;
}
```

[00:20:01](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1201s) Stocați răspunsul într-o variabilă `bool` și apoi verificați variabila. Aceasta separă calculul de utilizarea lui și dă rezultatului un nume lizibil, în loc să înglobeze întregul apel în condiție.

[00:20:09](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1209s) Transformați soluția într-o metodă de instanță: eliminați `static` și intrarea explicită de statistici. [00:20:13](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1213s) Păstrați tipul de returnare `bool`; faptul că devine metodă de instanță nu schimbă ce poate returna operația.

```csharp
// Inside Stats:
public bool IsDead()
{
    return Health <= 0;
}

// At the call site:
bool playerIsDead = playerStats.IsDead();
if (playerIsDead)
{
    Console.WriteLine("The game is over");
}
```

[00:20:23](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1223s) Nu se scrie niciun argument de statistici între paranteze: receptorul furnizează automat obiectul. [00:20:37](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1237s) Apelul este doar obiect, punct și nume de metodă. [00:20:50](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1250s) Înlocuiți verificarea inline cu o atribuire către booleanul denumit, ca rezultatul să poată fi reutilizat.

[00:21:14](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1274s) O **expresie** calculează o valoare. Pentru a evalua apelul, execuția intră în metodă, citește viața receptorului, o compară cu zero și returnează răspunsul boolean. Răspunsul nu apare înainte ca metoda să ruleze. [00:21:18](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1278s) În modelul explicativ, acel rezultat înlocuiește expresia din dreapta, iar **atribuirea** îl stochează în `playerIsDead`. O **condiție** folosește apoi valoarea stocată pentru a alege următoarea acțiune.

<details>
<summary>Ce returnează IsDead la viața 0, viața -2 și viața 1?</summary>

Respectiv true, true și false. Condiția este mai mică sau egală cu zero, deci zero este deja considerat moarte.

</details>

## Mutarea comportamentului într-o metodă de extensie

[00:22:01](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1321s) Demonstrația următoare întreabă dacă operația poate trăi altundeva, păstrând tipul de date concentrat pe datele sale. [00:22:05](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1325s) Codul unei metode poate fi mutat într-un fișier separat, dar mutarea lui într-o altă clasă schimbă cine îl declară. O metodă de instanță obișnuită trebuie în continuare declarată în propriul tip; forma externă demonstrată în continuare este o **metodă de extensie**.

[00:22:15](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1335s) Odată ce `IsDead` este în afara lui `Stats` și în interiorul clasei utilitare, nu mai poate rămâne o metodă de instanță obișnuită a lui `Stats`. Aceasta nu înseamnă că apelurile în stil de obiect nu pot invoca niciodată metode de instanță — exemplele anterioare fac exact asta. Înseamnă că o metodă declarată într-un alt container nu este membru de instanță al tipului receptorului.

Urmați reconstrucția intermediară:

1. Eliminați declarația `IsDead` demonstrată din `Stats`.
2. [00:22:26](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1346s) Puneți în `Logic` o metodă statică care acceptă explicit obiectul de statistici.
3. [00:22:38](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1358s) Apelați-o mai întâi ca metodă statică obișnuită.

```csharp
static class Logic
{
    public static bool IsDead(Stats target)
    {
        return target.Health <= 0;
    }
}

// Intermediate call:
bool playerIsDead = Logic.IsDead(playerStats);
```

O metodă statică nu are `this` automat, dar poate citi date de instanță prin obiectul furnizat ca parametru. Intrarea necesară aici este obiectul de statistici, nu niște „valori statice” din interiorul acelui obiect.

[00:22:38](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1358s) Mutarea într-o altă clasă nu face singură legitimă forma `playerStats.IsDead()`. [00:22:50](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1370s) Marcați primul parametru cu `this` pentru a permite această formă de apel:

```csharp
static class Logic
{
    public static bool IsDead(this Stats target)
    {
        return target.Health <= 0;
    }
}

// Extension syntax:
bool playerIsDead = playerStats.IsDead();

// Equivalent explicit static call, shown separately:
bool sameAnswer = Logic.IsDead(playerStats);
```

Aici **`this` pe primul parametru** marchează o metodă de extensie. Metoda rămâne statică; invocarea în stil de obiect furnizează receptorul ca primul său argument declarat. Tipul însuși nu a primit un nou membru de instanță obișnuit. Eliminarea metodei de instanță originale împiedică acel membru existent să aibă prioritate față de extensie.

[00:23:11](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1391s) Metoda trebuie să fie în interiorul unei clase statice pentru sintaxa predată aici. Mutarea ei într-o clasă non-statică produce diagnosticul înregistrat conform căruia metodele de extensie trebuie definite într-o clasă statică. Această lecție folosește sintaxa clasică cu primul parametru; [referința Microsoft despre metodele de extensie](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods) confirmă că acestea sunt metode statice invocate cu sintaxă de instanță.

[00:23:41](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1421s) Acest stil apare adesea în cod de configurare, inclusiv în cod care folosește **patternul builder**, care asamblează un rezultat prin operații succesive de configurare. [00:23:51](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1431s) Patternul în sine este amânat; aici nu se implementează niciun builder.

[00:23:59](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1439s) De ce alegem o metodă de extensie în locul unei metode de instanță? Această întrebare de proiectare este amânată explicit până la studierea **încapsulării**, controlul asupra accesului la internele unui obiect. Înțelegerea transformării actuale nu presupune rezolvarea acelei probleme de proiectare ulterioare.

<details>
<summary>Mutarea lui IsDead în Logic l-a făcut metodă de instanță a lui Stats?</summary>

Nu. Este o metodă statică în Logic. Marcarea primului său parametru cu `this` permite apelul în stil de receptor și transmite obiectul `Stats` către acel parametru.

</details>

## Înțelegerea punctului de intrare generat

[00:24:11](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1451s) Revenim la fișierul principal special. **Instrucțiunile de nivel superior** sunt o sintaxă de sursă comodă: compilatorul furnizează structura de program înconjurătoare și punctul de intrare.

[00:24:23](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1463s) Înregistrarea schițează o clasă `Program` cu un `Main` static care acceptă un tablou de argumente de linie de comandă. Forma ilustrată este `public static int Main(string[] args)`. Priviți-o ca pe un echivalent explicit doar explicativ, nu ca pe numele, nivelul de acces sau tipul de returnare garantate pentru codul generat de compilator. Forma punctului de intrare generat depinde de ce face programul de nivel superior.

Acest model video scurtat plasează instrucțiunile executabile și funcțiile locale în interiorul unei metode:

```csharp
// Explicit explanatory equivalent, not exact compiler output:
class Program
{
    public static int Main(string[] args)
    {
        GameState state = new GameState();
        IncrementCounter(state);
        return 0;

        static void IncrementCounter(GameState gameState)
        {
            gameState.AddedEnemies++;
        }
    }
}
```

**Punctul de intrare** este metoda în care începe execuția. **Argumentele de linie de comandă** sunt intrări la pornire; `string[] args` este parametrul care reprezintă un tablou de șiruri. [00:24:45](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1485s) Utilizarea lor detaliată este amânată.

[00:24:53](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1493s) Păstrați distinctă ordinea din sursă de această transformare conceptuală: într-un fișier care folosește instrucțiuni de nivel superior, codul executabil de nivel superior trebuie să preceadă declarațiile de spațiu de nume sau de tipuri. Tipurile definite de utilizator rămân declarații de tipuri; compilatorul nu mută fiecare declarație în corpul punctului de intrare. Structura generată nu promite o anumită ordine textuală „deasupra” sau „dedesubtul” tipurilor. Aceste reguli despre punctul de intrare și ordinea din sursă sunt documentate în [referința Microsoft despre instrucțiunile de nivel superior](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements).

[00:25:06](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1506s) Tipurile pot fi declarate și în interiorul altor tipuri. Înregistrarea exprimă explicit îndoiala dacă compilatorul folosește această dispunere aici; nu este dovadă că propriile tale tipuri sunt imbricate automat. [00:25:22](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1522s) Alte detalii adăugate de compilator, menționate la finalul schiței, sunt lăsate pentru mai târziu.

[00:25:28](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1528s) Funcțiile statice scrise anterior după codul executabil principal și înainte de declarațiile de tipuri ale utilizatorului sunt **funcții locale** în corpul punctului de intrare. Ele nu devin automat metode utilitare publice ale lui `Program`. Reconstrucția de mai sus arată amplasarea lor locală și explică de ce mutarea lor într-o clasă utilitară explicită le schimbă vizibilitatea.

## Păstrarea explicită a dependențelor funcțiilor locale

[00:25:41](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1541s) O **funcție locală** este declarată în interiorul altei funcții. Poate fi declarată chiar și după `return`: declarația în sine nu este o instrucțiune executată, deci nu rulează în acel punct și nu împiedică `return`-ul să iasă.

Acest exemplu video simplificat arată apelul înaintea declarației și declarația după `return`:

```csharp
static int Main(string[] args)
{
    GameState state = new GameState();
    IncrementCounter(state);
    return 0;

    static void IncrementCounter(GameState gameState)
    {
        gameState.AddedEnemies++;
    }
}
```

[00:25:52](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1552s) **Domeniul** său, regiunea în care numele este disponibil, este interiorul funcției care o conține. [00:26:00](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1560s) Codul din afara lui `Main` nu poate apela pur și simplu acest `IncrementCounter` local; nu este membru de clasă vizibil altor metode.

[00:26:13](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1573s) Dacă funcția locală omite `static`, poate accesa variabilele și parametrii locali din jur. **Captura** înseamnă folosirea acestor date din jur în locul cererii unei intrări explicite. Nu înseamnă că compilatorul capturează lacom fiecare variabilă locală, folosită sau nu.

[00:26:31](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1591s) Înregistrarea arată un diagnostic privind un nume deja vizibil din `Main`. Pericolul important este schimbarea sursei unui nume: eliminarea unui parametru explicit poate face corpul să se refere la variabila locală din jur. **Ascunderea** înseamnă mascarea unui nume cu altul; coliziunile depind de declarațiile și domeniile implicate, deci diagnosticul demonstrat nu este o regulă că orice nume identic produce mereu aceeași eroare.

Problema capturii și alternativa cu intrare explicită se pot reconstrui împreună:

```csharp
// Non-static local function in Main:
GameState state = new GameState();
IncrementCounter();

void IncrementCounter()
{
    state.AddedEnemies++; // uses the enclosing local
}

// Alternative static local function and call:
IncrementCounterExplicit(state);

static void IncrementCounterExplicit(GameState gameState)
{
    gameState.AddedEnemies++; // uses its declared input
}
```

[00:26:51](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1611s) Recomandarea cursului este să scrieți funcții locale statice atunci când operația trebuie să depindă doar de intrări explicite. Astfel, o referință accidentală la o variabilă locală din jur este respinsă, în loc de a cupla în tăcere funcția de acea variabilă. `static` nu este obligatoriu pentru fiecare funcție locală din C#; este protecția recomandată pentru aceste ajutoare. [Referința Microsoft despre funcțiile locale](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions) explică domeniul local, declarațiile după `return` și restricția de captură pentru funcțiile locale statice.

Lecția se încheie cu aceste limite intacte: mecanica detaliată a prefixului de clasă este amânată, sensul lui `static` este inițial amânat și apoi explicat, folosirile speciale ale câmpurilor statice rămân material ulterior, verificarea booleană a morții este o sarcină de practică, proiectarea metodelor de extensie așteaptă încapsularea, patternul builder este doar numit, iar gestionarea liniei de comandă și detaliile suplimentare generate de compilator rămân în afara demonstrației.

<details>
<summary>De ce ajută ca un ajutor să fie static atunci când este introdusă o altă stare de joc?</summary>

Ajutorul nu poate continua în tăcere să folosească variabila de stare din jur. `GameState`-ul său necesar trebuie transmis explicit, ceea ce face jocul intenționat vizibil la locul apelului.

</details>

## Istoricul codului

Aceste legături stabile către stările video urmăresc construcția de la cea mai veche la cea mai recentă. Transcrierea imutabilă verificată susține înregistrarea; nu au fost disponibile stări angajate exacte de sursă de luptă.

- [00:01:20](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=80s) — Mutarea declarațiilor de tipuri din fișierul sursă principal într-un al doilea fișier de proiect.
- [00:02:36](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=156s) — Înfășurarea funcțiilor mutate într-o clasă statică și calificarea apelurilor din exterior.
- [00:05:31](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=331s) — Încercarea unui contor static incrementat la adăugarea inamicilor.
- [00:08:35](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=515s) — Mutarea acelui contor care se schimbă în `GameState`; [00:08:46](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=526s) confirmă rezultatul înregistrat de șase.
- [00:10:33](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=633s) — Crearea unui `Stats` inițializat printr-o fabrică statică; [00:12:00](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=720s) adaugă valori implicite mai puternice.
- [00:14:32](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=872s) — Extragerea scăderii vieții într-o metodă statică de damage; [00:14:42](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=882s) o transformă în metodă de instanță.
- [00:16:38](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=998s) — Adăugarea funcției de reducere a atacului pe un singur obiect; [00:17:23](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1043s) o transformă într-un apel de instanță fără argumente.
- [00:20:09](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1209s) — Transformarea verificării booleană a morții într-o metodă de instanță; [00:20:50](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1250s) stochează rezultatul înainte de a-l verifica.
- [00:22:26](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1346s) — Mutarea verificării morții înapoi într-un ajutor static extern; [00:22:50](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1370s) activează sintaxa de apel de extensie.
- [00:24:23](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1463s) — Schițarea structurii explicite `Program`/`Main` din spatele instrucțiunilor de nivel superior.
- [00:25:41](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1541s) — Plasarea unei funcții locale după `return`; [00:26:51](https://www.youtube.com/watch?v=UVLUbfcCcG0&t=1611s) recomandă `static` pentru a evita captura accidentală.
