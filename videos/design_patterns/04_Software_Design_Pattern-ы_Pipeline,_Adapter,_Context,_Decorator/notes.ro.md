> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# De la instrucțiuni fixe la pipeline-uri configurabile

[00:00:00](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=0s) Scopul acestei lecții este să transforme o succesiune obișnuită de instrucțiuni C# într-un program ale cărui acțiuni pot fi stocate, reordonate, inspectate și furnizate de un apelant. Un câine își schimbă proprietarul, primește un nume nou și este afișat. Urmărirea acestui mic exemplu prin refactorizări succesive explică de ce fiecare obiect suplimentar este util.

Iată vocabularul folosit pe parcurs:

- **Șablon de proiectare (Design pattern):** o configurație reutilizabilă de componente pentru o clasă de probleme. **Șabloanele de creare** organizează crearea obiectelor; **șabloanele structurale** organizează compoziția obiectelor; **șabloanele de comportament** organizează execuția. **Compoziția** înseamnă combinarea obiectelor, inclusiv păstrarea unui obiect în interiorul altuia.
- **Pipeline:** o succesiune ordonată de operații. Mai târziu, un obiect `Pipeline` memorează aceste operații și le rulează. **Chain of Responsibility** este șablonul înrudit menționat la început; runner-ul acestei lecții vizitează fiecare pas configurat.
- **Context:** datele partajate disponibile în timpul unei rulări. Câinele și istoricul proprietarilor aparțin acestui context. Lista de instrucțiuni și setările fiecărui pas au roluri separate.
- **Refactorizare:** reorganizarea codului păstrând comportamentul intenționat. **Abstracția** permite apelanților să se refere la muncă printr-un nume; **încapsularea** pune acțiunile concrete într-o singură implementare în spatele acelui nume.
- **Funcție locală și captură:** o funcție declarată într-o funcție care o înconjoară sau în domeniul punctului de intrare poate folosi variabilele locale din acel domeniu. **Instrucțiunile de nivel superior** oferă un corp de punct de intrare fără a scrie explicit un `Main`.
- **Dependență:** datele sau un alt obiect de care o operație are nevoie. Un **adaptor** prezintă interfața pe care o așteaptă apelantul și transmite către logica existentă argumentele de care aceasta are nevoie.
- **Interfață și polimorfism:** o interfață este un contract comun; polimorfismul permite folosirea unor implementări diferite prin intermediul ei. **Dispecerizarea (dispatch)** selectează implementarea la timpul rulării. Cuvântul cheie C# `virtual` permite separat suprascrierea unei metode de clasă.
- **Obiect-pas:** un obiect care reprezintă o acțiune de efectuat mai târziu. **Configurația la momentul construcției** sunt date memorate la creare, precum un proprietar sau un nume ales. O **metodă fabrică (factory method)** construiește și returnează un obiect configurat.
- **Introspecție:** examinarea pașilor unui pipeline și a descrierilor pe care aceștia le expun. Un **test de tip** verifică tipul unui obiect. O **expresie lambda** este o funcție scurtă; aici ea servește drept **predicat**, o condiție care întoarce adevărat sau fals.
- **Decorator sau înveliș:** un obiect care conține un pas compatibil, îi păstrează contractul și adaugă comportament în jurul acțiunii sale.
- **Model și sintaxă de colecții:** `sealed` împiedică moștenirea clasei; `required` impune inițializarea unui membru. Un **initializer de obiect** furnizează membrii după `new`. `List<T>` este o colecție care poate crește; **new cu tip țintă** deduce tipul construit din declarație. O **expresie de colecție**, precum `[]`, furnizează conținutul unei colecții. Un **tablou** are lungime fixă; un **indice de la zero** numerotează primul element cu 0.
- **Nulabilitate și inițializare:** `null` înseamnă că nu există niciun obiect referit; `Owner?` exprimă faptul că absența este permisă. Un **constructor** inițializează un obiect. Un **câmp readonly** poate fi atribuit la declarație sau în constructorul său și nu poate fi reatribuit ulterior de un apelant. Accesorii de proprietate `get`, `set` și `init` controlează citirea, scrierea și inițializarea.
- **Sintaxă de control și de afișare:** `foreach` vizitează elementele unei colecții; `while` repetă; `switch/case` selectează o ramură a comenzii; `break` părăsește ramura respectivă; `return` returnează un rezultat sau părăsește punctul de intrare. `static` asociază un membru tipului său, `public` îl expune, iar `void` înseamnă că nu se returnează nicio valoare. **Interpolarea șirurilor** inserează valori în text; **coalescența nulă** (`??`) furnizează o valoare de rezervă pentru `null`.

Exercițiul însoțitor este [laboratorul Pipeline](../../../labs/2_design/02_pipeline.md). Extensiile sale sunt studiu suplimentar, nu alte implementări demonstrate aici.

[00:00:19](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=19s) Un șablon de proiectare este mai abstract decât exemplul anterior cu masca de biți, deoarece descrie o configurație recognoscibilă peste multe sarcini. Implementările concrete diferă, în timp ce componentele își păstrează responsabilități asemănătoare și comunică în moduri asemănătoare. [00:00:44](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=44s) Șabloanele de creare se referă la crearea obiectelor, iar șabloanele structurale la compoziția lor. [00:00:56](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=56s) Întrebarea de pornire aici este una de comportament: cum poate programul decide ce operații se execută și în ce ordine?

[00:01:04](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=64s) Răspunsul propus este să reprezentăm acțiunile ca date într-o colecție. Selectarea și ordonarea acelor valori definește munca programului. [00:01:23](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=83s) Înregistrarea menționează Chain of Responsibility ca o idee în apropiere și numește această formă secvențială un pipeline. [00:01:38](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=98s) Contextul său va fi un obiect care conține date accesibile mai multor părți ale programului.

**Domeniu de aplicare:** lecția dezvoltă un exemplu C# secvențial, în memorie. Ordinea controlată de utilizator apare doar după ce acțiunile devin valori. Inspecția produce descrieri pe consolă; un cadru general de introspecție este lăsat neterminat. Tipuri de context generice, oprirea anticipată și instrumente de inspecție mai bogate sunt extensiuni opționale de laborator. Exemplele de mai jos sunt extrageri scurtate sau reconstrucții, nu un singur program gata de lipit. O citare etichetată drept **cod salvat conex** sprijină designul rezultat fără a pretinde că un commit conține starea exactă de editor de mai devreme. Rezultatele rulării descriu verificări din înregistrare; aici nu se pretinde nicio execuție nouă.

## Creează datele câinelui și ale proprietarului

[00:01:42](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=102s) Începem cu datele pe care operațiile le vor modifica: un `Dog` cu un nume. [00:01:49](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=109s) O **clasă sealed** împiedică subclasele, corespunzând unei clase Java declarate `final`. [00:01:52](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=112s) Un **membru required** face inițializarea obligatorie la creare. Prin urmare, numele câinelui apare în **initializerul său de obiect**, acoladele de după `new Dog`.

