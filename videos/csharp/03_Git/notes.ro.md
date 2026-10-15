> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Git: salvează, revizuiește și partajează un proiect

[00:00:00](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=0s) Obiectivul acestei lecții este să transforme un folder cu fișiere funcționale într-un proiect ale cărui versiuni le poți înregistra, inspecta, recupera și partaja. Vei lucra mai întâi exclusiv pe computerul tău, apoi vei conecta acel istoric la GitHub, vei dezvolta o modificare pe o ramură separată și vei aduce înapoi în fișierele locale rezultatul fuziunii. Demonstrația folosește un program mic în C++, deși exercițiul din curs aplică aceste operații Git proiectului tău.

Termenii următori revin pe parcursul dezvoltării proiectului:

- **Consolă, comandă, argument, opțiune:** consola este fereastra în care introduci text; o comandă cere o acțiune, un argument furnizează o informație precum un nume de fișier, iar o opțiune schimbă modul în care acționează comanda. **Ajutorul** documentează comenzile disponibile. **HTML** este formatul de pagină afișat de vizualizatorul de ajutor demonstrat. **Clone** înseamnă copierea unui depozit împreună cu istoricul său.
- **Git, GitHub, depozit (repository), local, remote, protocol:** Git gestionează versiunile fișierelor; GitHub găzduiește depozite Git și unelte de colaborare. Un depozit conține stările de proiect înregistrate și istoricul lor. Munca locală se află pe acest computer; un depozit remote este o altă copie accesată printr-o conexiune. Un protocol specifică modul în care programele care comunică schimbă informații.
- **Director de lucru și .git:** directorul de lucru conține fișierele pe care le editezi; directorul ascuns `.git` conține istoricul și setările depozitului local.
- **Commit, mesaj, identificator sau hash, istoric, log, pager:** un commit înregistrează o stare pregătită a proiectului ca o singură unitate de schimbări. Mesajul său explică munca, iar identificatorul său îl deosebește de alte commituri. Istoricul leagă aceste stări înregistrate; un log le afișează. Un pager este un vizualizator interactiv care poate afișa un log lung.
- **Neurmărit (untracked), urmărit (tracked), modificat, zona de pregătire (staging):** un fișier neurmărit există pe disc fără a fi gestionat de Git. Un fișier urmărit se află sub gestiunea lui Git. Modificat înseamnă că i s-au schimbat conținuturile. Zona de pregătire conține versiunile selectate pentru următorul commit; adăugarea unui fișier acolo și commit-ul lui sunt pași separați.
- **Identitate de autor și domeniu de configurare:** commiturile memorează un nume și o adresă de e-mail de autor. Domeniul de configurare stabilește dacă setările se aplică doar acestui depozit sau, cu `--global`, mai multor depozite pentru utilizatorul curent.
- **Sursă, C++, compilator, g++, executabil, rezultat al compilării:** sursa este textul lizibil al unui program, aici scris în C++. Un compilator îl traduce într-un executabil care poate fi rulat; `g++` este compilatorul folosit aici. Rezultatul compilării este ceea ce produce compilarea. **Arhitectura** este proiectarea de mașină vizată de compilare; **codul de mașină binar** este reprezentarea executabilă a instrucțiunilor. **Funcțiile din biblioteci** sunt operații reutilizabile folosite de un program. Byții și kiloocteții (KB) măsoară dimensiunea fișierelor.
- **.gitignore, regulă de ignorare, metacaracter (wildcard), extensie:** `.gitignore` specifică fișierele neurmărite care trebuie omise din adăugările obișnuite și din afișarea stării. Fiecare regulă descrie un nume sau un tipar. Metacaracterul `*` se potrivește cu un text variabil din numele fișierului; o extensie este un sfârșit precum `.exe`.
- **Diff, restore, checkout:** un diff afișează diferențele dintre versiunile unor fișiere. Restore înlocuiește fișierele de lucru dintr-o stare de referință aleasă. Checkout selectează un commit sau o ramură și face starea acestuia disponibilă în directorul de lucru.
- **Ramură (branch), master, indicator de ramură, commit comun:** o ramură denumește o linie de dezvoltare; indicatorul ei identifică commitul din vârful ei. `master` este numele ramurii principale folosit în această înregistrare. Un commit comun aparține istoricului ambelor ramuri înainte ca lucrările lor ulterioare să se separe.
- **Rebase, merge, commit de fuziune (merge commit), squash:** rebase reaplică schimbările proprii ale unei ramuri peste o altă bază. Merge unește linii de istoric; un commit de fuziune înregistrează explicit acea unire. Squash combină rezultatul mai multor commituri într-un singur commit.
- **Pull request, ramură sursă, ramură țintă sau de bază:** un pull request propune integrarea pe GitHub. Ramura sursă furnizează schimbările; ținta, numită și bază, este ramura care le va primi.
- **Nume remote, origin, URL, HTTPS, SSH:** un nume remote este o etichetă scurtă pentru adresa unui depozit. `origin` este eticheta uzuală pentru remote-ul principal; un URL este adresa sa. HTTPS și SSH sunt metode de conectare alternative.
- **Upstream, push, pull, autentificare:** un upstream asociază o ramură locală cu o ramură remote. Push trimite munca comitată; pull obține și încorporează munca din remote. Autentificarea stabilește permisiunea de acces la contul de găzduire, separat de autorul înregistrat într-un commit.

Exersează configurarea în [laboratorul Git](../../../labs/1_basic/03_git.md), ignoră fișierele generate, configurează autorul, comitează și conectează-te la GitHub.

