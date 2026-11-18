> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# De la obiectele șir la mesajele formatate

[00:00:00](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=0s) Această lecție face legătura între textul pe care îl scrii în C# și obiectele, codurile de caractere și octeții de fișier care îl reprezintă. Pornește de la un șir imutabil, inspectează un caracter ca număr, reconstruiește acel caracter, examinează terminatoarele de linie și codificările, iar la final construiești mesaje cu interpolare și concatenare.

Termenii folosiți pe parcurs sunt:

- **Șir, obiect, referință și memorie dinamică:** un `string` este un obiect care conține text; o referință identifică acel obiect. Memoria dinamică este stocarea gestionată în care trăiesc asemenea obiecte.
- **Imutabilitate, tablou de caractere mutabil:** conținutul unui obiect imutabil nu se poate schimba după construire. Un `char[]` mutabil este un tablou ale cărui elemente de tip caracter pot fi înlocuite.
- **Caracter, glif și `char`:** un caracter este un element de text; un glif este forma sa desenată. `char` din C# este un tip de valoare care păstrează o singură unitate de cod UTF-16 pe 16 biți, nu o referință la un obiect.
- **Literal, compilator și indexator:** un literal scrie o valoare direct în codul sursă; compilatorul traduce sursa în cod executabil. Un indexator folosește paranteze pătrate pentru a citi un element după poziție.
- **`int`, `float`, tip static și supraîncărcare:** `int` păstrează un întreg pe 32 de biți, iar `float` un număr în virgulă mobilă. Tipul static al unei expresii este tipul ei cunoscut în timpul compilării. Supraîncărcările sunt versiuni diferite ale unei metode sau a unui operator, selectate în funcție de tipurile implicate.
- **Conversie numerică, cast și trunchiere:** o conversie schimbă tipul unei valori; o conversie implicită nu necesită cast, iar un cast explicit scrie tipul țintă între paranteze. Trunchierea înseamnă aici pierderea biților care nu încap în timpul unei restrângeri necontrolate. „Necontrolat” înseamnă că o conversie în afara intervalului are voie să elimine biți; conversia controlată o respinge în schimb.
- **Codificare, cod, ASCII, Unicode și punct de cod:** o codificare specifică modul în care este reprezentat textul. Un cod de caracter este numărul asociat lui. ASCII este mulțimea de coduri pe 7 biți, 0–127. Unicode atribuie puncte de cod — identificatori numerici — caracterelor din mai multe limbi.
- **Bit, octet, zecimal, hexazecimal și dump de octeți:** un bit este o cifră binară; un octet conține opt biți. Zecimal este baza zece, hexazecimal baza șaisprezece. Un dump de octeți afișează conținutul fișierului ca numere, de obicei în hexazecimal.
- **Secvență de evitare, caracter de control, LF, CR și CRLF:** o secvență de evitare scrie o valoare specială în sursă, cum ar fi `\n`. Caracterele de control influențează tratarea textului, nu desenarea unui glif. Line Feed (LF) este codul 10; Carriage Return (CR) este codul 13; CRLF este secvența de două caractere CR urmat de LF.
- **BOM și ordinea octeților:** un Byte Order Mark este un marcaj de codificare opțional la începutul unui fișier. Ordinea octeților descrie aranjarea octeților în unitățile multibyte.
- **UTF-8, UTF-16, UTF-32, unitate de cod, BMP și pereche de surogate:** aceste codificări Unicode folosesc unități de 8, 16 și 32 de biți. O unitate de cod este o asemenea unitate de stocare. Planul Multilingv de Bază (BMP) conține puncte de cod până la U+FFFF; punctele de cod suplimentare necesită o pereche de unități UTF-16, numită pereche de surogate.
- **Cod Alt și împărțirea automată pe rânduri (word wrap):** un cod Alt este o scurtătură de tastatură, dependentă de platformă, pentru introducerea unui caracter. Word wrap pliază vizual o linie lungă din editor fără să insereze un caracter newline.
- **Interpolare și expresie:** interpolarea inserează valori evaluate într-un șir folosind `$` și acolade. O expresie este cod sursă care calculează o valoare.
- **Concatenare, `string.Concat`, metodă statică și instantaneu:** concatenarea alipește bucăți de text. `string.Concat` este o metodă statică, apelată pe tip și nu pe o instanță, care alipește argumentele sale. Un instantaneu păstrează textul obținut la momentul construirii.
- **.NET, `Console.WriteLine` și `ToString`:** .NET furnizează runtime-ul și bibliotecile folosite aici. `Console.WriteLine` scrie o valoare urmată de un terminator de linie. `ToString` obține reprezentarea textuală a unei valori.

