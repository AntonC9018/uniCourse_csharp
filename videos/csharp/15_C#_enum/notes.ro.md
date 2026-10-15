> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Scop și vocabular

[00:00:00](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=0s) Construiește o reprezentare fiabilă a alegerilor discrete ale unei bombe, pornind de la text și trecând prin numere, constante cu nume și, în final, tipuri `enum` separate. În temă, un jucător citește un manual, iar altul inspectează bomba și urmează instrucțiunile pentru a decide ce fir să taie. [00:00:18](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=18s) Bomba are unul dintre trei portrete — Lenin, George Washington sau Hitler — împreună cu o culoare a firului, o stare a comutatorului și o valoare a cadranului. Această lecție dezvoltă reprezentarea portretelor și a culorilor lămpii; nu implementează jocul complet de dezarmare.

Termenii folosiți pe parcurs sunt:

- **Variabilă și atribuire:** o variabilă stochează o valoare curentă; atribuirea evaluează o expresie și îi stochează rezultatul. O atribuire ulterioară înlocuiește valoarea anterioară.
- **Tip, șir și întreg:** un tip descrie utilizările permise ale unei valori. Un `string` reprezintă text; `int` reprezintă un număr întreg dintr-un interval fix.
- **Egalitate și condiție:** `==` compară valori și produce `true` sau `false`; o condiție `if` alege dacă un bloc se execută.
- **Timp de compilare și timp de execuție:** timpul de compilare este momentul în care sursa este verificată și tradusă; timpul de execuție este momentul în care programul rulează. O eroare de compilare împiedică construirea, o eroare de execuție apare în timpul rulării, iar o eroare de logică poate produce un comportament greșit fără niciuna dintre ele.
- **Funcție, argument, parametru și semnătură:** o funcție efectuează o operație. Un argument este valoarea furnizată de apelant; un parametru este numele local al funcției pentru acea intrare. Semnătura descrie numele funcției și tipurile parametrilor; declarația ei menționează și tipul de retur.
- **Expresie, evaluare și `return`:** o expresie produce o valoare. Evaluarea o calculează, iar `return` încheie apelul funcției și furnizează rezultatul.
- **Bloc și domeniu de vizibilitate:** acoladele `{ }` grupează instrucțiunile într-un bloc; domeniul de vizibilitate stabilește unde poate fi folosit un nume declarat.
- **Constantă și număr magic:** o constantă este o valoare cu nume fixată la timpul compilării. Un număr magic este un literal numeric al cărui sens în aplicație este ascuns.
- **`switch`, `case` și `default`:** o instrucțiune `switch` selectează o ramură pentru o valoare. Un `case` etichetează o ramură care se potrivește; `default` gestionează valorile fără un `case` corespunzător.
- **Enumerare, membru și valoare de bază:** o enumerare (`enum`) este un tip de valoare distinct, cu constante cu nume numite membri. Fiecare membru are o valoare numerică de bază; tipul de bază implicit este `int`.
- **Abstrație și contract de tip:** abstracția permite cititorilor să lucreze cu nume semnificative, ascunzând detaliile reprezentării. Un contract de tip spune apelanților ce categorie de valori acceptă o funcție.
- **Cast:** conversie explicită, scrisă ca `(TargetType)expression`; conversia unui număr la un `enum` nu verifică dacă un membru are acel număr.
- **Aserțiune, diagnostic și excepție:** o aserțiune verifică o presupunere în timpul dezvoltării. `Debug.Assert` și `Debug.Fail` raportează diagnostice; o excepție întrerupe execuția normală și poate termina programul dacă nu este tratată.
- **Tablou și santinelă `Count`:** un tablou stochează un număr fix de elemente. O santinelă este un marcaj special; un membru final `Count` poate marca numărul de alegeri reale într-o convenție de numerotare consecutivă, pornită de la zero.

[Laboratorul de `enum`](../../../labs/1_basic/09_enum.md) oferă un exercițiu separat cu semaforul. Tot codul de mai jos este o reconstrucție scurtă a stărilor din videoclipul legat, cu corectările identificate unde este necesar. Nicio sursă locală inspectată nu conține aceste stări exacte ale programului cu bomba, deci mostrele nu sunt prezentate ca cod identic cu un commit.

# Începe cu un șir

[00:00:28](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=28s) Pornește de la reprezentarea cea mai simplă: un **șir**, o valoare de text, care ține numele portretului afișat. O **variabilă** stochează valoarea curentă. Jocul intenționează ca această variabilă să țină exact unul dintre cele trei nume la un moment dat; tipul `string` în sine permite orice text și nu impune această restricție.

Starea inițială reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=28s):

```csharp
string portrait = "Ленин";
Console.WriteLine(portrait);
```

[00:00:47](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=47s) Dacă valoarea curentă este `"Ленин"`, acea valoare identifică portretul afișat. Reatribuirea variabilei la alt nume schimbă portretul curent; nu adaugă un al doilea portret simultan.

[00:00:57](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=57s) Pentru a verifica ce portret este încărcat, repetă numele așteptat într-o comparație de **egalitate**. Condiția produce un rezultat boolean, iar blocul rulează doar când acesta este adevărat.

Starea cu verificarea șirului reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=57s):

```csharp
if (portrait == "Ленин")
{
    Console.WriteLine("Lenin is displayed");
}
```

Funcționează, dar face ca fiecare verificare să depindă de scrierea exactă a aceluiași text.

# Urmează ordinea de execuție