Checkoutul inspectat conține acel laborator, dar niciun proiect sursă corespunzător demonstrației înregistrate. Prin urmare, exemplele de mai jos trimit la stări stabile din video, în loc să pretindă un anumit blob de depozit. Comenzile sunt reproduceri simplificate ale procedurii; căile, valorile de autor, mesajele și numele de ramură `experiment` sunt ilustrative. Schițele de sursă arată doar comportamentul stabilit de transcriere.

Domeniul lecției este o introducere cu caracter local, urmată de un ciclu de publicare pe GitHub și de un pull request. Se deschide ajutorul specific comenzilor, dar clonarea nu este demonstrată. Configurarea SSH, ramurile fără istoric comun și implementarea internă a stocării în Git sunt în afara exemplului lucrat.

## Verifică dacă Windows poate rula Git

Începe prin a afla dacă Git este deja utilizabil, în loc să-l instalezi fără verificare.

1. [00:00:06](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=6s) Apasă tasta Windows, caută `cmd` și deschide consola, fereastra în care introduci comenzi text.
2. [00:00:14](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=14s) Ține apăsat Ctrl în timp ce derulezi rotița mouse-ului pentru a schimba dimensiunea fontului în consola demonstrată. Mărirea textului face atât ceea ce introduci, cât și răspunsul mai ușor de urmărit.
3. [00:00:20](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=20s) Introdu doar comanda `git`. Un argument ar furniza informații suplimentare după numele programului; fără niciunul, testul verifică dacă consola poate lansa programul.
4. [00:00:29](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=29s) Încearcă `where git` ca a doua verificare. Comanda `where` din Windows caută un program după nume și îi raportează locația. Comparația din lecție este cu `which` din Linux, care servește unui scop de căutare asemănător.
5. [00:00:44](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=44s) Căutarea înregistrată nu găsește niciun program Git. Acest rezultat motivează pasul de instalare.

Verificările de disponibilitate prezentate la acele stări din video se reduc la:

```cmd
git
where git
```

## Instalează Git și găsește documentația comenzilor

1. [00:01:01](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=61s) Rulează instalatorul lui Git. Pentru această introducere de bază, instructorul acceptă opțiunile implicite în loc să modifice setările opționale.
2. [00:01:12](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=72s) Reîncearcă Git după instalare. În înregistrare, vechea consolă nu îl poate lansa în continuare; deschiderea unei console noi rezolvă problema. Dacă se întâmplă același lucru, testează într-o fereastră nouă înainte de a conclude că instalarea a eșuat.
3. [00:01:24](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=84s) Introdu `git` în fereastra nouă. Acum afișează ajutorul, o listă care explică comenzile disponibile.
4. [00:01:30](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=90s) Cere explicit ajutorul general cu `git --help`. O opțiune selectează comportamentul unei comenzi; opțiunea `--help` cere documentația.
5. [00:01:35](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=95s) Cere documentația unei anumite comenzi cu `git clone --help`. Clone copiază un depozit împreună cu istoricul său, dar această apelare doar se documentează. În instalarea prezentată, se deschide o pagină HTML, formatul de documente web citit de browser.

Acestea sunt cererile de ajutor corespunzătoare:

```cmd
git
git --help
git clone --help
```

Prima lansare reușită confirmă disponibilitatea; cererile de documentație arată cum să investighezi comenzile atunci când ai nevoie de ele.

## Înțelege ce rămâne local

[00:01:56](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=116s) Git, programul de gestionare a versiunilor, poate inițializa un depozit și îți poate înregistra munca fără niciun serviciu de internet. GitHub, serviciul de găzduire, folosește Git pentru a stoca depozite online. Un depozit este colecția înregistrată de stări ale proiectului și istoricul lor; **local** înseamnă că acea copie se află pe computerul tău.

[00:02:20](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=140s) Un **remote** este alt depozit la care te conectezi. GitHub este o gazdă, iar alți furnizori care acceptă protocolul lui Git—regulile de comunicare folosite pentru schimbul de date ale depozitului—pot fi folosiți la fel.

[00:02:33](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=153s) Munca online este amânată în acest punct. Află mai întâi ce se întâmplă în interiorul folderului de proiect. Intrările de istoric care urmează nu necesită nici cont de GitHub, nici încărcare.

## Transformă folderul proiectului într-un depozit

Proiectul are deja un fișier sursă. Inițializarea adaugă în jurul fișierelor de lucru stocarea pentru gestionarea versiunilor.

1. [00:02:48](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=168s) Navighează în folderul de proiect dorit înainte de a rula `git init`. **Directorul de lucru** conține fișierele editate. Rularea inițializării într-o locație fără legătură ar crea depozitul pentru acea locație.
2. [00:03:04](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=184s) Inspectează folderul după inițializare: a apărut un director ascuns `.git`.
3. [00:03:09](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=189s) Dacă Explorer îl ascunde, deschide opțiunile folderului, alege View și activează afișarea fișierelor ascunse.
4. [00:03:18](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=198s) Acest director `.git` stochează înregistrările locale ale lui Git, inclusiv istoricul stărilor fișierelor din momentele în care au fost comise.
5. [00:03:35](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=215s) Un **commit** salvează o unitate de schimbări ca stare a proiectului. Fișierele adăugate sau editate devin parte din starea înregistrată la acel pas.

O secvență simplificată de comenzi pentru starea de inițializare este:

```cmd
cd /d C:\path\to\project
git init
```

Înlocuiește calea cu locația reală a proiectului. Lecția explică la ce servește istoricul, fără a inspecta modul în care Git implementează intern stocarea.

## Pregătește un fișier sursă pentru primul commit

