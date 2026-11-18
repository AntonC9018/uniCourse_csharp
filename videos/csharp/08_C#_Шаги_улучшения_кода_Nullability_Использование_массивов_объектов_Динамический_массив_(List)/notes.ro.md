> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Îmbunătățește un program de luptă pas cu pas

[00:00:00](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=0s) Această lecție transformă o demonstrație de luptă plină de instrucțiuni copiate într-un program condus de datele inamicilor. Urmează ordinea construirii: reproducă o rundă, corectează tratarea morții, grupează valorile înrudite, fă inițializarea explicită, extrage acțiunile repetate, prelucrează un tablou cu bucle și, în final, crește și filtrează o listă.

Iată termenii folosiți pe parcurs:

- **Tablou, element, indice și lungime:** un tablou conține un număr fix de celule de același tip; un element este valoarea unei celule, indicele identifică celula numerotând de la zero, iar lungimea este numărul de celule.
- **Entitate, înregistrare, stare și statistici:** o entitate este un participant la joc; o înregistrare înseamnă aici un grup de date care îl descrie, nu o cerință de a folosi cuvântul cheie `record` din C#. Starea este datele curente ale jocului; statisticile sunt valorile de viață și atac ale participantului. Exemplele își numesc tipurile `Stats`, `Enemy` și `GameState`.
- **Refactorizare și duplicare:** refactorizarea reorganizează codul păstrând comportamentul intenționat; duplicarea repetă aceeași logică în mai multe locuri.
- **Clasă, structură, câmp și `public`:** o clasă definește un tip de referință; o structură definește un tip de valoare. Un câmp stochează date care aparțin unei instanțe, iar `public` permite accesul din alt cod.
- **Obiect, referință, stivă și heap:** un obiect este o instanță creată; o referință identifică un obiect, fără să conțină câmpurile lui. Înregistarea schițează variabilele locale în memorie temporară (stivă) și obiectele în memorie dinamică (heap). Aceste schițe explică relațiile, nu o promisiune despre stocare fizică optimizată sau adrese numerice.
- **Alocare, `new` și dereferențiere:** alocarea rezervă memorie; `new` construiește o instanță; dereferențierea folosește o referință pentru a citi sau scrie membrii obiectului referit.
- **`null`, analiză nullable, adnotare și diagnostic:** `null` înseamnă că nu este referit niciun obiect. Analiza nullable verifică la compilare, adică înainte de execuție, prezența sau absența intenționată a referințelor; runtime-ul este momentul în care programul se execută; o adnotare precum `Stats?` permite absența. Un diagnostic raportează o problemă: o avertizare nu împiedică neapărat compilarea, în schimb o eroare o face.
- **Inițializare, inițializator de obiect și `required`:** inițializarea furnizează valori de pornire; un inițializator de obiect atribuie membrii în expresia de construcție; `required` obligă apelanții să furnizeze explicit un membru la construirea unei instanțe.
- **Inferență de tip, `new()` cu tip țintă și `var`:** inferența lasă compilatorul să determine tipul. În `Stats p = new();`, partea stângă determină tipul construit; în `var p = new Stats();`, partea dreaptă determină tipul variabilei locale.
- **Funcție sau metodă, parametru, argument, `void` și valoare de întoarcere:** o funcție grupează instrucțiuni; o metodă este o funcție asociată unui tip. Un parametru numește o intrare în declarație; un argument o furnizează la apel. `void` înseamnă că nu se întoarce niciun rezultat; o valoare de întoarcere duce rezultatul calculat către apelant.
- **Șir, literal și interpolare:** un șir este text de tip referință; un literal scrie direct o valoare, precum `6` sau `"Rat"`; interpolarea inserează valori în text folosind `$"...{value}..."`.
- **Buclă, iterație, `for`, `foreach`, `while`, `break`, `continue` și `return`:** o buclă repetă un corp, iar o parcurgere se numește iterație. `for` avansează printr-un start explicit, o condiție și o actualizare; `foreach` vizitează elementele unei colecții; `while` repetă cât timp o condiție este adevărată. `break` părăsește bucla, `continue` sare la iterația următoare, iar `return` părăsește funcția curentă.
- **Boolean, indicator, pază, condiție, negare și „sau”:** un boolean este `true` sau `false`; un indicator memorează o asemenea decizie; o pază testează dacă o operație ar trebui să continue; o condiție este expresia testată. Negarea logică (`!`) inversează un boolean; „sau” logic (`||`) acceptă oricare dintre două condiții.
- **Domeniu de vizibilitate și funcție fabrică:** domeniul de vizibilitate este regiunea în care un nume poate fi folosit; o funcție fabrică construiește și întoarce un obiect gata de utilizare.
- **Tablou dinamic, `List<T>`, argument generic și tablou de susținere:** un tablou dinamic crește prin memorie gestionată. `List<T>` este lista extensibilă din .NET; `T` specifică tipul elementului, adică argumentul generic. Tabloul de susținere este stocarea internă de dimensiune fixă, înlocuită atunci când este nevoie de mai mult loc; Count este numărul de intrări aflate în acel moment în listă.
- **Expresie de colecție, adăugare și filtrare:** o expresie de colecție precum `[a, b]` furnizează elemente pentru un tip de colecție țintă. Adăugarea alipește un element; filtrarea elimină elementele selectate de o condiție.
- **Lambda, predicat, delegat, suprasarcină și polimorfism:** o lambda precum `enemy => enemy.Stats.Health <= 0` furnizează o funcție mică; un predicat întoarce o decizie booleană. Un delegat reprezintă un comportament invocabil, o suprasarcină este o altă formă a unei operații invocabile, iar polimorfismul permite ca o interfață sau o operație să reprezinte comportamente diferite. Videoclipul folosește sintaxa de filtrare, dar amână aceste mecanisme mai profunde.
- **NullReferenceException și invariant:** `NullReferenceException` este eșecul din runtime la accesarea unui membru prin `null`; un invariant este o proprietate pe care programul trebuie să o păstreze adevărată, precum „un joc în desfășurare are un jucător”.

Exersează-te cu laboratorul real [program-improvement](../../../labs/1_basic/13_improvement_techniques.md), care aplică gruparea, tablourile, buclele și funcțiile unei biblioteci cu cinci cărți. Urmează apoi [laboratorul de nullability](../../../labs/1_basic/14_nullability.md). Exercițiul cu biblioteca este practică, nu codul de luptă din înregistrare.

Nu a fost disponibil niciun commit exact al sursei de luptă în dovezile verificate. Toate fragmentele de cod de mai jos sunt reconstrucții simplificate ale stărilor videoclipului legat, cu nume sau date scurtate unde ajută; nu sunt prezentate ca fișiere exacte din commituri.

## Tablouri și prima rundă de luptă