[00:01:11](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=71s) Instrucțiunile se execută în ordinea în care sunt scrise. Afișarea variabilei înainte de reatribuire afișează valoarea anterioară. **Atribuirea** înlocuiește valoarea curentă; instrucțiunile ulterioare văd înlocuirea.

Demonstrația ordinii de execuție reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=71s):

```csharp
string portrait = "Ленин";
Console.WriteLine(portrait);       // Afișează Ленин
portrait = "Гитлер";
if (portrait == "Ленин")
{
    Console.WriteLine("Matched");
}
```

[00:01:22](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=82s) Verificarea de după atribuirea ulterioară nu rulează: în acel moment, variabila conține `"Гитлер"`, nu `"Ленин"`. Condiția citește valoarea curentă, nu istoricul variabilei.

[00:01:32](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=92s) Mută verificarea înaintea reatribuirii și ea rulează, pentru că valoarea curentă este încă numele așteptat. Înregistrarea demonstrează executarea blocului după această mutare.

Starea cu verificarea mutată reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=92s):

```csharp
string portrait = "Ленин";
if (portrait == "Ленин")
{
    Console.WriteLine("Matched");
}
portrait = "Гитлер";
```

<details>
<summary>Ar schimba mutarea doar a instrucțiunii de afișare ce valoare vede condiția?</summary>

Nu. Condiția depinde de ce atribuire o precedă. Mutarea afișării schimbă ce se afișează în acel punct, dar nu schimbă variabila.

</details>

# De ce șirurile sunt identificatori fragili

[00:01:36](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=96s) Egalitatea șirurilor cere text identic: aceeași lungime și aceleași caractere în pozițiile corespunzătoare. Înregistrarea explică aceasta ca pe o verificare a caracterelor în ordine. Este un model util al rezultatului cerut, nu o promisiune despre implementarea exactă în runtime.

Demonstrația greșelii de scriere reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=108s):

```csharp
string portrait = "Гитлер";
if (portrait == "Гитлре")          // Nume așteptat scris greșit
{
    Console.WriteLine("Matched");
}
```

[00:01:48](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=108s) Numele așteptat scris greșit face ca această condiție să fie falsă, deci blocul nu se execută. [00:02:02](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=122s) Adăugarea de caractere eșuează și ea: un prefix comun nu este suficient, iar caracterele în plus schimbă lungimea.

[00:02:14](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=134s) O **eroare de compilare** este o problemă a sursei, detectată înainte de execuție. Un portret scris greșit între ghilimele este totuși un șir valid, deci compilatorul nu poate identifica greșeala aplicației. [00:02:32](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=152s) Nici nu este în mod inerent o eroare de execuție. Programul poate rula fără să se prindă și poate lua în tăcere ramura greșită: aceasta este o **eroare de logică**.

[00:02:36](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=156s) Lecția motivează și numerele prin volumul de lucru necesar comparării șirurilor: șirurile pot cere verificări de lungime și comparații pe conținutul lor, în timp ce compararea valorilor întregi nu implică parcurgerea textului. Nu interpretați „cu mult mai multe operații” drept un rezultat măsurat pentru fiecare comparație; căile rapide și potrivirile timpurii pot reduce munca de comparare a șirurilor.

[00:03:02](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=182s) Afișarea lizibilă este principalul motiv pentru care acest exemplu are nevoie de text. [00:03:19](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=199s) Vrem ca jucătorul să vadă un nume, dar decizia internă nu are nevoie să folosească acel nume drept identificator. [00:03:36](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=216s) Păstrarea identificatorului ca șir lasă riscul greșelii tăcute atât în atribuiri, cât și în verificările repetate. Separarea identității de textul afișat ne va permite să păstrăm o ieșire lizibilă fără să folosim text peste tot.

# Înlocuiește numele cu coduri întregi

[00:03:46](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=226s) Un **întreg** este o valoare întreagă. Schimbă tipul variabilei în `int` și stabilește codificarea: Lenin este 0, George Washington este 1, iar Hitler este 2.

Starea cu coduri întregi reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=226s):

```csharp
// 0 = Lenin, 1 = George Washington, 2 = Hitler
int portrait = 2;
if (portrait == 2)
{
    Console.WriteLine("Matched");
}
Console.WriteLine(portrait);       // Afișează 2
```

[00:04:06](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=246s) Un singur număr reprezintă acum întregul portret. Nu mai există un nume de scris greșit într-o comparație numerică, deși alegerea numărului greșit rămâne posibilă. Un `int` permite și valori din afara celor trei coduri intenționate.

[00:04:21](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=261s) Afișarea expune acum reprezentarea: `2` nu îi explică jucătorului că este afișat Hitler. [00:04:32](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=272s) Videoclipul amână scurt această problemă nouă înainte de a o relua mai târziu: avem nevoie de o modalitate de a converti un portret numeric înapoi în text lizibil.

# Construiește o funcție de transformare a numărului în text

[00:04:34](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=274s) O **funcție** poate mapa identitatea numerică pe textul necesar pentru ieșire. [00:04:50](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=290s) Ramifică după intrare: 0 se mapează la Lenin, 1 la Washington și 2 la Hitler.

[00:05:08](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=308s) Un cuvânt izolat precum `Hitler` este un identificator, nu un literal de șir. Fără o declarație care să-i dea sens, nu poate furniza textul dorit; scris singur ca instrucțiune, nu exprimă nici conversia. Rezultatul trebuie să fie o expresie care ocupă poziția argumentului de afișare.