[00:03:54](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=234s) Începe cu `git status`, care raportează starea curentă a depozitului. Fișierul de lucru inițial este sursă C++; exemplele îl numesc `a.cpp`.

1. [00:04:04](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=244s) Statusul raportează că încă nu există commituri și listează sursa ca fiind neurmărită.
2. [00:04:11](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=251s) **Neurmărit** înseamnă că fișierul există în sistemul de fișiere, dar Git nu a început să îi gestioneze schimbările. Faptul că l-ai pus în acest folder nu a înregistrat o versiune.
3. [00:04:19](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=259s) Rulează `git add a.cpp`. Aceasta pune fișierul sub urmărire și copiază versiunea sa curentă în **zona de pregătire**, starea de pregătire pentru următorul commit.
4. [00:04:28](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=268s) Rulează din nou statusul. Fișierul apare acum ca nou la schimbări pregătite pentru commit, în timp ce depozitul nu are în continuare commituri.
5. [00:04:47](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=287s) Completează procesul cu un commit atunci când vrei o intrare de istoric la care să te poți întoarce. Add selectează o versiune pentru înregistrare; nu creează singur acea intrare.

Tranziția înregistrată poate fi urmărită cu:

```cmd
git status
git add a.cpp
git status
```

<details>
<summary>Precizează istoricul imediat după primul add: câte commituri există?</summary>

Încă zero. Fișierul a ajuns în starea de pregătire, dar niciun commit nu a salvat acea stare ca intrare de istoric.

</details>

## Rezolvă autorul lipsă și înregistrează fișierul

[00:05:07](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=307s) Următoarea acțiune este `git commit -m "Add source"`. Opțiunea `-m` furnizează un mesaj, iar ghilimelele păstrează împreună o descriere cu mai multe cuvinte, ca un singur argument. [00:05:17](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=317s) **Mesajul** este documentația memorată odată cu commitul. Ar trebui să îl ajute pe un cititor viitor să înțeleagă schimbarea; Git nu stabilește pentru tine corectitudinea descrierii.

O variantă scurtă a primei încercări înregistrate este:

```cmd
git commit -m "Add source"
```

[00:05:40](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=340s) Această încercare eșuează pentru că informațiile autorului nu au fost configurate. Eroarea sugerează setări de furnizat înainte de a încerca din nou.

[00:06:01](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=361s) **Identitatea de autor** este numele și e-mailul scrise în commit. E-mailul nu trebuie să fie adresa unui cont GitHub. [00:06:12](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=372s) Git acceptă adresa introdusă fără să verifice dacă persoana care o introduce o deține, așa cum explică lecția. Prin urmare, doar câmpul autor nu stabilește o identitate verificată.

[00:06:32](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=392s) **Domeniul de configurare** îi spune lui Git unde se aplică setările. `--global` creează valori implicite pentru utilizatorul curent în mai multe depozite, astfel încât un proiect ulterior poate folosi același nume și același e-mail. Nu înseamnă o setare pentru fiecare utilizator al computerului. [00:06:51](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=411s) Fără această opțiune, configurarea din acest depozit este locală pentru acest proiect.

[00:07:15](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=435s) Corectia demonstrată stabilește local atât numele, cât și e-mailul. Folosește-ți propriile valori în locul celor ilustrative:

```cmd
git config user.name "Example Author"
git config user.email "author@example.com"
```

Pentru domeniul mai larg discutat la [00:06:32](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=392s), alternativa este:

```cmd
git config --global user.name "Example Author"
git config --global user.email "author@example.com"
```

[00:07:25](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=445s) Reîncearcă commitul după configurare. Acum Git îl creează cu succes:

```cmd
git commit -m "Add source"
```

[00:07:37](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=457s) Intrarea de istoric se află acum în depozitul local `.git` al acestui proiect. Nimic nu a fost publicat doar prin crearea ei.

## Citește prima intrare de istoric

[00:07:54](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=474s) Primul commit păstrează sursa în starea selectată pentru acea înregistrare. Editările ulterioare ale fișierului de lucru nu vor înlocui această versiune anterioară. [00:08:06](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=486s) **Identificatorul** unui commit, numit și hash, deosebește starea salvată; mesajul său explică de ce există acea intrare.

[00:08:14](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=494s) Vezi **istoricul**, înregistrarea conectată a commiturilor, prin **log**. `git log` afișează identificatorii de commit, numele și e-mailurile autorilor, datele și mesajele. Împreună, acestea te ajută să recunoști versiunea dorită.

Verificarea istoricului la această stare din video este:

```cmd
git log
```

Proiectul are acum o versiune de sursă înregistrată, precum și copia editabilă din directorul de lucru.

## Construiește un executabil din sursa înregistrată

1. [00:08:40](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=520s) Compilează sursa cu `g++`. Un **compilator** traduce sursa lizibilă—aici text de program C++—într-un **executabil**, un program pe care sistemul de operare îl poate rula.
2. [00:09:04](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=544s) Alege un nume de fișier de ieșire. În comanda simplificată de mai jos, `-o` este opțiunea de ieșire, iar `main.exe` este argumentul ei de nume.
3. [00:09:15](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=555s) Lansează programul rezultat. Compilarea a produs un fișier care poate fi rulat, din sursă.

Compilarea și execuția demonstrate sunt reprezentate de:

```cmd
g++ a.cpp -o main.exe
main.exe
```

[00:09:26](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=566s) Acest executabil este **rezultat al compilării**, generat din sursă. De obicei păstrezi sursa în Git și recreezi rezultatul după nevoie. [00:09:37](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=577s) Cineva care folosește alt computer poate compila sursa pentru propria **arhitectură**, proiectarea de mașină pe care trebuie să o vizeze executabilul său.