[00:02:04](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=124s) Adăugăm `Owner` și o listă de proprietari anteriori. [Codul salvat conex](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) conține ambele clase de model; această reconstrucție așază configurarea inițială înaintea declarațiilor, astfel încât instrucțiunile de nivel superior să rămână într-o poziție validă:

```csharp
List<Owner> previousOwners = new List<Owner>();
var dog = new Dog { Name = "Luke" };

sealed class Dog
{
    public required string Name;
    public Owner? Owner = null;
}

sealed class Owner
{
    public required string Name;
}
```

[00:02:26](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=146s) `List<Owner>` este o colecție de referințe la proprietari, susținută de un tablou, care poate crește. Comparația cu listele din Java este conceptuală: `List` din Java este o interfață, iar `ArrayList` este implementarea sa familiară bazată pe un tablou care poate crește. `new List<Owner>()` creează o colecție goală. [00:02:33](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=153s) **New cu tip țintă** evită repetarea tipului deja prezent în stânga. Contextul salvat folosește mai târziu **expresia de colecție** `[]` pentru lista sa goală. Aceste forme de scriere descriu listele goale din acest exemplu:

```csharp
List<Owner> previousOwners = new(); // Tipul vine din declarație.
var owner = new Owner { Name = "Anton" };

previousOwners.Add(owner);
previousOwners.Remove(owner);
```

[00:02:36](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=156s) `Add` adaugă la sfârșit o referință la un proprietar, inclusiv pe una păstrată într-o variabilă; `Remove` elimină un element potrivit. Lista istoricului conține proprietari, nu câini.

[00:02:45](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=165s) Creează un câine cu nume, lăsând proprietatea neprecizată. [00:02:56](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=176s) **Nulabilitatea** exprimă posibilitatea absenței: `Owner?` permite câmpului să reprezinte „niciun proprietar”. [00:03:07](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=187s) Comparația cu Java cere atenție: și variabilele de referință din Java pot conține null. Cu analiza nulabilității activate în C#, `Owner?` transmite intenția compilatorului; nu introduce o barieră la timpul rulării care să facă `Owner` incapabil să conțină null. Vezi [tipuri de referință nullable în C#](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/null-safety/nullable-reference-types) și [variabilele de referință din Java](https://docs.oracle.com/javase/specs/jls/se25/html/jls-4.html#jls-4.12.2).

[00:03:17](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=197s) Omiterea și nulabilitatea sunt independente. Câinele nu are modificatorul `required` pe câmpul său de proprietar, deci crearea poate omite câmpul și îl poate lăsa null. În schimb, `required Owner?` ar permite tot null, dar ar cere o inițializare explicită. `required` nu validează singur valoarea furnizată. [Regulile pentru membrii required](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required) deosebesc această obligație de avertismentele despre starea nulă.

## Scrie prima succesiune ordonată

[00:03:27](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=207s) Începem cu o schimbare obișnuită de proprietar. [00:03:32](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=212s) Succesiunea planificată este: atribuim un proprietar, facem ceva câinelui, apoi atribuim alt proprietar. [00:03:38](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=218s) Atribuirea modifică direct câmpul. [00:03:44](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=224s) Redenumirea câinelui oferă operația din mijloc.

[Implementarea salvată conexă a proprietății](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) înregistrează proprietarul care pleacă înainte de a-l înlocui. Extinsă la succesiunea inițială, această regulă arată așa:

```csharp
var owner1 = new Owner { Name = "Anton" };
dog.Owner = owner1;                 // La început nu există proprietar care pleacă.
dog.Name = "Lucy";

var owner2 = new Owner { Name = "Alex" };
if (dog.Owner != null)
    previousOwners.Add(dog.Owner);  // Îl salvăm pe Anton înainte de înlocuire.
dog.Owner = owner2;
```

[00:03:57](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=237s) Acesta este deja un **pipeline** în sensul obișnuit: o succesiune ordonată de operații. Compoziția sa este fixată de ordinea instrucțiunilor. [00:04:07](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=247s) În acest exemplu, variabilele de proprietar nu au intenționat nicio altă treabă: sunt atribuite câinelui și păstrate în istoric atunci când sunt înlocuite.

[00:04:12](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=252s) Prima atribuire de proprietar nu are niciun proprietar anterior de înregistrat. Înlocuirile ulterioare salvează proprietarul curent existent. Această distincție rezolvă narațiunea vagă despre „adăugarea unui proprietar”: lista este istoricul proprietarilor, deci primește proprietarul care **pleacă**, nu pe cel care sosește.

[00:04:24](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=264s) Fiecare înlocuire suplimentară ar repeta verificarea de null, adăugarea și atribuirea. Această duplicare face regula de proprietate mai greu de întreținut: fiecare copie ar necesita aceeași corecție. **Refactorizarea** va reorganiza această muncă într-o implementare comună, păstrând comportamentul intenționat.

## Extrage operația locală de schimbare a proprietarului

[00:04:35](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=275s) **Încapsularea** strânge instrucțiunile repetate într-un singur corp de funcție. [00:04:37](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=277s) Mai întâi o transformăm într-o **funcție locală**, declarată în domeniul punctului de intrare care o înconjoară. [00:04:45](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=285s) Prin **captură**, ea poate citi variabilele `dog` și `previousOwners` din jur, deci aceste dependențe nu au încă nevoie de parametri expliciți.

[Codul salvat conex](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) conține versiunea parametrizată ulterioară a acestui ajutor. Această reconstrucție îi arată forma locală timpurie:

```csharp
void ChangeOwner(Owner? newOwner)
{
    if (dog.Owner != null)
        previousOwners.Add(dog.Owner);
    dog.Owner = newOwner;
}

ChangeOwner(owner1);
dog.Name = "Lucy";
ChangeOwner(owner2);
```

[00:04:53](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=293s) Verificarea de null îi aparține **vechiului proprietar curent**. Câmpul curent și lista istoricului au semnificații diferite: câmpul spune cine deține câinele acum; lista înregistrează proprietarii înlocuiți de schimbări. Operația extrasă adaugă o intrare doar dacă a existat un proprietar care pleacă.

[00:05:04](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=304s) Un argument nullable permite `ChangeOwner(null)`, ceea ce șterge proprietatea și lasă câinele fără proprietar. Trimiterea lui null descrie noua stare; nu implică faptul că câinele era deja fără proprietar. Dacă Alex era proprietarul curent, Alex este salvat înainte de ștergerea câmpului. Dacă nu exista niciun proprietar, nu se adaugă nicio intrare în istoric.

[00:05:15](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=315s) Înlocuim atribuirile directe de câmpuri din locurile de apelare cu apeluri la un ajutor. [00:05:26](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=326s) **Abstracția** dă acestei munci un nume, `ChangeOwner`, prin care apelanții cer operația. Încapsularea păstrează instrucțiunile concrete scrise o singură dată în interiorul ei. [00:05:46](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=346s) Este suficient să apelăm acel nume; apelanții nu mai duplică corpul său.

<details>
<summary>Preziceți proprietarul curent și istoricul după secvența fără proprietar → Anton → Alex → null.</summary>