[00:05:15](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=315s) Introdu o funcție cu o intrare `int` și un rezultat `string`. Transcrierea se referă la conversia personalizată ca la o funcție „de int la string”. Aici este numită `PortraitToString` pentru a o deosebi de `portrait.ToString()` încorporată, fără parametri, care ar produce `"2"` pentru un întreg, nu o etichetă de portret.

Starea cu funcția de conversie reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=315s), cu o valoare de rezervă provizorie, examinată în curând:

```csharp
static string PortraitToString(int p)
{
    if (p == 0) { return "Ленин"; }
    if (p == 1) { return "Джордж Вашингтон"; }
    if (p == 2) { return "Гитлер"; }
    return "";
}
```

[00:05:25](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=325s) **Parametrul** `p` este numele local al intrării funcției. Nu trebuie să aibă același nume ca variabila `portrait` a apelantului. **Argumentul** este valoarea furnizată la locul apelului.

[00:05:36](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=336s) Înlocuiește numărul brut de la locul afișării cu un apel de conversie:

Locul de afișare modificat, reconstruit din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=336s):

```csharp
int portrait = 2;
Console.WriteLine(PortraitToString(portrait));
```

[00:05:46](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=346s) O formă echivalentă în doi pași stochează mai întâi textul convertit, apoi îl afișează:

Alternativa în doi pași reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=346s):

```csharp
string portraitText = PortraitToString(portrait);
Console.WriteLine(portraitText);
```

Ambele forme exprimă aceeași dependență: calculează eticheta înainte de a o afișa.

# Evaluează un apel și returnează rezultatul

[00:05:53](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=353s) O **expresie** este cod care produce o valoare, iar **evaluarea** calculează acea valoare în timpul execuției. Un apel de conversie este o expresie cu valoare de șir după ce argumentul său întreg a fost evaluat.

Folosind [starea din videoclip cu doi pași](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=353s):

```csharp
string portraitText = PortraitToString(portrait);
```

Urmează apelul în ordine:

1. [00:05:53](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=353s) Citește argumentul `portrait`, care conține în prezent 2.
2. [00:06:04](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=364s) Intră în funcție, parametrul ei primind acea valoare.
3. [00:06:16](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=376s) Evaluează expresia din dreapta înainte de a finaliza atribuirea.
4. [00:06:37](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=397s) Găsește ramura pentru 2 și obține șirul care se potrivește.
5. [00:06:52](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=412s) Apelul produce `"Гитлер"`; aceasta este evaluarea unei expresii în runtime, nu compilatorul editând sursa.
6. Stochează rezultatul în `portraitText`. O variabilă de șir păstrează o referință la șirul rezultat; atribuirea nu alocă neapărat un nou obiect șir.

[00:07:00](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=420s) O instrucțiune **return** identifică rezultatul furnizat apelantului și încheie această invocare a funcției.

Ramura bazată pe `return`, reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=420s):

```csharp
if (p == 2)
{
    return "Гитлер";
}
```

[00:07:13](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=433s) Înregistrarea adaugă acolade pentru a face din ramură un **bloc**, un grup de instrucțiuni. Păstrează acest stil clar, dar afirmația că `return` însuși ar cere întotdeauna aceste acolade este prea generală: `if (p == 2) return "Гитлер";` este C# valid. O funcție normală cu corp de bloc are în continuare nevoie de acoladele sale exterioare. [Referința C# pentru instrucțiunile de selecție](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements) distinge instrucțiunile simple de blocuri.

[00:07:21](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=441s) Odată executat `return`, instrucțiunile ulterioare din această funcție nu mai rulează. De aceea trei verificări `if` succesive pot înlocui un lanț `if/else if` atunci când fiecare ramură reușită returnează. Vezi [referința pentru instrucțiunea `return`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements#the-return-statement).

<details>
<summary>După ce ramura pentru 2 returnează, se oprește și apelantul?</summary>

Nu. Se încheie această invocare a funcției. Apelantul reia cu șirul returnat și finalizează atribuirea sau afișarea.

</details>

# Fă valorile neașteptate vizibile

[00:07:28](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=448s) Ce se întâmplă dacă `p` este 5? O funcție care returnează un șir nu poate ajunge la final fără să producă un șir sau fără să ia un traseu care nu returnează normal. Compilatorul vede că cele trei condiții nu acoperă fiecare întreg posibil.

[00:07:44](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=464s) O valoare de rezervă precum `return "";` satisface cerința de retur, dar poate ascunde o greșeală de programare. [00:07:49](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=469s) Lecția preferă un eșec imediat și vizibil în timpul dezvoltării în loc de a returna în tăcere o ieșire fără sens.

Starea cu diagnostic de dezvoltare reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=482s):

```csharp
static string PortraitToString(int p)
{
    if (p == 0) { return "Ленин"; }
    if (p == 1) { return "Джордж Вашингтон"; }
    if (p == 2) { return "Гитлер"; }

    System.Diagnostics.Debug.Fail("Unknown portrait");
    return "";
}
```

[00:08:02](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=482s) `Debug.Fail` este un **diagnostic**, un raport că o presupunere a eșuat. Compilatorul nu îl tratează ca pe o terminare garantată, deci `return`-ul următor rămâne necesar în această formă. [00:08:14](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=494s) Programul final intenționat nu ajunge niciodată în această ramură; faptul că ajunge în ea în timpul dezvoltării înseamnă că reprezentarea sau codul apelant trebuie reparat.

[00:08:28](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=508s) Presupunerea funcției este că intrarea este una dintre valorile de portret permise. [00:08:42](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=522s) Jocul definește trei portrete, deci o a patra valoare necunoscută nu are sens în acest model. [00:08:54](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=534s) Ar trebui raportată ca eroare, nu că i se atribuie o etichetă cu aspect plauzibil. Este o verificare a unei presupuneri de programare, nu alegerea obișnuită a jucătorului între portrete valide.

