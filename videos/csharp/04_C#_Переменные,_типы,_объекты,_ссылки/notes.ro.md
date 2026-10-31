> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Variabile, tipuri, obiecte și referințe

[00:00:00](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=0s) Această lecție vă învață să preziceți ce afișează un scurt program C#, urmărind ce păstrează fiecare variabilă. Începeți cu un număr, suprascrieți-l, apoi construiți un model de memorie pentru numere și șiruri. La final, copiați o variabilă în alta și întrebați dacă modificarea sursei îl modifică și destinația.

Termenii de care aveți nevoie sunt:

- **Variabilă, identificator și celulă de memorie:** o variabilă este o locație de stocare cu nume; identificatorul ei este numele său în program; o celulă de memorie este cutia folosită pentru a desena acea stocare.
- **Valoare și tip:** o valoare este informația stocată sau copiată. Un tip precizează ce fel de valoare este permisă. `int` reprezintă numere întregi, `double` reprezintă numere în virgulă mobilă, iar `string` reprezintă text.
- **Declarare și inițializare:** o declarație introduce o variabilă împreună cu tipul și numele ei; inițializarea îi furnizează prima valoare.
- **Atribuire și citire:** atribuirea scrie o valoare într-o variabilă; citirea obține valoarea sa curentă. Atribuirea folosește `=`, cu destinația în stânga.
- **Instrucțiune și literal:** o instrucțiune este o comandă din program; un literal scrie o valoare direct în codul sursă, cum ar fi `123` sau `"hello"`.
- **Compilator și runtime:** compilatorul verifică și traduce codul sursă; runtime-ul susține programul în execuție și îi gestionează memoria.
- **Stivă și heap:** stiva este zona de memorie temporară folosită pentru cutiile variabilelor locale în desenele acestei lecții. Heap-ul este memoria dinamică folosită pentru obiecte, numit și *куча* în înregistrare.
- **Obiect, adresă și referință:** un obiect este o instanță care conține date; o adresă descrie un loc în memorie; o referință permite programului să ajungă la un obiect. Desenele reprezintă referințele ca adrese de obiecte.
- **Tip valoare și tip referință:** o variabilă de tip valoare conține chiar valoarea; o variabilă de tip referință conține o referință către un obiect.
- **Clasă:** un fel de tip ale cărui instanțe sunt obiecte; variabilele de tip clasă păstrează referințe către aceste instanțe.
- **Ieșire în consolă și apel de funcție:** ieșirea în consolă este textul afișat de program. Un apel de funcție cere executarea unei operații; `Console.WriteLine(...)` scrie reprezentarea textuală a valorii furnizate și încheie linia.

Câțiva termeni de precizie apar în clarificările de mai jos, nu ca teme suplimentare predate în videoclip:

- **Imutabilitate:** caracterele unui obiect de șir existent nu pot fi modificate.
- **Colectarea gunoiului (garbage collection):** gestionarea automată a memoriei, care poate recupera obiecte nefolosite și poate muta obiectele rămase în viu, actualizând referințele.
- **Internarea șirurilor (string interning):** reutilizarea unui obiect de șir pentru text identic; un literal nu trebuie să aloce un obiect nou de fiecare dată când linia sa este executată.
- **Notație de pointer și de adresă:** un pointer reprezintă o adresă de memorie. Operatorul `&` din C# preia adresa unei variabile în regulile pointerilor; nu este o modalitate generală de a obține referința gestionată a unui obiect de șir.
- **Închidere în cutie (boxing) și suprasarcină (overload):** boxing-ul convertește o valoare de tip valoare într-un obiect care conține o copie. O suprasarcină este o versiune a unei funcții care acceptă anumite tipuri de parametri.

Exersare conexă: [laboratorul despre variabile](../../../labs/1_basic/04_variables.md) vă cere să urmăriți stocajul, atribuirile și ieșirea. Include și teme dincolo de această înregistrare.