Proprietarul curent este null. Istoricul conține Anton, apoi Alex. Absența inițială nu devine niciodată o intrare de proprietar, iar ștergerea unui proprietar existent înregistrează în continuare acel proprietar care pleacă.

</details>

## Reutilizează succesiunea și întâlnește duplicarea

[00:05:51](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=351s) Există acum mai multe acțiuni recognoscibile: schimbarea proprietarului, schimbarea numelui și afișarea. Afișarea se împarte în raportarea proprietarilor anteriori și raportarea câinelui. [00:06:13](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=373s) Fiecare poate fi tratată ca un pas disponibil într-un program.

[00:06:28](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=388s) Un utilizator tot nu își poate alege ordinea. Instrucțiunile o determină. [00:06:40](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=400s) Îmbunătățirea imediată este mai îngustă: să permitem invocarea din nou a aceleiași succesiuni. Compoziția la timpul rulării vine mai târziu.

[00:06:55](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=415s) Mutăm succesiunea într-o funcție dintr-o clasă separată. **Instrucțiunile de nivel superior** sunt instrucțiunile din afara acelor clase care formează corpul punctului de intrare al programului. [Codul salvat conex](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) păstrează un `Functions.Pipeline1` ulterior; această miniatură reprezintă extragerea inițială:

```csharp
Functions.Pipeline1(); // Instrucțiune de nivel superior.

static class Functions
{
    public static void Pipeline1()
    {
        // Creează câinele și istoricul său.
        // Schimbă proprietarul, redenumește, schimbă din nou proprietarul.
        // Afișează istoricul și câinele.
    }
}
```

`static` face ca această funcție să poată fi apelată prin tip fără a crea un obiect `Functions`. `public` expune membrul, iar `void` înseamnă că nu returnează nicio valoare.

[00:07:05](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=425s) Apelarea aceleiași funcții de două ori poate repeta aceeași succesiune. Construirea unei succesiuni **diferite** într-o a doua funcție necesită însă copierea și modificarea corpului său. [00:07:10](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=430s) O asemenea copie ar putea schimba nume, reordona acțiuni, afișa înaintea unei modificări sau afișa de două ori. [00:07:28](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=448s) Fiecare copie repetă și implementările acțiunilor. Prima problemă de rezolvat este această logică duplicată, înainte de a aborda ordinea controlată de apelant.

## Dă fiecărei operații dependențe explicite

[00:07:37](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=457s) Extragem corpurile comune ale acțiunilor în funcții separate. Mutarea unui bloc de afișare înseamnă transmiterea datelor pe care le obținea anterior din variabile locale, inclusiv lista de proprietari unde este nevoie. [00:07:57](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=477s) Schimbările de proprietar pot fi mutate și ele; expunerea intrărilor lor pregătește adaptorul ulterior.

O **dependență** este ceva de care o operație are nevoie ca să își facă treaba. [Funcțiile salvate conexe](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) arată separarea finală a acestor intrări. Această versiune scurtată omite etichetele de diagnostic:

```csharp
static void ChangeOwner(
    Dog dog, List<Owner> previousOwners, Owner? newOwner)
{
    if (dog.Owner != null)
        previousOwners.Add(dog.Owner);
    dog.Owner = newOwner;
}

static void ChangeName(Dog dog, string name)
{
    dog.Name = name;
}

static void PrintOwners(List<Owner> previousOwners)
{
    foreach (var owner in previousOwners)
        Console.WriteLine(owner.Name);
}

static void PrintDog(Dog dog)
{
    Console.WriteLine(dog.Name);
    if (dog.Owner != null)
        Console.WriteLine(dog.Owner.Name);
}
```

[00:08:08](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=488s) Schimbările de proprietar necesită trei intrări: câinele, proprietarii anteriori și proprietarul care sosește. [00:08:16](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=496s) Le transmitem pe toate trei explicit, în loc să le capturăm. Discuția anterioară despre afișarea combinată menționează lista de proprietari; în împărțirea rezultată, afișatorul istoricului are nevoie de acea listă, iar afișatorul câinelui are nevoie de câine. O **buclă foreach** vizitează elementele colecției pe rând.

[00:08:27](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=507s) Diferite locuri de apelare pot construi diferiți proprietari. Acea repetare furnizează configurația; nu dublează algoritmul de schimbare a proprietarului. [00:08:36](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=516s) Mutăm ajutorul într-o locație comună și folosim parametrul său de proprietar primit, în loc să creăm un proprietar în interiorul lui. [00:08:49](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=529s) Aplicăm aceeași extragere lui `ChangeName`, cu parametrii săi pentru câine și nume. Acțiunile sunt acum reutilizabile, deși fiecare succesiune trebuie în continuare să aranjeze apelurile lor.

## Identifică contextul comun de execuție

[00:09:08](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=548s) Reutilizarea acțiunilor într-o altă succesiune dezvăluie o altă structură repetată: fiecare succesiune stabilește aceleași feluri de stare și le transmite bucățile în jur. [00:09:19](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=559s) Aceste bucăți sunt câinele și **lista proprietarilor anteriori**. Referirile la o „listă de operații” din această parte a explicației nu trebuie citite drept colecția de instrucțiuni de mai târziu; modelul salvat clarifică rolul istoricului de proprietate.

[00:09:38](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=578s) Un **context** este grupul de date disponibil operațiilor în timpul execuției. [00:09:46](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=586s) Punem acel grup într-o clasă dedicată, cu câmpuri. [00:10:00](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=600s) Transmiterea a două variabile separate în fiecare succesiune lasă responsabilitatea de a păstra perechea laolaltă în sarcina apelanților.

[Contextul și apelurile salvate conexe](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) fac concretă tranziția intenționată. Această schiță contrastă listele de argumente; secțiunea următoare furnizează corpul adaptorului:

```csharp
// Intrări comune separate:
Functions.ChangeOwner(dog, previousOwners, newOwner);

// Intrare comună grupată:
Functions.ChangeOwner_Adapter(context, newOwner);
```

[00:10:04](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=604s) Fără context, fiecare apel privind proprietatea furnizează manual ambele bucăți comune, plus propriul proprietar nou. [00:10:25](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=625s) Operațiile existente folosesc intrări individuale, fie că au nevoie de o bucată comună sau de mai multe. Ele nu acceptă încă întregul context. [00:10:40](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=640s) Apelul dorit furnizează împreună datele comune disponibile și lasă operația să își aleagă ce are nevoie.

[00:10:48](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=648s) Apelanții pot apoi transmite `context`, în loc să precizeze câinele și istoricul la fiecare apel. [00:10:58](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=658s) Configurația specifică fiecărei operații rămâne distinctă: o operație de schimbare a proprietarului are nevoie de un **proprietar** nou, o redenumire de un **nume** nou, iar afișătoarele au nevoie doar de contextul de execuție. Menționarea trecătoare a „numelui nou” în discuția despre proprietate nu schimbă aceste dependențe.

