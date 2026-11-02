> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Căutarea membrilor moșteniți și compoziția

[00:00:00](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=0s) Această lecție pune două întrebări legate: cum găsește C# un membru atunci când clasele se moștenesc unele pe altele și dacă moștenirea este o modalitate utilă de a modela capacitățile unui animal. Începe cu un mic exemplu de clasă de bază și clasă derivată, construiește o ierarhie funcțională de animale, apoi înlocuiește ierarhia de capacități cu compoziție.

Vocabularul de care vei avea nevoie:

- **Clasă, obiect și referință:** o clasă definește un tip; un obiect este o instanță a acelui tip; o referință permite unei variabile să indice acel obiect.
- **Membru, câmp și metodă:** un membru aparține unei clase; un câmp stochează stare; o metodă efectuează o operație.
- **Moștenire, clasă de bază și clasă derivată (subclasă):** o clasă derivată extinde o clasă de bază și îi moștenește membrii. Exemplele folosesc membri accesibili public.
- **Compilator, domeniu de vizibilitate și căutarea membrilor:** compilatorul verifică și traduce codul sursă; domeniul de vizibilitate și căutarea determină la ce membru se referă un nume.
- **Modificator de acces:** un cuvânt-cheie care controlează accesibilitatea; exemplele folosesc `public` pentru a permite accesul din locurile de apel.
- **Metode de instanță și statice:** o metodă de instanță operează având un obiect ca receptor; o metodă statică aparține unui tip și nu are receptor de instanță.
- **Tip static și tip la runtime:** tipul static este tipul cunoscut compilatorului pentru o expresie; tipul la runtime este clasa efectivă a obiectului referit.
- **Ascunderea numelor și `new`:** o declarație derivată introduce propriul membru cu același nume; modificatorul `new` recunoaște explicit că acesta ascunde un membru moștenit.
- **Metodă virtuală, suprascriere și dispatch dinamic:** `virtual` permite o implementare derivată; `override` o furnizează; dispatchul dinamic alege acea implementare folosind obiectul de la runtime.
- **Polimorfism:** folosirea unui tip comun pentru a lucra cu obiecte ale căror implementări pot diferi; tratamentul său detaliat este amânat.
- **Conversie la tipul de bază, restrângere și cast:** o referință derivată poate fi folosită ca referință de bază; restrângerea obține un tip mai specific; o conversie explicită (cast) solicită o conversie.
- **Vector și buclă:** un vector ține o secvență de elemente de un singur tip de element declarat; o buclă prelucrează acele elemente.
- **Verificare de tip și tipar de declarare:** `is Sparrow s` testează compatibilitatea și, la succes, leagă o referință cu tipul specificat în `s`.
- **Compoziție, capacitate și modul:** compoziția asamblează un obiect din obiecte care colaborează; o capacitate este o abilitate precum înotul; un modul implementează o asemenea abilitate.
- **Funcție fabrică și constructor:** o fabrică asamblează și întoarce un obiect; un constructor inițializează o instanță la crearea ei cu `new`.
- **Inițializator de obiect:** acoladele de după crearea obiectului atribuie câmpuri sau proprietăți.
- **Interpolare de șiruri:** `$"...{expression}..."` încorporează valoarea unei expresii în text.
- **Null, referință nullable și tipar non-null:** `null` înseamnă că nu există obiect referit; `?` permite acea absență într-o declarație de tip de referință; `is not null` sau `is { }` testează prezența unei valori. Tiparul de proprietate gol `{ }` este sintaxa celui de-al doilea tipar.
- **Interfață:** un contract pe care tipuri diferite îl pot implementa; este menționată la final ca alternativă parțială, fără a fi dezvoltată aici.

Pentru practică anterioară cu metode de instanță, vezi [laboratorul despre metode de instanță](../../../labs/1_basic/16_method.md). Polimorfismul și interfețele sunt dezvoltate separat; [laboratorul despre polimorfism și interfețe](../../../labs/1_basic/18_interface.md) este un studiu suplimentar opțional, **nu material predat în acest videoclip**.