**Limita lecției:** obiectivul este să înțelegeți citirea, suprascrierea și atribuirea obișnuită. Înregistrarea folosește o imagine simplificată cu stiva și heap-ul și amână detaliile interne ale afișării și comportamentul mai avansat al referințelor. Nu a fost identificată nicio copie exactă a programului demonstrat din înregistrare. Exemplele de mai jos sunt reconstrucții simplificate, citate la stările lor video; exemplul din laborator este etichetat explicit ca fiind conex. Comentariile de ieșire sunt predicții bazate pe instrucțiunile prezentate, nu afirmații despre o nouă execuție locală.

## Citirea și suprascrierea unei variabile

[00:00:12](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=12s) Începeți declarând o variabilă numită `A` și inițializând-o cu `123`. O **declarare** introduce tipul și identificatorul; **inițializarea** oferă variabilei prima valoare. Aici `int` este tipul pentru numere întregi, `A` este identificatorul, iar `123` este un **literal** întreg, o valoare scrisă direct în sursă.

Stare inițială simplificată în videoclip:

```csharp
int A = 123;
Console.WriteLine(A); // 123
```

Punctul și virgula încheie fiecare **instrucțiune**, adică fiecare comandă. În **apelul de funcție** `Console.WriteLine(A)`, parantezele conțin valoarea de afișat. Scrierea lui `A` acolo **citește** valoarea curentă a variabilei; nu afișează litera A.

[00:00:27](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=27s) Rezultatul este `123` deoarece aceasta este valoarea din `A` când se execută instrucțiunea de afișare. Doar declararea unei variabile nu produce nicio ieșire în consolă.

[00:00:30](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=30s) Pentru ca aceeași variabilă să conțină un alt număr, adăugați o **atribuire**: puneți numele în stânga, `=` la mijloc și noua valoare în dreapta. Valoarea existentă este suprascrisă.

Stare modificată simplificată în videoclip:

```csharp
int A = 123;
A = 234;
Console.WriteLine(A); // 234
```

[00:00:41](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=41s) Următoarea ieșire este `234`. Atribuirea înlocuiește valoarea stocată; nu adaugă cifre și nu păstrează numărul anterior pentru următoarea afișare.

[00:00:45](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=45s) Ideea centrală este că o variabilă stochează o **valoare** de un anumit **tip**. Tipul descrie felul de informații permis, astfel încât operațiile ulterioare să îl poată folosi în mod previzibil. **Compilatorul**, care verifică și traduce sursa, impune compatibilitatea tipurilor.

[00:00:52](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=52s) Există două operații pe care trebuie să le țineți separate:

1. Citiți valoarea curentă folosind numele variabilei.
2. Actualizați-o atribuind acelui nume o valoare nouă, compatibilă.

<details>
<summary>Preziceți: dacă după atribuirea lui 234 urmează două afișări ale lui A, ce arată ele?</summary>

Ambele afișează 234. Citirea nu consumă valoarea și niciuna dintre afișări nu restaurează 123.
</details>

## Stiva și heap-ul

[00:01:05](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=65s) **Runtime-ul**, care susține programul cât timp rulează, gestionează memoria din spatele acestor operații. Lecția explică memoria folosind două zone.

[00:01:14](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=74s) **Stiva** este memorie temporară; **heap-ul** este memorie dinamică, numită *heap* sau *куча*. În modelul înregistrării, variabilele locale apar ca niște cutii pe stivă, iar obiectele de șir apar în heap. Aceasta transformă o declarație într-o întrebare concretă: ce trebuie să poată conține cutia acestei variabile?