[00:09:16](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=556s) O **aserțiune** verifică o presupunere, de exemplu `Debug.Assert(false, "Unknown portrait")` pe traseul neașteptat. Ambele apeluri, de aserțiune și de eșec, sunt condiționate de simbolul de compilare `DEBUG`. În mod normal sunt omise în compilările Release; aceasta nu este o consecință a optimizării singure. `return "";` necondiționat rămâne. De asemenea, aceste diagnostice nu garantează universal o blocare: comportamentul depinde de gestionarea diagnosticelor. Vezi [Debug.Assert](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.debug.assert) și [Debug.Fail](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.debug.fail).

Pentru o întrerupere garantată a fluxului normal de control, o alternativă corectată pentru [starea din videoclip cu valoare neașteptată](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=469s) folosește o **excepție**:

```csharp
throw new System.ArgumentOutOfRangeException(nameof(p));
```

Aici, `throw` ridică excepția, `new` construiește obiectul ei, iar `nameof(p)` furnizează numele parametrului ca șir. Această înlocuire este o clarificare de corectitudine, nu o afirmație că videoclipul a folosit exact acea linie. O excepție netratată termină programul; un apelant o poate și prinde într-un `catch`. Distincția contează mai târziu, când un `case` lipsă din `enum` ajunge la valoarea de rezervă.

# Dă nume numerelor magice

[00:09:39](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=579s) Rularea conversiei arată valoarea 2 rezolvându-se în Hitler. Programul afișează acum o etichetă utilă, dar sursa conține în continuare **numere magice**: numere al căror sens în aplicație nu este vizibil la locurile de folosire.

[00:10:02](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=602s) Un comentariu care spune „2 înseamnă Hitler” este doar o convenție scrisă lângă program. Nu leagă fiecare apariție a lui 2 de conceptul de portret.

[00:10:10](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=610s) Introdu **constante**, valori cu nume fixate la timpul compilării, astfel încât fiecare cod să își spună sensul:

Starea cu constante cu nume reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=610s):

```csharp
const int Lenin = 0;
const int GeorgeWashington = 1;
const int Hitler = 2;

int portrait = Hitler;
```

[00:10:28](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=628s) Înlocuiește fiecare 0, 1 și 2 specific portretului din logică cu constanta corespunzătoare, inclusiv în atribuiri și în verificările de conversie.

Verificările de conversie rescrise, reconstruite din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=628s):

```csharp
if (p == Lenin) { return "Ленин"; }
if (p == GeorgeWashington) { return "Джордж Вашингтон"; }
if (p == Hitler) { return "Гитлер"; }
```

Valorile numerice rămân, dar acum sunt declarate într-un singur loc și folosite prin nume semnificative. Astfel se îmbunătățește lizibilitatea, fără a separa încă portretele de celelalte categorii bazate pe întregi.

# Înlocuiește comparațiile repetate cu switch

[00:10:39](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=639s) Sunt propuse două îmbunătățiri: rescrierea comparațiilor repetate cu `switch` și darea fiecărei familii de valori a unui tip propriu. Mai întâi schimbăm ramificarea.

[00:10:55](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=655s) O **instrucțiune `switch`** selectează o ramură pe baza unei singure valori de intrare. Aceasta este forma comparațiilor repetate: compari același `p` cu constante diferite. Videoclipul folosește o instrucțiune `switch` cu etichete `case`, nu trăsătura separată din C# numită expresie `switch`.

Conversia bazată pe `switch`, reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=655s):

```csharp
static string PortraitToString(int p)
{
    switch (p)
    {
        case Lenin:
        {
            return "Ленин";
        }
        case GeorgeWashington:
        {
            return "Джордж Вашингтон";
        }
        case Hitler:
        {
            return "Гитлер";
        }
        default:
        {
            System.Diagnostics.Debug.Fail("Unknown portrait");
            return "";
        }
    }
}
```

[00:11:06](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=666s) Scrie acolade după fiecare etichetă `case` pentru a crea un **bloc** individual. [00:11:09](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=669s) Un bloc oferă variabilelor locale un **domeniu de vizibilitate** clar, regiunea în care numele lor pot fi folosite. Fără un bloc interior, o variabilă locală declarată într-o secțiune `switch` poate avea domeniu de vizibilitate pe tot blocul `switch`, în timp ce regulile de atribuire definită îi limitează totuși folosirea. Acoladele per `case` evită coliziuni și vizibilități derutante între nume.

[00:11:33](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=693s) Acest `switch` exprimă aceeași ramificare ca verificările anterioare. Un `return` iese din funcție, deci aceste cazuri nu au nevoie de `break`. Cazurile care ar continua după `switch` s-ar termina în mod normal cu `break`.

[00:11:51](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=711s) Înregistrarea amână pentru scurt sintaxa mai complicată pentru forme de `switch` care nu doar returnează din fiecare caz. [00:11:57](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=717s) Returnarea unui rezultat dintr-un caz selectat este modelul comun și util, accentuat aici. Nota rămâne la forma de instrucțiune demonstrată; nu se extinde într-un tutorial despre expresii `switch`.

# Separă portretele și culorile cu enum

[00:11:59](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=719s) Tema are și culori de lampă precum Blue, White, Red și Green. [00:12:09](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=729s) Trecerea unei culori acolo unde se așteaptă un portret nu are sens, chiar dacă ambele au codul numeric 1. Constantele întregi obișnuite nu pot impune diferența.