Pentru exerciții înrudite anterioare, [laboratorul de intrare în consolă](../../../labs/1_basic/08_input.md) acoperă citirea șirurilor și conversia textului numeric în numere. Este o lecție separată, nu un exercițiu demonstrat în această înregistrare.

**Domeniul de aplicare:** [00:03:43](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=223s) regulile detaliate pentru trunchiere sunt amânate. [00:09:14](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=554s) algoritmii interni ai codificărilor Unicode sunt, de asemenea, omiși intenționat. Aici scopul este să înțelegi reprezentarea, să recunoști octeții fișierului și să folosești sintaxa de bază pentru construirea șirurilor. Fragmentele de mai jos sunt reconstrucții simplificate ale stărilor din videoclipul legat; nu a fost disponibilă în checkout-ul deținut nicio sursă de lecție corespunzătoare.

## Șiruri și obiecte imutabile

[00:00:00](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=0s) Un șir este un obiect care reprezintă o succesiune de caractere. Conținutul său este text: chiar și un număr încorporat într-un mesaj ajunge în final să devină caractere. Gândirea la un șir ca la un tablou de caractere este utilă pentru citirea pozițiilor individuale, dar un `string` nu este un `char[]`.

[00:00:23](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=23s) Diferența importantă este mutabilitatea. Un tablou de caractere mutabil îți permite să înlocuiești un element; un șir imutabil nu îți permite niciodată să înlocuiești un caracter în obiectul existent. [00:00:32](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=32s) Obiectul șir trăiește în memoria dinamică. [00:00:39](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=39s) Reatribuirea variabilei înlocuiește referința ei, nu conținutul acelui obiect.

O versiune simplificată a stării inițiale a obiectului, de la [00:00:32](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=32s):

```csharp
string a = "Hello world";
string original = a;
a = "Another message";
// original se referă în continuare la "Hello world".

char[] letters = { 'H', 'i' };
letters[0] = 'B';          // Permis: tabloul este mutabil.
// original[0] = 'B';      // Eroare: indexerul unui string este numai pentru citire.
```

Aici obiectul este șirul stocat, iar referința este legătura variabilei cu el. După reatribuire, `a` identifică alt șir; `original` îl identifică în continuare pe primul. Această distincție va explica de ce textul concatenat nu urmărește modificările ulterioare ale variabilelor.

<details>
<summary>Dacă două variabile se referă la același șir, schimbarea uneia îl modifică și pe celălalt?</summary>

Nu. Reatribuirea schimbă doar referința acelei variabile. Obiectul imutabil comun rămâne neschimbat.

</details>

## Caractere, litere și afișare numerică

[00:00:55](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=55s) Memoria păstrează numărul unui caracter, nu gliful său — forma desenată pe ecran. [00:01:17](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=77s) Tipul de valoare `char` păstrează direct o singură unitate de cod UTF-16. Pentru literele simple folosite aici, o unitate reprezintă un caracter.

Construiește primul experiment în această ordine:

1. [00:01:27](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=87s) Citește primul caracter folosind un indexer: parantezele pătrate selectează poziția zero.
2. [00:01:35](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=95s) Scrie aceeași valoare ca literă de caracter. Ghilimelele simple indică `char`; ghilimelele duble indică `string`, chiar dacă șirul conține un singur caracter.
3. [00:01:42](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=102s) Compilatorul transformă literalul de caracter în codul său numeric. [00:01:49](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=109s) Atribuirea acelui literal unui `int` este acceptată printr-o conversie implicită.
4. [00:02:07](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=127s) Afișează `char`-ul pentru a vedea simbolul; [00:02:20](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=140s) afișează un `int` care conține aceeași valoare numerică pentru a vedea numărul.

