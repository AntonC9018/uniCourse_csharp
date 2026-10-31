> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Refactorizarea distanței pe grilă prin blocuri de cod, apeluri și tipuri de poziție

[00:00:00](https://www.youtube.com/watch?v=z51K0UhriOk&t=0s) Această lecție pornește de la patru variabile de coordonate și transformă calculul repetat al distanței pe grilă într-o operație reutilizabilă. Fiecare schimbare răspunde unei probleme concrete: diferențe negative, nume temporare care se ciocnesc, instrucțiuni duplicate și argumente de coordonate prea ușor de amestecat. Comparația finală întreabă de ce modificarea unui câmp printr-un parametru de funcție se comportă diferit pentru o structură și pentru o clasă.

Vocabularul folosit pe parcurs este:

- **Coordonată și axă:** X și Y descriu o poziție de-a lungul a două direcții; o pereche se scrie (X, Y). **Distanța pe grilă** numără pașii orizontali și verticali dintre celule. **Valoarea absolută**, scrisă cu `Math.Abs`, păstrează modulul unui număr fără semnul său.
- **Variabilă, tip și `int`:** o variabilă dă nume unui loc de stocare pentru o valoare; tipul ei spune ce fel de valoare păstrează. Aici `int` păstrează coordonate și distanțe întregi. **Declararea** introduce o variabilă, **inițializarea** îi dă valoarea inițială, iar **atribuirea** copiază o valoare din dreapta în stocarea din stânga. Un **scalar** este o valoare simplă, precum un număr întreg. **Atribuirea definită** este cerința din C# ca o variabilă locală să primească o valoare înainte de a fi citită.
- **Expresie și valoare:** o expresie este cod care se evaluează la o valoare; o valoare este rezultatul concret care poate fi stocat. Un **rezultat intermediar** este o valoare obținută pe drumul către răspunsul final.
- **Bloc, domeniu de vizibilitate și variabilă locală:** acoladele delimitează un bloc; domeniul de vizibilitate stabilește unde poate fi folosit un nume. O variabilă locală aparține unui bloc sau unei funcții care o înconjoară. **Durata ei de viață** (lifetime) descrie momentul în care acea variabilă există în modelul de execuție al lecției.
- **Refactorizare și duplicare:** refactorizarea schimbă organizarea programului păstrând comportamentul intenționat. Duplicarea înseamnă menținerea mai multor copii ale aceleiași operații.
- **Funcție, apel, semnătură, parametru și argument:** o funcție grupează instrucțiuni; un apel le rulează. Semnătura ei descrie numele și intrările tipizate. Un parametru este variabila de intrare a funcției; un argument este expresia furnizată la apel. **Apelantul** face apelul, iar **cel apelat** îl execută.
- **Tip de returnare, valoare returnată, `return` și `void`:** tipul de returnare declară felul rezultatului; valoarea returnată este acel rezultat. `return` o furnizează și încheie execuția curentă a funcției. `void` declară că o funcție nu returnează nicio valoare. O **funcție locală statică** nu poate captura variabilele locale din jur.
- **Stivă, cadru de apel, heap și adresă:** desenul numește stivă zona de stocare temporară de lucru; un cadru (frame) reprezintă parametrii și variabilele locale pentru un apel. Stocarea dinamică a obiectelor este heap-ul. O adresă identifică o locație de stocare. Cutiile sunt un model didactic, nu o promisiune despre exacta dispunere în memorie.
- **Tip definit de utilizator, `Position`, struct, tip valoare și câmp:** un tip definit de utilizator dă un nume unei forme utile de date. `Position` grupează X și Y. O structură este un tip valoare a cărui valoare conține câmpurile sale; un câmp este o parte numită a acelei valori. `public` permite codului din afara tipului să acceseze un câmp. Un **initializer de obiect** stabilește câmpuri numite la crearea unei valori, iar **operatorul punct** selectează un câmp.
- **Transmiterea prin valoare, copie și efect secundar:** apelurile obișnuite copiază valoarea argumentului în stocarea parametrului. Un efect secundar este o modificare în afara datelor private de lucru ale funcției.
- **`ref`, alias și `out`:** transmiterea explicită a unei variabile cu `ref` face ca parametrul să fie o altă cale de acces la stocarea apelantului. `out` este un mod separat de parametru de ieșire; nu este introdus de urmărirea valorii returnate din această lecție.
- **Clasă, tip referință, obiect și referință:** o instanță de clasă este un obiect în stocare dinamică. O variabilă de tip referință păstrează o referință către acel obiect. Copierea referinței permite ca două variabile să ajungă la același obiect.

[Laboratorul despre funcții](../../../labs/1_basic/05_functions.md) oferă întrebări de continuare despre copiile parametrilor și expresiile returnate. [Laboratorul despre îmbunătățirea programului](../../../labs/1_basic/13_improvement_techniques.md) combină mai târziu gruparea datelor și extragerea funcțiilor cu tablouri și bucle; aceste tehnici din urmă sunt dincolo de această înregistrare.

Exemplele de mai jos sunt reconstrucții scurte ale stărilor din videoclipul asociat. Niciun commit de sursă inspectat nu conține această succesiune exactă de editări din clasă. Un exemplu conex, inspectat și fixat la un commit, este identificat separat spre final; nu este prezentat drept programul înregistrat.

## Coordonatele devin o numărare de pași pe grilă

[00:00:00](https://www.youtube.com/watch?v=z51K0UhriOk&t=0s) O **coordonată** este o componentă a unei poziții: schița inițială dă jucătorului X = 5 și Y = 6, iar unui inamic X = 3 și Y = 2. O variabilă dă numele stocării fiecărui număr, iar `int` este tipul întreg folosit aici. [Starea de cod din video](https://www.youtube.com/watch?v=z51K0UhriOk&t=0s) la început poate fi reprezentată astfel:

```csharp
int xPlayer = 5;
int yPlayer = 6;
int xEnemy = 3;
int yEnemy = 2;
```

[00:00:25](https://www.youtube.com/watch?v=z51K0UhriOk&t=25s) Sarcina este să numărăm prin câte celule ale grilei trebuie să treacă inamicul ca să ajungă la jucător. Această **distanță pe grilă** numără mutările de-a lungul axelor X și Y, nu o linie dreaptă prin plan.

[00:00:30](https://www.youtube.com/watch?v=z51K0UhriOk&t=30s) În exemplul desenat, ajungerea la jucător necesită două mutări în sus și una la dreapta. [00:00:46](https://www.youtube.com/watch?v=z51K0UhriOk&t=46s) Fiecare mutare contribuie cu o celulă, deci totalul este numărul orizontal plus cel vertical: 1 + 2 = 3. Ordinea acelor mutări nu schimbă această sumă.

[00:01:01](https://www.youtube.com/watch?v=z51K0UhriOk&t=61s) Citește perechile de coordonate cu X întâi, Y al doilea. Desenul folosește jucătorul (3, 1) și inamicul (2, 3). Este un set nou, ilustrativ, de coordonate, nu valorile de la început (5, 6)/(3, 2). Convenția pentru coordonate contează când decizi ce valori să scădem.

## Elimină direcția cu valoarea absolută

[00:01:23](https://www.youtube.com/watch?v=z51K0UhriOk&t=83s) Scăderea simplă păstrează informația despre direcție. Pentru desen, X al inamicului minus X al jucătorului dă 2 − 3 = −1. Inversând scăderea obținem +1; numărul pașilor orizontali rămâne tot unu.

[00:01:37](https://www.youtube.com/watch?v=z51K0UhriOk&t=97s) Distanța nu ar trebui să se schimbe când jucătorul trece de pe o parte a inamicului pe cealaltă. Semnul ne spune o direcție de care nu avem nevoie pentru această întrebare. **Valoarea absolută** elimină semnul: atât −1, cât și +1 au modulul 1.

[00:01:58](https://www.youtube.com/watch?v=z51K0UhriOk&t=118s) [Starea din video cu valoarea absolută](https://www.youtube.com/watch?v=z51K0UhriOk&t=118s) introduce `Math.Abs`. În acest fragment simplificat, cele două ordini de scădere dau distanțe egale:

```csharp
int xDistance = Math.Abs(xEnemy - xPlayer);
int sameDistance = Math.Abs(xPlayer - xEnemy);
```

[00:02:22](https://www.youtube.com/watch?v=z51K0UhriOk&t=142s) Aplică același raționament pentru Y: inamicul poate fi deasupra sau dedesubtul jucătorului. Ia diferența absolută pe fiecare axă, apoi adună cele două rezultate nenegative. Formula este |enemyX − playerX| + |enemyY − playerY|. Ea măsoară pașii pe axe ilustrați aici.

## Separă rezultatele pe axe și rulează primul test

[00:02:41](https://www.youtube.com/watch?v=z51K0UhriOk&t=161s) Păstrează separat contribuția fiecărei axe, ca calculul să poată fi urmărit. [00:02:45](https://www.youtube.com/watch?v=z51K0UhriOk&t=165s) O **declarare** introduce o variabilă cu nume; **inițializarea** îi stochează prima valoare. Noua variabilă pentru distanța pe X păstrează un rezultat intermediar care poate fi citit din nou la formarea totalului.

[00:02:50](https://www.youtube.com/watch?v=z51K0UhriOk&t=170s) Exemplul stabilește apoi jucătorul la (1, 1) și inamicul la (2, 3). Editările de coordonate și [starea calculului împărțit](https://www.youtube.com/watch?v=z51K0UhriOk&t=170s) sunt rezumate de această reconstrucție:

```csharp
int xPlayer = 1;
int yPlayer = 1;
int xEnemy = 2;
int yEnemy = 3;

int x_dist = Math.Abs(xEnemy - xPlayer);
int y_dist = Math.Abs(yEnemy - yPlayer);
int z = x_dist + y_dist;
Console.WriteLine(z);
```

[00:03:11](https://www.youtube.com/watch?v=z51K0UhriOk&t=191s) Prezice ieșirea înainte de a rula: distanță orizontală 1, distanță verticală 2, total 3. Cuvintele rostite „1, 2, 3” numără pașii și contribuțiile; programul afișează doar totalul `3`, nu trei valori de coordonate. Prima rulare este baza de comparație pentru schimbările structurale ulterioare.

## Evaluează expresia înainte de a-i stoca valoarea

[00:03:37](https://www.youtube.com/watch?v=z51K0UhriOk&t=217s) Urmează instrucțiunile de sus în jos. Urmărirea oferă mai întâi stocare lui `xPlayer`, apoi lui `yPlayer`, apoi celor două coordonate ale inamicului. Schițează apoi stocare temporară pentru calculul intermediar. Aceste cutii explică de ce valori sunt necesare, fără ca fiecare cutie desenată să fie neapărat o alocare fizică distinctă.

[00:04:00](https://www.youtube.com/watch?v=z51K0UhriOk&t=240s) O **expresie** este o bucată de cod care are o valoare. O **valoare** este rezultatul concret care poate fi scris în stocare. Partea dreaptă a atribuirii distanței este o expresie, nu o porțiune de algebră stocată.

[Starea de evaluare a expresiei](https://www.youtube.com/watch?v=z51K0UhriOk&t=263s) este captată de:

```csharp
// Cu xEnemy == 2 și xPlayer == 1:
int x_dist = Math.Abs(xEnemy - xPlayer);
// Citește valorile -> scade -> aplică Math.Abs -> stochează 1.
```

[00:04:23](https://www.youtube.com/watch?v=z51K0UhriOk&t=263s) Înlocuind variabilele și efectuând calculul, expresia se reduce la un număr. [00:04:43](https://www.youtube.com/watch?v=z51K0UhriOk&t=283s) Apelul funcției de valoare absolută trebuie să aștepte evaluarea scăderii interioare a argumentului. Un nume de variabilă din acea expresie înseamnă „citește valoarea stocată sub acest nume”.

1. [00:05:11](https://www.youtube.com/watch?v=z51K0UhriOk&t=311s) Citește 2 din variabila X a inamicului și 1 din variabila X a jucătorului.
2. [00:05:30](https://www.youtube.com/watch?v=z51K0UhriOk&t=330s) Evaluează 2 − 1. Două valori operanzi produc un rezultat intermediar, 1.
3. [00:05:39](https://www.youtube.com/watch?v=z51K0UhriOk&t=339s) Transmite acel rezultat lui `Math.Abs`. Rămâne 1. Dacă ordinea scăderii ar fi produs −1, `Math.Abs(-1)` ar da tot 1; nu dă 0.
4. [00:05:50](https://www.youtube.com/watch?v=z51K0UhriOk&t=350s) Stochează valoarea 1 returnată în `x_dist`.
5. [00:05:59](https://www.youtube.com/watch?v=z51K0UhriOk&t=359s) Introdu `y_dist`, evaluează |3 − 1| și stochează valoarea 2 returnată.
6. [00:06:25](https://www.youtube.com/watch?v=z51K0UhriOk&t=385s) Citește cele două valori salvate, adună 1 + 2, stochează 3 în `z` și afișează rezultatul.

Această separare între expresie și valoarea ei rezultată va explica și cum poate apărea un apel de funcție scris de om în partea dreaptă a unei atribuiri.

## A doua măsurătoare dezvălui duplicarea

[00:06:46](https://www.youtube.com/watch?v=z51K0UhriOk&t=406s) Cu un singur inamic, aritmetica apare o singură dată. O a doua entitate, precum un copac, creează încă o măsurătoare. Prima încercare este de a copia calculul care funcționează și de a ajusta intrările de coordonate.

[00:06:53](https://www.youtube.com/watch?v=z51K0UhriOk&t=413s) În [starea cu cod duplicat](https://www.youtube.com/watch?v=z51K0UhriOk&t=413s), versiunea lipită are nevoie de alte nume temporare, precum `x3`. Această reconstrucție abreviată presupune că variabilele de coordonate există deja; a doua pereche este corectată mai târziu:

```csharp
int x_dist = Math.Abs(xEnemy - xPlayer);
int y_dist = Math.Abs(yEnemy - yPlayer);
int distanceToEnemy = x_dist + y_dist;

int x3 = Math.Abs(xTree - xPlayer);
int y3 = Math.Abs(yTree - yPlayer);
int distanceToTree = x3 + y3;
```

[00:07:09](https://www.youtube.com/watch?v=z51K0UhriOk&t=429s) Ambele copii ocupă în prezent un singur **domeniu de vizibilitate**, regiunea în care numele lor de variabile pot fi folosite. Declararea aceluiași nume local de două ori acolo eșuează. Redenumirea evită această coliziune imediată, dar lasă două implementări aritmetice de întreținut.

## Folosește blocuri surori pentru a reutiliza numele temporare

[00:07:18](https://www.youtube.com/watch?v=z51K0UhriOk&t=438s) Un **bloc** este un grup de instrucțiuni înconjurat de acolade. Numele sale locale aparțin domeniului de vizibilitate al acelui bloc. Așază copiile în blocuri surori separate, ca ambele să poată folosi `x_dist` și `y_dist`.

[Starea de editare cu blocuri](https://www.youtube.com/watch?v=z51K0UhriOk&t=438s) este ilustrată cu perechea de coordonate anterioară încă în loc:

```csharp
{
    int x_dist = Math.Abs(xEnemy - xPlayer);
    int y_dist = Math.Abs(yEnemy - yPlayer);
    Console.WriteLine(x_dist + y_dist);
}
{
    int x_dist = Math.Abs(xTree - xPlayer);
    int y_dist = Math.Abs(yTree - yPlayer);
    Console.WriteLine(x_dist + y_dist);
}
```

[00:07:32](https://www.youtube.com/watch?v=z51K0UhriOk&t=452s) Acestea sunt variabile distincte, deși scrierea lor coincide. Numele primului bloc nu sunt disponibile în al doilea bloc, deci nu se ciocnesc. [00:07:52](https://www.youtube.com/watch?v=z51K0UhriOk&t=472s) Acoladele delimitează regiunea în care pot fi folosite variabilele temporare ale fiecărei copii.

[00:07:55](https://www.youtube.com/watch?v=z51K0UhriOk&t=475s) Parcurgerea memoriei lasă acum unele variabile locale de coordonate din jur în afara desenului stivei, pentru a face loc variabilelor de bloc. O cutie omisă nu înseamnă că variabila corespunzătoare a dispărut din program. Continuă să urmărești variabilele aflate în domeniu și valorile lor de-a lungul instrucțiunilor, chiar și când diagrama stivei nu le desenează.

## Urmează durata de viață a blocului fără a presupune adrese exacte

[00:08:18](https://www.youtube.com/watch?v=z51K0UhriOk&t=498s) O variabilă locală este o variabilă introdusă într-un bloc sau într-o funcție. În această parcurgere, **durata ei de viață** se încheie la acolada de închidere care o înconjoară: instrucțiunile ulterioare nu pot accesa numele locale ale acelui bloc.

[00:08:32](https://www.youtube.com/watch?v=z51K0UhriOk&t=512s) [Parcurgerea duratei de viață a blocului](https://www.youtube.com/watch?v=z51K0UhriOk&t=512s) poate fi redusă la aceste valori independente:

```csharp
{ int x_dist = 1; int y_dist = 2; }
{ int x_dist = 2; int y_dist = 3; }
```

1. Intră în primul bloc și urmărește declarațiile și valorile atribuite.
2. [00:08:52](https://www.youtube.com/watch?v=z51K0UhriOk&t=532s) Ajunge la acolada lui de închidere; variabilele locale ale acelui bloc nu mai sunt disponibile. Intrarea în blocul următor introduce variabile noi.
3. [00:09:14](https://www.youtube.com/watch?v=z51K0UhriOk&t=554s) Stocarea folosită pentru temporarele anterioare poate fi reutilizată. Un alt nume de variabilă nu presupune o altă adresă fizică.
4. [00:09:34](https://www.youtube.com/watch?v=z51K0UhriOk&t=574s) Distinge locul aparent din schiță de o locație garantată la runtime.
5. [00:09:46](https://www.youtube.com/watch?v=z51K0UhriOk&t=586s) Înregistrarea explică această reutilizare cu o zonă rezervată pentru datele locale de lucru ale unei funcții la pornirea ei: **cadrul ei de apel** (stack frame). Compilatorul poate aranja altfel stocarea reală.

Punctul didactic este că identitatea și vizibilitatea variabilelor urmează domeniile de vizibilitate ale programului. O acoladă nu este o promisiune că runtime-ul obține sau eliberează o bucată separată de memorie fizică exact în acel moment.

## Păstrează rezultatul în domeniul de vizibilitate din jur

[00:09:58](https://www.youtube.com/watch?v=z51K0UhriOk&t=598s) Încercarea de a afișa `x_dist` după blocul său eșuează, pentru că numele este în afara domeniului său de vizibilitate. Cutia sa desenată anterior nu îl face din nou accesibil.

[00:10:16](https://www.youtube.com/watch?v=z51K0UhriOk&t=616s) Ca să păstrezi răspunsul, declară rezultatul în domeniul de vizibilitate din jur și **atribuie** lui din interiorul blocului. Atribuirea copiază valoarea expresiei din dreapta în variabila din stânga. [Editarea rezultatului exterior](https://www.youtube.com/watch?v=z51K0UhriOk&t=616s) devine:

```csharp
int dist;
{
    int x_dist = Math.Abs(xEnemy - xPlayer);
    int y_dist = Math.Abs(yEnemy - yPlayer);
    dist = x_dist + y_dist;
}
Console.WriteLine(dist);
```

[00:10:34](https://www.youtube.com/watch?v=z51K0UhriOk&t=634s) `int dist;` este o declarație fără inițializator. Nu oferă o valoare de pornire utilizabilă. Atribuirea din interiorul blocului furnizează acea valoare; C# cere ca unei variabile locale să îi fie atribuită definitiv o valoare înainte de a fi citită.

[00:10:44](https://www.youtube.com/watch?v=z51K0UhriOk&t=644s) Blocul își calculează temporarul pentru Y și atribuie suma, 3, lui `dist`. Închiderea blocului încheie accesul la temporarele sale, dar lasă `dist` disponibil, deoarece declarația lui aparține domeniului de vizibilitate din jur. [00:10:59](https://www.youtube.com/watch?v=z51K0UhriOk&t=659s) Coordonatele de intrare rămân disponibile și în exterior; doar valorile de lucru interne sunt ascunse.

[00:11:05](https://www.youtube.com/watch?v=z51K0UhriOk&t=665s) Mută în exteriorul blocurilor lor locale de calcul rezultatele de care are nevoie codul ulterior. [00:11:17](https://www.youtube.com/watch?v=z51K0UhriOk&t=677s) Redenumește rezultatele generice astfel încât să arate ce măsurătoare păstrează fiecare, de exemplu `distanceToEnemy` și numele corespunzător pentru distanța până la copac. Într-un identificator C# folosește o scriere validă precum camelCase; expresia descriptivă „distanță-până-la-inamic” nu este ea însăși un identificator.

[00:11:30](https://www.youtube.com/watch?v=z51K0UhriOk&t=690s) Rularea după această rearanjare păstrează comportamentul anterior. Aceasta este **refactorizare**: schimbarea structurii cu păstrarea a ceea ce face calculul.

<details>
<summary>De ce poate fi afișat rezultatul exterior, dar nu și cele două temporare pe axe?</summary>

Rezultatul a fost declarat în domeniul de vizibilitate din jur și atribuit în interiorul blocului. Temporarele au fost declarate în interiorul blocului. Atribuirea nu mută o declarație și nu prelungește domeniul de vizibilitate al unui temporar.

</details>

## Identifică calculul comun și repară intrările lui

[00:11:39](https://www.youtube.com/watch?v=z51K0UhriOk&t=699s) Blocurile surori au rezolvat problema numelor: o declarație duplicată într-un singur domeniu de vizibilitate este invalidă, în timp ce blocurile separate pot reutiliza nume. [00:11:51](https://www.youtube.com/watch?v=z51K0UhriOk&t=711s) Ele nu au rezolvat **duplicarea**, repetarea aceleiași operații în mai multe locuri.

[00:12:01](https://www.youtube.com/watch?v=z51K0UhriOk&t=721s) Compară cele două copii. Aritmetica lor este identică. Diferențele semnificative sunt intrările de coordonate și variabila de rezultat care primește răspunsul. [00:12:11](https://www.youtube.com/watch?v=z51K0UhriOk&t=731s) Prima copie măsoară jucător–inamic; cealaltă folosește inițial copac–jucător.

[00:12:20](https://www.youtube.com/watch?v=z51K0UhriOk&t=740s) Înregistrarea corectează a doua măsurătoare: pentru distanța copac–inamic, folosește coordonatele inamicului în locul celor ale jucătorului. [Starea de corectare a intrărilor](https://www.youtube.com/watch?v=z51K0UhriOk&t=740s) este reprezentată de:

```csharp
int x_dist = Math.Abs(xTree - xEnemy);
int y_dist = Math.Abs(yTree - yEnemy);
distanceToTree = x_dist + y_dist; // Aici: de la copac la inamic.
```

[00:12:39](https://www.youtube.com/watch?v=z51K0UhriOk&t=759s) O funcție comună poate primi intrări diferite la fiecare apel. [00:12:41](https://www.youtube.com/watch?v=z51K0UhriOk&t=761s) Păstrează componentele de coordonate legate de entitățile lor reale: compară X al inamicului cu X al copacului și Y al inamicului cu Y al copacului. Amestecarea X-ului copacului cu Y-ul jucătorului nu descrie niciuna dintre perechile intenționate. Numele variabilei de distanță ar trebui să comunice și el măsurătoarea reală.

## Descrie o funcție cu intrări și un rezultat

[00:13:02](https://www.youtube.com/watch?v=z51K0UhriOk&t=782s) O **funcție** grupează o singură secvență de instrucțiuni. Un **apel** o rulează cu intrări alese: jucător și inamic, jucător și copac sau copac și inamic. Un **parametru** este o variabilă de intrare tipizată în funcție; un **argument** este expresia furnizată la locul apelului. Argumente diferite permit aceleiași implementări să măsoare perechi diferite.

[00:13:19](https://www.youtube.com/watch?v=z51K0UhriOk&t=799s) O funcție poate produce și o **valoare returnată**, rezultatul pe care îl înapoiază. Un apel care produce o valoare este o expresie. [00:13:33](https://www.youtube.com/watch?v=z51K0UhriOk&t=813s) Atribuirea valorii acelui apel la o variabilă înlocuiește calculul inline anterior.

[Starea de definire a funcției](https://www.youtube.com/watch?v=z51K0UhriOk&t=838s) introduce această formă simplificată; aritmetica completă urmează în secțiunea următoare:

```csharp
static int Distance(int xa, int ya, int xb, int yb)
{
    return Math.Abs(xa - xb) + Math.Abs(ya - yb);
}
```

Citește definiția în ordinea construcției ei:

1. [00:13:58](https://www.youtube.com/watch?v=z51K0UhriOk&t=838s) `static` face ca aceasta să fie o **funcție locală statică**. Ea nu poate captura variabilele locale din jur, deci intrările de coordonate trebuie să fie explicite.
2. [00:14:14](https://www.youtube.com/watch?v=z51K0UhriOk&t=854s) Înregistrarea recomandă acest mic obicei pentru a preveni dependențele accidentale de acele variabile locale din jur. Aici el protejează separarea pe care o construim.
3. [00:14:24](https://www.youtube.com/watch?v=z51K0UhriOk&t=864s) **Tipul de returnare** precizează felul rezultatului produs. [00:14:39](https://www.youtube.com/watch?v=z51K0UhriOk&t=879s) Alege `int` pentru că distanța în celule este un număr întreg.
4. [00:14:51](https://www.youtube.com/watch?v=z51K0UhriOk&t=891s) Scrie numele funcției, apoi parantezele care conțin lista ei de parametri. Acest nume și lista tipizată descriu **semnătura** funcției, interfața sa apelabilă.
5. [00:15:03](https://www.youtube.com/watch?v=z51K0UhriOk&t=903s) Oferă patru parametri: X și Y ale primului obiect, apoi X și Y ale celui de-al doilea. [00:15:18](https://www.youtube.com/watch?v=z51K0UhriOk&t=918s) Acestea sunt toate cele patru intrări de coordonate de care depinde calculul.

Interzicerea capturării variabilelor locale este sensul relevant al lui `static` în acest exemplu de funcție locală. O funcție statică nu are nici instanță implicită de obiect ale cărei câmpuri să le citească. Ea poate folosi în continuare un obiect furnizat printr-un parametru sau poate aloca explicit un obiect.

## Extrage aritmetica și înlocuiește cele două blocuri

[00:15:20](https://www.youtube.com/watch?v=z51K0UhriOk&t=920s) Mută aritmetica repetată în funcție. [00:15:31](https://www.youtube.com/watch?v=z51K0UhriOk&t=931s) Termenul ei pentru X compară cele două intrări X; termenul pentru Y compară cele două intrări Y. Fiecare termen folosește valoarea absolută înainte de a se forma suma.

[Starea funcției extrase](https://www.youtube.com/watch?v=z51K0UhriOk&t=920s) este reconstruită cu nume intermediare descriptive:

```csharp
static int Distance(int xa, int ya, int xb, int yb)
{
    int x_dist = Math.Abs(xa - xb);
    int y_dist = Math.Abs(ya - yb);
    int dist = x_dist + y_dist;
    return dist;
}
```

[00:15:40](https://www.youtube.com/watch?v=z51K0UhriOk&t=940s) `return` face ca valoarea expresiei care îl urmează să devină rezultatul funcției. Aici acea expresie citește numărul stocat în `dist`. [00:15:48](https://www.youtube.com/watch?v=z51K0UhriOk&t=948s) Folosirile ulterioare ale lui `return` funcționează după aceeași regulă; cuvântul cheie nu cere un mecanism nou pentru fiecare funcție.

[00:15:51](https://www.youtube.com/watch?v=z51K0UhriOk&t=951s) La primul apel, alege inamicul ca primă entitate, dându-i X primul și Y al doilea. [00:16:05](https://www.youtube.com/watch?v=z51K0UhriOk&t=965s) Completează argumentele cu X-ul jucătorului pe a treia poziție și Y-ul jucătorului pe a patra. [Starea apelului de înlocuire](https://www.youtube.com/watch?v=z51K0UhriOk&t=951s) se citește:

```csharp
int distanceToEnemy =
    Distance(xEnemy, yEnemy, xPlayer, yPlayer);
```

[00:16:18](https://www.youtube.com/watch?v=z51K0UhriOk&t=978s) Acest apel produce același număr ca blocul inline anterior. Aritmetica are o singură implementare, iar apelantul decide unde să stocheze fiecare răspuns.

[00:16:33](https://www.youtube.com/watch?v=z51K0UhriOk&t=993s) Reutilizeaz-o pentru distanța inamic–copac înlocuind perechea de coordonate a jucătorului cu cea a copacului. [Editarea celui de-al doilea apel](https://www.youtube.com/watch?v=z51K0UhriOk&t=993s) este:

```csharp
int distanceFromEnemyToTree =
    Distance(xEnemy, yEnemy, xTree, yTree);
```

Fiecare exemplu descrie o stare din video, nu un fișier sursă exact, versionat. Se presupune că variabilele de coordonate au fost declarate înaintea acestor apeluri.

## Intră într-un apel cu stocare nouă pentru parametri

[00:16:43](https://www.youtube.com/watch?v=z51K0UhriOk&t=1003s) Un **apelant** face un apel; **cel apelat** execută funcția cerută. Următoarea parcurgere explică cum rămân separate datele lor de lucru.

[00:16:56](https://www.youtube.com/watch?v=z51K0UhriOk&t=1016s) Exemplul distanței folosește valori întregi de lucru în stocare temporară. **Stiva** este numele folosit pentru acea zonă temporară, iar un **cadru de apel** grupează parametrii și variabilele locale care aparțin unui apel activ. În această versiune nu este nevoie de niciun obiect de poziție în stocare dinamică, adică în **heap**. Aceasta rezultă din datele acestui exemplu; `static` în sine nu garantează că un program nu folosește memorie heap.

[00:17:00](https://www.youtube.com/watch?v=z51K0UhriOk&t=1020s) Variabilele locale de coordonate existente sunt evidențiate sau omise din unele părți ale desenului, ca atenția să se focalizeze pe apel. Ele nu sunt eliminate prin apelarea funcției.

[Urmărirea copierii parametrilor](https://www.youtube.com/watch?v=z51K0UhriOk&t=1025s) poate fi urmărită folosind:

```csharp
int xEnemy = 2, yEnemy = 3;
int xPlayer = 1, yPlayer = 1;
int distanceToEnemy =
    Distance(xEnemy, yEnemy, xPlayer, yPlayer);
// Parametrii funcției apelate: xa=2, ya=3, xb=1, yb=1.
```

1. [00:17:05](https://www.youtube.com/watch?v=z51K0UhriOk&t=1025s) Schițează stocare pentru variabila de rezultat a apelantului înainte de a plasa acolo rezultatul calculat. Desenul poate arăta o celulă goală sau cu o valoare implicită. Aceasta nu autorizează citirea unei variabile locale C# neatribuite.
2. [00:17:07](https://www.youtube.com/watch?v=z51K0UhriOk&t=1027s) Pregătește stocare pentru parametrii funcției apelate înainte de a începe corpul ei.
3. [00:17:18](https://www.youtube.com/watch?v=z51K0UhriOk&t=1038s) Citește fiecare valoare de argument și copiaz-o în celula parametrului corespunzător. Parametrul este acum o variabilă locală obișnuită a cadrului acelui apel.
4. [00:17:27](https://www.youtube.com/watch?v=z51K0UhriOk&t=1047s) Copiază prima coordonată în prima celulă și a doua în a doua celulă. Potrivirea se face după poziția în lista de argumente, nu după asemănarea numelor de variabile.
5. [00:17:36](https://www.youtube.com/watch?v=z51K0UhriOk&t=1056s) Al treilea parametru primește valoarea X a jucătorului, 1. Celula lui nouă și celula originală a apelantului conțin numere egale, dar rămân stocări diferite.
6. [00:18:18](https://www.youtube.com/watch?v=z51K0UhriOk&t=1098s) Cu intrările legate, începe prima instrucțiune a corpului funcției.

## Calculează în noul cadru de apel

[00:18:23](https://www.youtube.com/watch?v=z51K0UhriOk&t=1103s) Funcția apelată are acum propriile valori de intrare. [Urmărirea calculului din funcție](https://www.youtube.com/watch?v=z51K0UhriOk&t=1103s) este reprezentată de:

```csharp
int x_dist = Math.Abs(xa - xb); // |2 - 1| = 1
int y_dist = Math.Abs(ya - yb); // |3 - 1| = 2
int dist = x_dist + y_dist;     // 3
```

1. Urmărește prima celulă locală, numită `dist` sau un temporar pentru distanța pe X în parcurgere: citirea intrărilor X dă |2 − 1| = 1.
2. [00:18:32](https://www.youtube.com/watch?v=z51K0UhriOk&t=1112s) Stochează 1, apoi calculează și stochează contribuția Y |3 − 1| = 2 în celula ei locală nouă.
3. [00:18:43](https://www.youtube.com/watch?v=z51K0UhriOk&t=1123s) Adună cele două contribuții. Diagrama rezervă încă o cutie pentru sumă, chiar acolo unde nu are un nume scris cu sens.
4. [00:18:52](https://www.youtube.com/watch?v=z51K0UhriOk&t=1132s) Continuă strict de la prima instrucțiune până la ultima. Aceasta identifică ce valori au fost create pentru acest apel înainte de a se încheia.

Un **rezultat intermediar** este o valoare folosită pentru a obține un rezultat ulterior. Cutia sumei și cutia scăderii vizualizează asemenea valori. Execuția compilată nu trebuie să dea fiecărei expresii intermediare propria variabilă permanentă cu nume.

## Întoarce o valoare și reia apelantul

[00:19:06](https://www.youtube.com/watch?v=z51K0UhriOk&t=1146s) Returnarea îndeplinește două acțiuni legate: produce valoarea apelului și oprește execuția funcției apelate. [00:19:33](https://www.youtube.com/watch?v=z51K0UhriOk&t=1173s) Controlul revine la apelant, la fel ca în cazul unui apel fără argumente, și continuă după ce apelul s-a încheiat.

[Starea transferului prin return](https://www.youtube.com/watch?v=z51K0UhriOk&t=1187s) poate fi citită ca această pereche de fragmente, din apelant și din funcția apelată:

```csharp
// Apelant:
int distanceToEnemy =
    Distance(xEnemy, yEnemy, xPlayer, yPlayer);
Console.WriteLine(distanceToEnemy);

// La finalul funcției Distance:
return dist;
```

1. [00:19:47](https://www.youtube.com/watch?v=z51K0UhriOk&t=1187s) Reamintește **atribuirea**: copiază valoarea expresiei din dreapta în variabila din stânga.
2. [00:19:59](https://www.youtube.com/watch?v=z51K0UhriOk&t=1199s) Evaluează expresia de după `return`. Citirea lui `dist` dă 3.
3. [00:20:08](https://www.youtube.com/watch?v=z51K0UhriOk&t=1208s) Expresia de apel din apelant are deci valoarea 3; atribuirea ei stochează acea valoare în `distanceToEnemy`.
4. [00:20:25](https://www.youtube.com/watch?v=z51K0UhriOk&t=1225s) Gândește întregul apel ca fiind înlocuit, pentru această atribuire, de numărul returnat. Se transferă valoarea, nu variabila funcției apelate.
5. [00:20:44](https://www.youtube.com/watch?v=z51K0UhriOk&t=1244s) Reia instrucțiunea următoare a apelantului după instrucțiunea care conține apelul. Nu continua prin definiția funcției ca și cum ar fi încă un bloc inline.
6. [00:20:52](https://www.youtube.com/watch?v=z51K0UhriOk&t=1252s) Parametrii și variabilele locale ale funcției apelate nu mai sunt disponibile când acel apel se încheie. [00:21:03](https://www.youtube.com/watch?v=z51K0UhriOk&t=1263s) Cadrul său temporar nu mai oferă nume pe care apelantul să le acceseze.
7. [00:21:07](https://www.youtube.com/watch?v=z51K0UhriOk&t=1267s) Variabilele de coordonate deja existente ale apelantului rămân accesibile; domeniul lor de vizibilitate nu s-a încheiat când funcția apelată a întors.
8. [00:21:14](https://www.youtube.com/watch?v=z51K0UhriOk&t=1274s) Afișează numărul păstrat în variabila de rezultat a apelantului.

Apelantul păstrează 3 chiar și după ce `dist` din funcția apelată nu mai există. Valorile egale nu implică faptul că două variabile sunt o singură variabilă.

## Urmărește al doilea apel și verifică ambele ieșiri

[00:21:19](https://www.youtube.com/watch?v=z51K0UhriOk&t=1279s) Începe următoarea declarare de rezultat cu încă o variabilă locală neatribuită în parcurgere. [00:21:24](https://www.youtube.com/watch?v=z51K0UhriOk&t=1284s) Din nou, evaluează expresiile argumentelor și copiază valorile lor în parametri noi ai funcției apelate. Sunt celulele parametrilor care primesc copiile argumentelor; celula rezultatului din apelant primește mai târziu răspunsul returnat.

[Urmărirea celui de-al doilea apel](https://www.youtube.com/watch?v=z51K0UhriOk&t=1294s) folosește aceeași funcție:

```csharp
int distanceFromEnemyToTree =
    Distance(xEnemy, yEnemy, xTree, yTree);
Console.WriteLine(distanceFromEnemyToTree);
```

[00:21:34](https://www.youtube.com/watch?v=z51K0UhriOk&t=1294s) Valorile inamicului se pot recupera ca X = 2 și Y = 3. Cifrele rostite fragmentat nu stabilesc în mod fiabil perechea completă de coordonate a celeilalte entități. Nu este nevoie să o inventăm pentru a explica mecanismul.

[00:21:42](https://www.youtube.com/watch?v=z51K0UhriOk&t=1302s) Calculează distanța locală și transferă valoarea ei în variabila țintă a apelantului. [00:22:06](https://www.youtube.com/watch?v=z51K0UhriOk&t=1326s) Este aceeași operație de returnare și atribuire ca la primul apel. Ea nu introduce un parametru `out` sau un argument `out`. `out` este modul separat de parametru în care funcția apelată furnizează o ieșire printr-un parametru; acel mod nu este demonstrat aici.

[00:22:25](https://www.youtube.com/watch?v=z51K0UhriOk&t=1345s) Când al doilea apel se încheie, variabilele sale locale ajung la final; răspunsul deja stocat în apelant rămâne. [00:22:32](https://www.youtube.com/watch?v=z51K0UhriOk&t=1352s) Rularea înregistrată afișează `3` urmat de `5`. Acestea sunt rezultatele a două calcule de distanță, nu componente X și Y. Ele verifică că extragerea operației comune păstrează calculele intenționate.

<details>
<summary>Cele două apeluri își împart variabilele de parametri pentru că apelează aceeași funcție?</summary>

Fiecare apel are propriii parametri și propriile variabile locale de lucru. Numele apelului anterior nu mai sunt disponibile când acesta s-a întors. Stocarea poate fi reutilizată, dar al doilea apel primește tot valori noi de argument și calculează un rezultat nou.

</details>

## Fă dintr-o poziție o singură valoare tipizată

[00:22:49](https://www.youtube.com/watch?v=z51K0UhriOk&t=1369s) Patru argumente de același tip invită la o greșeală discretă. Compilatorul acceptă oricare patru numere întregi, chiar dacă apelantul furnizează mai întâi toate valorile X, amestecă entități sau inversează o coordonată. [Problema ordinii argumentelor](https://www.youtube.com/watch?v=z51K0UhriOk&t=1369s) este ilustrată de:

```csharp
// Ordinea intenționată:
Distance(xEnemy, yEnemy, xPlayer, yPlayer);
// O ordine corectă ca tip, dar posibil greșită:
Distance(xEnemy, xPlayer, yEnemy, yPlayer);
```

[00:23:22](https://www.youtube.com/watch?v=z51K0UhriOk&t=1402s) Grupează X și Y ale fiecărei entități într-o singură valoare. Funcția poate cere apoi două poziții, nu patru numere fără legătură aparentă între ele. [00:23:24](https://www.youtube.com/watch?v=z51K0UhriOk&t=1404s) Un **tip definit de utilizator** este o formă cu nume pentru datele de care programul are nevoie; `Position` va reprezenta acea pereche de coordonate. Modificatorul `public` face câmpurile sale accesibile din codul exemplului înconjurător.

[00:23:45](https://www.youtube.com/watch?v=z51K0UhriOk&t=1425s) Declară tipul ca pe o **structură**, adică un tip valoare. Un **tip valoare** păstrează direct conținutul valorii în propria sa stocare; pentru această poziție, conținutul sunt cele două coordonate. În modelul variabilelor locale ele nu necesită un obiect heap referit separat. Aceasta descrie reprezentarea valorii, nu o afirmație universală că structurile nu pot apărea niciodată în stocare heap.

[Starea de definire a tipului](https://www.youtube.com/watch?v=z51K0UhriOk&t=1425s) este simplificată la:

```csharp
struct Position
{
    public int X;
    public int Y;
}
```

[00:24:08](https://www.youtube.com/watch?v=z51K0UhriOk&t=1448s) Folosește `Position` oriunde programul are nevoie de perechea întreagă, în loc de a reprezenta acea pereche prin două variabile `int` independente. [00:24:25](https://www.youtube.com/watch?v=z51K0UhriOk&t=1465s) În programul actual, cu un singur fișier și instrucțiuni de nivel superior, pune declarația tipului după instrucțiunile executabile și funcțiile locale. Înregistrarea o păstrează la finalul fișierului. Aceasta este aranjarea curentă a fișierului; nu impune ca orice structură din orice proiect să fie declarată ultima și nu interzice alte fișiere sursă.

## Construiește o Position și numește-și câmpurile

[00:24:40](https://www.youtube.com/watch?v=z51K0UhriOk&t=1480s) Construiește o variabilă folosind noul tip. Un **initializer de obiect** este lista de atribuiri de câmpuri numite, încadrată în acolade și atașată creării; el face vizibilă perechea X/Y.

[Starea de construire a poziției](https://www.youtube.com/watch?v=z51K0UhriOk&t=1480s) introduce forma:

```csharp
Position position = new Position { X = 2, Y = 3 };
```

Citește declarația în ordine: `Position` dă tipul variabilei, `position` este numele ei, iar `new Position` creează o valoare de acel tip. Inițializatorul atribuie 2 lui X și 3 lui Y; virgula sa separă atribuirile de câmpuri. Valoarea întreagă inițializează variabila. Parcurgerea schimbă etichetele entităților și valorile în timp ce ilustrează această formă; folosește perechea ulterioară (1, 1)/(2, 3) când urmărești testul cunoscut al distanței.

[00:25:02](https://www.youtube.com/watch?v=z51K0UhriOk&t=1502s) O variabilă `Position` este desenată ca un singur bloc de stocare mai mare, care conține două celule întregi mai mici. Gruparea nu elimină niciuna dintre coordonate și nu transformă perechea într-un singur întreg.

[00:25:27](https://www.youtube.com/watch?v=z51K0UhriOk&t=1527s) Aceste părți numite sunt **câmpuri**. Numele variabilei ajunge la întreaga valoare a poziției; fiecare nume de câmp selectează o parte a ei. [00:25:55](https://www.youtube.com/watch?v=z51K0UhriOk&t=1555s) **Operatorul punct** unește aceste nume. În [starea de acces la câmp](https://www.youtube.com/watch?v=z51K0UhriOk&t=1555s):

```csharp
Position player = new Position { X = 1, Y = 1 };
Console.WriteLine(player.X);
```

Aici `player.X` înseamnă câmpul X care aparține acestei anumite variabile `player`, nu o variabilă globală separată numită X.

## Citește și modifică câmpuri în poziții independente

[00:26:15](https://www.youtube.com/watch?v=z51K0UhriOk&t=1575s) Citirea unui câmp poate inițializa altă variabilă. **Copia** este o valoare nou stocată, nu o legătură continuă cu câmpul sursă. [Demonstrația copierii câmpului și scrierii în câmp](https://www.youtube.com/watch?v=z51K0UhriOk&t=1575s) este reprezentată cu un Y de pornire ales:

```csharp
Position player = new Position { X = 1, Y = 2 };
int savedY = player.Y;
player.Y = 7;
Console.WriteLine(savedY); // Încă 2 în acest exemplu simplificat.
```

[00:26:43](https://www.youtube.com/watch?v=z51K0UhriOk&t=1603s) Câmpurile unei structuri pot fi suprascrise după creare. `player.Y = 7` modifică câmpul din propriul bloc al jucătorului. Nu modifică copia anterioară din `savedY`.

[00:27:01](https://www.youtube.com/watch?v=z51K0UhriOk&t=1621s) Folosește același tip din nou pentru a crea o poziție separată pentru inamic. [Starea cu două poziții](https://www.youtube.com/watch?v=z51K0UhriOk&t=1621s) poate fi redusă la:

```csharp
Position player = new Position { X = 1, Y = 1 };
Position enemy = new Position { X = 2, Y = 3 };
```

[00:27:15](https://www.youtube.com/watch?v=z51K0UhriOk&t=1635s) Desenul didactic are acum două celule de coordonate pentru fiecare poziție: în total patru celule de câmp întregi. [00:27:32](https://www.youtube.com/watch?v=z51K0UhriOk&t=1652s) Blocul jucătorului și blocul inamicului ocupă locații separate, deci modificarea unuia nu îl modifică pe celălalt.

[00:27:45](https://www.youtube.com/watch?v=z51K0UhriOk&t=1665s) Gruparea lui X și Y face ca fiecare poziție să fie un bloc complet. [00:27:50](https://www.youtube.com/watch?v=z51K0UhriOk&t=1670s) În desen, sub-celulele sale pot fi numite prin accese la câmpuri compuse: `player.X`, `player.Y`, `enemy.X` și `enemy.Y`. Tipul comun le definește forma; nu le unește stocările.

## Schimbă interfața după gruparea datelor

[00:28:10](https://www.youtube.com/watch?v=z51K0UhriOk&t=1690s) Oprește-te la starea intermediară: numai câmpurile stocate au fost grupate. Funcția primește încă argumente întregi separate. [Starea intermediară cu doar câmpuri grupate](https://www.youtube.com/watch?v=z51K0UhriOk&t=1690s) folosește:

```csharp
int distance =
    Distance(enemy.X, enemy.Y, player.X, player.Y);
```

[00:28:14](https://www.youtube.com/watch?v=z51K0UhriOk&t=1694s) Grupează acum și parametrii. Înlocuiește cei patru parametri întregi cu doi parametri de tip `Position`. [00:28:18](https://www.youtube.com/watch?v=z51K0UhriOk&t=1698s) Fiecare este o singură valoare tipizată care conține X și Y, nu o coordonată scalară.

[Editarea semnăturii grupate](https://www.youtube.com/watch?v=z51K0UhriOk&t=1694s) duce la această implementare și apel simplificate:

```csharp
static int Distance(Position a, Position b)
{
    int x_dist = Math.Abs(a.X - b.X);
    int y_dist = Math.Abs(a.Y - b.Y);
    return x_dist + y_dist;
}

int distance = Distance(enemy, player);
```

[00:28:24](https://www.youtube.com/watch?v=z51K0UhriOk&t=1704s) Apelantul transmite fiecare poziție completă ca argument. Accesele la câmpuri se mută în funcție. Aceasta încheie schimbarea interfeței: apelul nu mai asamblează manual perechile X/Y. Încă pot exista greșeli la crearea unei poziții, dar această semnătură elimină capcana ordinii a patru întregi.

## Lasă tipul parametrului să îi determine stocarea

[00:29:29](https://www.youtube.com/watch?v=z51K0UhriOk&t=1769s) Un **parametru** este stocare locală unui apel, deci transmiterea unui argument necesită spațiu pentru valoarea acelui parametru. [00:29:34](https://www.youtube.com/watch?v=z51K0UhriOk&t=1774s) Un parametru `Position` are nevoie de ambele câmpuri întregi. Faptul că este un singur parametru nu înseamnă că ocupă o singură cutie de mărimea unui întreg.

[Urmărirea dispunerii parametrilor](https://www.youtube.com/watch?v=z51K0UhriOk&t=1769s) este rezumată schematic lângă apel:

```text
Distance(enemy, player)
a: [ a.X | a.Y ]     b: [ b.X | b.Y ]
int result: [ one integer value ]
```

1. [00:29:41](https://www.youtube.com/watch?v=z51K0UhriOk&t=1781s) Desenează un bloc care îl înconjoară pe `a`, conținând două sub-celule.
2. [00:29:46](https://www.youtube.com/watch?v=z51K0UhriOk&t=1786s) Numește-le `a.X` și `a.Y`; fă la fel pentru `b`.
3. [00:29:52](https://www.youtube.com/watch?v=z51K0UhriOk&t=1792s) Compară cu o variabilă sau un parametru `int`, desenat ca o singură celulă. Sintaxa listei de parametri nu determină dimensiunea; tipul declarat determină.
4. [00:29:59](https://www.youtube.com/watch?v=z51K0UhriOk&t=1799s) O poziție conține X și Y, deci desenează întâi câmpul X și apoi câmpul Y.
5. [00:30:06](https://www.youtube.com/watch?v=z51K0UhriOk&t=1806s) Poziția inamicului folosită în parcurgere conține X = 2 și Y = 3.
6. [00:30:13](https://www.youtube.com/watch?v=z51K0UhriOk&t=1813s) Coordonatele jucătorului furnizează valori de 1. O celulă scalară `int` cu adnotarea 1 rămâne o singură celulă. Nu interpreta acea adnotare ca o afirmație că parametrul complet `Position` `b` a devenit un întreg.
7. [00:30:27](https://www.youtube.com/watch?v=z51K0UhriOk&t=1827s) Variabilele locale întregi au o singură cutie scalară pentru că tipul lor este `int`. [00:30:33](https://www.youtube.com/watch?v=z51K0UhriOk&t=1833s) Parametrii de tip poziție au două cutii de câmpuri pentru că tipul lor este `Position`; schimbarea acelui tip în `int` ar schimba ce valoare ar putea fi stocată.
8. [00:30:45](https://www.youtube.com/watch?v=z51K0UhriOk&t=1845s) Celula rezultatului din apelant este desenată inițial fără răspunsul ei, înainte de a fi umplute celulele parametrilor de poziție.

Aceasta este o distincție între tipuri și valorile lor. Nici întregul 1 din interiorul unui câmp, nici numărul argumentelor nu schimbă tipul declarat al poziției care le conține.

## Copiază poziția completă în fiecare parametru

[00:30:49](https://www.youtube.com/watch?v=z51K0UhriOk&t=1849s) Alocă cele două poziții de câmp pentru fiecare parametru de poziție și inițializează-le din apelant. Acesta este **pass by value**: copiază valoarea argumentului în stocarea separată a funcției apelate.

[Urmărirea copierii poziției întregi](https://www.youtube.com/watch?v=z51K0UhriOk&t=1849s) are aceste valori pentru testul cunoscut:

```text
caller enemy:  (X=2, Y=3)  -> callee a: (X=2, Y=3)
caller player: (X=1, Y=1)  -> callee b: (X=1, Y=1)
```

[00:30:58](https://www.youtube.com/watch?v=z51K0UhriOk&t=1858s) După copiere, `a.X` este egal cu X-ul inamicului din apelant, iar `a.Y` este egal cu Y-ul său. Aceste egalități descriu valori potrivite, nu stocare de câmpuri partajată.

[00:31:05](https://www.youtube.com/watch?v=z51K0UhriOk&t=1865s) Copiază întreaga valoare a poziției ca unitate în parametru. [00:31:21](https://www.youtube.com/watch?v=z51K0UhriOk&t=1881s) Pentru acest tip cu două câmpuri, acea copiere a valorii întregi include ambele câmpuri individuale. [00:31:30](https://www.youtube.com/watch?v=z51K0UhriOk&t=1890s) Copia jucătorului duce la fel fiecare valoare de coordonată, 1, în câmpul corespunzător al lui `b`. Copierea întregii perechi nu omite Y doar pentru că diagrama sau vorbirea evidențiază o singură celulă scalară.

## Calculează prin câmpuri și păstrează suma returnată

[00:31:37](https://www.youtube.com/watch?v=z51K0UhriOk&t=1897s) Funcția poate calcula acum citind câmpuri din copiile sale de parametri. [Calculul distanței grupate](https://www.youtube.com/watch?v=z51K0UhriOk&t=1897s) urmează acest fragment:

```csharp
int x_dist = Math.Abs(a.X - b.X); // |2 - 1| = 1
int y_dist = Math.Abs(a.Y - b.Y); // |3 - 1| = 2
int dist = x_dist + y_dist;     // 1 + 2 = 3
return dist;
```

1. Creează temporarul pentru distanța pe X și evaluează scăderea câmpurilor.
2. [00:31:43](https://www.youtube.com/watch?v=z51K0UhriOk&t=1903s) Cu valorile X 2 și 1, contribuția este 1.
3. [00:31:51](https://www.youtube.com/watch?v=z51K0UhriOk&t=1911s) Cu valorile Y 3 și 1, contribuția este 2. Aritmetica intenționată este 3 − 1; o subtitrare degradată care sugerează 3 − 3 nu poate explica rezultatul urmărit, 2.
4. [00:32:03](https://www.youtube.com/watch?v=z51K0UhriOk&t=1923s) Adună contribuțiile în variabila sumă, obținând 3.
5. [00:32:10](https://www.youtube.com/watch?v=z51K0UhriOk&t=1930s) Întoarce 3 și stocheaz-o în rezultatul apelantului. Celulele temporare de parametru și locale nu mai sunt accesibile.
6. [00:32:14](https://www.youtube.com/watch?v=z51K0UhriOk&t=1934s) Variabila de rezultat a apelantului păstrează distanța returnată. [00:32:19](https://www.youtube.com/watch?v=z51K0UhriOk&t=1939s) Niciun câmp sau local al funcției apelate nu trebuie să rămână disponibil pentru ca răspunsul să supraviețuiască.
7. [00:32:20](https://www.youtube.com/watch?v=z51K0UhriOk&t=1940s) Reia execuția după apelul funcției și folosește rezultatul păstrat.

Gruparea intrărilor schimbă modul în care apelul comunică pozițiile, păstrând aceeași aritmetică și același răspuns cunoscut.

## Prezice ce poate afecta modificarea unui parametru de structură

[00:32:29](https://www.youtube.com/watch?v=z51K0UhriOk&t=1949s) Un **efect secundar** este o modificare observabilă în afara datelor private de lucru ale funcției. Prezice dacă atribuirea lui `a.X = 5` în funcție modifică variabila inamicului din apelant.

[Starea de modificare a unui parametru de structură](https://www.youtube.com/watch?v=z51K0UhriOk&t=1949s) poate fi redusă la:

```csharp
Position enemy = new Position { X = 2, Y = 3 };
Change(enemy);
Console.WriteLine(enemy.X); // 2

static void Change(Position a)
{
    a.X = 5;
}
```

1. [00:32:33](https://www.youtube.com/watch?v=z51K0UhriOk&t=1953s) Creează stocare nouă de parametru pentru `a` și copiază în ea X și Y din apelant.
2. [00:32:39](https://www.youtube.com/watch?v=z51K0UhriOk&t=1959s) Pentru o funcție cu două poziții, creează și inițializează separat `b` în același mod.
3. [00:32:42](https://www.youtube.com/watch?v=z51K0UhriOk&t=1962s) O scriere în `a.X` vizează câmpul X din acea valoare locală de parametru.
4. [00:32:52](https://www.youtube.com/watch?v=z51K0UhriOk&t=1972s) Suprascrierea conținutului său nu îi unește stocarea cu cea a apelantului; parametrul rămâne o copie.
5. [00:33:03](https://www.youtube.com/watch?v=z51K0UhriOk&t=1983s) Nici modificarea lui `a`, nici modificarea lui `b` nu schimbă pozițiile originale ale apelantului.
6. [00:33:16](https://www.youtube.com/watch?v=z51K0UhriOk&t=1996s) Această scriere în câmp nu are niciun efect exterior asupra acelor poziții la transmiterea obișnuită. [00:33:21](https://www.youtube.com/watch?v=z51K0UhriOk&t=2001s) **Transmiterea prin valoare** înseamnă că fiecare apel copiază valoarea argumentului în parametrul său.

Tipul de returnare `void` din fragment înseamnă că funcția demonstrativă nu returnează nicio valoare. Funcția `Distance` anterioară poate în continuare să returneze un număr calculat din pozițiile sale copiate; returnarea unui rezultat și modificarea apelantului sunt mecanisme separate.

<details>
<summary>Dacă modificarea lui a.X în interiorul lui Distance schimbă răspunsul returnat, trebuie să se schimbe și enemy.X din apelant?</summary>

Nu. Funcția poate calcula un rezultat diferit din copia sa privată și poate returna acel număr lăsând poziția originală a apelantului neatinsă. Apelantul primește rezultatul doar prin returnarea și atribuirea explicite.

</details>

## Folosește ref pentru a crea un alias al variabilei apelantului

[00:33:27](https://www.youtube.com/watch?v=z51K0UhriOk&t=2007s) Următoarea caracteristică este o scurtă prezentare. Este prezentată ca o idee de care munca curentă din laborator probabil nu va avea nevoie.

[00:33:33](https://www.youtube.com/watch?v=z51K0UhriOk&t=2013s) **`ref`** schimbă modul în care este furnizată o variabilă: în loc să facă o copie obișnuită a valorii, parametrul devine un **alias**, o altă cale de acces la stocarea apelantului. [00:33:37](https://www.youtube.com/watch?v=z51K0UhriOk&t=2017s) Scrie `ref` atât în declarația parametrului, cât și în apel.

[Demonstrația ref explicit](https://www.youtube.com/watch?v=z51K0UhriOk&t=2013s) este simplificată la:

```csharp
Position enemy = new Position { X = 2, Y = 3 };
Change(ref enemy);
Console.WriteLine(enemy.X); // 5

static void Change(ref Position a)
{
    a.X = 5;
}
```

[00:33:57](https://www.youtube.com/watch?v=z51K0UhriOk&t=2037s) Scrierea afectează acum variabila originală. Acest efect exterior este motivul pentru care alegem acest mod de parametru.

[00:34:08](https://www.youtube.com/watch?v=z51K0UhriOk&t=2048s) Desenul reprezintă aliasul prin **adresa** celulei apelantului, folosind un număr precum 64 ca exemplu. O adresă identifică o locație de stocare; aici ne ajută să explicăm la ce variabilă ajunge o scriere. Este o imagine didactică, asemănătoare unui pointer, pentru un alias gestionat, nu o cerință de a scrie cod unsafe cu pointeri.

[00:34:15](https://www.youtube.com/watch?v=z51K0UhriOk&t=2055s) Valoarea X/Y nu este copiată într-un parametru de poziție independent ca înainte. Ambele nume oferă acces la o singură locație de stocare. [00:34:29](https://www.youtube.com/watch?v=z51K0UhriOk&t=2069s) La apel, `ref enemy` cere acces la stocarea acelei variabile, nu doar la valoarea sa curentă de coordonată.

[00:34:34](https://www.youtube.com/watch?v=z51K0UhriOk&t=2074s) Aceasta rămâne o prezentare avansată, nu tehnica așteptată pentru exercițiile curente. Lecția continuă către clase, care introduc un alt motiv pentru care scrierile în câmpuri pot fi partajate.

## Schimbă reprezentarea alegând o clasă

[00:34:40](https://www.youtube.com/watch?v=z51K0UhriOk&t=2080s) Înlocuiește declarația struct a poziției cu o declarație de clasă. Multe dintre sintaxele scrise pentru inițializare, acces la câmpuri și apel pot rămâne aceleași, dar sensul ei de stocare se schimbă.

[00:35:06](https://www.youtube.com/watch?v=z51K0UhriOk&t=2106s) O **clasă** definește obiecte de **tip referință**. Instanțele sale folosesc stocare dinamică, adică **heap**. O **referință** este valoarea prin care altă variabilă poate ajunge la un obiect. [00:35:15](https://www.youtube.com/watch?v=z51K0UhriOk&t=2115s) O variabilă de acest tip de clasă conține acea referință, nu direct cele două câmpuri de coordonate.

[Starea reprezentării prin clasă](https://www.youtube.com/watch?v=z51K0UhriOk&t=2106s) este ilustrată mai jos. Aceasta înlocuiește definiția struct anterioară; cele două declarații nu sunt gândite să coexiste:

```csharp
Position enemy = new Position();
Position player = new Position();
Position tree = new Position();

class Position
{
    public int X = 1;
    public int Y = 1;
}
```

[00:35:21](https://www.youtube.com/watch?v=z51K0UhriOk&t=2121s) Celula de memorie temporară pentru `enemy` păstrează o legătură către un obiect. [00:35:30](https://www.youtube.com/watch?v=z51K0UhriOk&t=2130s) Prin urmare, ea nu conține direct X și Y așa cum făcea celula struct de mai devreme.

1. [00:35:41](https://www.youtube.com/watch?v=z51K0UhriOk&t=2141s) Creează obiectul inamic cu propriile sale câmpuri X și Y. În această demonstrație de clasă, inițializatorii de câmp dau ambilor câmpuri valoarea 1.
2. [00:35:59](https://www.youtube.com/watch?v=z51K0UhriOk&t=2159s) Stochează în variabila inamic o referință către locația acelui obiect.
3. [00:36:04](https://www.youtube.com/watch?v=z51K0UhriOk&t=2164s) Creează un obiect separat pentru jucător la altă adresă ilustrativă, pornind și el de la X = 1 și Y = 1 proprii.
4. [00:36:21](https://www.youtube.com/watch?v=z51K0UhriOk&t=2181s) Stochează referința jucătorului în propria sa variabilă temporară.
5. [00:36:28](https://www.youtube.com/watch?v=z51K0UhriOk&t=2188s) Creează obiectul copac și variabila sa de referință corespunzătoare după același model.

Referințele ocupă celulele de lucru ale apelantului; cele trei obiecte create separat păstrează câmpurile efective în stocare dinamică. Alegerea clasei schimbă această reprezentare chiar și acolo unde expresiile sursă arată încă familiar.

## Copiază o referință și scrie în obiectul partajat

[00:36:45](https://www.youtube.com/watch?v=z51K0UhriOk&t=2205s) Intrarea din nou în funcție creează variabilele sale locale de lucru și stocarea de parametri în modelul temporar al stivei. [00:36:52](https://www.youtube.com/watch?v=z51K0UhriOk&t=2212s) Deoarece `Position` este acum o clasă, o celulă de parametru obișnuită păstrează o referință, nu o copie a câmpurilor X/Y ale obiectului.

[Urmărirea modificării prin parametru de clasă](https://www.youtube.com/watch?v=z51K0UhriOk&t=2212s) este reprezentată printr-un apel obișnuit, fără modificatorul `ref`:

```csharp
Position enemy = new Position();
Change(enemy);
Console.WriteLine(enemy.X); // 7

static void Change(Position a)
{
    a.X = 7;
}
```

1. Copiază valoarea referinței din variabila apelantului în parametrul `a`.
2. [00:37:11](https://www.youtube.com/watch?v=z51K0UhriOk&t=2231s) Urmează acea referință până la obiectul din stocarea dinamică.
3. [00:37:32](https://www.youtube.com/watch?v=z51K0UhriOk&t=2252s) Selectează câmpul X al obiectului cu `a.X`. Accesul la câmp ajunge la câmpul obiectului, nu la un câmp întreg privat inclus în celula parametrului `a`. Această folosire a unei referințe nu este un nou parametru implicit `ref`.
4. [00:37:41](https://www.youtube.com/watch?v=z51K0UhriOk&t=2261s) Scrie 7 în acel câmp. Adresa ilustrativă 64 din diagramă identifică obiectul partajat; nu este o adresă de mașină garantată.
5. [00:37:58](https://www.youtube.com/watch?v=z51K0UhriOk&t=2278s) Numai referința a fost copiată, deci variabila inamic și `a` ajung amândouă la obiectul al cărui X s-a schimbat.
6. [00:38:11](https://www.youtube.com/watch?v=z51K0UhriOk&t=2291s) Citirea lui X prin referința apelantului observă imediat acea scriere. Încheierea stocării de parametri a funcției nu anulează o modificare deja făcută obiectului.

Acesta este un **efect secundar**, o modificare în afara datelor locale private ale funcției apelate. Apare pentru că referința copiată ajunge la stocare de obiect partajată. Nu presupune aliasarea însăși a variabilei de referință a apelantului cu `ref`.

## Compară aceeași scriere în câmp între cele două tipuri

[00:38:22](https://www.youtube.com/watch?v=z51K0UhriOk&t=2302s) Compară clasa cu tipul valoare anterior, lăsând expresia de scriere în câmp neschimbată. [Comparația reprezentărilor](https://www.youtube.com/watch?v=z51K0UhriOk&t=2302s) se concentrează pe:

```csharp
static void Change(Position a)
{
    a.X = 7;
}
```

- Cu `Position` declarat ca struct și un parametru obișnuit, `a` conține câmpuri copiate. Scrierea afectează valoarea de poziție a funcției apelate.
- Cu `Position` declarat ca clasă și un parametru obișnuit, `a` conține o referință copiată. Scrierea afectează obiectul la care ajunge și apelantul.

[00:38:51](https://www.youtube.com/watch?v=z51K0UhriOk&t=2331s) Aceeași sintaxă cu punct nu spune întreaga poveste a stocării. Identifică întâi tipul, apoi ce conține variabila, apoi ce copiază apelul și, în final, unde aterizează scrierea. Acest raționament face mai ușor de urmărit abstrațiile ulterioare ale limbajului.

<details>
<summary>De ce transmiterea obișnuită a unei clase rămâne transmitere prin valoare, chiar dacă apelantul vede X-ul modificat?</summary>

Valoarea copiată este o referință. Variabila de referință a apelantului și parametru sunt separate, dar ambele se referă la același obiect. Scrierea într-un câmp modifică acel obiect; nu dovedește că variabila apelantului a fost transmisă cu ref.

</details>

## Precizează exact ce copiază transmiterea prin valoare

[00:39:06](https://www.youtube.com/watch?v=z51K0UhriOk&t=2346s) Stabilește regula finală cu precizie: argumentele obișnuite din C# sunt transmise **prin valoare**. Ce se copiază depinde de ce conține variabila argumentului.

[Distincția finală de copiere](https://www.youtube.com/watch?v=z51K0UhriOk&t=2346s) se rezumă astfel:

```text
struct Position argument -> copy of its X/Y value
class Position argument  -> copy of its object reference
explicit ref argument    -> alias to the caller's variable
```

[00:39:16](https://www.youtube.com/watch?v=z51K0UhriOk&t=2356s) O poziție de tip valoare păstrează direct valoarea coordonatelor; o poziție de tip referință păstrează legătura către un obiect. Apelurile obișnuite copiază aceste valori respective. Expresia „întotdeauna prin valoare” din explicația finală se referă la aceste apeluri obișnuite; modul `ref` demonstrat explicit este un alt mod. Transferul anterior prin valoarea returnată nu a introdus `out`, un alt mod distinct de parametru.

Aceasta permite ca o singură regulă să explice ambele exemple fără a confunda o referință de obiect copiată cu un alias al variabilei apelantului.

## Domeniul de vizibilitate și practică suplimentară

[00:28:10](https://www.youtube.com/watch?v=z51K0UhriOk&t=1690s) Lecția arată intenționat o versiune intermediară care grupează câmpurile poziției, dar transmite încă coordonate întregi separate. Apoi grupează parametrii. Acestea sunt două îmbunătățiri distincte.

[00:33:27](https://www.youtube.com/watch?v=z51K0UhriOk&t=2007s) Demonstrația cu `ref` este o prezentare și este probabil inutilă pentru laboratoarele curente. [00:34:34](https://www.youtube.com/watch?v=z51K0UhriOk&t=2074s) Cursul merge spre clase în schimb. [00:34:40](https://www.youtube.com/watch?v=z51K0UhriOk&t=2080s) Aici clasele servesc la a contrasta stocarea de valoare cu stocarea de obiect referit, nu la a dezvolta un design complet orientat pe obiecte.

Calculul numără pașii orizontali și verticali în grila ilustrată. Înregistrarea nu implementează un joc complet, căutare de drumuri cu obstacole, deplasare pe diagonală, tablouri sau o buclă peste entități. Cutiile sale de memorie explică domeniul de vizibilitate, copierea argumentelor, returnarea și modificarea unui obiect partajat. Dispunerea exactă, optimizările compilatorului și o descriere completă a gestionării memoriei sunt în afara acelui model. Fragmentele despre valoarea returnată nu predau parametrii de ieșire sau parsarea intrării din consolă.

Pentru practică opțională dincolo de această înregistrare, folosește [laboratorul despre funcții](../../../labs/1_basic/05_functions.md) ca să raționezi asupra variabilelor locale, apelurilor, tipurilor de returnare și expresiilor imbricate. Exercițiul său de recursivitate merge mai departe decât această lecție. [Laboratorul despre îmbunătățirea programului](../../../labs/1_basic/13_improvement_techniques.md) aplică datele grupate și funcțiile extrase unei biblioteci de cărți, împreună cu tablouri și bucle care nu sunt predate aici.

Un [exemplu conex din repository la commitul d5af1fb](https://github.com/AntonC9018/uniCourse_csharp/blob/d5af1fb372be56a453e3b139990013c28e50198c/examples/property_dict/Subclassing.cs) acceptă, de asemenea, două poziții grupate. Fragmentul său relevant, scurtat din acel fișier versionat, este:

```csharp
static float Distance(Position a, Position b)
{
    var diff = new Position(a.X - b.X, a.Y - b.Y);
    var dist = (float)Math.Sqrt(
        diff.X * diff.X + diff.Y * diff.Y);
    return dist;
}

record struct Position(float X, float Y);
```

Acesta este cod conex, nu o versiune exactă sau finală a programului din video. Folosește un record struct cu coordonate în virgulă mobilă și distanță euclidiană, metrica liniei drepte. Sintaxa de record, aritmetica în virgulă mobilă și formula cu radical nu sunt predate în această înregistrare. Păstrează suma întregilor bazată pe diferențele absolute din video când reconstruiești exemplul său pe grilă.

## Istoricul modificărilor de cod

Aceste legături stabile către stările din video identifică editările din clasă în ordinea construcției. Niciun commit inspectat nu furnizează fișierele lor sursă exacte. Exemplele din secțiunile corespunzătoare sunt reconstrucții simplificate.

- [00:00:00](https://www.youtube.com/watch?v=z51K0UhriOk&t=0s) — introdu variabile X/Y separate pentru jucător și inamic.
- [00:01:58](https://www.youtube.com/watch?v=z51K0UhriOk&t=118s) — elimină semnele scăderii cu valoarea absolută.
- [00:02:45](https://www.youtube.com/watch?v=z51K0UhriOk&t=165s) — salvează contribuția pe X; testul ulterior (1, 1)/(2, 3) dă totalul 3.
- [00:06:53](https://www.youtube.com/watch?v=z51K0UhriOk&t=413s) — lipește un al doilea calcul și redenumește variabilele sale temporare.
- [00:07:18](https://www.youtube.com/watch?v=z51K0UhriOk&t=438s) — pune copiile în blocuri surori pentru a reutiliza numele locale.
- [00:10:16](https://www.youtube.com/watch?v=z51K0UhriOk&t=616s) — declară rezultatul păstrat în exteriorul blocului său de calcul.
- [00:11:17](https://www.youtube.com/watch?v=z51K0UhriOk&t=677s) — dă rezultatelor păstrate nume specifice entităților.
- [00:12:20](https://www.youtube.com/watch?v=z51K0UhriOk&t=740s) — corectează măsurătoarea copacului ca să folosească coordonatele inamicului.
- [00:15:20](https://www.youtube.com/watch?v=z51K0UhriOk&t=920s) — extrage o funcție care întoarce întreg, cu patru parametri de coordonate.
- [00:16:33](https://www.youtube.com/watch?v=z51K0UhriOk&t=993s) — reutilizează funcția pentru distanța inamic–copac; rularea ulterioară afișează 3 și 5.
- [00:24:40](https://www.youtube.com/watch?v=z51K0UhriOk&t=1480s) — inițializează valori de poziție grupate, apoi citește și scrie în câmpurile lor.
- [00:28:14](https://www.youtube.com/watch?v=z51K0UhriOk&t=1694s) — înlocuiește parametrii de coordonate separați cu două poziții complete.
- [00:32:29](https://www.youtube.com/watch?v=z51K0UhriOk&t=1949s) — demonstrează că modificarea unui parametru de structură afectează copia sa privată.
- [00:33:33](https://www.youtube.com/watch?v=z51K0UhriOk&t=2013s) — arată transmiterea explicită cu ref pentru a modifica stocarea apelantului.
- [00:35:06](https://www.youtube.com/watch?v=z51K0UhriOk&t=2106s) — înlocuiește structura cu o clasă și urmărește referințele către obiecte dinamice.
- [00:37:32](https://www.youtube.com/watch?v=z51K0UhriOk&t=2252s) — scrie printr-o referință de clasă copiată obișnuit, în câmpul X al obiectului partajat.