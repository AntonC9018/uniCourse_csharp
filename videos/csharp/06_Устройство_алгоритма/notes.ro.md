> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Din ce este făcut un algoritm

[00:00:00](https://www.youtube.com/watch?v=C6plSGSYuyc&t=0s) O problemă de programare substanțială are, de obicei, nevoie de mai multe acțiuni coordonate. Această lecție dezvoltă o modalitate de a descoperi aceste acțiuni, de a descrie datele pe care le folosesc și de a transforma descrierea în instrucțiuni. Exemplul principal este găsirea lungimii medii a liniilor dintr-un fișier. Înmulțirea oferă un exemplu de pornire simplu; însumarea unei liste oferă piesa de construcție detaliată.

Vocabularul de mai jos este o prezentare anticipată. Fiecare idee este explicată din nou atunci când devine necesară:

- **Sarcină și algoritm:** o sarcină specifică un rezultat de obținut; un algoritm descrie cum se obține acesta din datele date. În această lecție, un program care rezolvă o sarcină este descris prin intrările, ieșirile și implementarea sa.
- **Intrare și ieșire:** intrarea este data furnizată unei sarcini; ieșirea este rezultatul pe care aceasta îl produce. O **reprezentare** este forma concretă folosită pentru a transporta acele date.
- **Interfață și implementare:** interfața descrie interacțiunea cu algoritmul, în special intrarea și ieșirea sa. Implementarea conține pașii care efectuează efectiv munca și stocarea lor temporară.
- **Descompunere și sub-algoritm:** descompunerea împarte o sarcină în sarcini mai mici. Un sub-algoritm este un algoritm folosit pentru a îndeplini una dintre acele sarcini mai mici. O **operație primitivă** este o operație deja disponibilă la nivelul de detaliu folosit.
- **Nivel înalt, nivel scăzut și abstrație:** o descriere la nivel înalt numește operațiile existente prin ceea ce realizează; o descriere la nivel scăzut le explică pașii mai mici. Abstrația permite ca un nume util să stea pentru un comportament fără a-i expune pașii interni.
- **Fișier, șir de caractere, caracter, linie și trecere de linie nouă:** un fișier stochează conținut; un șir de caractere reprezintă textul ca o succesiune de caractere. Un caracter este un element de text individual în modelul lecției. O linie este o porțiune din conținut separată de următoarea porțiune printr-o trecere la linie nouă.
- **Lungime și medie:** lungimea este numărul elementelor dintr-o succesiune sau numărul caracterelor dintr-un șir. Media de aici este media aritmetică: suma valorilor împărțită la numărul lor.
- **Tablou, element și indice:** un tablou este modelat ca celule de memorie consecutive care conțin valori, numite elemente. Un indice identifică un element prin decalajul său față de început; primul indice este zero.
- **Parcurgere și limite:** parcurgerea vizitează elementele unei colecții. Limitele specifică ce indici sunt valizi.
- **Variabilă și memorie de lucru:** o variabilă este o celulă cu nume a cărei valoare se poate schimba. Memoria de lucru este stocarea temporară pe care un algoritm o folosește în timpul execuției.
- **Buclă și sumă curentă:** o buclă repetă pași. O sumă curentă, numită și acumulator, stochează totalul acumulat până în acel moment.
- **Asociativitate, element neutru și reducere:** asociativitatea permite regruparea adunărilor fără a schimba rezultatul lor matematic. Zero este elementul neutru pentru adunare, pentru că adăugarea lui nu schimbă nimic. O reducere de la stânga la dreapta combină repetat un rezultat curent cu următorul element.
- **Atribuire și flux de control:** atribuirea scrie o valoare într-o variabilă. O atribuire compusă, precum `+=`, combină un calcul cu scrierea rezultatului înapoi. Fluxul de control determină ce instrucțiune urmează; un salt mută execuția la un marcaj cu nume, iar o buclă `for` împachetează execuția repetată.
- **Funcție, parametru și tip de retur:** o funcție împachetează instrucțiuni sub un nume. Un parametru primește intrarea pentru un apel; tipul de retur descrie felul rezultatului returnat. Un tip descrie ce fel de valoare ține o celulă sau un rezultat.
- **Buffer și felie:** un buffer ține o porțiune de date cât timp este procesată. O felie este o porțiune selectată dintr-o succesiune.
- **Vocabular opțional pentru exerciții:** o verificare de contract confirmă o condiție obligatorie; o aserțiune verifică o condiție așteptată, iar o excepție raportează un eșec. Acestea sunt amintite doar în materialul de studiu ulterior.
- **Obiect, adresă și referință:** un obiect grupează date conexe. O adresă identifică unde se află în memorie; o referință permite programului să ajungă la acel obiect printr-o valoare transmisă separat.

[Laboratorul Algoritmi](../../../labs/1_basic/11_algorithms.md) din depozit identifică această înregistrare. [Laboratorul de implementare în C#](../../../labs/1_basic/12_algorithms_code.md) este o temă de practică ulterioară, nu o implementare realizată în acest videoclip.

Înregistrarea se concentrează pe construirea și explicarea algoritmilor. Ea arată cum se mapează pașii de însumare în cod, dar nu creează, nu construiește și nu rulează niciun proiect. Împărțirea detaliată a fișierului, citirea cu buffere și un program complet de procesare a fișierelor rămân în afara scopului ei.

## Exemple de sarcini

[00:00:25](https://www.youtube.com/watch?v=C6plSGSYuyc&t=25s) O **sarcină** este ceva al cărui rezultat vrem să-l obținem. Gătirea borșului, însumarea unor numere, calcularea unei medii, măsurarea lungimii unui text dintr-un fișier și înmulțirea a două numere se încadrează toate în această descriere. Problema lungimii medii a liniilor combină mai multe dintre aceste idei mai mici. Un program poate rezolva o sarcină mare aranjând soluții pentru sarcini mai mici.

## Datele de intrare și de ieșire există chiar și atunci când nu sunt enunțate

[00:00:45](https://www.youtube.com/watch?v=C6plSGSYuyc&t=45s) Începeți prin identificarea **intrării**, informația pe care sarcina o primește, și a **ieșirii**, rezultatul pe care trebuie să îl furnizeze. Acestea sunt cele două părți comune ale fiecărui algoritm în modelul lecției.

[00:00:52](https://www.youtube.com/watch?v=C6plSGSYuyc&t=52s) O cerere scurtă poate lăsa ambele părți neenunțate. „Gătește borș” nu enumeră nicio intrare, dar gătirea cere totuși ceva asupra căruia să lucrezi. [00:01:04](https://www.youtube.com/watch?v=C6plSGSYuyc&t=64s) În funcție de locul de unde începe sarcina, acea intrare ar putea fi ingredientele sau banii necesari cumpărării lor. [00:01:16](https://www.youtube.com/watch?v=C6plSGSYuyc&t=76s) Desfăceți cererea înainte de a decide ce va consuma și ce va produce un program.

[00:01:25](https://www.youtube.com/watch?v=C6plSGSYuyc&t=85s) Exemplele fac distincția concretă:

| Sarcină | Intrare | Ieșire |
| --- | --- | --- |
| Gătește borș | Cel puțin ingredientele | Borș gătit |
| Însumă mai multe numere | Numerele | Suma lor |
| Calculează o medie | Valorile | Media lor |
| Găsește lungimea medie a liniilor într-un fișier | Fișierul | Un număr care descrie lungimea medie a liniilor |
| Înmulțește două numere | Două numere | Produsul lor |

[00:01:39](https://www.youtube.com/watch?v=C6plSGSYuyc&t=99s) Numerele fac exemplele sumei, mediei și produsului directe: valorile asupra cărora se operează sunt identificabile, iar răspunsul este o altă valoare. [00:02:01](https://www.youtube.com/watch?v=C6plSGSYuyc&t=121s) Specificarea unor asemenea date face comportamentul intenționat mai precis. „O sumă” sau „un produs” ne spune ce rezultat trebuie obținut, nu doar sugerează o activitate.

<details>
<summary>Ce returnează sarcina lungimii medii a liniilor: un fișier, o listă de linii sau un număr?</summary>

Un număr. Fișierul este intrarea; liniile sale și lungimile lor individuale sunt date intermediare folosite pentru calcularea rezultatului.
</details>

## Cum poate un fișier deveni date de intrare

[00:02:34](https://www.youtube.com/watch?v=C6plSGSYuyc&t=154s) Numirea intrării este doar începutul. **Reprezentarea** ei este forma concretă în care ajunge la program. [00:02:40](https://www.youtube.com/watch?v=C6plSGSYuyc&t=160s) Un număr poate fi furnizat direct ca **parametru**, adică o intrare acceptată de o operație apelabilă. „Un fișier” cere o deciție suplimentară despre ce anume să transmitem.

[00:02:57](https://www.youtube.com/watch?v=C6plSGSYuyc&t=177s) Două reprezentări duc la responsabilități diferite:

- Transmiteți un nume din sistemul de fișiere. Programul trebuie atunci să contacteze sistemul de fișiere, adică un context exterior datelor imediate ale algoritmului.
- Transmiteți o copie a conținutului, deja aflată în memorie. Programul primește informația, stocată până la urmă ca zerouri și unu, fără să fie nevoit să localizeze el însuși fișierul.

[00:03:18](https://www.youtube.com/watch?v=C6plSGSYuyc&t=198s) Ambele pot desemna același fișier. Alegerea unei reprezentări face parte din proiectarea programului.

[00:03:28](https://www.youtube.com/watch?v=C6plSGSYuyc&t=208s) Ingredientele au aceeași ambiguitate. Ele ar putea fi obiecte într-o lume virtuală, identificatori într-un joc, date stocate într-o bază de date sau bunuri reale cumpărate de la un magazin sau de la piață. Un **obiect** este aici o reprezentare de program a ceva din domeniul problemei.

[00:03:51](https://www.youtube.com/watch?v=C6plSGSYuyc&t=231s) Pentru o problemă complexă, înregistrați inițial intrarea la un nivel abstract: „ingrediente” sau „fișier”. Astfel descrierea rămâne utilă înainte de a fi alese detaliile concrete accidentale. [00:04:09](https://www.youtube.com/watch?v=C6plSGSYuyc&t=249s) Cerințele și proiectarea programului vor determina reprezentarea finală.

[00:04:31](https://www.youtube.com/watch?v=C6plSGSYuyc&t=271s) În această etapă descrierea privește datele de la margine — intrarea și ieșirea. Ea nu spune încă cum funcționează programul în interior.

## Interfață și implementare

[00:04:50](https://www.youtube.com/watch?v=C6plSGSYuyc&t=290s) **Interfața** este mijlocul de interacțiune cu un sistem. Pentru acești algoritmi, ea constă în ce intră și ce iese. Priviți programul ca pe o cutie: dați-i numere și primiți suma lor, sau dați-i ingrediente și primiți mâncarea pregătită. [00:05:00](https://www.youtube.com/watch?v=C6plSGSYuyc&t=300s) Puteți descrie acea interacțiune fără a specifica mecanismele interioare ale cutiei.

[00:05:12](https://www.youtube.com/watch?v=C6plSGSYuyc&t=312s) **Implementarea**, numită și realizare, este partea care efectuează efectiv sarcina pe datele furnizate. Interfața și implementarea descriu cele două fețe ale programului.

[00:05:23](https://www.youtube.com/watch?v=C6plSGSYuyc&t=323s) Un **algoritm** este procedura de rezolvare a unei sarcini descrisă prin datele sale de intrare, datele sale de ieșire și pașii concreți. [00:05:35](https://www.youtube.com/watch?v=C6plSGSYuyc&t=335s) Putem exprima implementarea sa ca o listă ordonată: ce trebuie să se întâmple cu intrarea pentru a produce ieșirea?

## Un prim algoritm: înmulțirea a două numere

[00:05:51](https://www.youtube.com/watch?v=C6plSGSYuyc&t=351s) Dați celor două intrări numele `A` și `B`, astfel încât instrucțiunile să se poată referi la ele fără ambiguități.

[00:06:04](https://www.youtube.com/watch?v=C6plSGSYuyc&t=364s) [Pașii de înmulțire înregistrați](https://www.youtube.com/watch?v=C6plSGSYuyc&t=364s) au această formă simplificată:

```text
input: A, B
1. Read A.
2. Read B.
3. Multiply the two values.
output: the product from step 3
```

1. Luați valoarea numită A.
2. Luați valoarea numită B.
3. Înmulțiți-le și folosiți rezultatul ca ieșire.

[00:06:21](https://www.youtube.com/watch?v=C6plSGSYuyc&t=381s) **Descompunerea** înseamnă împărțirea sarcinii în acțiuni mai mici. Aici un singur nivel este suficient: înmulțirea este deja o **operație primitivă**, o acțiune disponibilă direct la acest nivel. O problemă mai mare va necesita mai multe niveluri de descompunere.

## Descompunerea sarcinii lungimii medii a liniilor

[00:06:34](https://www.youtube.com/watch?v=C6plSGSYuyc&t=394s) Acum dați un nume intrării-fișier și considerați lungimea medie a liniilor sale. Înainte de a scrie instrucțiuni, identificați sarcinile mai simple care ar face posibil acest rezultat.

[00:06:54](https://www.youtube.com/watch?v=C6plSGSYuyc&t=414s) Descompunerea continuă împărțind fiecare pas dificil în altele mai mici. Până la urmă, o acțiune devine la fel de familiară ca înmulțirea numerelor, citirea unei valori sau transferul unei valori într-o altă celulă.

[00:07:14](https://www.youtube.com/watch?v=C6plSGSYuyc&t=434s) Acest proces are un punct de oprire practic: fiecare pas rămas trebuie să fie ceva ce știi cum să duci la îndeplinire. Nu trebuie să fie cea mai mică operație imaginabilă.

[00:07:23](https://www.youtube.com/watch?v=C6plSGSYuyc&t=443s) Un **sub-algoritm** este un algoritm existent folosit pentru o parte a unei soluții mai mari. Pentru a însuma numerele din mai multe liste:

1. Determinați suma primei liste.
2. Determinați suma celei de-a doua liste.
3. Determinați suma celei de-a treia liste.
4. Adunați acele sume.

[00:07:53](https://www.youtube.com/watch?v=C6plSGSYuyc&t=473s) Dacă însumarea unei liste este deja rezolvată, reutilizați acel algoritm la fiecare dintre primele trei pași. Nu există niciun motiv să-l dezvoltați de la zero în interiorul sarcinii mai mari.

## Analiza cerinței de medie

[00:07:57](https://www.youtube.com/watch?v=C6plSGSYuyc&t=477s) Reveniți la problema fișierului și întrebați-vă exact ce presupune „lungimea medie a liniilor”.

[00:08:07](https://www.youtube.com/watch?v=C6plSGSYuyc&t=487s) **Media** este media aritmetică a tuturor lungimilor individuale ale liniilor. Aceasta dă două acțiuni principale:

1. Obțineți lungimea fiecărei linii.
2. Calculați media acestor lungimi.

[00:08:22](https://www.youtube.com/watch?v=C6plSGSYuyc&t=502s) Prima acțiune are nevoie atât de liniile în sine, cât și de o operație care găsește lungimea unui **șir de caractere** dat, adică a unei succesiuni de caractere de text.

[00:08:51](https://www.youtube.com/watch?v=C6plSGSYuyc&t=531s) Mediazarea ar putea fi un sub-algoritm gata făcut. [00:09:03](https://www.youtube.com/watch?v=C6plSGSYuyc&t=543s) Pentru a-l dezvolta noi înșine, despărțim în însumarea valorilor și împărțirea acelei sume la numărul lor. [Descompunerea înregistrată a mediei](https://www.youtube.com/watch?v=C6plSGSYuyc&t=543s) poate fi rezumată fără sintaxă specifică unui limbaj:

```text
lengths = lengths of all the lines
total = sum of lengths
average = total / number of lengths
```

[00:09:07](https://www.youtube.com/watch?v=C6plSGSYuyc&t=547s) Împărțirea completează formula, dar operația „sumă” ascunde încă muncă.

[00:09:19](https://www.youtube.com/watch?v=C6plSGSYuyc&t=559s) Însumarea are nevoie de valori și de o modalitate de a le vizita. [00:09:34](https://www.youtube.com/watch?v=C6plSGSYuyc&t=574s) Fiecare valoare vizitată este adăugată la o **sumă curentă**, totalul obținut până atunci.

[00:09:38](https://www.youtube.com/watch?v=C6plSGSYuyc&t=578s) Conceptual, combinați primul și al doilea număr, adăugați-l pe al treilea la rezultat și continuați. Aceasta devine instrucțiunea „adaugă fiecare element la suma curentă”. [00:09:57](https://www.youtube.com/watch?v=C6plSGSYuyc&t=597s) Această instrucțiune este lăsată temporar nedescompusă, cât timp este examinată cealaltă ramură a problemei fișierului. Detaliile ei vor fi dezvoltate după ce au fost luate în considerare liniile și lungimile.

## Obținerea liniilor dintr-un fișier

[00:10:17](https://www.youtube.com/watch?v=C6plSGSYuyc&t=617s) Cealaltă ramură constă în obținerea liniilor și măsurarea lor. [00:10:30](https://www.youtube.com/watch?v=C6plSGSYuyc&t=630s) O **linie** este o porțiune din textul fișierului separată de următoarea porțiune printr-o **trecere la linie nouă**, adică o tranziție către o linie nouă.

[00:10:40](https://www.youtube.com/watch?v=C6plSGSYuyc&t=640s) Această definiție furnizează informația necesară extragerii liniilor: localizați tranzițiile din conținut.

[00:11:00](https://www.youtube.com/watch?v=C6plSGSYuyc&t=660s) Procedura simplă este:

1. Citiți în memorie întregul conținut al fișierului.
2. Găsiți trecerile la linie nouă.
3. Împărțiți conținutul la acele treceri.

[00:11:25](https://www.youtube.com/watch?v=C6plSGSYuyc&t=685s) Prima porțiune se întinde de la început până la prima trecere, următoarea de acolo până la a doua trecere, iar următoarea până la a treia. Aceste porțiuni sunt liniile individuale.

[00:11:45](https://www.youtube.com/watch?v=C6plSGSYuyc&t=705s) Citirea întregului fișier este un algoritm posibil, nu o cerință a oricărei procesări de fișiere. [00:11:53](https://www.youtube.com/watch?v=C6plSGSYuyc&t=713s) Sistemele mari pot citi în schimb porțiuni succesive până la o limită de linie sau pot folosi un **buffer**, o zonă delimitată care ține o porțiune de conținut. Bufferul poate fi analizat păstrând o stare între citiri. Această alternativă este amintită, dar nu este dezvoltată în mod deliberat.

[00:11:58](https://www.youtube.com/watch?v=C6plSGSYuyc&t=718s) Rămâneți la varianta cu întregul fișier: încărcați conținutul, găsiți-i tranzițiile și împărțiți acolo.

[00:12:10](https://www.youtube.com/watch?v=C6plSGSYuyc&t=730s) **Lungimea** unui șir de caractere este numărul de caractere pe care le conține. Un **caracter** este un element de text în acest model. Obținerea lungimii unui șir de caractere este o operație de bază furnizată de practic orice limbaj de programare, deci această parte nu mai are nevoie aici de un alt algoritm manual.

[00:12:22](https://www.youtube.com/watch?v=C6plSGSYuyc&t=742s) Aceeași metodă se aplică acum ambelor ramuri: despărțiți o componentă în componente mai mici până când acțiunile rămase sunt operații disponibile. [00:12:39](https://www.youtube.com/watch?v=C6plSGSYuyc&t=759s) Citirea conținutului unui fișier este tratată la acest nivel ca o asemenea primitivă disponibilă.

[00:12:49](https://www.youtube.com/watch?v=C6plSGSYuyc&t=769s) Căutarea trecerilor la linie nouă cere totuși parcurgerea caracterelor, dacă este dezvoltată manual. Însumarea cere, de asemenea, vizitarea valorilor și adăugarea fiecăreia la totalul curent. Sunt locuri de muncă diferite care împart aceeași nevoie de a construi o buclă de parcurgere; adăugarea unui caracter la o sumă numerică nu este testul pentru linie nouă. Construcția detaliată se întoarce acum la vizitarea valorilor unei liste.

## Tablouri și indici

[00:13:14](https://www.youtube.com/watch?v=C6plSGSYuyc&t=794s) Pentru a vizita fiecare valoare, decideți mai întâi cum sunt reprezentate valorile. Un **tablou** este modelat ca o regiune de memorie în care valorile sunt stocate consecutiv. Fiecare valoare stocată este un **element**.

[00:13:31](https://www.youtube.com/watch?v=C6plSGSYuyc&t=811s) În această imagine există o celulă pentru fiecare valoare. [00:13:53](https://www.youtube.com/watch?v=C6plSGSYuyc&t=833s) O altă bucată de memorie reține lungimea: câte valori există. [Explicația înregistrată a tabloului](https://www.youtube.com/watch?v=C6plSGSYuyc&t=811s) este reprezentată mai jos cu valori substituent, nu ca o transcriere exactă a ecranului:

```text
values:   [ a ][ b ][ c ][ d ]       length: 4
indices:    0    1    2    3
```

[00:14:10](https://www.youtube.com/watch?v=C6plSGSYuyc&t=850s) Un **indice** este poziția măsurată de la începutul tabloului. El ne spune câte elemente trebuie să sărim înainte de a citi elementul dorit. O literă precum i poate desemna indicele curent.

[00:14:23](https://www.youtube.com/watch?v=C6plSGSYuyc&t=863s) Primul element are indicele zero: nu sărim niciun element și citim valoarea de la început. [00:14:32](https://www.youtube.com/watch?v=C6plSGSYuyc&t=872s) Indicele trei înseamnă sărim elementele de la indicii zero, unu și doi, apoi citim al patrulea element. Indicele identifică poziția noastră curentă.

[00:14:58](https://www.youtube.com/watch?v=C6plSGSYuyc&t=898s) Trecerea la elementul următor înseamnă mărirea indicelui cu unu. Indicele unu sare un element; indicele doi sare două.

[00:15:30](https://www.youtube.com/watch?v=C6plSGSYuyc&t=930s) **Limitele** sunt indicii permiși. Pentru un tablou nevid cu N elemente, ele merg de la zero până la N minus unu. Ultimul element are N minus unu elemente înaintea lui, ceea ce explică scăderea.

[00:15:47](https://www.youtube.com/watch?v=C6plSGSYuyc&t=947s) Înregistrarea ajustează spațierea din jurul primei și ultimei celule, ca sfârșitul listării să fie mai ușor de diferențiat. Acea umplere este pentru lizibilitate; nu adaugă un element și nu schimbă un indice. [Explicația ultimului indice](https://www.youtube.com/watch?v=C6plSGSYuyc&t=960s) poate fi schițată astfel:

```text
          [ a ][ b ][ c ][ d ][ e ][ f ][ g ][ h ]
offset:     0    1    2    3    4    5    6    7   | end
```

[00:15:57](https://www.youtube.com/watch?v=C6plSGSYuyc&t=957s) Decalajul primei celule este zero; decalajul ultimei este numărul minus unu. [00:16:00](https://www.youtube.com/watch?v=C6plSGSYuyc&t=960s) Așadar, opt elemente se termină la indicele șapte. Aceasta este limita pe care parcurgerea o va folosi.

<details>
<summary>Un tablou conține opt valori. Ar trebui ca parcurgerea să citească indicele opt?</summary>

Nu. Indicii săi merg de la zero la șapte. Indicele opt este poziția de dincolo de ultima valoare, deci algoritmul trebuie să se oprească înainte de a încerca să o citească.
</details>

## Denumirea parcurgerii ca sub-algoritm

[00:16:19](https://www.youtube.com/watch?v=C6plSGSYuyc&t=979s) **Parcurgerea** înseamnă vizitarea elementelor colecției. Tratați-o ca pe un sub-algoritm propriu, astfel încât comportamentul ei să poată fi determinat separat de ceea ce se întâmplă cu fiecare valoare.

[00:16:34](https://www.youtube.com/watch?v=C6plSGSYuyc&t=994s) Pentru a vizita valorile în ordine, vizitați indicii lor în ordine și citiți elementul de la fiecare. Aceasta necesită un indice curent care se schimbă pe măsură ce execuția înaintează.

[00:16:40](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1000s) Patru reguli specifică acel indice curent:

1. Interpretați indicele ca numărul pozițiilor de elemente sărite de la început.
2. Porniți-l de la zero.
3. Măriți-l cu unu după procesarea unui element.
4. Opriți-vă când depășește ultimul indice valid.

[00:16:47](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1007s) Pornirea de la zero și avansarea cu unu vizitează elementele fără a lăsa goluri. [00:17:16](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1036s) Odată ce indicele este mai mare decât N minus unu, toți indicii disponibili au fost vizitați.

[00:17:32](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1052s) Această comparație furnizează regula de oprire; nu este nevoie de o numărare separată a iterațiilor. [00:17:48](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1068s) Indicele servește simultan două scopuri: localizează următoarea valoare și ne spune cât de departe a progresat parcurgerea.

## Scrierea parcurgerii ca pași

[00:18:01](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1081s) Acum transformați aceste reguli în **instrucțiuni**, adică acțiuni concrete pe care programul le va executa.

[00:18:17](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1097s) Indicele trebuie să supraviețuiască de la o instrucțiune la următoarea, deși se schimbă. O **variabilă** asigură acea stocare: este o celulă de memorie cu nume în care se poate scrie o valoare.

[00:18:28](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1108s) Creați celula și inițializați-o cu zero. Astfel, prima vizită încercată începe la începutul tabloului. [Construirea înregistrată a parcurgerii](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1108s) are următoarea formă intermediară simplificată. Operația de însumare curentă este încă un substituent în acest punct:

```text
create currentIndex
currentIndex = 0
check:
    if currentIndex > length - 1, finish
    value = element at currentIndex
    add value to the current sum       // storage for this comes next
    currentIndex = currentIndex + 1
    go back to check
```

[00:19:03](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1143s) Verificarea de sfârșit aparține înaintea primei citiri. Un tablou gol nu are indici valizi, deci este deja încheiat când indicele curent este zero.

[00:19:12](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1152s) Terminarea la acea verificare împiedică o citire dincolo de limite. [00:19:26](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1166s) Dacă indicele este valid, preluați elementul sărind numărul de poziții stocat în celula indicelui: zero la început, apoi unu, apoi doi și așa mai departe.

[00:19:37](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1177s) După adăugarea valorii preluate la suma curentă, treceți la elementul următor. [00:19:44](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1184s) Incrementarea indicelui — adăugarea lui cu unu — realizează acea deplasare.

[00:19:55](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1195s) Verificați din nou limitele după incrementare. Dacă indicele se află acum în afara tabloului, opriți-vă înaintea unei noi preluări.

[00:20:02](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1202s) O **buclă** repetă aceleași acțiuni până când condiția sa de oprire este îndeplinită. Procedura completă de parcurgere este deci:

1. Creați și inițializați variabila indicelui curent.
2. Verificați dacă indicele ei este încă valid; dacă nu, terminați.
3. Preluați elementul de la acel indice și executați asupra lui operația intenționată.
4. Măriți indicele cu unu.
5. Reveniți la verificare și repetați.

Acțiunile de preluare, adunare și incrementare reprezintă munca repetată. O singură trecere prin ele procesează o singură valoare; revenirea la verificare face din ele o parcurgere a întregului tablou.

## Adăugarea fiecărui element la suma curentă

[00:20:23](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1223s) Discuția de la pasul al șaselea revine la instrucțiunea amânată: adaugă fiecare element la suma curentă. Parcurgerea furnizează acum elementele, dar suma însăși are nevoie de o valoare de pornire și de stocare.

[00:20:41](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1241s) Pentru o listă goală, suma este definită ca zero. Inițializarea unui rezultat doar după citirea primului element ar lăsa acel caz fără răspuns.

[00:20:50](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1250s) Discuția indică spre **asociativitate**: în adunarea matematică, regruparea adunărilor nu schimbă rezultatul. De exemplu, combinarea primelor două valori și apoi combinarea rezultatului cu a treia dă aceeași sumă ca gruparea ultimelor două întâi. Rolul lui zero este mai precis cel de **element neutru** pentru adunare: adăugarea lui zero nu contribuie cu nimic. Asociativitatea explică regruparea; elementul neutru furnizează valoarea de bază pentru cazul vid.

[00:20:54](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1254s) Procesul de combinare de la stânga la dreapta al lecției este o **reducere**: combinați un rezultat cu următorul element, apoi folosiți acel nou rezultat în combinarea următoare.

[00:21:12](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1272s) Înlocuirea combinării inițiale cu o sumă de bază plus primul element transformă o sumă lungă în acțiuni uniforme. [00:21:22](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1282s) Porniți de la zero, adăugați prima valoare, adăugați a doua la rezultat și continuați cu a treia, a patra și a cincea. [Raționamentul înregistrat al sumei de bază](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1282s) poate fi scris astfel:

```text
base sum: 0
running sum after first element: 0 + first
running sum after second:       (0 + first) + second
continue by adding each next element to the preceding result
```

[00:21:36](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1296s) Parcurgerea deja dezvoltată ne spune cum să ajungem la fiecare element. Cerința suplimentară este să reținem valoarea acumulată între vizite.

[00:21:52](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1312s) O **sumă curentă**, sau acumulator, este o variabilă dedicată care ține acea valoare acumulată. [00:21:56](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1316s) Introduceți o celulă nouă pentru ea, conținând inițial suma de bază.

[00:22:08](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1328s) La prima trecere, adăugați primul element la zero și scrieți rezultatul înapoi în celula sumei. La fiecare trecere ulterioară, citiți totalul curent, adăugați următorul element și suprascrieți celula cu noul total.

[00:22:21](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1341s) În exemplul numeric, prima valoare dă cinci, iar următoarea cinci dă zece. Transcrierea salvată identifică apoi valoarea următoare ca nouă, producând nouăsprezece la [00:23:02](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1382s). Folosiți nouă la acest pas: zece plus patru ar da paisprezece, nu nouăsprezece.

[00:23:10](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1390s) Rezultatele intermediare desenate sunt noduri succesive ale procesului de adunare. Adăugând trei la nouăsprezece rezultă douăzeci și doi; adăugând opt rezultă treizeci. [Exemplul înregistrat de acumulare](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1390s) este rezumat aici:

```text
elements:       5, 5, 9, 3, 8
running sums:   0 -> 5 -> 10 -> 19 -> 22 -> 30
```

[00:23:22](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1402s) Celula sumei conține succesiv fiecare dintre acele rezultate intermediare. Când parcurgerea ajunge la nodul final, valoarea sa este totalul complet.

[00:23:39](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1419s) Combinați acumularea cu parcurgerea făcând ca fiecare vizită să adauge valoarea elementului în această celulă.

[00:24:00](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1440s) Modificarea pseudocodului anterior este mică, dar esențială: adăugați înaintea buclei un pas care creează celula sumei și scrie zero în ea. [Adăugarea înregistrată a acumulatorului](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1444s) produce această procedură completă de însumare, simplificată:

```text
create currentSum and write 0 into it
create currentIndex and write 0 into it
check:
    if currentIndex > length - 1, answer with currentSum
    currentSum = currentSum + element at currentIndex
    currentIndex = currentIndex + 1
    go back to check
```

1. Inițializați suma cu zero.
2. Inițializați indicele curent cu zero.
3. Dacă indicele a depășit ultima poziție validă, produceți suma stocată.
4. Altfel, adăugați elementul curent la sumă și stocați rezultatul.
5. Măriți indicele și reluați de la verificarea limitelor.

[00:24:21](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1461s) Pasul de terminare identifică acum explicit răspunsul: el se află în celula sumei curente. [00:24:45](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1485s) După ce toate elementele au contribuit o dată, acea celulă conține valoarea nodului final. Dacă nu ar fi existat elemente, ea ar conține în continuare suma de bază, zero.

<details>
<summary>Anticipați traseul pentru lista goală. Ce pași se execută și de unde vine răspunsul?</summary>

Algoritmul inițializează suma și indicele cu zero, apoi eșuează la prima verificare a limitelor, pentru că nu există niciun indice valid. Nu citește niciun element și returnează suma deja inițializată, zero.
</details>

## De la pași scriși la cod

[00:24:54](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1494s) O descriere a algoritmului suficient de concretă are un corespondent direct în cod. Creați o variabilă pentru suma curentă și atribuiți-i valoarea de bază.

[00:25:05](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1505s) Apoi creați variabila indicelui curent și inițializați-o cu zero. [00:25:18](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1518s) Verificarea dacă indicele este egal cu lungimea detectează prima poziție de dincolo de tablou. Aceasta este echivalentă cu depășirea lui N minus unu, pentru că această parcurgere pornește de la zero și avansează câte o poziție.

[00:25:33](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1533s) Adăugați elementul de la indicele curent la sumă, incrementați indicele și reveniți la verificare. [Traducerea înregistrată în cod](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1515s) motivează următoarea reconstrucție C# simplificată. Este un exemplu explicativ, nu o transcriere exactă a ecranului și nici o versiune de depozit pretinsă; presupuneți un tablou furnizat, numit values:

```csharp
int sum = 0;
int i = 0;

CheckIndex:
if (i == values.Length)
    goto Finished;

sum += values[i];
i += 1;
goto CheckIndex;

Finished:
; // Leave sum unchanged; it holds the answer.
```

Un **tip** descrie felul valorii stocate: `int` este folosit aici pentru exemplul mic cu numere întregi. O atribuire precum `i = 0` scrie o valoare în variabilă. `values.Length` citește numărul de elemente stocat; `values[i]` citește elementul identificat de indice. Atribuirea compusă `sum += values[i]` înseamnă „adaugă acest element la suma curentă și scrie rezultatul înapoi”. La fel, `i += 1` înlocuiește vechiul indice cu următorul. Un punct și virgulă încheie fiecare instrucțiune obișnuită. Marcajele cu nume urmate de două puncte identifică destinațiile salturilor; `goto` mută execuția la unul dintre ele. Instrucțiunea `if` efectuează verificarea de sfârșit înaintea oricărui acces la tablou. Punctul și virgula final, de sine stătător, este o instrucțiune vidă, astfel încât marcajul de final să aibă o destinație validă.

[00:25:58](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1558s) O **buclă `for`** împachetează inițializarea, condiția de continuare și actualizarea într-o singură construcție de buclă. Ea înlocuiește saltul manual înapoi, păstrând aceeași parcurgere. [Rafinarea înregistrată a buclei](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1558s) corespunde acestei versiuni simplificate:

```csharp
int sum = 0;
for (int i = 0; i < values.Length; i += 1)
{
    sum += values[i];
}
```

Între paranteze, prima parte inițializează indicele, a doua permite o nouă iterație doar cât timp indicele este valid, iar a treia îl avansează după corp. Punctele și virgulele separă cele trei părți. Acoladele grupează instrucțiunile repetate. Condiția de continuare este exprimată pozitiv, ca „indicele mai mic decât lungimea”; când acea condiție eșuează, execuția părăsește bucla.

[00:26:03](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1563s) Această traducere funcționează pentru că prosa specifică deja stocarea, inițializarea, verificarea, operația și actualizarea. Sintaxa limbajului oferă o modalitate directă de a exprima fiecare acțiune.

[00:26:08](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1568s) O **funcție** împachetează acele instrucțiuni sub un nume, astfel încât un alt algoritm să le poată apela. **Parametrul** ei descrie intrarea furnizată, iar **tipul de retur** descrie rezultatul. [Discuția înregistrată despre interfața unei funcții](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1568s) conduce la următoarea funcție completă simplificată:

```csharp
int Sum(int[] values)
{
    int sum = 0;
    for (int i = 0; i < values.Length; i += 1)
    {
        sum += values[i];
    }
    return sum;
}
```

Aici `Sum` numește comportamentul, `int[] values` declară un tablou de numere întregi ca intrare, iar `int` din față descrie rezultatul. `return sum` furnizează valoarea rămasă în acumulator. Denumirea comportamentului și declararea intrării și ieșirii sale îi dau algoritmului apelabil o **interfață**.

[00:26:14](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1574s) Același pas de proiectare a interfeței se aplică sub-algoritmului de medie: dați-i un nume, specificați valorile pe care le primește și specificați felul rezultatului pe care îl returnează. Implementarea sa poate folosi operația de însumare tocmai finalizată.

[00:26:23](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1583s) Operația de medie rămasă împarte suma la număr. [Calculul înregistrat al mediei](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1583s) este ilustrat matematic, nu printr-o alegere a tipurilor numerice din C#:

```text
Average(values):
    total = Sum(values)
    count = length of values
    return total / count
```

[00:26:31](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1591s) Numărul este deja stocat împreună cu tabloul. La acest nivel, acțiunile primitive rămase sunt citirea sumei, citirea lungimii și împărțirea. [00:26:38](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1598s) Câtul este răspunsul, ceea ce încheie sub-sarcina de medie.

Aceste mostre explică conversia de la pași la cod; nu sunt rezultate de compilare sau de execuție raportate. Rezultatul zero pentru o sumă vidă a fost dedus prin raționament, nu demonstrat printr-o rulare de test înregistrată. Înregistrarea nu stabilește o politică pentru intrare vidă la calcularea mediei: faptul că suma este zero nu definește prin el însuși împărțirea la un număr zero.

## Sub-algoritmul mai greu, desfășurat manual

[00:26:41](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1601s) Extragerea liniilor este partea deliberat mai grea a descompunerii. Discuția schițează cum să fie făcută manual, fără a completa o funcție de împărțire a fișierului.

[00:26:45](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1605s) O **felie** este porțiunea dintre pozițiile selectate dintr-o succesiune. Pentru a extrage linii succesive din șirul de caractere al conținutului:

1. Rețineți poziția la care începe linia curentă.
2. Examinați câte un caracter pe rând până când se ajunge la o trecere la linie nouă.
3. Luați ca linie felia de la poziția reținută până la acea limită.
4. Salvați poziția pentru linia următoare.
5. Continuați scanarea până la următoarea limită și repetați.

[00:27:01](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1621s) Fiecare acțiune din această schiță poate fi la rândul ei descompusă în pași de bază, la fel ca parcurgerea și acumularea. [00:27:17](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1637s) Principiile indexării și raționamentul obișnuit despre rezultatul cerut fac posibilă acea traducere; niciun mister nou nu separă procedura scrisă de cod.

[00:27:37](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1657s) Această lucrare detaliată este utilă atunci când o operație trebuie construită. Când există deja o funcție care însumează valorile, folosiți-o și lăsați acea parte a algoritmului de fișier la nivel înalt. Codul complet de scanare și politicile sale de margine nu sunt dezvoltate aici.

## Nivel înalt, nivel scăzut și abstrație

[00:27:55](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1675s) O descriere la **nivel înalt** folosește un algoritm existent după acțiunea pe care o îndeplinește. O descriere la **nivel scăzut** extinde acea acțiune în pașii ei primitivi. Ambele pot descrie același comportament la grade diferite de detaliu.

[00:28:02](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1682s) Procedura detaliată de însumare este la nivel scăzut față de „însumă această listă”: ea specifică variabilele, valorile lor inițiale, accesul la elemente, acumularea și actualizările indicelui.

[00:28:10](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1690s) La nivel înalt, este suficient să știm că există un algoritm de însumare și ce răspuns oferă. Un apelant nu trebuie să reanalizeze cum alocă celule temporare sau cum parcurge lista.

[00:28:55](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1735s) **Abstrația** este folosirea unei asemenea operații prin numele și semnificația ei, lăsând construcția sa internă în afara descrierii curente. [00:29:11](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1751s) Un nume util descrie ce se va întâmpla la nivelul la care sarcina mai mare este planificată.

<details>
<summary>De ce poate „însuma lungimile” să fie un pas complet într-o descriere, deși însumarea a necesitat mulți pași pentru a fi construită?</summary>

Odată ce există un algoritm de însumare și interfața lui este cunoscută, algoritmul mai mare îl poate apela ca pe o singură sub-sarcină. Implementarea sa efectuează în continuare lucrarea detaliată. Abstrația schimbă cât din acea lucrare trebuie să descrie apelantul.
</details>

## Desenarea unui algoritm ca celule de memorie

[00:29:15](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1755s) Descompunerea unui algoritm înseamnă specificarea interfeței sale, precum și a pașilor.

[00:29:30](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1770s) **Interfața** constă din intrare și ieșire. [00:29:38](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1778s) **Implementarea** constă din instrucțiunile concrete și din **memoria de lucru**: celule temporare precum indicele care se schimbă și suma curentă.

[00:29:47](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1787s) În imaginea de memorie, distingeți trei zone: celulele de intrare, celulele de ieșire și spațiul temporar folosit cât timp rulează algoritmul. [Modelul de memorie înregistrat](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1803s) este rezumat astfel:

```text
input                  working memory              output
[ a ] [ b ]            [ temporary sum ]            [ answer ]
```

[00:30:14](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1814s) Intrările sunt furnizate din exterior. Algoritmul nu alege conținutul lor; lucrează asupra datelor pe care le primește.

[00:30:19](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1819s) Pentru adunarea a două numere, celulele de intrare conțin valorile numite a și b. O celulă temporară poate conține suma lor, care este apoi livrată ca răspuns. Rezultatul exemplului este unsprezece. Aceasta distinge valorile furnizate din exterior, stocarea intermediară și ieșirea, fără a cere o dispunere fizică a memoriei pentru un anumit runtime.

## Transmiterea unei liste în practică

[00:30:42](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1842s) Desenul cu celule explică din ce este format un algoritm. Rămâne o întrebare practică: cum ajunge o listă întreagă la el?

[00:30:45](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1845s) În mod normal nu scriem fiecare element în poziții de intrare separate și apoi nu transmitem alături o lungime fără legătură cu ele.

[00:30:56](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1856s) În schimb, un **obiect** reprezintă lista și grupează elementele împreună cu lungimea sa. Obiectul se află la o **adresă**, o poziție în memorie. Celula transmisă poate conține valoarea care conduce la acest obiect — o **referință** în acest model explicativ. [Discuția înregistrată despre transmiterea unei liste](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1862s) poate fi schițată astfel:

```text
passed input cell                 list object's storage
[ address/reference ] ---------> [ elements ... | length ]

returned data can likewise be treated as separate memory
before the result is written to its destination
```

Elementele, lungimea și memoria folosită pentru datele returnate rămân părți distincte ale imaginii conceptuale. Transmiterea unei valori care identifică lista permite funcției să folosească colecția ca pe o singură intrare. Aceasta este o explicație a fluxului de date, nu o specificare a unei dispuneri exacte a obiectului.

## Sfera lecției și studiu suplimentar

[00:00:00](https://www.youtube.com/watch?v=C6plSGSYuyc&t=0s) Competența centrală este descompunerea unei sarcini netriviale și descrierea modului în care bucățile sale se potrivesc într-un program.

Limitele înregistrării sunt:

- [00:04:31](https://www.youtube.com/watch?v=C6plSGSYuyc&t=271s) Descrierea abstractă inițială a intrării/ieșirii lasă comportamentul intern nespecificat.
- [00:06:21](https://www.youtube.com/watch?v=C6plSGSYuyc&t=381s) Înmulțirea are nevoie în acest model de un singur nivel de descompunere.
- [00:09:57](https://www.youtube.com/watch?v=C6plSGSYuyc&t=597s) Adăugarea fiecărui element la suma curentă este amânată la început, apoi dezvoltată după ce parcurgerea este construită.
- [00:11:53](https://www.youtube.com/watch?v=C6plSGSYuyc&t=713s) Citirea pe bucăți și cea cu buffere sunt alternative amintite pentru sisteme mai mari; lecția urmează abordarea mai simplă, cu întregul fișier.
- [00:25:15](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1515s) Codul servește drept demonstrație a pașilor derivați. Nu există compilarea unui proiect și nici un test de execuție înregistrat.
- [00:26:41](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1601s) Împărțirea fișierului este schițată, nu implementată complet.
- [00:30:42](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1842s) Imaginea cu celule de memorie explică structura unui algoritm; ea nu impune o dispunere a memoriei pentru un anumit runtime.

**Practică suplimentară, nepredată în acest videoclip:** [laboratorul de implementare în C#](../../../labs/1_basic/12_algorithms_code.md) cere o funcție de algoritm care lucrează doar cu parametrii săi și nu afișează nimic. Tablourile, inclusiv tabloul de ieșire, sunt create de funcția principală și transmise ca argumente; algoritmul scrie răspunsul său în tabloul de ieșire, iar doar funcția principală afișează. Variantele de practică folosesc atât bucle `while`, cât și `for`. Aceste cerințe extind înregistrarea curentă într-un exercițiu de implementare.

Acel laborator ulterior propune și **verificări de contract** opționale, adică verificări că tablourile au lungimi acceptabile, folosind o aserțiune sau o excepție. O aserțiune verifică o condiție așteptată; o excepție raportează o eșec prin mecanismul de erori al limbajului. Aceste tehnici aparțin materialului ulterior. Practica suplimentară include și numărarea valorilor peste un prag, găsirea uneia sau a două cele mai mari valori, generarea numerelor prime sau calcularea numerelor Fibonacci. Nimic din toate acestea nu este necesar pentru a înțelege descompunerea din acest videoclip.

## Traseul codului din înregistrare

Legăturile de mai jos identifică, în ordine, stările constructive înregistrate. Mostrele din apropierea lor sunt reconstrucții simplificate; ele nu pretind o versiune exactă din depozit, fixată la un commit.

- [00:06:04](https://www.youtube.com/watch?v=C6plSGSYuyc&t=364s) — Denumirea celor două intrări și scrierea celor trei pași de înmulțire.
- [00:09:03](https://www.youtube.com/watch?v=C6plSGSYuyc&t=543s) — Exprimarea mediei ca suma împărțită la număr.
- [00:17:00](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1020s) — Specificarea celor patru reguli pentru indicele de tablou care se schimbă.
- [00:18:28](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1108s) — Păstrarea indicelui într-o variabilă și dezvoltarea parcurgerii cu verificarea limitelor.
- [00:24:04](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1444s) — Adăugarea celulei sumei inițializate înaintea buclei și identificarea ei ca rezultat.
- [00:25:15](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1515s) — Traducerea inițializării și a verificării de sfârșit în cod.
- [00:25:33](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1533s) — Adăugarea elementului indexat, incrementarea indicelui și saltul înapoi.
- [00:25:58](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1558s) — Înlocuirea controlului manual al parcurgerii cu o buclă `for`.
- [00:26:08](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1568s) — Împachetarea sumei într-o funcție cu nume, intrare și tip de retur.
- [00:26:23](https://www.youtube.com/watch?v=C6plSGSYuyc&t=1583s) — Reutilizarea sumei și a lungimii stocate pentru a completa calculul mediei.