[00:09:47](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=587s) Sursa lizibilă facilitează și revizuirea: Git poate compara liniile ei. Executabilul conține **cod de mașină binar**, instrucțiunile codificate pentru mașină, deci nu oferă aceeași explicație utilă linie cu linie a editărilor sursei.

[00:10:06](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=606s) Există și un cost de stocare. În acest exemplu sursa are 104 byți, în timp ce executabilul are aproximativ 60 KB. Byții și kiloocteții sunt unități de dimensiune; păstrarea doar a sursei care poate fi recompilată evită înregistrarea acestui rezultat mai mare. [00:10:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=621s) Lecția atribuie dimensiunea suplimentară de aici **funcțiilor din biblioteci** incluse, adică unor operații reutilizabile precum funcționalitatea de afișare în consolă pe care programul o apelează. Aceasta este o explicație a acestui exemplu, nu o descriere universală a dimensiunii fiecărui executabil.

[00:10:33](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=633s) Verifică statusul după compilare. Executabilul apare ca încă neadăugat: compilarea a creat un fișier pe disc, fără să-l pună în starea de pregătire sau să-l înregistreze.

```cmd
git status
```

## Dezvoltă și salvează reguli de ignorare

[00:10:40](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=640s) Noul executabil ar trebui să rămână un rezultat de compilare pe care îl poți regenera. Pentru a-l ține departe de adăugările obișnuite, creează `.gitignore`. O **regulă de ignorare** îi spune lui Git ce nume neurmărite să lase în afara listărilor obișnuite de stare și a pregătirii obișnuite.

1. Adaugă o regulă care numește primul executabil. [00:11:10](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=670s) Când verifici statusul, executabilul lipsește din listă; noul fișier de ignorare însuși rămâne vizibil.

   Aceasta este regula inițială și verificarea reprezentate de acele stări din video:

   ```gitignore
   main.exe
   ```

   ```cmd
   git status
   ```

2. [00:11:17](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=677s) Git recunoaște automat numele special `.gitignore` și interpretează liniile sale ca reguli. Nu rulezi acel fișier ca pe un program separat.
3. [00:11:29](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=689s) Compilează o a doua ieșire cu alt nume. O regulă care numește doar primul fișier îl lasă pe al doilea vizibil în status.

   O reproducere ilustrativă a acestei compilări intermediare este:

   ```cmd
   g++ a.cpp -o other.exe
   git status
   ```

4. [00:11:44](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=704s) Listează numele separate pe linii separate. [00:11:55](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=715s) După adăugarea celui de-al doilea nume, statusul nu mai listează nici acel executabil.

   Fișierul de ignorare intermediar este reprezentat de:

   ```gitignore
   main.exe
   other.exe
   ```

5. [00:12:03](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=723s) Înlocuiește numele cu un tipar cu extensie. Un **metacaracter** `*` se potrivește cu un text variabil, iar o **extensie** este sfârșitul numelui de fișier, aici `.exe`. O singură regulă acoperă acum ambele nume și alte fișiere neurmărite cu același sfârșit.

   Regula de înlocuire este:

   ```gitignore
   *.exe
   ```

6. [00:12:24](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=744s) Adaugă și comitează fișierul de ignorare. Executabilele generate rămân în afara proiectului înregistrat, în timp ce regula care descrie această politică devine parte din istoricul său.

   O secvență de înregistrare simplificată este:

   ```cmd
   git add .gitignore
   git commit -m "Ignore build outputs"
   ```

[00:12:49](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=769s) Logul arată acum intrări distincte pentru înregistrarea sursei și pentru configurarea ignorării executabilelor. Inspectează rezultatul cu:

```cmd
git log
```

Regulile discutate aici se aplică rezultatelor de compilare neurmărite. Exemplul nu elimină din istoric un executabil deja urmărit.

<details>
<summary>Explică de ce o regulă care numește main.exe nu a acoperit other.exe.</summary>

Prima regulă se potrivea unui anumit nume. Al doilea fișier are alt nume, deci avea nevoie de încă o linie. Regula ulterioară cu metacaracter le acoperă pe amândouă pentru că au același sfârșit .exe.

</details>

## Revizuiește o editare a sursei înainte de a o înregistra

[00:13:07](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=787s) Schimbă programul astfel încât să nu mai aștepte să păstreze consola deschisă după terminarea activității sale. Aceasta elimină două linii din sursă. Versiunea salvată le conține în continuare până când noua stare este comisa.

Afirmațiile C++ exacte nu sunt stabilite de transcriere. Această schiță de comportament, legată de acea stare din video, arată schimbarea cunoscută complet, fără să invente acele afirmații:

```text
Saved source:
    do the existing program work
    console-wait line 1
    console-wait line 2
    finish

Edited source:
    do the existing program work
    finish
```

1. [00:13:17](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=797s) Rulează statusul. Sursa este acum **modificată**, adică conținuturile acestui fișier urmărit s-au schimbat față de starea anterioară.
2. [00:13:25](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=805s) Inspectează un **diff**, o afișare a diferențelor, înainte de a înregistra editarea. `git diff` simplu compară fișierele de lucru cu versiunile lor pregătite. Aici editarea nu a fost pregătită, deci sursa pregătită coincide în continuare cu ultimul commit: comparația dezvăluie schimbarea față de versiunea salvată.
3. [00:13:50](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=830s) Diferența afișată confirmă eliminarea a două linii. Poți verifica editarea intenționată în loc să te bazezi doar pe cuvântul „modificat”.
4. [00:14:12](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=852s) Adaugă sursa editată în zona de pregătire și comiteaz-o. Un fișier urmărit folosește din nou `git add` atunci când vrei să pregătești o versiune nouă.

