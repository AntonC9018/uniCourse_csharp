> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Metode virtuale, clase abstracte și interfețe

Această lecție oferă un răspuns practic la o întrebare: când mai multe tipuri oferă aceeași operație, care implementare se execută? Pornim de la un animal și o pasăre, urmărim apelurile prin referințe și prin tabelul metodelor virtuale, apoi folosim aceleași idei pentru a transforma funcțiile meniului în obiecte care poartă comportament și date. [00:00:10](https://www.youtube.com/watch?v=82vaxb6o_rg&t=10s)

Termenii folosiți pe parcurs sunt:

- **Clasă, clasă de bază, clasă derivată, moștenire și este-un:** o clasă își descrie instanțele. O clasă derivată extinde o clasă de bază și trebuie să poată fi folosită oriunde este așteptat acel tip de bază. O **interfață** exprimă un contract de comportament pe care o clasă îl implementează; poate fi folosită și ca tip de referință.
- **Obiect, referință și receptor:** un obiect este o instanță concretă; o referință identifică acel obiect; receptorul este obiectul pe care rulează o metodă de instanță. Un **câmp** memorează date într-un obiect, iar un **câmp static** aparține tipului.
- **Tip static și tip de la timpul execuției:** tipul static aparține variabilei sau expresiei și determină căutarea membrilor la compilare. Tipul de la timpul execuției aparține obiectului efectiv. **Conversia ascendentă** privește un obiect derivat printr-un tip de clasă de bază sau de interfață implementată, fără a copia obiectul.
- **Ascunderea și suprascrierea metodelor:** ascunderea declară un membru distinct cu același nume; suprascrierea furnizează o implementare nouă pentru o operație virtuală moștenită. **Polimorfismul** permite ca aceeași operație, pe un tip de referință comun, să se comporte diferit pentru obiecte diferite.
- **Dispecerizare virtuală, legare statică și legare târzie:** dispecerizarea virtuală alege o suprascriere folosind tipul de la timpul execuției al receptorului. Un apel nevirtual își are ținta fixată de căutarea de la compilare. Videoclipul numește alegerea implementării la timpul execuției dispecerizare dinamică sau legare târzie; aceasta nu impune folosirea cuvântului cheie `dynamic` din C# și nu transformă o metodă de instanță într-una `static`.
- **Tabelul metodelor virtuale (vtable), poziția și adresa de cod:** tabelul conceptual al lecției asociază fiecărei operații virtuale o poziție care conține adresa de cod a implementării sale. Adresele ilustrate sunt etichete, nu adrese de proces măsurate.
- **`virtual`, `override` și `abstract`:** `virtual` permite înlocuirea în descendenți; `override` realizează acea înlocuire; o clasă abstractă nu poate fi construită direct, iar o metodă abstractă declară o operație fără a furniza un corp.
- **`sealed` și devirtualizarea:** o clasă `sealed` nu poate avea subclase; o suprascriere `sealed` nu mai poate fi suprascrisă. Devirtualizarea este o optimizare care transformă o operație virtuală într-un apel direct atunci când ținta ei este cunoscută.
- **`this` și `base`:** `this` desemnează receptorul curent; `base` accesează membrii moșteniți și, pentru apelurile de metodă de aici, selectează direct implementarea din baza.
- **Parametru, valoare de returnare, semnătură, suprasarcină și metodă cu corp expresie:** parametrii primesc argumentele apelului, iar o valoare de returnare este rezultatul unei metode; `void` înseamnă că nu există valoare returnată. O semnătură distinge metodele după nume și informațiile despre parametri; suprasarcinile au același nume, dar acceptă liste diferite de parametri. Un corp cu o singură expresie poate folosi `=>`. O valoare de returnare covariantă este un tip de returnare mai derivat, permis anumitor suprascrieri; este o clarificare modernă, dincolo de exemplele videoclipului.
- **Compoziție și încapsulare:** compoziția reprezintă un obiect folosind date sau obiecte componente care colaborează, în loc de câte o subclasă pentru fiecare proprietate. Încapsularea păstrează detaliile de implementare în interiorul tipului care le deține.
- **Implementare implicită și explicită a interfeței:** o metodă publică a clasei care corespunde poate satisface implicit o interfață; o implementare calificată explicit aparține unei interfețe denumite și se accesează prin acea interfață.
- **Conversie explicită și tipar de tip:** o conversie explicită schimbă modul în care o valoare este privită; un tipar precum `value is ISpeak speaker` verifică compatibilitatea și introduce o variabilă cu tipul testat.
- **Delegate, grup de metode și obiect de acțiune:** un delegate este o valoare invocabilă care se referă la un comportament de metodă; un grup de metode este un nume de metodă folosit pentru a selecta un comportament compatibil. Un obiect de acțiune oferă comportament printr-un contract de clasă sau de interfață și poate purta și configurare.
- **Membru obligatoriu, inițializator de obiect și constructor:** un membru obligatoriu trebuie inițializat la construirea unui obiect; un inițializator de obiect furnizează valorile membrilor după construire. Un constructor controlează modul în care este creată o instanță, iar un constructor privat limitează crearea la tipul care îl declară.
- **Singleton și `readonly`:** un singleton expune o singură instanță partajată. Un câmp readonly restrânge atribuirea la referința lui; nu face automat obiectul referit nemutabil.
- **Proprietate și câmp de sprijin:** o proprietate expune accesul la membri prin accesori; un câmp de sprijin memorează datele folosite de un accessor. Variantele de proprietăți sunt amânate aici.
- **Tip dedus și anotație „anulează-null”:** `var` deduce tipul static al unei variabile din inițializatorul ei. `!` poate suprima avertismentele analizei de nulabilitate; nu oferă nicio validare la timpul execuției.
- **Modificatori de acces:** `public` permite accesul din exterior, `private` limitează accesul la tipul care declară, iar `protected` permite accesul din clasa declarantă și din descendenții ei.
- **Serializare și identificator:** serializarea reprezintă datele unui obiect pentru stocare sau transfer; un identificator distinge o entitate în acea reprezentare.

Pentru practici, [laboratorul de polimorfism și interfețe](../../../labs/1_basic/18_interface.md) din depozit cere comenzi de meniu implementate o dată cu delegate-uri și o dată cu interfețe.

Înregistrarea este sursa pentru stările de demonstrare succesive de mai jos. Clonarea deținută nu oferă sursă commitată pentru aceste fragmente exacte, astfel că sunt etichetate drept **reconstrucții simplificate ale stărilor din videoclip**, nu versiuni exacte din depozit. Marcajul de timp apropiat al fiecărei mostre îi identifică contextul înregistrat. Declarațiile de clase și instrucțiunile de la locul apelului sunt prezentate împreună pentru explicație; puneți acele instrucțiuni în metoda de execuție a programului care le conține când asamblați un program rulabil. Diagramele cu tabele descriu modelul de dispecerizare al lecției; machetele reale de memorie, selecția instrucțiunilor și optimizările sunt detalii de implementare.

Lecția acoperă dispecerizarea metodelor, contractele de clasă și de interfață, proiectarea prin moștenire, obiectele de acțiune și un mic singleton. Corpurile implicite ale interfețelor, moștenirea între interfețe, comportamentul lui `base` în constructori și variantele de proprietăți sunt în afara domeniului demonstrat; [limitele lecției](#lesson-boundaries) reunesc excluderile finale.

## De la metode ascunse la dispecerizarea la timpul execuției

La [00:00:10](https://www.youtube.com/watch?v=82vaxb6o_rg&t=10s), clasa de bază este deliberat mică: `Animal` nu are câmpuri, iar singura sa operație afișează trei semne de întrebare. La [00:00:21](https://www.youtube.com/watch?v=82vaxb6o_rg&t=21s), `Bird : Animal` stabilește moștenirea, iar o metodă cu același nume afișează un cirip. O clasă derivată moștenește forma de bază, dar repetarea unui nume de metodă nu o suprascrie.

Stare inițială simplificată din videoclip:

```csharp
class Animal
{
    public void Speak() => Console.WriteLine("???");
}
class Bird : Animal
{
    public new void Speak() => Console.WriteLine("chirp");
}

// Instrucțiuni la locul apelului:
Bird bird = new Bird();
bird.Speak();               // chirp
Animal animal = bird;
animal.Speak();             // ???
```

Modificatorul explicit `new` marchează **ascunderea metodei**, adică un membru distinct cu același nume; face limpedă intenția exemplului inițial. Omiterea acelui modificator nu transformă metoda într-o suprascriere.

La [00:00:29](https://www.youtube.com/watch?v=82vaxb6o_rg&t=29s), o variabilă `Bird` apelează metoda vizibilă pe `Bird`. La [00:00:47](https://www.youtube.com/watch?v=82vaxb6o_rg&t=47s), atribuirea aceleiași păsări unei variabile `Animal` este o **conversie ascendentă** legală: orice pasăre este un animal. La [00:01:00](https://www.youtube.com/watch?v=82vaxb6o_rg&t=60s), apelul prin acea variabilă selectează `Animal.Speak`. **Tipul său static**, `Animal`, controlează căutarea membrilor; compilatorul nu alege metoda ascunsă doar pentru că obiectul se întâmplă să fie o pasăre.

Schimbarea motivată la [00:01:27](https://www.youtube.com/watch?v=82vaxb6o_rg&t=87s) este să păstrăm acea referință comună `Animal` selectând în schimb comportamentul din **tipul de la timpul execuției** al obiectului. Faceți operația virtuală:

1. La [00:01:58](https://www.youtube.com/watch?v=82vaxb6o_rg&t=118s), adăugați `virtual` la metoda de bază.
2. Înlocuiți modificatorul de ascundere din declarația derivată cu `override`.
3. Rulați același apel cu tip de bază. La [00:02:06](https://www.youtube.com/watch?v=82vaxb6o_rg&t=126s), rezultatul observat este ieșirea păsării.

Stare modificată simplificată:

```csharp
class Animal
{
    public virtual void Speak() => Console.WriteLine("???");
}
class Bird : Animal
{
    public override void Speak() => Console.WriteLine("chirp");
}

// Același obiect și același apel cu tip Animal:
Animal animal = new Bird();
animal.Speak();             // chirp
```

Aici **`virtual`** permite alegerea implementării la timpul execuției, iar **`override`** furnizează o înlocuire pentru acea operație moștenită. Aceeași sintaxă de apel poate exprima acum **polimorfismul**. Metodele sunt nevirtuale în mod implicit; o metodă nevirtuală nu poate fi suprascrisă. [Referință C# pentru virtual](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual).

<details>
<summary>De ce a schimbat inițial ieșirea faptul că variabila a devenit Animal?</summary>

Pentru că a schimbat căutarea membrilor la compilare. Metodele originale erau membri nevirtuali distincți. Odată ce Animal.Speak devine virtuală iar Bird suprascrie acea operație, apelul cu tip Animal poate selecta implementarea lui Bird pornind de la receptorul efectiv.

</details>

## Două referințe și un singur obiect

Analiza de la [00:02:09](https://www.youtube.com/watch?v=82vaxb6o_rg&t=129s) separă două variabile locale de informațiile de la timpul execuției care reprezintă cele două tipuri. Faptul că se menționează `Animal` și `Bird` nu înseamnă că s-a construit câte o instanță din fiecare clasă.

Stare simplificată din videoclip la [00:02:26](https://www.youtube.com/watch?v=82vaxb6o_rg&t=146s)–[00:02:50](https://www.youtube.com/watch?v=82vaxb6o_rg&t=170s):

```csharp
var bird = new Bird();       // tip static dedus: Bird
Animal animal = bird;       // copiază referința
animal.Speak();
```

1. `new Bird()` construiește un **obiect Bird** și produce o referință către el.
2. `var` deduce **tipul static** al variabilei din inițializatorul ei, aici `Bird`; nu este tipizare la timpul execuției.
3. Atribuirea lui `bird` către `animal` copiază **referința**, nu câmpurile obiectului. Ambele variabile identifică un singur obiect.
4. În `animal.Speak()`, expresia din stânga punctului furnizează **receptorul**, iar `Speak()` numește operația cerută. [00:03:18](https://www.youtube.com/watch?v=82vaxb6o_rg&t=198s)
5. Pentru operația virtuală, selecția de la timpul execuției ajunge la implementarea asociată tipului efectiv al acelui receptor. [00:03:26](https://www.youtube.com/watch?v=82vaxb6o_rg&t=206s)

La [00:03:00](https://www.youtube.com/watch?v=82vaxb6o_rg&t=180s), imaginea memoriei oferă celor două corpuri de metodă adrese de cod distincte. Acele corpuri pot coexista chiar dacă operația are același nume. Desenul tratează referințele ca adrese de obiect; o referință gestionată nu promite că un obiect va rămâne pentru totdeauna la aceeași adresă numerică brută.

## Modelul tabelului metodelor virtuale

La [00:03:43](https://www.youtube.com/watch?v=82vaxb6o_rg&t=223s), selecția de la timpul execuției are nevoie de evidență contabilă: cum identifică un tip implementarea sa pentru o operație? Lecția răspunde cu un **tabel al metodelor virtuale**, sau vtable, asociat fiecărui tip. O **poziție** este o intrare pentru o operație; **adresa sa de cod** spune mecanismului conceptual de dispecerizare unde începe acea implementare. [00:04:03](https://www.youtube.com/watch?v=82vaxb6o_rg&t=243s)

Starea de tabel ilustrată la [00:04:10](https://www.youtube.com/watch?v=82vaxb6o_rg&t=250s)–[00:05:06](https://www.youtube.com/watch?v=82vaxb6o_rg&t=306s) este:

| Tip de la timpul execuției | Poziția moștenită Speak |
| --- | --- |
| Animal | 100: Animal.Speak |
| Bird | 200: Bird.Speak |

Acestea sunt adrese ilustrative. Poziția aparține operației virtuale moștenite, nu doar oricărei metode care are textul `Speak` în nume. `Bird` are acea poziție pentru că `Animal` a declarat operația virtuală. **Suprascrierea** înlocuiește implementarea asociată poziției moștenite; nu introduce o a doua poziție pentru acea suprascriere.

Dacă `Bird` nu ar furniza nicio suprascriere, poziția sa ar păstra adresa moștenită 100. [00:05:13](https://www.youtube.com/watch?v=82vaxb6o_rg&t=313s)

Pentru apelul virtual reconstruit `animal.Speak()` discutat la [00:05:26](https://www.youtube.com/watch?v=82vaxb6o_rg&t=326s):

```csharp
Animal animal = new Bird();
animal.Speak();              // Implementarea Speak a lui Bird
```

Urmăriți modelul în ordine:

1. Citiți referința din variabilă.
2. Ajungeți la obiectul receptor.
3. Urmăriți informațiile de tip de la timpul execuției ale obiectului.
4. Citiți poziția acelui tip pentru operația virtuală selectată.
5. Invocați codul identificat de poziție. [00:05:55](https://www.youtube.com/watch?v=82vaxb6o_rg&t=355s)

Tipul static al variabilei determină faptul că `Speak` este o operație legală; tipul de la timpul execuției al receptorului determină ce suprascriere se execută.

## Comparație între obiecte de bază și apeluri nevirtuale

La [00:06:00](https://www.youtube.com/watch?v=82vaxb6o_rg&t=360s), adăugați un obiect real al clasei de bază. Convertirea ascendentă a unei păsări și construirea unui animal produc tipuri de la timpul execuției diferite.

Stare simplificată din videoclip, cu metodele virtuale încă în loc:

```csharp
Animal first = new Bird();
Animal second = new Animal();
first.Speak();               // chirp
second.Speak();              // ???
```

La [00:06:14](https://www.youtube.com/watch?v=82vaxb6o_rg&t=374s), al doilea obiect este într-adevăr un `Animal`. Memorarea lui într-o variabilă nu poate inventa un tip de la timpul execuției mai derivat. La [00:06:29](https://www.youtube.com/watch?v=82vaxb6o_rg&t=389s), urmărirea referinței sale ajunge deci la tabelul lui Animal și la adresa 100. La [00:06:54](https://www.youtube.com/watch?v=82vaxb6o_rg&t=414s)–[00:07:11](https://www.youtube.com/watch?v=82vaxb6o_rg&t=431s), sintaxa de apel identică cu tip `Animal` ajunge la implementarea lui Bird pentru primul receptor și la implementarea lui Animal pentru al doilea.

Rezumatul de la [00:07:19](https://www.youtube.com/watch?v=82vaxb6o_rg&t=439s)–[00:07:44](https://www.youtube.com/watch?v=82vaxb6o_rg&t=464s) este că `virtual` stabilește poziția moștenită a operației, iar `override` înlocuiește implementarea acelui tip în poziție.

La [00:07:53](https://www.youtube.com/watch?v=82vaxb6o_rg&t=473s), considerați eliminarea lui `virtual`. O `override` nu mai compilează atunci. Dacă membrul derivat este în schimb o metodă obișnuită ascunsă, operația lui Animal nu are poziție virtuală în acest model simplificat.

Stare nevirtuală reconstruită la [00:08:07](https://www.youtube.com/watch?v=82vaxb6o_rg&t=487s):

```csharp
class Animal
{
    public void Speak() => Console.WriteLine("???");
}
class Bird : Animal
{
    public new void Speak() => Console.WriteLine("chirp");
}

// Ținta este fixată de tipul static al expresiei:
Animal animal = new Bird();
animal.Speak();              // Animal.Speak
```

Un **apel nevirtual** folosește implementarea selectată din tipul expresiei și sare peste alegerea implementării virtuale. La [00:08:25](https://www.youtube.com/watch?v=82vaxb6o_rg&t=505s)–[00:08:43](https://www.youtube.com/watch?v=82vaxb6o_rg&t=523s), videoclipul numește aceasta un **apel static**: ținta este cunoscută la compilare. Acesta apelează în continuare o metodă de instanță și furnizează în continuare un obiect.

La [00:09:18](https://www.youtube.com/watch?v=82vaxb6o_rg&t=558s), alegerea unei suprascrieri din receptor la timpul execuției se numește **dispecerizare dinamică** sau **legare târzie**. Păstrați această terminologie distinctă de o expresie C# `dynamic`, care amână legarea limbajului, precum selectarea unui membru sau a unei suprasarcini. [Specificația C# pentru legarea expresiilor](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/expressions#123-static-and-dynamic-binding).

## Continuarea lanțului de suprasceri

La [00:09:34](https://www.youtube.com/watch?v=82vaxb6o_rg&t=574s), introduceți un al treilea nivel, `Sparrow : Bird`. O suprascriere moștenită rămâne suprascrisibilă cu excepția cazului `sealed`, deci `virtual` nu trebuie scris din nou.

Stare simplificată din videoclip la [00:09:37](https://www.youtube.com/watch?v=82vaxb6o_rg&t=577s)–[00:09:53](https://www.youtube.com/watch?v=82vaxb6o_rg&t=593s):

```csharp
class Sparrow : Bird
{
    public override void Speak() => Console.WriteLine("sparrow");
}

// Bird este tipul de referință, Sparrow este tipul obiectului:
Bird bird = new Sparrow();
bird.Speak();                // sparrow
```

**`override`** reutilizează operația virtuală moștenită la orice adâncime a lanțului de moștenire. Chiar și o referință cu tip de bază ajunge la cea mai derivată suprascriere aplicabilă. La [00:10:07](https://www.youtube.com/watch?v=82vaxb6o_rg&t=607s), desenul are acum informații de tip de la timpul execuției pentru Animal, Bird și Sparrow, cu tabele corespunzătoare. Selecția urmează tipul de la timpul execuției al obiectului, nu tipul scris pe variabila locală.

La [00:10:25](https://www.youtube.com/watch?v=82vaxb6o_rg&t=625s)–[00:10:54](https://www.youtube.com/watch?v=82vaxb6o_rg&t=654s), poziția moștenită Speak există deja în toți descendenții; fiecare suprascriere își schimbă propria implementare pentru acea poziție. Nu este necesară nicio declarație suplimentară de `virtual` și nici construirea manuală a tabelului.

La [00:11:04](https://www.youtube.com/watch?v=82vaxb6o_rg&t=664s)–[00:11:19](https://www.youtube.com/watch?v=82vaxb6o_rg&t=679s), eliminați o suprascriere derivată. Dacă metoda este eliminată complet, poziția moștenită păstrează implementarea moștenită: adresa 100, dacă nicio clasă intermediară nu suprascrie Animal.Speak. Dacă se elimină doar cuvântul cheie `override` și rămâne corpul cu același nume, acea declarație ascunde membrul moștenit și nu înlocuiește poziția sa virtuală. Un apel virtual cu tip de bază ajunge deci în continuare la implementarea moștenită.

## Câmpuri și receptorul ascuns

La [00:11:39](https://www.youtube.com/watch?v=82vaxb6o_rg&t=699s), metodele virtuale pot accesa **câmpuri** exact ca metodele obișnuite de instanță. Un câmp memorează date în fiecare obiect; receptorul ascuns **`this`** identifică obiectul concret ale cărui câmpuri le folosește metoda. Un obiect derivat conține câmpurile moștenite și poate adăuga altele proprii.

La [00:11:47](https://www.youtube.com/watch?v=82vaxb6o_rg&t=707s), o metodă derivată ar putea afișa un `Name` moștenit. La [00:11:57](https://www.youtube.com/watch?v=82vaxb6o_rg&t=717s)–[00:12:07](https://www.youtube.com/watch?v=82vaxb6o_rg&t=727s), demonstrația adaugă date derivate și afișează o dată pentru fiecare copil. Această reconstrucție simplificată numește câmpul derivat de număr de copii `Children`; contorul său de buclă `i` se incrementează după fiecare afișare. Ideea este comportamentul repetat din metoda derivată, nu o reconstrucție exactă a scrierii câmpului.

Stare simplificată cu câmpuri și buclă din videoclip:

```csharp
class Animal
{
    public required string Name;
    public virtual void Speak() => Console.WriteLine(Name);
}
class Bird : Animal
{
    public required int Children;
    public override void Speak()
    {
        for (int i = 0; i < Children; i++)
            Console.WriteLine("chirp");
    }
}

// Într-un loc de apel separat:
Animal animal = new Bird { Name = "sparrow", Children = 5 };
animal.Speak();
```

La [00:12:12](https://www.youtube.com/watch?v=82vaxb6o_rg&t=732s), construirea trebuie să furnizeze atât membrii obligatorii moșteniți, cât și pe cei derivați. **Inițializatorul de obiect** stabilește valorile membrilor pe instanța construită. Aceasta este o restricție a **membrilor obligatorii**, nu o regulă conform căreia orice câmp obișnuit trebuie inițializat întotdeauna explicit. [Referință C# pentru required](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required).

La [00:12:25](https://www.youtube.com/watch?v=82vaxb6o_rg&t=745s), rularea observată afișează ciripul de cinci ori și nu execută metoda lui Animal. La [00:12:34](https://www.youtube.com/watch?v=82vaxb6o_rg&t=754s), numele moștenit rămâne memorat în obiect chiar dacă această suprascriere nu îl citește niciodată. Dispecerizarea unei implementări derivate nu elimină datele moștenite.

La [00:12:48](https://www.youtube.com/watch?v=82vaxb6o_rg&t=768s)–[00:13:19](https://www.youtube.com/watch?v=82vaxb6o_rg&t=799s), distingeți o referință receptor de un șir precum numele său: variabila locală păstrează referința obiectului, nu o copie a numelui său sau a tuturor câmpurilor sale. Apelul virtual acționează asupra acelui obiect.

La [00:13:19](https://www.youtube.com/watch?v=82vaxb6o_rg&t=799s)–[00:14:46](https://www.youtube.com/watch?v=82vaxb6o_rg&t=886s), argumentul de siguranță este:

1. Runtime-ul alege implementarea lui Bird numai pentru un receptor compatibil cu Bird.
2. Acea metodă poate folosi deci membrii proprii ai lui Bird și membrii moșteniți ai lui Animal.
3. Dacă Bird nu are suprascriere, selecția păstrează implementarea moștenită; corpul lui Animal se poate baza pe faptul că receptorul este cel puțin un Animal.
4. Tipurile derivate pot adăuga câmpuri și operații virtuale fără să le invalideze pe cele moștenite. [00:14:52](https://www.youtube.com/watch?v=82vaxb6o_rg&t=892s)–[00:15:02](https://www.youtube.com/watch?v=82vaxb6o_rg&t=902s)

Ignorarea câmpurilor suplimentare din desen explică de ce un obiect derivat poate fi privit ca obiect de bază; nu înseamnă că o conversie ascendentă trunchiază sau copiază fizic obiectul.

Clarificarea explicită de la [00:15:18](https://www.youtube.com/watch?v=82vaxb6o_rg&t=918s)–[00:15:31](https://www.youtube.com/watch?v=82vaxb6o_rg&t=931s) este crucială: în interiorul metodei lui Bird, **tipul static al lui `this` este Bird**, clasa declarantă. Obiectul pe care îl desemnează poate avea tipul de la timpul execuției Bird sau un descendent precum Sparrow. Dispecerizarea nu face ca tipul de compilare al lui `this` să varieze în funcție de apelant. [Specificația C# pentru accesul this](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/expressions#12814-this-access).

## Adăugarea unei a doua metode virtuale

La [00:15:41](https://www.youtube.com/watch?v=82vaxb6o_rg&t=941s), o metodă virtuală poate începe la orice nivel. Adăugați `FlapWings` la Bird la [00:15:46](https://www.youtube.com/watch?v=82vaxb6o_rg&t=946s); Animal nu are o asemenea operație. Sparrow o poate suprascrie la [00:16:04](https://www.youtube.com/watch?v=82vaxb6o_rg&t=964s).

Stare simplificată din videoclip:

```csharp
class Animal
{
    public virtual void Speak() => Console.WriteLine("???");
}
class Bird : Animal
{
    public override void Speak() => Console.WriteLine("chirp");
    public virtual void FlapWings() => Console.WriteLine("bird flaps");
}
class Sparrow : Bird
{
    public override void Speak() => Console.WriteLine("sparrow");
    public override void FlapWings() => Console.WriteLine("sparrow flaps");
}

// Instrucțiuni la locul apelului:
Animal animal = new Sparrow();
// animal.FlapWings();        // eroare: absentă din contractul lui Animal
Bird bird = new Sparrow();
bird.Speak();
bird.FlapWings();
```

La [00:16:11](https://www.youtube.com/watch?v=82vaxb6o_rg&t=971s), o expresie cu tip Animal nu poate apela `FlapWings`, care există doar în Bird. Tipul static controlează membrii disponibili chiar și atunci când receptorul de la timpul execuției este un Sparrow. La [00:16:22](https://www.youtube.com/watch?v=82vaxb6o_rg&t=982s), schimbarea tipului expresiei în Bird face acel apel legal. La [00:16:32](https://www.youtube.com/watch?v=82vaxb6o_rg&t=992s), rularea înregistrată execută ambele implementări ale lui Sparrow.

La [00:16:38](https://www.youtube.com/watch?v=82vaxb6o_rg&t=998s), corpurile de metodă cu o singură instrucțiune sunt scurtate cu **sintaxa de corp-expresie**. `=>` introduce expresia care formează corpul; păstrează operația și comportamentul de dispecerizare. De exemplu, forma reconstruită `public override void Speak() => Console.WriteLine("sparrow");` este forma scurtă a unui corp care conține acea singură instrucțiune.

La [00:16:50](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1010s)–[00:17:44](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1064s), modelul are acum două poziții independente:

| Tip | Speak | FlapWings |
| --- | --- | --- |
| Animal | 100 | absent |
| Bird | 200 | 300 |
| Sparrow | 400 | 500 |

`virtual` introduce o poziție în clasa sa declarantă și în descendenți. `override` schimbă implementarea acelei operații în tipul derivat; nu afectează cealaltă poziție. La [00:17:54](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1074s), fără suprascrierea corespunzătoare, poziția ar păstra adresa moștenită.

La [00:18:11](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1091s)–[00:18:22](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1102s), ambele apeluri urmează aceeași traseu: referință → receptor → tip de la timpul execuției → poziția operației selectate → cale de cod. Numărul celorlalte metode virtuale nu schimbă această regulă.

## Extinderea unei implementări cu base

La [00:18:31](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1111s), o suprascriere poate adăuga comportament păstrând comportamentul existent din bază. Copierea corpului metodei de bază este fragilă: acesta poate fi complicat sau poate depinde de date **încapsulate** la care clasa derivată nu are acces. [00:18:55](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1135s)

Stare simplificată din videoclip la [00:19:09](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1149s):

```csharp
class Sparrow : Bird
{
    public override void FlapWings()
    {
        Console.WriteLine("prepare");
        base.FlapWings();    // selectează direct implementarea lui Bird
        Console.WriteLine("finish");
    }
}
```

**`base`** accesează membrii clasei de bază. Pentru această invocare de metodă, selectează direct implementarea moștenită, în loc să redispecerize receptorul curent către suprascrierea lui Sparrow. Receptorul este tot același obiect; nu se creează niciun obiect de bază separat.

La [00:19:20](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1160s)–[00:19:24](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1164s), compilatorul știe cărei implementări din bază se referă metoda înconjurătoare a lui Sparrow. O suprascriere ulterioară dintr-o subclasă nu înlocuiește ținta acelui apel calificat cu `base`.

La [00:19:25](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1165s), apelurile repetate calificate cu `base` sunt legale și rulează în ordinea din sursă. La [00:19:29](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1169s)–[00:20:06](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1206s), și alte metode de instanță accesibile ale clasei de bază pot fi apelate prin `base` din cod de instanță adecvat din clasa derivată. Accesibilitatea se aplică în continuare; exemplele lecției se referă la metode, nu la constructori sau la contexte statice arbitrare.

## Apelurile pe this rămân virtuale

Întrebarea de la [00:20:13](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1213s) este dacă un apel dintr-o metodă virtuală în alta folosește automat implementarea din aceeași clasă. Simplificați mostra înainte de a răspunde:

1. La [00:20:32](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1232s), eliminați din Sparrow suprasarcina suplimentară FlapWings.
2. La [00:20:34](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1234s), eliminați apelul suplimentar fără legătură.
3. La [00:20:40](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1240s), eliminați referința inutilă către Animal.
4. La [00:20:47](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1247s), păstrați Speak, FlapWings virtual din Bird și suprascrierea FlapWings din Sparrow.

Stare simplificată din videoclip:

```csharp
class Bird
{
    public virtual void Speak()
    {
        this.FlapWings();
    }
    public virtual void FlapWings() => Console.WriteLine("bird flaps");
}
class Sparrow : Bird
{
    public override void FlapWings() => Console.WriteLine("sparrow flaps");
}

// Instrucțiuni la locul apelului:
Bird bird = new Sparrow();
bird.Speak();                // corpul moștenit apelează Sparrow.FlapWings
```

La [00:20:59](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1259s), `this.FlapWings()` este un apel virtual pe receptorul curent. Tipul său static este Bird, dar tipul receptorului de la timpul execuției este Sparrow, deci rulează suprascrierea lui Sparrow. La [00:21:08](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1268s), eliminarea lui `this.` nu schimbă nimic: apelul necalificat de instanță folosește în continuare acel receptor și acea operație virtuală.

La [00:21:13](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1273s), `base` nu este o modalitate prin care Bird să își selecteze propriul corp: din Bird ar desemna clasa de bază a lui Bird. Apelurile obișnuite pe `this` nu îl forțează deci pe Bird să folosească corpul său implicit FlapWings atunci când receptorul este un Sparrow.

Soluția de la [00:21:22](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1282s)–[00:21:33](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1293s) extrage logica implicită într-un **ajutor privat nevirtual**. Accesul privat o ține în interiorul lui Bird; comportamentul nevirtual fixează ținta apelului.

Stare modificată simplificată:

```csharp
class Bird
{
    private void FlapWingsDefault() => Console.WriteLine("bird flaps");

    public virtual void FlapWings() => FlapWingsDefault();
    public virtual void Speak() => FlapWingsDefault();
}
class Sparrow : Bird
{
    public override void FlapWings() => Console.WriteLine("sparrow flaps");
}
```

Speak poate rula acum deliberat logica implicită a lui Bird. Alegeți un apel virtual obișnuit când descendenții trebuie să personalizeze comportamentul, iar acest ajutor când logica implicită trebuie să ruleze.

<details>
<summary>Dacă Bird.Speak apelează FlapWings fără this, participă și Sparrow?</summary>

Da. Un apel de instanță necalificat furnizează implicit același receptor. Eliminarea lui this nu transformă dispecerizarea virtuală într-un apel calificat cu base sau nevirtual.

</details>

## Moștenirea trebuie să însemne este-un

La [00:21:46](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1306s), **moștenirea** înseamnă **este-un**, nu doar „a fost creat de” sau „are un cod oarecum asemănător”. Dacă A derivă din B, orice A trebuie să poată fi folosit drept B. Pentru Bird și Animal, orice pasăre este un animal. [00:21:55](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1315s)

Recapitularea A/B/C de la [00:22:03](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1323s)–[00:22:25](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1345s) avertizează împotriva inventării unei baze comune doar pentru a reutiliza cod. O instanță derivată poartă și ea membrii moșteniți, ceea ce expune o nepotrivire dacă nu este logic o instanță a bazei.

La [00:22:35](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1355s), o poveste despre originea familială bunică → mamă → fiică sugerează ierarhia greșită de programare. Corecția de la [00:22:59](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1379s) este că `Daughter : Mother` ar afirma că orice fiică este și mamă, nu că o mamă a născut-o. Inversarea categoriilor ilustrează relația intenționată: o bunică este o mamă, iar o mamă este fiica cuiva. [00:23:14](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1394s) Aceasta este o analogie pedagogică; un model real de familie ar necesita decizii proprii de domeniu.

La [00:23:36](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1416s)–[00:24:11](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1451s), un exemplu din jocuri are un lanț mai potrivit. Un **identificator** distinge o entitate pentru **serializare**, adică stocarea sau transferul datelor sale. Un monstru este o entitate; un orc este un monstru.

Ierarhie simplificată din starea videoclipului:

```csharp
class Entity
{
    public int Id;
}
class Monster : Entity
{
    public int Health;
    public int Attack;
}
class Orc : Monster { }
```

Prin urmare, orice Orc are identitate de entitate și viață și atac de monstru. Acestea sunt părți moștenite ale aceluiași obiect, iar relația de tip transmite că un orc poate fi folosit oriunde este așteptat un monstru sau o entitate.

## Reprezentarea proprietăților prin date și compoziție

La [00:24:17](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1457s), considerați Animal → FlyingAnimal → BlackFlyingAnimal → Hawk. Codificarea proprietăților independente ca subclase devine fragilă: un urs negru care nu zboară nu încape sub BlackFlyingAnimal. [00:24:40](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1480s)

La [00:24:53](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1493s), adăugarea lui BlackAnimal sugerează extragerea comportamentului negru duplicat în acea clasă, dar atunci BlackFlyingAnimal ar avea nevoie atât de BlackAnimal, cât și de FlyingAnimal ca părinți de clasă. C# permite o singură clasă de bază directă. Interfețele implementate multiple, introduse mai târziu, nu elimină această restricție. [00:25:10](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1510s)–[00:25:13](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1513s)

La [00:25:17](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1517s)–[00:25:24](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1524s), **compoziția** este alternativa: reprezentați proprietățile care variază independent ca date sau asamblați obiecte din componente, în loc să cereți o subclasă pentru fiecare combinație. Demonstrația simplă pune culoarea și un indicator de zbor direct pe Animal.

Stare modificată simplificată din videoclip la [00:25:31](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1531s):

```csharp
class Animal
{
    public required string Name;
    public required string Colour;
    public bool CanFly;
}

// Instrucțiuni la locul apelului:
var bear = new Animal { Name = "bear", Colour = "black", CanFly = false };
var hawk = new Animal { Name = "hawk", Colour = "black", CanFly = true };
```

La [00:25:44](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1544s), valorile disting ursul negru de șoimul negru fără subclase pentru fiecare proprietate. La [00:26:02](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1562s), recomandarea este să puneți asemenea proprietăți în date sau în obiecte componente compuse. Culoarea și abilitatea de a zbura variază independent; o ierarhie de clase trebuie să exprime o relație de tip semnificativă.

La [00:26:14](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1574s)–[00:26:24](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1584s), recapitularea continuă observă că un model care distinge consecvent speciile de animale ar putea introduce Mammal, în loc să folosească Animal ca însemnând în mod specific mamifer. Aceasta conduce la întrebarea următoare: categoria generică Animal ar trebui oare să fie construibilă direct?

## Clase abstracte și sealed

La [00:26:32](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1592s), clasa Animal obișnuită poate fi încă instanțiată. Restricția dorită la [00:26:40](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1600s)–[00:26:47](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1607s) este să păstrăm Animal ca tip de referință și categorie de bază, construind doar specii concrete precum păsările sau mamiferele.

Faceți-o **clasă abstractă**, o clasă care nu poate fi instanțiată direct. Stare modificată simplificată la [00:27:03](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1623s):

```csharp
abstract class Animal
{
    public virtual void Speak() => Console.WriteLine("???");
}
class Bird : Animal { }

// Instrucțiuni la locul apelului:
// var animal = new Animal(); // eroare la compilare
Animal animal = new Bird();   // referință legală cu tip de bază
```

Exemplul de construire directă din înregistrare nu mai compilează. O clasă abstractă poate conține în continuare câmpuri și metode implementate; declararea clasei abstracte nu face automat fiecare metodă abstractă.

La [00:27:15](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1635s), restricția opusă a moștenirii este **`sealed`**, adică o clasă fără subclase. La [00:27:25](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1645s)–[00:27:40](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1660s), exemplul Sparrow motivează folosirea unor instanțe obișnuite de Bird cu un nume, în locul unei altei clase.

Stare independentă simplificată cu clasă sealed, din videoclip:

```csharp
sealed class Bird : Animal
{
    public required string Name;
    public override void Speak() => Console.WriteLine(Name);
}

// var bird = new Bird { Name = "sparrow" };
// class Sparrow : Bird { }  // eroare la compilare
```

La [00:27:59](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1679s), marcarea ca sealed comunică faptul că clasa este finală și intenționată pentru folosire directă. La [00:28:12](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1692s)–[00:28:44](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1724s), faptul că un receptor Bird nu poate aparține unei subclase a lui Bird poate permite **devirtualizarea**: un compilator de la timpul execuției poate alege un apel direct pentru că ținta este cunoscută. Aceasta este o posibilitate de optimizare, nu o garanție C# că fiecare asemenea apel folosește o anumită instrucțiune de mașină. Comportamentul observabil al metodei rămâne același.

La [00:28:44](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1724s), o metodă nevirtuală are, în mod similar, o implementare fixă; la [00:28:46](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1726s), o altă cale de a ajunge la optimizare este amânată. Secțiunea următoare restrânge marcarea sealed la o singură operație suprascrisă.

## Marcarea sealed a unei suprasceri

La [00:28:56](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1736s), marcați drept sealed o metodă suprascrisă, în loc să sigilați întreaga clasă. O **suprascriere sealed** păstrează contractul virtual moștenit, dar împiedică suprascrieri viitoare ale acelei operații.

Stare simplificată din videoclip:

```csharp
class Animal
{
    public virtual void Speak() => Console.WriteLine("???");
}
class Bird : Animal
{
    public sealed override void Speak() => Console.WriteLine("chirp");
}
class Sparrow : Bird
{
    // public override void Speak() { } // eroare: membrul moștenit este sealed
}
```

La [00:29:03](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1743s), încercarea de a suprascrie membrul sealed produce eroarea de compilator afișată, anume că nu poate fi suprascris fiindcă este sealed. Sintaxa legală este `sealed override`, nu o nouă declarație `sealed virtual`. [Referință C# pentru sealed](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed).

La [00:29:16](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1756s)–[00:29:25](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1765s), modelul păstrează implementarea lui Bird în poziția moștenită pentru Bird și subclasele sale. Un apel cu tip Bird are deci o țintă de suprascriere fixă și poate fi optimizat direct.

La [00:29:48](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1788s), un apel cu tip Animal este în continuare o operație virtuală: un alt subtip Animal ar putea implementa Speak diferit. Un anumit compilator de la timpul execuției poate optimiza cazurile cunoscute, dar marcarea sealed a unei ramuri nu elimină polimorfismul din contractul lui Animal.

## Impunerea comportamentului cu metode abstracte

La [00:30:05](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1805s), faceți însuși operația **abstractă**: clasa declarantă specifică metoda, dar nu furnizează niciun corp. O metodă de instanță abstractă participă la contractul virtual fără a cere un modificator `virtual` suplimentar.

Stare simplificată din videoclip:

```csharp
abstract class Animal
{
    public abstract void Speak();
}
class Bird : Animal
{
    public override void Speak() => Console.WriteLine("chirp");
}
class Sparrow : Bird { }     // moștenește implementarea lui Bird
```

La [00:30:16](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1816s)–[00:30:57](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1857s), un obiect nu poate avea un Animal abstract drept tip exact de la timpul execuției, deoarece Animal nu poate fi construit. Receptorul este un descendent concret. În modelul tabelului, Speak abstract al lui Animal declară poziția fără un corp implicit executabil.

Afirmația că tabelul unei clase abstracte este „nefolosit” privește dispecerizarea pe o instanță al cărei tip exact de la timpul execuției este acea clasă abstractă. Contractele, membrii implementați și informațiile de tip ale acesteia contează în continuare pentru descendenți; `abstract` nu le șterge.

La [00:30:57](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1857s)–[00:31:14](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1874s), Bird trebuie să furnizeze o implementare pentru a fi concretă. Ștergerea suprascrierii produce o eroare la compilare. O subclasă abstractă intermediară poate lăsa operația abstractă; fiecare descendent concret trebuie să aibă o implementare utilizabilă.

La [00:31:25](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1885s)–[00:31:48](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1908s), Sparrow nu trebuie să suprascrie din nou: implementarea lui Bird satisfăce deja cerința, iar Sparrow o moștenește. Condiția importantă este existența unei implementări undeva pe drumul său moștenit, nu o declarație repetată în fiecare descendent.

## Parametri, valori de returnare și suprasarcini

La [00:32:00](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1920s), metodele virtuale pot primi parametri și pot întoarce valori; `void Speak()` a fost doar demonstrația cea mai simplă. O **suprascriere** trebuie să corespundă semnăturii de parametri și accesibilității operației moștenite. Exemplele păstrează același tip de returnare; C# modern permite și anumite tipuri de returnare covariante, pe care acest videoclip nu le predă. [Referință C# pentru override](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/override).

La [00:32:23](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1943s), **suprasarcinile** folosesc același nume cu liste diferite de parametri. Fiecare suprasarcină virtuală este o operație independentă în modelul tabelului.

Stare simplificată din videoclip:

```csharp
class Animal
{
    public virtual void Speak() => Console.WriteLine("???");
    public virtual void Speak(string message) => Console.WriteLine(message);
}
class Bird : Animal
{
    public override void Speak() => Console.WriteLine("chirp");
    public override void Speak(string message) => Console.WriteLine("bird: " + message);
}

// Instrucțiuni la locul apelului:
Animal animal = new Bird();
animal.Speak();              // operația fără parametri, implementarea din Bird
animal.Speak("hello");       // operația cu șir, implementarea din Bird
```

La [00:32:29](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1949s) și [00:32:57](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1977s), lista de argumente distinge aceste operații. Pentru aceste apeluri obișnuite, tipate static, **selecția suprasarcinii se întâmplă mai întâi la compilare**; apoi dispecerizarea virtuală alege o suprascriere a semnăturii selectate. Nu este o parcurgere la timpul execuției care alege orice suprasarcină cu același nume după intrarea în tabel. [Specificația C# pentru rezolvarea suprasarcinilor](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/expressions#1264-overload-resolution).

## Introducerea unui contract de interfață

La [00:33:14](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1994s), o **interfață** seamănă cu o clasă abstractă într-un aspect important: specifică operații pe care implementările trebuie să le ofere. La [00:33:18](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1998s), introduceți o operație de aripi separat de contractul Speak al lui Animal. Convenția C# prefixează numele interfețelor cu `I`; prefixul este util, dar nu impus de limbaj. [00:33:19](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1999s)–[00:33:32](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2012s)

Stare inițială simplificată cu interfață, din videoclip, la [00:33:37](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2017s):

```csharp
abstract class Animal
{
    public abstract void Speak();
}
interface IFlapWings
{
    void FlapWings();
}
class Bird : Animal, IFlapWings
{
    public override void Speak() => Console.WriteLine("chirp");
    // FlapWings lipsește inițial: această clasă nu compilează.
}
```

Lista după două puncte exprimă două fapte distincte: Bird **moștenește** Animal și **implementează** IFlapWings. Nu îi dă lui Bird doi părinți de clasă. Diagrama lecției folosește un tabel de ierarhie de clase pentru metodele virtuale de clasă și o mapare separată de dispecerizare pentru interfețe. [00:33:44](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2024s)

La [00:33:58](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2038s), sintaxa apelului rămâne cea obișnuită, cu punct și paranteze. La [00:34:03](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2043s), o declarație care se termină în `;` nu are corp. Pentru contractul simplu de instanță prezentat, membrul interfeței este implicit public și abstract; scrierea explicită a lui `public` este legală, dar inutilă. [00:34:18](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2058s)–[00:34:47](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2087s)

O interfață nu poate fi instanțiată. [00:34:24](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2064s) Interfețele moderne pot declara și corpuri implicite și alte forme de membri; comportamentul de implementare implicită este amânat explicit la [00:34:11](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2051s). Distincția „fără câmpuri” a lecției înseamnă fără **câmpuri de instanță** pentru starea per obiect, nu că interfețele moderne nu ar putea declara niciodată vreo stare statică. [Referință C# pentru interfață](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface).

La [00:34:51](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2091s), compilatorul raportează că Bird nu a furnizat FlapWings. Adăugați o implementare publică potrivită la [00:35:01](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2101s):

```csharp
class Bird : Animal, IFlapWings
{
    public override void Speak() => Console.WriteLine("chirp");
    public void FlapWings() => Console.WriteLine("bird flaps");
}
```

Aceasta este **implementare implicită a interfeței**: metoda publică a clasei corespunde numelui, parametrilor și tipului de returnare ai operației din interfață. `override` este folosit pentru Speak pentru că Animal declară acel contract virtual; FlapWings nu are nevoie de el doar pentru a satisface o interfață.

La [00:35:12](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2112s)–[00:35:45](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2145s), demonstrația distinge moștenirea de clasă de apartenența la interfață și permite implementarea alături de o bază abstractă sau fără o clasă de bază proprie. Interfața contribuie cu un contract de comportament, nu cu stare de instanță moștenită.

La [00:35:52](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2152s), după eliminarea operației din clasa de bază într-o variantă, păstrarea lui `override` pe o metodă fără un membru de clasă suprascris corespunzător produce o eroare de compilator. Doar apartenența la o interfață nu este o suprascriere de metodă de clasă. La [00:36:10](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2170s), clasa implementantă poate declara la rândul ei acea metodă `virtual`; apoi descendenții ei o pot suprascrie în mod normal.

## Un singur obiect privit printr-o interfață

La [00:36:16](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2176s)–[00:36:36](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2196s), implementarea lui IFlapWings înseamnă că Bird poate fi folosit drept valoare IFlapWings. **Conversia ascendentă către o interfață** memorează o referință către același obiect; nu construiește un obiect de interfață și nu copiază pasărea.

Stare simplificată din videoclip:

```csharp
Bird bird = new Bird();
IFlapWings wings = bird;
wings.FlapWings();

// wings.Speak();             // eroare: nu există în IFlapWings
// new IFlapWings();          // eroare: o interfață nu este construibilă
```

La [00:36:36](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2196s), variabila cu tip de interfață este creată sau atribuită și apoi folosită pentru apel. Interpretați aceasta ca o declarație de referință precum cea de mai sus, nu ca un constructor numit `FlapWings.FlapWings()`.

La [00:36:29](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2189s) și [00:36:54](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2214s), **tipul static de interfață** limitează operațiile disponibile la contractul său. Obiectul Bird conține în continuare celelalte comportamente și date, dar o expresie IFlapWings nu expune Speak.

La [00:37:01](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2221s)–[00:37:15](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2235s), desenul memoriei copiază din nou doar o referință de obiect. La [00:37:15](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2235s)–[00:38:07](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2287s), obiectul are un singur tip de la timpul execuției, Bird, ale cărui informații de dispecerizare conțin mapări distincte de clasă și de interfață:

| Contract din acest exemplu | Operație | Implementare concretă |
| --- | --- | --- |
| Ierarhia de clase Animal | Speak | Bird.Speak |
| Interfața IFlapWings | FlapWings | Bird.FlapWings |

Fiecare contract prezentat conține o singură operație, deci fiecare tabel ilustrat are o singură poziție. Poziția lui Speak se află în maparea ierarhiei de clase, iar poziția lui FlapWings în maparea interfeței. Tabelul IFlapWings nu capătă astfel o intrare Speak.

La [00:38:07](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2287s)–[00:38:12](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2292s), fiecare intrare identifică cod de implementare executabil. Un singur obiect poate oferi ambele contracte; referințe separate nu implică copii separate ale câmpurilor sale.

## Urmărirea unui apel de interfață

Analiza apelului de interfață de la [00:38:19](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2299s)–[00:38:28](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2308s) adaugă un pas de selecție modelului anterior de apel virtual: alegeți maparea de dispecerizare pentru interfața cerută, apoi alegeți operația ei.

Apel simplificat din videoclip, analizat:

```csharp
Bird bird = new Bird();
IFlapWings wings = bird;
wings.FlapWings();
```

La [00:38:35](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2315s), un apel virtual de clasă folosește ierarhia de clase în care a fost introdusă operația sa. La [00:38:51](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2331s), atribuirea variabilei de interfață copiază în continuare aceeași referință. La [00:38:59](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2339s)–[00:39:25](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2365s), urmăriți un apel de interfață astfel:

1. Citiți referința memorată în `wings`.
2. Ajungeți la obiect și la informațiile sale de tip de la timpul execuției.
3. Folosiți **contractul static de interfață**, IFlapWings, pentru a identifica maparea de interfață necesară acelui tip de la timpul execuției.
4. Localizați operația FlapWings din mapare.
5. Invocați implementarea sa concretă. [00:40:22](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2422s)
6. Când invocarea revine, continuați cu următoarea instrucțiune a apelantului. [00:40:31](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2431s)

La [00:39:32](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2372s)–[00:39:59](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2399s), motivul mapării de la timpul execuției este că o variabilă IFlapWings poate desemna orice clasă implementantă compatibilă, nu doar Bird. Asemenea clase pot implementa mulțimi diferite de interfețe; desenul nu poate presupune o poziție fixă unică în tabel în fiecare tip de obiect.

La [00:39:59](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2399s), o clasă poate implementa multe interfețe moștenind o singură clasă. Nu poate moșteni un număr arbitrar de clase. La [00:40:14](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2414s), executarea apelului rezolvă operația de interfață cerută în acest model conceptual. Runtime-urile reale pot memora în cache sau optimiza acea rezolvare; lecția nu promite o căutare liniară literală și nici un tabel alocat independent la fiecare apel.

Adresa de cod a poziției este folosită pentru a invoca metoda. O metodă nu întoarce ca rezultat C# adresa următoarei instrucțiuni a apelantului; mecanismul de apel/retur al mașinii reia apelantul. Distincția contează pentru formularea din transcriere despre continuarea „de la o adresă”.

## Folosirea a două interfețe fără o clasă de bază proprie

La [00:40:34](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2434s), extindeți exemplul la două interfețe. Înlocuiți contractul Animal cu **ISpeak**, eliminați `override`-ul devenit neaplicabil și schimbați tipul de la locul apelului. [00:40:39](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2439s)–[00:41:02](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2462s)

Stare modificată simplificată din videoclip:

```csharp
interface ISpeak
{
    void Speak();
}
interface IFlapWings
{
    void FlapWings();
}
class Bird : ISpeak, IFlapWings
{
    public void Speak() => Console.WriteLine("chirp");
    public void FlapWings() => Console.WriteLine("bird flaps");
}

// Instrucțiuni la locul apelului:
Bird bird = new Bird();
ISpeak speaker = bird;
IFlapWings wings = bird;
speaker.Speak();
wings.FlapWings();
```

La [00:40:47](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2447s), Bird nu are nicio clasă de bază proprie declarată explicit; derivă totuși implicit din `object`. Niciuna dintre interfețe nu furnizează câmpuri de instanță. Aici interfețele seamănă cu contracte abstracte de comportament, dar nu sunt interschimbabile cu clase abstracte care poartă și stare de instanță comună.

La [00:41:07](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2467s)–[00:41:32](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2492s), apelul Speak folosește maparea lui ISpeak în locul fostei mapări a ierarhiei Animal. Tipul efectiv al receptorului selectează implementarea concretă, iar apelantul reia la revenire.

La [00:41:32](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2492s)–[00:41:50](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2510s), compatibilitatea interfeței urmează ideea cunoscută de verificare a tipurilor: dacă Bird implementează ISpeak, un Bird poate fi folosit acolo unde este așteptat un ISpeak. Conversiile ascendente cunoscute sunt implicite; o **conversie explicită** cere explicit privirea unei valori compatibile ca fiind de alt tip.

Stare simplificată de conversie și tipar, din videoclip, la [00:42:03](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2523s):

```csharp
object value = new Bird();
ISpeak speaker = (ISpeak)value;   // conversie explicită
speaker.Speak();

if (value is ISpeak checkedSpeaker)
    checkedSpeaker.Speak();        // tiparul verifică și declară variabila
```

**Tiparul de tip** combină un test de compatibilitate cu o declarație de variabilă. La [00:42:08](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2528s), dezvoltarea suplimentară a tipării interfețelor se oprește pentru că ideea relevantă este același mecanism de compatibilitate deja folosit pentru moștenirea de clasă.

## Metodele virtuale de clasă pot implementa contracte de interfață

La [00:42:29](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2549s), faceți virtuală metoda Speak a lui Bird, care implementează implicit interfața, și adăugați o suprascriere într-o subclasă. La [00:42:42](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2562s), construiți subclasa.

Stare simplificată din videoclip:

```csharp
class Bird : ISpeak, IFlapWings
{
    public virtual void Speak() => Console.WriteLine("bird");
    public void FlapWings() => Console.WriteLine("bird flaps");
}
class Sparrow : Bird
{
    public override void Speak() => Console.WriteLine("sparrow");
}

// Instrucțiuni la locul apelului:
ISpeak speaker = new Sparrow();
speaker.Speak();                    // sparrow
Bird bird = new Sparrow();
bird.Speak();                       // sparrow
```

La [00:42:46](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2566s), rularea înregistrată afișează implementarea suprascrisă prin referința de interfață. „Suprasărcinată” într-o descriere a acestui rezultat trebuie citit drept **suprascrisă**: schimbarea demonstrată înlocuiește o implementare virtuală moștenită, în loc să adauge o listă diferită de parametri.

La [00:43:00](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2580s)–[00:43:18](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2598s), modelul include acum atât operația Speak a ierarhiei de clase, cât și maparea sa ISpeak. Implementarea publică virtuală a lui Bird satisface interfața; suprascrierea lui Sparrow este atinsă pe oricare dintre căi. Aceasta nu este o cerință de a scrie o a doua implementare de interfață în Sparrow.

La [00:43:30](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2610s), două interfețe pot declara operații identice. La [00:44:00](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2640s), o singură metodă publică potrivită poate implementa ambele:

```csharp
interface ISpeak1
{
    void Speak();
}
class Bird : ISpeak, ISpeak1
{
    public virtual void Speak() => Console.WriteLine("bird");
}
class Sparrow : Bird
{
    public override void Speak() => Console.WriteLine("sparrow");
}
```

La [00:44:12](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2652s), ambele mapări de interfață pot conduce la aceeași cale de implementare virtuală. Înlocuirea acelei operații de clasă cu o suprascriere schimbă implementarea observată pentru ambele contracte din acest exemplu.

## Separarea explicită a semnăturilor identice

La [00:44:14](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2654s)–[00:44:17](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2657s), noul obiectiv este o ieșire diferită pentru același nume de operație, în funcție de faptul că același obiect este privit ca ISpeak sau ca ISpeak1.

Prima încercare de la [00:44:19](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2659s) nu compilează. Stare simplificată nereușită, din videoclip:

```csharp
class Bird : ISpeak, ISpeak1
{
    public void Speak() => Console.WriteLine("Speak");
    public void Speak() => Console.WriteLine("Speak1"); // semnătură duplicată
}
```

Două metode cu nume și liste de parametri identice nu pot fi deosebite doar prin corpuri. Nu sunt două suprasarcini.

La [00:45:06](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2706s)–[00:45:13](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2713s), folosiți **implementarea explicită a interfeței**: eliminați `public` și calificați fiecare nume de metodă cu interfața sa.

Stare corectată simplificată din videoclip:

```csharp
class Bird : ISpeak, ISpeak1
{
    void ISpeak.Speak() => Console.WriteLine("Speak");
    void ISpeak1.Speak() => Console.WriteLine("Speak1");
}

// Același obiect, două vederi prin interfețe:
var bird = new Bird();
ISpeak first = bird;
ISpeak1 second = bird;
first.Speak();                       // Speak
second.Speak();                      // Speak1
// bird.Speak();                     // aici nu există un membru public Speak
```

La [00:45:20](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2720s), ieșirile observate sunt Speak, respectiv Speak1. La [00:45:30](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2730s), fiecare operație de interfață are propria mapare de implementare. Implementările explicite sunt accesibile prin interfața corespunzătoare, nu ca metode publice pe Bird. [Ghid C# pentru implementarea explicită a interfeței](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/explicit-interface-implementation).

La [00:45:50](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2750s), o interfață poate declara mai multe metode, fiecare cu propria intrare de operație în modelul de tabel al lecției.

<details>
<summary>Cele două variabile de interfață de mai sus necesită două obiecte Bird?</summary>

Nu. Sunt două vederi asupra aceleiași referințe către Bird. Contractul de interfață selectat determină ce operație Speak implementată explicit este invocată.

</details>

## Limitele lecției

Lista finală de domeniu este explicită:

- La [00:46:00](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2760s), interfețele care moștenesc alte interfețe nu sunt explicate.
- La [00:46:05](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2765s), metodele virtuale pot fi `protected` în loc de publice. **Accesul protejat** permite folosirea din clasa declarantă și din descendenți; această posibilitate este menționată, dar nu demonstrată.
- La [00:46:12](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2772s), `base` în constructori este amânat la alt videoclip.
- La [00:46:21](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2781s), proprietățile virtuale și întrebările asociate despre câmpurile de sprijin sunt lăsate pentru citire independentă. O **proprietate** expune accesul la membri prin accesori; un **câmp de sprijin** este o stocare folosită de un accessor și nu orice proprietate are unul.
- La [00:46:24](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2784s)–[00:46:35](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2795s), proprietățile abstracte, proprietățile de interfață și celelalte cinci întrebări sunt repartizate pentru studiu separat, nu sunt dezvoltate aici.
- Alternativa anterioară de optimizare prin apel direct de la [00:28:46](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1726s) și corpurile implicite ale interfețelor de la [00:34:11](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2051s) rămân, de asemenea, în afara materialului demonstrat.

Aceste excluderi nu împiedică aplicarea exemplelor bazate pe metode de mai jos. Orice studiu al caracteristicilor amânate este studiu suplimentar, dincolo de acest videoclip.

## Refactorizarea funcțiilor meniului în obiecte de acțiune

La [00:46:45](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2805s), contractul de acțiune al meniului ar fi putut fi o interfață sau o clasă abstractă. Limbajul nu impune o singură proiectare. Un **obiect de acțiune** este o instanță care expune comportament printr-o operație comună, purtând opțional date împreună cu ea.

Proiectarea inițială transmite un comportament invocabil unui executor comun. Stare simplificată de meniu, reconstruită pentru comparația de la [00:47:14](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2834s):

```csharp
static void Greeting(string name) => Console.WriteLine("Hello, " + name);

static void ExecuteAction(System.Action<string> action, string name)
{
    action(name);
}

// La un loc de apel:
ExecuteAction(Greeting, "Ada");
```

Aici **delegate-ul** este o valoare invocabilă, iar `Greeting` este un **grup de metode** convertit într-un delegate compatibil. Executorul nu trebuie să știe care metodă de salut anume a fost furnizată.

La [00:46:48](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2808s)–[00:47:08](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2828s), înlocuiți acel parametru invocabil cu o **clasă abstractă de acțiune** a cărei metodă Execute este o operație abstractă de instanță. Reconstrucția intermediară următoare păstrează parametrul de nume până la următoarea refactorizare:

```csharp
abstract class Action
{
    public abstract void Execute(string name);
}
class GreetingAction : Action
{
    public override void Execute(string name)
        => Console.WriteLine("Hello, " + name);
}

// În clasa care conține meniul:
static void ExecuteAction(Action action, string name)
{
    action.Execute(name);
}
```

`Action` din această demonstrație este o clasă personalizată; `System.Action` este tipul delegate din framework folosit în reconstrucția anterioară.

La [00:46:55](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2815s)–[00:47:14](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2834s), Action abstract nu poate fi construit direct, deci executorul primește un descendent concret. El cunoaște **tipul static** Action, în timp ce obiectul își păstrează **tipul de la timpul execuției**. La [00:47:39](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2859s), creați GreetingAction și dați-i o suprascriere nestatică, deoarece Execute este un contract de instanță.

La [00:47:43](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2863s)–[00:47:50](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2870s), apoi din nou la [00:48:35](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2915s)–[00:48:42](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2922s), `action.Execute(...)` selectează suprascrierea concretă a receptorului. Furnizarea unui GreetingAction ajunge la prima instrucțiune a acelei metode. Mecanismul de dispecerizare este același urmărit deja pentru Animal și Bird.

La [00:47:14](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2834s)–[00:47:33](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2853s), comparația privește împachetarea comportamentului: un delegate simplu oferă o singură semnătură invocabilă; un contract de clasă poate oferi mai multe operații cu nume distincte printr-o singură referință de obiect. Imaginea lecției cu o singură adresă de funcție este o simplificare. Delegate-urile pot referi metode de instanță cu date țintă și pot combina mai multe intrări de invocare. Aceste capabilități nu transformă un delegate într-un contract de clasă cu mai multe metode. [Ghid C# pentru delegate](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/).

Niciuna dintre reprezentări nu este universal mai bună. Schimbarea următoare arată de ce un obiect care poartă configurare cu nume explicit poate fi potrivit pentru acest meniu.

## Memorarea configurației de salut în acțiune

La [00:48:48](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2928s), GreetingAction câștigă informații asociate acelui salut anume. Puneți numele într-un **câmp**, date memorate în obiectul de acțiune, în loc să îl transmiteți la fiecare execuție.

Aplicați schimbarea în ordine:

1. La [00:49:00](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2940s), eliminați argumentul de nume din Execute și citiți câmpul memorat.
2. La [00:49:10](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2950s), ștergeți funcția de salut separată.
3. La [00:49:16](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2956s), înlocuiți valoarea de grup de metode cu un GreetingAction construit.
4. Furnizați numele său obligatoriu la construire. [00:49:16](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2956s)–[00:49:23](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2963s)

Stare modificată simplificată din videoclip:

```csharp
abstract class Action
{
    public abstract void Execute();
}
class GreetingAction : Action
{
    public required string Name;
    public override void Execute() => Console.WriteLine("Hello, " + Name);
}

// În clasa care conține meniul:
static void ExecuteAction(Action action) => action.Execute();

// La un loc de apel:
ExecuteAction(new GreetingAction { Name = "Ada" });
```

**Membrul obligatoriu** face ca construirea să furnizeze Name; **inițializatorul de obiect** îl stabilește pe instanța nouă. Un nume este memorat acum alături de alegerea implementării, deci nu mai este necesar un argument de nume la execuție.

La [00:49:23](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2963s)–[00:49:34](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2974s), referința obiectului împachetează atât comportament, cât și configurație. Tipul de la timpul execuției conduce la implementarea lui Execute; câmpurile obiectului furnizează datele acelei implementări. Fiecare acțiune derivată își poate adăuga propria configurație fără a schimba contractul de bază abstract.

La [00:49:47](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2987s), aceeași conversie este în continuare necesară pentru celelalte acțiuni ale meniului. Greeting este primul caz tratat, nu refactorizarea completă a meniului.

## Finalizarea celorlalte clase de acțiune

La [00:49:59](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2999s), convertiți Addition dintr-o funcție în AdditionAction. Mutați ajutorul de citire într-o metodă privată, păstrând implementarea intrării în interiorul acțiunii. La [00:50:11](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3011s), extrageți funcția fără operație în NothingAction.

Stare modificată simplificată din videoclip:

```csharp
class AdditionAction : Action
{
    private static int Read() => int.Parse(Console.ReadLine()!);

    public override void Execute()
    {
        int first = Read();
        int second = Read();
        Console.WriteLine(first + second);
    }
}
class NothingAction : Action
{
    public override void Execute() { }
}

// Locuri de apel actualizate:
ExecuteAction(new AdditionAction());
ExecuteAction(new NothingAction());
```

Funcția Read de mai sus, pe un singur rând, este scurtată intenționat pentru a pune accent pe plasarea metodei; nu înseamnă că înregistrarea a înlocuit ajutorul său existent de validare a intrării cu exact acest corp. Păstrați comportamentul de validare al ajutorului original când aplicați refactorizarea unui meniu real. Semnul `!` „anulează-null” din exemplu doar suprimă avertismentele analizei de nulabilitate; nu face sigură o intrare lipsă sau invalidă.

La [00:50:23](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3023s)–[00:50:35](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3035s), **Execute abstract** obligă chiar și NothingAction să furnizeze un corp. Suprascrierea sa explicită goală înseamnă „această acțiune intenționat nu face nimic”. O clasă derivată concretă nu poate doar să omită metoda.

La [00:50:35](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3035s)–[00:50:45](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3045s), înlocuirea declarației abstracte cu un corp virtual gol ar schimba acea protecție: o suprascriere uitată ar moșteni în tăcere comportamentul implicit de inoperație. `abstract` face obligația vizibilă compilatorului.

La [00:50:48](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3048s), locurile de apel construiesc acum AdditionAction și NothingAction, în loc să numească vechile grupuri de funcții. Executorul rămâne uniform: primește Action și invocă Execute polimorfic.

## Reutilizarea unei acțiuni fără stare ca singleton

La [00:50:56](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3056s), AdditionAction nu are configurație sau câmpuri per instanță și nu schimbă starea obiectului între execuții. Citirea unei intrări noi de la consolă nu impune singură o instanță nouă de acțiune.

La [00:50:56](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3056s)–[00:51:10](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3070s), un **singleton** expune o singură instanță partajată, în loc să construiască o instanță nouă pentru fiecare selecție din meniu. Aceasta poate evita alocări repetate ale unui obiect mic, când nu există stare de instanță distinctă de păstrat. GreetingAction, al cărui Name diferă între saluti, motivează o altă alegere.

Construiți singletonul în ordinea demonstrată:

1. La [00:51:24](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3084s), restricționați construirea din exterior cu un **constructor privat**.
2. La [00:51:59](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3119s), încercarea de a construi acțiunea din exterior, chiar și cu un inițializator de obiect, eșuează: construirea are în continuare nevoie de acces la acel constructor.
3. La [00:52:20](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3140s), adăugați un **câmp static**, asociat tipului și nu fiecărei instanțe, care ține obiectul partajat.
4. La [00:52:27](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3147s), folosiți acel câmp la locul apelului.
5. La [00:52:34](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3154s), adăugați **readonly** pentru a împiedica înlocuirea obișnuită ulterioară a referinței partajate.

Stare finală simplificată a singletonului, din videoclip:

```csharp
class AdditionAction : Action
{
    private AdditionAction() { }

    public static readonly AdditionAction Instance = new AdditionAction();

    private static int Read() => int.Parse(Console.ReadLine()!);

    public override void Execute()
    {
        int first = Read();
        int second = Read();
        Console.WriteLine(first + second);
    }
}

// La un loc de apel:
ExecuteAction(AdditionAction.Instance);
// new AdditionAction();                 // eroare: constructor privat
// AdditionAction.Instance = null!;      // eroare: câmp readonly
```

Constructorul privat este accesibil inițializatorului de câmp din interiorul lui AdditionAction, deci acel inițializator poate crea obiectul. Apelanții din exterior reutilizează referința rezultată.

La [00:52:36](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3156s)–[00:52:55](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3175s), încercarea demonstrată de a reatribui câmpul readonly nu compilează. Regula exactă este mai largă decât „atribuit exact o dată la declarare”: câmpurile readonly pot fi atribuite și în contextul constructorului tipului declarant, inclusiv unui constructor static pentru un câmp static. Ele nu pot fi reatribuite de codul apelant ulterior obișnuit. `readonly` fixează referința aici; nu ar împiedica modificările câmpurilor din interiorul unui obiect partajat mutabil. [Referință C# pentru readonly](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly).

<details>
<summary>De ce partajăm AdditionAction, dar construim GreetingAction cu un nume?</summary>

AdditionAction nu are configurație per instanță în această demonstrație, deci toți apelanții îl pot reutiliza. GreetingAction memorează un anumit Name, deci saluturile distincte pot avea nevoie de obiecte configurate distincte. Decizia urmează datele și durata de viață a obiectului, nu doar faptul că ambele expun Execute.

</details>

## Istoricul modificărilor de cod

Aceste linkuri video stabile identifică **stările relevante din videoclip**, de la cea mai veche la cea mai recentă. Niciun commit de sursă inspectat nu conține codul exact al demonstrației; fragmentele reconstruite de mai sus nu sunt reprezentate drept versiuni comitate.

- [00:00:10](https://www.youtube.com/watch?v=82vaxb6o_rg&t=10s) — Animal și Bird încep cu metode Speak nevirtuale distincte; conversia ascendentă expune problema ascunderii metodelor.
- [00:01:58](https://www.youtube.com/watch?v=82vaxb6o_rg&t=118s) — Animal.Speak devine virtuală, iar Bird.Speak devine o suprascriere; următoarea rulare ajunge la Bird prin Animal.
- [00:09:34](https://www.youtube.com/watch?v=82vaxb6o_rg&t=574s) — Sparrow extinde lanțul de suprasceri; suprascrierile omise păstrează comportamentul moștenit.
- [00:12:03](https://www.youtube.com/watch?v=82vaxb6o_rg&t=723s) — Datele obiectului derivat și o buclă demonstrează ieșire repetată, în timp ce câmpurile moștenite rămân prezente.
- [00:15:46](https://www.youtube.com/watch?v=82vaxb6o_rg&t=946s) — Bird introduce FlapWings virtuală; Sparrow suprascrie două operații independente.
- [00:19:09](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1149s) — Un apel calificat cu base păstrează comportamentul existent al lui Bird în interiorul unei suprasceri.
- [00:20:32](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1232s) — Suprasarcinile, apelurile și referințele fără legătură sunt eliminate pentru a izola apelurile virtuale pe this.
- [00:21:33](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1293s) — Comportamentul implicit al lui Bird este mutat într-un ajutor privat nevirtual.
- [00:25:31](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1531s) — Proprietățile independente de culoare și zbor devin date de instanță, nu combinații de subclase.
- [00:27:03](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1623s) — Animal abstractă împiedică construirea directă; marcarea sealed restricționează apoi moștenirea suplimentară.
- [00:28:56](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1736s) — O suprascriere sealed îngheață o operație fără a sigila întreaga clasă.
- [00:30:05](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1805s) — Speak abstractă elimină corpul implicit și cere o implementare concretă.
- [00:32:23](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1943s) — Suprasărcinile virtuale demonstrează operații separate fără parametri și cu șir.
- [00:33:18](https://www.youtube.com/watch?v=82vaxb6o_rg&t=1998s) — IFlapWings adaugă un contract de interfață lângă ierarhia de clase.
- [00:35:01](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2101s) — Bird furnizează implementarea de interfață lipsă; apartenența la o interfață nu justifică prin ea însăși un override.
- [00:40:39](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2439s) — ISpeak înlocuiește Animal în demonstrația doar cu interfețe.
- [00:42:29](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2549s) — O metodă virtuală de clasă care implementează interfața permite ca suprascrierile din descendenți să servească apelurile de interfață.
- [00:45:06](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2706s) — Implementările explicite separă semnăturile Speak identice de două interfețe.
- [00:46:48](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2808s) — Comparația meniului introduce un contract abstract de acțiune în locul unei valori de funcție invocabile.
- [00:47:39](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2859s) — GreetingAction furnizează suprascrierea de instanță concretă.
- [00:49:00](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2940s) — Numele de salut trece de la un argument al lui Execute la configurare obligatorie a obiectului.
- [00:49:59](https://www.youtube.com/watch?v=82vaxb6o_rg&t=2999s) — AdditionAction și apoi NothingAction înlocuiesc funcțiile rămase; locurile de apel construiesc obiecte de acțiune.
- [00:52:27](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3147s) — AdditionAction folosește o singură instanță statică partajată.
- [00:52:34](https://www.youtube.com/watch?v=82vaxb6o_rg&t=3154s) — Readonly împiedică reatribuirea obișnuită a referinței partajate; încercarea de reatribuire eșuează.