Stare simplificată a caracterului și a afișării, [00:02:07](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=127s):

```csharp
string a = "Hello world";
char ch = a[0];
char literal = 'H';
int code = 'H';                // Conversie implicită char la int.
Console.WriteLine(ch);         // H
Console.WriteLine(code);       // 72
Console.WriteLine((int)ch);    // 72
```

[00:02:07](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=127s) Cast-ul `(int)ch` este o conversie numerică explicită. Păstrează numărul 72, dar îl reprezintă în tipul de întreg mai larg; nu păstrează aceeași dimensiune de stocare. Cast-ul este opțional la atribuirea din `char` în `int`, dar util în interiorul unei expresii de afișare.

[00:02:20](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=140s) Tipul static — tipul cunoscut compilatorului — selectează supraîncărcarea potrivită de `WriteLine`. Un argument `char` este afișat ca caracter; un argument `int` este afișat numeric. Ambele pot reprezenta 72, însă rezultatul lor diferă. Este o conversie a unui cod de caracter, nu o încercare de a interpreta textul `"72"` ca număr.

## Conversia unui număr înapoi în caracter

[00:02:53](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=173s) Inversăm acum experimentul: pornim de la întregul 72 și obținem caracterul corespunzător.

1. [00:02:53](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=173s) Stochează 72 într-un `int`.
2. [00:03:06](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=186s) Încearcă să atribui acea variabilă unui `char`. Compilatorul respinge conversia implicită, deoarece `int` are un interval mai mare.
3. [00:03:24](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=204s) Adaugă cast-ul explicit `(char)` pentru a obține tipul mai mic.
4. [00:03:45](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=225s) Afișează rezultatul. Demonstrația înregistrată arată `H`.

Stare simplificată a conversiei inverse, [00:03:24](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=204s):

```csharp
int code = 72;
// char ch = code;             // Eroare: este necesară o conversie explicită.
char ch = (char)code;
Console.WriteLine(ch);        // H
```

Un cast scrie tipul țintă între paranteze înaintea expresiei. `int` ocupă 32 de biți; `char` ocupă 16. Numărul 72 încape în ambele. [00:03:24](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=204s) Restrângerea numerică diferă de cast-urile tipurilor de referință discutate anterior: într-o conversie necontrolată, biții în plus pot fi eliminați. Această pierdere este **trunchierea** menționată aici. Conversiile controlate pot respinge valori din afara intervalului, așadar afirmația „cast-urile taie întotdeauna informația” este prea generală.