Pașii de revizuire și înregistrare sunt ilustrați împreună aici:

```cmd
git status
git diff
git add a.cpp
git commit -m "Remove the console waits"
```

Noua intrare de istoric conține sursa fără așteptări; sursa veche rămâne accesibilă prin commitul său anterior.

## Salvează mai multe fișiere și recuperează o ștergere nepregătită

1. [00:15:13](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=913s) Creează fișiere suplimentare într-un subfolder. Rezultatul de status afișat rezumă directorul neurmărit cu o singură intrare de folder, în loc să listeze imediat fiecare conținut al său.
2. [00:15:36](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=936s) Mută B și C în folderul proiectului. Statusul numește acum separat cele două fișiere.
3. [00:15:41](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=941s) Pregătește grupul cu `git add .`. Punctul desemnează directorul curent, deci fișierele sale noi și modificate sunt selectate împreună, sub rezerva regulilor de ignorare.
4. [00:16:07](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=967s) Comitește grupul pregătit. Mai multe fișiere pot aparține unei singure unități de schimbări.

Această secvență scurtă corespunde înregistrării cu mai multe fișiere:

```cmd
git status
git add .
git commit -m "Record B and C together"
```

[00:16:33](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=993s) Șterge fișierele comise din directorul de lucru. Copiile lor vizibile dispar, dar versiunile salvate rămân în Git. Ștergerea de pe disc nu a șters commitul care le conține.

[00:16:46](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1006s) **Restore** înlocuiește fișierele de lucru dintr-o stare de referință. Cu ștergerea demonstrată încă nepregătită, `git restore .` le reface din zona de pregătire, care aici păstrează conținuturile comise. Fișierele revin cu codul lor anterior.

Recuperarea de la această stare din video folosește:

```cmd
git restore .
```

<details>
<summary>Ce a făcut recuperarea posibilă: numele fișierelor sau conținuturile înregistrate anterior?</summary>

Conținuturile înregistrate. Git a putut returna versiunile anterioare pentru că acele fișiere fuseseră comise, iar conținuturile lor erau încă disponibile. Exemplul anulează o ștergere care nu a fost ea însăși pregătită sau comisa.

</details>

## Vizitează starea de dinaintea unei ștergeri comise

[00:17:52](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1072s) Repetă ștergerea, dar de această dată pregătește-o și comite-o. Un commit poate înregistra și absența, nu doar prezența: noua stare a proiectului nu mai include acele fișiere.

După ștergerea lor, operația demonstrată este reprezentată de:

```cmd
git add .
git commit -m "Record deletion of the earlier files"
```

[00:18:04](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1084s) Încearcă din nou `git restore .`. De data aceasta fișierele nu revin, pentru că starea curentă înregistrată și pregătită include ștergerea lor. Restore nu selectează automat o intrare de istoric anterioară.

```cmd
git restore .
```

Pentru a inspecta starea care le mai avea:

1. [00:18:18](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1098s) Afișează logul și copiază identificatorul unui commit de dinaintea ștergerii.
2. [00:18:44](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1124s) Apasă `q` dacă logul este deschis într-un **pager**, vizualizatorul interactiv al istoricului. Aceasta iese din vizualizator și îți redă controlul consolei.
3. [00:18:48](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1128s) Folosește **checkout**, care selectează starea unui commit sau a unei ramuri, cu acel identificator vechi. Fișierele revin pentru că acel commit selectat le conține.
4. [00:19:08](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1148s) Fă din nou checkout pe `master`. Aceasta este ramura principală folosită în lecție, iar commitul ei curent înregistrează ștergerea, deci fișierele dispar încă o dată.

Navigarea de mai jos folosește un substituent pentru hashul copiat:

```cmd
git log
git checkout OLD_COMMIT_ID
git checkout master
```

Commitul anterior a fost păstrat pe tot parcursul. Ce s-a schimbat a fost ce stare salvată a furnizat fișierele de lucru.

## Începe o linie de lucru separată

[00:19:30](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1170s) Pentru a dezvolta din starea mai veche, dă acelei lucrări propria ramură. O **ramură** denumește o linie de dezvoltare; **indicatorul** ei identifică commitul curent. Crearea uneia la acel commit vechi selectat oferă un loc cu nume de unde poate continua munca ulterioară.

[00:19:47](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1187s) Cazul care motivează abordarea este o funcție experimentală. Păstrarea ei pe o ramură separată îți permite să o investighezi independent de ramura principală.

1. Selectează commitul vechi care conține fișierele anterioare.
2. [00:20:01](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1201s) Creează și selectează o ramură cu `git checkout -b`, urmat de numele ei. Opțiunea `-b` cere o ramură nouă bazată pe starea curentă. Exemplele folosesc `experiment` drept nume ilustrativ.
3. [00:20:13](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1213s) Compară ramurile: experiment conține fișierele vechi, iar master conține ștergerea lor înregistrată. Listarea ramurilor confirmă și că ambele nume există.
4. [00:20:32](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1232s) Inspectează logul ramurii selectate pentru a-i verifica commiturile.
5. [00:20:39](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1239s) Comută folosind checkout, urmat de un nume de ramură.

O secvență simplificată pentru stabilirea și compararea acestor stări este:

```cmd
git checkout OLD_COMMIT_ID
git checkout -b experiment
git branch
git log
git checkout master
git log
git checkout experiment
```

