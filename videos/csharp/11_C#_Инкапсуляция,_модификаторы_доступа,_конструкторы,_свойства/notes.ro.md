> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Constructori și proprietăți de încapsulare

[00:00:00](https://www.youtube.com/watch?v=IESVXL16OBA&t=0s) Această lecție transformă starea de sănătate a unui personaj de luptă, liber scrisibilă, într-o stare care poate fi modificată doar prin operații deliberate. Urmăriți schimbările: de la metodele extrase, prin câmpuri private și constructori, până la proprietăți ale căror accesori controlează citirea și scrierea. Parcurgerea finală explică modul în care un obiect nou primește valorile sale inițiale.

Ideile și termenii folosiți mai jos sunt:

- **Clasă, obiect, câmp și metodă:** o clasă definește un tip; un obiect este o instanță a acestuia; un câmp păstrează stare; o metodă efectuează o operație. O metodă de instanță lucrează asupra unui anumit obiect.
- **Parametru, argument și `this`:** un parametru este o variabilă în declarația unui element apelabil; un argument îi furnizează valoarea la apel. `this` identifică obiectul curent, acționând ca receptor implicit pentru operațiile de instanță.
- **Refactorizare și abstractizare:** refactorizarea rearanjează codul păstrând comportamentul intenționat; abstractizarea dă unei operații un nume, astfel încât apelanții să poată lucra fără să repete implementarea. O modificare făcută pentru a ilustra o idee nu este în mod automat o îmbunătățire de design.
- **Contract sau precondiție:** o presupunere care trebuie să fie adevărată atunci când o operație începe, precum „jucătorul este în viață”. **Validarea** verifică dacă o valoare respectă o regulă.
- **Aserțiune, Debug și Release:** `Debug.Assert` verifică o presupunere în timpul dezvoltării. Compilările Debug includ în mod normal aceste apeluri; compilările Release le omit în mod normal.
- **Modificator de acces, `public`, `private` și încapsulare:** modificatorii de acces controlează unde sunt accesibili membrii. Membrii publici sunt disponibili apelanților; membrii privați sunt restricționați la tipul care îi declară. Încapsularea ascunde datele stocate în spatele unor operații controlate.
- **Inițializator de obiect și `required`:** un inițializator atribuie membri accesibili după creare; `required` impune inițializarea membrului de către apelant. Nu acordă acces la un membru privat.
- **Funcție fabrică și constructor:** o fabrică este o funcție obișnuită care întoarce un obiect; un constructor inițializează un obiect nou creat și folosește numele clasei în declarație. `new` creează și inițializează un obiect.
- **Argument cu nume:** un argument etichetat cu numele parametrului său, precum `health: 20`, astfel încât sensul să fie vizibil fără a-i ține minte poziția.
- **Valoare implicită, `readonly` și construcție cu tip țintă:** valorile implicite sunt valorile inițiale ale stocării, aici zero pentru numere întregi. Un câmp readonly poate fi inițializat la declarare sau în constructorul corespunzător. Un `new(...)` cu tip țintă își ia tipul din declarația înconjurătoare.
- **Supraîncărcare și rezolvarea supraîncărcărilor:** supraîncărcările oferă liste diferite de parametri pentru același nume apelabil; rezolvarea selectează o supraîncărcare aplicabilă din argumentele furnizate.
- **Constructor de copiere, fabrică de copiere și metodă de copiere:** trei moduri de a construi un obiect nou din valorile unui obiect existent.
- **Getter, setter și limitare la interval (clamping):** un getter întoarce o valoare stocată; un setter o modifică și o poate valida. Clamping înlocuiește o valoare din afara intervalului cu o valoare de la limită, de exemplu înlocuirea stării de sănătate negative cu zero.
- **Proprietate și accessor:** o proprietate este un membru cu nume ale cărui accesori `get` și `set` realizează citirea și scrierea, cu sintaxe de apel asemănătoare câmpurilor.
- **Proprietate automată și câmp de susținere:** o proprietate automată are accesori fără corp; compilatorul îi furnizează stocarea ascunsă, numită câmp de susținere. Accesorii expliciți pot folosi un câmp pe care îl declarați.
- **`value`, PascalCase și convenția cu liniuță de subliniere:** `value` este parametrul implicit al unui setter de proprietate. PascalCase începe cuvintele cu majuscule, ca în `Health`; câmpurile private folosesc convențional o liniuță de subliniere la început, ca în `_health`.
- **Referință, memorie dinamică și stocare temporară pe stivă:** o referință identifică un obiect; lecția desenează obiectul în memorie dinamică și parametrii în memorie temporară, pentru a le deosebi duratele de viață. Adresele numerice desenate sunt etichete explicative, nu adrese pe care o referință C# obișnuită le expune.

[Laboratorul de încapsulare](../../../labs/1_basic/17_encapsulation.md) aplică aceste idei unei biblioteci. Sarcinile sale suplimentare de filtrare depășesc această parcurgere a luptei.

Sursa de luptă nu este prezentă în acest checkout. Fiecare bloc de cod de mai jos este o **reconstrucție simplificată a stării din videoclipul asociat**, cu denumiri coerente pentru claritate, nu o versiune exactă din commit. Singura citare de laborator imutabilă este identificată drept exemplu conex. Aceste etape sunt alternative și modificări succesive, nu blocuri de lipit împreună într-un singur program.

## Pregătește clasa pentru modificări controlate ale stării de sănătate

[00:00:05](https://www.youtube.com/watch?v=IESVXL16OBA&t=5s) Începeți prin a muta înapoi în clasa personajului operația `Take` extrăsă anterior. O **metodă** este o operație declarată pe clasă; o **metodă de instanță** primește obiectul asupra căruia acționează prin receptorul implicit `this`. Astfel, încapsularea are o clasă concretă cu care să lucreze.

Problema inițială este afișarea repetată, legată de un nume de jucător codificat fix. La [00:00:23](https://www.youtube.com/watch?v=IESVXL16OBA&t=23s), extrageți acea afișare într-o metodă cu un **parametru**: numele personajului atacat. **Argumentul** apelului furnizează „Player” sau numele inamicului. O stare simplificată, înainte și după, este:

```csharp
// Before: a fixed label at the call site.
Console.WriteLine($"Player health: {player.Health}");

// After: inside Stat, with Health still public at this stage.
public int Health;
public void PrintHealthOfEnemy(string name)
{
    Console.WriteLine($"{name} health: {Health}");
}
// Call site:
player.PrintHealthOfEnemy("Player");
```

Aici `int` stochează un număr întreg, `string` stochează text, `void` înseamnă că metoda nu întoarce nicio valoare, iar `Console.WriteLine` afișează o linie. Șirul prefixat cu semnul dolar inserează expresiile scrise în acolade.

La [00:00:50](https://www.youtube.com/watch?v=IESVXL16OBA&t=50s), mutați `PrintHealthOfEnemy` mai aproape de declarația lui `Health`. Codul care atinge un câmp rămâne lângă acel câmp, ceea ce face relația dintre stare și operații mai ușor de observat. Mutarea codului și înlocuirea unei etichete fixe cu un parametru pregătesc clasa pentru modificările ulterioare.

## Partajează și redenumește operația de damage

[00:01:00](https://www.youtube.com/watch?v=IESVXL16OBA&t=60s) Apoi, extrageți operația de damage după metodele existente și înlocuiți apelurile funcției vechi acolo unde era folosită. O funcție obișnuită poate primi ținta ca prim parametru explicit; metoda de instanță corespunzătoare primește aceeași țintă implicit, prin `this`. Abstractizarea este operația „primește damage”, nu scăderea brută.

```csharp
// Conceptual function form:
static void ApplyDamage(Stat target, int damage)
{
    target.Health -= damage;
}

// Corresponding member inside Stat:
public void TakeDamage(int damage)
{
    Health -= damage;
}
// Replacement call:
enemy.TakeDamage(player.Attack);
```

`static` marchează o operație fără receptor de obiect implicit. `-=` scade și atribuie rezultatul. Acest exemplu izolează actualizarea câmpului; funcția de luptă din jur își păstrează în continuare verificarea morții și mesajul.

La [00:01:32](https://www.youtube.com/watch?v=IESVXL16OBA&t=92s), folosiți operația extrasă și pentru jucător. Logica din jur a jucătorului diferă ușor, dar acțiunea comună de damage se potrivește în continuare. La [00:01:45](https://www.youtube.com/watch?v=IESVXL16OBA&t=105s), redenumiți noul membru pentru a exprima faptul că damage-ul este primit. **Refactorizarea** rearanjează codul, iar **abstractizarea** dă acțiunii un nume util. Instructorul face explicit această modificare pentru a susține explicația următoare; schimbarea codului în sine nu este dovadă de îmbunătățire.

## Identifică presupunerea că jucătorul este în viață

[00:02:02](https://www.youtube.com/watch?v=IESVXL16OBA&t=122s) În acest stadiu, damage-ul asupra jucătorului se produce printr-o singură funcție de luptă. Imediat după, aceasta verifică dacă jucătorul a murit și afișează mesajul decesului. Păstrarea acestor acțiuni împreună face succesiunea previzibilă.

```csharp
player.TakeDamage(enemy.Attack);
if (player.Health <= 0)
{
    Console.WriteLine("Player died");
}
```

La [00:02:25](https://www.youtube.com/watch?v=IESVXL16OBA&t=145s), imaginați-vă o altă parte a programului care atribuie direct zero:

```csharp
player.Health = 0; // Bypasses the damage-and-message sequence.
```

Jucătorul este acum mort fără a fi primit damage prin acea succesiune, deci mesajul decesului nu a fost afișat niciodată. Aceasta este o atribuire, `=`; o verificare de egalitate, `==`, ar doar să inspecteze valoarea.

La [00:02:46](https://www.youtube.com/watch?v=IESVXL16OBA&t=166s), funcția de atac presupune în continuare că jucătorul este în viață. Astfel, un jucător mort poate schimba atacuri cu inamicul, producând un comportament incorect. Acea presupunere este o **precondiție**, un contract pe care apelanții trebuie să-l respecte. La [00:02:58](https://www.youtube.com/watch?v=IESVXL16OBA&t=178s), se ia în considerare o verificare a decesului, dar sarcina mai imediată este expunerea unui apel care încalcă precondiția existentă.

## Expune starea invalidă cu o aserțiune

[00:03:06](https://www.youtube.com/watch?v=IESVXL16OBA&t=186s) Adăugați o **aserțiune**, o verificare în timpul dezvoltării a unei presupuneri, la începutul funcției de atac. `Debug.Assert` primește o condiție booleană; fals înseamnă că presupusul contract a fost încălcat. Stare simplificată din videoclip:

```csharp
using System.Diagnostics;

static void AttackPlayer(Stat player)
{
    Debug.Assert(player.Health > 0);
    // Existing exchange of attacks follows.
}
```

La [00:03:24](https://www.youtube.com/watch?v=IESVXL16OBA&t=204s), distingeți compilările **Debug**, folosite în mod normal la testare și dezvoltare, de compilările **Release**, folosite în mod normal pentru programul finalizat. La [00:03:42](https://www.youtube.com/watch?v=IESVXL16OBA&t=222s), scopul este să prindem erori considerate imposibile în cod corect, fără să lăsăm acele verificări în compilarea normală Release. Mai precis, apelurile la `Debug.Assert` sunt condiționate de simbolul de compilare `DEBUG`; configurația compilării determină de obicei acel simbol. O aserțiune nu este înlocuitor pentru validarea la timpul de execuție care este necesară.

La [00:04:10](https://www.youtube.com/watch?v=IESVXL16OBA&t=250s), starea de sănătate este zero, deci condiția „jucător în viață” este falsă. Execuția de la [00:04:11](https://www.youtube.com/watch?v=IESVXL16OBA&t=251s) declanșează aserțiunea și se oprește/se blochează în mediul demonstrat. Eșecul este raportat în punctul în care funcția de atac ar continua altfel sub o presupunere greșită. Raportarea aserțiunilor depinde de mediul de dezvoltare; înregistrarea demonstrează comportamentul de eșec, nu o garanție universală de excepție.

La [00:04:28](https://www.youtube.com/watch?v=IESVXL16OBA&t=268s), continuarea cu o stare invalidă ar fi mai rea decât dezvăluirea imediată a eșecului. Următoarea întrebare este cum să împiedicăm codul fără legătură să atribuie `Health` de la început.

<details>
<summary>Ar rezolva ștergerea aserțiunii bug-ul cu jucătorul mort?</summary>
Nu. Ar elimina diagnosticul și ar lăsa funcția de atac să continue cu aceeași stare de sănătate invalidă. Tranziția de stare și limita de acces trebuie reparate.
</details>

## Restrânge câmpul cu private

[00:04:45](https://www.youtube.com/watch?v=IESVXL16OBA&t=285s) Un **modificator de acces** controlează unde este disponibil un membru. Înlocuiți `public` cu `private` pe câmpul de sănătate. `public` permite folosirea din exteriorul clasei; `private` restricționează accesul obișnuit la tipul care declară. Modificare simplificată:

```csharp
// Before:
public int Health;

// After, inside Stat:
private int Health;

public void TakeDamage(int damage)
{
    Health -= damage; // Still permitted here.
}
```

La [00:05:09](https://www.youtube.com/watch?v=IESVXL16OBA&t=309s), metodele declarate în `Stat` pot în continuare să citească și să scrie câmpul, inclusiv o metodă fabrică declarată în același tip. La [00:05:25](https://www.youtube.com/watch?v=IESVXL16OBA&t=325s), limita este clasa, nu o anumită metodă sau un anumit obiect: o metodă `Stat` poate accesa și câmpurile private ale altui obiect `Stat`.

La [00:05:37](https://www.youtube.com/watch?v=IESVXL16OBA&t=337s), codul din exterior nu mai poate citi sau scrie `Health`. Editorul respinge referințele externe la el; numele membrului nu poate fi folosit cu succes acolo. La [00:05:49](https://www.youtube.com/watch?v=IESVXL16OBA&t=349s), **citirea** din exterior este amânată deliberat. Mai întâi examinăm interdicția scrierii directe, care este intenția subliniată la [00:05:55](https://www.youtube.com/watch?v=IESVXL16OBA&t=355s).

La [00:06:00](https://www.youtube.com/watch?v=IESVXL16OBA&t=360s), apelul de damage funcționează în continuare, deoarece modifică indirect câmpul privat printr-o metodă care îl poate accesa. La [00:06:12](https://www.youtube.com/watch?v=IESVXL16OBA&t=372s), acea metodă devine punctul de intrare controlat. Apelanții cer o operație; clasa deține detaliile actualizării.

## Definește încapsularea prin operații

[00:06:36](https://www.youtube.com/watch?v=IESVXL16OBA&t=396s) Starea de sănătate are trei operații utile: afișarea ei, micșorarea ei și verificarea dacă personajul a murit. Acestea acoperă interacțiunile necesare exemplului. O stare simplificată din videoclip, orientată pe operații, este:

```csharp
private int Health;
public void PrintHealth(string name)
{
    Console.WriteLine($"{name} health: {Health}");
}
public void TakeDamage(int damage)
{
    Health -= damage;
}
public bool IsDead()
{
    return Health <= 0;
}
```

Aici `bool` este o valoare adevărat/fals, iar `return` furnizează rezultatul metodei. Aceste operații sunt ilustrative; stadiile următoare rafinează modul în care sunt verificate actualizările.

La [00:06:54](https://www.youtube.com/watch?v=IESVXL16OBA&t=414s) și [00:07:03](https://www.youtube.com/watch?v=IESVXL16OBA&t=423s), **încapsularea** înseamnă păstrarea datelor ascuse, permițând totuși interacțiunea prin metodele clasei. Nu înseamnă că nicio operație nu poate modifica datele. La [00:07:13](https://www.youtube.com/watch?v=IESVXL16OBA&t=433s), metodele oferă un control granular: „primește damage” are un sens pe care „suprascrie acest întreg” nu îl are. La [00:07:25](https://www.youtube.com/watch?v=IESVXL16OBA&t=445s), această limită împiedică atribuirea exterioară accidentală a unei valori invalide pentru situația curentă. Clasa trebuie totuși să implementeze corect propriile metode; doar `private` nu validează un număr întreg.

## Repară crearea cu o fabrică internă

[00:07:45](https://www.youtube.com/watch?v=IESVXL16OBA&t=465s) Odată ce starea de sănătate devine privată, editorul marchează accesările directe anterioare ca erori. La [00:07:51](https://www.youtube.com/watch?v=IESVXL16OBA&t=471s), crearea are nevoie și ea de atenție: un **inițializator de obiect** atribuie membri accesibili după crearea obiectului și nu poate atribui un câmp privat din exteriorul clasei sale.

```csharp
// Former external construction:
var enemy = new Stat { Attack = 7, Health = 20 };
// Health is now inaccessible here.
```

La [00:08:09](https://www.youtube.com/watch?v=IESVXL16OBA&t=489s), un **constructor** este introdus ca mecanism de inițializare a stării ascunse, asemănător unei fabrici. Mai întâi explorăm o **funcție fabrică** obișnuită, o funcție care întoarce un obiect. La [00:08:18](https://www.youtube.com/watch?v=IESVXL16OBA&t=498s), `required` nu poate rămâne pe acest câmp privat: membrii required trebuie să fie suficient de accesibili încât apelanții să îi poată inițializa. doar eliminarea lui `required` nu acordă acces extern prin inițializator. Eliminați combinația invalidă private-required și furnizați inițializarea în interiorul tipului.

La [00:08:22](https://www.youtube.com/watch?v=IESVXL16OBA&t=502s), generalizați fabrica ca să primească câte un parametru pentru fiecare valoare de inițializat. La [00:08:30](https://www.youtube.com/watch?v=IESVXL16OBA&t=510s), folosiți-o pentru a crea inamicul, în loc de a atribui câmpul privat la locul de creare din exterior. Stare intermediară simplificată:

```csharp
// Inside Stat:
public static Stat Create(int attack, int health)
{
    return new Stat { Attack = attack, Health = health };
}

// Outside Stat:
var enemy = Stat.Create(7, 20);
```

Acest inițializator este legal în interiorul lui `Stat`, deoarece acel cod își poate accesa propriul membru privat. Se bazează pe construcția fără parametri disponibilă în acest stadiu intermediar. Modificările următoare înlocuiesc acest mecanism cu un constructor.

## Fă argumentele fabricii lizibile

[00:08:42](https://www.youtube.com/watch?v=IESVXL16OBA&t=522s) Apelul fabricii `Create(7, 20)` este ambiguu pentru un cititor care nu își amintește pozițiile parametrilor. Sunt aceste valorile attack și apoi health, sau health și apoi attack? La [00:08:49](https://www.youtube.com/watch?v=IESVXL16OBA&t=529s), folosiți **argumente cu nume**, care etichetează valorile cu numele parametrilor lor.

La [00:09:01](https://www.youtube.com/watch?v=IESVXL16OBA&t=541s), două puncte leagă numele parametrului de argumentul furnizat. Modificare simplificată la locul apelului:

```csharp
var enemy = Stat.Create(attack: 7, health: 20);

// The same mapping, written in the other order:
var sameValues = Stat.Create(health: 20, attack: 7);
```

La [00:09:06](https://www.youtube.com/watch?v=IESVXL16OBA&t=546s), apelul cu argumente cu nume reordonate funcționează ca înainte: numele păstrează ce valoare ajunge în ce parametru. Acest exemplu numeste fiecare argument; rezultatul său nu implică reguli de reordonare arbitrară pentru amestecuri de argumente poziționale și cu nume.

La [00:09:24](https://www.youtube.com/watch?v=IESVXL16OBA&t=564s), inamicul vine în continuare din fabrică și se comportă ca obiectul inițializat direct mai devreme. Denumirea îmbunătățește lizibilitatea, dar rămâne o funcție de creare generică. Pasul următor folosește construcția de inițializare dedicată a limbajului.

## Înlocuiește fabrica cu un constructor

[00:09:36](https://www.youtube.com/watch?v=IESVXL16OBA&t=576s) Un **constructor** inițializează un obiect nou creat. El îndeplinește rolul de inițializare al fabricii, dar este invocat ca parte din `new`. La [00:09:43](https://www.youtube.com/watch?v=IESVXL16OBA&t=583s), „fără nume propriu” înseamnă că nu poate avea un nume de metodă arbitrar. La [00:09:49](https://www.youtube.com/watch?v=IESVXL16OBA&t=589s), declarați-l cu numele clasei și o listă de parametri, fără tip de întoarcere.

```csharp
class Stat
{
    public int Attack;
    private int Health;

    public Stat(int attack, int health)
    {
        Attack = attack;
        Health = health;
    }
}
```

La [00:10:02](https://www.youtube.com/watch?v=IESVXL16OBA&t=602s), câmpurile întregi au inițial **valoarea implicită**, zero; corpul constructorului atribuie apoi valorile furnizate. La [00:10:20](https://www.youtube.com/watch?v=IESVXL16OBA&t=620s), gândiți-vă la un constructor de instanță ca primind un receptor de obiect ascuns, `this`, prin care poate scrie câmpurile obiectului nou. Acest receptor este implicit în sintaxa C# obișnuită.

La [00:10:34](https://www.youtube.com/watch?v=IESVXL16OBA&t=634s), înlocuiți apelul fabricii cu o construcție directă. La [00:10:56](https://www.youtube.com/watch?v=IESVXL16OBA&t=656s), attack și health intră acum prin argumentele constructorului, în locul unui inițializator de obiect extern:

```csharp
var enemy = new Stat(attack: 7, health: 20);
```

La [00:11:00](https://www.youtube.com/watch?v=IESVXL16OBA&t=660s), compilatorul cere în continuare inițializarea de către apelant dacă `Attack` rămâne `required`. Atribuirea lui în acest constructor nu îi spune compilatorului, prin ea însăși, că obligația membrului required este îndeplinită. La [00:11:05](https://www.youtube.com/watch?v=IESVXL16OBA&t=665s), eliminați `required` din `Attack` în acest exemplu, deoarece inițializarea aparține acum constructorului.

La [00:11:16](https://www.youtube.com/watch?v=IESVXL16OBA&t=676s), refactorizați locurile de creare din fișierul principal, precum și definiția tipului, astfel încât inițializarea să urmeze același traseu prin constructor. Rezultatul este un singur loc responsabil de atribuirea acelor valori inițiale.

## Folosește constructori fără a pierde limita de acces

[00:11:29](https://www.youtube.com/watch?v=IESVXL16OBA&t=689s) Un beneficiu al constructorilor este inițializarea câmpurilor **readonly**: după inițializarea permisă, acestea nu mai pot fi reatribuite de metodele obișnuite de instanță. Înregistrarea subliniază atribuirea în constructor; un inițializator de câmp la declarare este, de asemenea, permis.

La [00:11:36](https://www.youtube.com/watch?v=IESVXL16OBA&t=696s), un al doilea beneficiu este **construcția cu tip țintă**: `new(...)` poate omite numele tipului când contextul înconjurător îl furnizează. Alternative simplificate din starea videoclipului:

```csharp
class Stat
{
    public readonly int Attack;
    private int Health;

    public Stat(int attack, int health)
    {
        Attack = attack;
        Health = health;
    }
}

Stat player = new(attack: 7, health: 20);
```

Tipul variabilei declarate, `Stat`, furnizează tipul pentru `new`. Aceasta este altceva decât `var`, care cere compilatorului să deducă tipul variabilei din inițializatorul ei.

La [00:12:03](https://www.youtube.com/watch?v=IESVXL16OBA&t=723s) și [00:12:13](https://www.youtube.com/watch?v=IESVXL16OBA&t=733s), un câmp privat poate fi inițializat printr-un constructor sau prin metode fabrică declarate în interiorul aceluiași tip. Ele funcționează deoarece acolo codul poate scrie câmpul. La [00:12:20](https://www.youtube.com/watch?v=IESVXL16OBA&t=740s), restricția rămâne: apelanții din exterior nu pot atribui singuri acel câmp.

La [00:12:27](https://www.youtube.com/watch?v=IESVXL16OBA&t=747s), luați în calcul compromisul de confort. Fără încapsulare, o clasă cu toate câmpurile publice poate folosi un inițializator și nu are nevoie de niciun constructor scris de utilizator. Compilatorul furnizează un constructor fără parametri când clasa nu declară niciunul. Această abordare păstrează inițializarea confortabilă, dar renunță la limita de acces controlată.

## Alege supraîncărcări de constructor și fabrici cu nume

[00:12:40](https://www.youtube.com/watch?v=IESVXL16OBA&t=760s) O **supraîncărcare** de constructor este un alt constructor cu o listă de parametri diferită. Adăugați o supraîncărcare fără parametri, care să furnizeze valorile implicite zero, alături de versiunea cu doi parametri. Valorile implicite sunt valorile zero pe care câmpurile le primesc înainte de execuția corpului; atribuirile explicite fac vizibilă construcția implicită intenționată.

La [00:12:56](https://www.youtube.com/watch?v=IESVXL16OBA&t=776s), comparați construcția fără parametri cu o fabrică vagă `CreateDefault`. La [00:13:13](https://www.youtube.com/watch?v=IESVXL16OBA&t=793s), eliminați acea fabrică și folosiți în schimb un constructor explicit. Stare rezultată simplificată:

```csharp
public Stat()
{
    Attack = 0;
    Health = 0;
}

public Stat(int attack, int health)
{
    Attack = attack;
    Health = health;
}

Stat empty = new();
Stat enemy = new(attack: 7, health: 20);
```

O supraîncărcare explicită fără parametri contează odată ce a fost declarat alt constructor: compilatorul nu mai furnizează cel implicit fără parametri. Orice suprascriere ulterioară a valorilor implicite trebuie să folosească în continuare operații accesibile; construirea nu face starea de sănătate privată scrisibilă din exterior.

La [00:13:20](https://www.youtube.com/watch?v=IESVXL16OBA&t=800s) și [00:13:28](https://www.youtube.com/watch?v=IESVXL16OBA&t=808s), **rezolvarea supraîncărcărilor** selectează constructorul fără parametri pentru niciun argument și constructorul cu doi parametri pentru acești doi argumente. Numărul de argumente distinge aceste supraîncărcări particulare; rezolvarea supraîncărcărilor ia în considerare în general și tipurile parametrilor și alte reguli de aplicabilitate.

La [00:13:45](https://www.youtube.com/watch?v=IESVXL16OBA&t=825s), constructorii nu pot primi un nume arbitrar precum `CreateStrong`: numele din declarație trebuie să corespundă tipului. La [00:13:59](https://www.youtube.com/watch?v=IESVXL16OBA&t=839s), o variantă de creare cu nume, al cărei sens nu este distins de argumente, poate folosi în continuare o funcție fabrică. Constructorii sunt utili fără a înlocui fiecare fabrică posibilă.

## Copiază un obiect în trei moduri echivalente

[00:14:06](https://www.youtube.com/watch?v=IESVXL16OBA&t=846s) Un **constructor de copiere** ia alt obiect de același tip și inițializează câmpurile acestui obiect din el. Un constructor `Stat` poate citi starea de sănătate privată a celuilalt obiect `Stat`, deoarece accesul privat aparține tipului.

La [00:14:24](https://www.youtube.com/watch?v=IESVXL16OBA&t=864s), o **fabrică de copiere** este o funcție obișnuită la fel de validă, care întoarce un obiect nou construit. La [00:14:42](https://www.youtube.com/watch?v=IESVXL16OBA&t=882s), o **metodă de copiere** folosește câmpurile obiectului curent ca argumente pentru constructor. Stare simplificată din videoclip, toate declarate în interiorul lui `Stat`:

```csharp
public Stat(Stat other)
{
    Attack = other.Attack;
    Health = other.Health;
}

public static Stat Create(Stat source)
{
    return new Stat(source.Attack, source.Health);
}

public Stat Copy()
{
    return new Stat(Attack, Health);
}
```

La [00:14:54](https://www.youtube.com/watch?v=IESVXL16OBA&t=894s), ilustrați folosirea lor cu jucătorul:

```csharp
Stat a = new(player);          // Copy constructor.
Stat b = Stat.Create(player); // Copy factory.
Stat c = player.Copy();       // Copy method.
```

La [00:15:09](https://www.youtube.com/watch?v=IESVXL16OBA&t=909s), acestea sunt opțiuni interschimbabile pentru duplicarea stării întregi a acestui exemplu. Fiecare creează un obiect nou; schimbarea stării sale de sănătate nu schimbă starea de sănătate întreagă a obiectului original. Aceasta este altceva decât atribuirea `Stat a = player;`, care copiază referința și se referă la același obiect.

La [00:15:16](https://www.youtube.com/watch?v=IESVXL16OBA&t=916s), completarea din IDE prezintă constructorul de copiere, fabrica și metoda de copiere ca alternative identificabile. Demonstrația stabilește alegeri echivalente, nu o cerință de a păstra toate trei în fiecare clasă.

<details>
<summary>De ce poate constructorul de copiere citi starea de sănătate privată a altui obiect?</summary>
Pentru că este cod declarat în interiorul aceluiași tip. Accesul privat nu este restricționat doar la instanța curentă.
</details>

## Restaurează citirea cu un getter

[00:15:36](https://www.youtube.com/watch?v=IESVXL16OBA&t=936s) Revenim acum la nevoia amânată mai devreme: citirea stării de sănătate fără a acorda permisiunea de a o suprascrie. La [00:15:45](https://www.youtube.com/watch?v=IESVXL16OBA&t=945s), motivul concret este afișarea stării curente de sănătate a jucătorului pentru depanare, într-o buclă. La [00:16:01](https://www.youtube.com/watch?v=IESVXL16OBA&t=961s), codul din exterior nu poate citi câmpul privat.

La [00:16:12](https://www.youtube.com/watch?v=IESVXL16OBA&t=972s), adăugați un **getter**, o metodă care întoarce valoarea stocată. La [00:16:18](https://www.youtube.com/watch?v=IESVXL16OBA&t=978s), înlocuiți citirile bazate pe câmp cu apeluri la `GetHealth`. Stare simplificată din videoclip:

```csharp
// Inside Stat:
public int GetHealth()
{
    return Health;
}

// In the surrounding loop:
Console.WriteLine(player.GetHealth());
```

La [00:16:29](https://www.youtube.com/watch?v=IESVXL16OBA&t=989s), câmpul rămâne încapsulat: un număr întreg întors permite observarea fără a permite atribuirea directă a întregului stocat. La [00:16:39](https://www.youtube.com/watch?v=IESVXL16OBA&t=999s), acest design poate adăuga ulterior o metodă de scriere, păstrând câmpul de bază privat. Permisiunile de citire și de scriere sunt alegeri separate.

## Controlează scrierile cu un setter

[00:16:56](https://www.youtube.com/watch?v=IESVXL16OBA&t=1016s) Un **setter** este o operație care stochează o valoare furnizată. Începeți cu `SetHealth`, care își atribuie parametrul; la [00:17:03](https://www.youtube.com/watch?v=IESVXL16OBA&t=1023s), adăugați **validare**, o verificare că valoarea respectă regula stării de sănătate nenegative. La [00:17:15](https://www.youtube.com/watch?v=IESVXL16OBA&t=1035s), fiecare actualizare dirijată prin acest setter întâlnește acea verificare.

La [00:17:24](https://www.youtube.com/watch?v=IESVXL16OBA&t=1044s), schimbați modificatorul de acces al setterului independent de getter. La [00:17:28](https://www.youtube.com/watch?v=IESVXL16OBA&t=1048s), făcându-l privat, blocăm apelurile din exterior, dar permitem în continuare apelurile din alte metode ale lui `Stat`. Stare intermediară simplificată:

```csharp
private int Health;

public int GetHealth()
{
    return Health;
}

private void SetHealth(int health)
{
    Debug.Assert(health >= 0);
    Health = health;
}

public void TakeDamage(int damage)
{
    SetHealth(Health - damage);
}
```

La [00:17:41](https://www.youtube.com/watch?v=IESVXL16OBA&t=1061s), înlocuiți scrierile directe ale stării de sănătate din clasă cu apeluri la setter, astfel încât validarea să se execute și pentru aceste modificări. Doar definirea unui setter nu împiedică codul intern să îl ocolească atribuind direct câmpul.

La [00:18:02](https://www.youtube.com/watch?v=IESVXL16OBA&t=1082s), un singur apel de damage poate calcula o stare de sănătate negativă. În versiunea cu aserțiune, aceasta încalcă contractul și poate opri/bloca execuția de depanare; aritmetica însăși cu întregi negativi nu este o cădere a C#. La [00:18:15](https://www.youtube.com/watch?v=IESVXL16OBA&t=1095s), **clamping** oferă o altă politică: înlocuiește rezultatele negative cu zero, în loc să le respingă.

```csharp
private void SetHealth(int health)
{
    if (health < 0)
    {
        health = 0;
    }
    Health = health;
}
```

Clamping modifică parametrul primit înainte de a-l stoca. Validarea respinge sau diagnostichează valorile greșite; clamping acceptă o valoare corectată, de la limită. Ele exprimă politici diferite. Aserțiunile singure nu impun respingerea într-o compilare normală Release.

## Leagă citirea și scrierea într-o proprietate

[00:18:29](https://www.youtube.com/watch?v=IESVXL16OBA&t=1109s) O **proprietate** leagă operațiile conexe de citire și scriere sub un singur nume de membru. **Accesorii** săi, `get` și `set`, sunt operații asemănătoare metodelor, în timp ce apelanții folosesc sintaxe asemănătoare câmpurilor. La [00:18:43](https://www.youtube.com/watch?v=IESVXL16OBA&t=1123s), înregistrarea introduce această sintaxă C# frecventă: acoladele de după tipul și numele unui membru conțin operațiile permise ale acestuia. Se aseamănă cu o declarație de câmp, dar declară o proprietate.

La [00:18:53](https://www.youtube.com/watch?v=IESVXL16OBA&t=1133s), alegeți o proprietate lizibilă din exterior, cu scrierea restricționată la clasă. La [00:19:04](https://www.youtube.com/watch?v=IESVXL16OBA&t=1144s), faceți accesul de citire al proprietății public. La [00:19:12](https://www.youtube.com/watch?v=IESVXL16OBA&t=1152s), împărțirea rezultată a permisiunilor este:

```csharp
public int Health { get; private set; }

// Outside Stat:
Console.WriteLine(player.Health);
// player.Health = 0; // Not allowed: the setter is private.
```

Modificatorul `public` aparține aici proprietății; `get` îl moștenește, în timp ce `private set` este mai restrictiv. La [00:19:21](https://www.youtube.com/watch?v=IESVXL16OBA&t=1161s), accesorii sunt operații complete asemănătoare metodelor și pot avea corpuri explicite.

La [00:19:29](https://www.youtube.com/watch?v=IESVXL16OBA&t=1169s), distingeți numele pe măsură ce codul crește: metodele folosesc **PascalCase**, precum `GetHealth`, iar stocarea privată folosește convențional un nume cu litere mici și o liniuță de subliniere la început, precum `_health`. Proprietățile folosesc și ele frecvent PascalCase. Aceste convenții ajută cititorii să recunoască rolurile; ele nu determină permisiunile de acces.

## Înțelege ce generează o proprietate automată

[00:19:41](https://www.youtube.com/watch?v=IESVXL16OBA&t=1181s) Cu o **proprietate automată**, `get;` și `set;` fără corp cer compilatorului să furnizeze implementarea și un **câmp de susținere** ascuns, stocarea valorii proprietății. Actualizați operația de damage ca să folosească proprietatea:

```csharp
public int Health { get; private set; }

public void TakeDamage(int damage)
{
    Health = Health - damage;
}
```

La [00:19:58](https://www.youtube.com/watch?v=IESVXL16OBA&t=1198s) și [00:20:04](https://www.youtube.com/watch?v=IESVXL16OBA&t=1204s), setterul generat doar stochează valoarea furnizată. Înlocuirea setterului anterior, verificat sau cu clamping, cu acest setter fără corp îi elimină logica personalizată. Proprietățile automate nu reproduc automat validarea dintr-o metodă care a fost ștearsă.

La [00:20:13](https://www.youtube.com/watch?v=IESVXL16OBA&t=1213s), setterul generat este privat deoarece `set` a fost declarat privat. La [00:20:17](https://www.youtube.com/watch?v=IESVXL16OBA&t=1217s), citirea din exterior folosește acum `player.Health` în loc de `player.GetHealth()`. Getterul generat este un accessor, nu o metodă sursă numită `GetHealth`.

La [00:20:42](https://www.youtube.com/watch?v=IESVXL16OBA&t=1242s), accesibilitatea proprietății guvernează metodele sale accessor, cu excepția cazului în care un accessor este făcut mai restrictiv. La [00:20:53](https://www.youtube.com/watch?v=IESVXL16OBA&t=1253s), câmpul de susținere creat de compilator are un nume ascuns pe care acest exemplu nu îl poate referi în cod sursă normal. Membrul vizibil este proprietatea.

<details>
<summary>Un setter privat de proprietate automată garantează starea de sănătate nenegativă?</summary>
Nu. Limitează cine poate scrie, dar o atribuire internă permisă poate stoca în continuare un număr negativ. Păstrarea regulii anterioare necesită un corp explicit de setter sau altă operație verificată.
</details>

## Extinde accesorii pentru a restaura validarea

[00:21:04](https://www.youtube.com/watch?v=IESVXL16OBA&t=1264s) În locul stocării ascunse a proprietății automate, declarați propriul **câmp de susținere** și scrieți corpuri explicite de **accessor**. La [00:21:18](https://www.youtube.com/watch?v=IESVXL16OBA&t=1278s) și [00:21:20](https://www.youtube.com/watch?v=IESVXL16OBA&t=1280s), extindeți `get` pentru a întoarce acel câmp și `set` pentru a-l atribui. La [00:21:25](https://www.youtube.com/watch?v=IESVXL16OBA&t=1285s), parametrul implicit al setterului se numește `value`: este valoarea primită, nu numele proprietății. Reconstrucție simplificată intermediară a stării de sănătate:

```csharp
private int _health;

public int Health
{
    get
    {
        return _health;
    }
    private set
    {
        _health = value;
    }
}

public void TakeDamage(int damage)
{
    Health = Health - damage;
}
```

La [00:21:33](https://www.youtube.com/watch?v=IESVXL16OBA&t=1293s), descompunem atribuirea finală: partea sa dreaptă citește `Health` prin `get`, scade damage-ul, iar partea sa stângă trimite rezultatul prin `set` ca `value`. Așadar, accesorii se execută, deși la locul apelului operația pare una pe câmp.

La [00:21:42](https://www.youtube.com/watch?v=IESVXL16OBA&t=1302s), restaurați validarea în setter și verificați `value`, parametrul primit vizibil acolo. Modificarea acelui accessor este:

```csharp
private set
{
    Debug.Assert(value >= 0);
    _health = value;
}
```

Atribuirea din constructor prin `Health` poate folosi același setter; atribuirea directă către `_health` l-ar ocoli.

Un [exemplu conex din laboratorul comisat](https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/17_encapsulation.md) pune, de asemenea, o aserțiune într-un setter. Aceasta este sintaxă de proprietate de sprijin, nu sursa exactă de luptă. Nucleul său simplificat este:

```csharp
set
{
    Debug.Assert(value != "");
    _nameInclude = value;
}
```

Fragmentul din laboratorul comisat expune public câmpul său de susținere; acel detaliu nu ar trebui copiat atunci când scopul este prevenirea ocolirii proprietății.

La [00:21:50](https://www.youtube.com/watch?v=IESVXL16OBA&t=1310s), proprietățile încapsulează accesul la câmpuri și sunt preferate aici față de metode separate de getter/setter, deoarece ambele operații împart același nume de proprietate. Faptul că folosiți aserțiuni sau clamping rămâne o politică explicită în accessor.

## Distinge câmpurile, parametrii și proprietățile

[00:22:02](https://www.youtube.com/watch?v=IESVXL16OBA&t=1322s) O linie de subliniere la început marchează convențional un câmp privat. Ea distinge stocarea permanentă de un parametru cu nume similar, atunci când codul este citit izolat. **PascalCase** capitalizează începutul fiecărui cuvânt; o proprietate precum `Health` și o metodă precum `GetHealth` folosesc acest stil. Un **parametru** cu litere mici precum `health` sau valoarea specială `value` a setterului reprezintă date de intrare.

Versiune simplificată a numelor, pentru constructorul și ideea de câmp de susținere din videoclip:

```csharp
private int _health;

public Stat(int attack, int health)
{
    Attack = attack;
    _health = health;
}
```

La [00:22:46](https://www.youtube.com/watch?v=IESVXL16OBA&t=1366s), denumirea este un indiciu al sensului. Un membru în PascalCase poate fi un câmp public sau o proprietate; capitalizarea singură nu poate stabili care este. Atribuirea de mai sus face rolurile explicite: `health` este intrarea temporară, iar `_health` este stocarea obiectului. Ea ilustrează denumirea, nu validarea; folosirea lui `Health = health` ar dirija inițializarea prin setterul proprietății.

## Păstrează sintaxa de utilizare adăugând abstractizare

[00:22:53](https://www.youtube.com/watch?v=IESVXL16OBA&t=1373s) Lecția descurajează câmpurile publice în clasele al căror stare este încapsulată, recunoscând totuși că înlocuirea unui câmp public nelimitat cu o proprietate publică nelimitată și trivială nu ar aduce imediat nicio validare sau comportament nou. Ambele alegeri pot funcționa în acest stadiu introductiv.

La [00:23:07](https://www.youtube.com/watch?v=IESVXL16OBA&t=1387s), un câmp poate deveni mai târziu o proprietate cu logică adăugată, păstrând sintaxa obișnuită de citire și atribuire. Declarații succesive simplificate și apelant neschimbat:

```csharp
// Earlier member:
public int Attack;

// Alternative later member:
public int Attack { get; set; }

// Ordinary usage in either version:
enemy.Attack = 7;
Console.WriteLine(enemy.Attack);
```

Acestea sunt declarații alternative, nu doi membri care trebuie declarați împreună. Păstrarea aceluiași nume permite ca utilizarea obișnuită din sursa prezentată aici să rămână neschimbată; aceasta nu înseamnă că orice folosire a câmpurilor și a proprietăților este interschimbabilă.

La [00:23:27](https://www.youtube.com/watch?v=IESVXL16OBA&t=1407s) și [00:23:48](https://www.youtube.com/watch?v=IESVXL16OBA&t=1428s), avantajele concrete ale câmpurilor față de proprietăți sunt amânate explicit, deoarece este prea devreme pentru o asemenea comparație. La [00:24:06](https://www.youtube.com/watch?v=IESVXL16OBA&t=1446s), folosirea unei proprietăți adaugă o limită de abstractizare: citirea invoca un getter, în loc să expună direct stocarea. La [00:24:22](https://www.youtube.com/watch?v=IESVXL16OBA&t=1462s), sintaxa simplă de la locul apelului nu arată dacă membrul este un câmp sau o proprietate. Aceasta este sfera echivalenței demonstrate aici.

## Urmărește alocarea și argumentele constructorului

[00:24:35](https://www.youtube.com/watch?v=IESVXL16OBA&t=1475s) Terminați cu o mică parcurgere a unui constructor. O **referință** identifică un obiect; înregistrarea reprezintă obiectele în **memorie dinamică** și parametrii în **stocare temporară pe stivă**, pentru a explica ce valori supraviețuiesc apelului. Considerați acesta un model conceptual de stocare, nu o garanție a dispunerii fizice exacte a runtime-ului.

Reconstrucție simplificată a stării din videoclip:

```csharp
class Test
{
    private int data1;
    private int data2;

    public Test(int data1, int data2)
    {
        this.data1 = data1;
        this.data2 = data2;
    }
}

Test t = new Test(data1: 1, data2: 3);
```

Urmăriți construcția în ordine:

1. La [00:24:35](https://www.youtube.com/watch?v=IESVXL16OBA&t=1475s), declarația introduce variabila locală `t`. Înainte de a se încheia inițializatorul ei, nu are încă o referință utilizabilă.
2. La [00:24:59](https://www.youtube.com/watch?v=IESVXL16OBA&t=1499s), este alocat noul obiect. Câmpurile sale întregi declarate conțin inițial valorile implicite, zero.
3. La [00:25:08](https://www.youtube.com/watch?v=IESVXL16OBA&t=1508s), următoarea operație este inițializarea prin constructor.
4. La [00:25:09](https://www.youtube.com/watch?v=IESVXL16OBA&t=1509s), țineți cont de trei intrări conceptuale: receptorul ascuns `this`, plus cele două argumente explicite `data`. Sunt **doi parametri declarați**, nu trei parametri expliciți.
5. La [00:25:18](https://www.youtube.com/watch?v=IESVXL16OBA&t=1518s), reprezentați receptorul și fiecare parametru ca celule de stocare temporară. Un parametru este o variabilă de intrare temporară; un câmp este stocare care aparține obiectului.
6. La [00:25:29](https://www.youtube.com/watch?v=IESVXL16OBA&t=1529s), distingeți numele care par identice. În `this.data1 = data1;`, `this.data1` selectează câmpul obiectului, iar `data1` necalificat selectează parametrul, deoarece acesta îl umbrește pe câmp.
7. La [00:25:48](https://www.youtube.com/watch?v=IESVXL16OBA&t=1548s), dați lui `this` referința noului obiect. Receptorul este implicit; apelanții nu scriu un argument explicit suplimentar pentru el.
8. La [00:26:24](https://www.youtube.com/watch?v=IESVXL16OBA&t=1584s), valorile furnizate de expresia `new` devin parametrii expliciți ai constructorului, pregătiți pentru prima atribuire.

Corecția privind rezolvarea numelor contează: un `data1` necalificat **nu** selectează câmpul când un parametru cu același nume este în domeniu. Scrierea `data1 = data1;` ar atribui parametrul lui însuși și ar lăsa câmpul neschimbat.

## Finalizează construirea și păstrează referința obiectului

[00:26:33](https://www.youtube.com/watch?v=IESVXL16OBA&t=1593s) În ilustrarea cu adrese din înregistrare, `this` conține o referință de obiect etichetată 8000. Urmăriți același obiect prin atribuiri:

1. La [00:26:33](https://www.youtube.com/watch?v=IESVXL16OBA&t=1593s), citiți receptorul deținut în `this`.
2. La [00:26:34](https://www.youtube.com/watch?v=IESVXL16OBA&t=1594s), folosiți-l pentru a localiza câmpul `data1` al obiectului și pentru a copia valoarea primului parametru în acel câmp.
3. La [00:26:34](https://www.youtube.com/watch?v=IESVXL16OBA&t=1594s), folosiți același receptor pentru a localiza `data2` și pentru a copia acolo valoarea celui de-al doilea parametru.
4. La [00:26:37](https://www.youtube.com/watch?v=IESVXL16OBA&t=1597s), după ce inițializarea se încheie, expresia `new` produce referința noului obiect.
5. La [00:26:42](https://www.youtube.com/watch?v=IESVXL16OBA&t=1602s), stocați acea referință în `t`.
6. La [00:26:45](https://www.youtube.com/watch?v=IESVXL16OBA&t=1605s), stocarea temporară a parametrilor nu mai este necesară; valorile câmpurilor inițializate rămân în obiect.

Rezultatul simplificat al stării constructorului precedente:

```csharp
// Constructor body:
this.data1 = data1; // Field gets 1.
this.data2 = data2; // Field gets 3.

// When new Test(1, 3) finishes:
// t holds the reference to the initialized Test object.
```

La [00:26:38](https://www.youtube.com/watch?v=IESVXL16OBA&t=1598s), clarificați observația din înregistrare despre încercarea de a întoarce ceva: un constructor C# nu are tip de întoarcere și nu poate folosi `return expression;` pentru a alege rezultatul. Rezultatul unui `new` reușit este referința obiectului creat, nu o valoare de întoarcere a constructorului.

Eticheta de adresă rostită mai târziu, „88000”, nu stabilește o altă alocare. Relația importantă este că `this` în timpul inițializării și `t` ulterior identifică același obiect nou creat. Etichete numerice precum 8000 și 88000 sunt scurtături de diagramă, nu valori literale accesibile printr-o referință gestionată obișnuită.

„Datele temporare sunt șterse” descrie sfârșitul rolului lor în apel. Nu este o promisiune că octeții sunt suprascriși în siguranță sau că fiecare parametru ocupă o celulă fizică pe stivă. Obiectul păstrează câmpurile întregi copiate independent de acele intrări temporare.

<details>
<summary>De ce supraviețuiesc câmpurile după ce parametrii constructorului dispar?</summary>
Constructorul a copiat valorile de intrare în stocarea proprie a obiectului. Variabila locală păstrează o referință către acel obiect inițializat; câmpurile nu depind de existența în continuare a variabilelor parametru.
</details>

## Sfera lecției

Parcurgerea acoperă accesul privat, operațiile controlate, aserțiunile de depanare, constructorii și fabricile, argumentele cu nume, copierea unei stări întregi simple și accesorii proprietăților. Citirea din exterior este amânată la [00:05:49](https://www.youtube.com/watch?v=IESVXL16OBA&t=349s) și restaurată la [00:15:36](https://www.youtube.com/watch?v=IESVXL16OBA&t=936s). Avantajele detaliate ale câmpurilor publice față de proprietăți sunt amânate la [00:23:48](https://www.youtube.com/watch?v=IESVXL16OBA&t=1428s). Refactorizarea luptei este ilustrativă, nu o reproiectare completă a jocului și nici un tratament complet al validării la timpul de execuție.

## Traseul istoriei codului

Acestea sunt stări din înregistrare, în ordine cronologică; nu a fost disponibilă nicio sursă exactă de luptă din checkout-ul deținut.

- [00:00:05](https://www.youtube.com/watch?v=IESVXL16OBA&t=5s) — Mută înapoi în clasă operația extrasă.
- [00:00:23](https://www.youtube.com/watch?v=IESVXL16OBA&t=23s) — Parameterizează afișarea stării de sănătate și o plasează lângă câmp.
- [00:01:00](https://www.youtube.com/watch?v=IESVXL16OBA&t=60s) — Mută damage-ul într-o operație de instanță partajată; redenumește-o la [00:01:45](https://www.youtube.com/watch?v=IESVXL16OBA&t=105s).
- [00:03:06](https://www.youtube.com/watch?v=IESVXL16OBA&t=186s) — Asertează precondiția „jucător în viață”; execuția cu starea de sănătate zero eșuează la [00:04:11](https://www.youtube.com/watch?v=IESVXL16OBA&t=251s).
- [00:04:45](https://www.youtube.com/watch?v=IESVXL16OBA&t=285s) — Face starea de sănătate privată și dirijează interacțiunea din exterior prin metode.
- [00:08:22](https://www.youtube.com/watch?v=IESVXL16OBA&t=502s) — Inițializează starea privată printr-o fabrică internă; etichetează argumentele la [00:09:01](https://www.youtube.com/watch?v=IESVXL16OBA&t=541s).
- [00:09:49](https://www.youtube.com/watch?v=IESVXL16OBA&t=589s) — Declară constructorul cu parametri și înlocuiește crearea prin fabrică la [00:10:34](https://www.youtube.com/watch?v=IESVXL16OBA&t=634s).
- [00:13:13](https://www.youtube.com/watch?v=IESVXL16OBA&t=793s) — Înlocuiește crearea implicită cu o supraîncărcare de constructor fără parametri.
- [00:14:06](https://www.youtube.com/watch?v=IESVXL16OBA&t=846s) — Adaugă construirea prin copiere, apoi alternativele fabrică și metodă.
- [00:16:12](https://www.youtube.com/watch?v=IESVXL16OBA&t=972s) — Adaugă observarea prin getter; adaugă scrieri prin setter verificat la [00:16:56](https://www.youtube.com/watch?v=IESVXL16OBA&t=1016s).
- [00:19:04](https://www.youtube.com/watch?v=IESVXL16OBA&t=1144s) — Expune citirile publice ale proprietății cu setter privat; accesorii automati elimină verificările anterioare.
- [00:21:18](https://www.youtube.com/watch?v=IESVXL16OBA&t=1278s) — Extinde accesorii în jurul unei stocări explicite de susținere și restaurează validarea.
- [00:24:35](https://www.youtube.com/watch?v=IESVXL16OBA&t=1475s) — Urmărește alocarea, receptorul ascuns, atribuirile de câmpuri și referința păstrată după construire.