[00:11:14](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=674s) Gruparea stării o face transmisibilă ca un singur argument către o funcție sau o configurație. [00:11:22](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=682s) Definirea structurii și a valorilor implicite într-o singură clasă elimină detaliile de configurare repetate; rulările independente pot crea în continuare instanțe independente de context. [00:11:34](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=694s) Fiecare acțiune poate adapta obiectul comun la propriile intrări, în loc ca fiecare loc de apelare să facă acea selecție.

[00:11:42](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=702s) Un **pipeline** rămâne succesiunea ordonată de operații. [00:11:50](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=710s) Îi creeăm contextul de execuție atunci când este nevoie de o rulare. Aceasta separă întrebarea „ce acțiuni sunt configurate?” de întrebarea „ce câine și ce istoric va folosi această rulare?”.

<details>
<summary>Ar trebui ca noul nume și lista de instrucțiuni să fie ambele câmpuri ale acestui Context?</summary>

Contextul comun de execuție din această lecție conține câinele și istoricul. Numele ales pentru o redenumire va fi memorat pe acel pas, iar pipeline-ul va păstra lista de instrucțiuni. Acestea sunt trei responsabilități separate.

</details>

## Construiește Context și adaptează funcțiile

[00:11:55](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=715s) Scriem clasa de context cu o listă de istoric inițial goală. [00:12:00](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=720s) Cerem un câine furnizat din exterior. [00:12:08](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=728s) Creeăm un context, îi furnizăm câinele și îl transmitem succesiunii alese pentru rulare.

[Codul salvat conex](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) conține acest model și adaptatoarele de mai jos. Această configurare scurtată arată noul container de stare și puntea pentru proprietate:

```csharp
var context = new Context
{
    Dog = new Dog { Name = "Luke" }
};

sealed class Context
{
    public List<Owner> PreviousOwners = [];
    public required Dog Dog;
}

// În interiorul lui Functions:
public static void ChangeOwner_Adapter(Context context, Owner? newOwner)
{
    ChangeOwner(context.Dog, context.PreviousOwners, newOwner);
}
```

**Expresia de colecție** `[]` creează istoricul gol în acest tip de listă declarat. **Initializerul de obiect** furnizează câinele required, iar valoarea implicită a istoricului este definită o singură dată în clasă.

[00:12:17](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=737s) Funcțiile inițiale își cunosc parametrii individuali, nu existența lui `Context`. Înregistrarea numește conținutul lor „logică pură”, adică operația concretă a fost separată de apelantul său. Ele modifică în continuare obiecte sau afișează ieșire; aceasta nu este o afirmație despre puritate funcțională.

[00:12:30](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=750s) O variantă este să rescriem toate operațiile să accepte direct contextul. [00:12:38](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=758s) Alternativa aleasă este un **adaptor**, un șablon de proiectare structural care prezintă interfața cerută, reutilizând vechea implementare.

[00:12:51](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=771s) Adaptorul pentru proprietate acceptă contextul și proprietarul care sosește. [00:13:02](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=782s) Toată puntea lui constă în extragerea câinelui și a istoricului și apelarea funcției existente. [00:13:16](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=796s) Un asemenea adaptor este util ori de câte ori datele disponibile trebuie convertite sau o submulțime selectată pentru altă interfață. Nu trebuie copiat niciun algoritm de proprietate în el.

[00:13:28](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=808s) Repetăm puntea pentru celelalte funcții. În aceeași sursă conexă, instrucțiunile lor esențiale de transmitere sunt:

```csharp
// În interiorul lui Functions:
public static void ChangeName_Adapter(Context context, string name)
{
    ChangeName(context.Dog, name);
}

public static void PrintOwners_Adapter(Context context)
{
    PrintOwners(context.PreviousOwners);
}

public static void PrintDog_Adapter(Context context)
{
    PrintDog(context.Dog);
}
```

Succesiunile vorbesc acum în termenii contextului comun. Fiecare adaptor deține mica decizie despre ce câmpuri îi trebuie operației pe care o încapsulează.

## Dă viitoarelor acțiuni o interfață comună

[00:14:07](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=847s) Adaptatoarele pregătesc fluxul de date, dar scopul principal rămâne: furnizarea pașilor înșiși din exterior. [00:14:09](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=849s) Un pipeline configurabil trebuie să trateze acțiunile ca pe valori. [00:14:20](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=860s) Doar extragerea funcțiilor lasă în urmă o funcție scrisă manual, care are ordinea și intrările specifice operației codificate în ea, urmată de încă un apel care o rulează.

[00:14:43](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=883s) Transmiterea unui tablou de acțiuni ar elimina nevoia de a scrie un runner nou pentru fiecare succesiune. [00:14:46](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=886s) Pentru a apela uniform feluri diferite de pași, introducem o **interfață**: un contract care spune ce poate face fiecare pas.

[Codul salvat conex](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) conține interfața de pas rezultată, ulterior extinsă cu introspecția. În această etapă de construcție, contractul ei are nevoie doar de execuție:

```csharp
interface IPipelineStep
{
    void Execute(Context context);
}
```

[00:15:01](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=901s) `I` inițial este o convenție de denumire din C# pentru interfețe; în Java numele omit de obicei această literă. Nu are niciun efect special asupra execuției. [00:15:05](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=905s) Fiecare pas primește același fel de context de execuție. Numele metodei și forma parametrilor sunt acum uniforme, deși munca diferă. Aceasta stabilește **polimorfismul**: apelanții pot folosi implementări diferite printr-un singur contract.

## Stochează o schimbare de proprietar ca obiect-pas

[00:15:08](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=908s) Transformăm acțiunea de schimbare a proprietarului în `ChangeOwnerPipelineStep`. [00:15:17](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=917s) Proprietarul care sosește devine **configurație la momentul construcției**, furnizată la crearea obiectului-pas. [00:15:20](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=920s) Facem clasa să implementeze interfața de pas. [00:15:29](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=929s) Metoda sa de execuție poate reutiliza adaptorul de proprietate deja scris.

[Implementarea salvată a pasului](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) are această formă. Introspecția este omisă aici, fiind adăugată mai târziu:

```csharp
sealed class ChangeOwnerPipelineStep : IPipelineStep
{
    public required Owner? Owner;

    public void Execute(Context context)
    {
        Functions.ChangeOwner_Adapter(context, Owner);
    }
}

// La un loc de apelare:
IPipelineStep step = new ChangeOwnerPipelineStep
{
    Owner = new Owner { Name = "Anton" }
};
```

[00:15:43](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=943s) Un **obiect-pas** reprezintă muncă viitoare: această valoare înseamnă „la execuție, setează proprietarul câinelui la Anton”. [00:16:03](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=963s) Poate fi păstrat într-o variabilă înainte de a se executa vreo acțiune. Crearea sa nu modifică niciun câine.

[00:16:08](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=968s) Există două surse de intrare. Pasul își păstrează proprietarul ales; `Execute` primește contextul comun de execuție la timpul rulării. Înregistrarea numește intrările externe memorate tot „context”, dar aici **configurația** le denumește mai precis. Câmpul salvat este inscriptibil: furnizarea unui proprietar inițial nu face ca acea referință să devină imutabilă.