[00:20:54](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1254s) Creează fișierul D pe experiment și înregistrează adăugarea lui. Transcrierea îl identifică mai târziu ca fișier text; exemplele următoare folosesc `d.txt`. Textul său inițial nu este stabilit, deci schimbarea semnificativă este adăugarea acestui fișier.

După crearea lui, secvența de comenzi scurtă este:

```cmd
git add d.txt
git commit -m "Introduce D"
```

[00:21:22](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1282s) Istoricul ulterior al ramurii experiment conține acum adăugarea lui D, iar istoricul ulterior al lui master conține ștergerea fișierelor vechi. Acestea sunt schimbări separate după starea lor comună anterioară. Nicio ramură nu a primit automat commitul nou al celeilalte.

## Identifică munca unică fiecărei ramuri

[00:21:35](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1295s) Înainte de a combina munca, identifică **commitul comun**, ultima intrare de istoric partajată de ambele ramuri. Schimbarea experimentală de dus înainte este cea care a urmat după acel punct comun: adăugarea lui D.

1. [00:22:03](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1323s) Citește logul experimental. El conține adăugarea lui D și nu conține commitul care șterge fișierele anterioare.
2. [00:22:12](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1332s) Comută pe master și citește-i logul. Ștergerea este prezentă acolo, iar adăugarea lui D lipsește.

Cele două verificări corespund la:

```cmd
git checkout experiment
git log
git checkout master
git log
```

Această comparație izolează munca nouă de pe fiecare parte. Mutarea experimentului ar trebui să adauge D fără să anuleze ștergerea deja înregistrată pe master.

## Mută experimentul pe istoricul mai nou al lui master

[00:22:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1341s) **Rebase** schimbă baza unei linii de lucru prin reaplicarea schimbărilor sale proprii după istoricul altei ramuri. Aici îl va face pe experiment să includă baza mai nouă a lui master înainte de a aplica adăugarea lui D.

[00:22:59](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1379s) Starea comună este commitul care a adăugat fișierele anterioare. De acolo, master a înregistrat ștergerea lor, iar experiment a înregistrat adăugarea lui D. [00:23:19](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1399s) Găsirea commitului comun separă trecutul partajat de aceste două acțiuni ulterioare. [00:23:31](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1411s) Secvența intenționată este acum: întâi ștergerea din master, apoi adăugarea lui D de către experiment.

Această schiță simbolică reprezintă explicația din video. Etichetele sunt descrieri, nu hashuri de commit:

```text
Before:
                  +-- delete earlier files       master
shared addition --+
                  +-- add D                      experiment

After experiment is rebased:
shared addition --> delete earlier files --> add D
                    master                  experiment
```

1. [00:23:56](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1436s) Comută pe ramura care trebuie actualizată și rulează acolo `git rebase master`. În acest exemplu, ramura este experiment.
2. [00:24:10](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1450s) Inspectează-i noul istoric. Acesta include acum schimbarea din master, urmată de adăugarea lui D de către experiment. Master indică în continuare commitul ștergerii, iar experiment indică adăugarea ulterioară.

Actualizarea demonstrată se rezumă prin:

```cmd
git checkout experiment
git rebase master
git log
```

[00:24:31](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1471s) **Limită de domeniu:** explicația presupune ramuri cu un commit comun. Înregistrarea menționează că ramurile fără unul sunt mai complicate, dar nu parcurge acel caz.

[00:24:39](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1479s) Istoricul partajat nu este repetat ca a doua copie a acelorași commituri în acest exemplu. [00:24:43](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1483s) Următoarea actualizare rulează din master, relativ la experiment. Deoarece istoricul experimentului include deja starea lui master, master poate avansa la același commit final. Cei doi indicatori de ramură identifică apoi aceeași stare.

Continuarea din înregistrare este reprezentată de:

```cmd
git checkout master
git rebase experiment
```

<details>
<summary>Imediat după rebase-ul experimentului peste master, care ramură conține adăugarea lui D?</summary>

Experimentul. Baza lui include acum ștergerea din master, dar master nu s-a mutat doar pentru că experimentul a fost rebazat. Actualizarea următoare a lui master face ca cele două vârfuri să coincidă.

</details>

## Alege modul în care munca ramurilor intră în istoricul principal

[00:25:01](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1501s) Fluxul recomandat păstrează `master` ca linie principală și pornește de la ea o ramură de lucru separată. Munca la o funcție nouă sau la un laborator se desfășoară pe acea ramură înainte de integrare.

[00:25:13](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1513s) Un **pull request** propune acea integrare pe GitHub. El aparține serviciului de găzduire, nu mulțimii de comenzi Git. **Ramura sursă** furnizează schimbările, iar **ținta**, numită și ramură de bază, le primește.

[00:25:45](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1545s) **Merge** unește istorice. Varianta explicată aici adaugă un **commit de fuziune**, o intrare care înregistrează explicit unirea celor două ramuri. [00:25:51](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1551s) Înainte de fuziunea în master, instructorul recomandă actualizarea ramurii de lucru prin rebazarea ei peste master. [00:26:06](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1566s) Motivul este ca ramura de lucru să conțină deja cele mai recente schimbări ale liniei principale, ceea ce face integrarea ulterioară mai simplă în acest exemplu. Acea pregătire nu garantează că orice combinație posibilă de schimbări va fi lipsită de conflicte.

Pregătirea descrisă în înregistrare este:

```cmd
git checkout experiment
git rebase master
```

