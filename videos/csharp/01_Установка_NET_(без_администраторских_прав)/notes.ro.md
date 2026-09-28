> Această notă a fost generată de AI (opencode/muse-spark-1.3-contributor-free) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.

# Instalarea .NET SDK fără drepturi de administrator

[00:00:00](https://www.youtube.com/watch?v=QsO1HedgKt8&t=0s) Această lecție arată cum instalează instructorul un .NET SDK pentru dezvoltare C# fără drepturi de administrator, cum face comanda `dotnet` disponibilă în console noi, cum construiește un proiect mic și cum pregătește un editor opțional. **.NET SDK** este kitul de dezvoltare software care conține instrumentele necesare pentru compilarea surselor C#. O **consolă** este o fereastră text pentru comenzi; **PowerShell** este consola Windows folosită aici. Un **script** este un fișier cu comenzi, iar `.ps1` este extensia de script PowerShell. O **extensie** după ultimul punct din numele unui fișier îi spune Windows-ului ce tip de fișier este.

Termenii folosiți în demonstrație sunt: **calea**, locația unui fișier sau folder; **`cd`**, comanda care schimbă folderul curent; **calea relativă**, o cale măsurată față de acel folder; **calea completă**, una care include unitatea și folderele; **politica de execuție**, regula PowerShell pentru rularea scripturilor; **variabila de mediu**, o setare cu nume moștenită de programe; **`PATH`**, variabila de mediu căutată pentru comenzi; și **`DOTNET_ROOT`**, variabila de mediu care indică folderul de instalare .NET. Un **proiect** este un folder cu surse și setări de compilare: `Program.cs` este un fișier sursă C#, iar un fișier `.csproj` este fișierul XML cu setările proiectului. **XML** este text editabil cu etichete imbricate. Un **executabil** poate fi rulat; o **bibliotecă** furnizează cod pentru alt program. Un **șablon** generează fișiere de pornire. Un **IDE** este un editor cu instrumente de dezvoltare; VS Code și Rider sunt cele două editoare menționate aici.

## Salvarea scriptului de instalare

[00:00:30](https://www.youtube.com/watch?v=QsO1HedgKt8&t=30s) Instructorul salvează cu Ctrl+S programul de instalare PowerShell descărcat. Primul nume de fișier se poate termina în `.ps1.txt`; `.txt` îl face să arate ca un document text în loc de scriptul `.ps1` pe care PowerShell ar trebui să-l ruleze. Eliminați doar `.txt` de la final. Această tranziție de nume de fișier este un exemplu simplificat al [salvării și redenumirii înregistrate](https://www.youtube.com/watch?v=QsO1HedgKt8&t=30s); niciun commit din depozit nu surprinde fișierul descărcat:

```text
dotnet-install.ps1.txt  →  dotnet-install.ps1
```

[00:00:47](https://www.youtube.com/watch?v=QsO1HedgKt8&t=47s) Dacă sufixul este ascuns, instructorul deschide File Explorer și activează **View → Show → File name extensions**. Extensiile de nume de fișier sunt sufixele vizibile precum `.txt` și `.ps1`. Astfel instructorul poate vedea și elimina sufixul suplimentar chiar și după ce fișierul a fost descărcat.

## Accesarea scriptului în PowerShell

[00:01:01](https://www.youtube.com/watch?v=QsO1HedgKt8&t=61s) Instructorul deschide meniul Start din Windows, caută **PowerShell** și apasă Enter. PowerShell este fereastra de comenzi care va rula fișierul `.ps1`.

[00:01:21](https://www.youtube.com/watch?v=QsO1HedgKt8&t=81s) Înainte de rulare, instructorul folosește `cd` (**change directory**) pentru a intra în folderul care conține scriptul. O **cale de folder** denumește directorul; numele fișierului scriptului nu face parte din argumentul `cd`. Această comandă simplificată ilustrează [navigarea înregistrată](https://www.youtube.com/watch?v=QsO1HedgKt8&t=81s):

```powershell
cd "C:\Users\Student\Downloads"
```

[00:01:37](https://www.youtube.com/watch?v=QsO1HedgKt8&t=97s) O **cale relativă** pornește din folderul curent al PowerShell-ului, deci `cd Downloads` funcționează dacă acel folder se află direct în cel curent. O **cale completă** identifică unitatea și toate folderele, ca în exemplul de mai sus; instructorul spune că ambele forme funcționează.

[00:02:00](https://www.youtube.com/watch?v=QsO1HedgKt8&t=120s) Dacă scriptul se află pe unitatea D, iar PowerShell este pe unitatea C, instructorul tastează `D:` pentru a schimba unitatea, apoi navighează spre folder. [Schimbarea de unitate înregistrată](https://www.youtube.com/watch?v=QsO1HedgKt8&t=120s) este ilustrată prin:

```powershell
D:
cd "D:\Downloads"
```

[00:02:15](https://www.youtube.com/watch?v=QsO1HedgKt8&t=135s) Când Explorer arată o cale prescurtată, instructorul folosește opțiunea de copiere a căii. O **cale de fișier** copiată include numele fișierului, deci instructorul elimină acel nume de fișier înainte de a transmite către `cd` calea rămasă a folderului. Starea intermediară este PowerShell poziționat lângă programul de instalare, gata să-l invoce.

<details>
<summary>Preziceți: ce se întâmplă dacă calea copiată se termină tot cu numele fișierului scriptului atunci când este pasată lui <code>cd</code>?</summary>

`cd` încearcă să intre în acel nume de fișier ca și cum ar fi un folder. Eliminați numele fișierului și păstrați directorul care îl conține.
</details>

## Rularea programului de instalare

[00:02:33](https://www.youtube.com/watch?v=QsO1HedgKt8&t=153s) Instructorul încearcă mai întâi să ruleze scriptul direct. PowerShell îl respinge conform **politicii de execuție**, regula care controlează executarea scripturilor, deci instructorul ajustează acea politică și reîncearcă. Transcrierea nu păstrează comanda exactă pentru politică afișată pe ecran. Ca exemplu doar pentru sesiunea curentă, [referința PowerShell pentru politica de execuție](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.security/set-executionpolicy) de la Microsoft documentează domeniul **Process**, care se aplică procesului curent PowerShell. Aceasta este o comandă ilustrativă alături de [eșecul și ajustarea înregistrate](https://www.youtube.com/watch?v=QsO1HedgKt8&t=153s), nu o transcriere a opțiunii de pe ecran:

```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
```

[00:03:00](https://www.youtube.com/watch?v=QsO1HedgKt8&t=180s) Instructorul reîncearcă programul de instalare și adaugă `9.0` pentru a selecta .NET 9 SDK. În această înregistrare, programul de instalare ar selecta altfel versiunea 8, despre care instructorul spune că este de asemenea utilizabilă. Sintaxa `-Channel` de mai jos este un mod simplificat și documentat de a selecta o linie majoră de SDK, susținut de [referința scriptului de instalare](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-install-script); [selectarea 9.0 înregistrată](https://www.youtube.com/watch?v=QsO1HedgKt8&t=180s) este sursa pentru starea lecției, iar niciun instantaneu exact al comenzii nu este consemnat:

```powershell
.\dotnet-install.ps1 -Channel 9.0
```

[00:03:11](https://www.youtube.com/watch?v=QsO1HedgKt8&t=191s) Când PowerShell cere confirmarea, instructorul alege **R**, adică **Run once**, în loc să acorde o opțiune permanentă mai largă. Programul de instalare face apoi ca `dotnet` să poată fi folosit în acea consolă curentă.

## Facerea comenzii dotnet disponibilă în console noi

[00:03:32](https://www.youtube.com/watch?v=QsO1HedgKt8&t=212s) Instructorul testează `dotnet`: funcționează în consola folosită pentru instalare, dar o consolă nou deschisă la început nu o găsește. **Comanda `dotnet`** pornește programul .NET instalat din linia de comandă; rezultatul arată că noua sesiune nu are folderul de instalare în calea de căutare a comenzilor. Acest [test înregistrat](https://www.youtube.com/watch?v=QsO1HedgKt8&t=212s) simplificat contrastează cele două sesiuni:

```text
installation console: dotnet  → found
new console:          dotnet  → not found yet
```

[00:04:01](https://www.youtube.com/watch?v=QsO1HedgKt8&t=241s) Instructorul copiază directorul de instalare care conține `dotnet`, deschide editorul de variabile de mediu din Windows, selectează **Path**-ul de utilizator, apasă **Edit**, apoi **New**, lipește directorul și confirmă. **`PATH`** este lista de directoare căutate atunci când o comandă este tastată fără cale completă. Instructorul folosește setarea de utilizator deoarece această instalare se face fără drepturi de administrator. Calea de mai jos este ilustrativă; folosiți directorul care conține efectiv `dotnet.exe`-ul vostru instalat. Ea stă alături de [editarea PATH înregistrată](https://www.youtube.com/watch?v=QsO1HedgKt8&t=241s):

```text
User Path:  C:\Users\Student\.dotnet
```

[00:04:55](https://www.youtube.com/watch?v=QsO1HedgKt8&t=295s) Deoarece acel director conține `dotnet.exe`, tastarea `dotnet` într-o consolă permite Windows-ului să-l găsească și să-l lanseze. Rezultatul-cheie este descoperirea comenzii într-o consolă nou deschisă, nu doar în sesiunea programului de instalare.

<details>
<summary>De ce a găsit prima consolă <code>dotnet</code>, iar una nouă nu?</summary>

Sesiunea programului de instalare avea acces la calea sa de instalare. Noua sesiune avea nevoie de directorul care conține `dotnet.exe` în `PATH`-ul de utilizator.
</details>

## Construirea unui proiect minimal

[00:05:05](https://www.youtube.com/watch?v=QsO1HedgKt8&t=305s) Instructorul creează un **folder de proiect**, un loc unde păstrează programul și setările sale de compilare; numele și locația lui sunt la alegerea cursantului. [00:05:10](https://www.youtube.com/watch?v=QsO1HedgKt8&t=310s) Proiectul minimal inițial constă din două fișiere distincte: **`Program.cs`**, textul sursă C# care rulează, și **`<project-name>.csproj`**, setările de proiect citite de SDK. [Exercițiul de curs asociat la revizia de sincronizare](https://github.com/AntonC9018/uniCourse_csharp/blob/a944ae1fd13128aa386007b0681b6019d2b8b97e/labs/1_basic/01_install.md) cere acele fișiere, dar **nu este un commit exact al proiectului de pe ecranul instructorului**. Aceste scurte exemple ilustrează rolurile lor:

```csharp
// Program.cs — simplified example
Console.WriteLine("Hello, World!");
```

```xml
<!-- Example.csproj — simplified intermediate state -->
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
  </PropertyGroup>
</Project>
```

[00:05:50](https://www.youtube.com/watch?v=QsO1HedgKt8&t=350s) Instructorul descrie fișierul `.csproj` ca **XML** editabil, adică text obișnuit structurat prin etichete de deschidere și de închidere. Aici `Project` include setările, `PropertyGroup` le grupează, iar `TargetFramework` denumește versiunea .NET vizată de proiect. Codul de mai sus este un exemplu explicativ; transcrierea nu păstrează textul exact al proiectului.

[00:06:21](https://www.youtube.com/watch?v=QsO1HedgKt8&t=381s) Instructorul menționează că consola poate genera fișierele minimale și lasă demonstrația acelei comenzi pentru mai târziu în lecție. Aceasta este limita de scop în acest punct: mai întâi înțelegeți fișierele construite manual, apoi vedeți generarea.

[00:07:03](https://www.youtube.com/watch?v=QsO1HedgKt8&t=423s) Proiectul construit manual trebuie încă să declare că este un **executabil**, un program care poate rula, și nu o **bibliotecă**, adică cod destinat utilizării de către alt program. Instructorul adaugă `OutputType` setat la `Exe` în `.csproj`. Următoarea modificare simplificată stă alături de [editarea înregistrată a proiectului](https://www.youtube.com/watch?v=QsO1HedgKt8&t=423s); [exercițiul de curs asociat fixat la commit](https://github.com/AntonC9018/uniCourse_csharp/blob/a944ae1fd13128aa386007b0681b6019d2b8b97e/labs/1_basic/01_install.md) descrie proiectul, dar nu conține exact acest fișier intermediar:

```xml
<PropertyGroup>
  <OutputType>Exe</OutputType>
  <TargetFramework>net9.0</TargetFramework>
</PropertyGroup>
```

Proiectul intermediar avea sursa și un framework țintă; proiectul modificat declară acum un tip de ieșire rulabil. Lecția nu arată în acest punct o rulare separată reușită a proiectului construit manual.

## Generarea proiectului dintr-un șablon

[00:07:39](https://www.youtube.com/watch?v=QsO1HedgKt8&t=459s) Într-un folder nou gol, instructorul folosește comanda `dotnet` cu **`new console`**. Un **șablon** este un model de pornire inclus în SDK; șablonul `console` creează o aplicație minimă de consolă. [Exercițiul de curs asociat la o revizie fixă](https://github.com/AntonC9018/uniCourse_csharp/blob/a944ae1fd13128aa386007b0681b6019d2b8b97e/labs/1_basic/01_install.md) cere de asemenea această comandă, iar [invocarea înregistrată](https://www.youtube.com/watch?v=QsO1HedgKt8&t=459s) arată această stare a lecției:

```powershell
dotnet new console
```

[00:08:11](https://www.youtube.com/watch?v=QsO1HedgKt8&t=491s) Instructorul inspectează rezultatul: șablonul a produs automat atât un fișier de proiect `.csproj`, cât și `Program.cs`. [Starea generată înregistrată](https://www.youtube.com/watch?v=QsO1HedgKt8&t=491s) este rezumată mai jos; aceste nume de fișiere sunt rezultatul observat, nu o afirmație despre un instantaneu exact consemnat:

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

[00:08:36](https://www.youtube.com/watch?v=QsO1HedgKt8&t=516s) VS Code este opțional: consola și SDK-ul oferă deja fluxul de lucru cu proiectul. Instructorul recomandă și **Rider**, un **IDE** alternativ, adică un program care combină editarea și instrumentele de dezvoltare. Înregistrarea nu predă configurarea Rider. Instructorul continuă cu VS Code pentru demonstrație.

[00:09:15](https://www.youtube.com/watch?v=QsO1HedgKt8&t=555s) În VS Code, instructorul instalează **C# Dev Kit**, o **extensie** care adaugă suport pentru dezvoltare C#. Extensia este selectată din vizualizarea Extensions din VS Code; instalarea ei este un pas de configurare a editorului, nu o modificare a `Program.cs` sau a fișierului de proiect.

[00:09:42](https://www.youtube.com/watch?v=QsO1HedgKt8&t=582s) Instructorul distinge două **variabile de mediu**, setări cu nume pe care programele le citesc din mediul lor. **`PATH`** permite unei console să localizeze comanda `dotnet` după nume; **`DOTNET_ROOT`** indică unui editor sau unui instrument dependent de .NET directorul .NET instalat. Aceasta descrie configurarea instructorului; [ghidul Microsoft de instalare pe Windows](https://learn.microsoft.com/dotnet/core/install/windows) menționează de asemenea că unele instrumente folosesc `DOTNET_ROOT`.

[00:09:52](https://www.youtube.com/watch?v=QsO1HedgKt8&t=592s) Instructorul creează o variabilă de utilizator numită `DOTNET_ROOT` și îi atribuie același director de SDK adăugat în `PATH`. Acesta trebuie să fie folderul care conține `dotnet.exe`-ul instalat, nu numele fișierului executabilului. Această valoare ilustrativă stă alături de [editarea înregistrată a variabilei](https://www.youtube.com/watch?v=QsO1HedgKt8&t=592s):

```text
DOTNET_ROOT = C:\Users\Student\.dotnet
```

[00:10:14](https://www.youtube.com/watch?v=QsO1HedgKt8&t=614s) În final, instructorul închide și redeschide VS Code pentru ca aplicația să primească noua valoare a variabilei de mediu. Lecția se încheie cu acea configurare așteptată a editorului; nu arată o compilare sau o rulare separată reușită în VS Code după redeschidere.

## Traseul înregistrat al codului

Nu a fost găsit niciun commit de depozit care să conțină comanda PowerShell exactă sau fișierele intermediare de proiect arătate în această înregistrare. Aceste linkuri specifice încărcării identifică stările înregistrate ale lecției, de la cea mai veche la cea mai nouă; [exercițiul de curs asociat fixat la commit](https://github.com/AntonC9018/uniCourse_csharp/blob/a944ae1fd13128aa386007b0681b6019d2b8b97e/labs/1_basic/01_install.md) descrie sarcina finală a studentului și nu este un instantaneu exact. Perechea sa de fișiere cerută poate fi ilustrată cu acest exemplu simplificat de stare finală:

```text
project/
├── Program.cs
└── project.csproj
```

- [Invocarea programului de instalare la 03:00](https://www.youtube.com/watch?v=QsO1HedgKt8&t=180s) — după obstacolul politicii de execuție, instructorul selectează SDK 9.0 și rulează scriptul.
- [Proiectul construit manual la 05:50](https://www.youtube.com/watch?v=QsO1HedgKt8&t=350s) — există sursa și setările editabile de proiect XML.
- [Proiectul executabil la 07:03](https://www.youtube.com/watch?v=QsO1HedgKt8&t=423s) — `OutputType` se schimbă în `Exe`.
- [Proiectul de consolă generat la 08:11](https://www.youtube.com/watch?v=QsO1HedgKt8&t=491s) — șablonul produce `Program.cs` și fișierul `.csproj`.