[00:16:30](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=990s) Runner-ul va accepta obiecte-pas, nu apeluri directe de funcții. [00:16:40](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1000s) El așteaptă obiecte de tipul interfeței comune drept date de instrucțiune. [00:16:45](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1005s) Această clasă este așadar încă un **adaptor**: face puntea dintre funcțiile existente și un obiect care implementează contractul de execuție așteptat.

[00:16:55](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1015s) Acțiunile au trecut de la apeluri imediate la obiecte cu metode de execuție care pot fi dispecerizate. [00:17:02](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1022s) Implementarea unei interfețe este același concept general ca în Java, dar sintaxa declarației diferă: C# o listează după `:`. [00:17:05](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1025s) Nici `override`, nici `virtual` nu sunt necesare pe această metodă C# pentru a implementa interfața. **Dispecerizarea (dispatch)** prin `IPipelineStep` selectează implementarea concretă. Adnotarea `@Override` din Java este opțională, deci comparația rostită nu trebuie luată drept o cerință. Vezi [implementarea interfețelor în C#](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces) și [adnotarea Override din Java](https://docs.oracle.com/javase/specs/jls/se25/html/jls-9.html#jls-9.6.4.4). Suprascrierea metodelor de clasă este o problemă separată, reluată odată cu runner-ul.

## Transformă celelalte acțiuni în obiecte-pas

[00:17:16](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1036s) Repetăm conversia pentru redenumire: memorăm șirul ales și implementăm execuția apelând adaptorul existent. [00:17:26](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1046s) Numirea funcției de transmitere drept adaptor face vizibil rolul său de punte. [00:17:29](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1049s) Aplicăm aceeași construcție afișătoarelor. [00:17:49](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1069s) Rezultatul este o clasă de pas concretă pentru fiecare fel de acțiune.

[Clasele salvate conexe](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) conțin aceste implementări. Iată o acțiune configurabilă și una fără setări suplimentare:

```csharp
sealed class ChangeNamePipelineStep : IPipelineStep
{
    public required string Name;

    public void Execute(Context context)
    {
        Functions.ChangeName_Adapter(context, Name);
    }
}

sealed class PrintDogPipelineStep : IPipelineStep
{
    public void Execute(Context context)
    {
        Functions.PrintDog_Adapter(context);
    }
}
```

[00:17:52](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1072s) Creează mai întâi un pas, apoi execută-l odată ce există un context. Apelul de execuție furnizează starea rulării, iar câmpurile obiectului poartă setările proprii:

```csharp
IPipelineStep rename = new ChangeNamePipelineStep { Name = "Lucy" };
rename.Execute(context);
```

Aceasta este în continuare muncă C# obișnuită la timpul rulării. Schimbarea este că acțiunea și setările ei alese pot fi acum transmise în jur înainte de execuție.

## Colectează și execută pașii

[00:18:01](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1081s) Rescriem prima succesiune construind proprietari și valori de pas în ordine: schimbare de proprietar, redenumire, încă o schimbare de proprietar, apoi cele două afișătoare. Inițial fiecare valoare ar putea fi păstrată într-o variabilă separată și executată individual.

[00:18:40](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1120s) Pentru că valorile implementează aceeași interfață, o colecție le poate conține pe toate și poate invoca aceeași metodă pe fiecare. [00:19:06](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1146s) Înregistrarea numește această metodă virtuală: mecanismul relevant aici este dezvoltarea prin interfață, care alege implementarea pentru fiecare obiect-pas. [00:19:12](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1152s) Înlocuim apelurile numite individual cu un tablou de `IPipelineStep`.

[Fabrica salvată conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) păstrează compoziția de cinci pași într-o listă. Această reconstrucție arată etapa anterioară cu tablou:

```csharp
var owner1 = new Owner { Name = "Anton" };
var owner2 = new Owner { Name = "Alex" };

IPipelineStep[] steps =
[
    new ChangeOwnerPipelineStep { Owner = owner1 },
    new ChangeNamePipelineStep { Name = "Lucy" },
    new ChangeOwnerPipelineStep { Owner = owner2 },
    new PrintOwnersPipelineStep(),
    new PrintDogPipelineStep()
];

foreach (var step in steps)
    step.Execute(context);
```

Un **tablou** are lungime fixă. Tipul elementului este interfața, deci fiecare poziție poate conține o clasă implementantă diferită. [00:19:27](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1167s) **Bucla foreach** vizitează elementele memorate în ordine și transmite același context fiecăruia. Succesiunea de acțiuni a devenit date obișnuite, parcurse de un runner generic.

[00:19:33](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1173s) Rulăm succesiunea refactorizată ca să verificăm că această reprezentare se comportă în continuare conform intenției. [00:19:39](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1179s) Ieșirea înregistrată raportează schimbările de proprietar și proprietarii anteriori. [00:19:59](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1199s) Acțiunile finale de afișare arată rezultatul redenumirii configurate și al schimbărilor de proprietar. [00:20:07](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1207s) Citește separat cele două nume afișate: **Alex este proprietarul curent; Lucy este numele câinelui**. Nu sunt două nume atribuite proprietarului. Pornind fără proprietar, compoziția salvată de cinci pași lasă Anton în istoric, cu Alex curent și Lucy pe câine.

[Aceleași funcții salvate de afișare și modificare](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) susțin această așteptare scurtată; ea omite liniile de diagnostic, în loc să pretindă o transcriere exactă a consolei:

```text
Previous Owners:
Anton
Dog Information:
Name: Lucy
Owner: Alex
```

<details>
<summary>Ce se schimbă dacă PrintDog este mutat înaintea redenumirii?</summary>

Acel afișător raportează numele prezent în acel moment anterior. O redenumire ulterioară modifică în continuare câinele, dar nu poate modifica un text deja afișat. Ordinea listei face parte din comportamentul programului.

</details>

## Întoarce un pipeline dintr-o fabrică

[00:20:23](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1223s) Odată ce succesiunea devine date, un apelant o poate obține dintr-o metodă de creare separată. [00:20:30](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1230s) Introducem un obiect `Pipeline` care să păstreze pașii și o metodă publică care să îi execute. **Pipeline** denumește acum atât succesiunea ordonată, cât și clasa responsabilă de stocarea și rularea ei.

[00:20:43](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1243s) Discuția despre `virtual` privește moștenirea claselor: metodele obișnuite de clasă din C# sunt non-virtuale, dacă nu sunt declarate altfel. [00:20:52](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1252s) Metodele de instanță Java obișnuite, suprascriptibile, folosesc dispecerizare dinamică fără cuvântul cheie `virtual`, iar `final` împiedică suprascrierea. Această comparație nu schimbă apelurile de pas dispecerizate prin interfață. Vezi [metodele virtuale în C#](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual) și [metodele finale în Java](https://docs.oracle.com/javase/specs/jls/se25/html/jls-8.html#jls-8.4.3.3). În exemplul salvat, `Pipeline` este sealed, iar metoda sa `Execute` nu este virtuală.