[00:26:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1581s) **Squash** oferă o altă formă a istoricului: schimbările mai multor commituri sunt combinate într-un singur commit rezultat. [00:26:34](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1594s) Să presupunem că un commit adaugă un fișier și altul îl editează. Squash înregistrează rezultatul lor combinat într-o singură intrare, în loc să păstreze acele două intrări separat în istoricul țintă.

O schiță simplificată a acelei explicații este:

```text
Working branch:  commit adding file -> commit editing file
Squashed entry:  one commit with their combined result
```

Aceste alegeri influențează modul în care istoricul documentează munca. Pull request-ul demonstrat mai târziu folosește varianta cu un commit de fuziune suplimentar.

## Dă proiectului local o adresă remote

[00:26:56](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1616s) GitHub intră acum în flux ca loc de stocare a unui **depozit remote**, o copie online dincolo de computerul care găzduiește proiectul tău local. [00:27:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1641s) Aceasta face posibilă copierea proiectului pe altă mașină, comiterea muncii local, trimiterea ei prin GitHub și obținerea acelei lucrări pe alt computer.

Configurează conexiunea în această ordine:

1. [00:27:43](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1663s) Creează un depozit GitHub pentru proiect. Lecția îl descrie ca pe un folder online care conține Git.
2. [00:28:00](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1680s) Lasă-l gol pentru această primă conectare la un proiect local existent. Evitarea fișierelor precreate evită existența unei stări remote inițiale separate de reconciliat cu munca ta existentă.
3. [00:28:05](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1685s) Adaugă conexiunea cu `git remote add`.
4. [00:28:19](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1699s) Alege un **nume remote**, eticheta scurtă pe care comenzile o vor folosi pentru a căuta această conexiune. [00:28:25](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1705s) `origin` este convenția pentru remote-ul principal; numele este o alegere, nu o ramură specială.
5. [00:28:33](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1713s) Furnizează **URL-ul** depozitului, adresa sa. Adresa GitHub afișată acceptă fie versiunea care se termină în `.git`, fie versiunea fără acel sufix.
6. [00:28:42](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1722s) Folosește **HTTPS**, metoda de conectare aleasă pentru introducere. **SSH** este menționată ca alternativă care necesită configurare suplimentară, nedemonstrată aici.
7. [00:28:50](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1730s) Confirmă adresa configurată cu `git remote show origin`.

Pașii de conectare se reduc la acest exemplu; înlocuiește OWNER și REPOSITORY cu adresa ta:

```cmd
git remote add origin https://github.com/OWNER/REPOSITORY.git
git remote show origin
```

În acest punct intermediar, Git știe unde se află celălalt depozit. Adresa singură nu a trimis istoricul local acolo.

## Publică ramura principală cu un upstream

[00:29:04](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1744s) Prima încercare de `git push` raportează că ramura nu are **upstream**, asocierea dintre o ramură locală și omologul ei remote. **Push** trimite munca comisa; asocierea inițială le spune comenzilor scurte de push ulterioare unde trebuie să ajungă acea muncă.

1. [00:29:22](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1762s) Asociază master local cu master de la origin. Git caută adresa memorată sub numele remote `origin`; master identifică ramura de la acea destinație.
2. [00:29:58](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1798s) Urmează autentificarea din browser cerută de instalarea demonstrată. **Autentificarea** stabilește permisiunea de acces la serviciul de găzduire.
3. [00:30:15](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1815s) După ce autorizarea reușește, pagina GitHub afișează fișierele și istoricul încărcate. Aceasta este prima confirmare vizibilă că munca locală salvată a ajuns în remote.

Prima comandă de mai jos reprezintă încercarea eșuată; reîncercarea stabilește atât asocierea, cât și trimite munca:

```cmd
git checkout master
git push
git push --set-upstream origin master
```

[00:30:43](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1843s) Commiturile încărcate afișează un autor diferit de contul care deține depozitul. [00:31:00](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1860s) Aceasta este consecvența configurării locale a autorului. Autoria unui commit este numele și e-mailul salvate când commitul este făcut; autentificarea este permisiunea folosită pentru a-l trimite. Încărcarea nu rescrie acele câmpuri de autor ca să corespundă contului conectat.

<details>
<summary>Dacă faci push folosind altă autentificare GitHub, numele de autor înregistrat va deveni acea autentificare?</summary>

Nu. Commitul conține deja informațiile sale de autor. Autentificarea determină accesul la destinație, în timp ce commitul publicat păstrează informațiile de autor înregistrate local.

</details>

## Trimite un commit local ulterior

Următoarea editare face observabilă diferența dintre înregistrare și publicare.

1. [00:31:17](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1877s) Adaugă `world` în D și comitează local fișierul schimbat.
2. [00:31:33](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1893s) Aruncă o privire la GitHub înainte de a trimite ceva. Noul commit lipsește în continuare acolo, deși există în depozitul local.
3. [00:31:51](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1911s) Rulează `git push`. Noul commit apare apoi pe GitHub.

Această schiță reprezintă doar schimbarea de conținut cunoscută din înregistrare; textul și aspectul inițial complete nu sunt furnizate:

```text
D before: its existing text
D after:  its existing text with "world" added
```

Secvența simplificată de comenzi care o însoțește este:

```cmd
git add d.txt
git commit -m "Add world to D"
git push
```

Deoarece master are deja un upstream, push-ul său poate folosi asocierea memorată. Trimiterea commiturilor și actualizarea ramurii remote este o operație separată de crearea commitului; push-ul reușit de aici nu este o fuziune automată a unor istorice divergente oarecare.

## Publică separat următorul experiment