[00:12:24](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=744s) O **enumerare**, adică `enum`, este un tip de valoare distinct, cu constante cu nume numite **membri**. **Valoarea sa de bază** este numerică, iar `int` este tipul de bază implicit. Spre deosebire de două grupuri de valori `const int`, două enumerări oferă două categorii distincte.

[00:12:33](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=753s) Declară o singură dată familia de portrete:

Starea cu `enum` de portrete reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=753s):

```csharp
enum Portrait
{
    Lenin = 0,
    GeorgeWashington = 1,
    Hitler = 2
}
```

[00:12:46](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=766s) Membrii folosesc virgule, nu puncte și virgule ca între declarațiile obișnuite de constante. Numele enumerării și tipul de bază se declară o singură dată; fiecare membru nu repetă `const int`. Membrii înșiși au tipul `Portrait`, cu valori numerice de rezervă.

[00:12:55](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=775s) În afara declarației, scrie numele tipului, un punct și numele membrului: `Portrait.Hitler`. Aceasta seamănă cu calificarea unui câmp dintr-o clasă statică.

[00:13:16](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=796s) Aplică aceeași schimbare culorilor:

Starea cu două enumerări reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=796s):

```csharp
enum Color
{
    Blue = 0,
    White = 1,
    Red = 2,
    Green = 3
}

Portrait portrait = Portrait.Hitler;
Color lamp = Color.White;
```

[00:13:26](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=806s) O funcție de transformare a culorii în text poate folosi același model `switch` cu etichete; schimbarea categoriei și a cazurilor sale face designul comun mai vizibil.

[00:13:29](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=809s) Folosirea încercată a lui `Color.White` acolo unde se așteaptă un portret produce acum o eroare de compilare. [00:13:36](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=816s) Mai precis, este atribuirea sau apelul între tipuri diferite care eșuează; `Color.White` în sine este un membru de culoare valid. Nu este implicată nicio cerință de iterație.

Atribuirea respinsă reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=809s):

```csharp
Portrait portrait = Portrait.Hitler;
// portrait = Color.White;       // Eroare de compilare: tipuri enum diferite
```

[00:13:43](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=823s) Aceasta este **abstrație**: codul raționează despre portrete și culori după nume, în timp ce reprezentarea lor numerică se află în spatele tipurilor.

[00:14:02](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=842s) Schimbă parametrul funcției de conversie de la `int` la `Portrait` și califică cazurile sale cu `Portrait.`.

Starea cu semnătură tipizată reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=842s):

```csharp
static string PortraitToString(Portrait p)
{
    switch (p)
    {
        case Portrait.Lenin: { return "Ленин"; }
        case Portrait.GeorgeWashington: { return "Джордж Вашингтон"; }
        case Portrait.Hitler: { return "Гитлер"; }
        default:
        {
            System.Diagnostics.Debug.Fail("Unknown portrait");
            return "";
        }
    }
}
```

[00:14:15](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=855s) Acest **contract de tip** al parametrului spune apelanților că funcția acceptă categoria de portrete. Respinge un argument de culoare accidental. Comunică alegerile intenționate cu nume, dar nu dovedește că fiecare valoare `Portrait` posibilă are un nume declarat. Această limitare explică următoarele două demonstrații.

# Păstrează ramura default

[00:14:33](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=873s) `switch`-ul are în continuare nevoie de ramura sa **default**, care gestionează intrările fără un caz care să se potrivească.

[00:14:47](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=887s) Adaugă încă un membru de enumerare fără să actualizezi conversia. [00:15:03](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=903s) Noul membru este acum un argument legal pentru o funcție care ia `Portrait`, dar funcția nu are o ramură pentru el.

Starea cu extensie și caz lipsă reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=887s); `AdditionalPortrait` este un substituent descriptiv pentru alegerea adăugată:

```csharp
enum Portrait
{
    Lenin = 0,
    GeorgeWashington = 1,
    Hitler = 2,
    AdditionalPortrait = 3
}

// Switch-ul anterior are în continuare doar trei cazuri cu nume.
Console.WriteLine(PortraitToString(Portrait.AdditionalPortrait));
```

[00:15:09](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=909s) Valoarea netratată ajunge la valoarea de rezervă de diagnostic. În mediul de dezvoltare demonstrat, aceasta expune cazul lipsă, îndrumând atenția către funcția de conversie. Cu forma din videoclip, cu diagnostic de depanare plus `return` gol, rezultatul depinde de diagnostic și poate deveni o etichetă goală când `DEBUG` lipsește. Cu alternativa care folosește excepția, cazul lipsă întrerupe execuția în orice compilare.

Extinderea unei enumerări și extinderea fiecărei operații exhaustive de pe ea sunt modificări separate. Tipul permite noul argument; nu scrie automat comportamentul lipsă.

# Separă numele membrilor de textul afișat

[00:15:34](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=934s) Afișează direct o valoare de enumerare și, când valoarea ei are un nume declarat, ieșirea folosește în mod normal numele membrului, nu numărul de bază. [00:15:55](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=955s) Așadar, un membru nou poate fi afișat după nume chiar dacă conversia personalizată nu are o ramură care să se potrivească.

Starea cu ieșire de enumerare după nume reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=955s):

```csharp
Portrait portrait = Portrait.Lenin;
Console.WriteLine(portrait);                    // Lenin
Console.WriteLine(PortraitToString(portrait));   // Ленин
```