Exemplele de mai jos sunt reconstrucții scurte ale **stărilor din videoclipul** legat, cu nume explicative și cu ieșire. Nu a fost identificat niciun fișier sursă corespunzător în checkout-ul deținut, deci acestea nu sunt prezentate ca instantane exacte ale depozitului. Fiecare exemplu își are ca scop explicația modificării locale sale; versiunile succesive înlocuiesc definițiile anterioare.

## Câmpuri și metode moștenite

[00:00:14](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=14s) O **clasă de bază** furnizează stare și operații pe care o **clasă derivată** le moștenește. În `class Derived : Base`, cele două puncte declară moștenirea. Un **câmp** stochează date în obiect, astfel încât un obiect derivat își poate citi câmpul public moștenit sub același nume. Modificatorul de acces `public` permite accesul din aceste locuri de apel.

Starea inițială din videoclip, simplificată:

```csharp
class Base
{
    public int Value;
    public void BaseMethod() => Console.WriteLine(Value);
}

class Derived : Base
{
    public int Extra;
    public void Method() => Console.WriteLine(Extra);
}

// La locul de apel:
Derived d = new Derived();
d.Value = 10;  // câmp moștenit
d.Extra = 20;  // câmp adăugat de Derived
```

[00:00:28](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=28s) O **metodă de instanță** se apelează printr-o referință la obiect. Declarația ei poate fi în clasa derivată sau într-o clasă de bază accesibilă. Receptorul rămâne `d`: apelează `d.Method()` pentru metoda adăugată și `d.BaseMethod()` pentru metoda moștenită. O referință de tip de bază poate apela și ea membrul de bază. Stare simplificată la locul de apel:

```csharp
d.Method();
d.BaseMethod();

Base b = d;
b.BaseMethod();
```

[00:00:48](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=48s) Domeniul de vizibilitate **moștenit** înseamnă că membrul de bază rămâne accesibil prin obiectul derivat. Mutarea unei operații în clasa de bază nu îi obligă pe apelanți să renunțe la referința lor derivată. Fii exact cu sintaxa: `Base.Method()`, cu `Base` drept nume de clasă, nu poate apela o metodă de instanță obișnuită fără un receptor.

[00:00:57](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=57s) Mai departe, schimbăm metodele în **metode statice**, care nu au receptor de instanță. Starea din videoclipul legat este reconstruită aici:

```csharp
class Base
{
    public static void BaseMethod() { }
}

class Derived : Base
{
    public static void Method() { }
}

Derived.Method();
Derived.BaseMethod();
Base.BaseMethod();
// d.BaseMethod(); // invalid pentru o metodă statică
```

[00:01:04](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=64s) Metodele statice trebuie accesate folosind un nume de tip, chiar dacă există întâmplător un obiect. „Fără instanță” înseamnă aici fără receptor de instanță pentru metodă, nu că programul nu poate conține obiecte. [00:01:11](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=71s) Atât `Derived.BaseMethod()`, cât și `Base.BaseMethod()` ajung la membrul static moștenit.

[00:01:19](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=79s) **Căutarea membrilor** este rezolvarea unui nume de membru de către compilator. În acest exemplu simplu, când nu găsește `BaseMethod` în `Derived`, continuă prin clasele de bază și găsește declarația în `Base`. [00:01:33](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=93s) Această căutare are loc la compilare. Programul aflat în execuție nu caută repetat în declarațiile claselor pentru a rezolva acest apel ne-virtual.

## Ascunderea și tipul static

[00:01:51](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=111s) Acum dăm ambelor clase o metodă cu același nume și aceeași semnătură. Problema care motivează exemplul este că declarația derivată ascunde declarația moștenită. Adăugăm modificatorul **`new`** pentru a recunoaște **ascunderea intenționată a numelui**; acesta se aplică declarațiilor statice și celor de instanță.