[00:03:43](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=223s) Lecția lasă regulile detaliate ale trunchierii pentru mai târziu; acest exemplu folosește o valoare din interval și stabilește direcția conversiei. [Referința C# pentru caracter](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/char) confirmă reprezentarea pe doi octeți și direcțiile conversiei.

## Citirea tabelului ASCII

[00:03:51](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=231s) De ce devine 72 `H`? O codificare asociază textul cu reprezentări numerice. Pentru caracterele folosite până acum, tabelul ASCII oferă codurile familiare; cunoscând codul, poți reconstrui caracterul.

[00:04:13](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=253s) **Corecție a formulării largi din înregistrare:** șirurile și caracterele `char` din C# folosesc în memorie Unicode UTF-16, nu ASCII ca codificare generală. ASCII este mulțimea mai mică de coduri comună acestor exemple. ASCII are 128 de intrări, numerotate 0–127, nu 127. [Documentația Microsoft despre ASCII](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.ascii) descrie acest interval.

Citește tabelul în ordinea demonstrată:

| Poziție în videoclip | Caracteristică a tabelului | Semnificație |
| --- | --- | --- |
| [00:04:39](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=279s) | Spațiu, zecimal 32 | Nu are un glif vizibil, așa că un tabel îl poate eticheta între paranteze. Parantezele sunt notație explicativă. |
| [00:04:41](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=281s) | Punctuație, 33–47 | Acest bloc urmează ordinea tabelului; există și altă punctuație în alte zone. |
| [00:04:50](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=290s) | Litere mari, 65–90 | `A` începe la 65; literele succesive cresc cu unu. |
| [00:04:59](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=299s) | Litere mici, 97–122 | Acest bloc ulterior păstrează și el ordinea alfabetică. |
| [00:05:07](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=307s) | `H`, zecimal 72 | Este cu șapte poziții după `A`. |

[00:05:12](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=312s) Scrierea lui `'H'` ca întreg dă 72: compilatorul rezolvă codul de caracter al literalului, nu o formă grafică. [00:05:29](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=329s) Unui `char` căruia i se atribuie `'H'` îi conține aceeași valoare de cod. **Încă se afișează ca `H` când este trecut ca `char`; pentru a expune 72 este necesară o expresie de tip întreg.**

Comparație simplificată între tabel și valoare, [00:05:29](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=329s):

```csharp
int number = 'H';             // 72
char symbol = 'H';            // Unitate de cod cu valoarea 72.
Console.WriteLine(symbol);   // H
Console.WriteLine((int)symbol); // 72
char restored = (char)number;
```

[00:05:38](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=338s) Eliminarea cast-ului din ultima atribuire face atribuirea să nu mai compileze: o variabilă întreagă generală nu poate încăpea implicit într-un `char`. Nici consultarea codului, nici afișarea lui nu schimbă această regulă de tipuri.

<details>
<summary>Care este codul literei I și de ce afișarea unui char care îl conține arată I și nu 73?</summary>

Blocul literelor mari este consecutiv, deci I urmează după H la 73. Supraîncărcarea WriteLine pentru char interpretează valoarea drept caracter. Fă cast la int pentru a afișa 73.

</details>

## Introducerea unui line feed într-un literal

[00:05:49](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=349s) Tabelul de coduri include și caractere de control: valori care influențează tratarea textului fără să deseneze simboluri obișnuite. [00:05:57](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=357s) Line Feed (LF), codul 10, reprezintă sfârșitul de linie folosit în exemplul următor. Problema este să afișăm `Hello` și `World` pe linii separate cu un singur apel.

1. [00:06:10](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=370s) Pune secvența de evitare `\n` între cuvinte, în interiorul literalului de șir.
2. Compilatorul interpretează backslash-ul și `n` ca o singură valoare LF, nu ca două caractere literal.
3. [00:06:28](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=388s) Rulează programul: înregistrarea arată `Hello`, apoi un sfârșit de linie, apoi `World`.

Stare simplificată a codului cu newline, [00:06:10](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=370s):

```csharp
Console.WriteLine("Hello\nWorld");
```

Ieșirea înregistrată la [00:06:28](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=388s):

```text
Hello
World
```

[00:06:31](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=391s) Deoarece LF este o singură valoare, încape și într-un literal de caracter. Ghilimelele simple cer exact o valoare `char`; mai multe caractere obișnuite între ele produc o eroare de compilare. Cele două caractere sursă ale secvenței de evitare nu o transformă într-o valoare cu două caractere.

Verificare simplificată caracter-versus-număr, [00:06:43](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=403s):

```csharp
char lf = '\n';
// char invalid = 'Hello';    // Eroare: prea multe caractere.
Console.WriteLine(lf);       // Scrie LF, apoi propriul sfârșit de linie al lui WriteLine.
int lfCode = lf;
Console.WriteLine(lfCode);   // 10
```

[00:06:43](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=403s) Afișarea LF ca `char` produce un sfârșit de linie, iar stocarea lui ca întreg afișează 10. Reține că `WriteLine` adaugă și propriul său sfârșit de linie; afișarea LF prin el poate crea așadar o linie goală suplimentară.

## Terminatoare de linie specifice platformei

[00:06:55](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=415s) Un sfârșit de lină nu este întotdeauna un singur caracter. Linux folosește convențional LF, iar Windows folosește convențional CRLF: Carriage Return, codul 13, urmat de Line Feed, codul 10. Fișierele pot folosi oricare dintre convenții, independent de sistemul de operare pe care sunt deschise.

Comparație simplificată între terminatoarele de linie, [00:06:55](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=415s):

```csharp
string linuxEnding = "\n";     // Un singur caracter: 10.
string windowsEnding = "\r\n"; // Două caractere: 13, apoi 10.
```

Aici `\r` este o secvență de evitare pentru CR. Adăugarea ei înaintea lui `\n` schimbă conținutul real al șirului. Secvența de evitare unică `\n` rămâne LF; nu se transformă automat în două caractere doar pentru că programul rulează pe Windows.

## Citirea octeților unui fișier text și BOM

[00:07:16](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=436s) Un fișier text stochează octeți codificați, nu litere desenate. Un dump de octeți este o vedere de editor care afișează direct acești octeți numerici. Pentru a lega acea vedere de experimentul anterior cu caracterul:

1. [00:07:16](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=436s) Deschide fișierul text într-o vedere care afișează valori de octet.
2. [00:07:33](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=453s) Stabilește baza numerică înainte de a interpreta o valoare afișată.
3. [00:07:55](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=475s) Localizează newline-ul la începutul textului demonstrat.
4. [00:08:01](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=481s) Ia în calcul BOM-ul UTF-8 opțional înainte de a identifica unde începe textul.
5. [00:08:22](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=502s) Decodifică `0D 0A` drept CR urmat de LF.

[00:07:33](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=453s) **Corecție a bazei numerice:** valoarea afișată `45` este hexazecimală, nu zecimalul 45. Hexazecimalul `0x45` este zecimalul 69 și corespunde literei mari `E`. Litera mică `e` este hexazecimalul `65`, adică zecimalul 101. Eticheta de literă din transcriere nu trebuie să te facă să asimilezi `e` minuscule cu 45.

O succesiune de octeți simplificată pentru starea de inspecție a fișierului, [00:08:22](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=502s), face distincția explicită:

```text
EF BB BF | 0D 0A | 45
UTF-8 BOM | CR LF | E

0D hexadecimal = 13 decimal
0A hexadecimal = 10 decimal
45 hexadecimal = 69 decimal
```

[00:07:55](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=475s) Primele caractere de text din această demonstrație sunt un sfârșit de linie, dar nu trebuie să fie primii octeți fizici. [00:08:01](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=481s) Un Byte Order Mark (BOM) UTF-8 este prefixul opțional de trei octeți `EF BB BF`. Deplasează poziția la care încep octeții de text.

[00:08:16](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=496s) BOM înseamnă **Byte Order Mark**, nu „Mask”. Ordinea octeților este aranjarea octeților care formează o unitate multibyte; numele reflectă folosirea marcajului la codificările sensibile la ordinea octeților. UTF-8 nu are alegere privind ordinea octeților; acolo BOM-ul funcționează ca o semnătură de codificare. [00:08:22](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=502s) Octeții `0D 0A` care urmează sunt sfârșitul de linie Windows de două caractere din acest fișier UTF-8. [Explicația Unicode despre BOM](https://www.unicode.org/faq/utf_bom.html#BOM) clarifică distincția.

## Unicode UTF-8 și char-ul de doi octeți

[00:08:41](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=521s) Literele ruse nu apar în intervalul ASCII 0–127. [00:08:53](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=533s) Unicode furnizează puncte de cod — identificatori numerici — pentru caracterele din numeroase limbi. Un punct de cod Unicode și octeții folosiți pentru codificarea lui sunt idei înrudite, dar distincte.

[00:09:04](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=544s) UTF-8 este prezentat ca codificarea des folosită, alături de UTF-16 și UTF-32. Ele reprezintă același text Unicode folosind unități de cod de dimensiuni diferite. [00:09:09](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=549s) Caracterele ASCII își păstrează valorile ASCII pe un singur octet **în UTF-8**; această afirmație despre un singur octet nu trebuie extinsă la UTF-16 sau UTF-32. [00:09:20](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=560s) Alte caractere UTF-8 ocupă doi, trei sau patru octeți.

Următoarea comparație este un exemplu simplificat de reprezentare pentru discuția despre codificări, [00:09:49](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=589s), nu un program separat confirmat în surse:

```text
Character      UTF-8 bytes      UTF-16 code unit
H              48               0048
Ж              D0 96            0416
```

Tabelul folosește hexazecimalul. Prin urmare, `H` are în continuare codul zecimal 72, chiar dacă octetul său UTF-8 este scris ca `48` în dump.

[00:09:49](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=589s) Un octet conține opt biți. Cea mai mică unitate a UTF-8 este un octet; cea a UTF-16 este șaisprezece biți, adică doi octeți. Chiar și o literă ASCII ocupă o singură unitate UTF-16 de doi octeți. [00:10:10](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=610s) Compromisul de dimensiune depinde de text: multe caractere BMP care ocupă trei octeți în UTF-8 ocupă doi octeți în UTF-16. Caracterele din afara BMP ocupă patru octeți atât în UTF-8, cât și în UTF-16; UTF-32 folosește patru octeți pe punct de cod, deci nu este o opțiune universală de economisire a spațiului. [Comparația codificărilor Unicode](https://www.unicode.org/faq/utf_bom.html) dă regulile de la unul la patru octeți, de la una la două unități și de o singură unitate pe 32 de biți.

[00:10:26](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=626s) Asta ne readuce la `char`: el este exact o unitate de cod UTF-16 care ocupă doi octeți. BMP-ul, adică Planul Multilingv de Bază, acoperă punctele de cod până la U+FFFF. Un caracter BMP precum `Ж` încape într-un singur `char`; un punct de cod suplimentar necesită o pereche de surogate, adică două unități de cod UTF-16, și nu poate fi reprezentat drept un singur caracter complet într-un `char`. Așadar, „un caracter” este o descriere utilă de pornire pentru aceste exemple, dar nu o garanție pentru tot textul Unicode. [Definiția char în C#](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/char) enunță modelul bazat pe unități de cod.

[00:09:14](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=554s) Lecția nu implementează codificatoare și nu explică algoritmii lor la nivel de biți. Limita practică este să distingi codul unui caracter, unitățile sale UTF-16 din memorie și octeții de fișier codificați.

## Introducerea caracterelor greu de tastat

[00:10:52](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=652s) Un cod Alt este o scurtătură de tastatură pentru introducerea unui caracter care este incomod de tastat direct. În mediul demonstrat:

1. Ține apăsat Alt.
2. Tastează secvența numerică pe tastatura numerică.
3. Eliberează Alt pentru a insera caracterul rezultat.

[00:11:05](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=665s) Exemplele numerice din jurul valorilor 47 și 89 ilustrează tehnica; [00:11:15](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=675s) Alt+0151 este oferit pentru o liniuță lungă. Acestea sunt scurtături de introducere dependente de platformă și de aplicație, nu o regulă generală că introducerea Alt zecimală este întotdeauna egală cu un punct de cod Unicode. [Ghidul Microsoft pentru introducerea caracterelor](https://support.microsoft.com/en-us/word/insert-ascii-or-unicode-latin-based-symbols-and-characters) distinge metodele de introducere.

[00:11:19](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=679s) Simbolurile BMP rezultate pot încăpea într-un `char` de doi octeți. Ceea ce contează pentru stocare este caracterul rezultat, nu numărul de taste apăsate. Un exemplu simplificat de stocare a caracterului, [00:11:19](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=679s):

```csharp
char dash = '—'; // Un caracter BMP: o singură unitate de cod UTF-16.
```

## Construirea unui mesaj cu interpolare

[00:11:30](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=690s) Problema următoare este să inserăm valori care se schimbă într-un text lizibil fără să alipim manual fiecare bucată. **Interpolarea șirurilor** permite acest lucru punând `$` imediat înaintea ghilimelei duble de deschidere și plasând expresiile între acolade.

Urmează construcția:

1. Pornește de la un mesaj cu vârstă fixă, precum `"My Age is 12"`.
2. [00:11:39](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=699s) Păstrează vârsta într-o variabilă și înlocuiește numărul scris cu `{age}` într-un literal interpolat.
3. [00:11:46](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=706s) Afișează mesajul. Rezultatul înregistrat este `My Age is 12`.
4. [00:12:04](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=724s) Prelungește textul cu `Hello My name is {name}`, inserând numele în acea poziție.
5. [00:12:29](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=749s) Folosește între acolade o expresie precum `h + 10` când valoarea inserată trebuie calculată.

Modificare simplificată a codului mesajului cu vârstă, [00:11:39](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=699s):

```csharp
int age = 12;
string message = $"My Age is {age}";
Console.WriteLine(message); // My Age is 12
```

[00:11:55](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=715s) Interpolarea evaluează și formatează o valoare ca parte a unui singur mesaj comun. Textul literal obișnuit este copiat în rezultat; acoladele marchează expresia a cărei valoare devine text.

Stare simplificată cu nume și expresie, [00:12:04](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=724s), cu extinderea prin expresie la [00:12:29](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=749s):

```csharp
string name = "Alex"; // Un nume ilustrativ.
int h = 12;
string greeting = $"Hello My name is {name}. My Age is {h + 10}";
Console.WriteLine(greeting);
// Hello My name is Alex. My Age is 22
```

O expresie calculează o valoare: `h + 10` este evaluat la 22 în acest exemplu, valoare care este apoi inserată. Ultima afișare este rezultatul prezis al exemplului; testul înregistrat cu vârsta de mai sus este rezultatul demonstrat.

[00:12:20](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=740s) **Corecție privind word wrap:** word wrap pliază vizual o linie lungă în editor; nu adaugă un newline în șir. Transcrierea distinge explicit între afișarea pe linii împărțite și o întreruper reală de linie. Spațiile și celelalte texte literale rămân în rezultat, în timp ce un `\n` explicit creează un LF real. Literalii de șir obișnuiți, între ghilimele, nu pot conține pur și simplu un newline fizic din sursă neescapat. Acest exemplu folosește forma interpolată obișnuită descrisă în [referința C# pentru interpolare](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated).

<details>
<summary>Schimbarea lui age după crearea mesajului modifică automat mesajul?</summary>

Nu. Expresia este evaluată la crearea șirului. Recreează mesajul pentru a include noua vârstă.

</details>

## Alipirea șirurilor și păstrarea unui instantaneu

[00:12:47](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=767s) **Concatenarea** alipește bucăți de text existente. Metoda statică `string.Concat` primește argumente de tip șir și returnează textul lor combinat. „Static” înseamnă că metoda este apelată pe `string`, nu pe o anumită instanță de șir.

1. [00:12:47](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=767s) Apelează `string.Concat` cu primul și al doilea șir.
2. [00:12:59](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=779s) Afișează rezultatul: testul înregistrat arată primul element de intrare urmat imediat de al doilea.
3. [00:13:12](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=792s) Gândește-te dacă rezultatul rămâne legat de variabile.
4. [00:13:26](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=806s) Reatribuie prima variabilă. Textul alipit anterior rămâne neschimbat.
5. [00:13:44](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=824s) Adaugă `", "` ca încă un argument pentru a insera un separator.
6. [00:14:02](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=842s) Compară această construcție cu interpolarea.
7. [00:14:09](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=849s) Înlocuiește apelul explicit cu operatorul `+` suprasărcinat pentru șiruri.

Stări simplificate ale alipirii inițiale și ale reatribuirii, [00:12:47](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=767s) și [00:13:26](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=806s):

```csharp
string a = "Hello";
string b = "World";
string c = string.Concat(a, b);
Console.WriteLine(c); // HelloWorld

a = "Changed";
Console.WriteLine(c); // Tot HelloWorld
```

[00:13:12](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=792s) Un instantaneu păstrează textul obținut la momentul construirii sale. `Concat` primește referințe către obiectele de șir curente, nu o legătură permanentă cu variabilele `a` și `b`. [00:13:26](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=806s) În acest exemplu cu două șiruri nevide, construiește un obiect nou ce conține caracterele primului obiect și apoi caracterele celui de-al doilea. Reatribuirea lui `a` îi schimbă referința, nu șirul original și nici rezultatul alipit.

Modificare simplificată a separatorului, [00:13:44](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=824s), folosind din nou elementele de intrare originale:

```csharp
string a = "Hello";
string b = "World";
string explicitJoin = string.Concat(a, ", ", b);
string interpolated = $"{a}, {b}";
string withPlus = a + ", " + b;
Console.WriteLine(explicitJoin); // Hello, World
```

[00:14:02](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=842s) Atât interpolarea, cât și un apel explicit la `Concat` sunt moduri valide de a construi acest text. [00:14:09](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=849s) Pentru șiruri, operatorul `+` suprasărcat înseamnă concatenare, nu adunare numerică. [00:14:28](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=868s) Compilatorul poate transforma această expresie cu trei șiruri într-un apel `string.Concat` cu cei trei operanzi. Asta explică rezultatul echivalent pentru acest exemplu; nu este o promisiune că orice expresie de șir sau formă de interpolare are instrucțiuni compilate identice.

<details>
<summary>Dacă a devine Changed, ce trebuie să faci pentru a obține Changed, World?</summary>

Construiește un rezultat nou folosind valorile curente ale lui a și b. Rezultatul existent este un instantaneu imutabil și nu se poate actualiza singur.

</details>

## Conversia unei valori în text cu ToString

[00:14:36](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=876s) Obiectele .NET moștenesc `ToString`, o metodă care obține reprezentarea lor textuală. [00:14:41](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=881s) Valori precum `int`, `char` și `float` o pot folosi atunci când un șir este cerut explicit.

Stare finală simplificată a conversiei, [00:14:41](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=881s):

```csharp
int age = 12;
char initial = 'H';
float value = 1.5f;
string ageText = age.ToString();       // "12"
string initialText = initial.ToString(); // "H"
string valueText = value.ToString();   // Formatarea depinde de mediu.
Console.WriteLine(age);               // Nu este nevoie de ToString explicit.
```

Aici `ToString` produce text, spre deosebire de cast-ul lui `'H'` la `int`, care produce codul numeric 72. Sufixul `f` scrie un literal de tip `float`.

[00:14:44](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=884s) Conversia explicită este utilă atunci când o altă operație cere un `string`. Afișarea de obicei nu o cere: `Console.WriteLine` are supraîncărcări pentru tipurile de valoare uzuale și o supraîncărcare `object` pentru celelalte obiecte. Ea selectează supraîncărcarea aplicabilă și afișează reprezentarea. [API-ul WriteLine](https://learn.microsoft.com/en-us/dotnet/api/system.console.writeline) documentează aceste alternative. Limita finală a lecției este reprezentarea de bază și construirea mesajelor, nu formatarea personalizată sau implementarea acelor metode de bibliotecă.

## Istoricul modificărilor de cod

Checkout-ul deținut nu a oferit o sursă confirmată exactă pentru aceste demonstrații. Următoarele legături stabile către stările din videoclip înregistrează progresia relevantă, de la cea mai veche:

- [00:00:32](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=32s) — Pornește de la un obiect șir imutabil; reatribuirea schimbă o referință.
- [00:01:27](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=87s) — Citește un caracter după indice și compară afișarea caracterului cu cea a întregului.
- [00:02:53](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=173s) — Pornește de la 72, adaugă cast-ul explicit la char și afișează H.
- [00:06:10](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=370s) — Inserează LF cu o secvență de evitare; testează afișarea pe două linii.
- [00:07:16](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=436s) — Inspectează octeții fișierului, apoi identifică BOM-ul și prefixul CRLF.
- [00:11:39](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=699s) — Înlocuiește o vârstă fixă cu interpolare; o prelungește cu un nume și o expresie.
- [00:12:47](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=767s) — Alipește șiruri cu Concat; arată comportamentul de instantaneu și adaugă un separator.
- [00:14:09](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=849s) — Folosește adunarea șirurilor pentru aceeași alipire.
- [00:14:36](https://www.youtube.com/watch?v=dTbrzEXPH-s&t=876s) — Se încheie cu ToString și motivul pentru care WriteLine de obicei nu are nevoie de conversie explicită.