[00:15:58](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=958s) Prima linie rezolvă valoarea cu nume într-o reprezentare de șir. Acest comportament încorporat ține de formatarea enumerărilor; este independent de funcția de conversie explicită a jocului. [Documentația `Enum.ToString`](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tostring) descrie ieșirea după nume și ieșirea numerică atunci când nu există niciun nume care să se potrivească.

[00:16:10](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=970s) Numele unui membru este un identificator intern, nu neapărat text potrivit pentru jucător. Formularea înregistrării despre spații trebuie înțeleasă ca o cerință pentru etichetele de afișare: identificatorii C# pot folosi litere cirilice, dar identificatorii obișnuitori nu pot conține spații. O etichetă lizibilă precum „Джордж Вашингтон” poate.

[00:16:32](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=992s) Păstrează o conversie explicită atunci când șirul returnat este destinat utilizatorului. Ea permite ca textul afișat să difere de identificatorii din sursă. [00:16:58](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1018s) Trecerea lui `Portrait.Lenin`, scris cu litere latine în sursă, returnează eticheta rusă `"Ленин"`. Redenumirea unui membru intern nu trebuie să redefinească formularea adresată utilizatorului.

# Înțelege stocarea numerică și tipurile de execuție

[00:17:20](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1040s) Un membru precum `Portrait.Hitler` este o constantă cu valoarea numerică de rezervă 2. Valoarea sa poate fi încorporată în codul compilat; o variabilă a acestui `enum` stochează sarcina numerică, nu textul `"Hitler"`.

Starea cu comparație numerică reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1040s):

```csharp
Portrait portrait = Portrait.Hitler;
if (portrait == Portrait.Hitler)
{
    Console.WriteLine("Matched");
}
```

[00:17:32](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1052s) Înregistrarea descrie enumerarea ca pe un concept de timp de compilare și valoarea stocată ca pe un număr. Păstrează observația despre stocarea numerică, dar corectează afirmația că tipul enumerării dispare în runtime: .NET păstrează informația de tip a enumerării și numele membrilor. Stocarea enumerărilor și operațiile numerice nu necesită stocarea șirurilor.

[00:17:40](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1060s) Această informație păstrată explică și formatarea după nume. [00:17:44](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1064s) Afișarea enumerării și afișarea unei conversii întregi explicite pot produce reprezentări diferite ale aceleiași sarcini numerice. Numele nu este ales exclusiv de compilator; formatarea enumerărilor în runtime îl poate rezolva. Vezi [referința tipului `System.Enum`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum#the-systemenum-type-and-enum-constraint) și [`Enum.ToString`](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tostring).

[00:17:55](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1075s) Explicația „fără conversie în șir” aparține verificării de egalitate, nu producerii textului afișat. Compararea a două valori `Portrait` le compară valorile numerice. [00:18:22](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1102s) Numele de membru semnificative fac ca aceste numere să se documenteze singure.

[00:18:23](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1103s) O constantă contribuie cu valoarea sa numerică fixă la acea comparație. [00:18:24](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1104s) Când ambele valori sunt 2, egalitatea este adevărată și blocul păzit rulează. Nu are loc nicio comparație a șirurilor `"Hitler"`.

# Fă cast fără a presupune validarea

[00:18:27](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1107s) Un **cast** convertește explicit o expresie la un tip țintă. Numerele pot fi convertite la un `enum` chiar dacă niciun membru nu declară acea valoare.

Starea cu cast direct reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1110s):

```csharp
Portrait portrait = (Portrait)2;
Console.WriteLine(portrait == Portrait.Hitler);  // True
```

[00:18:30](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1110s) Aici literalul 2 produce aceeași valoare de enumerare ca `Portrait.Hitler`. [00:18:33](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1113s) Cast-ul poate acționa direct asupra unui literal sau asupra altei expresii; nu este necesară o variabilă întreagă intermediară.

[00:18:35](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1115s) Această conversie nu efectuează nicio **validare a apartenenței**, adică nu verifică lista de membri. Ea convertește valoarea numerică la tipul enumerare. [00:18:37](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1117s) Un număr absent din acea listă poate fi totuși stocat.

Starea cu valoare fără nume reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1172s):

```csharp
Portrait portrait = (Portrait)123;
Console.WriteLine(portrait == Portrait.Hitler);  // False
Console.WriteLine(PortraitToString(portrait));   // Atinge ramura default
```

[00:19:32](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1172s) Cast-ul nu produce nicio plângere la timpul compilării doar pentru că 123 nu are nume. [00:19:40](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1180s) În rularea înregistrată, atribuirea reușește, iar eșecul apare mai târziu, când valoarea ajunge pe traseul programat de verificare/eșec. Egalitatea în sine nu provoacă o blocare: produce un rezultat boolean. Diagnosticul sau excepția din logica înconjurătoare este sursa eșecului.

[00:19:41](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1181s) O valoare `Portrait` poate folosi orice valoare reprezentabilă de `int`-ul său de bază; membrii cu nume nu sunt o mulțime închisă, validată automat. [00:19:45](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1185s) De aceea cast-ul numeric ocolește presupunerea că ajung doar portrete cunoscute.

[00:19:45](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1185s) Cast-ul invers extrage întregul de bază:

Starea cu conversie inversă reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1185s):

```csharp
int code = (int)Portrait.Hitler;
Console.WriteLine(code);          // 2
```

