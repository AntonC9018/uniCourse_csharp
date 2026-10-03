> Această notă a fost generată de AI (opencode/space-bunny-free) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Git: de la un folder local la o modificare GitHub fuzionată

Această lecție construiește un drum complet: de la verificarea lui Git pe Windows până la înregistrarea modificărilor locale, recuperarea fișierelor, lucrarea pe o ramură separată, publicarea pe GitHub, fuzionarea unui pull request și readucerea rezultatului pe computer. Urmează pașii în ordine: fiecare operație ulterioară depinde de starea creată anterior.

Vocabularul folosit pe parcurs:

- **Consolă / comandă / argument / opțiune:** o consolă acceptă comenzi text; o comandă cere o operație, un argument furnizează o valoare precum un nume de fișier, iar o opțiune precum `--help` selectează un comportament. **HTML** este formatul de documente web folosit de pagina de ajutor prezentată aici.
- **Git / GitHub:** Git înregistrează versiunile proiectului; GitHub găzduiește online depozite Git și oferă funcții de colaborare. Un **depozit (repository)** conține istoricul înregistrat al proiectului. **Local** înseamnă pe acest computer; **remote** înseamnă un alt depozit accesat printr-o conexiune.
- **Director de lucru / .git:** directorul de lucru conține fișierele pe care le editezi; directorul ascuns `.git` conține înregistrările și setările depozitului local.
- **Commit / mesaj / identificator:** un commit înregistrează o stare a proiectului ca unitate de schimbări, mesajul său descrie munca, iar identificatorul (hash) distinge acea intrare din istoric. **Istoric / log / pager:** istoricul este succesiunea de commituri; `git log` îl afișează, uneori printr-un pager care așteaptă navigarea sau ieșirea.
- **Neurmărit (untracked) / urmărit (tracked) / modificat:** un fișier neurmărit se află pe disc, dar nu este urmărit de Git; un fișier urmărit a fost adăugat în gestionarea lui Git; un fișier modificat diferă de starea sa înregistrată sau de cea din index.
- **Zonă de pregătire (staging) / add:** zona de pregătire ține versiunile fișierelor pregătite pentru următorul commit; `git add` pune schimbările acolo. Pregătirea și commitul sunt acțiuni separate.
- **Identitate de autor / domeniu de configurare:** un commit înregistrează un nume și o adresă de e-mail de autor. Configurarea locală a repozitului se aplică unui singur proiect; `--global` configurează valorile implicite pentru utilizatorul curent, în toate depozitele.
- **Sursă / compilator / executabil:** sursa este textul lizibil al unui program; un compilator precum `g++` traduce sursa C++ într-un program executabil. **Rezultatul compilării (build output)** este rezultatul generat. **Arhitectura** descrie proiectarea de mașină a computerului țintă; **codul de mașină binar** este reprezentarea executabilă. **Funcțiile din biblioteci** oferă funcționalitate reutilizabilă. Byții și kiloocteții (KB) măsoară dimensiunea fișierelor.
- **.gitignore / regulă / metacaracter (wildcard) / extensie:** `.gitignore` conține reguli pentru ignorarea fișierelor neurmărite. Un metacaracter precum `*` se potrivește cu un text variabil; o extensie este un sfârșit de nume de fișier precum `.exe`.
- **Diff / restore / checkout:** un diff arată diferențele dintre stările unor fișiere; restore readuce fișierele de lucru la o stare înregistrată aleasă; checkout selectează starea unui commit sau a unei ramuri.
- **Ramură (branch) / master / indicator de ramură / commit comun:** o ramură denumește o linie de lucru, iar indicatorul ei identifică commitul curent. `master` este numele ramurii principale folosit în această înregistrare. Un commit comun aparține istoricului ambelor ramuri înainte de divergența lor.
- **Rebase / merge / squash:** rebase reaplică schimbările unei ramuri pe o bază actualizată; merge unește istoricurile ramurilor și poate înregistra un commit de fuziune; squash combină schimbările mai multor commituri într-un singur commit rezultat.
- **Pull request / sursă / țintă:** un pull request este o propunere adresată GitHub pentru a încorpora o ramură în alta. Ramura sursă furnizează munca; ramura țintă (de bază) o primește.
- **Nume remote / origin / URL / HTTPS / SSH:** un nume remote abreviază adresa unui depozit, convențional `origin` pentru conexiunea principală. Un URL specifică acea adresă. HTTPS și SSH sunt metode de conectare; exemplul începe cu HTTPS.
- **Upstream / push / pull / autentificare:** un upstream asociază o ramură locală cu omologul său remote; push trimite commituri către un remote; pull preia și încorporează schimbările din remote. Autentificarea autorizează accesul la un serviciu și este distinctă de autorul înregistrat într-un commit.