[00:02:15](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=135s) În acest context, `new` înseamnă „știu că există deja un membru moștenit și declar un membru separat care îl ascunde”. Este diferit de `new Derived()`, unde `new` creează un obiect. Înregistrarea descrie diagnosticul ca o eroare; în mod obișnuit, omiterea modificatorului de ascundere produce un **avertisment al compilatorului**, deși un proiect poate trata avertismentele ca erori. Vezi [referința C# pentru modificatorul new](https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/new-modifier).

[00:02:26](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=146s) Revenim la metodele de instanță și comparăm două variabile care indică același obiect. Stare simplificată din videoclip:

```csharp
class Base
{
    public void Method() => Console.WriteLine("Base");
}

class Derived : Base
{
    public new void Method() => Console.WriteLine("Derived");
}

Derived d = new Derived();
Base b = d;
d.Method();
b.Method();
```

Urmărește apelurile în ordine:

1. [00:02:35](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=155s) **Tipul static** al lui `d` este `Derived`, deci căutarea pornește de acolo și găsește `Derived.Method`.
2. Dacă metoda ar lipsi de acolo, căutarea ar continua în clasele sale de bază.
3. [00:02:49](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=169s) Tipul static al lui `b` este `Base`, deci căutarea găsește `Base.Method`, chiar dacă **tipul la runtime** al obiectului este `Derived`.
4. [00:03:03](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=183s) Rularea demonstrației afișează mai întâi `Derived` și apoi `Base`.

[00:03:14](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=194s) Pentru aceste metode ascunse, ne-virtuale, tipul variabilei la momentul compilării determină membrul selectat. Schimbarea tipului static al referinței nu schimbă obiectul la care aceasta indică. [00:03:25](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=205s) Alegerea comportamentului după tipul de la runtime este un mecanism separat; aici urmează doar o scurtă demonstrație.

De ce atribuirea lui `d` la `b` face ca al doilea apel să afișeze `Base`?

<details>
<summary>Răspuns</summary>
Ambele referințe indică același obiect Derived. Metodele sunt declarații ne-virtuale separate, iar căutarea pentru b.Method pornește de la tipul static Base. Atribuirea nu face din b o variabilă de tip Derived.
</details>

## O scurtă demonstrație de dispatch virtual

[00:03:33](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=213s) Înlocuim ascunderea cu o **metodă virtuală** și o **suprascriere**. O metodă virtuală permite o implementare derivată; o suprascriere furnizează implementarea acelei metode virtuale moștenite. **Dispatchul dinamic** alege apoi implementarea după obiectul de la runtime.

Stare modificată, simplificată, din videoclip:

```csharp
class Base
{
    public virtual void Method() => Console.WriteLine("Base");
}

class Derived : Base
{
    public override void Method() => Console.WriteLine("Derived");
}

Derived d = new Derived();
Base b = d;
d.Method();  // Derived
b.Method();  // Derived
```

[00:03:56](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=236s) Rularea demonstrată execută acum implementarea derivată la ambele apeluri. Compilatorul rezolvă în continuare membrul virtual folosind tipul static al expresiei; dispatchul acelui membru folosește tipul obiectului de la runtime. Acest contrast scurt arată de ce `new` și `override` sunt diferite. Polimorfismul detaliat rămâne în afara scopului principal al acestui videoclip.

## Construirea ierarhiei de animale

[00:04:01](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=241s) Trecem de la regulile căutării la un model concret. **Clasa de bază** este acum `Animal`. **Câmpul** ei reține vârsta în zile, iar metoda sa comună de instanță afișează vârsta și numele clasei concrete. `GetType()` întoarce tipul de la runtime; proprietatea sa `Name` oferă numele clasei. Interpolarea de șiruri, sintaxa `$"...{expression}..."`, inserează valorile expresiilor în textul afișat.

Stare inițială simplificată pentru animale, din videoclip:

```csharp
class Animal
{
    public int AgeInDays;

    public void Print() =>
        Console.WriteLine($"{GetType().Name}: {AgeInDays} days");
}

class Penguin : Animal
{
    public void Swim() => Console.WriteLine("Swimming");
}

class Sparrow : Animal
{
    public void Fly() => Console.WriteLine("Flying");
}
```

[00:04:15](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=255s) **Subclasele** adaugă doar abilitățile lor specifice: `Penguin` adaugă `Swim`, iar `Sparrow` adaugă `Fly`. Ambele moștenesc vârsta și afișarea. Metoda de afișare din clasa de bază poate raporta `Penguin` sau `Sparrow` prin `GetType()` fără a fi suprascrisă.

[00:04:35](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=275s) O **conversie la tipul de bază** permite folosirea unei referințe derivate acolo unde tipul ei de bază este așteptat. Folosește-o pentru a pune animale concrete diferite într-un singur **vector**, o secvență al cărei tip de element declarat este `Animal`. Stare simplificată de construire a vectorului, din videoclip:

```csharp
Penguin p = new Penguin();
Sparrow s = new Sparrow();

Animal[] animals =
{
    p,
    s,
    new Penguin()
};
```

[00:04:52](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=292s) Compilatorul știe că `Penguin : Animal`, deci permite `p` într-o poziție `Animal`. [00:05:11](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=311s) Același raționament se aplică și pentru `s`: `Sparrow : Animal` face referința sa utilizabilă ca referință `Animal`. Aceasta nu creează un obiect nou și nu îi șterge tipul concret de la runtime.

## Prelucrarea stării comune

[00:05:24](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=324s) Un **ajutor static** este o operație apelată fără receptor de instanță. Dă-i un parametru `Animal` și lasă o **buclă** să îl aplice fiecărui element al vectorului. Câmpul comun cu vârsta este disponibil pentru că este declarat pe tipul de bază comun.

Stare simplificată de prelucrare comună, din videoclip:

```csharp
static void PassDay(Animal animal)
{
    animal.AgeInDays++;
}

foreach (Animal animal in animals)
{
    PassDay(animal);
    animal.Print();
}
```

`foreach` vizitează fiecare element; `++` incrementează câmpul cu unu. Acest câmp este „comun” în sensul că are o declarație comună, **nu** în sensul unei singure valori globale: fiecare obiect animal are propria sa vârstă.

[00:05:37](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=337s) Aceeași buclă apelează metoda comună de afișare. Clasele diferite de la runtime nu împiedică folosirea stării și a operațiilor moștenite prin `Animal`.

[00:05:42](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=342s) În afara buclei, o variabilă cu tip static `Penguin` permite un apel direct la înot. [00:05:52](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=352s) Citirea unui element dintr-un `Animal[]` produce o expresie cu tip static `Animal`; acel tip singur nu poate garanta înotul. Stare intermediară simplificată, care nu compilează:

```csharp
p.Swim();           // permis: p este Penguin
// animals[0].Swim(); // nu compilează: tipul elementului este Animal
```

Chiar dacă primul element se întâmplă să fie un pinguin, compilatorul nu poate deduce această garanție din tipul declarat al vectorului.

## Restrângerea unei referințe înainte de a folosi o abilitate specifică

[00:06:14](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=374s) Soluția este **restrângerea**: obține o referință cu tipul subclasei specifice înainte de a apela abilitatea ei. O **verificare de tip** confirmă compatibilitatea la runtime. Un **tipar de declarare** atât verifică compatibilitatea, cât și leagă o variabilă tipizată.

[00:06:24](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=384s) Stare modificată simplificată, din videoclip:

```csharp
Animal animal = animals[0];

if (animal is Penguin penguin)
{
    penguin.Swim();
}
```

[00:06:33](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=393s) În `is Penguin penguin`, `Penguin` numește tipul cerut, iar `penguin` numește variabila legată la succes. Variabila se referă la același obiect; tiparul nu construiește un pinguin. [00:06:38](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=398s) Dacă obiectul de la runtime este incompatibil, corpul este sărit. [00:06:45](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=405s) Prin urmare, apelul abilității rulează doar după ce compatibilitatea a fost stabilită.

[00:06:58](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=418s) O verificare fără desemnarea unei variabile, precum `animal is Sparrow`, testează obiectul, dar lasă `animal` cu tipul static `Animal`. Un apel specific de tip Sparrow prin acea variabilă originală totuși eșuează. [00:07:09](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=429s) Convertește întâi, apoi apelează. O **conversie explicită**, scrisă `(Sparrow)animal`, solicită tipul de referință specific; verificarea precedentă face ca această conversie să fie sigură în exemplu.

Forme simplificate, intermediară și corectată, din această stare a videoclipului:

```csharp
if (animal is Sparrow)
{
    // animal.Fly(); // invalid: animal este încă Animal
    ((Sparrow)animal).Fly();
}

// Un tipar de declarare combină verificarea și conversia:
if (animal is Sparrow sparrow)
{
    sparrow.Fly();
}
```

[00:07:18](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=438s) Aplică acest tipar într-o buclă peste obiecte de tip de bază. Stare simplificată de filtrare, din videoclip:

```csharp
foreach (Animal animal in animals)
{
    if (animal is Penguin penguin)
    {
        penguin.Swim();
    }
}
```

[00:07:32](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=452s) Fiecare element curent este testat independent. [00:07:52](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=472s) Un element potrivit primește apelul de metodă al tipului concret; elementele incompatibile nu fac nimic. [00:07:57](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=477s) Astfel poți acționa doar asupra pinguinilor, păstrând un singur vector amestecat.

Ar provoca un Sparrow din vector aruncarea unei excepții în bucla de înot?

<details>
<summary>Răspuns</summary>
Nu. Tiparul de declarare eșuează pentru acel element, deci corpul nu se execută. Bucla nu face o conversie neverificată și nu apelează Swim pe o referință Animal.
</details>

[00:08:07](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=487s) **Limită de scop:** **polimorfismul**, lucrul printr-un tip comun cu implementări diferite, este lăsat intenționat unui alt videoclip pentru tratamentul său complet. Acest exemplu folosește verificări explicite și referințe de subclase; nu dezvoltă un design general de capacități bazat pe metode virtuale sau interfețe.

## Verificarea exemplului cu moștenire

[00:08:12](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=492s) Rularea din înregistrare confirmă că ajutorul de trecere a zilei se execută separat pentru fiecare animal și mărește vârsta fiecăruia cu o zi. O operație comună de afișare poate apoi prezenta starea actualizată a fiecărui animal.

[00:08:26](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=506s) Mai departe, metoda specifică grigoleului este apelată direct prin variabila de tip Sparrow, iar bucla de filtrare apelează `Swim` pentru fiecare pinguin. Secvență de apeluri testată, simplificată:

```csharp
foreach (Animal animal in animals)
{
    PassDay(animal);
    animal.Print();
}

s.Fly();

foreach (Animal animal in animals)
{
    if (animal is Penguin penguin)
        penguin.Swim();
}
```

[00:08:36](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=516s) Adăugăm încă un `Penguin` în vector. Cu trei pinguini prezenți, înotul se execută de trei ori — o dată pentru fiecare element potrivit din vector. Elementul adăugat schimbă munca efectuată de buclă fără să necesite o nouă instrucțiune de apel al abilității.

Acestea sunt rezultate demonstrate în înregistrare, nu afirmații că exemplele reconstruite au fost executate în această sesiune de redactare.

## Înlocuirea ierarhiei cu compoziție

[00:08:44](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=524s) Ierarhia funcțională arată ce poate face moștenirea; nu este o recomandare de a modela astfel capacitățile animalelor. [00:08:54](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=534s) Sfatul de proiectare este să folosim moștenirea cu măsură și să preferăm **compoziția** pentru acest scenariu.

[00:08:59](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=539s) **Compoziția** înseamnă construirea unui animal din obiecte care colaborează. O **capacitate** este o abilitate precum înotul sau zborul, iar un **modul** este un obiect separat care implementează acea abilitate. Dă-i lui `Animal` date comune și un nume, apoi dă modulelor de capacități acces la acel animal.

[00:09:06](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=546s) Stare simplificată de compoziție, din videoclip:

```csharp
class Animal
{
    public string Name = "";
    public int AgeInDays;
    public SwimModule? Swimming;
    public FlyModule? Flying;

    public void Print() =>
        Console.WriteLine($"{Name}: {AgeInDays} days");
}

class SwimModule
{
    Animal animal;

    public SwimModule(Animal animal)
    {
        this.animal = animal;
    }

    public void Swim() =>
        Console.WriteLine($"{animal.Name} swims");
}

class FlyModule
{
    Animal animal;

    public FlyModule(Animal animal)
    {
        this.animal = animal;
    }

    public void Fly() =>
        Console.WriteLine($"{animal.Name} flies");
}
```

Un **constructor**, precum `SwimModule(Animal animal)`, inițializează un modul nou creat. `this.animal` numește câmpul său, iar `animal` necalificat din dreapta numește parametrul. Modulul deține o **referință** la animalul comun, astfel încât operația lui poate folosi același nume și aceleași date. Nicio declarație de moștenire nu leagă aceste trei clase.

Anotările de **referință nullable** `SwimModule?` și `FlyModule?` permit **null**, adică absența unui modul. Un animal poate avea înot, zbor, ambele sau niciunul. Gestionarea numelui aparține datelor comune ale animalului, nu unor implementări separate în subclase.

## Asamblarea animalelor cu fabrici

[00:09:23](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=563s) Clasele concrete `Penguin` și `Sparrow` dispar din versiunea cu compoziție. Crearea trece în **funcțiile fabrică**: funcții care asamblează și întorc animale configurate.

[00:09:35](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=575s) Construim pinguinul în etape:

1. Creăm un `Animal` cu numele și vârsta inițială.
2. Creăm un modul de înot folosind o referință la acel animal.
3. Atribuim modulul câmpului de înot și lăsăm zborul absent.
4. Returnăm animalul asamblat.

Un **inițializator de obiect** este atribuirea de câmpuri dintre acolade de după `new Animal()`. Stare simplificată a fabricii de pinguin, din videoclip:

```csharp
static Animal CreatePenguin(int age)
{
    Animal animal = new Animal()
    {
        Name = "Penguin",
        AgeInDays = age
    };
    animal.Swimming = new SwimModule(animal);
    return animal;
}
```

Designul modulului poate el însuși depinde de date furnizate fabricii sau poate folosi un tip de module suficient de general; compoziția nu impune pentru totdeauna o singură implementare codificată fix.

[00:09:50](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=590s) Repetăm modelul de construcție pentru un grigole, dându-i zbor în loc de înot. Stare simplificată a celei de-a doua fabrici, din videoclip:

```csharp
static Animal CreateSparrow(int age)
{
    Animal animal = new Animal()
    {
        Name = "Sparrow",
        AgeInDays = age
    };
    animal.Flying = new FlyModule(animal);
    return animal;
}
```

Ambele funcții întorc același tip general. Numele lor descriu asamblarea aleasă, nu subclase diferite la runtime.

## Citirea capacităților din câmpuri

[00:10:18](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=618s) În acest model simplificat, un grigole zboară dar nu înotă, un pinguin înotă dar nu zboară, iar o rață poate face ambele. „Pasăre” ca atare nu determină combinația de capacități. Verificarea clasei unei specii este deci sursa greșită de informații despre capacități pentru noul design.

[00:10:39](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=639s) Citește în schimb **câmpurile**-module ale lui `Animal` general. Un câmp stochează referința către un obiect de capacitate; prezența lui îi spune programului ce operație este disponibilă.

[00:10:59](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=659s) Animalele pot împărtăși în continuare un singur **vector**, iar operațiile comune de vârstă și afișare continuă să funcționeze, acum fără o ierarhie de subclase de animale. Stare simplificată cu vector compus, din videoclip:

```csharp
Animal[] animals =
{
    CreatePenguin(10),
    CreateSparrow(20)
};

foreach (Animal animal in animals)
{
    PassDay(animal);
    animal.Print();
}
```

Aceste valori ale vârstei sunt illustrative. Aici vectorul conține animale asamblate, nu module de abilități. Înotul și zborul rămân în câmpurile de module ale fiecărui animal. Nu este nevoie să extragi o referință de tip Penguin sau Sparrow pentru a folosi datele comune.

## Verificarea existenței unui modul înainte de a-l apela

[00:11:19](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=679s) Un câmp de modul poate fi **null**, adică nu există niciun obiect de capacitate. Apelarea unei metode printr-o referință absentă ar eșua, așa că verifică înainte de a invoca operația. Formă simplificată din prima stare a videoclipului:

```csharp
foreach (Animal animal in animals)
{
    SwimModule? swimming = animal.Swimming;
    if (swimming != null)
    {
        swimming.Swim();
    }
}
```

Înregistrarea arată și verificarea directă a câmpului. Ambele forme întreabă dacă capacitatea există, nu dacă animalul are un anumit tip de specie.

[00:11:39](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=699s) Scurtăm verificarea cu un **tipar de declarare**, care leagă la succes o referință de modul utilizabilă. Stare revizuită simplificată, din videoclip:

```csharp
if (animal.Swimming is SwimModule swimming)
{
    swimming.Swim();
}
```

[00:11:46](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=706s) Pare testul de subtip de mai devreme, dar câmpul de intrare are deja tipul declarat `SwimModule?`. Nu există un tip la runtime mai îngust de descoperit: pentru această expresie, succesul se reduce la faptul că câmpul este non-null. Sintaxa generală poate testa compatibilitatea la runtime când tipul de intrare este mai puțin specific.

[00:11:54](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=714s) Eliminăm tipul de modul repetat cu un **tipar de proprietate gol**, `{ }`. Acest **tipar non-null** se potrivește cu o valoare prezentă și o poate lega de o variabilă. Verificare finală simplificată din înregistrare:

```csharp
if (animal.Swimming is { } swimming)
{
    swimming.Swim();
}

// Un test non-null fără capturarea unei variabile noi:
if (animal.Swimming is not null)
{
    animal.Swimming.Swim();
}
```

Cele două forme exprimă o verificare non-null, dar doar prima formă prezentată leagă `swimming`. Nu combina cele două scrieri în `is not null swimming`. Tiparul gol nu cere un alt tip și nu copiază obiectul modul: variabila sa reține referința câmpului evaluat. Vezi [explicația Microsoft despre tiparele de proprietate](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/patterns/property-positional-patterns).

[00:12:11](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=731s) Avantajul este că clientul nu mai numește o specie și nu mai repetă un tip concret de modul în testul său. Dacă tipul declarat al câmpului se schimbă, oferind în continuare operația necesară, această formă de verificare a prezenței poate rămâne neschimbată.

[00:12:32](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=752s) Înotul și zborul aparțin unor **module** separate, nu tipului de pasăre. Codul comun al clientului poate citi aceste câmpuri și poate folosi capacitățile oriunde este nevoie. Acesta este avantajul reutilizării pe care schița de moștenire care urmează îl reproduce cu greutate, fără implementări repetate sau o ierarhie mai complicată.

Cum s-ar schimba bucla de înot dacă s-ar adăuga o rață cu ambele module?

<details>
<summary>Răspuns</summary>
Bucla poate rămâne aceeași. Ea verifică câmpul de înot al fiecărui Animal, deci modulul de înot al raței este invocat la fel ca cel al pinguinului. Un grigole cu câmpul de înot nul este sărit. Nu este nevoie de o verificare de tip Duck.
</details>

## De ce alternativa cu moștenire devine stânjenitoare

[00:12:49](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=769s) Revenim la moștenire pentru a expune presiunea de proiectare. Un pinguin înotă, un grigole zboară, iar o rață are nevoie de amândouă. Schiță simplificată a moștenirii din această stare a videoclipului:

```csharp
class Animal { }

class Penguin : Animal
{
    public void Swim() { }
}

class Sparrow : Animal
{
    public void Fly() { }
}

class Duck : Animal
{
    public void Swim() { } // repetă operația de înot
    public void Fly() { }  // repetă operația de zbor
}
```

[00:13:01](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=781s) Discuția ia în considerare și suprascrierea comportamentului. O **suprascriere** înlocuiește o implementare virtuală de bază potrivită, ca în demonstrația anterioară despre dispatch; schița de mai sus arată doar duplicarea capacităților și nu pretinde că metodele ei suprascriu ceva. Corpurile goale sunt substituenți: comportamentul real ar putea afișa informații sau calcula date, ceea ce ar face duplicarea mai costisitoare.

[00:13:20](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=800s) O posibilă ieșire este o **funcție statică** comună apelată de metoda fiecărei clase. Aceasta poate centraliza implementarea, dar reutilizarea vine atunci dintr-un apel obișnuit la un ajutor, nu din moștenirea acelui comportament. Clasele adaugă totuși metode de redirecționare; moștenirea contribuie puțin la acea reutilizare. Videoclipul schițează această opțiune fără să o dezvolte într-un design complet.

[00:13:34](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=814s) Altă problemă este locul de apel. O referință `Animal` nu poate invoca `Swim` doar pentru că `Penguin` și `Duck` îl declară independent: `Animal` trebuie să expună un membru comun potrivit. Stare simplificată la locul de apel, care nu compilează:

```csharp
Animal animal = new Penguin();
// animal.Swim(); // invalid: Animal nu declară niciun membru Swim
```

[00:14:05](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=845s) Pentru a moșteni o singură implementare a înotului, introducem o clasă de bază comună pentru înot. Dar o rață are nevoie și de o implementare comună pentru zbor. [00:14:29](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=869s) C# permite ca o clasă să moștenească de la o singură **clasă de bază**, deci nu poate moșteni pur și simplu atât o clasă de înot, cât și una de zbor. Încercările de a încadra fiecare combinație de capacități într-o ierarhie de clase acumulează clase similare și cod duplicat. Aceasta este limitarea ilustrată aici, nu o afirmație că moștenirea nu ar putea niciodată reutiliza cod.

[00:14:44](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=884s) **Interfețele** — contracte pe care tipurile le implementează — pot exprima parțial capacități multiple. În abordarea obișnuită cu contracte de interfață discutată aici, fiecare clasă trebuie totuși să ofere o implementare sau să delege către cod comun; doar declararea contractului nu furnizează cele două implementări de clasă dorite. C# modern acceptă și implementări implicite de interfață, deci „interfețele nu pot conține niciodată implementări reutilizabile” ar fi prea general. Această funcție **nu este predată aici**; [documentația Microsoft despre moștenire](https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/inheritance) o descrie.

Lecția se încheie cu compromisul de proiectare: păstrează datele comune ale animalului într-un singur loc și compune capacități independente atunci când combinațiile lor variază. Polimorfismul detaliat, proiectarea completă a interfețelor și implementările implicite de interfață rămân în afara lecției.

## Istoricul modificărilor de cod

Dovezile disponibile pentru aceste fragmente în evoluție sunt înregistrarea. Aceste legături stabile cu timestamp identifică stări din videoclip, nu instantane de commit; nu este folosit ca dovadă niciun commit fără legătură care adaugă exerciții.

- [00:00:14](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=14s) Base/Derived începe cu stare moștenită și metode de instanță.
- [00:00:57](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=57s) Metodele devin statice; apelurile folosesc nume de clase.
- [00:01:51](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=111s) Metodele cu același nume introduc ascunderea intenționată cu `new`.
- [00:02:26](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=146s) Referințele de tip de bază și de tip derivat demonstrează selecții ne-virtuale diferite.
- [00:03:33](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=213s) `virtual` și `override` fac ca ambele apeluri să selecteze implementarea derivată.
- [00:04:01](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=241s) Animal/Penguin/Sparrow introduce vârsta și afișarea comune, cu abilități separate.
- [00:05:24](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=324s) O singură buclă pe vector actualizează starea comună.
- [00:06:24](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=384s) Tiparele de tip restrâng în siguranță referințele înainte de apelurile de abilitate.
- [00:08:12](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=492s) Rularea confirmă îmbătrânirea per animal; adăugarea unui pinguin adaugă încă un apel de înot.
- [00:08:59](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=539s) Compoziția mută abilitățile în module separate.
- [00:09:50](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=590s) Fabricile asamblează combinația corespunzătoare de module.
- [00:11:19](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=679s) Apelurile de capacitate verifică mai întâi existența unui modul.
- [00:11:39](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=699s) Un tipar de declarare combină verificarea modulului și legarea.
- [00:11:54](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=714s) Un tipar de proprietate gol elimină tipul de modul repetat.
- [00:12:49](https://www.youtube.com/watch?v=y1KNWcgUUMU&t=769s) Schița finală cu moștenire expune duplicarea și limita unei singure clase de bază.