[Referința pentru conversiile enumerărilor](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum#conversions) confirmă ambele direcții de conversie și valorile fără nume.

<details>
<summary>Face un parametru de tip Portrait inutilă ramura default?</summary>

Nu. Un apelant poate furniza o valoare fără nume printr-un cast explicit. Un membru cu nume adăugat ulterior poate fi, de asemenea, fără ramură corespunzătoare. Tipul împiedică amestecarea accidentală a categoriilor, dar nu validează completitudinea.

</details>

# Evaluează aritmetica prin conversii întregi

[00:19:51](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1191s) Convertește valorile enumerărilor la `int` pentru a le expune valorile numerice, apoi efectuează aritmetică întreagă obișnuită. **Evaluarea** înseamnă în continuare calcularea fiecărei expresii înainte de a-i folosi rezultatul.

Demonstrația înmulțirii reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1191s):

```csharp
int hitlerCode = (int)Portrait.Hitler;
Console.WriteLine(hitlerCode);    // 2

int product = (int)Portrait.Lenin * (int)Portrait.GeorgeWashington;
Console.WriteLine(product);       // 0
```

[00:20:01](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1201s) Prezice ieșirea urmărind subexpresiile, nu citind numele ca text:

1. [00:20:09](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1209s) Înmulțirea are nevoie de valorile ambelor expresii operanzi. În C#, aceste expresii operanzi sunt evaluate de la stânga la dreapta.
2. [00:20:57](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1257s) `Portrait.Lenin` are valoarea de bază 0.
3. [00:21:05](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1265s) Conversia lui la `int` produce 0.
4. [00:21:11](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1271s) `Portrait.GeorgeWashington` are valoarea de bază 1, deci cast-ul lui produce 1.
5. [00:21:12](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1272s) Înmulțește operanzii calculați: 0 × 1 = 0.

[00:20:55](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1255s) Rezultatul înregistrat este zero, așa cum prezică această evaluare.

[00:21:21](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1281s) Valoarea numerică de rezervă a lui `Portrait.Hitler` este 2, dar nu generaliza aceasta la o conversie implicită a oricărei expresii de enumerare la `int`. Extragerea numerică obișnuită de aici folosește `(int)`. [00:21:23](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1283s) Un rezultat întreg calculat poate fi apoi convertit înapoi la `Portrait`, dacă este nevoie de un rezultat de tip enumerare.

Conversia rezultatului întreg reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1283s):

```csharp
int product = (int)Portrait.Lenin * (int)Portrait.GeorgeWashington;
Portrait result = (Portrait)product;    // Valoare de bază 0
```

[00:21:29](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1289s) Un exemplu mai larg de aritmetică modulară este omis intenționat. Lecția cere tehnica conversiei întregi, nu o implementare a parcurgerii ciclice a valorilor.

[00:21:37](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1297s) Ideea de reținut este reprezentarea numerică. [00:21:56](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1316s) Afirmația că „orice operație întreagă funcționează pe enumerări” are nevoie de o precizare: operațiile întregi arbitrare pot fi efectuate după conversie, dar expresiile de enumerare nu suportează direct toți operatorii întregi. De exemplu, înmulțirea a doi operanzi de enumerare cere convertirea lor mai întâi. [Specificația operațiilor pe enumerări](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/enums#206-enum-values-and-operations) listează operatorii suportați direct.

<details>
<summary>Verifică conversia produsului înapoi la Portrait dacă rezultatul este un portret declarat?</summary>

Nu. Cast-ul doar convertește numărul. Aici 0 corespunde întâmplător lui Lenin; alt calcul ar putea produce o valoare fără nume.

</details>

# Lasă valorile omise să continue de la membrul precedent

[00:22:03](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1323s) Membrii pot omite inițializatorii lor numerici. Fiecare membru omis primește valoarea membrului imediat precedent plus unu.

Starea cu valori omise reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1323s):

```csharp
enum Color
{
    Blue = 0,
    White,       // 1
    Red          // 2
}
```

[00:22:10](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1330s) Aceasta recreează succesiunea explicită 0, 1, 2. [00:22:17](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1337s) Setarea doar a lui Blue la 0 este suficientă pentru a da lui White și Red valorile 1 și 2. [00:22:22](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1342s) Dacă și primul membru își omite inițializatorul, el primește valoarea zero.

[00:22:24](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1344s) Un inițializator explicit începe o succesiune nouă. Formularea comprimată din transcriere despre atribuirea lui 5 și obținerea lui 6 nu trebuie citită ca schimbarea membrului atribuit explicit: membrul scris `= 5` este 5; următorul membru omis este 6.

Demonstrația saltului explicit reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1344s):

```csharp
enum Color
{
    Blue,        // 0
    White = 5,   // 5
    Red          // 6
}
```

[00:22:32](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1352s) Red primește 6 deoarece membrul imediat precedent are valoarea 5. Regula este membrul precedent plus unu, nu numărul membrilor și nu o regulă care să suprascrie o atribuire explicită. Vezi [specificația membrilor de enumerare](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/enums#204-enum-members).

# Folosește o santinelă Count pentru a dimensiona un tablou

[00:22:40](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1360s) Restaurează succesiunea consecutivă pornită de la zero, apoi adaugă un membru final omis, numit `Count`. Acesta este o **santinelă**, un marcaj, nu o culoare reală de lampă.

Starea cu `Count` pentru trei culori reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1360s):

```csharp
enum Color
{
    Blue = 0,
    White,       // 1
    Red,         // 2
    Count        // 3
}
```

[00:22:51](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1371s) În rezultatul demonstrat, Count se evaluează la 3. [00:23:01](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1381s) Aceasta corespunde celor trei culori reale care îl precedă.

[00:23:03](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1383s) Count este derivat din succesiunea numerică a declarației, nu dintr-o dimensiune scrisă separat. [00:23:09](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1389s) Inserează încă o culoare reală înaintea santinelei și devine 4.