[00:21:07](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1267s) Metoda de execuție parcurge pașii memorați și le oferă fiecăruia același context. [00:21:10](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1270s) Schimbăm funcția de creare astfel încât doar să construiască pipeline-ul. [00:21:15](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1275s) Declarăm `Pipeline` drept tip de întoarcere și inițializăm un obiect pipeline. [00:21:27](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1287s) Întoarcem acel obiect în loc să îl rulăm.

[Runnerul și fabrica salvate conexe](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) conțin această separare. Această fabrică abreviată folosește doi pași ca să păstreze vizibilă distincția:

```csharp
sealed class Pipeline
{
    public List<IPipelineStep> Steps = new();

    public void Execute(Context context)
    {
        foreach (var step in Steps)
            step.Execute(context);
    }
}

// În interiorul lui Functions:
public static Pipeline CreatePipeline()
{
    var pipeline = new Pipeline();
    pipeline.Steps.Add(new ChangeNamePipelineStep { Name = "Lucy" });
    pipeline.Steps.Add(new PrintDogPipelineStep());
    return pipeline;
}

// La apelant:
var configured = Functions.CreatePipeline();
configured.Execute(new Context { Dog = new Dog { Name = "Luke" } });
```

O **metodă fabrică** creează și returnează un obiect configurat. `return pipeline` transferă acel rezultat apelantului. Lista `List<IPipelineStep>` care poate crește înlocuiește tabloul anterior, cu lungime fixă, astfel încât conținutul să poată fi editat.

[00:21:43](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1303s) O a doua fabrică sau un al doilea loc de apelare poate construi o succesiune independent. [00:21:48](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1308s) Mutăm execuția completă la apelant, care furnizează și contextul. Numirea construirii „pură” aici înseamnă că nu rulează pipeline-ul; ea alocă în continuare obiecte și construiește o listă modificabilă.

Procedura rezultată este:

1. Construiește pașii cu setările lor.
2. Pune-i într-un pipeline și întoarce-l.
3. Creează sau alege contextul comun de execuție.
4. Cere pipeline-ului să se execute când este nevoie.

## Editează un pipeline între rulări

[00:21:59](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1319s) Același pipeline poate rula repetat fără a reconstrui obiectele-pas. Reutilizarea aceluiași context păstrează și câinele și istoricul așa cum le-au modificat rulările anterioare; execuția nu le resetează. [00:22:12](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1332s) După o rulare, un apelant poate edita succesiunea configurată pentru următoarea rulare.

[00:22:20](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1340s) Prima editare demonstrată elimină primul pas și adaugă altă acțiune la sfârșit. [Codul salvat conex](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) conține lista editabilă și fabrica, iar instrucțiunile următoare reconstruiesc acest experiment din editor:

```csharp
var pipeline = Functions.CreatePipeline();
pipeline.Execute(context);

pipeline.Steps.RemoveAt(0);
pipeline.Steps.Add(new ChangeNamePipelineStep { Name = "Luke" });
```

`RemoveAt` elimină elementul de la un **indice de la zero**: 0 înseamnă primul element. `Add` adaugă la sfârșit. Această operație schimbă lista de instrucțiuni; nu anulează niciun lucru deja efectuat asupra contextului.

[00:22:30](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1350s) Expunerea colecției permite apelanților să schimbe compoziția și ordinea acțiunilor. [00:22:43](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1363s) **Introspecția** începe cu examinarea obiectelor-pas configurate: parcurge lista, îi recunoaște intrările și decide pe care să le păstreze. [00:22:49](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1369s) Obține pipeline-ul implicit la timpul rulării, apoi inspectează și editează pașii concreți pe care îi conține.

[00:23:02](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1382s) Următoarea editare elimină a doua **schimbare de proprietar**. Aceasta înseamnă a doua apariție a acelui fel de acțiune, nu neapărat al doilea element al colecției. [00:23:08](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1388s) Adăugăm la sfârșit o schimbare de nume, ca să ruleze ultima. Pentru valoarea implicită de cinci pași din aceeași sursă conexă, acea schimbare de proprietar se află la indicele 2:

```csharp
var edited = Functions.CreatePipeline();
edited.Steps.RemoveAt(2); // A doua acțiune de schimbare a proprietarului din valoarea implicită.
edited.Steps.Add(new ChangeNamePipelineStep { Name = "Luke" });
edited.Execute(context);
```

Acest exemplu aplică editarea demonstrată valorii implicite salvate și inspectate; nu este o captură exactă a fiecărei liste temporare din editor. Alege indicele din colecția curentă reală.

[00:23:11](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1391s) Verificarea înregistrată confirmă că schimbarea de proprietar eliminată nu mai rulează și că redenumirea adăugată rulează la sfârșit. Afișătoarele aflate înaintea acelei redenumiri finale arată în continuare starea pe care o întâlnesc mai devreme. Schimbarea colecției schimbă comportamentul fără a mai necesita un alt runner scris manual.

<details>
<summary>Se comportă două execuții asupra aceluiași Context ca două rulări independente, de la zero?</summary>

Nu. Ele împart o stare modificabilă. Schimbările de proprietar pot adăuga din nou proprietari deja atribuiți, iar numele câinelui se păstrează. Creează un context nou pentru o stare de pornire independentă; reutilizează unul când intenția este să îi continui starea existentă.

</details>

## Inspectează descrierile și filtrează după tip

[00:23:21](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1401s) **Introspecția** examinează acțiunile configurate, în loc să le execute. [00:23:24](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1404s) O parcurgere care afișează tipurile pașilor dezvăluie ce obiecte sunt prezente. Ea nu dezvăluie automat toate setările lor memorate.

[00:23:32](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1412s) Raportarea setărilor necesită o metodă suplimentară de descriere. Aceasta este lăsată inițial pentru altă lucrare, apoi lecția implementează o versiune minimă pentru consolă. [00:23:47](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1427s) Adăugăm `Introspect`, astfel încât fiecare pas să se poată raporta. [00:24:11](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1451s) În timpul conversiei afișătoarelor, codul și numele copiate sunt ajustate, inclusiv `PrintOwners` și afișarea câinelui. Rezultatul salvat are două tipuri distincte: `PrintOwnersPipelineStep` afișează istoricul, iar `PrintDogPipelineStep` afișează câinele.

[00:24:20](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1460s) Adăugăm `Introspect` în interfață, astfel încât fiecare implementare de pas să trebuiască să îl furnizeze. [Interfața și clasele salvate conexe](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) arată acel contract și separarea afișătoarelor. Acest fragment arată cum devine inspectabil un nume configurat:

```csharp
interface IPipelineStep
{
    void Execute(Context context);
    void Introspect();
}

sealed class ChangeNamePipelineStep : IPipelineStep
{
    public required string Name;

    public void Execute(Context context)
    {
        Functions.ChangeName_Adapter(context, Name);
    }

    public void Introspect()
    {
        Console.WriteLine($"ChangeName({Name})");
    }
}

// Parcurgere pentru inspecție:
foreach (var step in pipeline.Steps)
    step.Introspect();
```

