> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Proprietăți dinamice, chei tipizate și registre

[00:00:00](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=0s) Această lecție construiește un mecanism prin care codul de execuție reutilizabil poate transporta date alese de cel care îl apelează. Pornim de la parcurgerea unui arbore și de la sistemele unui joc, construim o conductă (pipeline) și schimbăm repetat reprezentarea contextului ei, până când pași independenți pot împărți același context fără să împrăștie conversii de tip și fără să reutilizeze accidental aceeași cheie.

Termenii folosiți în acest parcurs sunt:

- **Abstrație și parcurgere:** o abstracție expune o operație ascunzând-i implementarea; parcurgerea este mecanismul care vizitează nodurile unui arbore sau pașii unei conducte într-o ordine stabilită.
- **Strategie și delegat:** o strategie furnizează comportamentul pe care abstracția îl aplică. Un delegat este o valoare de funcție invocabilă; o interfață poate exprima o strategie care are nevoie de mai multe callback-uri.
- **Context și proprietăți dinamice:** contextul este datele transportate de-a lungul unei execuții. Proprietățile dinamice sunt intrări pe care apelanții le atașează acelui context fără a adăuga câmpuri clasei sale declarate. Aici „dinamic” descrie o stocare extensibilă, nu cuvântul-cheie `dynamic` din C#.
- **Conductă, pas și constructor (builder):** o conductă rulează o mulțime ordonată de pași; un pas execută o singură operație; un constructor adună configurația înainte de a construi conducta.
- **Entitate, sistem, capabilitate și componentă:** o entitate reprezintă un obiect de joc prin datele sale. Un sistem procesează entitățile care au capabilitățile de care are nevoie. O componentă grupează date înrudite, cum ar fi conținutul inventarului sau viața.
- **Stare mutabilă și imutabilă:** datele mutabile se pot schimba după creare; configurația imutabilă rămâne fixă. Un mutex sau un lock serializează accesul la stare mutabilă partajată. O cursă (race) apare când accese suprapuse produc un rezultat dependent de ordine; contendenția este așteptarea cauzată de sincronizare.
- **Structură „fat”:** un singur container de date declarat static include toate proprietățile de care aplicația ar putea avea nevoie, chiar dacă o anumită operație folosește foarte puține dintre ele.
- **Bază abstractă, interfață și conversie de tip (cast):** o bază abstractă este un contract de moștenire care nu poate fi instanciat ca atare. O interfață descrie membrii obligatorii. Un cast cere o valoare ca fiind de alt tip și poate eșua la runtime.
- **Generice și inferența tipurilor:** un tip sau o metodă generică are un parametru de tip precum `TContext` sau `T`; inferența tipurilor permite compilatorului să determine un argument dintr-un apel. Siguranța statică a tipurilor înseamnă că tipurile incompatibile pot fi respinse înainte de execuție.
- **Contravarianță și covarianță:** varianța guvernează conversiile între interfețe sau delegați generici construiți. Contravarianța permite ca un consumator al unui tip de referință mai larg să fie folosit drept consumator al unuia mai îngust; covarianța funcționează în direcția de ieșire.
- **Decorator:** o învelitoare adaugă lucru înainte sau după o operație existentă, de exemplu calcularea adâncimii înainte de a o folosi o strategie învelită.
- **Cheie, tablou și dicționar:** o cheie identifică o intrare; un tablou stochează celule numerotate, iar un dicționar asociază chei cu valori. Stocarea pozițională rară (sparse) rezervă goluri nefolosite. Capacitatea este o indicație de alocare. O coliziune apare când intrări fără legătură folosesc aceeași identitate; o cheie cu spațiu de nume (namespaced) include un prefix de proprietate.
- **Cheie tipizată:** `Key<T>` transportă o identitate împreună cu tipul valorii stocate sub ea, cunoscut la compilare. Cheia identifică datele; datele rămân în context.
- **Obiect, tip de valoare, tip de referință și boxing (încapsulare):** un slot `object` poate ține valori de tipuri diferite. Un tip de valoare precum `int` își reprezintă direct datele; un tip de referință precum o clasă sau un șir se accesează printr-o referință la obiect. Boxing-ul înfășoară o valoare de tip valoare într-o reprezentare de obiect.
- **Flyweight, pool și ECS:** un flyweight partajează obiecte reutilizabile; un pool reutilizează obiecte alocate. Entity Component System (ECS) organizează entități, date de componente și sisteme, de obicei stocând datele după tipul componentei.
- **Registru, singleton și metadate:** un registru centralizează crearea cheilor. Un singleton este o instanță partajată, care poate fi totuși referită prin variabile de instanță obișnuite. Metadatele descriu o cheie, de exemplu numele sau tipul valorii sale; introspecția citește această descriere la runtime.
- **Nulabilitate și constrângeri:** nulabilitatea exprimă dacă o valoare poate lipsi. O constrângere generică restricționează argumentele de tip permise; `notnull` exprimă o așteptare de tip nenul. Operatorul de „anulare a nullului” `!` suprimă un avertisment de nulabilitate fără a verifica valoarea.
- **Constante și limite de acces:** un `const` este o constantă la timp de compilare; un câmp `static readonly` poate ține o identitate creată o singură dată la runtime. O asamblare este o unitate compilată precum o DLL de bibliotecă; `internal` restrânge accesul la acea unitate. Un indexer este accesul prin paranteze pătrate precum `items[key]`.
- **Hash și mulțimi de hash:** un calcul de hash mapează o cheie la un număr folosit pentru organizarea căutării. O mulțime de hash urmărește valori distincte și raportează dacă o inserare adaugă una nouă.
- **Sintaxa de inițializare:** un inițializator de obiect atribuie membrii imediat după construcție. `required` impune inițializarea unui membru; `init` permite atribuirea în timpul inițializării. O santinelă este o valoare rezervată care înseamnă că o proprietate lipsește.
- **Date de joc:** poziția în lume localizează o entitate; un sprite este imaginea ei; un shader este cod de randare, cum ar fi codul care desenează valurile apei. Proprietățile fizice descriu modul în care o entitate interacționează cu mișcarea sau coliziunile.

[Laboratorul despre proprietăți](../../../labs/2_design/04_properties.md) este exercițiul conex. Acesta îți cere să separi contextul dinamic al unei biblioteci de datele și operațiile definite de consumatori. Prezentarea sa scurtă spune că un registru întreg nu apare în video, dar această înregistrare chiar discută și demonstrează acel design începând cu [01:08:02](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4082s); înregistrarea stabilește acoperirea lecției aici.

**Domeniu de aplicare.** Implementarea trece de la un context fix, prin moștenire, interfețe, generice, până la intrări dinamice într-un dicționar, chei tipizate și înregistrare. Nu implementează un joc complet, un ECS, un pool de flyweight-uri, alocare atomică a cheilor sau registre specializate pe context. Alternativele respective explică limitele de proiectare, nefiind cadre completate. Exemplele simplificate de mai jos reconstruiesc stările didactice cu nume coerente; versiunile de repository inspectate sunt marcate explicit ca **cod conex**, fiind exemple salvate diferite și nu copii exacte ale fiecărei stări de pe ecran. Rezultatele de compilare și de ieșire raportate sunt rezultatele înregistrării, nu teste rulate din nou.

## Parcurgere și o strategie conștientă de adâncime

[00:00:04](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4s) O abstracție de parcurgere a arborelui știe cum să viziteze nodurile. Nu ar trebui să decidă și tot ce se poate întâmpla util cu un nod. Furnizarea acelui comportament din exterior face parcurgerea reutilizabilă: azi adunăm numere, mâine modificăm noduri, mai târziu strângem un raport.

[00:00:22](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=22s) Operația aplicată la fiecare nod este **strategia**, comportamentul ales. Un delegat simplu poate adăuga numărul fiecărui nod la o sumă curentă. **Contextul** este starea care supraviețuiește între vizite: inițializezi `Sum` cu zero, apoi acumulezi în ea. Păstrarea sumei într-un câmp al unui obiect strategie este o modalitate de a împacheta operația împreună cu datele ei.

[00:01:11](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=71s) O strategie mai exigentă are nevoie și de adâncimea nodului, nu doar de nod. Un singur delegat care primește doar nodul nu furnizează informații despre poziția în parcurgere. O interfață poate expune în schimb un callback de intrare în copil și unul de ieșire spre părinte. Callback-urile ordonate permit strategiei să mențină adâncimea fără să inspecteze forma arborelui.

[00:01:46](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=106s) Acumularea ponderată cu adâncimea adună `node.Value * depth`. Primul nivel folosește coeficientul 1, al doilea 2, iar al treilea 3. Schița vorbită menționează valori precum 1, 2, 4 și 6; acestea sunt produse de exemplu, cum ar fi `2 * 2 = 4` și `2 * 3 = 6`, nu o regulă potrivit căreia patru noduri cu valoarea 1 produc aceste patru contribuții.

[00:02:03](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=123s) Operația reconstruită este:

```csharp
sealed class WalkContext
{
    public int Depth;
    public int Sum;
}

// Schiță: intră în rădăcină ca nivel 1 și echilibrează fiecare intrare cu o ieșire.
void Enter(WalkContext c) => c.Depth++;
void Visit(int nodeValue, WalkContext c) => c.Sum += nodeValue * c.Depth;
void Exit(WalkContext c) => c.Depth--;
```

[00:02:17](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=137s) Increment la intrare, decrement la ieșire. Adâncimea aparține contextului partajat la fel ca suma, pentru că ambele trebuie să persiste între vizitele nodurilor. Parcurgerea deține ordinea callback-urilor; strategia deține semnificația lor. Codul de mai sus este o schiță bazată pe video, nu o implementare găsită în repository.

## Trecerea contextului printr-o abstracție fixă

[00:02:31](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=151s) O parcurgere asociată cu o strategie nu explică încă pe deplin proprietățile dinamice. Cazul dificil este o **grupare fixă**: abstracția și strategia ei au fost deja configurate împreună, dar un apelant exterior trebuie să furnizeze datele pentru o anumită rulare.

[00:02:51](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=171s) Apelantul nu invocă strategia direct. Parcurgerea o invocă. Anterior, strategia și o referință la contextul ei puteau circula ca un singur obiect, cu suma curentă stocată în implementarea strategiei. Odată ce acea strategie este fixată în grupare, atașarea datelor la momentul construirii este prea devreme pentru date specifice fiecărei rulări.

[00:03:50](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=230s) Furnizarea contextului la construirea întregii grupări poate împinge comunicarea către globale sau către altă stare capturată partajată. Soluția de ocolire este să serializezi rularea cu un mutex și să lași toată comunicarea să treacă prin acea stare partajată. Aceasta permite unui singur fir să ruleze la un moment dat, dar transformă lucrul independent în acces sincronizat la aceleași date.

[00:04:36](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=276s) Fluxul dorit este altul: apelantul furnizează contextul abstracției; abstracția îl transportă prin parcurgere; strategia îl citește sau îl modifică; apelantul inspectează rezultatul. Rezultatul ar putea descrie structura de date, sau operația ar putea adăuga un `X` ales de apelant la fiecare nod.

[00:05:04](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=304s) În versiunea parametrizată, operația pregătește datele folosite de pașii următori în loc să îi apeleze ea însăși pe ei. Prin urmare, `X` circulă în context: un apelant nu îl poate transmite direct unui callback pe care îl apelează doar parcurgerea.

[00:05:36](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=336s) Utilizatorul alege pașii strategiei, iar abstracția îi encapsulează aplicarea asupra structurii. Acești pași au totuși nevoie de date care pot veni doar din exterior. Aceasta este condiția relevantă pentru aplicarea abordării cu context. Subtitrarea de lângă [00:05:58](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=358s) este ilizibilă; ideea recuperabilă este contextul furnizat din exterior, nu o regulă economică sau de programare suplimentară.