[00:00:00](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=0s) Un **tablou** este o stocare de lungime fixă pentru valori de un singur tip de element. În această reconstrucție a stării inițiale din videoclip, `int[]` este tipul, `health` este numele variabilei, `new` creează obiectul tablou, iar `3` îi alege lungimea. [Tranzcrierea imutabilă aferentă](https://github.com/AntonC9018/uniCourse_csharp/blob/4814706ae2fafbc797829239a33afc2eeefa29e8/videos/csharp/08_C%23_Шаги_улучшения_кода_Nullability_Использование_массивов_объектов_Динамический_массив_%28List%29/internal/transcript.md) înregistrează această explicație inițială; exemplul de mai jos reconstruiește starea din videoclip, nu un program din commit:

```csharp
int[] health = new int[3];
health[0] = 10; // index 0 identifies the first element
```

Obiectul tablou se află în memorie dinamică în modelul înregistrării. Lungimea lui nu se schimbă ulterior. [00:00:13](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=13s) Tablourile pot avea alte tipuri de elemente decât `int`; contează atunci când valoarea stocată devine în cele din urmă o înregistare completă despre un inamic.

[00:00:19](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=19s) Începe cu un jucător și câțiva inamici. Fiecare participant are nevoie de perechea sa proprie de viață și atac. Variabilele separate funcționează la început, dar adăugarea inamicilor înmulțește atât declarațiile de date, cât și instrucțiunile care le folosesc.

[00:00:42](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=42s) Acțiunea inițială de luptă scade atacul unui inamic din viața jucătorului. [00:01:01](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=61s) Adaugă replica: și inamicul pierde valoarea de atac a jucătorului. Iată o reconstrucție scurtată a blocurilor repetate, folosind atacuri egale ilustrative:

```csharp
int playerHealth = 10;
int playerAttack = 5;
int[] enemyHealth = [10, 10, 10];
int[] enemyAttack = [1, 1, 1];

playerHealth -= enemyAttack[0];
enemyHealth[0] -= playerAttack;
playerHealth -= enemyAttack[1];
enemyHealth[1] -= playerAttack;
playerHealth -= enemyAttack[2];
enemyHealth[2] -= playerAttack;

Console.WriteLine(playerHealth);
Console.WriteLine(enemyHealth[0]);
Console.WriteLine(enemyHealth[1]);
Console.WriteLine(enemyHealth[2]);
```

Operatorul `-=` scade valoarea din dreapta și memorează rezultatul înapoi în stânga. `Console.WriteLine` afișează o valoare pentru inspecție. [00:01:13](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=73s) Ambele părți pierd viață, pentru că fiecare tur al inamicului include replica jucătorului. [00:01:23](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=83s) Execuția înregistrată lasă jucătorul cu **7 viață** și fiecare inamic cu **5**. Aceasta verifică blocurile copiate pentru cazul inițial, dar nu verifică tratarea morții.

## Oprește atacurile ulterioare când jucătorul moare

[00:01:39](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=99s) Dacă primul inamic îl omoară pe jucător, inamicii ulteriori nu trebuie să acționeze: blocurile lor ar avaria și inamicii, prin replica unui jucător deja mort. Verificarea aparține între schimburi complete de inamic, astfel încât schimbul următor să fie sărit.

[00:01:55](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=115s) Daunele pot duce viața sub zero. Demonstrația folosește viața jucătorului **6** și ajunge la **-2** după al doilea inamic. Moartea înseamnă `health <= 0`, nu doar `health == 0`; a fi în viață înseamnă `health > 0`.

[00:02:15](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=135s) Adaugă paza de „este în viață” după un schimb. [00:02:27](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=147s) Repet-o înaintea blocului fiecărui inamic ulterior. Această reconstrucție a stării din videoclip ilustrează două atacuri de câte patru puncte de daune:

```csharp
int playerHealth = 6;
int playerAttack = 5;
int enemy1Health = 10, enemy2Health = 10, enemy3Health = 10;

playerHealth -= 4;
enemy1Health -= playerAttack;

if (playerHealth > 0)
{
    playerHealth -= 4;
    enemy2Health -= playerAttack;
}
if (playerHealth > 0)
{
    playerHealth -= 4;
    enemy3Health -= playerAttack;
}
if (playerHealth <= 0)
    Console.WriteLine("Player lost");
```

[00:02:35](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=155s) Testul înregistrat confirmă că primul inamic acționează, al doilea îl omoară pe jucător, iar al treilea nu acționează; viața jucătorului este **-2**. Corecția păstrează schimbul stabilit pe ambele părți, împiedicând în schimb un nou schimb după moarte.

[00:02:47](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=167s) Un al patrulea inamic ar necesita încă o pereche viață/atac, încă un bloc copiat, indici sau nume de variabile revizuite peste tot și încă o instrucțiune de afișare. [00:03:07](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=187s) Rezultatul dorit este altul: înregistrarea datelor unui inamic nou ar trebui să fie suficientă. Această duplicare crescătoare motivează **refactorizarea**, o reorganizare care păstrează regula de luptă intenționată.

<details>
<summary>De ce nu funcționează o verificare a morții doar pentru zero?</summary>

Un atac poate duce viața direct de la o valoare pozitivă la una negativă. Un jucător la -2 este mort, deși viața sa nu este exact zero. Folosește condiția de moarte inclusivă și oprește tururile ulterioare ale inamicilor.

</details>

## Grupează viața și atacul în Stats

[00:03:24](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=204s) Când un program pare încurcat, întâi grupează datele care aparțin unei **entități**, unui participant, într-o singură valoare. Un **câmp** stochează o bucată din datele acelei entități. O clasă sau o structură poate grupa câmpuri; înregistrarea alege aici statistici bazate pe clasă.

[00:03:41](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=221s) Mută viața și atacul în câmpuri publice. **Public** face câmpurile accesibile din codul de luptă înconjurător. O reconstrucție simplificată a acestei stări din videoclip este:

```csharp
class Stats
{
    public int Health;
    public int Attack;
}

Stats player = new Stats { Health = 6, Attack = 5 };
Stats enemy1 = new Stats { Health = 10, Attack = 4 };

player.Health -= enemy1.Attack;
enemy1.Health -= player.Attack;
```

[00:03:49](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=229s) Mutarea declarațiilor strică codul care mai așteaptă variabile libere. Repară-l în ordine:

1. Declară o variabilă de noul tip.
2. Creează-i instanța și păstrează vechile valori numerice.
3. Înlocuiește fiecare acces anterior la viață/atac cu un acces prin posesorul său.

[00:04:05](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=245s) Punctul din `player.Health` face vizibilă originea datelor: această viață aparține jucătorului. [00:04:16](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=256s) Jucătorul și inamicii au aceeași formă viață/atac, deci o singură clasă comună `Stats` le poate descrie pe toate; tipuri separate pentru statisticile jucătorului și ale inamicilor nu adaugă încă nicio distincție utilă.

[00:04:34](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=274s) Înlocuiește valorile libere ale jucătorului și valorile libere ale fiecărui inamic cu instanțe ale acelei clase comune. Versiunea inițială plasează aceste câmpuri și inițializarea lor pe tipul jocului. [00:04:59](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=299s) Inițializarea poate avea loc în schimb în funcția principală, unde începe execuția. [00:05:02](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=302s) Crearea jocului întâi și atribuirea jucătorului în apropiere este o organizare echivalentă a datelor intenționate, cu condiția ca nimic să nu citească câmpul prea devreme.

[00:05:20](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=320s) **new cu tip țintă** înseamnă că destinația furnizează tipul construit. Aceste forme scurtate descriu aceeași construcție în această stare din videoclip:

```csharp
Stats player = new Stats() { Health = 6, Attack = 5 };
// Alternative declaration:
Stats otherPlayer = new() { Health = 6, Attack = 5 };
```

A doua linie conține în continuare `new` și paranteze; doar omite `Stats` după `new`. Scrierea unui nume de tip fără o expresie de creare a obiectului nu creează un obiect.

## Urmărește referințele și câmpurile implicite

[00:05:39](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=339s) O **referință** identifică un obiect; nu încorporează câmpurile obiectului. Urmărește prima construcție înainte de a repara inițializarea. Acest fragment scurtat al stării din videoclip lasă deliberat câmpurile imbricate neinițializate:

```csharp
class GameState
{
    public Stats Player;
    public Stats Enemy1;
}

GameState state = new GameState();
```

[00:05:54](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=354s) `GameState` este o **clasă**, un tip de referință, nu o **structură**, care ar fi un tip de valoare. Înregistrarea numește localul `state` o variabilă în memorie temporară/de stivă. Tratează aceasta ca pe o schiță a memoriei sale: faptul important este că variabila conține o referință la obiectul nou alocat.

Urmează construcția:

1. [00:06:01](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=361s) Localul deține o referință, nu toate câmpurile jocului.
2. [00:06:13](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=373s) `new GameState()` alocă obiectul jocului în memorie dinamică/de heap și produce referința sa.
3. [00:06:18](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=378s) Obiectul are stocare pentru fiecare câmp declarat, conform tipului acelui câmp.
4. [00:06:30](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=390s) Fiecare câmp `Stats` este el însuși de tip referință.
5. [00:06:36](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=396s) Așadar `Player` și `Enemy1` dețin referințe către obiecte de statistici distincte, nu date viață/atac scrise inline.
6. [00:06:47](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=407s) Încă nu a fost creată nicio instanță de statistici. Aceste câmpuri-referință conțin așadar valoarea lor implicită, `null`.
7. [00:06:58](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=418s) Înregistrarea desenează această absență ca adresa zero. Aceasta este starea **null**: nu există niciun obiect referit de urmat.

Crearea obiectului conținător nu creează recursiv o instanță pentru fiecare câmp de tip clasă. O variabilă locală care așteaptă atribuirea este o problemă separată: C# nu permite citirea unei variabile locale neatribuite doar pentru că obiectele primesc valori implicite.

## Activează analiza nullable și exprimă intenția

[00:07:05](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=425s) Contractul intenționat este că un câmp `Stats` obișnuit conține un obiect real, pe când `Stats?` permite absența unui obiect. **Analiza nullable** verifică această intenție; nu modifică reprezentarea din runtime și nu construiește automat o instanță.

[00:07:19](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=439s) Înainte de activarea verificării nullable, compilatorul poate permite referințe fie cu un obiect, fie cu `null`, fără diagnosticele intenționate. [00:07:28](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=448s) `null` este scrierea din C# pentru referința absentă desenată ca zero; atribuirea unui întreg `0` unei referințe nu este sintaxa C# corespunzătoare.

[00:07:34](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=454s) Activează caracteristica nullable, apoi distinge tipurile care permit absența de cele care nu ar trebui să o permită. [00:07:39](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=459s) Un tip de clasă fără adnotare exprimă o intenție de non-null în contextul activat. [00:07:47](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=467s) Câmpul neinițializat este locul în care această intenție și inițializarea nu se potrivesc.

Pentru această setare, [laboratorul de nullability asociat, aflat în commit](https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/14_nullability.md) furnizează următorul fragment de proiect simplificat. Este practică de sprijin, nu o stare exactă a proiectului de luptă:

```xml
<PropertyGroup>
  <Nullable>enable</Nullable>
  <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
</PropertyGroup>
```

[00:08:00](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=480s) Cu verificarea nullable dezactivată, câmpurile de referință obișnuite pot ține oricare dintre stări fără aceleași avertismente. [00:08:16](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=496s) Activarea verificării raportează că câmpul non-nullable nu a fost inițializat la terminarea construcției.

[00:08:28](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=508s) Câmpul ar trebui să refere ceva, dar în prezent nu referă. [00:08:41](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=521s) Adăugarea unui semn de întrebare declară absența acceptabilă. Modificarea stării din videoclip este:

```csharp
class GameState
{
    public Stats? Player;
}
```

[00:08:56](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=536s) `Stats?` este o **adnotare nullable**: câmpul poate fi `null` sau poate referi un obiect `Stats`. [00:08:58](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=538s) Valoarea sa implicită null corespunde acum intenției declarate, deci avertismentul de câmp non-nullable neinițializat dispare.

Păstrează deosebite două modificări: activarea analizei expune problema; adăugarea lui `?` schimbă contractul declarat. Diagnosticele nullable sunt în mod normal avertismente. Setarea laboratorului de a transforma avertismentele în erori le face eșecuri de compilare. Nici numele simplu de tip, nici `?` nu modifică stocarea din runtime și nu oferă o garanție de non-null la runtime. Această distincție urmează [explicația Microsoft despre nullable references](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/null-safety/nullable-reference-types).

## Un jucător trebuie să existe înainte de a fi folosit

[00:09:15](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=555s) A permite absența jucătorului este modelul greșit pentru acest joc. **Invariantul** de joc în desfășurare, o proprietate care ar trebui să fie mereu adevărată, este „jucătorul există”. Eliminarea unui avertisment prin slăbirea acelei promisiuni nu îl respectă.

[00:09:24](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=564s) O declarație non-nullable poate ajunge totuși să conțină `null` la runtime dacă inițializarea lipsește sau diagnosticul este ignorat. [00:09:34](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=574s) Crearea jocului lasă câmpurile sale de referință neinițializate null chiar și fără semn de întrebare. Reconstrucția următoare a stării din videoclip ilustrează folosirea care eșuează:

```csharp
GameState state = new GameState();
state.Player.Health = 10; // fails if Player was never created
```

Exemplul cu câmp absent și atribuirea explicită a lui `null` încalcă amândouă contractul intenționat. Atribuirea explicită a lui null poate produce o avertizare nullable când verificarea este activată; „nu este o eroare” nu înseamnă „este sigur”.

[00:10:06](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=606s) **Dereferențierea** înseamnă urmărirea unei referințe pentru a accesa un membru. Eșecul apare la acel acces, nu doar la momentul memorării referinței null. [00:10:11](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=611s) Un câmp de referință neinițializat conține null; [00:10:19](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=619s) cererea valorii `Health` de la el aruncă o **NullReferenceException**, pentru că nu există niciun obiect de statistici.

[00:10:27](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=627s) Dacă `Player` este onest adnotat ca nullable, accesul direct primește un diagnostic de posibil null. [00:10:44](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=644s) O pază poate stabili că o referință este prezentă înainte de a o folosi. [00:10:59](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=659s) Testează cu cuvântul cheie `null`, nu cu zero numeric. Această alternativă scurtată a stării din videoclip permite absența:

```csharp
if (state.Player != null)
{
    state.Player.Health = 10;
}
```

[00:11:11](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=671s) Dar jucătorul jocului ar trebui să existe întotdeauna, deci o ramură de tip „nu face nimic când lipsește” nu este corecția finală. Fă ca crearea să garanteze prezența. [00:11:23](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=683s) Promisiunea de a atribui jucătorul imediat după lasă totuși un gol. [00:11:30](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=690s) O citire inserată înaintea acelei atribuiri provoacă o eroare.

[00:11:40](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=700s) Înregistrarea numește `required` drept soluția viitoare, dar o amână intenționat cât timp urmărește inițializarea.

<details>
<summary>Schimbarea înapoi a lui Stats? în Stats creează un jucător?</summary>

Nu. Adnotarea descrie utilizarea intenționată. Programul are în continuare nevoie de o expresie de creare a obiectului și de o atribuire. Altfel, un câmp poate rămâne null și poate eșua la accesare.

</details>

## Inițializarea secvențială poate citi prea devreme

[00:11:48](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=708s) **Inițializarea** furnizează valori de pornire. În prima abordare, obiectul de joc este creat, iar fiecare referință imbricată este atribuită într-o instrucțiune ulterioară. Această stare simplificată din videoclip este:

```csharp
GameState state = new GameState();
state.Player = new Stats { Attack = 5, Health = 10 };
state.Enemy1 = new Stats { Attack = 4, Health = 10 };
state.Enemy2 = new Stats { Attack = 4, Health = 10 };
state.Enemy3 = new Stats { Attack = 1, Health = 10 };
```

[00:12:04](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=724s) Accesul la `state.Player` urmărește referința jocului până la câmpul numit `Player`. [00:12:14](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=734s) Expresia de creare din dreapta trebuie să producă valoarea atribuită acelui câmp.

Urmărește atribuirea jucătorului:

1. [00:12:23](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=743s) Creează o instanță **Stats** în memorie dinamică. Obiectul conținător este `GameState`; partea dreaptă a atribuirii jucătorului construiește `Stats`.
2. [00:12:36](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=756s) Rezervă cele două câmpuri întregi ale obiectului de statistici, `Attack` și `Health`.
3. [00:12:38](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=758s) Scrie atacul în câmpul său, apoi scrie viața în câmpul său.
4. [00:12:40](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=760s) Rezultatul expresiei completate este referința noului obiect; memoreaz-o în câmpul destinație, cel al jucătorului.
5. [00:12:52](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=772s) Repetă secvența de creare și atribuire pentru fiecare inamic.

[00:13:10](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=790s) În timpul acestei secvențe, câmpurile ulterioare pot fi în continuare null. [00:13:23](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=803s) Citirea dintr-un obiect anterior, deja creat, poate funcționa; citirea dintr-un obiect atribuit ulterior nu poate. Iată modelul scurtat, care eșuează, al stării din videoclip:

```csharp
GameState state = new GameState();
state.Player = new Stats { Attack = 5, Health = 10 };
state.Enemy1 = new Stats
{
    Attack = 4,
    Health = state.Enemy2.Health // Enemy2 has not been created
};
state.Enemy2 = new Stats { Attack = 4, Health = 10 };
```

[00:13:40](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=820s) Instrucțiunile se execută secvențial. `Enemy2` nu poate furniza viață înainte de rularea instrucțiunii sale de creare. [00:13:47](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=827s) Execuția înregistrată eșuează la acea citire prematură. [00:13:57](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=837s) O expresie de creare întreagă face parte dintr-o singură instrucțiune, deci linia raportată poate conține mai multe acțiuni imbricate; accesul invalid se află în interiorul ei.

## Construiește starea cu un singur inițializator de obiect

[00:14:13](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=853s) Evită instrucțiunile de inițializare care depind de o stare parțial completată. Un **inițializator de obiect** furnizează atribuiri de membri în interiorul expresiei de creare.

[00:14:22](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=862s) Înlocuiește atribuirile separate ale câmpurilor cu o singură instrucțiune de construcție. În această reconstrucție scurtată a stării din videoclip:

```csharp
GameState state = new GameState
{
    Player = new Stats { Attack = 5, Health = 10 },
    Enemy1 = new Stats { Attack = 4, Health = 10 },
    Enemy2 = new Stats { Attack = 4, Health = 10 },
    Enemy3 = new Stats { Attack = 1, Health = 10 }
};
```

[00:14:39](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=879s) Localul `state` nu este încă atribuit cât timp inițializatorul său este evaluat. Încercarea de a-l citi în propria sa inițializare este o eroare de compilare. Nici simpla numire a lui `Enemy2` nu transformă atribuirea de câmp din inițializator într-o variabilă locală.

[00:14:59](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=899s) Odată ce acea instrucțiune se încheie, folosește starea rezultată. „Inițializează împreună” înseamnă o expresie de construcție cu localul atribuit după; crearea imbricată se rulează în continuare în ordine, nu simultan.

[00:15:10](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=910s) Rămâne un pericol: un câmp obișnuit poate fi omis. [00:15:20](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=920s) În configurația demonstrată, omiterea celui de-al treilea inamic nu produce o eroare la locul construcției. [00:15:23](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=923s) El rămâne null.

Reconstrucția care eșuează a stării din videoclip este:

```csharp
GameState state = new GameState
{
    Player = new Stats { Health = 10, Attack = 5 },
    Enemy1 = new Stats { Health = 10, Attack = 4 },
    Enemy2 = new Stats { Health = 10, Attack = 4 }
    // Enemy3 omitted
};
state.Enemy3.Attack++;
```

[00:15:32](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=932s) Contractul declarat al unui câmp non-nullable poate face ca accesele ulterioare să pară acceptabile, fără a verifica dacă acest inițializator anume l-a furnizat. O declarație de clasă poate primi totuși o avertizare de câmp neinițializat; verificarea lipsă la locul construcției din înregistrare nu este o regulă conform căreia compilatorul nu avertizează niciodată despre referințe neinițializate.

[00:15:49](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=949s) Incrementarea lui `Enemy3.Attack` se compilează în configurația demonstrată, dar eșuează la runtime. [00:15:52](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=952s) Rezultatul înregistrat este o **NullReferenceException** netratată, raportată pe **linia 27**. Intenția non-null a declarației nu a creat instanța lipsă.

## Cere valori explicite sau alege valori implicite

[00:16:03](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=963s) Un membru **required** trebuie furnizat la construirea unui obiect. [00:16:16](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=976s) Adăugarea acestui modificator celui de-al treilea inamic omis transformă omiterea într-o eroare de compilare. Această declarație simplificată a stării din videoclip aplică regula și celorlalte câmpuri esențiale:

```csharp
class GameState
{
    public required Stats Player;
    public required Stats Enemy1;
    public required Stats Enemy2;
    public required Stats Enemy3;
}
```

Acum un inițializator care lasă afară `Enemy3` trebuie reparat înainte de rularea programului. Modificatorul cere o atribuire explicită; nu alocă magic obiectul membrului.

[00:16:28](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=988s) Același raționament se aplică numerelor. Un câmp întreg de viață neinițializat devine implicit zero, ceea ce poate însemna că o entitate pornește moartă. [00:16:51](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1011s) Declararea vieții ca required impune o alegere din exterior.

[00:16:57](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1017s) Alternativ, alege un **inițializator de câmp** rezonabil, o atribuire de pornire în declarația câmpului. Acestea sunt variante de proiect scurtate din videoclip, nu două definiții de inclus împreună:

```csharp
class ExplicitStats
{
    public required int Health; // caller must choose, even if it chooses 0
    public int Attack;
}

class DefaultStats
{
    public int Health = 10; // omitted health begins at 10
    public int Attack;
}
```

[00:17:06](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1026s) Fără o suprascriere explicită la construcție, inițializatorul de câmp furnizează 10. Alege `required` când o decizie din exterior este obligatorie și o valoare implicită când omiterea are o interpretare plină de sens.

Precizare: required înseamnă „furnizat”, nu „valid” sau „non-null la runtime”. Furnizarea lui null unei referințe required non-nullable satisface în continuare verificarea de prezență a atribuirii, dar produce o avertizare nullable. [Referința Microsoft pentru membrii required](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required) distinge acea avertizare de eroarea pentru omisiunea completă a membrului.

[00:17:08](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1028s) Lecția pune acum în aside alte întrebări despre inițializare și revine la codul de luptă duplicat rămas.

## Extrage acțiunile repetate în funcții

[00:17:14](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1034s) După gruparea datelor, fiecare acces trebuie să-și numească posesorul: viața jucătorului vine prin obiectul jucătorului, atacul inamicului prin obiectul inamicului. Corectează această consistență acolo unde erau folosite variabilele libere.

[00:17:25](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1045s) Următorul semnal de refactorizare este un bloc repetat cu doar câteva intrări care se schimbă. [00:17:42](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1062s) Obiectul de statistici inspectat și eticheta afișată diferă; acțiunea în sine este comună. O **funcție** împachetează acea acțiune, iar **parametrii** săi numesc ceea ce se schimbă.

[00:17:55](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1075s) Mută o acțiune repetată într-o funcție. [00:17:59](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1079s) Alege **void**, adică funcția execută instrucțiuni fără să întoarcă un rezultat calculat. [00:18:16](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1096s) Modificarea vieții și afișarea rezultatului sunt chiar scopul, deci o valoare de întoarcere nu este necesară.

Extragerea simplificată a stării din videoclip este:

```csharp
void Attack(Stats player, Stats enemy, string enemyName)
{
    player.Health -= enemy.Attack;
    enemy.Health -= player.Attack;
    Console.WriteLine($"{enemyName} attacks; player health: {player.Health}");
}
```

Aici un **șir** este text, iar **interpolarea** încorporează expresii în text: `$` activează înlocuirile `{enemyName}` și `{player.Health}`. Tipul se află înaintea numelui fiecărui parametru.

[00:18:20](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1100s) Enunță valorile de care depinde corpul. [00:18:22](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1102s) Furnizează statisticile jucătorului, statisticile inamicului și numele inamicului. [00:18:29](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1109s) Înlocuiește referințele hardcodate la obiect și la etichetă din funcție cu numele parametrilor respectivi. Corpul său funcționează acum pentru orice intrări potrivite.

[00:18:39](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1119s) Un apel furnizează **argumente**, valorile concrete pentru acei parametri, și înlocuiește blocul copiat. [00:18:51](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1131s) Păstrează fiecare apel în ramura în care jucătorul este în viață. Această stare intermediară scurtată din videoclip numește în continuare inamicii individual:

```csharp
if (state.Player.Health > 0)
    Attack(state.Player, state.Enemy1, "Enemy 1");

if (state.Player.Health > 0)
    Attack(state.Player, state.Enemy2, "Enemy 2");

if (state.Player.Health > 0)
    Attack(state.Player, state.Enemy3, "Enemy 3");
```

Deoarece `Stats` este un tip de referință, argumentele identifică aceleași obiecte de statistici folosite de apelant; modificarea câmpurilor lor schimbă viața acelor obiecte.

[00:18:56](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1136s) Aplică aceeași regulă de extragere afișării repetate a vieții și a numelui. [00:19:21](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1161s) Pune numele afișat care se schimbă într-un argument șir, în loc să-l hardcodezi în comportamentul comun. [00:19:25](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1165s) Păstrarea etichetei la locul apelului lasă o singură implementare a afișării.

O reconstrucție scurtată a acestei a doua extrageri este:

```csharp
void PrintHealth(Stats stats, string name)
{
    Console.WriteLine($"{name}: {stats.Health}");
}

string enemyName = "Enemy 1";
Attack(state.Player, state.Enemy1, enemyName);
PrintHealth(state.Enemy1, enemyName);
```

[00:19:35](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1175s) Procedura pentru fiecare inamic are acum acțiuni comune de atac și de raportare, executate pe rând pentru fiecare inamic. [00:19:40](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1180s) Adăugarea unui inamic necesită în continuare crearea datelor sale și adăugarea apelurilor, dar nu mai necesită copierea implementării de atac și afișare. Bucla pe tablou care urmează va elimina acele locuri de apel rămase repetate.

[00:19:58](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1198s) Execuția demonstrată se oprește după ce doi inamici acționează și jucătorul moare. [00:20:04](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1204s) Ridicarea vieții jucătorului îi permite să ajungă la inamicii rămași și să îi rănească pe toți, arătând că oprirea anterioară ținea de tratarea morții, nu de un apel lipsă.

## Fă un singur tip Enemy și un singur tablou

[00:20:11](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1211s) Lista rămasă de apeluri repetate este o problemă de date. Extinderea mulțimii de inamici ar trebui să însemne extinderea unei colecții, apoi lăsarea unei bucle să aplice acțiunea comună. [00:20:56](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1256s) Aplicarea acelorași daune ale jucătorului mai multor inamici este un exemplu de operație care ar trebui exprimată o singură dată.

[00:21:00](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1260s) Găsește corpul buclei numind pașii săi: efectuează schimbul pentru inamicul curent, apoi testează dacă jucătorul poate continua. [00:21:13](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1273s) Fiecare pas are nevoie atât de statisticile inamicului curent, cât și de numele său, deci adună aceste valori împreună în loc să menții secvențe deconectate.

[00:21:38](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1298s) O **înregistrare de inamic** înseamnă datele unui singur inamic; nu trebuie să folosească cuvântul cheie `record` din C#. Tipul poate câștiga ulterior mai multe câmpuri per inamic, pe măsură ce programul are nevoie de ele. [00:21:52](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1312s) Statisticile și numele aparțin împreună, pentru că descriu același participant și sunt folosite împreună.

[00:22:00](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1320s) Creează o clasă **Enemy** care ține acele două câmpuri. Reconstrucția apropiată a stării din videoclip folosește câmpuri required pentru a păstra crearea explicită:

```csharp
class Enemy
{
    public required Stats Stats;
    public required string Name;
}
```

În `public Stats Stats;`, primul `Stats` este tipul, iar al doilea este numele câmpului. Faptul că un câmp are aceeași scriere ca un tip este permis; pozițiile lor sintactice le deosebesc.

[00:22:13](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1333s) Adună înregistrările într-un singur tablou. [00:22:17](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1337s) `Enemy[] enemies` este un tablou ale cărui elemente se referă la instanțe de `Enemy`, fiecare cu statisticile și numele său.

[00:22:31](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1351s) Alocă **cinci** celule. [00:22:46](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1366s) Umple fiecare celulă cu un inamic nou inițializat; demonstrația include un Goblin ca ultimă intrare. O reconstrucție scurtată a stării din videoclip, cu etichete ilustrative pentru intrările anterioare, este:

```csharp
Enemy[] enemies = new Enemy[5]
{
    new Enemy { Stats = new Stats { Attack = 6, Health = 10 }, Name = "Enemy 1" },
    new Enemy { Stats = new Stats { Attack = 4, Health = 10 }, Name = "Enemy 2" },
    new Enemy { Stats = new Stats { Attack = 1, Health = 10 }, Name = "Enemy 3" },
    new Enemy { Stats = new Stats { Attack = 1, Health = 10 }, Name = "Enemy 4" },
    new Enemy { Stats = new Stats { Attack = 1, Health = 10 }, Name = "Goblin" }
};
```

Înlocuiește câmpurile de inamic numite individual cu un singur câmp de colecție. Această continuare simplificată a aceleiași stări din videoclip memorează tabloul de inamici grupat:

```csharp
class GameState
{
    public required Stats Player;
    public required Enemy[] Enemies;
}
```

Alocarea doar a lui `new Enemy[5]` nu creează cinci inamici: cele cinci celule sale cu valoare de referință pornesc null. Umplerea fiecărei celule este esențială înainte de dereferențiere.

[00:22:52](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1372s) Înregistrarea întrerupe refactorizarea aici ca să explice cum funcționează de fapt un tablou de instanțe de clasă. Acea explicație de memorie precedă folosirea tabloului finit în luptă.

## Urmărește un tablou de obiecte imbricate

[00:22:56](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1376s) Introdu un local temporar care să separă construcția tabloului de construcția stării jocului. O **variabilă temporară** aici este stocare locală obișnuită, folosită doar cât timp se construiește rezultatul; ea va ține referința tabloului, nu elementele tabloului scrise inline.

Această reconstrucție scurtată cu un singur element urmărește primul element al stării cu cinci elemente din videoclip:

```csharp
Enemy[] enemies = new Enemy[1]
{
    new Enemy
    {
        Stats = new Stats { Attack = 6, Health = 10 },
        Name = "Enemy 1"
    }
};
```

Citește-o dinspre construcția imbricată spre exterior:

1. [00:23:29](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1409s) Localul are un nume și un tip de tablou. Înainte de a se încheia inițializatorul, nu este un local atribuit pe care codul să-l poată citi.
2. [00:23:39](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1419s) Evaluarea expresiei din dreapta creează obiectul tablou. Videoclipul folosește cinci celule; exemplul abreviat folosește una.
3. [00:23:45](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1425s) Tabloul are și lungimea sa, care memorează câte celule există, ca iterația ulterioară să poată acoperi întregul tablou.
4. [00:24:05](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1445s) Evaluează un inițializator de element și pune referința rezultată în celula corespunzătoare.
5. [00:24:12](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1452s) `new Enemy` creează un obiect de clasă în memorie dinamică. Celula tabloului va conține referința lui, nu datele complete ale câmpurilor sale.
6. [00:24:23](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1463s) Obiectul inamic are două câmpuri: statistici și nume. Inițializează-le în ordinea expresiei.
7. [00:24:37](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1477s) Câmpul de statistici este și el de tip clasă, deci are nevoie de o referință către un obiect de statistici construit separat.
8. [00:25:01](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1501s) Expresia imbricată creează **Stats**, tipul cerut de acel câmp. [00:25:12](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1512s) Cu `new()` cu tip țintă, acel tip vine din tipul declarat al câmpului destinație. Formularea „Stat” din transcriere se referă la același tip de statistici.
9. [00:25:24](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1524s) Scrie **literalurile** întregi, valori scrise direct în sursă: atacul **6** și viața **10** ajung în câmpurile obiectului de statistici ca numere.
10. [00:25:41](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1541s) Construcția statisticilor produce o referință, care este memorată în câmpul de statistici al inamicului.
11. [00:25:51](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1551s) Literalul șir furnizează o referință către text, iar câmpul de nume al inamicului memorează acea referință. Un **șir** este un tip de referință; câmpul său nu conține toate caracterele scrise inline. Aceasta nu presupune o alocare nouă la fiecare întâlnire a unui literal.
12. [00:26:15](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1575s) Expresia de inamic completată produce referința inamicului, care devine primul element al tabloului. Adresa desenată **88000** este o ilustratie, nu o valoare pe care codul sursă să o controleze.
13. [00:26:21](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1581s) Repetă structura pentru celelalte intrări. Fiecare celulă a tabloului conține referința de la începutul grafului de obiecte al unui inamic.
14. [00:26:53](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1613s) Când toate intrările sunt inițializate, întreaga expresie a tabloului produce referința **tabloului**; atribuie-o variabilei locale.

Lanțul este așadar: local → tablou → inamic → statistici sau nume. Fiecare săgeată înseamnă o relație de referință. Nu confunda referința tabloului cu referința unui element.

<details>
<summary>Câte obiecte inamic creează prin el însuși new Enemy[5]?</summary>

Niciunul. Creează un singur obiect tablou cu cinci elemente-referință null. Un inițializator de element sau o atribuire ulterioară trebuie să construiască separat fiecare Enemy, iar și câmpul Stats al fiecărui Enemy are nevoie de propria inițializare.

</details>

## Tipuri de elemente și inferența tipului local

[00:27:03](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1623s) Tablourile împărtășesc aceeași idee a celulelor fixe; **tipul elementului** lor determină ce conține fiecare celulă:

- Un tablou de întregi stochează numere.
- Un tablou de instanțe de clasă stochează referințe către obiecte.
- Un tablou de structuri stochează în elementul însuși valorile câmpurilor fiecărei structuri. Un câmp de tip referință din interiorul unei structuri conține în continuare o referință.

[00:27:32](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1652s) **Var** cere inferența tipului local la compilare. [00:27:34](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1654s) Deoarece expresia atribuită produce un `GameState`, acesta este tipul localului. Compararea stării din videoclip poate fi scurtată la:

```csharp
GameState explicitState = new GameState
{
    Player = new Stats { Health = 10, Attack = 5 },
    Enemies = new Enemy[0]
};

var inferredState = new GameState
{
    Player = new Stats { Health = 10, Attack = 5 },
    Enemies = new Enemy[0]
};
```

Aceste alternative folosesc ulteriorul câmp grupat `Enemies` pentru a păstra exemplul scurt. Ambele localuri sunt de tip static `GameState`. `var` nu înseamnă că tipul lor se poate schimba ulterior. Diferă de `new()` cu tip țintă: `var` inferă tipul din partea stângă pe baza celei din dreapta, în timp ce `new()` are nevoie de un tip de destinație deja cunoscut.

## Înlocuiește copiile per inamic cu foreach

[00:27:36](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1656s) O **buclă foreach** ia câte un element pe rând și execută același corp pentru el. Acesta este pasul care elimină locurile de apel de luptă separate pentru fiecare inamic numit.

[00:27:52](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1672s) Elementul are deja create câmpurile de statistici și nume pe `Enemy`, deci corpul comun le poate folosi. [00:28:03](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1683s) După un schimb, asigură-te că jucătorul mai este în viață înainte de a lua alt tur de inamic. Starea inițială a buclei din videoclip poate fi reconstruită astfel:

```csharp
foreach (Enemy enemy in state.Enemies)
{
    Attack(state.Player, enemy.Stats, enemy.Name);
    if (state.Player.Health <= 0)
        break;
}
```

[00:28:24](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1704s) **Break** iese imediat din această buclă. sare peste toți inamicii rămași, ducând regula anterioară „niciun tur după moartea jucătorului” în corpul comun unic.

[00:28:30](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1710s) Înregistrarea examinează verificările de viață duplicate pe măsură ce blocurile vechi cu pază devin o buclă. Dacă se știe că jucătorul este viu la intrare, verificarea din nou înaintea primului schimb nu adaugă informație. [00:28:53](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1733s) Viața se poate schimba doar acolo unde se aplică dămătura; verificarea utilă este după acea acțiune și înaintea altui schimb.

[00:29:10](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1750s) Elimină verificarea redundantă și pune în corp lucrul necesar pentru fiecare inamic. Bucla are în continuare nevoie de garanția că jucătorul este viu la intrare, furnizată de controlul de luptă din jur; această simplificare nu justifică atacul cu un jucător inițial mort.

[00:29:23](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1763s) În `foreach`, `var` inferă tipul elementului din colecție. [00:29:44](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1784s) Un tablou local poate folosi și el `var` când expresia sa din dreapta furnizează un tip de tablou. Forma echivalentă scurtată a stării din videoclip este:

```csharp
var enemies = new Enemy[0]; // type: Enemy[]
foreach (var enemy in enemies) // type of enemy: Enemy
{
    Console.WriteLine(enemy.Name);
}
```

Acest exemplu de tablou gol demonstrează doar sintaxa. Cu tabloul inițializat de cinci inamici, același corp rulează de cinci ori, dacă nu se iese mai devreme din control.

## Păstrează inițializarea într-o funcție fabrică

[00:29:53](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1793s) Variabila temporară `enemies` aparține construirii. Lupta ar trebui să citească inamicii deținuți de starea jocului, ca ulteriorul cod să aibă o singură sursă clară a datelor jocului. [00:30:14](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1814s) Împiedică reutilizarea accidentală a temporarului de construcție.

[00:30:24](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1824s) Un **domeniu de bloc** este regiunea din interiorul acoladelor în care declarațiile locale sunt vizibile. Mutarea construirii într-un bloc ascunde `enemies` după aceea, dar ascunde și un `state` declarat acolo.

[00:30:42](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1842s) Mută declarația stării jocului deasupra blocului, apoi atribuie-o în interior. Această reconstrucție intermediară a stării din videoclip păstrează tabloul temporar privat acelui bloc:

```csharp
GameState state;
{
    var enemies = new Enemy[0];
    state = new GameState
    {
        Player = new Stats { Health = 10, Attack = 5 },
        Enemies = enemies
    };
}
// enemies is out of scope; state is assigned and usable
foreach (var enemy in state.Enemies)
    PrintHealth(enemy.Stats, enemy.Name);
```

[00:31:02](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1862s) Un local trebuie declarat acolo unde utilizările sale intenționate îl pot vedea și trebuie atribuit înainte de a fi citit. Când muți doar declarația în afară, scrie explicit tipul `GameState`: un `var state;` neinițializat nu poate infera un tip. Starea este necesară în exterior; variabila temporară de inamici, în mod deliberat, nu.

[00:31:16](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1876s) Extrage inițializarea într-o **funcție fabrică**, o funcție care construiește și întoarce un rezultat gata de utilizare. [00:31:35](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1895s) Apelantul își obține atunci starea direct din acea funcție. [00:31:36](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1896s) Tabloul temporar rămâne în propriul domeniu al funcției.

Organizarea rezultată, scurtată, a stării din videoclip este:

```csharp
GameState CreateState()
{
    var enemies = new Enemy[0]; // use the initialized enemy data in the full demo
    return new GameState
    {
        Player = new Stats { Health = 10, Attack = 5 },
        Enemies = enemies
    };
}

var state = CreateState();
```

**Valoarea de întoarcere** este `GameState`-ul creat; `return` i-l predă apelantului. Această funcție nu poate fi `void`, pentru că apelantul are nevoie de un rezultat. Detaliile inițializării nu mai ocupă domeniul apelantului, iar lupta citește `state.Enemies`.

## Repetă rundele și sare peste inamicii morți

[00:31:48](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1908s) O parcurgere procesează o rundă de luptă. Pentru a continua până când o parte este învinsă, repetă acea parcurgere într-o buclă din jur.

[00:32:02](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1922s) Înainte de a permite unui inamic să acționeze, inspectează-i viața. Un inamic mort nu trebuie să ia un tur. **Continue** sare peste corpul rămas pentru inamicul curent și trece la următorul. Corpul stării intermediare din videoclip pentru o rundă este:

```csharp
foreach (var enemy in state.Enemies)
{
    if (enemy.Stats.Health <= 0)
        continue;

    Attack(state.Player, enemy.Stats, enemy.Name);

    if (state.Player.Health <= 0)
        break;
}
```

[00:32:14](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1934s) Doar verificarea vieții inamicilor nu poate schimba viața jucătorului. Dacă runda începe cu un jucător viu și se oprește imediat după un schimb letal, o altă verificare a jucătorului după testul inamicilor morți este redundantă.

[00:32:24](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1944s) Afișează viața după trecerea cu inamicii, astfel încât fiecare rundă să aibă un rezultat observabil. O continuare simplificată a aceleiași stări din videoclip este:

```csharp
PrintHealth(state.Player, "Player");
foreach (var enemy in state.Enemies)
    PrintHealth(enemy.Stats, enemy.Name);
```

Parcurgerea de luptă și parcurgerea de raportare au sarcini diferite. Raportarea poate arăta viața inamicilor care nu au acționat pentru că jucătorul a murit; viața negativă și zero rămân vizibile pentru verificarea rezultatului.

## Întreabă dacă mai există vreun inamic în viață

[00:32:29](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1949s) Repetarea luptei necesită două condiții de oprire: jucătorul moare sau toți inamicii mor. [00:32:50](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1970s) Exprimă ambele înainte de a organiza repetarea. Moartea jucătorului este direct `state.Player.Health <= 0`; înfrângerea inamicilor necesită o inspecție a colecției.

[00:33:01](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1981s) Pentru a detecta faptul că toți inamicii sunt morți, vizitează inamicii și întreabă dacă vreunul are viață pozitivă. [00:33:10](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1990s) O funcție auxiliară este mai curată decât propagarea unui **indicator** boolean, o variabilă care memorează decizia, prin codul de luptă al apelantului.

[00:33:16](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1996s) Trimite colecția ca parametru și întoarce un răspuns **boolean**. Această reconstrucție scurtată a stării din videoclip numește forma pozitivă a testului:

```csharp
bool AnyEnemyAlive(Enemy[] enemies)
{
    foreach (var enemy in enemies)
    {
        if (enemy.Stats.Health > 0)
            return true;
    }
    return false;
}
```

[00:33:26](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2006s) Un singur inamic viu dovedește că nu sunt toți morți. Dacă parcurgerea nu găsește niciunul, toți inamicii sunt morți. Demonstrația descrie și întrebarea inversă, „Au murit toți inamicii?”; inversarea răspunsului inversează folosirea pazei.

[00:33:30](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2010s) O **pază** este o condiție care decide dacă procesarea ar trebui să continue. Refolosește acest auxiliar pentru decizia respectivă: continuă să lupți cât timp răspunde adevărat sau oprește-te când răspunde fals. `!` din `!AnyEnemyAlive(...)` înseamnă negare logică, transformând „vreun inamic viu” în „niciunul viu”.

<details>
<summary>Ce ar trebui să întoarcă AnyEnemyAlive pentru o colecție goală?</summary>

Fals. Bucla nu găsește niciun inamic viu, deci execuția ajunge la ultimul return. Aceasta oferă o condiție de terminare utilă când nu mai rămân inamici.

</details>

## Dă luptei repetate o buclă explicită

[00:33:40](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2020s) Dacă jucătorul trăiește și cel puțin un inamic trăiește, controlul trebuie să revină la începutul rundei. [00:34:14](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2054s) Refolosește acel început în loc să scrii încă o copie.

[00:34:22](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2062s) Înregistrarea ia în calcul revenirea la un punct etichetat, apoi recomandă o buclă. Un **return** gol nu sare la începutul rundei: el părăsește funcția. Saltul de revenire discutat în transcriere este un salt dirijat de etichetă; structura mai clară este o buclă `while` infinită cu ieșiri explicite. [Referința Microsoft pentru instrucțiunile de salt](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements) distinge return de controlul buclei.

[00:34:30](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2070s) Înlocuiește ieșirile de nivel funcție cu **break** când acțiunea dorită este părăsirea acestei bucle de runde repetate. [00:34:40](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2080s) **While** repetă cât timp condiția sa este adevărată; `while (true)` începe întotdeauna o altă iterație, dacă o ieșire din corp nu o oprește.

Starea rezultată, scurtată, din videoclip este:

```csharp
while (true)
{
    if (state.Player.Health <= 0 || !AnyEnemyAlive(state.Enemies))
        break;

    foreach (var enemy in state.Enemies)
    {
        if (enemy.Stats.Health <= 0)
            continue;

        Attack(state.Player, enemy.Stats, enemy.Name);
        if (state.Player.Health <= 0)
            break; // exits the enemy pass
    }

    PrintHealth(state.Player, "Player");
    foreach (var enemy in state.Enemies)
        PrintHealth(enemy.Stats, enemy.Name);
}
```

Operatorul `||` înseamnă „sau”: oricare dintre cele două condiții de oprire este suficientă. Break-ul interior împiedică un alt schimb cu un inamic; break-ul exterior încheie repetarea rundelor. Paza următoare observă eventualele morți din parcurgerea precedentă.

[00:34:53](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2093s) În execuția înregistrată, fiecare inamic este atacat în prima rundă și viața este afișată. În runda următoare, doi inamici lovesc jucătorul, jucătorul moare, iar inamicii rămași în viață își păstrează viața redusă. [00:35:02](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2102s) Dacă jucătorul primește **50 viață**, supraviețuiește până în runda a doua și îi învinge pe toți inamicii. Acest test verifică repetarea și condiția alternativă de oprire.

Rezultatul așteptat depinde de statisticile de intrare, dar regula de control nu depinde: nu rula alt schimb cu un inamic după moartea jucătorului și nu începe altă rundă de luptă după ce toți inamicii sunt morți.

## Un tablou fix nu poate crește

[00:35:12](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2112s) Cu repetarea funcționând, următoarea întrebare de proiectare este lăsată inițial deschisă: cum poate apărea un inamic nou după fiecare rundă? [00:35:42](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2142s) Inamicul propus are **un atac și o viață**. Aceasta necesită un container al cărui număr de elemente poate crește.

[00:35:56](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2156s) **Lungimea** unui tablou obișnuit este fixată la creare. Schimbarea unei celule este permisă; adăugarea unei aselmenea celule la un tablou cu cinci celule nu este.

[00:36:09](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2169s) O soluție manuală ar fi:

1. Alocă un tablou nou cu un element mai lung.
2. Copiază în el referințele vechi ale inamicilor.
3. Pune referința noului inamic în ultima celulă.
4. Înlocuiește referința către vechiul container cu referința către cel nou.

O reconstrucție scurtată a acelei soluții propuse în starea din videoclip este:

```csharp
Enemy[] bigger = new Enemy[enemies.Length + 1];
for (int i = 0; i < enemies.Length; i++)
    bigger[i] = enemies[i];
bigger[enemies.Length] = newEnemy;
enemies = bigger;
```

Aici `for` este forma de buclă controlată de indice: pornește `i` de la zero, repetă cât timp este mai mic decât vechea lungime și îl incrementează cu `i++`. Valorile copiate sunt referințe către inamicii existenți; aceasta nu clonează statisticile lor.

[00:36:18](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2178s) .NET oferă un **tablou dinamic** mai la îndemână, o colecție extensibilă gestionată pentru apelant. [00:36:28](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2188s) Un `List<T>` urmărește un tablou de susținere intern și înlocuiește acea stocare cu un tablou mai mare când nu mai are loc. Tabloul de susținere are el însuși lungime fixă; lista gestionează înlocuirea și păstrează elementele.

## Schimbă containerul de inamici în List

[00:36:38](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2198s) **List<T>** este tipul de listă extensibilă. Numele dintre paranteze unghiulare este **argumentul său generic**, tipul fiecărui element. Schimbă containerul de inamici din `Enemy[]` în `List<Enemy>`.

[00:37:07](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2227s) Colecția stochează înregistrări complete despre inamici. [00:37:17](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2237s) Argumentul său de element trebuie așadar să fie `Enemy`, nu tipul anterior alcătuit doar din statistici.

[00:37:21](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2241s) O **expresie de colecție** furnizează elemente inițiale cu paranteze pătrate și folosește tipul destinație pentru a alege colecția. Această reconstrucție scurtată a stării din videoclip păstrează două intrări inițiale ilustrative:

```csharp
List<Enemy> enemies =
[
    new Enemy
    {
        Name = "Enemy 1",
        Stats = new Stats { Attack = 6, Health = 10 }
    },
    new Enemy
    {
        Name = "Goblin",
        Stats = new Stats { Attack = 1, Health = 10 }
    }
];
```

Aceeași sintaxă de element în paranteze pătrate poate inițializa un tablou sau o listă într-o versiune de limbaj compatibilă. Nu este o instrucțiune de a aloca un tablou obișnuit indiferent de destinație. Expresiile de colecție necesită C# 12 sau ulterior; aceasta este o clarificare de sintaxă, nu un pas separat de instalare predat în lecție. [Referința Microsoft pentru expresiile de colecție](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/collection-expressions) descrie tipajul destinației.

[00:37:31](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2251s) Actualizează și declarația care păstrează inamicii în starea jocului. Orice parametru auxiliar care accepta anterior un tablou de inamici trebuie să corespundă tipului de colecție schimbat. O continuare scurtată a acestei conversii, din starea videoclipului, este:

```csharp
class GameState
{
    public required Stats Player;
    public required List<Enemy> Enemies;
}

bool AnyEnemyAlive(List<Enemy> enemies)
{
    foreach (var enemy in enemies)
        if (enemy.Stats.Health > 0)
            return true;
    return false;
}
```

Înregistrările memorate și accesările la câmpurile lor rămân aceleași. `foreach` poate vizita în continuare fiecare inamic; s-au schimbat doar tipul containerului și declarațiile care depind de el.

## Fă apariția și anunțarea unui inamic printr-o singură metodă

[00:37:41](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2261s) **Add** alipește un element nou la listă. Pune adăugarea la sfârșitul unei runde de joc care continuă, după procesarea inamicilor existenți. Reconstrucția inițială a apariției, din starea videoclipului, folosește șoarecele mic propus:

```csharp
state.Enemies.Add(new Enemy
{
    Name = "Rat",
    Stats = new Stats { Attack = 1, Health = 1 }
});
```

[00:37:51](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2271s) Rularea programului modificat arată dămătura și viața jucătorului, apoi dămătura și viața șoarecelui într-un ciclu ulterior. Înregistrarea nou adăugată rămâne în colecție și participă la runda următoare.

[00:38:08](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2288s) Deoarece șoarecele este acum un obiect, afișează-i numele când este adăugat. [00:38:20](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2300s) Adăugarea și anunțarea unui inamic sunt utile în mai multe locuri, deci extrage-le în loc să duci acele două acțiuni.

[00:38:36](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2316s) Ia un **parametru Enemy**: apelantul alege ce să apară, în timp ce operația comună actualizează starea jocului. [00:38:39](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2319s) Metoda adaugă acel inamic și îi afișează propriul nume, deci comportamentul nu este legat de șoricei. Extragerea scurtată a stării din videoclip este:

```csharp
void AddEnemy(GameState state, Enemy enemy)
{
    state.Enemies.Add(enemy);
    Console.WriteLine($"New enemy: {enemy.Name}");
}
```

[00:39:01](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2341s) Creează șoricei prin metoda extrasă. Pentru următorul test, schimbă statisticile șoarecelui: demonstrația scade atacul la **4** și ridică viața la **100**, făcând dămătura și moartea lui eventuală mai ușor de observat. Apelul simplificat apropiat din acea stare a videoclipului este:

```csharp
AddEnemy(state, new Enemy
{
    Name = "Rat",
    Stats = new Stats { Attack = 4, Health = 100 }
});
```

[00:39:12](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2352s) Execuția înregistrată afișează viața jucătorului/inamicului, anunță un inamic nou și apoi arată șoarecele primind dămătură. Aceasta verifică operația refactorizată de apariție.

[00:39:25](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2365s) Cei doi șoricei supraplus din rezultat sunt o indiciu de control al fluxului: jocul a continuat cu o tură în plus, iar fiecare pas de adăugare finalizat a produs încă un șoarece. Lista păstrează intrările, așa cum i s-a cerut. Verifică ordinea terminării și a apariției, în loc să tratezi intrările suplimentare ca pe o eroare de container.

## Elimină inamicii morți după o condiție

[00:39:37](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2377s) Inamicii morți pot dispărea din listă în loc de a fi păstrați și săritați la nesfârșit. Folosește API-ul de eliminare de nivel înalt al listei pentru a-i selecta, în loc să deplasezi manual intrările supraviețuitoare.

[00:40:02](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2402s) Sintaxa neobișnuită a argumentului numește un element, apoi dă o condiție. [00:40:04](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2404s) Condiția este viața din statistici mai mică sau egală cu zero. Această modificare reconstruită a stării din videoclip elimină fiecare intrare moartă:

```csharp
state.Enemies.RemoveAll(enemy => enemy.Stats.Health <= 0);
```

O **lambda** furnizează o funcție mică inline. Aici `enemy` este numele parametrului său; `=>` separă acel nume de corpul său. Corpul este un **predicat**, un test boolean. Pentru fiecare intrare, un rezultat adevărat înseamnă „elimină acest inamic”. Aceasta este o selecție după o condiție, nu o buclă care rulează doar cât timp o condiție rămâne adevărată.

[00:40:13](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2413s) Odată ce intrările moarte sunt eliminate între parcurgerile de luptă, fiecare inamic prezent la începutul parcurgerii următoare este în viață. Verificarea explicită a inamicilor morți de la începutul acelei parcurgeri poate fi eliminată în baza acestui invariant. Păstrează break-ul pentru moartea jucătorului: filtrarea inamicilor nu împiedică jucătorul să moară în timpul unui schimb.

[00:40:23](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2423s) Execuția înregistrată arată că șoarecele ucis dispare și apare doar șoarecele următor; cel ucis nu mai este procesat. [00:40:36](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2436s) Dacă îi dai unui inamic mai multă viață, supraviețuirea lui mai lungă devine vizibilă în rezultatul final. Intrările moarte sunt eliminate, iar odată ce ultimul inamic supraviețuitor moare, bucla se încheie în loc să adauge încă un șoarece pentru altă rundă.

[00:40:51](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2451s) Runda următoare luptă acum doar împotriva intrărilor încă prezente în listă. O reconstrucție scurtată a fluxului final face ordinea explicită:

```csharp
while (true)
{
    if (state.Player.Health <= 0 || state.Enemies.Count == 0)
        break;

    foreach (var enemy in state.Enemies)
    {
        Attack(state.Player, enemy.Stats, enemy.Name);
        if (state.Player.Health <= 0)
            break;
    }

    PrintHealth(state.Player, "Player");
    foreach (var enemy in state.Enemies)
        PrintHealth(enemy.Stats, enemy.Name);

    state.Enemies.RemoveAll(enemy => enemy.Stats.Health <= 0);

    if (state.Player.Health <= 0 || state.Enemies.Count == 0)
        break;

    AddEnemy(state, new Enemy
    {
        Name = "Rat",
        Stats = new Stats { Attack = 4, Health = 100 }
    });
}
```

Aceasta este o reconstrucție clarificată, nu o afirmație că această aranjare exactă a fost commitată. **Count** este numărul intrărilor curente din listă. După filtrare, un count zero înseamnă că nu mai rămân inamici. Eliminarea și apariția au loc după iterația peste inamici, iar verificarea de ieșire are loc înaintea apariției, deci un joc încheiat nu primește încă un șoarece.

[00:41:03](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2463s) Lecția amână explicit mecanica exactă a delegatelor, a formelor invocabile înrudite și a polimorfismului. Un **delegat** reprezintă un comportament invocabil, o **suprasarcină** este o altă formă a unei operații, iar **polimorfismul** privește comportamente diferite printr-o operație sau interfață comună; sunt numite aici pentru a marca limita, nu predate în profunzime.

[00:41:18](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2478s) În această etapă este suficient să citești forma filtrului: un element numit, o condiție booleană și o operație de colecție care o folosește. [00:41:24](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2484s) Această lectură explică eliminarea observată fără a necesita utilajul amânat.

<details>
<summary>De ce verificăm finalizarea jocului înainte de a adăuga următorul șoarece?</summary>

Dacă toți inamicii au murit, adăugarea mai întâi a unui șoarece face din nou lista nevidă. Dacă jucătorul a murit, apariția mai întâi creează un inamic inutil. Filtrează și verifică condițiile de oprire înainte de a programa apariția din runda următoare.

</details>

## Lista este un alt obiect de referință

[00:41:31](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2491s) **List<T> este un tip de referință**. O variabilă de listă conține o referință către obiectul listă, așa cum o variabilă de tablou conține o referință către un obiect tablou.

Ideea finală de memorie din videoclip poate fi scurtată la:

```csharp
List<Enemy> enemies = new List<Enemy>();
List<Enemy> sameList = enemies; // another reference to the same list object
```

[00:41:39](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2499s) Obiectul listă păstrează, la rândul său, o referință către tabloul său de susținere curent. Celulele de inamici ale acelui tablou dețin referințe către inamici, ale căror statistici și nume sunt la rândul lor referințe. Creșterea listei înlocuiește stocarea sa internă fără să ceară codului jocului să recreeze fiecare inamic.

[00:41:46](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2506s) Înregistrarea sare peste detaliile de implementare mai profunde despre acest nivel suplimentar de referință. Punctul final al lecției este practic: grupează datele unei entități, scrie fiecare acțiune o singură dată, iterează peste o colecție și folosește un container extensibil când inamicii pot fi adăugați sau eliminați.

Limitele de domeniu contează de-a lungul lecției: `required` este menționat și amânat temporar înaintea demonstrației sale ulterioare; întrebările despre inițializare sunt apoi puse în aside pentru a reveni la refactorizare; memoria tablourilor de clase este întreruptă și explicată separat; apariția unui inamic este ridicată întâi ca întrebare deschisă și apoi tratată cu o listă. Mecanica delegatelor și a polimorfismului, precum și detaliile mai profunde despre internele listei rămân amânate la final. Ele nu sunt condiții prealabile pentru urmărirea construcției demonstrate.

## Istoricul modificărilor de cod

Nu au fost disponibile stări exacte din commit ale sursei de luptă în dovezile verificate. Aceste legături stabile către stările din videoclip urmăresc secvența relevantă de construcție, de la cea mai veche la cea mai recentă; fragmentele de mai sus sunt reconstrucții scurtate.

- [00:00:42](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=42s) — Scrie schimburi duplicate per inamic, apoi afișează viața pentru a verifica o rundă.
- [00:02:15](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=135s) — Protejează schimburile ulterioare, astfel încât un jucător mort să nu mai poată continua lupta.
- [00:03:41](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=221s) — Grupează viața și atacul în statistici comune și repară accesele calificate prin posesor.
- [00:08:16](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=496s) — Exposează inițializarea lipsă a referințelor cu analiza nullable.
- [00:14:22](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=862s) — Construiește starea jocului cu un singur inițializator de obiect.
- [00:16:03](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=963s) — Cere atribuiri explicite pentru membrii esențiali și distinge valorile explicite de cele implicite.
- [00:17:55](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1075s) — Extrage acțiunile repetate de atac și raportare cu parametri.
- [00:22:00](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1320s) — Combină statisticile și numele în Enemy și adună inamicii într-un tablou.
- [00:27:36](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1656s) — Înlocuiește locurile de apel individuale cu foreach și cu un break la moarte.
- [00:31:16](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=1876s) — Mută construcția într-o funcție fabrică care întoarce starea gata.
- [00:34:30](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2070s) — Repetă rundele într-o buclă explicită, cu condiții de oprire.
- [00:36:38](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2198s) — Schimbă tabloul fix de inamici în List<Enemy>.
- [00:37:41](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2261s) — Alipește inamicii apăruți între runde.
- [00:38:39](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2319s) — Extrage adăugarea și anunțarea unui inamic într-o singură operație.
- [00:40:04](https://www.youtube.com/watch?v=zeuNRQBdwuY&t=2404s) — Elimină intrările moarte după condiția de viață înainte de runda următoare.