[Exercițiul Git din curs](https://AntonC9018.github.io/uniCourse_csharp/ru/labs/basic/git/) oferă sarcini de practică. Nu este dovadă pentru instalarea sau stările intermediare de cod din înregistrare.

Dovezile furnizate nu identifică un commit de depozit imutabil care să conțină acest proiect demonstrativ. Exemplele de mai jos sunt secvențe de comenzi simplificate sau schițe de comportament alături de legături către stările înregistrate; căile, numele de ramuri, valorile de autor și mesajele sunt ilustrative. Ele nu pretind să reproducă un blob de sursă indisponibil.

## Verifică Git într-o consolă Windows

1. [00:00:06](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=6s) Deschide meniul Start din Windows, caută `cmd` și deschide consola: aceasta este fereastra în care se introduc comenzile text.
2. [00:00:14](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=14s) Ține apăsat Ctrl și rotește rotița mouse-ului pentru a mări sau micșora fontul din consola afișată, făcând comenzile și rezultatele mai lizibile.
3. [00:00:20](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=20s) Introdu `git` fără argumente. Un argument este o valoare care urmează după comandă; omiterea lor testează dacă consola poate executa Git.
4. [00:00:29](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=29s) Alternativ, folosește `where` din Windows pentru a localiza un program după nume. Are rolul ilustrat de `which` pe Linux.
5. [00:00:44](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=44s) În înregistrare, căutarea nu găsește niciun program Git, deci următoarea acțiune este instalarea.

Verificări simplificate de disponibilitate corespunzătoare acestor pași înregistrați:

```cmd
git
where git
```

## Instalează Git și deschide ajutorul său

1. [00:01:01](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=61s) Rulează instalatorul lui Git și păstrează setările implicite, așa cum alege instructorul pentru această configurare de bază.
2. [00:01:12](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=72s) După instalare, deschide o consolă nouă. În demonstrație, fereastra existentă încă nu găsește Git, iar cea nouă găsește.
3. [00:01:24](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=84s) Rulează doar `git`: acum afișează o listă de ajutor cu comenzile disponibile.
4. [00:01:30](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=90s) O opțiune selectează comportamentul unei comenzi; `--help` cere explicit ajutorul general.
5. [00:01:35](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=95s) Cere documentația unei singure comenzi cu `git clone --help`. Configurația demonstrată deschide o pagină de ajutor HTML, un document afișat într-un browser. Acest pas citește documentația comenzii clone; nu clonează un proiect.

Cereri simplificate de ajutor pentru instalarea înregistrată:

```cmd
git
git --help
git clone --help
```

## Separă Git de GitHub

[00:01:56](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=116s) **Git** este instrumentul care înregistrează versiunile. **GitHub** este un serviciu care folosește Git pentru a stoca depozite online. Un depozit este istoricul înregistrat al proiectului, iar Git poate crea și folosi unul complet local, fără o conexiune la GitHub.

[00:02:20](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=140s) Un depozit remote este o altă copie accesată printr-o conexiune. GitHub este o gazdă posibilă; îl poate găzdui și un alt furnizor care acceptă protocolul lui Git.

[00:02:33](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=153s) **Domeniu și ordine:** operațiile pe GitHub sunt amânate intenționat cât timp lecția stabilește operațiile locale în folderul proiectului. Pentru commiturile locale următoare nu este nevoie de niciun cont online sau de publicare.

## Inițializează depozitul proiectului

1. [00:02:48](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=168s) Intră în folderul proiectului înainte de a rula `git init`. Directorul de lucru este folderul ale cărui fișiere urmează să le gestionezi; inițializarea în altă parte ar asocia Git cu locația greșită.
2. [00:03:04](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=184s) Inițializarea creează directorul ascuns `.git` în interiorul folderului proiectului.
3. [00:03:09](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=189s) Dacă nu este vizibil în Explorer, deschide opțiunile sale, mergi la View și activează afișarea fișierelor ascunse.
4. [00:03:18](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=198s) Directorul `.git` stochează istoricul depozitului, inclusiv stările fișierelor înregistrate la commituri.
5. [00:03:35](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=215s) Un commit este o unitate înregistrată de schimbări: păstrează o stare a proiectului care conține fișierele adăugate sau modificate la acel pas.

Secvență simplificată de inițializare pentru starea înregistrată a folderului proiectului:

```cmd
cd /d C:\path\to\project
git init
```

Calea este un substituent pentru proiectul tău. Proiectul rezultat conține fișiere de lucru obișnuite alături de înregistrările ascunse ale depozitului.

## Pregătește primul fișier sursă

[00:03:54](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=234s) Folosește `git status` pentru a inspecta starea curentă a depozitului. În acest stadiu folderul de lucru conține fișierul sursă C++ inițial; numele de fișier simplificat folosit mai jos este `a.cpp`.

1. [00:04:04](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=244s) Primul rezultat al statusului raportează că nu există commituri și un fișier sursă neurmărit.
2. [00:04:11](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=251s) **Neurmărit** înseamnă că fișierul există pe disc, dar Git nu a început să îl urmărească. Faptul că ești în folder nu este suficient pentru a-l salva în istoric.
3. [00:04:19](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=259s) Rulează `git add` cu numele fișierului. Astfel începe urmărirea fișierului și versiunea sa curentă ajunge în **zona de pregătire**, mulțimea de schimbări pregătite pentru următorul commit.
4. [00:04:28](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=268s) Statusul listează acum un fișier nou la schimbări de commitat. Tot nu există commituri.
5. [00:04:47](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=287s) Pregătirea nu înregistrează o intrare separată în istoric. Este nevoie de un commit înainte ca aceasta să devină o stare salvată la care poți reveni.

Secvență simplificată pentru tranziția de la neurmărit la pregătit:

```cmd
git status
git add a.cpp
git status
```

<details>
<summary>După ce add reușește, a creat Git primul commit?</summary>

Nu. Fișierul este pregătit pentru un commit, dar istoricul înregistrat nu conține încă niciun commit. Pregătirea și înregistrarea sunt pași separați.

</details>

## Configurează autorul și creează primul commit

[00:05:07](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=307s) Folosește `git commit -m "..."` pentru a înregistra starea pregătită. Aici `-m` este opțiunea pentru mesaj; ghilimelele păstrează descrierea împreună, ca un singur argument. [00:05:17](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=317s) **Mesajul de commit** este documentație salvată împreună cu commitul, deci descrie ce s-a schimbat, în loc să fie tratat ca text de consolă de unică folosință.

Prima încercare înregistrată este reprezentată de această comandă simplificată:

```cmd
git commit -m "Add source file"
```

[00:05:40](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=340s) Acea încercare eșuează pentru că Git nu are o identitate de autor configurată. Urmează configurația sugerată în eroare înainte de a reîncerca.

[00:06:01](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=361s) **Identitatea de autor** înseamnă numele și e-mailul scrise într-un commit; nu este un nume de utilizator GitHub. E-mailul nu trebuie să corespundă unui cont GitHub. [00:06:12](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=372s) Instructorul explică că Git nu verifică deținerea adresei furnizate când o înregistrează, deci câmpul autor nu este, singur, o dovadă de identitate.

[00:06:32](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=392s) **Domeniul de configurare** determină unde se aplică aceste valori. `--global` stabilește valori implicite pentru utilizatorul curent, în toate depozitele, inclusiv proiectele viitoare. [00:06:51](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=411s) Omisiunea lui aici stochează valorile pentru acest depozit. [00:07:15](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=435s) Demonstrația configurează local atât numele, cât și e-mailul pentru acest proiect.

Configurare locală simplificată și reîncercare, corespunzătoare corectării înregistrate:

```cmd
git config user.name "Example Author"
git config user.email "author@example.com"
git commit -m "Add source file"
```

Pentru a alege domeniul mai larg discutat în înregistrare, aceleași setări primesc opțiunea:

```cmd
git config --global user.name "Example Author"
git config --global user.email "author@example.com"
```

[00:07:25](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=445s) Comanda de commit repetată reușește după configurare. [00:07:37](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=457s) Rezultatul ei există doar în depozitul local `.git`. Un commit local reușit nu a publicat nimic pe internet.

## Inspectează starea înregistrată

[00:07:54](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=474s) Commitul păstrează starea fișierului sursă din momentul înregistrării. [00:08:06](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=486s) **Identificatorul** său, adică hash-ul, și mesajul său diferențiază această versiune de cele ulterioare.

[00:08:14](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=494s) `git log` afișează **istoricul**: identificatori, nume de autor, e-mail de autor, dată și mesaj. Aceste câmpuri îți permit să recunoști și apoi să selectezi o anumită stare.

Inspecție simplificată a istoricului pentru sursa nou înregistrată:

```cmd
git log
```

În acest moment, sursa se află atât în folderul de lucru, cât și păstrată în prima intrare din istoric.

## Compilează programul și păstrează sursa

1. [00:08:40](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=520s) Compilează sursa C++ cu `g++`. Un **compilator** traduce sursa lizibilă într-un **executabil**, programul generat pe care sistemul de operare îl poate rula.
2. [00:09:04](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=544s) Alege numele rezultatului cu `-o`, opțiunea pentru fișierul de ieșire. Schimbarea argumentului ei de nume schimbă numele programului generat.
3. [00:09:15](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=555s) Rulează executabilul rezultat.

Secvență simplificată de compilare și rulare, corespunzătoare înregistrării:

```cmd
g++ a.cpp -o main.exe
main.exe
```

[00:09:26](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=566s) Executabilul este **rezultat al compilării**, un fișier generat din sursă. De obicei este exclus din Git, fiindcă poate fi recompilat. [00:09:37](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=577s) Păstrarea sursei permite altei persoane să compileze pentru propria **arhitectură**, proiectarea de mașină a computerului său.

[00:09:47](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=587s) Sursa este text, deci Git poate arăta schimbările linie cu linie. Executabilul conține **cod de mașină binar**, mult mai puțin util pentru acest fel de revizuire a sursei. [00:10:06](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=606s) Demonstrația compară un fișier sursă de 104 byți cu un executabil de aproximativ 60 KB; byții și KB măsoară dimensiunea, iar excluderea rezultatului generat reduce ce se stochează. [00:10:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=621s) Instructorul atribuie dimensiunea suplimentară din acest exemplu **funcțiilor din biblioteci** incluse, adică funcționalității reutilizabile folosite de program. Este o explicație a exemplului afișat, nu o regulă fixă de dimensiune pentru ieșirea oricărui compilator.

[00:10:33](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=633s) După compilare, statusul listează executabilul ca neurmărit. Compilarea unui fișier nu l-a pregătit și nu l-a comis.

Verificare simplificată la acea stare intermediară înregistrată:

```cmd
git status
```

## Ignoră rezultatele compilării și înregistrează regulile

[00:10:40](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=640s) Pentru a evita pregătirea accidentală a executabilelor la adăugările ulterioare, creează `.gitignore`. O **regulă de ignorare** îi spune lui Git ce fișiere neurmărite să lase în afara adăugărilor și listărilor obișnuite.

1. Scrie numele primului executabil. [00:11:10](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=670s) Statusul arată acum noul `.gitignore`, iar acel executabil dispare din lista obișnuită a fișierelor neurmărite.

   Prima regulă simplificată și verificarea ei, corespunzătoare stării înregistrate:

   ```gitignore
   main.exe
   ```

   ```cmd
   git status
   ```

2. [00:11:17](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=677s) Numele special exact `.gitignore` contează: Git interpretează automat conținutul său ca reguli.
3. [00:11:29](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=689s) Compilează un alt executabil cu alt nume. Prima regulă menționează doar primul fișier, deci al doilea apare în continuare în status.
4. [00:11:44](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=704s) Pune fiecare nume de fișier pe linia sa. [00:11:55](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=715s) Adăugarea celui de-al doilea nume face ca și al doilea executabil să dispară din status.

   Stare intermediară simplificată cu două nume:

   ```gitignore
   main.exe
   other.exe
   ```

5. [00:12:03](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=723s) Înlocuiește numele individuale cu un tipar cu metacaracter. Un **metacaracter** `*` se potrivește cu un text variabil, iar **extensia** `.exe` este sfârșitul numelui de fișier. Așadar, o singură regulă acoperă ambele nume de executabil.

   Înlocuire simplificată, corespunzătoare acestei schimbări de reguli înregistrate:

   ```gitignore
   *.exe
   ```

6. [00:12:24](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=744s) Adaugă și comitează chiar fișierul `.gitignore`. Regulile sunt text de proiect, care ar trebui înregistrat chiar dacă fișierele generate sunt excluse.

   Secvență simplificată pentru salvarea stării regulilor înregistrate:

   ```cmd
   git add .gitignore
   git commit -m "Ignore executable files"
   git log
   ```

[00:12:49](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=769s) Logul distinge acum două etape: adăugarea fișierului sursă și commitul care introduce regulile de ignorare.

<details>
<summary>De ce ignorarea primului executabil nu l-a ascuns pe al doilea?</summary>

Un nume de fișier literal se potrivește exact cu acel nume. Un al doilea nume are nevoie de propria regulă, sau de un tipar precum *.exe, care se potrivește cu ambele.

</details>

## Editează, verifică și comitează sursa

[00:13:07](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=787s) Următoarea modificare elimină două linii de sursă al căror rol era să țină consola deschisă după executarea activității programului. Scopul este ca execuția să se încheie fără acele așteptări.

Transcrierea stabilește comportamentul modificării, dar nu afirmațiile exacte. Această **schiță de comportament** simplificată reprezintă stările inițiale și rezultată ale sursei; nu este o listare C++ verbatim sau care poate fi rulată:

```text
Before:
    perform the program's existing work
    first console-wait statement
    second console-wait statement
    finish

After:
    perform the program's existing work
    finish
```

1. [00:13:17](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=797s) După editare, `git status` marchează sursa urmărită drept **modificată**: textul ei curent diferă de versiunea salvată.
2. [00:13:25](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=805s) Examinează un **diff**, o afișare a diferențelor, cu `git diff`. În această stare demonstrată nimic nu a pregătit editarea, deci comparația dezvăluie schimbările curente față de sursa salvată. Mai precis, un diff simplu compară fișierul de lucru cu versiunea pregătită; aici acea versiune pregătită coincide în continuare cu ultimul commit.
3. [00:13:50](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=830s) Rezultatul identifică două linii eliminate, confirmând editarea intenționată.
4. [00:14:12](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=852s) Pregătește fișierul urmărit și modificat cu `git add` și înregistrează noua sa stare cu încă un commit.

Secvență simplificată de verificare și înregistrare, corespunzătoare acelor pași înregistrați:

```cmd
git status
git diff
git add a.cpp
git commit -m "Remove console waits"
```

Aceasta creează o versiune ulterioară, păstrând sursa anterioară în istoric.

## Înregistrează mai multe fișiere și anulează o ștergere necomisă

1. [00:15:13](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=913s) Creează fișiere suplimentare într-un subfolder. Rezultatul de status demonstrat grupează conținutul neurmărit sub o singură intrare de folder, deci lista inițială nu trebuie să arate fiecare nume de fișier.
2. [00:15:36](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=936s) Mută B și C în folderul principal al proiectului. Statusul le listează acum individual.
3. [00:15:41](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=941s) Pregătește schimbările împreună cu `git add .`. Punctul înseamnă directorul curent; fișierele sale noi și modificate sunt pregătite împreună, sub rezerva regulilor de ignorare.
4. [00:16:07](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=967s) Comitește fișierele pregătite ca o singură unitate de schimbări.

Secvență simplificată de adăugare în lot pentru starea înregistrată cu mai multe fișiere:

```cmd
git status
git add .
git commit -m "Add B and C"
```

[00:16:33](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=993s) Acum șterge din directorul de lucru fișiere comise. Astfel dispare copiile pe care le vezi pe disc, dar nu și versiunile anterioare din istoricul depozitului.

[00:16:46](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1006s) **Restore** readuce fișierele de lucru la starea înregistrată selectată. În acest exemplu ștergerea nu este nici pregătită, nici comisă, iar `git restore .` returnează fișierele cu codul lor anterior.

Comandă simplificată de recuperare pentru acea ștergere necomisă din înregistrare:

```cmd
git restore .
```

Rezultatul arată de ce un commit salvat diferă de simplul fapt de a avea fișiere în folder.

## Inspectează istoricul după o ștergere comisă

[00:17:52](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1072s) Repetă ștergerea și înregistreaz-o de data aceasta: absența fișierelor devine parte din noua stare comisă.

Comenzi simplificate după ștergerea fișierelor în exemplul înregistrat:

```cmd
git add .
git commit -m "Delete earlier files"
```

[00:18:04](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1084s) Rularea lui `git restore .` nu le mai recuperează acum. Restore nu are o ștergere necomisă de anulat: însăși starea curentă înregistrată nu conține acele fișiere.

Pentru a vedea starea anterioară:

1. [00:18:18](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1098s) Rulează `git log` și alege identificatorul unui commit de dinaintea ștergerii.
2. [00:18:44](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1124s) Dacă istoricul este afișat într-un **pager**, un vizualizator interactiv, apasă `q` pentru a-l părăsi și a reveni la introducerea comenzilor.
3. [00:18:48](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1128s) Folosește `git checkout` cu acel identificator vechi. **Checkout** selectează starea unui commit sau a unei ramuri; fișierele reapar atunci când acel commit vechi selectat le conține.
4. [00:19:08](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1148s) Folosește `git checkout master` pentru a reveni la starea curentă a ramurii principale. Ștergerea comisă de pe ea face ca fișierele anterioare să dispară din nou.

Navigare simplificată din înregistrare, cu `OLD_COMMIT_ID` în locul identificatorului copiat din log:

```cmd
git log
git checkout OLD_COMMIT_ID
git checkout master
```

<details>
<summary>De ce funcționează restore înainte de commitul de ștergere, dar nu și după?</summary>

Înainte de commit, starea înregistrată conține încă fișierele. După commit, absența lor este starea înregistrată. Selectează un commit anterior pentru a inspecta versiunea care le conține.

</details>

## Creează o ramură de lucru independentă

[00:19:30](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1170s) Pentru a continua de la commitul vechi, creează acolo o ramură cu nume. O **ramură** denumește o linie de lucru; ea oferă schimbărilor următoare o linie independentă de `master`. [00:19:47](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1187s) Acest lucru este util pentru o funcție experimentală pe care vrei să o dezvolți separat de starea principală a proiectului.

1. Selectează din nou commitul vechi care conține fișierele.
2. [00:20:01](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1201s) Creează și selectează o ramură nouă cu `git checkout -b`. Un indicator de creare a ramurii creează ramura din starea curentă; argumentul următor numește ramura. Numele simplificat folosit în toată această notă este `experiment`.
3. [00:20:13](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1213s) Compară stările: ramura experimentală conține fișierele anterioare, iar `master` conține ștergerea lor.
4. [00:20:32](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1232s) Folosește logul pentru a inspecta istoricul ramurii selectate.
5. [00:20:39](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1239s) Fă checkout după numele ramurii pentru a comuta între aceste stări.

Construirea și inspecția simplificate ale ramurii, corespunzătoare înregistrării:

```cmd
git checkout OLD_COMMIT_ID
git checkout -b experiment
git log
git checkout master
git checkout experiment
```

[00:20:54](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1254s) Adaugă fișierul D pe ramura experimentală, pregătește-l și comitează-l. Textul său inițial exact nu este stabilit de transcrierea furnizată; schimbarea de stare importantă este că această ramură înregistrează acum un fișier suplimentar.

Secvență simplificată după crearea lui D în acea stare înregistrată:

```cmd
git add D
git commit -m "Add D"
```

[00:21:22](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1282s) Doar istoricul ulterior al ramurii experimentale conține această adăugare; doar istoricul ulterior al lui master conține ștergerea fișierelor anterioare. Un **indicator de ramură** identifică commitul curent al ramurii, iar acești doi indicatori conduc acum la schimbări diferite.

## Identifică schimbările de după commitul comun

[00:21:35](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1295s) Pentru a continua experimentul, identifică mai întâi **commitul comun**, ultimul punct împărtășit din ambele istorice. Munca unică ramurii experimentale după acel punct este adăugarea lui D.

1. [00:22:03](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1323s) Inspectează ramura experimentală: logul ei conține adăugarea lui D și nu conține ștergerea din master.
2. [00:22:12](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1332s) Comută pe master și inspectează din nou: ștergerea este prezentă, iar adăugarea lui D lipsește.

Procedură simplificată de comparare pentru acele stări de ramură înregistrate:

```cmd
git checkout experiment
git log
git checkout master
git log
```

Distincția privește commiturile de după istoricul comun, nu copierea fiecărui fișier vechi dintr-un folder în altul.

## Rebazează ramura de lucru peste master

[00:22:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1341s) **Rebase** reaplică schimbările proprii ale unei ramuri pe o bază nouă, furnizată de o altă ramură. Aici munca experimentală are nevoie de baza mai nouă a lui master.

[00:22:59](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1379s) Ambele ramuri au în comun commitul care a adăugat fișierele anterioare. După acel commit comun, master șterge acele fișiere, iar experiment adaugă D. [00:23:19](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1399s) Explicația identifică mai întâi acel punct comun, pentru a distinge istoricul partajat de munca nouă. [00:23:31](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1411s) Rezultatul intenționat este secvențial: păstrează ștergerea din master, apoi aplică adăugarea experimentală a lui D.

Schiță simplificată a istoricului pentru aceste stări înregistrate; literele sunt etichete, nu identificatori reali de commit:

```text
Before:
    shared file addition -> deletion             (master)
                         -> add D                (experiment)

After rebasing experiment:
    shared file addition -> deletion             (master)
                                     -> add D    (experiment)
```

1. Selectează ramura a cărei bază vrei să o actualizezi.
2. [00:23:56](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1436s) Rulează `git rebase master` de pe acea ramură.
3. [00:24:10](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1450s) Inspectează rezultatul: istoricul include acum ștergerea din master urmată de adăugarea experimentală. Indicatorii de ramură identifică în continuare commituri diferite; master nu s-a mutat încă pe cel mai nou commit al experimentului.

Comenzi simplificate corespunzătoare rebase-ului înregistrat:

```cmd
git checkout experiment
git rebase master
git log
```

[00:24:31](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1471s) **Limită de domeniu:** lecția observă că ramurile fără niciun commit comun sunt un caz mai complex și nu parcurge acest exemplu. Exemplul de aici depinde de un istoric partajat.

[00:24:39](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1479s) Commiturile comune nu sunt repetate în exemplul explicat. [00:24:43](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1483s) Înregistrarea actualizează apoi master față de ramura experimentală, aducând ambii indicatori la același commit și deci la aceeași stare curentă.

Secvență simplificată pentru acea continuare înregistrată, care avansează master de-a lungul liniei deja comune:

```cmd
git checkout master
git rebase experiment
```

<details>
<summary>Imediat după rebase-ul ramurii experiment pe master, a primit și master fișierul D?</summary>

Nu. Experiment conține acum baza lui master plus adăugarea lui D, dar master indică în continuare commitul de ștergere. Actualizarea ulterioară a lui master aduce ambele ramuri la același vârf.

</details>

## Alege un flux de lucru pentru integrarea ramurilor

[00:25:01](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1501s) Instructorul recomandă păstrarea lui `master` ca ramură principală și realizarea schimbărilor noi pe o ramură de lucru separată, bazată pe ea.

[00:25:13](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1513s) Un **pull request** este propunerea GitHub de a încorpora o ramură de lucru în ramura principală. Este o funcție a serviciului, nu o comandă Git. **Sursa** furnizează schimbările; **ținta**, numită și ramură de bază, le primește.

[00:25:45](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1545s) Un **merge** unește istorice. Varianta descrisă aici creează un **commit de fuziune** separat, o intrare de istoric care înregistrează unirea. [00:25:51](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1551s) Înaintea acelei integrări, instructorul recomandă rebasarea ramurii de lucru peste master. [00:26:06](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1566s) Motivul enunțat este simplificarea integrării, făcând mai întâi ca baza de lucru să includă schimbările din master. Aceasta descrie fluxul demonstrat, nu garantează că toate fuziunile viitoare vor fi fără conflicte.

Secvență simplificată de pregătire corespunzătoare acelei recomandări:

```cmd
git checkout experiment
git rebase master
```

[00:26:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1581s) **Squash** combină schimbările mai multor commituri într-un singur commit rezultat. [00:26:34](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1594s) De exemplu, adăugarea unui fișier într-un commit și editarea lui într-un altul pot deveni o singură intrare de istoric care conține rezultatul lor combinat.

Schiță simplificată pentru explicația squash-ului înregistrat:

```text
Working history: add file -> edit file
Squashed result: one commit containing the final file
```

Aceste alternative explică forme diferite de istoric. Demonstrația ulterioară cu pull request folosește un commit de fuziune.

## Conectează un depozit GitHub gol

[00:26:56](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1616s) GitHub stochează un **depozit remote**, o copie online disponibilă dincolo de acest computer. [00:27:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1641s) Acea copie permite ca un proiect să fie copiat pe altă mașină, iar schimbările să fie transferate prin GitHub când se continuă lucrul între computere.

1. [00:27:43](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1663s) Creează un depozit pe GitHub care să găzduiască acest proiect: lecția îl descrie ca pe un folder remote care conține Git.
2. [00:28:00](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1680s) Pornește de la un remote gol pentru acest proiect local existent. Evită fișierele inițiale în remote, care ar putea intra în conflict cu istoricul deja creat local.
3. [00:28:05](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1685s) Adaugă local conexiunea cu `git remote add`.
4. [00:28:19](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1699s) Alege un **nume remote**, numele scurt pe care comenzile ulterioare îl folosesc pentru adresă. [00:28:25](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1705s) Convenția pentru remote-ul principal este `origin`.
5. [00:28:33](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1713s) Furnizează **URL-ul** său, adresa depozitului. Adresa GitHub afișată se poate termina în `.git` sau poate omite acel sufix.
6. [00:28:42](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1722s) Folosește **HTTPS**, metoda de conectare aleasă pentru acest exemplu introductiv. **SSH** este alternativa menționată, care necesită configurare suplimentară.
7. [00:28:50](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1730s) Verifică adresa cu `git remote show origin`.

Comenzi simplificate de conectare pentru configurarea înregistrată; înlocuiește substituentul de proprietar și depozit:

```cmd
git remote add origin https://github.com/OWNER/REPOSITORY.git
git remote show origin
```

Adăugarea acestei conexiuni înregistrează o adresă. Ea nu încarcă singură commiturile locale existente.

## Publică master și distinge autorul de autentificare

[00:29:04](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1744s) Primul `git push` eșuează pentru că această ramură locală nu are **upstream**: asocierea cu omologul ei din remote nu a fost setată.

1. [00:29:22](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1762s) La împingere, leagă master local de ramura master de la `origin`. `origin` se rezolvă la URL-ul înregistrat mai devreme; este numele remote-ului, nu numele unei ramuri.
2. [00:29:58](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1798s) Completează autentificarea din browser cerută de configurarea demonstrată. **Autentificarea** autorizează accesul la serviciul de găzduire.
3. [00:30:15](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1815s) După autorizare reușită, GitHub afișează fișierele și istoricul încărcate, confirmând că munca locală înregistrată a fost publicată.

Secvență simplificată pentru prima împingere, corespunzătoare eșecului și corectării:

```cmd
git checkout master
git push
git push --set-upstream origin master
```

Prima împingere de mai sus reprezintă încercarea eșuată demonstrată. `--set-upstream` la reîncercare înregistrează și asocierea ramurii, pe lângă trimiterea commiturilor sale.

[00:30:43](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1843s) Afișajul GitHub arată un autor de commit diferit de proprietarul depozitului. [00:31:00](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1860s) Aceasta rezultă din numele și e-mailul de autor configurate mai devreme: **autoria de commit** este stocată odată cu commitul, în timp ce **autentificarea** autorizează împingerea. Publicarea păstrează autorul înregistrat; nu îl înlocuiește cu contul folosit pentru încărcare.

## Comitește local, apoi împinge următoarea editare

1. [00:31:17](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1877s) Adaugă textul `world` în D și înregistrează încă un commit local.
2. [00:31:33](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1893s) Noua intrare de istoric există în continuare doar local. GitHub nu s-a schimbat doar pentru că commitul a reușit.
3. [00:31:51](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1911s) Rulează `git push`; commitul trimis atunci apare pe GitHub.

Schiță simplificată a schimbării de conținut pentru editarea lui D din înregistrare; textul și aspectul existente nu sunt reproduse:

```text
D before: existing content
D after:  existing content plus "world"
```

Secvență simplificată de înregistrare și publicare pentru aceeași schimbare:

```cmd
git add D
git commit -m "Update D with world"
git push
```

Acum că upstream-ul lui master este configurat, această împingere folosește asocierea sa memorată cu ramura remote.

## Pregătește și publică ramura de lucru

1. [00:32:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1941s) Revino la ramura experimentală și actualizeaz-o din master înaintea următoarei editări, ca munca să pornească de la baza curentă.
2. [00:32:39](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1959s) Editează din nou D și comitează schimbarea pe acea ramură, păstrând munca nouă separată pentru un pull request viitor.

Secvență simplificată pentru acea construcție înregistrată; textul nou exact nu este furnizat de transcriere:

```cmd
git checkout experiment
git rebase master
```

Editează D, apoi continuă aceeași procedură înregistrată:

```cmd
git add D
git commit -m "Change D on the working branch"
```

3. [00:32:59](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1979s) Ramura experimentală nu a fost încă publicată. Prima ei împingere are nevoie de propria asociere upstream; asocierea lui master nu configurează fiecare ramură.
4. [00:33:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2001s) După publicare, GitHub arată ramura experimentală cu un commit înaintea lui master. Commitul în plus este munca de integrat.

Prima publicare simplificată a acelei ramuri de lucru înregistrate:

```cmd
git push --set-upstream origin experiment
```

## Deschide și fuzionează pull request-ul

1. [00:33:46](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2026s) Creează un **pull request** pe GitHub, propunerea de a încorpora ramura de lucru publicată în ramura principală.
2. [00:33:57](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2037s) Selectează `experiment` ca sursă și `master` ca țintă/bază. Comparația arată schimbările prezente în sursă și lipsă din țintă.
3. [00:34:10](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2050s) Creează cererea și folosește acțiunea ei de fuziune pentru a încorpora acele schimbări în master, pe GitHub.
4. [00:34:28](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2068s) După fuziune, ramura de lucru remote poate fi ștearsă, fiindcă munca ei suplimentară este deja inclusă în master.
5. [00:34:39](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2079s) Inspectează istoricul lui master pe GitHub. El conține atât commitul de lucru transferat, cât și un commit de fuziune suplimentar care înregistrează integrarea prin pull request.

Schiță simplificată a istoricului pentru rezultatul înregistrat; aceasta este varianta cu commit de fuziune, nu ilustrația anterioară cu squash:

```text
Remote master after merge:
    earlier master history
    working-branch change to D
    pull-request merge commit joining the histories
```

Direcția selectată de la sursă la țintă contează: master primește munca în plus din experiment.

## Preia rezultatul fuzionat înapoi în master local

[00:35:01](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2101s) Comutarea pe master local după fuziunea de pe GitHub nu preia automat commiturile noi. Ramura locală are în continuare starea sa anterioară. [00:35:16](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2116s) Preia separat munca din remote, pentru ca fișierele locale să reflecte rezultatul pull request-ului.

1. [00:35:24](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2124s) Selectează ramura locală dorită, apoi rulează `git pull`. **Pull** preia și încorporează schimbările din ramura sa remote configurată.
2. [00:35:28](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2128s) D se actualizează, iar `git log` arată acum cele două commituri primite: schimbarea de lucru și commitul de fuziune al pull request-ului.

Secvență simplificată corespunzătoare sincronizării finale înregistrate:

```cmd
git checkout master
git pull
git log
```

Starea locală finală reflectă acum fuziunea de pe GitHub. Editarea locală, commitul, publicarea și primirea muncii din remote au fost fiecare acțiuni separate.

## Traseu înregistrat al istoricului codului

Acestea sunt legături stabile către stări din video, de la cea mai veche. Dovezile furnizate nu oferă o sursă legată de un commit pentru acest exemplu, deci nu se folosește în locul ei commitul unui exercițiu din curs, fără legătură.

- [00:07:25](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=445s) Primul commit local reușit înregistrează sursa inițială.
- [00:12:49](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=769s) Istoricul include acum adăugarea sursei și regulile de ignorare a executabilelor, comise.
- [00:14:12](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=852s) Editarea sursei care elimină două linii de așteptare a consolei este pregătită și înregistrată.
- [00:16:07](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=967s) B și C sunt înregistrate împreună, într-un commit cu mai multe fișiere.
- [00:17:52](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1072s) Un commit ulterior înregistrează ștergerea fișierelor anterioare.
- [00:20:54](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1254s) Ramura experimentală înregistrează D din starea comună anterioară.
- [00:24:10](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1450s) Rebase așază adăugarea lui D după ștergerea din master, pe ramura experimentală.
- [00:24:43](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1483s) Ambii indicatori de ramură ajung la același commit curent după actualizarea lui master.
- [00:30:15](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1815s) Istoricul local existent este publicat pe GitHub.
- [00:31:51](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1911s) Schimbarea ulterioară a lui D, care conține world, este împinsă.
- [00:33:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2001s) Ramura de lucru este publicată cu un commit în plus, cu schimbarea lui D.
- [00:34:39](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2079s) Master de pe GitHub conține schimbarea de lucru și commitul de fuziune al pull request-ului.
- [00:35:28](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2128s) Master local primește ambele commituri, completând istoricul proiectului înregistrat.