**Interpolarea șirurilor** folosește `$"..."` și acolade pentru a insera o valoare în ieșire. Aici descrierea include numele ales.

[00:24:30](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1470s) Acel nume este **configurație la momentul construcției**. Înregistrarea îl numește și „context”, dar în designul salvat `Introspect()` nu primește niciun context de execuție. Poate descrie o redenumire înainte de a i se furniza un câine. Descrierea este ceea ce implementarea alege să expună; inspecția nu dezvăluie în mod intrinsec fiecare detaliu privat.

[00:24:39](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1479s) Deoarece colecția conține obiecte de tipuri recognoscibile, apelanții pot elimina toți pașii unui fel ales. Aceeași sursă conexă păstrează acest exemplu ca comentariu:

```csharp
pipeline.Steps.RemoveAll(step => step is PrintOwnersPipelineStep);
```

**Testul de tip** `is PrintOwnersPipelineStep` întreabă dacă obiectul are acel tip. **Lambda** `step => ...` este o funcție scurtă folosită drept **predicat**: adevărat selectează o intrare pentru eliminare. `RemoveAll` elimină toate potrivirile, iar `RemoveAt` elimină o singură intrare indexată.

[00:24:57](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1497s) Verificarea înregistrată nu găsește niciun pas `PrintOwners` rămas după filtrare, chiar dacă au fost adăugate mai mulți. Alte feluri de pași rămân. Aceasta confirmă apartenența fără a efectua acțiunile pipeline-ului.

**Domeniu de aplicare:** implementarea afișează descrieri pe consolă. O reprezentare generalizată a datelor unui pas sau un cadru de introspecție mai larg nu este completat în video.

## Configurează pașii prin intrare de consolă

[00:25:15](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1515s) Pornim cu un pipeline gol și citim repetat o comandă de la consolă. Tipurile de pași cunoscute oferă un vocabular mic de acțiuni care pot fi construite la timpul rulării. [00:25:34](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1534s) Pentru `ChangeName`, citim o a doua linie cu numele ales și adăugăm un pas de redenumire configurat. [00:25:43](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1543s) Ordinea în care sunt introduse pașii devine ordinea execuției.

[00:26:03](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1563s) Adăugăm o comandă de execuție. [00:26:13](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1573s) Adăugăm o comandă de inspecție, ca utilizatorul să poată examina instrucțiunile acumulate înainte de a le rula.

[Bucla de consolă salvată conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82/examples/pipeline/Program.cs) include aceste ramuri și o comandă pentru proprietate. Această reconstrucție scurtată păstrează configurarea principală și fluxul de control:

```csharp
var pipeline = new Pipeline();

while (true)
{
    Console.Write("Enter step:");
    switch (Console.ReadLine())
    {
        case "ChangeName":
            pipeline.Steps.Add(new ChangeNamePipelineStep
            {
                Name = Console.ReadLine() ?? ""
            });
            break;
        case "PrintDog":
            pipeline.Steps.Add(new PrintDogPipelineStep());
            break;
        case "PrintOwners":
            pipeline.Steps.Add(new PrintOwnersPipelineStep());
            break;
        case "Introspect":
            foreach (var step in pipeline.Steps)
                step.Introspect();
            break;
        case "Execute":
            pipeline.Execute(new Context
            {
                Dog = new Dog { Name = "Luke" }
            });
            break;
        default:
            return;
    }
}
```

`while (true)` repetă până când controlul iese din buclă. `switch` alege un `case` după valoarea comenzii; `break` încheie ramura respectivă, iar `default` tratează celelalte valori. Aici `return` iese din punctul de intrare de nivel superior. `Console.ReadLine()` citește o linie și poate întoarce null. **Coalescența nulă**, `??`, furnizează șirul gol în acel caz.

Bucla completă conexă acceptă și `ChangeOwner`, citește un nume de proprietar, creează proprietarul și îl memorează pe un pas. Fiecare comandă `Execute` din această versiune salvată creează un context și un câine noi. Aceasta diferă de reutilizarea intenționată a aceluiași context în experimentul precedent.

Urmează interacțiunea înregistrată:

1. [00:26:24](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1584s) Introdu `ChangeName`, apoi `123`. Pasul memorează acel nume ca muncă viitoare.
2. [00:26:33](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1593s) Introdu `PrintDog` și `PrintOwners` pentru a adăuga acțiunile de afișare.
3. [00:26:34](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1594s) Alege inspecția. Ieșirea listează pașii configurați, confirmând că intrarea a construit succesiunea așteptată.
4. [00:26:35](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1595s) Invocă execuția ca să rulezi acele acțiuni în ordinea lor memorată.
5. [00:26:40](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1600s) Adaugă încă un `PrintDog` și execută din nou. Afișarea are loc de două ori: intrările duplicate sunt păstrate ca instrucțiuni separate.

Bucla de comenzi este o demonstrație mică, nu un sistem complet de validare a intrării sau de persistență. În sursa inspectată, comenzile nerecunoscute ies din program.

<details>
<summary>Introducerea lui ChangeName redenumește imediat un câine?</summary>

Ea construiește un pas care conține numele ales. Introspect descrie acea instrucțiune. Execute furnizează un context și efectuează acțiunea asupra câinelui său.

</details>

## Introdu o dependență de tip decorator

[00:26:43](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1603s) Reprezentarea pașilor individuali ca obiecte face posibil și atașarea unor lucrări suplimentare în jurul unei singure acțiuni alese. [00:26:51](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1611s) Un **decorator**, sau înveliș, conține un pas deja compatibil, implementează același contract și adaugă comportament, delegând lucrarea originală. Înregistrarea propune un pas care afișează descrierea unei acțiuni interioare și o execută.

[00:27:02](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1622s) Pasul `IPipelineStep` interior este o **dependență**: învelișul are nevoie de el ca să funcționeze, deci trebuie furnizat la construirea învelișului. Aceasta este **compoziție**, combinarea obiectelor prin păstrarea unuia în interiorul altuia.

[00:27:34](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1654s) Un constructor sau un câmp inscriptibil ar putea primi acea dependență. Alegerea de proiectare este să împiedicăm apelanții să o înlocuiască ulterior. [00:27:37](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1657s) Înregistrarea ia în considerare un membru public required, cu doar getter, drept mod de a permite inițializarea o singură dată. Această formulare are nevoie de o corectare de sintaxă: o proprietate required are nevoie de un accesor `set` sau `init` accesibil; o proprietate required cu adevărat doar-getter nu poate fi inițializată de apelant. [Regulile pentru proprietăți required](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required) stabilesc această constrângere.