## O conductă separă ordinea de date

[00:06:12](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=372s) O **conductă** este o succesiune ordonată de funcții sau obiecte-pas. Parcurgerea ei este pur și simplu o buclă; pașii configurați furnizează strategia. Fiecare pas primește același context de execuție, îl poate actualiza și lasă informații pe care pașii ulteriori sau apelantul le pot inspecta.

[Conducta de tip istoric al proprietarului (cod conex)](https://github.com/AntonC9018/uniCourse_csharp/blob/a386c9c8c77cc4f67311059663399b5b793ed47d/examples/pipeline_library/Pipeline.cs) conține o buclă `Execute` ordonată și un context care transportă un câine și proprietarii anteriori. Un exemplu scurt face separarea vizibilă:

```csharp
void Execute(Context context)
{
    foreach (var step in steps)
        step.Execute(context);
}

// Apelantul poate inspecta informațiile acumulate după execuție.
pipeline.Execute(context);
Console.WriteLine(context.PreviousOwners.Count);
```

[00:06:40](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=400s) Bucla stabilește ce pas este apelat în continuare. Ea transmite contextul fără să interpreteze datele domeniului. În exemplul anterior, conducta acumulează proprietarii anteriori, astfel încât rezultatul și numărul foștilor proprietari pot fi raportate împreună.

[00:07:07](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=427s) Apelantul poate invoca parcurgerea, dar nu îi modifică implementarea. Apelantul controlează pașii la construirea conductei; în timpul unei rulări, apelantul alege contextul, nu reconstruiește strategia.

[00:07:44](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=464s) O listă fixă poate fi parcursă repetat. Aceeași configurație a conductei servește la multe rulări, iar fiecare rulare primește propriile date. De aceea strategia se aplică prin abstracție și nu prin apelarea directă a fiecărui pas.

[00:08:20](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=500s) Dacă bucla ar examina câmpurile de sumă, nume sau proprietar ca să decidă cum să execute, ar deveni cuplată la acest domeniu. La [00:08:36](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=516s) ea doar predă contextul neschimbat fiecărui pas; la [00:08:46](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=526s) logica de parcurgere rămâne identică indiferent de intrare. Această indiferență este motivul pentru care un context poate deveni adesea **dinamic**: conținutul extensibil contează pentru pași, iar parcurgerea trebuie doar să transporte containerul.

## Entitățile transportă mulțimi diferite de date

[00:09:05](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=545s) Un joc oferă un exemplu mai amplu de context extensibil. O **entitate** este un obiect de joc descris prin date, iar entități diferite au nevoie de combinații diferite:

- [00:09:19](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=559s) Iarba decorativă are nevoie de o **poziție în lume** care să o localizeze, de culoare și de o imagine sau **sprite**.
- [00:09:26](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=566s) Apa poate avea nevoie de un **shader** de randare a valurilor, adică cod care desenează suprafața, și de un parametru de încetinire, alături de poziție și de **proprietăți fizice** care descriu interacțiunile la [00:09:39](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=579s).
- [00:09:49](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=589s) Un jucător are nevoie de atac, viteză și viață.
- [00:09:57](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=597s) Un inventar stochează și folosește obiecte. El poate fi referit ca obiect sau sistem auxiliar, nu stocat ca trei câmpuri primitive.
- [00:10:06](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=606s) Un zombi poate avea un inventar redus, care măcar să suporte echiparea obiectelor.
- [00:10:23](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=623s) Un zombi obișnuit și un zombi cu capacități magice au în comun unele statistici, dar diferă prin datele suplimentare pe care le transportă.

[00:10:37](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=637s) Aceste exemple motivează tratarea uniformă a plantelor, jucătorilor, zambilor și apei ca entități. „Uniform” înseamnă că sistemele își pot inspecta datele printr-o reprezentare comună; nu înseamnă că fiecare entitate are fiecare proprietate.

[Exemplul conex de moștenire](https://github.com/AntonC9018/uniCourse_csharp/blob/d5af1fb372be56a453e3b139990013c28e50198c/examples/property_dict/Subclassing.cs) exprimă o capabilitate magică printr-o interfață și date grupate:

```csharp
public interface IMage
{
    MageTrait Mage { get; }
}
public sealed class MageTrait
{
    public int Intelligence;
    public int WandPower;
    public int Spirit;
}
```

[00:10:44](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=644s) Un context de joc conține starea entităților, ca să aibă sistemele undeva ce citi și ce actualiza. La [00:10:56](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=656s) reprezentarea comună intenționată pentru entități trebuie să suporte orice, de la poziție, sprite și culoare până la animații, statistici, obiecte de inventar și un obiect ținut în mână. O **capabilitate** sunt datele sau contractul care face ca o operație să aibă sens pentru o entitate.

## Sistemele aplică strategia de joc

[00:11:22](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=682s) Un **sistem** execută o anumită parte a simulării folosind datele entităților din contextul de joc. Un sistem alege ținte de atac dintre inamici; altul aplică atacurile la [00:11:29](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=689s). Un sistem de inventar gestionează operațiile pe care utilizatorul vrea să le facă acum la [00:11:43](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=703s). Chiar și „încetinește jucătorul în apă” trebuie implementat de un sistem la [00:11:53](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=713s).

[00:12:03](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=723s) Proiectarea constă în funcționalitate independentă care operează pe un context partajat. Fiecare sistem așteaptă datele necesare muncii sale. Fie le cere acele date, fie filtrează entitățile care nu le au la [00:12:37](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=757s). Un sistem magic ignoră tufișurile fără capabilitate magică la [00:12:44](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=764s). Datele de capabilitate trăiesc alături de celelalte date ale entității la [00:12:51](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=771s).

[00:12:59](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=779s) Intenția utilizatorului intră prin conținutul entităților, iar sistemele activate sau dezactivate sunt o altă parte a configurației strategiei la [00:13:03](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=783s). Strategia fixă a lecției descrie sistemele selectate pentru o configurație de execuție stabilită. Nu face ca fiecare dată de juc să fie imutabilă.

[00:13:12](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=792s) Un sistem este scris pentru contextul partajat, nu ca să fie invocat separat de apelant pentru o entitate aleasă. La [00:13:24](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=804s) un sistem magic caută toate entitățile care folosesc acum magia și acționează asupra lor. Abstracția aplică strategia.

[00:13:42](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=822s) Sistemele au nevoie de context din exterior, pentru că entitățile dețin starea și alegerile pe care sistemele le inspectează. În acest sens, entitățile însele funcționează drept context la [00:13:59](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=839s): utilizatorul influențează simularea autonomă prin date.

[00:14:07](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=847s) Exemplul de joc urmărește să producă o simulare și experiența jucătorului; prin urmare, sistemele rulează autonom. Exemplul conductei de la [00:14:28](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=868s) evidențiază în schimb o cerere explicită de a parcurge o listă ordonată și de a transmite contextul mai departe. La [00:14:40](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=880s) ideea comună rămâne: fiecare entitate sau context deține proprietăți citite de comportamente externe.

## Pornim de la un contract fix pentru conductă

[00:15:01](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=901s) Un proiect nou transformă discuția anterioară în cod. Pornim de la o conductă al cărei apelant furnizează contextul, inclusiv tipul său declarat. Pentru moment, reprezentarea contextului este fixă; schimbarea ei este un pas ulterior, la [00:15:20](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=920s).

[00:15:26](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=926s) O **interfață de pas** oferă un singur punct de intrare uniform, care primește contextul. Pasul citește intrarea de care are nevoie sau își inițializează lucrarea din acea intrare; conducta rulează o succesiune ordonată fixă. [Contractul conex de bibliotecă](https://github.com/AntonC9018/uniCourse_csharp/blob/a386c9c8c77cc4f67311059663399b5b793ed47d/examples/pipeline_library/Pipeline.cs) are această formă, scurtată aici:

```csharp
public interface IPipelineStep
{
    void Execute(Context context);
}

public sealed class Pipeline
{
    public void Execute(Context context)
    {
        foreach (var step in steps)
            step.Execute(context);
    }
}
```

[00:15:42](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=942s) Abstracția acceptă contextul din exterior și îl transmite fiecărui pas. Apelul `pipeline.Execute(context)` nu cere niciodată apelantului să caute un anumit pas și să îl alimente direct pe acesta.

[00:16:03](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=963s) Operațiile și sistemul de execuție pot fi separate, sau obiectele-operație pot transporta deja datele lor. În această formă combinată, un parametru de context explicit pentru fiecare apel este inutil. Lecția încearcă intenționat mai întâi această formă mai simplă, ca limitarea ei să devină vizibilă.

## Construim mai întâi un salut cu date capturate

[00:16:15](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=975s) Eliminăm temporar contextul, mutăm structura conductei într-un fișier separat și lăsăm câteva handler-e simple în fișierul principal. La [00:16:26](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=986s) această separare izolează parcurgerea reutilizabilă de codul de demonstrație. Un handler este un obiect-pas a cărui metodă `Execute` efectuează operația.

[00:16:32](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=992s) Dacă `Execute()` nu are parametru de intrare, numele salutului trebuie furnizat la construirea pasului și păstrat într-un câmp. Nu a fost găsit niciun commit salvat exact pentru această demonstrație fără parametru. [Configurarea conexă a pasului capturat](https://github.com/AntonC9018/uniCourse_csharp/blob/a386c9c8c77cc4f67311059663399b5b793ed47d/examples/pipeline_library/Pipeline.cs) stochează în schimb configurații precum un nume de înlocuire în câmpurile pasului, păstrând în continuare un context explicit. Starea inițială simplificată, bazată pe video, este:

```csharp
sealed class GreetingStep
{
    public string Name;
    public GreetingStep(string name) => Name = name;
    public void Execute() => Console.WriteLine($"Hello, {Name}");
}

// Schiță de utilizare a constructorului: construiește pasul, construiește conducta, execută.
var greeting = new GreetingStep("Антон");
var builder = new PipelineBuilder();
builder.Add(greeting);
var pipeline = builder.Build();
pipeline.Execute();
```

[00:16:52](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1012s) Construirea este o procedură:

1. Creezi pasul cu datele de care are nevoie.
2. Adaugi acel pas la constructor și construiești conducta.
3. Execuți conducta configurată, care apelează pasul.

[00:17:08](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1028s) Rularea înregistrată afișează `Hello, Антон`, ceea ce confirmă că pasul a primit numele și că parcurgerea a ajuns la el. La [00:17:17](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1037s), folosirea aceluiași sistem de trei ori repetă datele capturate; schimbarea numelui este incomodă pentru că a fost fixată la construire.

[00:17:38](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1058s) Datele de execuție stocate într-un pas creat o singură dată se comportă, față de utilizatorii săi, ca o stare globală partajată. Nu este literal o variabilă globală C#; problema este durata de viață și reutilizarea partajată.

[00:17:48](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1068s) Demonstrația creează structura în avans cu „Антон”, apoi schimbă datele stocate în „собака” sau „кошка”. La [00:18:05](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1085s), schimbarea contextului de execuție înseamnă pătrunderea în obiectul strategie și modificarea câmpului său. Aceasta amestecă configurația reutilizabilă a strategiei cu datele unei rulări anume.

## Pasii mutabili partajați cer blocare

[00:18:22](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1102s) Configurația conductei este gândită să fie **imutabilă**: după construcție, pașii aleși și ordinea lor rămân fixe. O intrare mutabilă încapsulată în acești pași subminează acest obiectiv. Dacă două locuri, ideal două fire, îl reutilizează la [00:18:37](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1117s), atunci partajează și datele de execuție încapsulate.

[00:18:56](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1136s) Instanța mutabilă existentă are nevoie de un blocaj în jurul folosirii sale. Secțiunea critică este mai mare decât doar atribuirea unui nume:

1. [00:19:06](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1146s) Obții blocajul pentru această instanță de conductă.
2. Setezi datele de context ale rulării.
3. Execuți imediat, în timp ce ții încă blocajul.
4. Eliberezi blocajul doar când execuția care folosește acele date s-a terminat.

[00:19:23](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1163s) Altfel, firul A poate seta „собака”, firul B îl poate înlocui cu „кошка”, iar firul A poate executa salutul greșit. Aceasta este o **cursă (race)**, un rezultat determinat de interclasarea operațiilor pe stare partajată.

[00:19:37](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1177s) Blocarea evită acea cursă, dar produce **contendență**: alt fir rămâne blocat până la terminarea întregii execuții. Separarea contextului de rulare de configurația fixă a strategiei evită această serializare anume.

[00:19:49](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1189s) Transmite intrare proaspătă din exterior la fiecare apel `Execute`, în loc să o capturezi la crearea conductei. Regula se aplică oricărei abstracții cu această formă, nu doar unei conducte, la [00:20:00](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1200s).

[00:20:08](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1208s) Numele ar trebui să sosească în momentul invocării. Numai că apelantul invocă întreaga conductă, așa că un parametru de nume nu poate fi direcționat către un singur pas encapsulat. Un parametru de context comun este drumul către acel pas, la [00:20:16](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1216s). Contextele separate nu fac nici ele automat tot codul thread-safe: pașii trebuie să evite altă stare de execuție mutabilă partajată, iar apelanții trebuie să evite modificarea simultană a aceluiași context.

## Trecem un context proaspăt pentru fiecare execuție

[00:20:25](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1225s) Creezi un **context**, obiectul care conține datele pentru o execuție. Îl treci în `Execute`-ul conductei; conducta îl transmite fiecărui pas. La [00:20:32](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1232s) adaugi un câmp de nume la contextul inițial gol și muți acolo accesul la datele salutului.

[Conducta conexă cu context explicit](https://github.com/AntonC9018/uniCourse_csharp/blob/a386c9c8c77cc4f67311059663399b5b793ed47d/examples/pipeline_library/Pipeline.cs) transportă date furnizate de apelant prin pași. Această reconstrucție mai mică arată refactorizarea salutului:

```csharp
sealed class Context
{
    public required string Name;
}
sealed class GreetingStep : IPipelineStep
{
    public void Execute(Context context) =>
        Console.WriteLine($"Hello, {context.Name}");
}

// Aceeași conductă configurată poate primi intrări diferite.
pipeline.Execute(new Context { Name = "собака" });
pipeline.Execute(new Context { Name = "кошка" });
```

[00:20:41](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1241s) Pasul își citește acum numele din obiectul primit. Poți crea un context proaspăt pentru fiecare utilizare, sau poți reutiliza un context și înlocui datele întretre rulări finalizate, la [00:21:00](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1260s). Reutilizarea este o decizie a apelantului; partajarea lui între rulări simultane ar reintroduce o cursă.

[00:21:12](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1272s) Construirea obiectului inline, în apel, elimină o variabilă intermediară ne-necesară. Un **inițializator de obiect**, acoladele cu atribuirile de membri după `new`, setează câmpul înainte de execuție. `required` cere locurilor de construcție să inițializeze membrul. La [00:21:40](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1300s) pasul este încă legat static de acest tip de context anume: numele cerut trebuie să fie un membru declarat. Am separat duratele de viață, dar nu am făcut încă extensibilă reprezentarea datelor.

## Un context mare ajunge la limita bibliotecii

[00:21:54](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1314s) Salutul este doar una dintre conductele posibile. O a doua conductă are nevoie de o sumă, așa că un context partajat fix trebuie să câștige un câmp fără legătură la [00:22:07](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1327s).

[Exemplul conex de structură „fat”](https://github.com/AntonC9018/uniCourse_csharp/blob/d5af1fb372be56a453e3b139990013c28e50198c/examples/property_dict/FatStruct.cs) pune toate câmpurile cunoscute într-o singură clasă de entitate și marchează valorile numerice absente cu **santinele**, valori rezervate care indică faptul că o proprietate nu este prezentă. Versiunea simplificată pentru conductă este:

```csharp
sealed class Context
{
    public string Name = "";
    public int Sum;
}
// Salutul folosește Name; aritmetica folosește Sum.
void AddFive(Context c) => c.Sum += 5;
void AddTen(Context c) => c.Sum += 10;
```

[00:22:27](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1347s) Construiești conducta aritmetică din doi pași, adăugând cinci, apoi zece. O execuți cu un context nou creat, apoi afișează câmpul numeric rezultat la [00:22:57](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1377s); narrațiunea numește câmpul rezultat „id”. Nu deduce un algoritm de identificatori din această denumire rostită: demonstrația privește date aritmetice acumulate.

[00:23:07](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1387s) Înregistrarea raportează că demonstrația curentă nu funcționează. Aceasta duce la problema de proiectare de la [00:23:10](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1390s): un context a fost extins să conțină atât date de salut, cât și date aritmetice. Este o eșecare intermediară raportată, nu dovada că o clasă cu două câmpuri ar fi invalidă în C#.

[00:23:24](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1404s) Amestecarea câmpurilor nu este imposibilă prin ea însăși. Inițializezi membrii nefolosiți și lași fiecare conductă să îi ignore. La [00:23:32](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1412s) o variantă este să pui toate câmpurile de care aplicația ar putea avea vreodată nevoie într-un singur context mare.

[00:23:40](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1420s) Asta se practică, mai ales în jocuri. Denumirea este **structură „fat”**, un singur container cunoscut static, cu toate proprietățile anticipate; subtitraredile o redă inconsistent ca „F-struct” sau „Fract”. Avantajul său, la [00:24:05](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1445s), este simplicitatea și accesul la câmpuri complet cunoscut. Chiar repository-ul exemplu folosește o clasă pentru această reprezentare, așa că „struct” aici este o poreclă de proiectare, nu o cerință de a folosi `struct` din C#.

[00:24:16](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1456s) Costurile sale sunt câmpurile și stocarea nefolosite și, la [00:24:21](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1461s), necesitatea de a cunoaște fiecare domeniu care o folosește. Limita decisivă apare la [00:24:41](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1481s): dacă o bibliotecă deține constructorul, conducta și tipul fix de context, consumatorul ei nu poate modifica acel tip ca să adauge propriile sale date de aplicație.

[00:25:10](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1510s) **Proprietățile dinamice** rezolvă problema extensibilității: codul consumatorului atașează date unui context definit de bibliotecă, fără a schimba declarația clasei contextului. Indiferent de reprezentare, apelantul tot trebuie să definească informația pe care vrea să o transmită etapelor ulterioare, la [00:25:21](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1521s).

## O bază abstractă permite contexte definite de apelant

[00:25:23](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1523s) Există mai multe moduri de a furniza date definite de apelant: o bază abstractă cu contexte derivate, parametri generici sau stocare într-un dicționar/tablou. Lecția le explorează acum pe rând; sunt alternative, nu cerințe de a combina fiecare tehnică.

[00:25:46](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1546s) Mai întâi, transformi contextul într-o **clasă de bază abstractă**, un marcaj de moștenire care nu poate fi instanciat direct, și îi elimini câmpurile de date. Pașii acceptă instanțe ale căror clase derivă din acea bază. [Contractele conexe de moștenire](https://github.com/AntonC9018/uniCourse_csharp/blob/d5af1fb372be56a453e3b139990013c28e50198c/examples/property_dict/Subclassing.cs) separă, similar, o bază comună de date specializate. O reconstrucție scurtă a acestei etape a conductei este:

```csharp
abstract class Context { }
sealed class GreetingContext : Context
{
    public required string Name;
}
sealed class MathContext : Context
{
    public int Sum;
    public int Multiplier = 1;
}
interface IPipelineStep
{
    void Execute(Context context);
}
```

[00:26:03](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1563s) Parametrul comun admite orice context derivat. La [00:26:11](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1571s) `object` ar putea servi tehnic drept o bază și mai largă. Clasa abstractă este utilă pentru că documentează intenția și exclude obiectele fără legătură, la [00:26:14](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1574s). Moștenirea explicită face fluxul de date mai controlat, la [00:26:26](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1586s); nu este indispensabilă modelului, la [00:26:37](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1597s).

[00:26:47](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1607s) Codul intermediar nu compilează pentru că locurile de apel au în continuare nevoie de un context concret. O bază abstractă nu poate fi creată cu `new Context()`. Fiecare conductă are nevoie de un context concret potrivit, la [00:26:50](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1610s).

[00:27:03](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1623s) Definește și contextul aritmetic împreună cu membrii săi obligatorii. Poate transporta informații suplimentare precum un multiplicator, la [00:27:11](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1631s), iar mai mulți pași aritmetici îl pot folosi în comun, la [00:27:25](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1645s). Furnizarea claselor de date repară construirea, dar semnătura largă a pasului încă nu spune fiecărui pas ce membri poate folosi.

## Cast-urile concrete mută erorile la runtime

[00:27:34](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1654s) Actualizezi fiecare invocare a salutului să creeze explicit contextul concret de salut. Numele pentru salut/afișare/grilă din demonstrație variază; această notă folosește `GreetingContext` pentru exemplul cu nume. La [00:28:00](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1680s) pasul de salut are nevoie de acel tip concret la runtime, în timp ce parametrul său rămâne baza abstractă.

[Specializarea după tipul de runtime (cod conex)](https://github.com/AntonC9018/uniCourse_csharp/blob/d5af1fb372be56a453e3b139990013c28e50198c/examples/property_dict/Subclassing.cs) conține teste de capabilitate precum `character is not IMage`. Acest exemplu reconstruiește cast-urile concrete, mai neîngăduitoare, din video:

```csharp
void Greet(Context context)
{
    var greeting = (GreetingContext)context;
    Console.WriteLine(greeting.Name);
}
void Add(Context context)
{
    var math = (MathContext)context;
    math.Sum += 5;
}
```

[00:28:05](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1685s) Convertești parametrul de bază la tipul de salut. La [00:28:12](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1692s) pasul aritmetic are nevoie de un cast echivalent. Un **cast** convertește o referință la tipul cerut numai dacă obiectul actual suportă acel tip; moștenirea unei baze comune nu face tipurile derivate surori interschimbabile.

[00:28:28](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1708s) Fiecare pas care are nevoie de câmpuri specializate repetă un cast. Evitarea acestei repetări este amânată, la [00:28:36](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1716s). Un pas care deține deja o referință `MathContext` nu ar mai avea nevoie să convertească din nou acea referință, la [00:28:39](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1719s), dar un parametru `Execute(Context)` impune în continuare o restrângere înainte de accesarea câmpurilor doar aritmetice.

[00:28:50](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1730s) Compilatorul cunoaște doar contractul larg al conductei. Nu poate stabili că o anumită conductă configurată așteaptă un context de salut. La [00:29:12](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1752s) trimiterea unui context aritmetic către ea este acceptată de semnătura statică a apelului.

[00:29:24](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1764s) Execuția eșuează apoi când pasul de salut convertește acel obiect aritmetic la tipul așteptat. Este o conversie invalidă la runtime, nu o eroare de compilare.

[00:29:34](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1774s) Abordarea schimbă garanțiile statice în favoarea flexibilității. Genericele recuperează parțial aceste garanții și sunt introduse mai târziu, la [00:29:39](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1779s).

<details>
<summary>Preziceți: dă celor două clase de context aceeași bază abstractă vreunul dintre ele sigur pentru fiecare pas?</summary>

Nu. Baza le permite doar să intre în abstracție. Un pas care face cast către un tip concret frate poate eșua în continuare când primește obiectul greșit. Cerința reală a pasului este mai puternică decât parametrul său declarat.
</details>

## Operațiile mixte au nevoie de capabilități partajate

[00:29:45](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1785s) O a doua limitare apare când o conductă trebuie atât să salute, cât și să facă aritmetică. Poți adăuga pasul de salut la [00:30:01](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1801s), dar contractul bază abstractă nu dovedește că toți pașii pot consuma același obiect.

[00:30:02](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1802s) Dacă un pas face cast la `MathContext` și altul la contextul separat care conține numele, o singură intrare nu poate satisface ambele clase concrete fără legătură. Compilatorul nu impune compatibilitatea pașilor în această versiune cu bază largă. Cast-ul problematic are loc în pasul ulterior, la [00:30:46](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1846s), așa că eșecul este amânat până la execuție. Formularea compilatorului din narațiune, în jurul [00:30:23](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1823s), trebuie citită împreună cu acea explicație de runtime, nu ca o afirmație că această versiune ar respinge deja static combinația.

[00:30:54](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1854s) O bază comună oferă un punct de intrare comun, dar nu exprimă fiecare combinație de date specializate. Revenind la analogia jocului, de la [00:31:00](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1860s), sistemul este conducta, iar entitățile procesate sunt contextele. Funcționalitatea mixtă este frecventă, la [00:31:11](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1871s); multe operații independente pot avea nevoie de aceluiași obiect.

[00:31:42](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1902s) Exemplul arborelui face vizibilă încă o dependență. O operație calculează un nivel; alta scalează nodul curent cu acel nivel, la [00:32:00](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1920s). Valoarea este doi, trei sau unu în funcție de adâncime, deci a doua operație depinde de contextul produs de prima.

[00:32:08](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1928s) Extragi calculul nivelului într-o strategie proprie. Un **decorator**, o învelitoare în jurul altei strategii, execută această lucru pregătitoare înainte de a invoca corpul învelit, la [00:32:17](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1937s). Alte strategii pot reutiliza aceeași componentă producătoare de adâncime, la [00:32:26](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1946s), iar o a treia strategie dintr-o altă grupare configurată poate depinde, de asemenea, de ea, la [00:32:58](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1978s).

[00:33:11](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1991s) Reutilizarea acelui decorator păstrează ordinea nivel-înainte-de-utilizare în ambele locuri. Chiar dacă lucra pregătitoare este conceptual separată de operația principală, ieșirea ei poate fi necesară operației principale, la [00:33:29](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2009s).

[00:33:44](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2024s) Parcurgerea arborelui este costisitoare, așa că faci o singură parcurgere cu multe strategii atașate aceluiași context. Scrierea fiecărei strategii ca o nouă variabilă locală și un nou apel face metoda de parcurgere să crească, la [00:34:00](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2040s). Următorul pas de proiectare este să exprimi doar capabilitățile pe care fiecare operație le cere, în loc să fixăm fiecare pereche concretă strategie–context.

## Interfețele fac posibil un context combinat

[00:34:13](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2053s) O **interfață** descrie membrii de care are nevoie un pas, fără să impună o anumită clasă de implementare. La [00:34:24](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2064s) introduci un contract îngust care expune numele, printr-o metodă getter sau printr-o proprietate. La [00:34:37](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2077s) schimbi cast-ul pasului pe interfață și îi dai aritmeticii un contract echivalent pentru datele sale.

[Exemplul conex de interfață de capabilitate](https://github.com/AntonC9018/uniCourse_csharp/blob/d5af1fb372be56a453e3b139990013c28e50198c/examples/property_dict/Subclassing.cs) folosește `IMage` și `MageTrait` pentru această separare. Contractele conductei reconstruite sunt:

```csharp
interface IHasName { string Name { get; } }
interface IHasSum { int Sum { get; set; } }

sealed class CombinedContext : Context, IHasName, IHasSum
{
    public required string Name { get; init; }
    public int Sum { get; set; }
}

void Greet(Context c) => Console.WriteLine(((IHasName)c).Name);
void AddFive(Context c)
{
    var math = (IHasSum)c;
    math.Sum += 5;
}
```

[00:34:50](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2090s) O conductă mixtă are nevoie de un obiect care implementează ambele interfețe. La [00:35:14](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2114s) creezi un context combinat suplimentar, declari toate datele necesare și inițializezi acel obiect la locul de apel. Aceasta repetă declarațiile de date, dar acum un singur obiect poate satisface ambele contracte de capabilitate. În exemplu, `init` limitează atribuirea numelui la inițializare; `required` cere apelanților să îl furnizeze.

[00:35:30](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2130s) După ce obiectul combinat lipsă este creat și inițializat, înregistrarea raportează o compilare reușită. La [00:35:34](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2134s) motivul este refactorizarea pe interfețe; bucla de execuție a conductei nu a avut nevoie să se schimbe.

[00:35:52](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2152s) Dependența de interfețe este validă și obișnuită. Am câștigat combinații, dar `Execute(Context)` se bazează în continuare pe cast-uri la runtime. Următoarea refactorizare leagă tipul declarat de intrare de constructor și de pașii săi.

## Tipizăm împreună constructorul, conducta și pașii

[00:36:01](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2161s) Un **parametru de tip generic** stă pentru un tip ales de apelant. Parametrizezi constructorul cu tipul său de context; conducta construită va cere atunci acel tip la invocare, la [00:36:18](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2178s). Un argument incompatibil devine eroare la timp de compilare, la [00:36:29](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2189s), iar un pas care cere contextul greșit nu poate fi adăugat, la [00:36:37](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2197s).

[Bucla de execuție ne-generică (cod conex)](https://github.com/AntonC9018/uniCourse_csharp/blob/a386c9c8c77cc4f67311059663399b5b793ed47d/examples/pipeline_library/Pipeline.cs) ilustrează bucla păstrată în această refactorizare; nu conține această stare didactică generică exactă. Acest exemplu bazat pe video arată unde trebuie să ajungă parametrul de tip:

```csharp
interface IPipelineStep<TContext>
{
    void Execute(TContext context);
}

sealed class Pipeline<TContext>
{
    private readonly IPipelineStep<TContext>[] steps;
    public Pipeline(IPipelineStep<TContext>[] steps) => this.steps = steps;

    public void Execute(TContext context)
    {
        foreach (var step in steps)
            step.Execute(context);
    }
}

sealed class PipelineBuilder<TContext>
{
    private readonly List<IPipelineStep<TContext>> steps = new();
    public void Add(IPipelineStep<TContext> step) => steps.Add(step);
    public Pipeline<TContext> Build() => new(steps.ToArray());
}
```

[00:36:46](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2206s) Elimină contextul fixat cu text din implementarea bibliotecii. Aplică modificările în ordine:

1. [00:36:54](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2214s) Înlocuiește tipul concret al parametrului cu `TContext`, un substituent generic.
2. [00:37:07](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2227s) Propagă același parametru prin interfața pasului, constructor, conducta construită și metoda de execuție.
3. [00:37:15](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2235s) Biblioteca poate acum transporta orice tip de context ales fără să-i cunoască câmpurile.
4. [00:37:25](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2245s) Programul intermediar totuși eșuează, pentru că codul clientului nu a identificat încă tipul concret așteptat de constructor și de pași.
5. [00:37:37](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2257s) Construiește un constructor pentru tipul cerut și actualizează parametrul `Execute` al fiecărui pas la acel tip. Accesul direct la membri înlocuiește cast-urile.

[00:37:58](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2278s) Rezultatul este un contract declarat: adăugarea unui pas într-o conductă garantează că pasul primește o intrare de tipul ales de conductă. La [00:38:01](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2281s) un pas `MathContext` este reutilizabil în orice conductă compatibilă cu context aritmetic; o conductă pentru un context fără legătură nu îl poate accepta.

[00:38:21](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2301s) Definirea contextului aritmetic lipsă face ca acea parte să compileze. La [00:38:28](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2308s) pașii aritmetici concreți trebuie în continuare schimbați la `MathContext`, iar vechea interfață comună de context poate fi eliminată. Observația de la [00:38:36](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2316s) privește această proiectare cu tip concret anume: faptul că o interfață devine generică nu elimină prin sine cerința de context declarată.

[00:38:45](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2325s) Rămân două erori, inclusiv pasul nume/grilă care așteaptă alt context decât conducta aritmetică. La [00:39:16](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2356s) trecerea constructorului la tipul nume/grilă respinge în schimb pașii aritmetici. Comentarea unui apel problematic la [00:39:35](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2375s) permite examinarea separată a erorilor rămase.

[00:39:37](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2377s) Trimiterea contextului combinat grilă/aritmetică către o conductă doar aritmetică eșuează și dacă acele clase implementează doar interfețe comune și niciuna nu este atribuibilă celeilalte. Folosește tipul așteptat sau reproiectează contractele. O clasă derivată poate fi în continuare trimisă acolo unde se așteaptă baza sa; genericele impun atribuibilitatea, nu egalitatea exactă a clasei de runtime.

[00:39:49](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2389s) Abstracția rămâne reutilizabilă, dar verificările de tip au loc acum atât la adăugarea pașilor, cât și la furnizarea contextului de execuție. La [00:40:03](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2403s) conducta generică concretă este completă. Limitarea ei rămasă este compunerea pașilor care descriu capabilități obligatorii diferite.

## Contravarianța admite pași pentru capabilități

[00:40:06](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2406s) Schimbarea tipului concret al unui pas de nume în vreun context combinat concret ar lăsa în continuare aritmetica să aștepte altă clasă. La [00:40:30](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2430s) te îndepărtezi de un parametru concret specific și descrii interfața cerută de pas. La [00:40:34](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2434s) redenumești consecvent substituentul generic de context: numele neutre evită confundarea unui parametru de tip cu o clasă de context reală.

[00:40:43](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2443s) Doar redenumirea unui parametru de tip nu poate stabili că acesta implementează o interfață. Compilatorul are nevoie de o conversie validă între cerința declarată a pasului și contextul ales de constructor, la [00:40:50](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2450s).

[00:41:05](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2465s) Declari parametrul generic al pasului cu `in`. Acesta este **modificatorul de contravarianță** din C#, nu un atribut și nu modificatorul `in` al unui argument individual de metodă. Este sigur aici pentru că un pas consumă contextul ca intrare și nu îl produce ca ieșire, la [00:41:11](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2471s).

Nu a fost găsit niciun commit salvat exact pentru această etapă de varianță. [Contractele conexe de capabilitate](https://github.com/AntonC9018/uniCourse_csharp/blob/d5af1fb372be56a453e3b139990013c28e50198c/examples/property_dict/Subclassing.cs) arată ideea de interfață fără varianță generică. Starea didactică simplificată este:

```csharp
interface IPipelineStep<in TContext>
{
    void Execute(TContext context);
}

sealed class GreetingStep : IPipelineStep<IHasName>
{
    public void Execute(IHasName context) =>
        Console.WriteLine(context.Name);
}

// CombinedContext implementează IHasName și IHasSum.
IPipelineStep<IHasName> broadConsumer = new GreetingStep();
IPipelineStep<CombinedContext> combinedConsumer = broadConsumer;
```

[00:41:47](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2507s) Lecția trimite la covarianță și contravarianță drept lectură suplimentară, reiterat la [00:41:51](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2511s). **Contravarianța** înseamnă că un consumator capabil să proceseze orice `IHasName` poate procesa sigur și contextul mai îngust `CombinedContext`, la [00:41:57](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2517s). Un context cu capabilități suplimentare este acceptabil; unul care îi lipsește capabilitatea cerută, nu.

[Referința C# pentru `in` generic](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/in-generic-modifier) confirmă această direcție de intrare și restricția la tipuri de referință. Returnarea lui `TContext` din această interfață nu ar fi compatibilă cu o declarare doar contravariantă.

[00:42:01](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2521s) Aceleași tratament pe bază de interfață îi dai pasului de nume/grilă. La [00:42:03](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2523s) codul tot nu compilează, în timp ce apelantul furnizează un context care nu corespunde cerinței declarate. La [00:42:12](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2532s) corectarea contextului apelantului restaurează compilarea, iar apelul funcționează.

[00:42:21](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2541s) Acum o conductă aleasă pentru un context combinat concret poate admite două implementări de pas fără legătură: una consumă `IHasName`, cealaltă consumă `IHasSum`. Ambele trebuie să fie capabilități ale aceluiași obiect primit. Tiparea statică rămâne, iar pașii nu trebuie să numească acea clasă concretă.

<details>
<summary>De ce poate fi folosit un consumator al lui IHasName cu CombinedContext, dar nu invers?</summary>

Fiecare CombinedContext din acest exemplu furnizează un nume, deci consumatorul mai larg de nume are tot ce îi trebuie. Un consumator care cere CombinedContext ar putea avea nevoie și de o sumă; o implementare arbitrară a lui IHasName nu trebuie să furnizeze una.
</details>

## Înlocuim câmpurile statice cu stocare dinamică

[00:42:34](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2554s) Până aici fiecare context a fost **static** în forma datelor sale: clasa și interfețele sale decideau ce membri există. Chiar și cu generice, la [00:42:42](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2562s), datele disponibile depind de tipul ales la timp de compilare. Cast-urile anterioare au dezvăluit aceeași limită.

[00:43:13](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2593s) Existența și tipul declarat al unui câmp sunt determinate de clasa contextului. Alternativa de la [00:43:25](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2605s) este un singur tip de context capabil să transporte mulțimi diferite de date la runtime.

[00:43:35](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2615s) Creezi o nouă clasă `Context` simplă. Pornește goală, așa că nimic din ce folosea vechile câmpuri nu mai compilează împotriva ei. La [00:43:45](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2625s) schimbi semnăturile pașilor pe acea clasă și le elimini parametrii de tip de context; la [00:43:47](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2627s) elimini instalația generică rămasă și clasele de context concrete vechi.

[00:43:49](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2629s) Rezultatul intermediar nu compilează: nu există câmp `Name` sau `Sum` de citit sau inițializat. Acest lucru este așteptat în timpul tranziției, nu reprezintă API-ul final.

[00:44:02](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2642s) Noul protocol este **stocarea bazată pe chei**: pui o valoare în context sub o cheie și o recuperezi mai târziu folosind aceeași cheie. De exemplu, numele la zero și suma la unu, la [00:44:16](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2656s), astfel încât pașii independenți își pot citi propriile intrări, la [00:44:18](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2658s).

[Exemplul conex de tablou de proprietăți](https://github.com/AntonC9018/uniCourse_csharp/blob/d5af1fb372be56a453e3b139990013c28e50198c/examples/property_dict/PropertyArray.cs) stochează obiecte într-un tablou, dar le caută după tipul componentei, în loc să folosească cheile poziționale din acest video. Punctul de pornire pozițional simplificat este:

```csharp
sealed class Context
{
    public required object[] Data;
}
var context = new Context { Data = new object[2] };
context.Data[0] = "Антон";
context.Data[1] = 0;
```

[00:44:24](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2664s) O cheie ar putea identifica o poziție într-un tablou, o intrare într-un dicționar sau un obiect găsit prin parcurgerea unei colecții. La [00:44:29](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2669s) lecția alege inițial `object[]`: un tablou ale cărui celule pot ține reprezentări de obiect ale unor tipuri diferite. Apelantul trebuie să creeze și să inițializeze tabloul înainte de execuție. **Proprietățile dinamice** înseamnă aici conținut variabil în spatele unui container comun, nu invocare la runtime prin `dynamic` din C#.

## Stabilim cheile tabloului și inițializăm configurația

[00:45:35](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2735s) Înlocuiește pozițiile fără nume cu constante publice, denumite inițial `Name = 0` și `One = 1`. O **cheie** identifică poziția de stocare agreată. Constantele partajate permit apelanților și pașilor să folosească aceeași configurație, la [00:45:53](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2753s).

[Stocarea în tablou (cod conex)](https://github.com/AntonC9018/uniCourse_csharp/blob/d5af1fb372be56a453e3b139990013c28e50198c/examples/property_dict/PropertyArray.cs) demonstrează conținuturi eterogene `object[]`; acest exemplu bazat pe video folosește în schimb chei poziționale:

```csharp
static class GreetingKeys
{
    public const int Name = 0;
    public const int One = 1; // A doua poziție în schița timpurie.
}
void Greet(Context context)
{
    var name = (string)context.Data[GreetingKeys.Name];
    Console.WriteLine($"Hello, {name}");
}

Context CreateGreeting(string name)
{
    var context = new Context
    {
        Data = new object[GreetingKeys.Name + 1]
    };
    context.Data[GreetingKeys.Name] = name;
    return context;
}
```

[00:46:03](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2763s) Citești celula folosind constanta cheie. Ea iese ca `object`, așa că pasul o convertește la șirul așteptat, la [00:46:32](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2792s). Afișarea nu necesită în sine un cast la șir — `Console.WriteLine` are și alte suprasîncărcări — dar contractul acestui pas spune că numele său este un șir.

[00:46:53](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2813s) Crearea unui context înseamnă alocarea unor celule suficiente, apoi umplerea pozițiilor obligatorii. La [00:47:05](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2825s) scrii numele folosind cheia partajată. Aceeași atribuire este reiterate la [00:47:13](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2833s).

[00:47:12](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2832s) Un tablou de lungime unu funcționează doar pentru că cheia numelui este întâmplător zero. Dacă cheia devine trei, acea lungime devine invalidă. La [00:47:27](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2847s) dimensionează după poziția obligatorie, în loc să fixezi unu; la [00:47:30](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2850s) astfel păstrezi acordul asupra configurației cheilor.

[00:47:42](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2862s) Contextul complet poate fi trecut direct abstracției; pasul rezolvă datele folosind cheia sa partajată. La [00:47:46](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2866s) extragi construirea și inițializarea contextului într-un ajutor, apoi îl reutilizezi pentru exemplele de salut cu câinele și gândacul, furnizând doar numele.

## Citim, modificăm și scriem valori partajate

[00:48:02](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2882s) Aritmetica urmează aceeași convenție. Pasul legat de sumă expune constantele `Sum` și `Date`, citește valoarea curentă ca `object` și o convertește la tipul cerut.

[Exemplul conex de tablou eterogen](https://github.com/AntonC9018/uniCourse_csharp/blob/d5af1fb372be56a453e3b139990013c28e50198c/examples/property_dict/PropertyArray.cs) citește și el o valoare și o înlocuiește după modificare. Reconstrucția aritmetică pozițională este:

```csharp
static class MathKeys
{
    public const int Sum = 0;
    public const int Date = 1;
}

void Add(Context c, int amount)
{
    var sum = (int)c.Data[MathKeys.Sum];
    c.Data[MathKeys.Sum] = sum + amount;
}
void Multiply(Context c, int factor)
{
    var sum = (int)c.Data[MathKeys.Sum];
    c.Data[MathKeys.Sum] = sum * factor;
}

var context = new Context
{
    Data = new object[Math.Max(MathKeys.Sum, MathKeys.Date) + 1]
};
context.Data[MathKeys.Sum] = 0;
context.Data[MathKeys.Date] = DateTime.Now;
```

[00:48:19](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2899s) Scrii rezultatul modificat înapoi în celulă, ca pașii ulteriori să îl vadă. Schimbarea doar a numărului local dezincapsulat nu actualizează tabloul. La [00:49:02](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2942s) multiplicatorul repetă această procedură de citire-modificare-scriere.

[00:49:09](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2949s) Noua demonstrație aritmetică construiește două celule. Mai general, la [00:49:29](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2969s), capacitatea trebuie să fie **cel mai mare indice de cheie cerut plus unu**; lungimea doi este suficientă numai pentru pozițiile zero și unu. Aceasta furnizează pozițiile, nu tipurile lor de valoare și nici inițializarea.

[00:49:39](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2979s) Inițializezi celula sumei la zero și celula datei la valoarea sa inițială, prin constante. La [00:49:54](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2994s) pasul de raportare extrage și afișează ambele valori, convertind fiecare conform propriului contract. Suma este numerică; o intrare de dată/oră are nevoie de tipul ei dată/oră. Nu ambele devin automat „timp” pentru că au fost obiecte.

[00:49:53](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2993s) Cheile partajate fixate cu text pot funcționa. Primul cost, la [00:50:01](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3001s), este coordonarea exact a aceleiași constante pe ambele părți; constantele publice reduc deja dezacordurile accidentale. Un enum le-ar putea împacheta, dar lecția ia intenționat altă direcție, la [00:50:17](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3017s).

## Tablourile rare și cast-urile dezvăluie contracte ascunse

[00:50:24](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3024s) O colecție mare de sisteme fără legătură produce o configurație pozițională mare. Un sistem folosește pozițiile doi și trei, altul doar poziția patru, la [00:50:45](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3045s). Totuși, acel al doilea sistem trebuie să aloce cinci celule pentru a include indicele patru, la [00:51:00](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3060s), lăsând cea mai mare parte a celulelor nefolosite, la [00:51:07](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3067s).

[00:51:31](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3091s) Atribuirea unui `int` într-o celulă `object` provoacă **boxing**, adică alocarea unei reprezentări de obiect a acelui **tip de valoare**, a cărui valoare are altfel o reprezentare directă precum un întreg. Lecția consideră aceasta o problemă mai puțin urgentă decât nepotrivirile de tip.

[00:51:53](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3113s) Contractul esențial este ascuns: fiecare cheie cere un anumit tip de valoare, iar fiecare cititor face conversia în consecință. Pui un întreg acolo unde salutul așteaptă un șir, la [00:52:02](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3122s), și recuperarea eșuează la runtime.

[Exemplul conex final cu dicționar tipizat](https://github.com/AntonC9018/uniCourse_csharp/blob/9e0565fa096e0e58867255a6cd1dd38d017a2d2c/examples/property_dict/Program.cs) demonstrează același pericol de identitate greșită/tip greșit folosind chei în loc de poziții de tablou. O eșecare pozițională minimă este:

```csharp
context.Data[GreetingKeys.Name] = 42; // Permis de object[].
var name = (string)context.Data[GreetingKeys.Name]; // Conversie invalidă la runtime.
```

[00:52:22](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3142s) Există două reparații imediate:

1. Stochează intrări doar pentru identitățile efectiv prezente, evitând alocarea fiecărei poziții intermediare.
2. [00:52:30](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3150s) Dă accesului prin cheie un contract care cunoaște tipul, astfel încât clienții să nu mai facă manual cast fiecărui obiect recuperat și să nu scrie din greșeală tipul greșit.

Abstracția de stocare ar putea avea în continuare nevoie de un cast în interior; scopul este să îl centralizăm și să îl constrângem, nu să pretindem că stocarea eterogenă este tipizată static până la capăt.

## Boxing-ul este o problemă de stocare separată

[00:52:36](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3156s) Boxing-ul este intenționat în afara reparației de cadru finalizate aici. Înlocuirea tabloului cu un dicționar și adăugarea cheilor tipizate nu va elimina boxing-ul tipurilor de valoare stocate ca obiecte.

[00:52:54](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3174s) Lecția menționează rezolvarea problemelor de alocare la un nivel superior, cu obiecte **flyweight** și un **pool** reutilizabil, în loc de a încapsula direct valori noi. Acestea sunt abordări de partajare/reutilizare, nu cod implementat în această lecție.

[00:53:12](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3192s) Dezvoltarea de jocuri oferă **ECS**, Entity Component System, ca altă proiectare de stocare. În organizarea descrisă, la [00:53:24](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3204s), fiecare tip de date are propriul tablou: un tablou de nume, altul de sume, peste multe entități. La [00:53:45](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3225s) apelanții trec un identificator și rezolvă datele în stocarea specifică tipului potrivit. Astfel, pe acea cale, datele valorice rămân în afara celulelor de tip `object`.

Aceste observații stabilesc o limită: lecția explică de ce accesul prin chei tipizate este util chiar dacă stocarea bazată pe obiecte continuă să aloce. Nu construiește un ECS și nu măsoară nicio implementare.

## Module independente se pot ciocni pe chei

[00:53:51](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3231s) A patra și cea mai grea problemă este **coliziunea de chei**, adică folosirea aceleiași identități de stocare pentru sensuri fără legătură. La [00:54:00](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3240s) o eroare de tip „off by one” poate scrie în zero în loc de unu. Numele și datele numerice ajung sub aceeași cheie, la [00:54:12](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3252s).

[00:54:23](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3263s) Paguba poate rămâne ascunsă până când un pas recuperează valoarea și face cast. Pornirea și verificările statice obișnuite pe `object[]` nu detectează nepotrivirea semantică.

[00:54:40](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3280s) Un singur autor care întreține o listă comună de chei e mai puțin probabil să atribuie același indice de două ori din greșeală. Module independente care își întrețin fiecare propria listă, la [00:54:55](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3295s), reprezintă o altă problemă: ambele pot alege legitim zero pentru date fără legătură. Module care partajează un context pot intra astfel în conflict chiar dacă fiecare listă este corectă intern, la [00:55:08](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3308s).

[00:55:13](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3313s) Două abordări sunt identitățile șir și un **registru**, un alocator central pentru chei. Un registru atribuie valori la runtime, la [00:55:30](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3330s), iar fiecare modul participant trebuie să folosească același alocator, la [00:55:36](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3336s). Alocările distincte produc atunci identități distincte.

[00:55:52](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3352s) Identitățile numerice depind de ordinea inițializării: modulul A primește numărul mic dacă se înregistrează primul; inversând ordinea, modulul B îl primește. Codul ar trebui să păstreze și să folosească cheia returnată, în loc să se bazeze pe o anumită valoare numerică.

<details>
<summary>Partajarea unei liste de constante într-un singur modul împiedică coliziunile cu alt modul?</summary>

Nu. Celălalt modul poate alege în mod independent aceleași numere. Ambele au nevoie de un schem comun de identitate, cum ar fi un singur registru sau nume coordonate, dacă intrările lor trăiesc în același context.
</details>

## Un dicționar stochează doar intrările prezente

[00:56:18](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3378s) Lecția urmărește acum alocarea tablourilor rare și cast-urile din clienți. Prima reparație este un **dicționar**, un container asociativ care stochează intrări pentru cheile efectiv adăugate. A doua este modelul cheii tipizate, la [00:56:29](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3389s), unde cheia transmite și tipul valorii sale.

[00:56:43](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3403s) Înlocuiește tabloul cu un dicționar. La [00:56:54](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3414s) o capacitate inițială de doi poate găzdui cele două intrări demonstrate; capacitatea este o indicație de alocare, nu un maxim fix.

[Exemplul conex de dicționar](https://github.com/AntonC9018/uniCourse_csharp/blob/9e0565fa096e0e58867255a6cd1dd38d017a2d2c/examples/property_dict/Program.cs) conține atât dicționare simple șir/`object`, cât și varianta encapsulată cu chei tipizate. Etapa ne-tipizată simplificată este:

```csharp
var data = new Dictionary<int, object>(capacity: 2);
data.Add(MathKeys.Sum, 0);
data.Add(MathKeys.Date, DateTime.Now);
var sum = (int)data[MathKeys.Sum];
data[MathKeys.Sum] = sum + 5;
```

[00:57:06](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3426s) `Add` aruncă o excepție dacă acea cheie există deja, făcând inserarea duplicată vizibilă în loc să înlocuiească tăcut intrarea. Aceasta detectează o coliziune într-un anumit dicționar; nu alocă global chei unice, nu validează tipuri de valoare și nu împiedică suprascrierea prin indexer. [Documentația pentru Dictionary.Add](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.add) face distincția între adăugarea duplicată și înlocuirea prin indexer.

[00:57:48](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3468s) Creează dicționarul în interiorul contextului, în loc să păstrezi structura de stocare anterioară. Schița folosește o capacitate inițială mică. La [00:57:54](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3474s) o interfață `TryGetValue` bazată pe chei poate rămâne neschimbată: recuperează în continuare după cheie, acum prin căutare în dicționar. Tablourile în sine nu expun `TryGetValue`; observația privește abstracția de acces din jur.

[00:58:02](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3482s) Dicționarele sunt implementarea obișnuită pentru aceste contexte, adesea cu chei mai stricte pentru a reduce conflictele. Am evitat alocarea golurilor poziționale, dar căutarea după valori `object` are în continuare nevoie de un contract de tip.

## Listele de componente sunt o altă reprezentare

[00:58:17](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3497s) Dicționarele nu sunt singurul container posibil. Într-o variantă cu liste, la [00:58:26](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3506s), inspectezi tipurile obiectelor în locul pozițiilor fixe; obținerea datelor numerice poate presupune parcurgerea pentru componenta dorită.

[00:58:34](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3514s) Un simplu `int` nu poate distinge suma de viață. Atașezi o identitate obiectului-valoare sau stochezi perechi explicite cheie/valoare, reiterate la [00:58:54](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3534s). O **componentă** grupează unul sau mai mulți membri de date înrudite și le dă un tip distinct.

[Starea conexă cu tablou de componente](https://github.com/AntonC9018/uniCourse_csharp/blob/ec66732a25273a9f3eaaa73866811e82f43d7853/examples/property_dict/Program.cs) distinge statistici întregi marcate și componente tipizate. Un exemplu scurt este:

```csharp
sealed class SumComponent
{
    public int Value;
}

var components = new List<object> { new SumComponent { Value = 0 } };
foreach (var component in components)
{
    if (component is SumComponent sum)
        sum.Value += 5;
}
```

[00:59:07](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3547s) Listele de componente sunt folosite pe scară largă în dezvoltarea de jocuri, cu Unity dat ca exemplu. La [00:59:11](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3551s) lecția favorizează ECS pentru problema stocării în joc; aceasta este o preferință de proiectare în discuție, nu o afirmație că orice utilizare a unei liste de componente ar fi incorectă.

[00:59:18](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3558s) Tipul componentei devine cheia, iar câmpul componentei conține valoarea. Adaugi componenta necesară și cauți după tipul ei. La [00:59:31](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3571s) un dicționar indexat după tipul componentei poate optimiza acea căutare.

[00:59:33](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3573s) Dicționarul simplu cheie/valoare este în continuare prezentat drept implementarea de context de uz general, cea mai comună, repetat la [01:00:20](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3620s). Stocarea în componente explică o alternativă; lecția revine la intrările obișnuite cheie/valoare.

## Ascundem stocarea în spatele cheilor tipizate

[01:00:29](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3629s) Păstrează dicționarul, dar fă-l privat în interiorul contextului. Contextul devine o **abstrație**, care expune operații fără a expune stocarea de valori `object`. La [01:00:48](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3648s) metodele sale publice trebuie să fie strict tipizate.

[01:01:05](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3665s) Intern, identitatea poate rămâne un întreg. Extern, o **cheie tipizată**, `Key<T>`, combină acea identitate cu tipul valorii exprimat prin `T`. Clienții nu mai manipulează poziții brute și apoi ghicesc tipul. O cheie oferă acces la o valoare; identitatea ei nu este chiar valoarea stocată.

[Implementarea conexă a cheii tipizate](https://github.com/AntonC9018/uniCourse_csharp/blob/9e0565fa096e0e58867255a6cd1dd38d017a2d2c/examples/property_dict/Program.cs) folosește identități șir și o structură generică readonly. Iată versiunea cu întregi reconstruită din înregistrare:

```csharp
public readonly struct Key<T>
{
    public readonly int Value;
    public Key(int value) => Value = value;
}

static class Keys
{
    public static readonly Key<string> Name = new(0);
    public static readonly Key<int> Sum = new(1);
}
```

[01:01:21](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3681s) Metodele contextului vor accepta o cheie pentru a obține date. La [01:01:32](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3692s) implementarea nu este încă completă: doar căutarea nu ajunge până când nu există și operația de adăugare corespunzătoare, la [01:01:42](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3702s).

[01:01:38](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3698s) Cheia trebuie să poarte tot ce este necesar pentru a-i rezolva intrarea: identitatea de runtime alege intrarea din dicționar, iar tipul său generic static determină tipul public rezultatului. La [01:01:46](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3706s) cheia valorii declară explicit acel tip.

[01:02:01](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3721s) Înlocuirea unei constante întregi cu `const Key<int>` nu compilează. Problema este **const**, nu folosirea unei structuri: o structură de cheie definită de utilizator și inițializată nu poate fi o constantă la timp de compilare. O `readonly struct` rămâne o reprezentare validă pentru chei. Constantele C# includ mai mult decât cele trei exemple rostite în înregistrare; distincția relevantă este între valori constante la timp de compilare și instanțe de chei construite. Vezi [referința C# pentru const](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const).

[01:02:12](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3732s) Creează cheia o singură dată și păstreaz-o într-un câmp, sau expune inițial o valoare `static` stabilă. `static` descrie un membru partajat de tip; `readonly` împiedică reatribuirea ulterioară a câmpului. La [01:02:28](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3748s) cheia sumei este inițializată cu identitatea unu. Crearea ulterioară a registrului face cu atât mai importantă stocarea o singură dată.

## Accesul generic mută cast-urile în context

[01:02:40](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3760s) `Get` trebuie să returneze tipul asociat cheii sale. `Key<int>` înseamnă un rezultat `int`, la [01:02:55](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3775s), nu un `object` care necesită un cast din partea apelantului. O scriere compatibilă acceptă doar un întreg, așa că o scriere cu șir este respinsă, la [01:03:07](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3787s).

[01:03:14](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3794s) Exemplul schimbă valoarea greșită înapoi la zero numeric, ca să compileze. Subtitrarea spune „ноль” (zero), nu `null` din C#; `null` nu este un înlocuitor valid pentru un întreg nenulabil.

[Metodele generice Add și Get (cod conex)](https://github.com/AntonC9018/uniCourse_csharp/blob/9e0565fa096e0e58867255a6cd1dd38d017a2d2c/examples/property_dict/Program.cs) ascund un dicționar și fac castul în interiorul lui `Get<T>`. Această extensie simplificată cu identitate întreagă include setterul discutat în video:

```csharp
sealed class Context
{
    private readonly Dictionary<int, object> data = new();

    public T Get<T>(Key<T> key) => (T)data[key.Value];
    public void Add<T>(Key<T> key, T value) => data.Add(key.Value, value!);
    public void Set<T>(Key<T> key, T value) => data[key.Value] = value!;
}

// Codul client nu mai are cast-uri explicite.
var sum = context.Get(Keys.Sum);
context.Set(Keys.Sum, sum + 5);
// context.Set(Keys.Sum, "five"); // Nicio apelare generică compatibilă.
```

[01:03:19](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3799s) Actualizezi apelanții la `Get`, eliminând cast-urile lor manuale. Numele revine ca șir; suma revine ca întreg. La [01:03:29](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3809s) un setter folosește aceeași relație cheie–tip de valoare.

[01:03:41](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3821s) Compilatorul deduce argumentul generic din tipul cheii. Nu examinează obiectul efectiv al dicționarului ca să deducă un tip la runtime. Dicționarul stochează în continuare obiecte, iar castul intern poate eșua dacă asocierea identitate–tip a fost coruptă.

[01:03:55](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3835s) Completezi trei operații: `Get` returnează o valoare de tip `T`; `Set` scrie un `T`; `Add` acceptă o cheie și un `T` corespunzător și returnează `void`. Parametrii cheie și valoare trebuie să coincidă în același `T`.

[01:04:20](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3860s) `Set` seamănă cu `Get`, dar scrie prin sintaxa de paranteze pătrate a dicționarului. La [01:04:28](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3868s) narațiunea îl numește operator de indexare suprasîrcărcat. În C# construcția relevantă este **indexerul** dicționarului, `data[key.Value] = value`; nu este un operator generic propriu pe `Key<T>`.

[01:04:34](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3874s) `Get` nu primește niciun argument de valoare. Recuperează un obiect și îl convertește la tipul determinat de cheie. La [01:04:46](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3886s) `T` este încă necunoscut până când metoda declară `<T>`. Un parametru de tip nu poate apărea pur și simplu într-o metodă ne-generică, dacă tipul care o conține nu îl declară deja.

`!` din acest exemplu tranzitoriu este operatorul de anulare a nullului: suprimă un avertisment de nulabilitate; nu validează și nu convertește valoarea la runtime. Etapa următoare trebuie să aleagă un contract de null coerent, în loc să lase acel detaliu implicit.

## Finalizăm inițializarea și alegem un contract pentru null

[01:05:00](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3900s) Scrierile generice și stocarea cu valori `object` ridică o întrebare de **nulabilitate**: API-ul permite valori nule? Modificarea intermediară permite valori nule în dicționar; la [01:05:16](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3916s) experimentează și cu un rezultat generic nulabil. Este un contract temporar în construcție.

[01:05:34](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3934s) Direcția dorită devine apoi respingerea intrării nule. Pentru tipurile de referință, adnotările de nulabilitate produc avertismente ale compilatorului în mod implicit; proiectele pot trata avertismentele ca erori. Ele nu sunt verificări la runtime. Tipurile de valoare nenulabile precum `int` nu pot accepta direct `null`. [Documentația tipurilor de referință nulabile](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/null-safety/nullable-reference-types) explică faptul că adnotările ghidează analiza statică.

[Contextul conex salvat](https://github.com/AntonC9018/uniCourse_csharp/blob/9e0565fa096e0e58867255a6cd1dd38d017a2d2c/examples/property_dict/Program.cs) folosește o scriere cu anulare a nullului și un cast la recuperare, în locul unei politici complete pentru null la runtime. Această extensie simplificată, explicit nenulă, a ideii didactice face politica vizibilă:

```csharp
sealed class Context
{
    private readonly Dictionary<int, object> data = new();

    public void Add<T>(Key<T> key, T value) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(value);
        data.Add(key.Value, value);
    }
    public T Get<T>(Key<T> key) where T : notnull =>
        (T)data[key.Value];

    public void Set<T>(Key<T> key, T value) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(value);
        data[key.Value] = value;
    }
}

var context = new Context();
context.Add(Keys.Name, "Антон");
context.Add(Keys.Sum, 0);
var name = context.Get(Keys.Name); // Dedus ca string.
var sum = context.Get(Keys.Sum);   // Dedus ca int.
context.Set(Keys.Sum, sum + 5);
```

**Constrângerea generică** `where T : notnull` exprimă așteptarea de tip nenul; garda de runtime aruncă pentru un `null` efectiv. Constrângerile și gardele de mai sus clarifică contractul; ele nu sunt prezentate drept o transcriere exactă a codului final din video. Dacă `null` este o valoare legitimă, alege stocare nulabilă și un API care exprimă asta. O cheie lipsă din dicționar este o altă condiție: căutarea prin indexer aruncă în lipsă; faptul că valoarea este nulabilă nu transformă o cheie lipsă într-o căutare reușită.

[01:05:43](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3943s) Cea mai mare parte a codului funcționează acum în înregistrare, inițializarea încă eșuând. La [01:05:59](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3959s) o repari creând contextul și adăugând fiecare intrare obligatorie. La [01:06:13](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3973s) urma apelurilor este: creează containerul, apelează `Add`, repetă pentru fiecare intrare.

[01:06:22](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3982s) Pentru apelul cu nume, compilatorul deduce `T` ca `string` din cheia tipizată și valoarea compatibilă. La [01:06:39](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3999s) cheia numelui are identitatea numerică zero și stochează sub acea identitate numele exemplului, „Антон”. Zero este numărul cheii; numele stocat nu este `null`.

[01:06:52](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4012s) Stocarea convertește valoarea generică în `object`. Dacă `T` este un tip de valoare, aceasta provoacă boxing, la [01:07:02](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4022s). Un șir este deja un **tip de referință**, o valoare accesată printr-o referință la obiect, și nu este încapsulat pentru acea conversie.

[01:07:09](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4029s) Getterul nu are nicio valoare furnizată din care să deducă; tipul cheii este suficient. Setterul urmează aceeași relație statică, dar folosește în continuare argumentul său de valoare pentru a efectua scrierea. Orice formulare că setterul ignoră acel argument se referă la determinarea tipului, nu la eliminarea valorii. Inferența tipurilor generice este făcută de compilator, nu de runtime.

[01:07:26](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4046s) Încearcă singur apelurile mici, așa cum sugerează înregistrarea. Prezică rezultatul schimbării scrierii sumei într-un șir înainte de a încerca. La [01:07:32](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4052s) acea pereche greșită cheie/valoare este respinsă de compilator; la [01:07:47](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4067s) recuperarea are deja tipul de retur cunoscut.

[01:07:56](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4076s) Boxing-ul rămâne în afara scopului acestei implementări. Mutarea cast-urilor în context îmbunătățește contractul public fără a schimba stocarea bazată pe obiecte.

<details>
<summary>Ce garantează Key&lt;int&gt; și ce lasă nedovedit?</summary>

Leagă apelurile obișnuite Add/Set/Get de valori și rezultate întregi. Nu dovedește că intrarea există, că un apelant nu a falsificat altă cheie tipizată pentru aceeași identitate sau că o operație concurentă nu va modifica containerul. De asemenea, nu elimină boxing-ul din stocarea bazată pe obiecte.
</details>

## Un registru alocă identități de cheie stabile

[01:08:02](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4082s) Un **registru** centralizează crearea cheilor. Prima versiune poate fi o clasă centrală cu un contor care pornește de la zero. Apelanții cer chei noi, în loc să scrie ei înșiși identitățile.

[01:08:27](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4107s) O metodă generică `NextKey<T>` alege tipul valorii cheii. La [01:08:33](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4113s) construiește și returnează o cheie care conține următoarea identitate. La [01:08:40](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4120s) păstrezi local valoarea curentă a contorului, incrementezi contorul și returnezi identitatea salvată.

Nu a fost găsit nicio stare numerică de registru salvată exact. [Cheile tipizate create manual (cod conex)](https://github.com/AntonC9018/uniCourse_csharp/blob/9e0565fa096e0e58867255a6cd1dd38d017a2d2c/examples/property_dict/Program.cs) arată construcția pe care înregistrarea o înlocuiește. Acest alocator reconstruit face ordinea explicită:

```csharp
sealed class KeyRegistry
{
    private int counter;

    public Key<T> NextKey<T>()
    {
        var identity = counter;
        counter++;
        return new Key<T>(identity);
    }
}
```

[01:08:49](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4129s) Apelurile următoare produc identități diferite, pentru că contorul este avansat înainte de completarea metodei. Nimic nu se execută după `return` în această metodă; numărul returnat este valoarea veche, în timp ce contorul memorat este deja pregătit pentru următorul apel.

[01:08:56](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4136s) O clasă statică cu stare statică devine stare globală când este folosită de multe sisteme și este mai greu de reutilizat. La [01:09:05](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4145s) preferi un registru instantiabil, ținut ca un **singleton** partajat, în loc de a face clasa inerent statică. Un singleton este o instanță aleasă, nu cerința ca toți membrii să fie statici.

[01:09:09](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4149s) Ideal, instanța partajată trăiește în afara claselor consumatoare, păstrându-le mai puțin dependente structural de o globală. La [01:09:15](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4155s) o proiectare mai strictă ar putea da fiecărui context propriul tip de cheie și propriul registru, astfel încât cheile pentru API-uri de containere fără legătură să nu poată fi amestecate.

[01:10:02](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4202s) Acea proiectare specializată este lăsată intenționat neimplementată, pentru că necesită prea mult timp. La [01:10:08](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4208s) un registru generalizat este de obicei suficient; implementarea comună poate sta în spatele unor wrapper-e sau al unor metode convenționale statice. La [01:10:12](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4212s) un wrapper poate expune metode specializate pentru tipul de cheie al unui anumit context. La [01:10:16](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4216s) acest wrapper este din nou amânat, iar demonstrația păstrează versiunea mai simplă.

## Înregistrăm fiecare cheie o singură dată și ținem cont de concurență

[01:10:21](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4221s) Introduci o singură instanță partajată de registru, numită `KeyRegistry` în demonstrație. Implementarea are un punct de acces static pentru că declarațiile de chei îl folosesc la pornire, la [01:10:32](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4232s). Aceasta nu impune ca clasa de registru din spate să fie statică. O instanță partajată poate fi transmisă și printr-o referință obișnuită, la [01:11:24](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4284s).

[Câmpurile conexe de chei stabile](https://github.com/AntonC9018/uniCourse_csharp/blob/9e0565fa096e0e58867255a6cd1dd38d017a2d2c/examples/property_dict/Program.cs) își declară cheile create manual drept câmpuri `static readonly`. Această variantă reconstruită își obține identitățile din registru:

```csharp
static class ApplicationKeys
{
    private static readonly KeyRegistry Registry = new();

    public static readonly Key<string> Name = Registry.NextKey<string>();
    public static readonly Key<int> Sum = Registry.NextKey<int>();
}
```

[01:11:33](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4293s) Cere fiecare cheie exact o dată, de exemplu cu `NextKey<string>()`, și stochează rezultatul într-un câmp. La [01:11:38](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4298s) un getter care apelează `NextKey` la fiecare acces este greșit: fiecare citire ar aloca o identitate nouă și ar pierde legătura cu valoarea deja stocată.

[01:11:40](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4300s) Un singur alocator partajat elimină dublarea accidentală între chei înregistrate corect, dacă alocarea este serializată și intervalul contorului nu este epuizat. Registre separate folosite cu același context nu ar oferi această garanție.

[01:11:43](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4303s) Două fire pot intra în cursă în timp ce citesc și actualizează contorul. Operațiile atomice sunt sarcina de cercetare sugerată; un alocator concurent nu este implementat. Câmpurile de chei stabile elimină înregistrarea repetată, dar nu rezolvă înregistrarea simultană inițială. Nici un alocator thread-safe nu face, singur, sigură scrierea concurentă într-un dicționar obișnuit de context.

<details>
<summary>De ce funcționează un câmp de cheie, dar eșuează o proprietate care apelează NextKey la fiecare citire?</summary>

Câmpul păstrează o singură identitate. Proprietatea ar produce o identitate diferită de fiecare dată, astfel încât inserarea și căutarea ar adresa intrări diferite, deși proprietatea are același nume în codul sursă.
</details>

## Metadatele registrului fac valorile inspectabile

[01:11:49](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4309s) Alocarea cheilor printr-un singur registru permite un alt magazin de chei sau o altă reprezentare de fundal, fără a schimba principiul. La [01:12:41](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4361s) înregistrarea poate reține și tipul asociat fiecărei identități.

[01:12:58](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4378s) O metodă precum `GetOfKey(key)` poate returna tipul valorii înregistrate. La [01:13:12](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4392s) un apelant care cunoaște doar identitatea numerică poate interoga acele metadate chiar și fără un câmp de cheie cu nume static. Aceasta este **introspecția**: examinarea descrierilor intrărilor stocate la runtime.

[01:13:30](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4410s) Adaugi `PrintCurrentData` în context pentru depanare. La [01:13:35](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4415s) aceasta parcurge intrările și afișează câte o linie care descrie tipul asociat fiecărei identități.

[01:13:48](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4428s) Doar tipul este adesea prea puțin. Furnizezi un nume la crearea unei chei, la [01:13:52](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4432s), și menții o colecție precum `keyNames` care mapează identitățile către nume lizibile, la [01:13:55](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4435s).

Nu a fost găsită nicio implementare exactă a introspecției. [Dicționarul conex de context](https://github.com/AntonC9018/uniCourse_csharp/blob/9e0565fa096e0e58867255a6cd1dd38d017a2d2c/examples/property_dict/Program.cs) furnizează intrările pe care o asemenea buclă le-ar inspecta. O reconstrucție simplificată, în care `GetName` și `GetOfKey` reprezintă interogări ale registrului, este:

```csharp
void PrintCurrentData()
{
    foreach (var (identity, value) in data)
    {
        Console.WriteLine(
            $"{registry.GetName(identity)}: {value} " +
            $"({registry.GetOfKey(identity)})");
    }
}
```

[01:14:05](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4445s) Contextul deține cheia și valoarea unei intrări, apoi cere registrului numele lizibil al acesteia. La [01:14:24](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4464s) metadatele centralizate evită duplicarea șirurilor și tipurilor descriptive în fiecare valoare de cheie copiată. „Disponibil global” aici înseamnă disponibil utilizatorilor registrului partajat, nu că descrierile trebuie să fie variabile globale.

[01:14:47](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4487s) Numele trebuie furnizate la crearea cheilor, iar demonstrația afișează conținutul contextului nou creat pentru a verifica înregistrarea. La [01:14:58](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4498s) o înregistrare de nume lipsă se corectează cu un apel precum `keys.AddName(name, key)`.

[01:15:11](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4511s) Cheile întregi pot rămâne identități mici în memorie, în timp ce registrul le furnizează numele și metadatele de tip. La [01:15:24](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4524s) `PrintCurrentData` află ce înseamnă fiecare întreg înainte de a raporta intrarea. Registrul descrie cheile; contextul stochează în continuare valorile de execuție.

## Numele de tip șir pot deveni cheile

[01:15:44](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4544s) O variantă mai simplă folosește șirurile însele drept identități de cheie, în loc de numere plus metadate separate. **Cheile de tip șir** mută numele lizibile în identitate.

[01:15:57](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4557s) Registrul poate urmări numele înregistrate într-o **mulțime de hash**, o colecție de valori distincte. La [01:16:12](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4572s) `HashSet.Add` întoarce true când numele este inserat pentru prima dată. La [01:16:22](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4582s) arunci excepție dacă întoarce false, raportând că cheia este deja înregistrată.

[Cheia cu valoare de tip șir și dicționarul (cod conex)](https://github.com/AntonC9018/uniCourse_csharp/blob/9e0565fa096e0e58867255a6cd1dd38d017a2d2c/examples/property_dict/Program.cs) folosește șiruri pentru identitatea cheii, dar nu conține acest registru. Schimbarea simplificată a înregistrării este:

```csharp
public readonly struct Key<T>
{
    public readonly string Value;
    public Key(string value) => Value = value;
}

sealed class StringKeyRegistry
{
    private readonly HashSet<string> names = new();

    public Key<T> NextKey<T>(string name)
    {
        if (!names.Add(name))
            throw new InvalidOperationException("Key already registered");
        return new Key<T>(name);
    }
}
```

[01:16:36](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4596s) Identitatea numerică anterioară trebuie înlocuită consecvent cu șirul numelui, inclusiv tipul câmpului cheii. Un **hash** este rezultatul numeric folosit pentru organizarea căutării după cheie; nu elimină necesitatea comparării cheilor pentru egalitate. La [01:16:50](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4610s) această versiune renunță la stocarea separată a metadatelor și stochează valorile contextului sub șiruri. Păstrează totuși parametrul generic `T` în contractul static al fiecărei chei tipizate.

[01:17:07](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4627s) Față de indexarea directă într-un tablou, căutarea în dicționar introduce calcularea hash-ului și lucrul asociat; șirurile necesită calcularea sau obținerea unui hash și compararea cheilor când este nevoie. Aceasta este o explicație a costurilor structurale, nu un benchmark înregistrat sau o dovadă că orice sarcină cu chei de tip șir este mai lentă decât orice alternativă.

[01:17:22](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4642s) Numele fac mai ușoară exprimarea proprietății. O bibliotecă poate folosi o **identitate cu spațiu de nume**, adică un nume cu un prefix de proprietate, precum `my-library.sum`, reducând conflictele accidentale fără a coordona poziții numerotate. Spațiul de nume este o convenție, nu o unicitate matematică; registrul detectează în continuare un nume înregistrat repetat.

## Restrângem construirea pentru a proteja contractul cheii

[01:17:44](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4664s) Ocolirea registrului reintroduce riscurile de conflict. La [01:17:50](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4670s) codul clientului își poate construi propria cheie tipizată, iar la [01:18:03](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4683s) îi poate da acea cheie o identitate existentă alegând un alt tip generic.

[Demonstrația conexă cu cheie falsificată](https://github.com/AntonC9018/uniCourse_csharp/blob/9e0565fa096e0e58867255a6cd1dd38d017a2d2c/examples/property_dict/Program.cs) construiește un `Key<MaliciousThing>` cu aceeași identitate de șir ca `Key<Dog>`; căutarea ulterioară a câinelui încearcă să convertească valoarea incompatibilă. Această reconstrucție minimală folosește un întreg și un șir:

```csharp
var registeredSum = new Key<int>("sum");
context.Add(registeredSum, 15);

var forgedName = new Key<string>("sum");
var name = context.Get(forgedName); // Eșec de cast la runtime: valoarea stocată este int.
```

[01:18:16](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4696s) Compilatorul are încredere în tipul declarat al cheii falsificate. Nu poate deduce din identitatea de șir că altă cheie spune că valoarea este un întreg. Aceasta ilustrează limita cheilor tipizate: apelurile publice cu siguranță de tip se bazează pe o asociere validă între identitate și tip.

[01:18:35](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4715s) Făci construirea cheii `internal`, păstrezi identitatea într-un câmp readonly și elimini constructorul public. [Constructorul public de dinaintea acestei protecții (cod conex)](https://github.com/AntonC9018/uniCourse_csharp/blob/9e0565fa096e0e58867255a6cd1dd38d017a2d2c/examples/property_dict/Program.cs) permite încă intenționat falsificarea; schimbarea bazată pe video este:

```csharp
public readonly struct Key<T>
{
    public readonly string Value;
    internal Key(string value) => Value = value;
}
```

[01:18:52](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4732s) Demonstrația compilează în continuare pentru că codul bibliotecii și al aplicației sunt în același proiect și în aceeași asamblare. `internal` este o **limită de acces la asamblare**: o asamblare este unitatea compilată care conține codul bibliotecii. Modificatorul nu impune accesul doar prin registru în interiorul acelei unități. La [01:19:36](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4776s) un consumator obișnuit dintr-o asamblare separată care importă biblioteca nu poate apela constructorul `internal`.

[01:19:40](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4780s) Construirea intenționată a unei chei false este mai puțin probabilă în uzul normal și cere cunoașterea unei identități existente și a unui tip incompatibil. La [01:19:53](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4793s) restricționarea constructorului este prezentată ca o protecție minimă, opțională, nu ca o cerință a modelului.

Proiectarea rezultată păstrează parcurgerea ignorantă de câmpurile domeniului, transmite contextul de execuție din exterior și oferă citirilor și scrierilor clientului un contract cu tipuri. Identitățile emise de registru și construirea restrânsă întăresc acel contract; existența intrării, politica pentru null, concurența și alocarea stocării rămân decizii separate.

## Studiu și traseul istoricului de cod

[01:07:26](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4046s) Pentru studiu practic, creează o intrare de salut și una de sumă, execută doi pași independenți care le folosesc și prezică ce modificări eșuează la compilare și care eșuează abia la extragere. [Laboratorul despre proprietăți](../../../labs/2_design/04_properties.md) îți cere să plasezi contextul într-o bibliotecă, datele și operațiile consumatorului într-un alt proiect, apoi să introduci înregistrarea centralizată a cheilor și să documentezi compromisurile.

**Opțional, dincolo de lecția implementată:** încearcă alocarea atomică a cheilor, un registru specializat pe tipul de cheie al unui context sau stocare ECS cu tipuri distincte. Acestea au fost discutate sau amânate, nu implementate în înregistrare. O referință completă asupra varianței este, de asemenea, opțională; înțelegerea conversiei doar de intrare folosite mai sus este suficientă pentru această lecție.

## Istoricul modificărilor de cod

Stările didactice relevante de mai jos sunt ordonate de la cea mai veche la cea mai recentă. Timestamp-urile sunt legături stabile către stările înregistrate; citările din repository alături exemplelor de mai sus identifică implementări conexe inspectate, nu instantanee exacte găsite prin presupunere.

- [00:15:01](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=901s) Începe exemplul de cod cu context furnizat de apelant și un contract fix pentru conductă.
- [00:16:15](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=975s) Elimină temporar contextul și captează datele salutului într-un pas.
- [00:17:08](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1028s) Rulează primul salut și observă `Hello, Антон`.
- [00:20:32](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1232s) Mută datele numelui de rulare într-un context explicit transmis fiecărui pas.
- [00:25:46](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1546s) Introdu o bază abstractă de context și contexte concrete definite de apelant.
- [00:29:24](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=1764s) Observă eșecul de cast pe context greșit la runtime.
- [00:35:14](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2114s) Introdu un context combinat care implementează interfețe de capabilitate.
- [00:36:54](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2214s) Propagă un tip de context generic prin constructor, pași și conductă.
- [00:41:05](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2465s) Declară parametrul de pas doar de intrare ca contravariant.
- [00:43:35](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2615s) Înlocuiește câmpurile de context statice cu un context dinamic comun.
- [00:45:35](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=2735s) Folosește chei poziționale partajate, apoi expune problemele de configurație și de cast.
- [00:56:43](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3403s) Înlocuiește stocarea pozițională rară cu intrări în dicționar.
- [01:00:29](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3629s) Ascunde dicționarul și introduce cheile tipizate și accesul generic.
- [01:05:59](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=3959s) Finalizează construirea contextului și inițializarea intrărilor.
- [01:08:02](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4082s) Centralizează crearea cheilor noi într-un registru.
- [01:11:33](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4293s) Stochează fiecare cheie înregistrată o singură dată într-un câmp stabil.
- [01:13:30](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4410s) Adaugă introspecție pentru depanare și metadate de registru lizibile.
- [01:16:36](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4596s) Treci de la identități numerice la nume de șir înregistrate.
- [01:18:35](https://www.youtube.com/watch?v=-YG0SKqdL0U&t=4715s) Restrângi construirea cheii peste limita de asamblare a bibliotecii.