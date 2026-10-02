> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# De la fișierele sursă la apelurile de funcție

[00:00:02](https://www.youtube.com/watch?v=jLWY_id6nXU&t=2s) Urmărim un mic program de consolă C# de la fișierele sale sursă până la rezultatul compilat, apoi urmărim execuția prin instrucțiuni individuale și prin grupuri denumite de instrucțiuni. Scopul este să explicăm atât de unde provine programul care poate fi rulat, cât și de ce acțiunile sale se desfășoară într-o anumită ordine.

[Laboratorul proiect de bază](../../../labs/1_basic/02_project.md) oferă practică conexă. [Laboratorul Git](../../../labs/1_basic/03_git.md) tratează separat inițializarea depozitului; această înregistrare explică doar de ce folderele generate ar trebui ignorate.

Vocabularul necesar parcurgerii este:

- **Fișier sursă și configurare de proiect:** un fișier sursă conține text C#, în mod normal într-un fișier `.cs`; fișierul `.csproj` descrie cum se construiește proiectul. **SDK-ul .NET** furnizează instrumentele de dezvoltare, inclusiv **compilatorul**, care traduce sursa în cod de program compilat.
- **Compilare și build:** compilarea traduce codul; un build efectuează toate lucrările necesare producerii rezultatului, inclusiv pregătirea și compilarea. **Lansarea** înseamnă pornirea acelui rezultat.
- **Instrucțiune de nivel superior, clasă și metodă de intrare:** o instrucțiune de nivel superior este cod executabil scris direct în fișierul sursă principal. Compilatorul o pune într-o clasă generată, un container pentru membrii programului, și într-o metodă de intrare generată, rutina prin care programul pornește. Implementarea lor este amânată.
- **Instrucțiune (statement):** o singură acțiune executabilă din programul sursă. Cursul folosește termenul „instrucțiune”; termenul din engleză este „statement”. Exemplele pun câte o instrucțiune pe fiecare linie, dar o instrucțiune și o linie fizică de text nu sunt întotdeauna același lucru.
- **Consolă și `Console.WriteLine`:** consola este interfața terminală de intrare/ieșire; `Console.WriteLine` afișează o valoare urmată de un rând nou.
- **DLL, bibliotecă, executabil și runtime:** o DLL este un assembly .NET compilat; o bibliotecă furnizează cod reutilizabil altor programe; un executabil este un fișier rezultat pe care îl poți lansa. **Runtime-ul .NET** oferă mediul de execuție pentru cod .NET compilat. Faptul că poate fi lansat separat nu dovedește că o aplicație a fost distribuită cu propriul runtime.
- **`bin`, `obj` și artefacte de build:** artefactele sunt fișiere generate. `bin` conține rezultatul buildului; `obj` conține datele intermediare de build. Un **cache** stochează lucrări reutilizabile. Un **pachet** furnizează software reutilizabil; o **dependență** este software de care proiectul are nevoie.
- **`Debug` și framework țintă:** `Debug` este configurația de build folosită aici; framework-ul țintă identifică versiunea .NET către care proiectul este direcționat, prezentată în înregistrare ca `net9.0`.
- **Cale completă și operator de apel:** o cale completă pornește de la unitatea de disc sau rădăcina sistemului de fișiere, astfel încât terminalul să poată găsi fișierul independent de folderul curent. Exemplul PowerShell folosește operatorul său de apel, `&`, pentru a porni executabilul desemnat de o cale între ghilimele.
- **Depozit Git și `.gitignore`:** un depozit înregistrează istoricul surselor. `.gitignore` listează căi pe care Git ar trebui să le lase deoparte când analizează fișierele neversionate.
- **Execuție secvențială și procesor:** procesorul execută lucrul programului. Execuția secvențială înseamnă că acțiunile din secvență se termină pe rând, înainte ca următoarea să înceapă.
- **`Thread.Sleep(1000)`:** un apel care pune în pauză threadul curent, adică secvența de execuție urmărită, pentru 1.000 de milisecunde, adică o secundă.
- **Funcție sau metodă, definiție și corp:** o funcție este o secvență denumită de instrucțiuni. Definiția ei îi dă un nume și un corp, instrucțiunile dintre `{` și `}`. Înregistrarea folosește și cuvântul „metodă”.
- **`void`, paranteze, acolade și punct-virgulă:** `void` este cuvântul-cheie aflat înaintea numelor de funcții prezentate aici, nu un nume al funcției în sine. Parantezele `()` apar în definiții și apeluri; acoladele `{}` închid un corp; punct-virgula `;` încheie instrucțiunile prezentate aici. Sintaxa detaliată a declarațiilor și semnificația lui `void` ca tip de retur sunt amânate.
- **Apel, apelant, întoarcere și apel imbricat:** un apel pornește corpul unei funcții; apelantul este codul care face acel apel. Întoarcerea reia apelantul după apel. Un apel imbricat are loc când corpul unei funcții apelează alta.

Exemplele de mai jos sunt reconstrucții reduse ale acțiunilor înregistrate, nu afirmații despre stări exacte ale editorului. Linkurile către video identifică acele stări. Exemplele de depozit sunt marcate explicit ca fiind conexe atunci când nu sunt proiectul înregistrat.

## Fișierele sursă și de proiect

[00:00:02](https://www.youtube.com/watch?v=jLWY_id6nXU&t=2s) Un **fișier sursă** este textul pe care îl citește compilatorul. Proiectul existent începe cu un singur fișier sursă C#, `Program.cs`, alături de **configurația de proiect**, fișierul `.csproj`. În acest proiect obișnuit cu SDK .NET, fișierele C# sunt incluse automat; nu este nevoie să înregistrezi fiecare nume de fișier sursă.

[00:00:10](https://www.youtube.com/watch?v=jLWY_id6nXU&t=10s) Codul inițial poate fi la fel de mic ca această **instrucțiune de nivel superior**, scrisă direct în fișierul sursă:

```csharp
// Program.cs
Console.WriteLine("Hello world!");
```

**Compilatorul** traduce aceste instrucțiuni și le plasează într-o clasă și o metodă de intrare generate. Această transformare este menționată mai târziu în parcurgere, la [00:03:17](https://www.youtube.com/watch?v=jLWY_id6nXU&t=197s); nu presupune scrierea manuală a clasei sau a metodei de intrare.

[00:00:25](https://www.youtube.com/watch?v=jLWY_id6nXU&t=25s) Verifică dacă fișierul sursă trebuie să rămână în rădăcina proiectului:

1. Creează un subfolder în interiorul proiectului.
2. Mută fișierul sursă existent în el, lăsând configurația de proiect în rădăcină.
3. [00:00:33](https://www.youtube.com/watch?v=jLWY_id6nXU&t=33s) Compilează și lansează din nou. Programul rulează în continuare.

Mutarea schimbă locația, nu conținutul sursei:

```text
Before:                   After:
project/                  project/
  project.csproj            project.csproj
  Program.cs                nested/
                              Program.cs
```

[00:00:36](https://www.youtube.com/watch?v=jLWY_id6nXU&t=36s) Atât fișierele `.cs` din rădăcină, cât și cele din subfoldere sunt găsite de includerea implicită a surselor din proiect. Acest exemplu mută unicul fișier; nu păstrează două copii ale instrucțiunilor de pornire. Regula de includere implicită nu înseamnă nici că orice configurație de proiect posibilă trebuie să includă orice fișier: o configurație explicită poate schimba implicitările.

Pentru un context de depozit verificat, [această configurație de proiect SDK](https://github.com/AntonC9018/uniCourse_csharp/blob/c79a3523fbee1e5fc4916628888cf269c26a0f67/examples/builder/pipeline.csproj) este un **exemplu conex**, nu configurația proiectului înregistrat. Acest fragment scurt identifică SDK-ul, rezultatul executabil și framework-ul țintă fără să enumere numele fișierelor sursă:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
  </PropertyGroup>
</Project>
```

## Rezultatul buildului și lansările separate

[00:00:45](https://www.youtube.com/watch?v=jLWY_id6nXU&t=45s) Un **build** creează automat un folder `bin`. Aceasta este destinația rezultatului compilat, care poate include o **DLL**, un assembly compilat care poate servi drept **bibliotecă** de cod pentru alte programe, și un **executabil**, fișierul folosit pentru a porni programul. Al doilea folder generat, `obj`, stochează date intermediare, nu rezultatul pe care îl lansezi în mod normal.

[00:01:07](https://www.youtube.com/watch?v=jLWY_id6nXU&t=67s) **Compilarea** și **lansarea** sunt acțiuni separate chiar și atunci când o singură comandă le cere pe amândouă:

```powershell
dotnet run
```

Proiectul este construit după necesitate, apoi programul rezultat pornește. Rezultatul rămâne pe disc; nu este eliminat când programul se termină.

[00:01:14](https://www.youtube.com/watch?v=jLWY_id6nXU&t=74s) Lansează independent acel rezultat existent:

1. Localizează executabilul sub `bin`.
2. Copiază **calea completă**, inclusiv unitatea de disc, folderele și numele fișierului.
3. [00:01:20](https://www.youtube.com/watch?v=jLWY_id6nXU&t=80s) Introdu calea respectivă în terminal.
4. [00:01:23](https://www.youtube.com/watch?v=jLWY_id6nXU&t=83s) Observă din nou rularea programului.

De exemplu, în PowerShell operatorul de apel `&` lansează o cale de executabil între ghilimele. Numele proiectului și folderele de aici sunt substituenți:

```powershell
& "C:\path\to\project\bin\Debug\net9.0\project.exe"
```

[00:01:28](https://www.youtube.com/watch?v=jLWY_id6nXU&t=88s) Aceasta arată că programul compilat poate fi lansat separat de comanda de dezvoltare. **Runtime-ul .NET** furnizează în continuare mediul necesar pentru a executa codul său compilat. „Rulează singur” înseamnă aici că nu mai este necesară o altă compilare a surselor; nu dovedește o distribuție autonomă cu runtime inclus.

[00:01:36](https://www.youtube.com/watch?v=jLWY_id6nXU&t=96s) Pentru a crea rezultatul fără a-l porni, folosește comanda de build:

```powershell
dotnet build
```

[00:01:46](https://www.youtube.com/watch?v=jLWY_id6nXU&t=106s) Rezultatul este un rezultat de compilare/build, nu ieșirea tipărită a programului: `dotnet build` nu lansează aplicația. Acest lucru este util când producerea binarului și rularea lui sunt pași diferiți.

[00:01:51](https://www.youtube.com/watch?v=jLWY_id6nXU&t=111s) Uită-te în `bin/Debug/net9.0` în proiectul înregistrat. **Debug** este configurația; folderul **framework-ului țintă** indică versiunea .NET. Instrucțiunea generică „uită-te în bin/debug” este o prescurtare pentru această cale mai completă, ale cărei nume de fișier specifice proiectului depind de proiect:

```text
bin/
  Debug/
    net9.0/
      project.exe
      project.dll
      ...
```

<details>
<summary>De ce poate rula executabilul fără a folosi din nou comanda de build și rulare?</summary>

Buildul anterior a tradus deja sursele și a salvat rezultatul compilat. Lansarea acelui rezultat este o acțiune separată. Ea folosește în continuare mediul de runtime necesar.
</details>

## Artefacte de build de unică folosință

[00:01:57](https://www.youtube.com/watch?v=jLWY_id6nXU&t=117s) **Artefactele de build** sunt fișierele produse de build. Folderul `obj` conține artefacte intermediare și informații de build memorate în cache, inclusiv informații despre **pachete** și **dependențe**, software-ul folosit de proiect. Aceasta este distinct de rezultatul lansabil din `bin`; datele legate de pachete din `obj` nu înseamnă că fiecare pachet instalat este stocat exclusiv acolo.

[00:02:16](https://www.youtube.com/watch?v=jLWY_id6nXU&t=136s) Un **cache** poate fi recreat deoarece intrările sale există în continuare. Înregistrarea verifică această proprietate:

1. [00:02:19](https://www.youtube.com/watch?v=jLWY_id6nXU&t=139s) Șterge folderele generate `bin` și `obj`.
2. [00:02:25](https://www.youtube.com/watch?v=jLWY_id6nXU&t=145s) Construiește din nou din sursele și configurația de proiect neschimbate.
3. [00:02:28](https://www.youtube.com/watch?v=jLWY_id6nXU&t=148s) Observă reapariția folderelor și fișierelor generate.

Pasul de reconstruire este:

```powershell
dotnet build
```

Demonstrația restaurează aceleași fișiere intermediare și rezultate necesare. Ideea este reproductibilitatea din aceleași intrări, nu garanția că timestamp-urile sau fiecare octet al fiecărui artefact trebuie să coincidă întotdeauna. Păstrează sursele și configurația de proiect; acestea sunt ceea ce face posibilă regenerarea.

## Ignorarea fișierelor reproductibile

[00:02:31](https://www.youtube.com/watch?v=jLWY_id6nXU&t=151s) Un **depozit Git** ar trebui să înregistreze intrările necesare reproducerii programului. Stocarea rezultatului de build de unică folosință ar adăuga fișiere care pot fi regenerate din configurație și surse.

[00:02:43](https://www.youtube.com/watch?v=jLWY_id6nXU&t=163s) Pune numele folderelor generate în **`.gitignore`**, fișierul care exclude din urmărirea Git obișnuită căile neversionate care se potrivesc. Regula minimală a lecției este ilustrată de acest fragment scurt din [fișierul de ignorare al depozitului](https://github.com/AntonC9018/uniCourse_csharp/blob/c70666b5a2c1f3be23798f9a90d2952249a938a4/.gitignore) verificat, un **exemplu de depozit conex**, nu fișierul înregistrat:

```gitignore
bin
obj
```

[00:02:53](https://www.youtube.com/watch?v=jLWY_id6nXU&t=173s) Ignoră ambele foldere, astfel încât conținutul lor generat să rămână în afara istoricului surselor. Aceste reguli împiedică adăugarea normală a rezultatelor neversionate noi; adăugarea unei reguli de ignorare nu elimină singură fișierele pe care Git le urmărește deja. Inițializarea depozitului și celelalte comenzi Git aparțin [laboratorului Git](../../../labs/1_basic/03_git.md) separat.

## Codul de pornire și scopul lecției

[00:02:58](https://www.youtube.com/watch?v=jLWY_id6nXU&t=178s) De aici încolo, concentrează-te pe **fișierul sursă principal**: fișierul care conține codul executat la pornirea programului. O **instrucțiune de nivel superior** este scrisă direct în acel fișier, în afara unei clase sau metode scrise explicit:

```csharp
Console.WriteLine("Hello world!");
```

[00:03:17](https://www.youtube.com/watch?v=jLWY_id6nXU&t=197s) Compilatorul furnizează o **clasă**, un container pentru membrii programului, și o **metodă de intrare**, rutina prin care începe execuția. Structura lor completă este lăsată pentru mai târziu. Această lecție studiază secvența vizibilă și funcțiile scrise în fișierul principal, nu implementarea generată.

[00:05:11](https://www.youtube.com/watch?v=jLWY_id6nXU&t=311s) O a doua limită apare când apar funcțiile: sintaxa detaliată a declarației de funcție, în special semnificația lui `void`, este amânată deliberat. Sarcina imediată este să înțelegi cum se execută un corp denumit când este apelat.

## Instrucțiunile se execută în ordinea din sursă

[00:03:28](https://www.youtube.com/watch?v=jLWY_id6nXU&t=208s) O **instrucțiune** (`statement`), numită **instrucțiune** pe tot parcursul cursului, efectuează o acțiune. Aici instrucțiunile de afișare ocupă linii separate. „O linie” este o descriere comodă a acestor exemple, nu o regulă generală de gramatică: o instrucțiune poate cuprinde mai multe linii.

[00:03:37](https://www.youtube.com/watch?v=jLWY_id6nXU&t=217s) Extinde salutul inițial cu mai multe acțiuni de afișare. **`Console.WriteLine`** trimite o valoare și un rând nou către **consolă**, interfața de ieșire a terminalului. Această versiune redusă face ordinea din sursă ușor de urmărit:

```csharp
Console.WriteLine("Hello world!");
Console.WriteLine(1);
Console.WriteLine(2);
Console.WriteLine(3);
```

[00:03:47](https://www.youtube.com/watch?v=jLWY_id6nXU&t=227s) Pornirea programului rulează instrucțiunile în ordinea în care au fost scrise. [00:03:53](https://www.youtube.com/watch?v=jLWY_id6nXU&t=233s) Ieșirea observată urmează această ordine:

```text
Hello world!
1
2
3
```

[00:04:01](https://www.youtube.com/watch?v=jLWY_id6nXU&t=241s) Fiecare instrucțiune are ceva de făcut; în acest exemplu acțiunea este afișarea. [00:04:16](https://www.youtube.com/watch?v=jLWY_id6nXU&t=256s) **Execuția secvențială** înseamnă că acțiunea curentă se încheie înainte ca execuția să treacă la următoarea. **Procesorul** efectuează lucrul programului; în această secvență de execuție simplă, acțiunea de afișare ulterioară nu o depășește pe cea anterioară.

[00:04:22](https://www.youtube.com/watch?v=jLWY_id6nXU&t=262s) Odată terminată instrucțiunea de salut, execuția trece la instrucțiunea următoare, apoi la cea de după ea. [00:04:36](https://www.youtube.com/watch?v=jLWY_id6nXU&t=276s) Dacă o acțiune durează mult, acțiunile următoare o așteaptă. Această descriere privește programul secvențial din lecție; nu este o afirmație despre toate programele concurente.

<details>
<summary>Prezică ieșirea dacă instrucțiunea care afișează 3 este mutată deasupra celei care afișează 1.</summary>

După salut, valorile se afișează în noua lor ordine din sursă: 3, apoi 1, apoi 2. Instrucțiunile nu își sortează valorile.
</details>

## Cum faci vizibilă o întârziere

[00:04:40](https://www.youtube.com/watch?v=jLWY_id6nXU&t=280s) Fă regula de așteptare observabilă inserând o întârziere între acțiunile de afișare. **`Thread.Sleep(1000)`** pune în pauză **threadul** curent, adică secvența de execuție urmărită, pentru 1.000 de milisecunde: o secundă. O versiune redusă a modificării este:

```csharp
Console.WriteLine("Hello world!");
Thread.Sleep(1000);
Console.WriteLine(1);
Thread.Sleep(1000);
Console.WriteLine(2);
Thread.Sleep(1000);
Console.WriteLine(3);
```

[00:04:55](https://www.youtube.com/watch?v=jLWY_id6nXU&t=295s) Rularea înregistrată arată ieșire sosind cu pauze de o secundă. [00:04:57](https://www.youtube.com/watch?v=jLWY_id6nXU&t=297s) Cât timp un apel de pauză nu s-a terminat, instrucțiunea de afișare următoare nu a început. [00:05:04](https://www.youtube.com/watch?v=jLWY_id6nXU&t=304s) Când întârzierea se încheie, acel apel este considerat finalizat, iar execuția continuă.

Ordinea rămâne aceeași; pauzele adăugate expun timpul petrecut în așteptare. Exemplele ulterioare omit întârzierile pentru a păstra lizibilitatea căilor de apel. Aceeași regulă de finalizare-înainte-de-continuare se aplică și dacă întârzierile rămân într-un corp de funcție.

## Definirea unei secvențe denumite

[00:05:11](https://www.youtube.com/watch?v=jLWY_id6nXU&t=311s) O **funcție**, numită și **metodă** în înregistrare, dă mai multor instrucțiuni un nume. **Definiția** ei precizează numele și **corpul**, secvența cuprinsă între acolade.

[00:05:18](https://www.youtube.com/watch?v=jLWY_id6nXU&t=318s) Motivul introducerii ei acum este practic: grupează acțiuni, astfel încât un singur nume să le poată cere pe toate. Nu lăsa sintaxa declarației să te distragă de acest model de execuție. Forma înregistrată, cu un corp inițial gol, este:

```csharp
void Example()
{
    // The grouped statements will go here.
}
```

[00:05:20](https://www.youtube.com/watch?v=jLWY_id6nXU&t=320s) **`void`** este cuvântul-cheie aflat înaintea numelui în această definiție. Nu este numele listei de instrucțiuni și nu declară singură o funcție completă. **Parantezele** urmează după `Example`; **acoladele** închid corpul. Semnificația detaliată a lui `void` ca tip de retur este în afara explicației lecției.

[00:05:37](https://www.youtube.com/watch?v=jLWY_id6nXU&t=337s) Denumirea listei `Example` face posibilă cererea instrucțiunilor sale mai târziu. Acea cerere este un **apel**, scris aici cu numele și parantezele. **Punct-virgula**, `;`, încheie această instrucțiune de apel:

```csharp
Example();
```

Definiția spune ce conține secvența. Apelul spune când se rulează.

## Intrarea într-o funcție și revenirea

[00:05:46](https://www.youtube.com/watch?v=jLWY_id6nXU&t=346s) Mută cele trei acțiuni de afișare în corpul lui `Example`. Păstrează instrucțiunile obișnuite de pornire în afara lui. [00:05:57](https://www.youtube.com/watch?v=jLWY_id6nXU&t=357s) Afișează `Anton`, apelează lista și afișează din nou `Anton` după aceea. Această reconstrucție redusă păstrează vizibile salutul și cele trei acțiuni:

```csharp
Console.WriteLine("Hello world!");
Console.WriteLine("Anton");
Example();
Console.WriteLine("Anton");

void Example()
{
    Console.WriteLine(1);
    Console.WriteLine(2);
    Console.WriteLine(3);
}
```

[00:06:14](https://www.youtube.com/watch?v=jLWY_id6nXU&t=374s) Rularea trece prin acțiunile de afișare din exterior, prin acțiunile grupate și apoi prin instrucțiunea următoare din exterior. Cele două afișări ale lui `Anton` încadrează apelul în această reconstrucție; mutarea celor trei instrucțiuni cu numere în funcție nu pune acele afișări înconjurătoare în corpul ei.

[00:06:24](https://www.youtube.com/watch?v=jLWY_id6nXU&t=384s) Un **apel** transferă temporar execuția într-o funcție. **Apelantul** este codul care face acea cerere. La apel, nu trece imediat la următoarea instrucțiune din exterior:

1. [00:06:42](https://www.youtube.com/watch?v=jLWY_id6nXU&t=402s) Intră în prima instrucțiune a corpului apelat.
2. [00:06:47](https://www.youtube.com/watch?v=jLWY_id6nXU&t=407s) Rulează instrucțiunile acelui corp de sus în jos, ca în secvența de pornire.
3. [00:07:03](https://www.youtube.com/watch?v=jLWY_id6nXU&t=423s) După ce ultima sa instrucțiune se finalizează, funcția se termină.
4. [00:07:10](https://www.youtube.com/watch?v=jLWY_id6nXU&t=430s) **Revino** la apelant și continuă cu instrucțiunea de după apel.

Pentru exemplul redus, rezultatul este:

```text
Hello world!
Anton
1
2
3
Anton
```

Revenirea „la apel” descrie reluarea continuării sale. Nu înseamnă că același apel este executat din nou automat. Apelul este finalizat doar după ce corpul său se termină.

## Rularea aceluiași corp din nou

[00:07:23](https://www.youtube.com/watch?v=jLWY_id6nXU&t=443s) Adaugă un alt apel după instrucțiunea următoare din exterior. Fiecare apel pornește același corp de la prima sa instrucțiune; al doilea apel nu reia de la mijlocul primei rulări:

```csharp
Console.WriteLine("Anton");
Example();
Console.WriteLine("Anton");
Example();

void Example()
{
    Console.WriteLine(1);
    Console.WriteLine(2);
    Console.WriteLine(3);
}
```

[00:07:31](https://www.youtube.com/watch?v=jLWY_id6nXU&t=451s) Traseul intră din nou în corp și repetă toate cele trei acțiuni. Rezultatul pentru această secvență de pornire scurtată este:

```text
Anton
1
2
3
Anton
1
2
3
```

[00:07:44](https://www.youtube.com/watch?v=jLWY_id6nXU&t=464s) Finalizarea ultimei instrucțiuni a corpului te întoarce la codul care a făcut al doilea apel. [00:07:49](https://www.youtube.com/watch?v=jLWY_id6nXU&t=469s) Dacă nu mai există instrucțiuni de pornire, programul se termină. Nu execută definiția de funcție apropiată ca pe încă o acțiune.

<details>
<summary>Înseamnă revenirea din primul apel că corpul nu mai poate rula din nou?</summary>

Nu. Un apel ulterior pornește o nouă execuție a aceluiași corp, de la începutul său. Fiecare apel are propria sa continuare în apelant.
</details>

## Definiții și nume

[00:07:58](https://www.youtube.com/watch?v=jLWY_id6nXU&t=478s) O **definiție** face disponibil un corp denumit; nu este o instrucțiune care să ruleze acel corp doar pentru că execuția ajunge la locul unde a fost scrisă. Apelele determină dacă și când se întâmplă acțiunile sale.

[00:08:09](https://www.youtube.com/watch?v=jLWY_id6nXU&t=489s) Redenumește `Example` în `Hello` și actualizează apelele. Cuvântul-cheie rămâne `void`; partea schimbată este numele funcției. Această stare redusă are aceleași acțiuni grupate:

```csharp
Hello();

void Hello()
{
    Console.WriteLine(1);
    Console.WriteLine(2);
    Console.WriteLine(3);
}
```

[00:08:21](https://www.youtube.com/watch?v=jLWY_id6nXU&t=501s) Ieșirea nu se schimbă pentru o secvență de apeluri echivalentă, deoarece instrucțiunile din corp nu s-au modificat. `Hello` îl înlocuiește pe `Example`, nu pe `void`. Este posibil să alegi alt nume valid, cu condiția ca apelele să folosească și ele acel nume.

[00:08:32](https://www.youtube.com/watch?v=jLWY_id6nXU&t=512s) Un program poate defini mai multe funcții, fiecare cu instrucțiuni diferite. Experimentul următor separă această posibilitate de a defini un corp de decizia de a-l apela.

## Funcții nefolosite și apeluri explicite

[00:08:43](https://www.youtube.com/watch?v=jLWY_id6nXU&t=523s) Adaugă încă o listă denumită fără să o apelez. Noul corp nu are niciun efect asupra ieșirii de pornire. În acest exemplu redus există un corp suplimentar de afișare, dar secvența principală nu îl cere niciodată:

```csharp
Hello();
Console.WriteLine("Anton");

void Hello()
{
    Console.WriteLine(1);
    Console.WriteLine(2);
    Console.WriteLine(3);
}

void PrintAnton()
{
    Console.WriteLine("Anton");
}
```

[00:09:01](https://www.youtube.com/watch?v=jLWY_id6nXU&t=541s) Înregistrarea verifică că adăugarea unei liste nefolosite nu adaugă ieșirea ei: ieșirea relevantă `Anton` este văzută o dată, nu de două ori. Un diagnostic de editor sau de build despre o funcție nefolosită este diferit de ieșirea produsă prin rularea instrucțiunilor sale.

[00:09:14](https://www.youtube.com/watch?v=jLWY_id6nXU&t=554s) Pentru a folosi noua funcție, adaugă explicit un **apel**. Pentru exemplul redus, adaugă această instrucțiune la secvența de pornire, înainte de definiții:

```csharp
PrintAnton();
```

[00:09:35](https://www.youtube.com/watch?v=jLWY_id6nXU&t=575s) Acum rulează și acțiunile funcției. Exemplul scurt afișează cele trei numere, apoi `Anton` din instrucțiunea exterioară, apoi `Anton` din apelul adăugat. Când acel apel se finalizează, execuția reia la următoarea instrucțiune de pornire, dacă există. Definirea corpului a făcut posibil apelul; adăugarea apelului a provocat execuția.

<details>
<summary>De ce adăugarea unei a doua definiții singure nu adaugă o a doua afișare?</summary>

O definiție furnizează instrucțiuni disponibile. Nu cere execuția lor. Corpul suplimentar rulează doar atunci când un cod executat îl apelează.
</details>

## Apeluri în corpurile funcțiilor

[00:09:39](https://www.youtube.com/watch?v=jLWY_id6nXU&t=579s) Un **apel imbricat** este un apel făcut în corpul altei funcții. Nu există o regulă specială pentru el: corpul apelat rulează până la finalizare înainte ca apelantul să poată continua.

[00:09:52](https://www.youtube.com/watch?v=jLWY_id6nXU&t=592s) Traseul intră în `Hello`, îi rulează cele trei acțiuni, apoi ajunge la un apel către altă listă. O reconstrucție redusă arată această direcție a imbricării; numele celei de-a doua liste și valorile afișate aici sunt ilustrative:

```csharp
Hello();

void Hello()
{
    Console.WriteLine(1);
    Console.WriteLine(2);
    Console.WriteLine(3);
    Other();
}

void Other()
{
    Console.WriteLine("A");
    Console.WriteLine("B");
}
```

Urmește fiecare transfer, în loc să te uiți doar pe tot fișierul de sus în jos:

1. Apelul de pornire intră în `Hello`.
2. [00:09:58](https://www.youtube.com/watch?v=jLWY_id6nXU&t=598s) Cele trei instrucțiuni de afișare ale sale se finalizează în ordine.
3. [00:10:07](https://www.youtube.com/watch?v=jLWY_id6nXU&t=607s) Ultimul său apel intră în `Other`, pornind de la prima instrucțiune a acelui corp.
4. [00:10:10](https://www.youtube.com/watch?v=jLWY_id6nXU&t=610s) Cele două instrucțiuni din corpul interior se finalizează.
5. [00:10:18](https://www.youtube.com/watch?v=jLWY_id6nXU&t=618s) Execuția revine la locul apelului din `Hello`.
6. [00:10:25](https://www.youtube.com/watch?v=jLWY_id6nXU&t=625s) Deoarece aceea era ultima instrucțiune a lui `Hello`, și `Hello` se termină.
7. [00:10:33](https://www.youtube.com/watch?v=jLWY_id6nXU&t=633s) Execuția revine din nou, de această dată la apelul de pornire. Cum nu mai este nimic după el, programul se încheie.

Descrierea mai scurtă „o funcție îl apelează pe `Hello`” funcționează la fel de bine: inversează care corp conține apelul, iar apelul intră întâi în `Hello`, îi afișează cele trei acțiuni în ordine și revine la acel apelant. Contează locul apelului și continuarea sa, nu care nume este cel exterior.

Revenirea are loc în continuare chiar și când nu există instrucțiunea următoare. Ea este modul în care finalizarea corpului interior îi permite corpului înconjurător, apoi secvenței de pornire, să se termine. Un apel imbricat final nu este un motiv să sari peste apelant.

## Urmărirea întregului traseu de execuție

[00:10:41](https://www.youtube.com/watch?v=jLWY_id6nXU&t=641s) Parcurgerea finală urmărește o altă instrucțiune și un alt apel după revenire. Pentru a face vizibile atât revenirea intermediară, cât și sfârșitul inevitabil, această variație redusă adaugă o continuare în codul de pornire:

```csharp
Hello();
Console.WriteLine("After the first call");
Hello();

void Hello()
{
    Console.WriteLine(1);
    Console.WriteLine(2);
    Console.WriteLine(3);
    Other();
}

void Other()
{
    Console.WriteLine("A");
    Console.WriteLine("B");
}
```

Folosește aceeași procedură la fiecare nivel:

1. [00:10:41](https://www.youtube.com/watch?v=jLWY_id6nXU&t=641s) După ce un apel se finalizează, continuă cu instrucțiunea următoare din apelantul său. Aici se afișează mesajul dintre cele două apeluri.
2. [00:11:01](https://www.youtube.com/watch?v=jLWY_id6nXU&t=661s) La un apel de funcție, intră în prima instrucțiune a corpului denumit. Al doilea apel de pornire pornește `Hello` de la început.
3. [00:11:08](https://www.youtube.com/watch?v=jLWY_id6nXU&t=668s) Execută instrucțiunile acelui corp în ordine, finalizând fiecare înainte de a începe următoarea; un apel imbricat urmează aceeași regulă.
4. [00:11:16](https://www.youtube.com/watch?v=jLWY_id6nXU&t=676s) Când ultima instrucțiune a corpului se termină, revino la locul apelului. Dacă ultima instrucțiune a fost tot un apel, finalizarea sa permite și corpului înconjurător să se termine.
5. [00:11:25](https://www.youtube.com/watch?v=jLWY_id6nXU&t=685s) Un corp care există doar ca definiție nu contribuie cu nicio acțiune, dacă nu este apelat undeva pe traseul executat.

<details>
<summary>Prezică de câte ori se afișează A și explică de ce definițiile nu adaugă rulări suplimentare.</summary>

A se afișează de două ori. Fiecare apel de pornire intră în `Hello`; fiecare execuție a lui `Hello` îl apelează o singură dată pe `Other`. `Other` afișează A și B și revine la `Hello`, care revine la secvența de pornire. Cele două definiții doar își fac corpurile disponibile.
</details>

[00:11:16](https://www.youtube.com/watch?v=jLWY_id6nXU&t=676s) „Revino la linia cu apelul” și „continuă după apel” descriu două părți ale aceluiași eveniment: controlul revine la acel apelant, apelul este acum finalizat și următoarea instrucțiune poate începe. La ultimul apel de pornire nu există instrucțiune următoare, așa că urmează terminarea.

## Practică opțională dincolo de înregistrare

[Laboratorul proiect de bază](../../../labs/1_basic/02_project.md) adaugă experimente precum suprimarea buildului înainte de lansare și păstrarea unei ferestre de consolă deschise pentru citire. Acestea sunt extensii de laborator, nu teste înregistrate în această lecție.

Ulteriorul [laborator despre funcții cu parametri](../../../labs/1_basic/05_functions.md) studiază intrările și rezultatele funcțiilor și sensul complet al lui `void`. Acele detalii de declarație, precum și clasa și metoda de intrare generate de compilator, rămân în afara scopului didactic al acestui videoclip. Înțelegerea secvenței de apel și revenire de aici nu depinde de parcurgerea acelui studiu suplimentar.

## Istoricul codului înregistrat

Niciun fișier sursă versionat verificat nu reproduce acest program demonstrativ temporar. Stările legate de construcție sunt oferite de linkurile către video; citările conexe de mai sus, la proiect și la fișierul de ignorare, nu sunt commit-uri din istoricul acelui program.

- [00:00:10](https://www.youtube.com/watch?v=jLWY_id6nXU&t=10s) — Pornim de la un singur fișier sursă C# care conține cod de pornire.
- [00:00:25](https://www.youtube.com/watch?v=jLWY_id6nXU&t=25s) — Mutăm sursa într-un subfolder; compilarea și lansarea reușesc în continuare.
- [00:01:07](https://www.youtube.com/watch?v=jLWY_id6nXU&t=67s) — Construim și pornim împreună, lăsând rezultatul compilat pe disc.
- [00:01:14](https://www.youtube.com/watch?v=jLWY_id6nXU&t=74s) — Lansăm executabilul existent prin calea sa completă.
- [00:01:46](https://www.youtube.com/watch?v=jLWY_id6nXU&t=106s) — Construim separat, fără a porni programul.
- [00:02:19](https://www.youtube.com/watch?v=jLWY_id6nXU&t=139s) — Ștergem folderele generate; reconstruirea restaurează artefactele necesare.
- [00:03:37](https://www.youtube.com/watch?v=jLWY_id6nXU&t=217s) — Extindem secvența de afișare și observăm execuția în ordinea din sursă.
- [00:04:44](https://www.youtube.com/watch?v=jLWY_id6nXU&t=284s) — Inserăm pauze de o secundă pentru a face vizibilă așteptarea secvențială.
- [00:05:46](https://www.youtube.com/watch?v=jLWY_id6nXU&t=346s) — Mutăm acțiuni de afișare în `Example` și îl apelăm între instrucțiuni din exterior.
- [00:07:23](https://www.youtube.com/watch?v=jLWY_id6nXU&t=443s) — Apelăm din nou același corp și revenim după fiecare execuție.
- [00:08:16](https://www.youtube.com/watch?v=jLWY_id6nXU&t=496s) — Redenumim `Example` în `Hello` fără a-i schimba corpul.
- [00:08:43](https://www.youtube.com/watch?v=jLWY_id6nXU&t=523s) — Adăugăm o funcție nefolosită; ieșirea de pornire rămâne neschimbată până se adaugă un apel.
- [00:09:14](https://www.youtube.com/watch?v=jLWY_id6nXU&t=554s) — Adăugăm apelul lipsă, astfel încât al doilea corp să se execute.
- [00:09:39](https://www.youtube.com/watch?v=jLWY_id6nXU&t=579s) — Adăugăm un apel într-un corp de funcție și urmărim revenirile imbricate.
- [00:10:41](https://www.youtube.com/watch?v=jLWY_id6nXU&t=641s) — Urmărim instrucțiunile următoare și încă un apel, până când programul nu mai are lucru rămas.