[Decoratorul salvat final](https://github.com/AntonC9018/uniCourse_csharp/blob/df46c75bc2516786091ed07ebc48fe9c47b461c7/examples/pipeline/Program.cs) folosește în schimb un constructor și un câmp public readonly. Acest fragment arată memorarea dependenței sale; secțiunea următoare furnizează metodele:

```csharp
// Membri ai PrintStep_AndExecute_PipelineStep:
public readonly IPipelineStep Step;

public PrintStep_AndExecute_PipelineStep(IPipelineStep step)
{
    Step = step;
}
```

Un **constructor** inițializează obiectul pe măsură ce este creat. Un **câmp readonly** permite atribuirea la declarație sau în acel constructor, împiedicând înlocuirea ulterioară de către un apelant. Câmpul salvat este public, deci apelanții îl pot citi. Menținerea referinței fixe nu îngheață conținutul obiectului interior.

Accesorul `get` al unei proprietăți o citește; `set` permite atribuirea; `init` permite atribuirea în timpul inițializării. O proprietate cu doar getter, inițializată în constructor, este o altă modalitate de a expune o referință de citire. O proprietate `required` cu `get; init;` permite configurarea prin initializer de obiect; nu trebuie descrisă ca membru doar-getter sau ca o garanție universală de „exact o atribuire”.

## Rafinează raportarea din jurul execuției

[00:27:39](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1659s) Prima versiune de execuție rulează pasul interior și apoi inspectează învelișul. [00:27:59](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1679s) Următoarea modificare inspectează și pasul interior înainte de a-l rula. Acestea sunt stări succesive din video; nu trebuie confundate cu corpul final salvat.

[Metodele decoratorului salvate final](https://github.com/AntonC9018/uniCourse_csharp/blob/df46c75bc2516786091ed07ebc48fe9c47b461c7/examples/pipeline/Program.cs) păstrează descrierea interioară înaintea acțiunii, cu inspecția învelișului ca operație separată. Această comparație rezumă modificarea:

```csharp
// Prima versiune de Execute:
Step.Execute(context);
Introspect();

// Versiunea următoare adaugă raportarea înaintea acțiunii:
Step.Introspect();
Step.Execute(context);
Introspect();

// Corpul Execute salvat final:
Step.Introspect();
Step.Execute(context);
```

Clasa salvată implementează interfața comună de pas astfel:

```csharp
sealed class PrintStep_AndExecute_PipelineStep : IPipelineStep
{
    public readonly IPipelineStep Step;

    public PrintStep_AndExecute_PipelineStep(IPipelineStep step)
    {
        Step = step;
    }

    public void Execute(Context context)
    {
        Step.Introspect();
        Step.Execute(context);
    }

    public void Introspect()
    {
        Console.WriteLine("PrintName_AndExecute_ThingBelowMe");
        Step.Introspect();
    }
}
```

Executarea învelișului descrie pasul interior, apoi deleagă acțiunea cu același context. Inspectarea învelișului afișează propria etichetă, apoi descrie ce conține. Autodescrierea de după acțiune din prima versiune lipsește din metoda de execuție salvată. Implementarea păstrează raportarea simplă; un design de introspecție mai general este lăsat în afara acestei demonstrații.

## Compune și observă pași decorați

[00:28:24](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1704s) Creăm un pas de afișare obișnuit, apoi construim un înveliș în jurul lui. [Compoziția salvată finală](https://github.com/AntonC9018/uniCourse_csharp/blob/df46c75bc2516786091ed07ebc48fe9c47b461c7/examples/pipeline/Program.cs) include un ajutor și începe cu pași de redenumire și de afișare înveliți. Acest exemplu scurtat arată cum poate fi decorată orice acțiune compatibilă:

```csharp
IPipelineStep Wrap(IPipelineStep step)
{
    return new PrintStep_AndExecute_PipelineStep(step);
}

var plainPrinter = new PrintDogPipelineStep();
pipeline.Steps.Add(Wrap(plainPrinter));
pipeline.Steps.Add(Wrap(
    new ChangeNamePipelineStep { Name = "Lucy" }));
```

[00:28:44](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1724s) Runner-ul apelează în continuare același contract de pas. Când întâlnește un înveliș, descrierea apare înaintea acțiunii originale. [00:28:45](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1725s) Execuția înregistrată confirmă această ordine: o linie suplimentară de descriere precedă comportamentul de afișare. Afișătorul obișnuit este reutilizat fără a-i modifica implementarea.

[00:29:00](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1740s) Un înveliș poate primi orice `IPipelineStep`, inclusiv alt înveliș. Descrierea „numelui” unui pas nu impune ca fiecare clasă să declare un câmp `Name` literal; această implementare folosește `Introspect` drept contract de descriere.

[00:29:17](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1757s) Înfășurarea lui `ChangeName` sau `PrintDog` face ca acea acțiune să se raporteze singură, pe lângă treaba sa obișnuită. [00:29:35](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1775s) Inspecția finală înregistrată arată acțiunea de raportare exterioară conținându-și pasul interior. Execuția include „Выполняю этот шаг” („Execut acest pas”). Acea linie în limba rusă este o observație din înregistrare; commitul salvat folosește șiruri de descriere în engleză.

<details>
<summary>Ce deosebește adaptorul anterior de acest decorator?</summary>

Adaptorul face puntea unei nepotriviri: funcțiile existente iau intrări separate, în timp ce runner-ul așteaptă un pas bazat pe interfață, cu un parametru Context. Decoratorul primește un pas deja compatibil și adaugă raportare, păstrând acel contract.

</details>

## Istoricul modificărilor de cod

Legăturile din video indică stări tranzitorii din editor; cele două commituri inspectate păstrează stări salvate ale lecției. Reconstrucțiile anterioare sunt legate de aceste commituri, nu de instantane exacte din commituri.

- [00:01:42](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=102s) — Începe modelul câine/proprietar și proprietatea opțională.
- [00:04:37](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=277s) — Extrage funcția locală care centralizează înlocuirea proprietarului și istoricul.
- [00:11:55](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=715s) — Construiește contextul comun de execuție și adaptează funcțiile existente.
- [00:15:08](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=908s) — Reprezintă acțiunile ca obiecte care implementează interfața de execuție.
- [00:19:12](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1152s) — Execută o colecție în locul apelurilor numite individual.
- [00:21:10](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1270s) — Separă construirea unui pipeline de execuția sa de către apelant.
- [00:25:15](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1515s) — Configurează și inspectează pașii din intrarea de consolă; [d6e14fd — exemplu](https://github.com/AntonC9018/uniCourse_csharp/commit/d6e14fd023172f33faba0c4e0be4e8ea62b0ec82) păstrează acest design salvat.
- [00:28:24](https://www.youtube.com/watch?v=X4IDL3CmV-A&t=1704s) — Compose acțiuni cu învelișul de raportare; [df46c75 — pași](https://github.com/AntonC9018/uniCourse_csharp/commit/df46c75bc2516786091ed07ebc48fe9c47b461c7) adaugă decoratorul și este starea finală inspectată a lecției.

Comparația compactă de cod de lângă acele referințe imutabile este:

```csharp
// d6e14fd: obiect-pas obișnuit.
pipeline.Steps.Add(new PrintDogPipelineStep());

// df46c75: îl compunem cu un înveliș de raportare.
pipeline.Steps.Add(
    new PrintStep_AndExecute_PipelineStep(new PrintDogPipelineStep()));
```