1. [00:32:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1941s) Revino la ramura experimentală și actualizeaz-o cu schimbările din master. Următoarea bucată de lucru va porni de la starea curentă a liniei principale.
2. [00:32:39](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1959s) Editează din nou D și comitează pe experiment. Aceasta creează o schimbare nouă de propus printr-un pull request, independent de master.

Pregătirea și înregistrarea sunt ilustrate mai jos. Editează fișierul între rebase și add; transcrierea nu stabilește textul nou exact:

```cmd
git checkout experiment
git rebase master
git add d.txt
git commit -m "Revise D in the experiment"
```

3. [00:32:59](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1979s) Încearcă să publici acea ramură. Ea nu a existat anterior în remote, deci are nevoie de propriul upstream. Asocierea stabilită pentru master nu configurează automat experimentul.
4. [00:33:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2001s) Stabilește upstream-ul experimentului și trimite-l. GitHub arată acum ramura experimentală publicată cu un commit înaintea lui master. Commitul suplimentar este editarea lui D tocmai înregistrată.

Prima publicare a acestei ramuri este reprezentată de:

```cmd
git push --set-upstream origin experiment
```

Remote-ul are acum două linii de lucru cu nume, linia experimentală purtând încă o schimbare.

## Aduce experimentul în master printr-un pull request

Folosește **pull request-ul** al serviciului GitHub, propunerea de a muta munca unei ramuri într-o țintă, pentru a integra experimentul publicat.

1. [00:33:46](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2026s) Deschide interfața de pull request și creează o cerere nouă.
2. [00:33:57](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2037s) Alege experiment ca **sursă** și master ca **țintă/bază**. Comparația afișează schimbările din experiment care lipsesc din master; această direcție stabilește ce ramură primește munca.
3. [00:34:10](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2050s) Creează cererea, apoi folosește acțiunea ei de fuziune. Schimbarea din experiment intră în master pe GitHub.
4. [00:34:28](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2068s) Ramura de lucru remote poate fi acum ștearsă: munca ei suplimentară a fost deja inclusă în master.
5. [00:34:39](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2079s) Inspectează logul remote al lui master. Acesta include commitul schimbării transferate și un commit de fuziune suplimentar care înregistrează integrarea prin pull request.

O privire simbolică asupra rezultatului înregistrat, folosind etichete în loc de hashuri reale, este:

```text
                     +-- D edit on experiment --+
earlier master state +-------------------------+-- PR merge
                                                     master on GitHub
```

Intrarea suplimentară de fuziune păstrează faptul că liniile s-au unit. Acesta este rezultatul demonstrat cu commit de fuziune, nu alternativa cu o singură intrare squash discutată mai devreme.

## Primește munca fuzionată pe acest computer

[00:35:01](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2101s) Comută pe master local după fuziunea de pe GitHub. Fișierele și logul său reflectă în continuare starea locală anterioară: schimbarea ramurilor local nu a descărcat commiturile remote.

[00:35:16](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2116s) Obține explicit acele schimbări, ca proiectul computerului să corespundă proiectului remote fuzionat.

1. [00:35:24](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2124s) Pe ramura locală dorită, rulează `git pull`. **Pull** preia schimbările remote și le încorporează în acea ramură locală, folosind conexiunea configurată.
2. [00:35:28](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2128s) Verifică rezultatul. D s-a actualizat, iar logul include acum cele două intrări primite—schimbarea experimentală și commitul de fuziune al pull request-ului.

Sincronizarea finală este reprezentată de:

```cmd
git checkout master
git pull
git log
```

Întregul ciclu a ajuns acum în fișierele locale de lucru: editarea a schimbat fișierul, add i-a selectat versiunea, commitul a înregistrat-o, push-ul a publicat-o, pull request-ul a integrat-o în remote, iar pull-ul a adus înapoi acel rezultat remote.

<details>
<summary>De ce selectarea lui master nu a arătat schimbarea fuzionată până la rularea lui pull?</summary>

Fuziunea de pe GitHub a schimbat depozitul remote. Master local a rămas la commitul său anterior până când munca remote a fost obținută și încorporată. Checkout a selectat starea locală deja disponibilă; pull a actualizat-o.

</details>

## Istoricul modificărilor de cod

Niciun commit inspectat nu conține proiectul demonstrației. Următoarele referințe la stări stabile din video urmăresc în ordine cronologică stările sale relevante salvate și publicate:

- [00:07:25](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=445s) Primul commit reușit înregistrează sursa inițială după configurarea locală a autorului.
- [00:12:49](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=769s) O a doua intrare de istoric înregistrează regulile de ignorare a executabilelor.
- [00:14:12](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=852s) O altă versiune a sursei elimină cele două linii de așteptare a consolei.
- [00:16:07](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=967s) B și C sunt înregistrate împreună, într-o singură unitate de schimbări.
- [00:17:52](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1072s) Un commit nou face ca ștergerea fișierelor anterioare să devină parte din starea salvată.
- [00:20:54](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1254s) Ramura creată din starea mai veche înregistrează adăugarea lui D.
- [00:24:10](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1450s) Rebase așază adăugarea lui D după ștergerea din master.
- [00:24:43](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1483s) Actualizarea următoare aduce master și experiment la același commit.
- [00:30:15](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1815s) Commiturile locale existente devin vizibile în depozitul remote.
- [00:31:51](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1911s) Commitul ulterior care adaugă world în D ajunge pe GitHub.
- [00:33:21](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2001s) Experimentul este publicat cu încă un commit de editare a lui D.
- [00:34:39](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2079s) Master remote conține schimbarea de lucru și commitul de fuziune al pull request-ului.
- [00:35:28](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=2128s) Master local primește schimbările fuzionate și actualizează D.