Starea cu culoare adăugată reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1389s):

```csharp
enum Color
{
    Blue = 0,
    White,
    Red,
    Yellow,      // 3
    Count        // 4
}
```

[00:23:19](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1399s) Count funcționează pentru că fiecare valoare omisă depinde de precedenta sa. [00:23:21](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1401s) El nu inspectează un tablou, nu reflectează asupra membrilor și nu numără automat declarațiile. Valoarea este egală cu numărul alegerilor reale doar pentru că acestea pornesc de la zero, sunt consecutive și preced santinela. Golurile, valorile de bază duplicate, o pornire diferită de zero sau mutarea lui Count pot rupe această egalitate. Count este el însuși un membru de enumerare declarat, deci codul aplicației trebuie totuși să îl distingă de o culoare utilizabilă.

[00:23:31](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1411s) Înlocuiește dimensiunea hardcodată a tabloului cu valoarea numerică a santinelei. Un **tablou** are un număr fix de elemente când este creat. Dimensiunea dorită aici este un element pentru fiecare culoare reală.

Schimbarea dimensionării tabloului reconstruită din [videoclip](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1411s):

```csharp
// Înainte: un literal trebuie întreținut separat.
int[] entries = new int[3];

// După: folosește convenția culorilor consecutive pornite de la zero.
int[] entriesByColor = new int[(int)Color.Count];
```

[00:23:38](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1418s) Cu această convenție, adăugarea culorii Yellow cere o singură linie de declarație nouă; alocarea care consumă tabloul nu are nevoie de propria editare a dimensiunii. [00:23:55](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1435s) Acest model este util când un tablou are nevoie de câte un element pentru fiecare alegere dintr-o enumerare. [00:24:03](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1443s) Trecerea valorii Count ca lungime a tabloului creează exact atâtea elemente: trei înainte de inserarea lui Yellow și patru după. Cast-ul `(int)` face ca dimensiunea să fie un întreg. În `new int[length]`, `new` creează un tablou de întregi, iar parantezele pătrate îi specifică lungimea; `int[]` este tipul de tablou al variabilei.

<details>
<summary>Dacă lui White i se atribuie explicit 5, mai poate fi Count de încredere ca număr de culori?</summary>

Nu. Valorile omise continuă de la 5, deci santinela urmează succesiunea numerică în loc să numere nume. Restaurează valori consecutive pornite de la zero înainte de a folosi această convenție de dimensionare.

</details>

# Domeniu de aplicare și practică

Lecția construiește o reprezentare și o funcție de conversie, în loc să rezolve regulile complete de dezarmare a bombei. [00:04:32](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=272s) Conversia în text este amânată pentru scurt, apoi implementată. [00:11:51](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=711s) Sintaxa `switch` mai complicată este amânată. [00:21:29](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1289s) Aplicarea aritmeticii modulare este omisă; tehnica cerută este conversia între o enumerare și întregul ei de bază.

Pentru practică dincolo de exemplul lucrat din videoclip, folosește [laboratorul de `enum`](../../../labs/1_basic/09_enum.md). Acesta îți cere să modelezi un semafor, să mapezi fiecare culoare la o acțiune, să afișezi acțiunea, să avansezi starea și să demonstrezi parcurgerea ciclică. Este un exercițiu separat, nu cod implementat în acest videoclip. Păstrează etichetele de afișare separate de numele membrilor enumerării și reține că un cast nu validează o alegere.

# Istoricul modificărilor de cod

Acestea sunt stări stabile din videoclip, cu marcaje de timp, în ordinea construirii. Istoricul local inspectat nu furnizează nicio sursă commituită pentru aceste stări exacte; niciun commit fără legătură, care adaugă exerciții, nu este substituit.

- [00:00:28](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=28s) — Reprezintă portretul curent cu un șir.
- [00:01:32](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=92s) — Mută verificarea înaintea reatribuirii, ca să observe valoarea anterioară.
- [00:03:46](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=226s) — Înlocuiește textul portretului cu coduri întregi.
- [00:05:15](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=315s) — Introduce funcția personalizată de transformare a numărului în etichetă.
- [00:07:00](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=420s) — Returnează o etichetă din ramura selectată.
- [00:08:02](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=482s) — Expune valorile neașteptate printr-un diagnostic de dezvoltare și o valoare de rezervă.
- [00:10:10](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=610s) — Denumește codurile portretelor cu constante.
- [00:10:55](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=655s) — Înlocuiește comparațiile repetate cu un `switch`.
- [00:12:33](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=753s) — Introduce enumerarea `Portrait`, apoi o enumerare `Color` separată.
- [00:14:02](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=842s) — Schimbă parametrul conversiei în `Portrait`.
- [00:14:47](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=887s) — Extinde enumerarea și demonstrează cazul lipsă din conversie.
- [00:15:34](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=934s) — Compară ieșirea încorporată după numele membrului cu etichetele explicite de afișare.
- [00:18:27](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1107s) — Face cast unui număr la `Portrait`, fără validarea apartenenței.
- [00:19:51](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1191s) — Extrage valori întregi pentru afișare și aritmetică.
- [00:22:03](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1323s) — Lasă valorile membrilor omise să continue succesiunea numerică.
- [00:22:40](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1360s) — Adaugă o santinelă `Count`.
- [00:23:31](https://www.youtube.com/watch?v=LLfU1a7pTxg&t=1411s) — Înlocuiește o lungime literală de tablou cu valoarea `Count`.