Folosiți cutiile pentru a raționa despre valorile stocate și referințe. Amplasarea lor fizică este o simplificare didactică; distincția definitorie între tipurile valoare și tipurile referință este ce conține variabila, așa cum este descris în [referința C# despre tipurile valoare](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types).

## Declararea unei celule de memorie tipizate

[00:01:20](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=80s) O **celulă de memorie** este un singur slot de stocare din desen. O declarație scrie mai întâi tipul și apoi numele variabilei, introducând un slot capabil să conțină o valoare compatibilă cu acel tip.

Stare de declarare simplificată:

```csharp
int A;
string s;
// Aceste declarații, singure, nu furnizează valori de afișat.
```

[00:01:36](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=96s) Un slot `int` conține o valoare întreagă. Un slot `string` conține o referință către un obiect de șir, nu spațiu suficient pentru toate caracterele acelui obiect. Tipul determină felul de conținut cerut de slot.

[00:02:00](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=120s) **Identificatorul**, adică numele variabilei, este modul în care codul sursă ulterior se referă la acest stocaj. Numirea cutiei `A` este o convenție utilă de desen; nu înseamnă că runtime-ul caută într-un tabel de nume de text la fiecare citire.

## Numerele trăiesc în celulă

[00:02:21](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=141s) Pentru exemplele numerice, valoarea ajunge direct în propria cutie a variabilei. Citirea numelui ei obține acel număr.

[00:02:35](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=155s) Variabilele întregi și cele în virgulă mobilă se comportă la fel din acest punct de vedere: cutiile lor conțin valori numerice, nu referințe către obiecte numerice desenate separat.

[00:02:45](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=165s) Imaginați o celulă într-un tabel, cu un tip atașat. Atribuirile trebuie să fie compatibile cu acel tip. O celulă întreagă nu poate deveni o celulă de text doar pentru că o atribuire ulterioară încearcă să pună text în ea.

[00:03:02](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=182s) Următoarea construcție adaugă o celulă numerică numită `B`. În acest exemplu simplificat, `double` este un **tip în virgulă mobilă**, folosit pentru a reprezenta numere cu parte zecimală:

```csharp
double B = 12.5;
// Model: celula lui B conține valoarea numerică 12.5.
```

Punctul zecimal aparține literalului numeric. Declarația introduce `B`, iar inițializarea plasează valoarea în celula sa. Utilizările ulterioare ale lui `B` citesc acea valoare.

<details>
<summary>Explicați: de ce poate o variabilă int să fie suprascrisă cu alt întreg, dar nu cu un șir?</summary>

Suprascrierea schimbă valoarea, în timp ce tipul variabilei rămâne fix. O referință către un șir nu este o valoare întreagă.
</details>

## O variabilă de tip șir face referire la un obiect

[00:03:21](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=201s) Un **tip referință** schimbă ceea ce intră în variabilă. Valoarea sa este o referință prin care programul ajunge la un obiect. Conținutul obiectului trăiește separat de slotul variabilei.

[00:03:33](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=213s) În cazul lui `string`, textul aparține unui **obiect**, o instanță care conține datele caracterelor. Un **literal de șir** scrie text între ghilimele duble. Ghilimelele sunt sintaxă a sursei, nu parte din text.

Stare simplificată de declarare a șirului:

```csharp
string s = "hello";
// Model: s conține referința Rhello.
// Rhello ajunge la obiectul de șir care conține "hello".
```

[00:03:56](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=236s) Obiectul de șir reprezintă textul însuși. Celula variabilei conține referința, nu caracterele. În desenul lecției, inițializarea creează obiectul în memoria dinamică și pune adresa lui în variabilă.

Acest desen nu stabilește o alocare nouă la fiecare executare a unui literal. **Internarea șirurilor**, care poate reutiliza un obiect pentru text identic, este un detaliu de implementare în afara demonstrației. Consultați [documentația runtime-ului despre internarea șirurilor](https://learn.microsoft.com/en-us/dotnet/api/system.string.intern).

## Obiecte, adrese și referințe

[00:04:04](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=244s) O **adresă** identifică un loc în memorie. Diagrama atribuie fiecărui obiect o locație și folosește acea locație pentru a explica cum poate fi găsit.

[00:04:14](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=254s) O **referință** este valoarea care permite programului să ajungă la un obiect. Atribuirea unui șir la o variabilă de tip șir stochează acea referință; nu copiază caracterele obiectului în propriul slot al variabilei.

Model simplificat de adrese pentru declarația anterioară:

```text
variable cell       object in dynamic memory
s: Rhello  ------>  "hello"
```

Etichete precum `Rhello` sunt referințe simbolice, nu adrese de mașină măsurate. Deși înregistrarea tratează locul unui obiect ca fiind fix, obiectele gestionate se pot muta. **Colectarea gunoiului**, gestionarea automată a memoriei obiectelor de către runtime, actualizează referințele când comprimă memoria. Acest detaliu depășește videoclipul, dar împiedică interpretarea schiței ca garanție a unor adrese permanente. Consultați [gestionarea memoriei în .NET](https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/fundamentals).

## Un al doilea șir înseamnă o a doua variabilă

[00:04:42](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=282s) Introduceți o altă variabilă inițializată cu alt text. Modelul are acum un alt obiect într-o altă locație.

Stare simplificată cu două șiruri:

```csharp
string s = "hello";
string other = "world";
// s     conține Rhello -> "hello"
// other conține Rworld -> "world"
```

[00:04:57](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=297s) Fiecare declarație introduce propria celulă de variabilă, denumită prin identificatorul său. Cele două variabile pot conține referințe independente. O a doua variabilă nu impune în mod intrinsec un al doilea obiect: dacă i s-ar atribui referința primei variabile, ambele ar ajunge la același obiect. Aici textul diferit motivează două obiecte în desen.

## Denumiți celula și referința ei

[00:05:08](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=308s) Notația adreselor din explicația memoriei întreabă unde trăiește obiectul referit. Distingeți acea locație a obiectului de locația celulei de variabilă care conține referința sa.

Notația schematică de preluare a adresei nu este o rețetă C# pentru citirea adresei unui obiect de șir gestionat. Un **pointer** reprezintă o adresă; **operatorul de adresă** `&` din C# preia adresa unei variabile permise în regulile pointerilor. Nu întoarce pur și simplu referința gestionată a unui obiect de șir. Consultați [documentația C# despre operatorul de adresă](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/pointer-related-operators).

[00:05:19](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=319s) Crearea unei variabile noi introduce o celulă cu nume în model. Aceasta trebuie deosebită de atribuirea ulterioară: atribuirea către o variabilă deja declarată îi suprascrie conținutul și nu creează o altă celulă de variabilă.

[00:05:31](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=331s) Vocabularul are acum două părți distincte: **obiectul** este instanța care conține datele, iar **referința** este valoarea prin care o variabilă ajunge la acea instanță. O variabilă de tip șir conține partea a doua.

## Construiți exemplul de copiere a șirului

[00:05:51](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=351s) Întrebarea care motivează exemplul este dacă copierea lui `a` în `b` îl face pe `b` să urmărească modificările ulterioare ale lui `a`. Construiți două variabile, copiați prima referință în a doua, înlocuiți prima referință, apoi afișați a doua variabilă.

Această reconstrucție ilustrativă folosește `"привет"` pentru textul inițial și `"Hello 1"` pentru textul inițial al destinației. Literalul original exact este neclar în transcriere; următoarea urmărire este consecventă la nivel intern și nu revendică o stare exactă de ecran:

```csharp
string a = "привет";
string b = "Hello 1";
b = a;
a = "Hello 2";
Console.WriteLine(b); // привет
```

[00:06:15](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=375s) Înainte de a examina rezultatul, preziceți ce valoare anterioară supraviețuiește. Întrebarea privește referința curentă din `b`, nu orice literal apărut cel mai recent în sursă.

[00:06:21](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=381s) Înregistrarea trece de la propunerea unei rulări la explicarea rezultatului prin modificările memoriei. Următoarea urmărire oferă motivul; nu revendică un test executat separat.

## Executați cele două declarații

[00:06:38](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=398s) În desenul pas cu pas, cutia unei variabile este introdusă când se ajunge la declarația ei. Acesta este un model de execuție, nu o garanție că alocarea fizică pe stivă se întâmplă exact la acea linie de cod. O atribuire ulterioară către `b` se bazează pe declarația sa anterioară.

[00:06:46](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=406s) Urmăriți prima declarație și inițializare din exemplul anterior:

1. Introduceți `a`, al cărui tip permite referințe către obiecte de șir.
2. Reprezentați textul inițial, `"привет"` în această reconstrucție, ca pe un obiect în memoria dinamică.
3. Puneți referința acelui obiect, numită `Rfirst`, în `a`.

Stare intermediară:

```text
a: Rfirst  ---->  "привет"
```

[00:07:06](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=426s) Instrucțiunile obișnuite din acest exemplu se execută de sus în jos. Fiecare pas pornește de la starea lăsată de pasul precedent.

[00:07:17](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=437s) A doua declarație introduce `b` și îl inițializează cu o referință către obiectul care reprezintă `"Hello 1"`. Numiți acea referință `Rsecond`:

```text
a: Rfirst   ----> "привет"
b: Rsecond  ----> "Hello 1"
```

Există două celule de variabilă. În model există și două obiecte cu conținuturi diferite. Acestea sunt fapte distincte: celulele de variabilă și obiectele trebuie numărate independent.

## Copiați referința

[00:08:01](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=481s) Următoarea instrucțiune este o **atribuire**, o scriere în variabila destinație. Citirea variabilei din dreapta furnizează valoarea sa curentă. Pentru o variabilă de tip șir, acea valoare este o referință.

Stare simplificată după a treia linie:

```csharp
b = a;
// a: Rfirst -> "привет"
// b: Rfirst -> "привет"
```

[00:08:05](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=485s) Ambele celule conțin acum `Rfirst`. Referința anterioară din `b` a fost suprascrisă; atribuirea nici copiază datele caracterelor într-un obiect nou, nici nu face din `b` un nume alternativ pentru celula lui `a`.

Un **exemplu de exersare conex, deja încadrat**, nu starea exactă din înregistrare, apare în [laboratorul despre variabile la 52a1e276](https://github.com/AntonC9018/uniCourse_csharp/blob/52a1e27639e0b78319180bee2b1aa492667d8488/labs/1_basic/04_variables.md). Versiunea sa scurtă face vizibilă aceeași regulă de copiere:

```csharp
string a = "1";
string b = a;
a = "2";
Console.WriteLine(a); // 2
Console.WriteLine(b); // 1
```

<details>
<summary>Preziceți: după b = a, suprascrierea lui a îl suprascrie și pe b?</summary>

Nu. Atribuirea este o copiere unică a referinței stocate. Variabilele păstrează celule distincte, chiar dacă aceste celule conțin referințe către același obiect.
</details>

## Înlocuiți doar referința din a

[00:08:16](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=496s) Următoarea instrucțiune atribuie `"Hello 2"` lui `a`. Înregistrarea desenează un obiect nou la altă adresă, apoi suprascrie referința lui `a` cu referința către acel obiect.

Stare simplificată după a patra linie:

```csharp
a = "Hello 2";
// a: Rnew   -> "Hello 2"
// b: Rfirst -> "привет"
```

Urmăriți schimbarea în ordine:

1. Obțineți referința care reprezintă noul șir.
2. Scrieți-o în `a`, înlocuind acolo `Rfirst`.
3. Lăsați `b` neschimbat, conținând în continuare `Rfirst`.

Aceasta este o schimbare a referinței variabilei, nu o schimbare a caracterelor obiectului vechi. **Imutabilitatea** înseamnă că acele caractere nu pot fi modificate în obiectul de șir existent. Consultați [referința C# despre șiruri](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/reference-types). Reutilizarea literalelor poate influența momentul alocării, dar nu schimbă rezultatul atribuirii.

## Afișați prin referința lui b

[00:08:49](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=529s) Apelul final citește referința curentă a lui `b` și o furnizează operației de scriere în consolă.

Stare finală simplificată:

```csharp
Console.WriteLine(b); // привет în reconstrucția explicită de mai sus
```

[00:09:09](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=549s) Operația de ieșire folosește acea referință pentru a obține reprezentarea textuală a obiectului. Afișează șirul, nu adresa simbolică și nici textul ulterior din `a`.

Rezultatul este textul inițial pe care `a` îl avea când a rulat `b = a`. Dacă acel text inițial este `"Hello 1"`, rezultatul este `Hello 1`. Cu inițializarea explicită `"привет"` de mai sus, rezultatul este `привет`. Textul inițial `"Hello 1"` al destinației nu poate supraviețui suprascrierii prin `b = a`. Această distincție păstrează exemplul și ieșirea sa prezisă consistente.

[00:09:33](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=573s) Mecanismul exact din interiorul funcției de afișare este amânat intenționat. În această etapă, observația utilă este că ieșirea urmează referința păstrată în `b`.

## Comparați tipurile valoare și tipurile referință

[00:09:41](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=581s) Acum eliminați obiectele de șir separate din desen și folosiți numere întregi. Un **tip valoare** conține valoarea direct în variabilă; un **tip referință** conține o referință către un obiect. Prin urmare, urmărirea cu întregi necesită mai puține cutii.

[00:10:05](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=605s) Inițializați o variabilă întreagă cu `204`:

```csharp
int a = 204;
// Model: celula lui a conține 204.
```

[00:10:24](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=624s) Valoarea `204` intră chiar în celulă. Nu există niciun obiect desenat separat a cărui adresă să trebuiască copiată în această variabilă.

[00:10:40](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=640s) Exact de aceea `int` este un **tip valoare**: citirea variabilei obține valoarea sa întreagă.

[00:10:58](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=658s) Un **tip referință**, precum `string`, furnizează în schimb referința pe care o conține. Atribuirea obișnuită copiază acea referință.

[00:11:05](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=665s) O **clasă** definește un tip ale cărui instanțe sunt obiecte. O variabilă de tip clasă stochează, de asemenea, o referință către o instanță, nu direct conținutul instanței.

[00:11:18](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=678s) În acest exemplu obișnuit de declarare a unui întreg, nu este necesar niciun obiect separat pe heap pentru `204`. Aceasta este o afirmație despre exemplu, nu o regulă universală conform căreia întregii nu ar putea face parte niciodată din obiecte de pe heap. Definiția privește valorile stocate, nu o regiune fizică obligatorie de memorie. [Documentația C# despre tipurile valoare](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types) definește aceeași distincție.

## Copiați întregi și apoi modificați sursa

[00:11:28](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=688s) Refaceți demonstrația cu două variabile folosind valori întregi. Prima declarație introduce `A` și scrie în ea `204`.

[00:11:41](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=701s) A doua declarație introduce o celulă separată `B` și scrie în acea celulă `305`.

Demonstrație simplificată cu întregi:

```csharp
int A = 204;
int B = 305;
B = A;
A = 607;
Console.WriteLine(B); // 204
```

[00:11:54](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=714s) Executați copierea citind valoarea din dreapta și scrizând-o în variabila din stânga:

1. Inițial, `A` conține `204` și `B` conține `305`.
2. După `B = A`, ambele conțin `204`. Valoarea anterioară `305` din `B` este înlocuită.
3. După următoarea atribuire, se schimbă doar variabila numită în stânga acesteia.

[00:12:04](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=724s) Transcrierea descrie următoarea schimbare ca `A = 607`. [00:12:14](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=734s) Noua valoare înlocuiește valoarea anterioară din `A`. Starea intermediară este acum `A: 607` și `B: 204`. Punerea lui `607` în `B` ar fi un alt program și ar schimba ieșirea acestuia.

[00:12:22](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=742s) Regula de copiere este aceeași ca pentru șiruri: copiați o singură dată conținutul curent al sursei în destinație. Aici conținutul este reprezentat de valori întregi; mai devreme erau referințe. Niciuna dintre operații nu creează o legătură durabilă între celulele de variabilă.

<details>
<summary>Preziceți: ce ar conține B dacă lui A i s-ar atribui 607 înainte de B = A?</summary>

B ar primi 607, deoarece atribuirea citește valoarea sursei în momentul în care are loc copierea. În ordinea prezentată primește 204 și îl păstrează.
</details>

## Afișarea și limitele lecției

[00:12:30](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=750s) Înregistrarea amână mecanismul mai avansat din spatele afișării. [00:12:34](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=754s) Ea indică și către lucrări viitoare cu referințe, unde distincția dintre valorile stocate și referințele stocate devine importantă.

[00:12:37](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=757s) Explicația schițează afișarea numerelor întregi ca pe o conversie a valorii într-un obiect dinamic. [00:12:49](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=769s) Acest obiect este apoi transmis mai departe, iar tipul său este inspectat. Aceasta este explicația introductivă a înregistrării, nu o cerință conform căreia fiecare apel de afișare a unui întreg să împacheteze (să boxeze) mai întâi argumentul.

**Clarificare de precizie, dincolo de mecanismul demonstrat:** **boxing-ul** convertește o valoare de tip valoare într-un obiect care conține o copie, așa cum este documentat în [boxing în C#](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing). Totuși, o **suprasarcină** este o versiune a unei funcții care acceptă anumite tipuri de parametri, iar [`Console.WriteLine` are o suprasarcină pentru întregi](https://learn.microsoft.com/en-us/dotnet/api/system.console.writeline#system-console-writeline(system-int32)). Un simplu argument întreg o poate folosi direct.

[00:12:53](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=773s) Regula observabilă rămâne simplă: afișați valoarea stocată în acel moment în `B`. În reconstrucția explicită cu întregi, aceasta este `204`.

Lecția stabilește declarațiile, stocajul tipizat, citirea, suprascrierea, copierea obișnuită a valorilor și copierea referințelor. Lasă internele afișării și comportamentul avansat al referințelor pentru mai târziu. Colectarea gunoiului, reutilizarea literalelor, regulile pointerilor și selectarea suprasarcinilor de mai sus sunt clarificări de precizie, nu proceduri suplimentare de implementare predate în videoclip.

## Istoricul modificărilor de cod

Nu a fost identificat niciun commit exact al sursei lecției. Următoarele stări video cu marcaje de timp formează traseul codului, în ordinea construcției; exemplele de mai sus le reconstruiesc.

- [00:00:12](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=12s) — Declară și afișează `A` cu valoarea `123`.
- [00:00:30](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=30s) — Suprascrie `A` cu `234`; ieșirea urmează valoarea sa curentă.
- [00:01:20](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=80s) — Separă tipul unei variabile de numele ei; contrastează stocarea întregilor cu stocarea referințelor către șiruri.
- [00:03:02](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=182s) — Adaugă celula numerică `B`.
- [00:03:33](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=213s) — Introduce un obiect de șir și o variabilă care păstrează referința lui.
- [00:04:42](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=282s) — Adaugă o altă variabilă de șir și reprezintă alt text printr-un alt obiect.
- [00:05:51](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=351s) — Construiește demonstrația de copiere și reatribuire a două șiruri.
- [00:07:17](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=437s) — Inițializează a doua variabilă înainte de copiere.
- [00:08:01](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=481s) — Copiază referința lui `a` în `b`.
- [00:08:16](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=496s) — Înlocuiește doar referința lui `a` cu referința pentru `"Hello 2"`.
- [00:09:09](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=549s) — Explică ieșirea prin referința inițială păstrată în `b`.
- [00:10:05](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=605s) — Înlocuiește modelul cu referințe cu un întreg stocat direct într-o celulă.
- [00:11:28](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=688s) — Construiește variabile întregi independente inițializate cu `204` și `305`.
- [00:11:54](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=714s) — Copiază valoarea întreagă a lui `A` în `B`.
- [00:12:04](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=724s) — Atribuie `607` lui `A`, în timp ce `B` își păstrează valoarea copiată.
- [00:12:53](https://www.youtube.com/watch?v=wjsV9CvZfGI&t=773s) — Se încheie cu afișarea valorii curente din `B`.