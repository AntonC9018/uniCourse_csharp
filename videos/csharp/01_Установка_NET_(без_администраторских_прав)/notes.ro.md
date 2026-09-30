> Această notă a fost generată de AI (gpt-6-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/muse-spark-1.3-contributor-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Instalarea .NET SDK fără drepturi de administrator

[00:00:00](https://www.youtube.com/watch?v=QsO1HedgKt8&t=0s) **.NET SDK** conține instrumentele necesare pentru compilarea surselor C#. Această lecție arată cum:

- se instalează SDK-ul fără drepturi de administrator.
- se face comanda `dotnet` disponibilă în console noi.
- se construiește manual un proiect minimal și se generează unul dintr-un șablon.
- se configurează un editor opțional.

Lecție asociată: [Instalarea .NET și crearea unui prim proiect](../../../labs/1_basic/01_install.md).

Concepte folosite în demonstrație:

- **Consola:** o fereastră text pentru comenzi. **PowerShell** este consola Windows folosită aici.
- **Scriptul:** un fișier cu comenzi. `.ps1` este extensia de script PowerShell; o **extensie** de nume de fișier este sufixul de după ultimul punct.
- **Calea:** locația unui fișier sau folder. `cd` schimbă folderul curent; o **cale relativă** pornește din acel folder, iar o **cale completă** include unitatea și folderele.
- **Politica de execuție:** regula PowerShell pentru rularea scripturilor.
- **Variabila de mediu:** o setare cu nume moștenită de programe. `PATH` listează folderele căutate pentru comenzi; `DOTNET_ROOT` indică folderul de instalare .NET.
- **Proiectul:** un folder cu surse și setări de compilare. `Program.cs` conține sursa C#; un fișier `.csproj` conține setările proiectului în **XML**, text editabil cu etichete imbricate.
- **Executabilul:** un program care poate rula. O **bibliotecă** furnizează cod pentru alt program.
- **Șablonul:** un model care generează fișiere de pornire.
- **IDE:** un editor cu instrumente de dezvoltare. VS Code și Rider sunt cele două editoare menționate aici.

## Salvarea scriptului de instalare

Pentru a instala .NET fără drepturi de administrator, folosiți [scriptul PowerShell de instalare de la Microsoft](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-install-script). Videoclipul ilustrează acești pași de pregătire:

1. [00:00:30](https://www.youtube.com/watch?v=QsO1HedgKt8&t=30s) Descărcați scriptul și salvați-l cu Ctrl+S.
2. Verificați numele fișierului salvat. Dacă se termină în `.ps1.txt`, eliminați doar `.txt`-ul final, astfel încât PowerShell să vadă un script `.ps1`. Acest exemplu simplificat urmează [salvarea și redenumirea înregistrate](https://www.youtube.com/watch?v=QsO1HedgKt8&t=30s); fișierul descărcat nu are niciun instantaneu în depozit:

   ```text
   dotnet-install.ps1.txt  →  dotnet-install.ps1
   ```

[00:00:47](https://www.youtube.com/watch?v=QsO1HedgKt8&t=47s) Dacă extensiile de nume de fișier sunt ascunse, deschideți File Explorer și activați **View → Show → File name extensions**. Apoi eliminați sufixul suplimentar `.txt`, fie înainte, fie după salvarea fișierului.

## Accesarea scriptului în PowerShell

1. [00:01:01](https://www.youtube.com/watch?v=QsO1HedgKt8&t=61s) Deschideți meniul Start din Windows, căutați **PowerShell** și apăsați Enter.
2. [00:01:21](https://www.youtube.com/watch?v=QsO1HedgKt8&t=81s) Folosiți `cd` (**change directory**) pentru a intra în folderul care conține scriptul. Transmiteți calea folderului, nu numele fișierului scriptului. Această comandă simplificată ilustrează [navigarea înregistrată](https://www.youtube.com/watch?v=QsO1HedgKt8&t=81s):

   ```powershell
   cd "C:\Users\Student\Downloads"
   ```

[00:01:37](https://www.youtube.com/watch?v=QsO1HedgKt8&t=97s) O **cale relativă** pornește din folderul curent al PowerShell-ului, deci `cd Downloads` funcționează când Downloads se află direct în el. O **cale completă** denumește unitatea și folderele, ca în exemplul de mai sus. Ambele forme funcționează atunci când identifică folderul scriptului.

[00:02:00](https://www.youtube.com/watch?v=QsO1HedgKt8&t=120s) Dacă scriptul se află pe unitatea D, iar PowerShell este pe unitatea C, introduceți `D:` pentru a comuta la locația curentă de pe D, apoi folosiți `cd` pentru a ajunge în folder. Aceasta este [schimbarea de unitate înregistrată](https://www.youtube.com/watch?v=QsO1HedgKt8&t=120s):

```powershell
D:
cd "D:\Downloads"
```

În PowerShell, `cd "D:\Downloads"` poate comuta unitatea și folderul într-o singură comandă: `cd` este un alias pentru `Set-Location`, care acceptă căi de pe alte unități ([referința PowerShell pentru locație](https://learn.microsoft.com/en-us/powershell/scripting/samples/managing-current-location)).

[00:02:15](https://www.youtube.com/watch?v=QsO1HedgKt8&t=135s) Dacă Explorer afișează o cale prescurtată, folosiți opțiunea de copiere a căii. O **cale de fișier** copiată include numele fișierului; eliminați acel nume de fișier înainte de a transmite către `cd` folderul care îl conține. PowerShell este apoi poziționat lângă programul de instalare.

<details>
<summary>Preziceți: ce se întâmplă dacă calea copiată se termină tot cu numele fișierului scriptului atunci când este pasată lui <code>cd</code>?</summary>

`cd` încearcă să intre în acel nume de fișier ca și cum ar fi un folder. Eliminați numele fișierului și păstrați directorul care îl conține.
</details>

## Rularea programului de instalare

[00:02:33](https://www.youtube.com/watch?v=QsO1HedgKt8&t=153s) Rularea directă a scriptului poate eșua din cauza **politicii de execuție** din PowerShell. Dacă se întâmplă asta, ajustați politica pentru sesiunea curentă și reîncercați. Transcrierea nu păstrează comanda exactă afișată pe ecran. [Referința Microsoft pentru `Set-ExecutionPolicy`](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.security/set-executionpolicy) documentează domeniul **Process**, care se aplică doar procesului PowerShell curent. Acest exemplu însoțește [eșecul și ajustarea înregistrate](https://www.youtube.com/watch?v=QsO1HedgKt8&t=153s); nu este o transcriere:

```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
```

[00:03:00](https://www.youtube.com/watch?v=QsO1HedgKt8&t=180s) La reîncercarea programului de instalare, selectați un canal de SDK suportat.

> **Înregistrare (.NET 9):** videoclipul selectează SDK 9.0. Fără acea selecție, programul său de instalare ar fi ales .NET 8, despre care lecția spune că era de asemenea utilizabil. [Selecția înregistrată](https://www.youtube.com/watch?v=QsO1HedgKt8&t=180s) este reprezentată de comanda simplificată `.\dotnet-install.ps1 -Channel 9.0`; comanda exactă de pe ecran nu este păstrată într-un instantaneu din depozit.

Pentru o configurare nouă, instalați .NET 10 SDK. Din septembrie 2026, [.NET 10 este versiunea LTS activă](https://dotnet.microsoft.com/en-us/platform/support/policy), cu suport până pe 14 noiembrie 2028. Aceeași politică listează .NET 9 în suport de mentenanță până pe 10 noiembrie 2026; nu este încă scos din suport. [Referința scriptului de instalare](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-install-script) documentează `-Channel` pentru selectarea unei versiuni majore:

```powershell
.\dotnet-install.ps1 -Channel 10.0
```

[00:03:11](https://www.youtube.com/watch?v=QsO1HedgKt8&t=191s) Dacă PowerShell cere confirmarea, alegeți **R** pentru **Run once**. După instalare, `dotnet` funcționează în acea consolă.

## Facerea comenzii dotnet disponibilă în console noi

[00:03:32](https://www.youtube.com/watch?v=QsO1HedgKt8&t=212s) Testați `dotnet` în consola de instalare și într-o consolă nouă. În înregistrare funcționează în prima, dar lipsește inițial în a doua. Programul de instalare adaugă locația sa la calea de căutare a comenzilor din sesiunea curentă, nu la `PATH`-ul persistent de utilizator ([referința scriptului de instalare](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-install-script)). Acest [test înregistrat](https://www.youtube.com/watch?v=QsO1HedgKt8&t=212s) simplificat arată diferența:

```text
installation console: dotnet  → found
new console:          dotnet  → not found yet
```

[00:04:01](https://www.youtube.com/watch?v=QsO1HedgKt8&t=241s) Adăugați directorul care conține `dotnet.exe` la **Path**-ul de utilizator, lista de directoare în care Windows caută o comandă introdusă fără cale completă:

1. Copiați directorul de instalare care conține `dotnet.exe`.
2. Deschideți editorul de variabile de mediu din Windows și selectați **Path**-ul de utilizator.
3. Faceți clic pe **Edit**, apoi pe **New**, lipiți acel director și confirmați.

Folosiți directorul din instalarea voastră. Acest exemplu stă alături de [editarea PATH înregistrată](https://www.youtube.com/watch?v=QsO1HedgKt8&t=241s):

```text
User Path:  C:\Users\Student\.dotnet
```

[00:04:55](https://www.youtube.com/watch?v=QsO1HedgKt8&t=295s) Deschideți o consolă nouă și introduceți `dotnet`. Windows poate găsi și lansa `dotnet.exe` deoarece directorul său se află acum în `PATH`-ul de utilizator.

<details>
<summary>De ce a găsit prima consolă <code>dotnet</code>, iar una nouă nu?</summary>

Sesiunea programului de instalare avea acces la calea sa de instalare. Noua sesiune avea nevoie de directorul care conține `dotnet.exe` în `PATH`-ul de utilizator.
</details>

## Construirea unui proiect minimal

[00:05:05](https://www.youtube.com/watch?v=QsO1HedgKt8&t=305s) Creați un **folder de proiect** oriunde doriți să păstrați programul și setările sale de compilare. [00:05:10](https://www.youtube.com/watch?v=QsO1HedgKt8&t=310s) Începeți cu două fișiere distincte:

- **`Program.cs`:** textul sursă C# care conține instrucțiunile programului.
- **`<project-name>.csproj`:** setările de proiect citite de SDK.

Aceste scurte exemple ilustrează rolurile lor. Ele adaptează frameworkul țintă al proiectului la .NET 10 pentru o configurare nouă; înregistrarea nu păstrează o copie exactă a fișierelor sale de proiect:

```csharp
// Program.cs — simplified example
Console.WriteLine("Hello, World!");
```

```xml
<!-- Example.csproj — simplified intermediate state for .NET 10 -->
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
</Project>
```

[00:05:50](https://www.youtube.com/watch?v=QsO1HedgKt8&t=350s) Un fișier `.csproj` este **XML** editabil: `Project` include setările, `PropertyGroup` le grupează, iar `TargetFramework` denumește versiunea .NET vizată de proiect. Exemplul folosește [`net10.0`](https://learn.microsoft.com/en-us/dotnet/standard/frameworks) pentru recomandarea curentă; nu este o transcriere a fișierului de pe ecran.

[00:06:21](https://www.youtube.com/watch?v=QsO1HedgKt8&t=381s) Consola poate genera și ea aceste fișiere. Mai întâi examinați fișierele construite manual; comanda de generare apare mai târziu în lecție.

[00:07:03](https://www.youtube.com/watch?v=QsO1HedgKt8&t=423s) Adăugați `OutputType` setat la `Exe` în `.csproj`, astfel încât proiectul să se compileze ca **executabil**, un program care poate rula, și nu ca **bibliotecă**, care furnizează cod pentru alt program. Această modificare simplificată însoțește [editarea înregistrată a proiectului](https://www.youtube.com/watch?v=QsO1HedgKt8&t=423s) și folosește ținta .NET 10 pentru un proiect nou:

```xml
<PropertyGroup>
  <OutputType>Exe</OutputType>
  <TargetFramework>net10.0</TargetFramework>
</PropertyGroup>
```

Proiectul intermediar avea sursa și un framework țintă; acum declară în plus un tip de ieșire rulabil. Înregistrarea nu arată aici o rulare separată reușită a proiectului construit manual.

## Generarea proiectului dintr-un șablon

[00:07:39](https://www.youtube.com/watch?v=QsO1HedgKt8&t=459s) Într-un folder nou gol, rulați `dotnet new console`. Șablonul `console` generează o aplicație minimă de consolă. [Invocarea înregistrată](https://www.youtube.com/watch?v=QsO1HedgKt8&t=459s) arată acest pas:

```powershell
dotnet new console
```

[00:08:11](https://www.youtube.com/watch?v=QsO1HedgKt8&t=491s) Inspectați fișierele generate. Șablonul creează automat `Program.cs` și un fișier `.csproj`. Acest arbore simplificat rezumă [starea generată înregistrată](https://www.youtube.com/watch?v=QsO1HedgKt8&t=491s); nu este un instantaneu exact consemnat:

```text
new-project/
├── Program.cs
└── new-project.csproj
```

<details>
<summary>Auto-verificare: care fișier conține instrucțiuni C# și care fișier îi spune SDK-ului cum să le compileze?</summary>

`Program.cs` conține instrucțiuni C#. Fișierul XML `.csproj` conține setările proiectului, inclusiv tipul de ieșire executabil.
</details>

## Configurarea unui editor opțional

[00:08:36](https://www.youtube.com/watch?v=QsO1HedgKt8&t=516s) VS Code este opțional: SDK-ul și consola oferă deja fluxul de lucru cu proiectul. Videoclipul recomandă și **Rider** ca **IDE** alternativ, dar nu predă configurarea lui. Folosește VS Code pentru demonstrația rămasă.

1. [00:09:15](https://www.youtube.com/watch?v=QsO1HedgKt8&t=555s) În vizualizarea Extensions din VS Code, instalați **C# Dev Kit** pentru suport de dezvoltare C#. Aceasta schimbă configurarea editorului, nu `Program.cs` sau fișierul de proiect.
2. [00:09:42](https://www.youtube.com/watch?v=QsO1HedgKt8&t=582s) Distingeți cele două **variabile de mediu**: `PATH` permite unei console să găsească `dotnet` după nume, iar `DOTNET_ROOT` indică extensiei C# din acest videoclip directorul de instalare. [Ghidul Microsoft de instalare pe Windows](https://learn.microsoft.com/en-us/dotnet/core/install/windows) documentează setarea ambelor pentru o instalare de utilizator.
3. [00:09:52](https://www.youtube.com/watch?v=QsO1HedgKt8&t=592s) Creați o variabilă de utilizator numită `DOTNET_ROOT` cu același director adăugat în `PATH`. Folosiți folderul care conține `dotnet.exe`, nu numele fișierului executabilului. Această valoare ilustrativă însoțește [editarea înregistrată a variabilei](https://www.youtube.com/watch?v=QsO1HedgKt8&t=592s):

   ```text
   DOTNET_ROOT = C:\Users\Student\.dotnet
   ```

4. [00:10:14](https://www.youtube.com/watch?v=QsO1HedgKt8&t=614s) Închideți și redeschideți VS Code pentru ca acesta să primească noua variabilă. Lecția se încheie cu acea configurare așteptată a editorului; nu arată o compilare sau o rulare separată reușită în VS Code.

## Traseul înregistrat al codului

Niciun commit din depozit nu conține comanda PowerShell exactă sau fișierele intermediare de proiect arătate în această înregistrare. Aceste linkuri identifică stările înregistrate, în ordine. Perechea de fișiere cerută a proiectului poate fi ilustrată cu acest exemplu simplificat de stare finală:

```text
project/
├── Program.cs
└── project.csproj
```

- [Invocarea programului de instalare la 03:00](https://www.youtube.com/watch?v=QsO1HedgKt8&t=180s) — după obstacolul politicii de execuție, videoclipul selectează SDK 9.0 și rulează scriptul.
- [Proiectul construit manual la 05:50](https://www.youtube.com/watch?v=QsO1HedgKt8&t=350s) — există sursa și setările editabile de proiect XML.
- [Proiectul executabil la 07:03](https://www.youtube.com/watch?v=QsO1HedgKt8&t=423s) — `OutputType` se schimbă în `Exe`.
- [Proiectul de consolă generat la 08:11](https://www.youtube.com/watch?v=QsO1HedgKt8&t=491s) — șablonul produce `Program.cs` și fișierul `.csproj`.
