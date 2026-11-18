> Această notă a fost generată de AI (gpt-6.1-sol) pe baza videoclipului asociat și poate conține greșeli. Verificați videoclipul și sursele citate atunci când acuratețea contează.
> Traducerea în limba română a fost generată de AI (opencode/space-bunny-free) din nota în limba engleză și poate conține greșeli. Verificați nota originală atunci când acuratețea contează.

# Scop și termeni

[00:00:26](https://www.youtube.com/watch?v=z5CUTiubzwU&t=26s) Scopul acestei lecții este să facă vizibile **dependențele** unui program: să identificăm de ce are nevoie o transformare simplă a unui element, să decidem cum îi livrăm ce are nevoie și să schimbăm structura fără a pierde rezultatul testat. Traseul este deliberat incremental: reguli inline, o funcție extrasă, un pachet de configurație, servicii, comportamente alternative, o reuniune închisă și, în final, asamblarea automată a dependențelor. Acesta este cel mai simplu exemplu posibil; un videoclip ulterior tratează o situație mai dificilă și mai realistă.

Termenii folosiți în continuare:

- **Element, câmp, proprietate, inițializator, required și float:** un element este o intrare de tip nume și preț; un câmp stochează date; o proprietate expune accesul la un membru prin accesori; un inițializator atribuie membri în timpul creării; `required` impune inițializarea unui membru; `float` este tipul numeric de precizie simplă folosit pentru prețuri.
- **Parametru, argument, semnătură, prototip și context:** un parametru declară o intrare, un argument îi furnizează valoarea, o semnătură descrie forma intrări/ieșire a unui element apelabil, un prototip declară acea formă fără corp, iar contextul este informația din jur necesară pentru efectuarea operației.
- **Dependență, intrare directă și configurație:** o dependență este ceva de care are nevoie o operație; intrarea directă este valoarea procesată în acest apel concret; configurația stabilește regulile care guvernează procesarea. Lecția distinge și dependențe de tip serviciu.
- **Listă, dicționar, mulțime și valoare de rezervă:** o listă păstrează o colecție ordonată; un dicționar mapează chei la valori; o mulțime permite verificări de apartenență; o valoare de rezervă este valoarea folosită când o căutare nu are nicio intrare.
- **Responsabilitate unică, abstractizare, încapsulare și refactorizare:** responsabilitatea unică atribuie unei componente o singură sarcină coerentă; abstractizarea expune contractul util; încapsularea ascunde reprezentarea sau dependențele în spatele lui; refactorizarea schimbă structura păstrând comportamentul observabil intenționat.
- **Rezultat nullable și null:** un element nullable poate fi un element sau null; null înseamnă că această transformare nu a produs niciun element.
- **xUnit, Verify, instantaneu (snapshot), regresie, async și await:** xUnit rulează testele; Verify compară ieșirea reală serializată cu un instantaneu aprobat, adică rezultatul așteptat salvat; o regresie este o schimbare nedorită a comportamentului; `async` permite așteptarea asincronă, iar `await` așteaptă finalizarea.
- **Serviciu, constructor, private, readonly și static:** un serviciu leagă o operație de contextul ei la nivel de clasă; un constructor inițializează o instanță; `private` ascunde un membru de accesul exterior; `readonly` restricționează reatribuirea unui câmp; `static` aparține unui tip, nu unei instanțe.
- **Record struct:** un tip valoare cu membri orientați pe date generați de compilator; aici este folosit ca pachet de configurație concis.
- **Delegat, grup de metode, lambda, captură, felie (slice) și funcție pură:** un delegat este o valoare apelabilă cu semnătură declarată; un grup de metode numește o metodă compatibilă; o lambda este o funcție anonimă; captura îi oferă acces la variabilele din jur; o felie selectează o parte dintr-un șir sau dintr-o secvență; o funcție pură își determină răspunsul din intrările sale, fără efecte secundare externe.
- **Interfață, implementare, polimorfism și Strategy:** o interfață specifică operații; o implementare furnizează corpuri; polimorfismul permite ca același apel să ajungă la implementări diferite; Strategy alege o operație furnizând o implementare.
- **Ierarhie deschisă și ierarhie închisă:** consumatorii pot adăuga implementări într-o ierarhie deschisă; o ierarhie închisă enumeră alternativele suportate.
- **Reuniune etichetată, variantă, dispatch, conversie (cast), tipar de tip și dynamic:** o reuniune etichetată reprezintă exact o alternativă aleasă; fiecare alternativă este o variantă; dispatch îi selectează operația; o conversie recuperează un tip cunoscut; un tipar de tip verifică și denumește o valoare tipizată; `dynamic` amână selectarea membrului până la runtime.
- **Clasă, struct, tip referință, tip valoare, stivă, heap și boxing:** o clasă este un tip referință; un `struct` este un tip valoare; referințele identifică obiecte, iar valorile își conțin conținutul; stiva și heap-ul descriu stocarea; boxing-ul copiază o valoare într-o reprezentare de obiect pe heap-ul gestionat.
- **Injecția dependențelor, inversarea controlului și inversarea dependențelor:** injecția furnizează dependențele din exterior; inversarea controlului dă codului exterior responsabilitatea selectării și creării lor; inversarea dependențelor exprimă o dependență printr-o abstractizare.
- **Container DI, container IoC, furnizor de servicii și graf de dependențe:** DI și containerul IoC denumesc mecanismul de asamblare a dependențelor; un furnizor este obiectul de rezolvare a serviciilor construit din el; un graf descrie ce obiecte necesită alte obiecte.
- **Colecție de servicii, înregistrare, descriptor, fabrică, metodă de extensie, argument de tip generic și builder:** colecția stochează înregistrări mutabile; un descriptor definește modul în care este furnizat un tip de serviciu; o fabrică creează o instanță; o metodă de extensie adaugă sintaxă apelabilă unui tip existent; un argument de tip generic selectează un tip între paranteze ascuțite; un builder transformă configurația într-un model finit utilizabil.
- **Singleton, scope și durată de viață scoped:** o înregistrare singleton reutilizează o instanță în interiorul unui furnizor; un scope grupează un context de execuție; serviciile scoped sunt reutilizate în acel scope.
- **Options, IOptions, IOptionsSnapshot și pipeline:** Options grupează setări înrudite într-un tip cu nume; IOptions este un înveliș tipizat pentru accesarea opțiunilor; IOptionsSnapshot furnizează opțiuni memorate în cache pentru un scope; un pipeline este o succesiune de pași de procesare.

Pentru exerciții, laboratorul înrudit [dependențe](../../../labs/2_design/05_dependenices.md) aplică aceste idei la citirea, transformarea și scrierea datelor. Este o asignare separată, nu o dovadă a unui pas de instalare sau a unei stări intermediare a codului din această înregistrare.

Toate exemplele de mai jos sunt exemple scurte, didactice. O citare imutabilă identifică o stare salvată care a fost inspectată; când editarea live nu este păstrată exact, citarea este etichetată explicit ca fiind conexă sau finală. Timestamp-urile din video identifică pasul tranzitoriu.

## Pornim cu un proiect de cod și un proiect de teste

[00:00:00](https://www.youtube.com/watch?v=z5CUTiubzwU&t=0s) Pornim cu două proiecte: unul pentru codul principal și unul pentru teste. Descrierea înregistrării face trimitere la șablonul GitHub. [00:00:10](https://www.youtube.com/watch?v=z5CUTiubzwU&t=10s) Clasele sunt goale, dar instrumentele pentru aserțiuni și pentru instantanee sunt deja configurate. **xUnit** este runnerul de teste, iar **Verify** este biblioteca de comparare a instantaneelor.

[helperul din șablon](https://github.com/AntonC9018/uniCourse_csharp/blob/5253379090ffec2086654d0a6d1957e0dbd81637/examples/dependencies_simplified/code/Code.cs), [clasa de test goală](https://github.com/AntonC9018/uniCourse_csharp/blob/5253379090ffec2086654d0a6d1957e0dbd81637/examples/dependencies_simplified/tests/Tests.cs) și [referințele de proiect din șablon](https://github.com/AntonC9018/uniCourse_csharp/blob/5253379090ffec2086654d0a6d1957e0dbd81637/examples/dependencies_simplified/tests/tests.csproj) stabilesc starea inițială salvată. Formă simplificată:

```csharp
// În proiectul de cod:
public static class Helper { }

// În proiectul de teste:
public class Tests { }
```

Proiectul de teste face referire la proiectul de cod și importă xUnit, Verify și Verify.Xunit. De aceea exemplul se poate concentra pe dependențe, nu pe configurarea pachetelor.

## Construim transformarea inline

[00:00:38](https://www.youtube.com/watch?v=z5CUTiubzwU&t=38s) Un **element** este intrarea de tip nume și preț a operației. Dăm elementului membri de date, apoi parcurgem o **listă**, o colecție ordonată de elemente, aplicând aceleași reguli fiecărui element.

1. [00:00:58](https://www.youtube.com/watch?v=z5CUTiubzwU&t=58s) Creăm o listă de răspunsuri, punem în ea elementele transformate și o returnăm. Doar efectuarea unor acțiuni ar lăsa apelantul fără niciun rezultat.
2. [00:01:09](https://www.youtube.com/watch?v=z5CUTiubzwU&t=69s) Sărim elementele cu preț sub patru: nu efectuăm nicio transformare și nu adăugăm nimic pentru ele.
3. [00:01:16](https://www.youtube.com/watch?v=z5CUTiubzwU&t=76s) Aplicăm și o limită superioară de preț, astfel încât și un element prea scump să dispară din răspuns.
4. [00:01:27](https://www.youtube.com/watch?v=z5CUTiubzwU&t=87s) Adăugăm datele elementului necesare pentru compararea prețului minim. Schița vorbită se referă la stocarea informației despre prețul minim al elementului; implementarea salvată ajunge în cele din urmă să stocheze `Price` pe element și să furnizeze separat pragul minim.
5. [00:01:40](https://www.youtube.com/watch?v=z5CUTiubzwU&t=100s) Înlocuim numele special cu o marcă, trecem numele rezultat la majuscule, apoi adăugăm elementul nou în răspuns.

Nu există un commit inspectat pentru fiecare tastă codificată fix. [Prima transformare salvată, conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/2e88085eed097ef2a1e1345951fa37665caf0756/examples/dependencies_simplified/code/Code.cs) păstrează aceeași succesiune de filtrare/remapare/majuscule după introducerea configurației. Această reconstrucție simplificată a stării de la [00:01:40](https://www.youtube.com/watch?v=z5CUTiubzwU&t=100s) folosește o limită superioară ilustrativă:

```csharp
public class Item
{
    public required string Name;
    public required float Price;
}

// În interiorul funcției originale de procesare a listei:
var answer = new List<Item>();
foreach (var item in items)
{
    if (item.Price < 4) continue;
    if (item.Price > 50) continue;
    string name = item.Name == "Anton" ? "Mark" : item.Name;
    answer.Add(new Item { Name = name.ToUpper(), Price = item.Price });
}
return answer;
```

Un **câmp** stochează datele elementului. Un **inițializator**, acoladele care urmează după `new Item`, furnizează membrii săi. Expresia cu semnul întrebărei și două puncte alege o înlocuire sau numele original; `continue` sare peste restul lucrului pentru această iterație a buclei. Verificările salvate ulterior folosesc `<=` și `>=`, excluzând chiar valorile prag. Nu deduceți comportamentul pentru egalitate din această descriere verbală timpurie despre „sub”.

## Lăsăm apelanții să aleagă regulile

[00:01:55](https://www.youtube.com/watch?v=z5CUTiubzwU&t=115s) Valorile minime și maxime codificate fix împiedică reutilizarea cu o altă politică. Un **parametru** declară o intrare a funcției; un **argument** furnizează o valoare la locul apelului. Adăugăm câte un parametru pentru fiecare prag.

1. [00:02:10](https://www.youtube.com/watch?v=z5CUTiubzwU&t=130s) Introducem un **dicționar**, o mapare de chei la valori, pentru transformările de nume. Regulile devin date furnizate de apelant, în loc de cazuri speciale în funcție.
2. [00:02:19](https://www.youtube.com/watch?v=z5CUTiubzwU&t=139s) Adăugăm intrări numite de minim și de maxim în discuția de configurare în evoluție. Versiunea tipizată salvată se stabilește pe două argumente numerice de prag și un dicționar separat de nume de tip șir la șir; nu pune `float`-uri în acel dicționar de nume.
3. [00:02:23](https://www.youtube.com/watch?v=z5CUTiubzwU&t=143s) Căutăm numele curent pentru a-i selecta înlocuirea.
4. [00:02:32](https://www.youtube.com/watch?v=z5CUTiubzwU&t=152s) Folosim numele original drept **valoare de rezervă** atunci când nu există nicio mapare. Faptul că nu este configurat nu face un element invalid.

[Versiunea salvată bazată pe parametri](https://github.com/AntonC9018/uniCourse_csharp/blob/2e88085eed097ef2a1e1345951fa37665caf0756/examples/dependencies_simplified/code/Code.cs) are această formă simplificată:

```csharp
static List<Item> ProcessItems(
    List<Item> items, float minPriceCutoff, float maxPriceCutoff,
    Dictionary<string, string> nameRemap)
{
    var answer = new List<Item>();
    foreach (var item in items)
    {
        if (item.Price <= minPriceCutoff) continue;
        if (item.Price >= maxPriceCutoff) continue;
        var name = nameRemap.GetValueOrDefault(item.Name, item.Name);
        answer.Add(new Item { Name = name.ToUpper(), Price = item.Price });
    }
    return answer;
}
```

**Configurație** înseamnă setările care controlează procesarea. Aici include pragurile și maparea. **float** este tipul numeric al pragurilor. `GetValueOrDefault` folosește al doilea argument când cheia lipsește; nu sare peste trecerea la majuscule. Elementul salvat `Item` are câmpurile `required` `Name` și `Price`: **required** impune inițializarea lor.

## Înregistrăm rezultatul cu Verify

[00:02:39](https://www.youtube.com/watch?v=z5CUTiubzwU&t=159s) Construim date de test care exercită ramurile: un element ieftin de eliminat, un element acceptat nemapat, un element scump de eliminat și numele special marcat.

1. [00:02:58](https://www.youtube.com/watch?v=z5CUTiubzwU&t=178s) Adăugăm un nume complet scris cu litere mici pentru a verifica trecerea la majuscule.
2. [00:03:09](https://www.youtube.com/watch?v=z5CUTiubzwU&t=189s) Apelăm funcția cu reguli explicite, inclusiv dicționarul de nume către marcă. Testul controlează politica.
3. [00:03:23](https://www.youtube.com/watch?v=z5CUTiubzwU&t=203s) Trecem răspunsul rezultat către **Verify**, ajutorul de aserțiune care compară rezultatul serializat cu un **instantaneu** aprobat.
4. [00:03:30](https://www.youtube.com/watch?v=z5CUTiubzwU&t=210s) La prima rulare nu există încă ieșire așteptată, deci Verify produce un instantaneu primit, pentru revizuire.
5. [00:03:37](https://www.youtube.com/watch?v=z5CUTiubzwU&t=217s) Îl inspectăm: numele configurat devine marca, numele nemapat își păstrează identitatea, iar numele cu litere mici este transformat în majuscule.
6. [00:03:46](https://www.youtube.com/watch?v=z5CUTiubzwU&t=226s) Salvăm/aprobăm instantaneul așteptat; noua rulare reușește.
7. [00:03:53](https://www.youtube.com/watch?v=z5CUTiubzwU&t=233s) Modificăm intrarea sau implementarea și comparăm din nou. O diferență neașteptată face testul să eșueze și este afișată pentru inspecție. Același mecanism de regresie este reiterat la [00:03:59](https://www.youtube.com/watch?v=z5CUTiubzwU&t=239s) și [00:04:01](https://www.youtube.com/watch?v=z5CUTiubzwU&t=241s).

[Primul test salvat](https://github.com/AntonC9018/uniCourse_csharp/blob/2e88085eed097ef2a1e1345951fa37665caf0756/examples/dependencies_simplified/tests/Tests.cs) și [rezultatul aprobat](https://github.com/AntonC9018/uniCourse_csharp/blob/2e88085eed097ef2a1e1345951fa37665caf0756/examples/dependencies_simplified/tests/Tests.Test1.verified.txt) păstrează acest rezultat. Apel și așteptare simplificate:

```csharp
var result = Helper.ProcessItems(
    items, minPriceCutoff: 5.0f, maxPriceCutoff: 50.0f,
    nameRemap: new Dictionary<string, string> { ["Anton"] = "Mark" });
await Verify(result);
// Nume așteptate: MARK, FORK, LOWER-CASE
// Prețuri așteptate: 40.0, 26.0, 40.0
```

O **regresie** este o schimbare nedorită a comportamentului stabilit. „Nemodificat” aici înseamnă nemapat: Fork devine în continuare FORK, deoarece trecerea la majuscule este necondiționată după căutare. În testul salvat, **async** permite așteptarea asincronă, iar **await** așteaptă ca Verify să se termine. Argumentele nume identifică explicit parametrii lor; sufixul `f` face dintr-o literali numeric un `float`.

Un fișier „primit” de la prima rulare nu este dovada că conținutul său e corect: inspecționați-l înainte de a-l face baseline aprobat. Secvența de reușită, eșec și aprobare de mai sus descrie înregistrarea; aceste note nu pretind o nouă rulare locală de teste.

<details>
<summary>Preziceți numele când un dicționar nu are intrare pentru „lower-case”.</summary>

Căutarea întoarce „lower-case” ca valoare de rezervă, iar trecerea la majuscule produce LOWER-CASE. Dicționarul controlează remaparea, nu faptul că operația ulterioară de trecere la majuscule se execută.

</details>

## Extragem transformarea unui singur element

[00:04:11](https://www.youtube.com/watch?v=z5CUTiubzwU&t=251s) **Principiul responsabilității unice** atribuie unei funcții o singură sarcină coerentă. Lecția îl formulează ca „fă un singur lucru”. [00:04:22](https://www.youtube.com/watch?v=z5CUTiubzwU&t=262s) Funcția curentă creează un răspuns, parcurge intrările și aplică regulile de transformare. Aceste responsabilități distincte ascund de ce are nevoie transformarea.

1. [00:04:38](https://www.youtube.com/watch?v=z5CUTiubzwU&t=278s) Separăm parcurgerea de transformare.
2. [00:04:55](https://www.youtube.com/watch?v=z5CUTiubzwU&t=295s) Decidem ce face operația extrasă și cum ar trebui să fie **interfața** ei, contractul vizibil apelanților.
3. [00:05:00](https://www.youtube.com/watch?v=z5CUTiubzwU&t=300s) Definim **semnătura** sa, forma intrări/ieșire, ca un element care intră și, posibil, unul care iese.
4. [00:05:13](https://www.youtube.com/watch?v=z5CUTiubzwU&t=313s) Returnăm null când nu se produce niciun element nou. [00:05:16](https://www.youtube.com/watch?v=z5CUTiubzwU&t=316s) **null** înseamnă absența unui element de ieșire, nu un element de eroare.
5. [00:05:33](https://www.youtube.com/watch?v=z5CUTiubzwU&t=333s) Adăugăm în lista de răspunsuri doar rezultatele diferite de null.

Extragerea live are declarații incomplete cât timp este editată. [Extracția completată, conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/407acfc8643e7a38c4031696e2eaa858541a46e4/examples/dependencies_simplified/code/Code.cs) păstrează separarea după adăugarea configurării numelor ignorate. Parcurgere simplificată:

```csharp
var answer = new List<Item>();
foreach (var item in items)
{
    Item? newItem = ProcessItem(
        item, minPriceCutoff, maxPriceCutoff, nameRemap, ignoredNames);
    if (newItem != null) answer.Add(newItem);
}
return answer;
```

Un **rezultat nullable**, scris `Item?`, permite fie un `Item`, fie null. Parcurgerea deține iterația și acumularea; `ProcessItem` deține transformarea și deciziile de respingere. Aceasta este **refactorizare**: reorganizarea programului păstrând stabilă ieșirea intenționată.

## Declarăm intrarea directă

[00:05:42](https://www.youtube.com/watch?v=z5CUTiubzwU&t=342s) Ceva de ce are nevoie direct metoda extrasă este o **dependență**. Elementul curent este necesar la fiecare apel, iar metoda ar putea fi invocată repetat sau din locuri diferite, deci ajunge ca parametru.

[00:05:59](https://www.youtube.com/watch?v=z5CUTiubzwU&t=359s) În clasificarea acestei lecții, acel element este **dependența directă de tip parametru**, intrarea pentru această invocare. [00:06:11](https://www.youtube.com/watch?v=z5CUTiubzwU&t=371s) Configurația și serviciile sunt celelalte categorii, introduse mai târziu.

1. [00:06:16](https://www.youtube.com/watch?v=z5CUTiubzwU&t=376s) Scriem din nou contractul: sosește un element și poate ieși un element nou.
2. [00:06:23](https://www.youtube.com/watch?v=z5CUTiubzwU&t=383s) Declarăm parametrul pentru elementul primit și tipul de întoarcere `Item?`.
3. [00:06:32](https://www.youtube.com/watch?v=z5CUTiubzwU&t=392s) Folosim `return null` în ramurile fără ieșire. Această stare intermediară live încă nu compilează; extragerea nu și-a primit încă tot contextul anterior.

[Declarația completată, conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/407acfc8643e7a38c4031696e2eaa858541a46e4/examples/dependencies_simplified/code/Code.cs) este mai completă decât acea stare temporară. Reamintire a semnăturii:

```csharp
// Forma prototip înainte de a fi restaurat contextul lipsă:
Item? ProcessItem(Item item /* configurația lipsește încă */);
```

Un **prototip** enunță forma intrări/ieșire a unui element apelabil fără corpul implementării; aceasta este o semnătură schematică, nu o metodă C# completă și autonomă. Marcajul nullable descrie rezultatul, nu un element de intrare nullable. Extragerea unui bloc rămâne neterminată până când datele sale necesare au traversat noua limită.
## Trecem explicit configurația

[00:06:46](https://www.youtube.com/watch?v=z5CUTiubzwU&t=406s) Pe lângă element, funcția are nevoie de **configurație**, valorile care îi determină regulile. Extragerea lasă acei identificatori indisponibili până când contextul lor este furnizat.

[00:07:08](https://www.youtube.com/watch?v=z5CUTiubzwU&t=428s) Dacă le-am face globale, am ascunde dependența și am forța în mod greșit apelanți diferiți să împartă o singură politică. Valorile pot diferi între căi de invocare și surse.

1. [00:07:30](https://www.youtube.com/watch?v=z5CUTiubzwU&t=450s) Preluăm cele trei valori de context existente din metoda exterioară și le declarăm ca parametri ai metodei interioare.
2. [00:07:37](https://www.youtube.com/watch?v=z5CUTiubzwU&t=457s) Le transmitem direct la apel. Configurația rămâne o dependență directă a consumatorului ei, chiar dacă lecția îi distinge rolul de elementul per apel.
3. [00:07:53](https://www.youtube.com/watch?v=z5CUTiubzwU&t=473s) Recunoaștem de ce aparțin aceste setări operației interioare: scopul lor inițial era să execute corect acea logică interioară.

[Versiunea completată cu parametri, conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/407acfc8643e7a38c4031696e2eaa858541a46e4/examples/dependencies_simplified/code/Code.cs) adaugă ulterior mulțimea numelor ignorate ca a patra valoare de configurație. Semnătură simplificată:

```csharp
static Item? ProcessItem(
    Item item,
    float minPriceCutoff, float maxPriceCutoff,
    Dictionary<string, string> nameRemap,
    HashSet<string> ignoredNames)
{
    // Fiecare identificator necesar regulilor este furnizat explicit.
    if (item.Price <= minPriceCutoff || item.Price >= maxPriceCutoff)
        return null;
    // Urmează restul regulilor.
    return new Item { Name = item.Name, Price = item.Price };
}
```

Numărul se schimbă în timpul construcției: elementul inițial plus trei setări devin elementul plus patru setări odată cu adăugarea ignorării. Rolul semantic al unei dependențe și sintaxa livrării ei sunt întrebări diferite; o valoare de configurație poate fi livrată în continuare ca parametru.

## Adăugăm nume ignorate și urmărim argumentul spre exterior

[00:08:16](https://www.youtube.com/watch?v=z5CUTiubzwU&t=496s) Adăugăm regula „sari elementele al căror nume este ignorat”. O **mulțime** este o colecție care permite verificări de apartenență; versiunea salvată folosește `HashSet<string>` pentru aceste nume.

1. [00:08:37](https://www.youtube.com/watch?v=z5CUTiubzwU&t=517s) Adăugăm parametrul de mulțime la funcția care are nevoie de el, apoi îl transmitem la fiecare invocare a acelei funcții.
2. [00:09:05](https://www.youtube.com/watch?v=z5CUTiubzwU&t=545s) Observăm costul: fiecare apelant intermediar trebuie să transporte configurația chiar dacă doar o retransmite.
3. [00:09:23](https://www.youtube.com/watch?v=z5CUTiubzwU&t=563s) Actualizăm apelul din test pentru a furniza numele ignorate.
4. [00:09:37](https://www.youtube.com/watch?v=z5CUTiubzwU&t=577s) Adăugăm încă o intrare ignorată păstrând răspunsul așteptat neschimbat. Testul prezentat arată că ignorarea o elimină.
5. [00:10:01](https://www.youtube.com/watch?v=z5CUTiubzwU&t=601s) Actualizăm și semnătura funcției exterioare și apelul de retransmitere. Noile dependențe se răspândesc spre exterior prin lanțul de apeluri.

[Extracția salvată cu transportul numelor ignorate](https://github.com/AntonC9018/uniCourse_csharp/blob/407acfc8643e7a38c4031696e2eaa858541a46e4/examples/dependencies_simplified/code/Code.cs) înregistrează această formă:

```csharp
static List<Item> ProcessItems(
    List<Item> items, float min, float max,
    Dictionary<string, string> map, HashSet<string> ignored)
{
    var answer = new List<Item>();
    foreach (var item in items)
    {
        var output = ProcessItem(item, min, max, map, ignored);
        if (output != null) answer.Add(output);
    }
    return answer;
}

// În interiorul lui ProcessItem:
if (ignoredNames.Contains(item.Name)) return null;
```

Aceasta este retransmitere de **context**: funcția exterioară transportă ce are nevoie operația interioară. Instantaneul neschimbat este semnificativ, pentru că a fost introdusă deliberat o intrare nouă care nu ar trebui să producă ieșire.

## Grupăm setările

[00:10:10](https://www.youtube.com/watch?v=z5CUTiubzwU&t=610s) Pentru a livra împreună toate setările metodei interioare, creăm o clasă sau un `struct`; această alegere nu contează pentru acest pas de grupare. [00:10:12](https://www.youtube.com/watch?v=z5CUTiubzwU&t=612s) Un pachet înlocuiește lunga listă de argumente retransmise.

1. [00:10:20](https://www.youtube.com/watch?v=z5CUTiubzwU&t=620s) Îl numim `ProcessItemConfig`, pentru că conține **configurația** operației, nu orice fel de dependență.
2. [00:10:32](https://www.youtube.com/watch?v=z5CUTiubzwU&t=632s) Păstrăm intrarea directă a elementului în afara pachetului.
3. [00:10:37](https://www.youtube.com/watch?v=z5CUTiubzwU&t=637s) Mutăm setările separate în noul tip; acceptăm și retransmitem o singură valoare de configurație.
4. [00:10:55](https://www.youtube.com/watch?v=z5CUTiubzwU&t=655s) Numim parametrul `processItemConfig` după abstractizarea pe care o reprezintă.
5. [00:11:02](https://www.youtube.com/watch?v=z5CUTiubzwU&t=662s) Actualizăm testele să construiască pachetul cu valorile sale înainte de a-l transmite metodei exterioare.
6. [00:11:31](https://www.youtube.com/watch?v=z5CUTiubzwU&t=691s) Întregul pachet călătorește mai departe către operația imbricată. [00:11:32](https://www.youtube.com/watch?v=z5CUTiubzwU&t=692s) Apelanții nu mai repetă fiecare argument individual; consumatorul citește membrii de care are nevoie.

[Pachetul de configurație salvat](https://github.com/AntonC9018/uniCourse_csharp/blob/9968dcccb5b84a1622eb0e2f93b4b2b3de182146/examples/dependencies_simplified/code/Code.cs) folosește un **record struct**, un tip valoare concis cu membri orientați pe date generați. Construire și retransmitere simplificate:

```csharp
public readonly record struct ProcessItemConfig(
    float minPriceCutoff, float maxPriceCutoff,
    Dictionary<string, string> nameRemap,
    HashSet<string> ignoredNames);

var config = new ProcessItemConfig(
    5, 50, new Dictionary<string, string> { ["Anton"] = "Mark" },
    new HashSet<string> { "ignored" });
var answer = Helper.ProcessItems(items, config);
// Bucla exterioară retransmite: ProcessItem(item, processItemConfig)
```

**Abstractizare** înseamnă conceptul util prezentat apelanților: „configurație pentru procesarea unui element”. Reduce sintaxa de retransmitere, dar apelanții decid în continuare valorile de configurație. **readonly** limitează modificarea stării generate a structului; nu face dicționarul sau mulțimea referite imutabile.

## Dăm unei operații propriul context

[00:11:42](https://www.youtube.com/watch?v=z5CUTiubzwU&t=702s) **Serviciile** sunt următoarea categorie de dependențe: o operație împachetată împreună cu datele de care are nevoie. [00:11:52](https://www.youtube.com/watch?v=z5CUTiubzwU&t=712s) Remaparea numelor înseamnă în prezent căutare în dicționar. Acea reprezentare nu trebuie să definească întreaga abstractizare.

1. [00:12:02](https://www.youtube.com/watch?v=z5CUTiubzwU&t=722s) Considerăm alte reguli: înlocuirea numelor cu un prefix potrivit sau extragerea unei felii pentru un sufix potrivit.
2. [00:12:22](https://www.youtube.com/watch?v=z5CUTiubzwU&t=742s) Permitem reguli de remapare mai specifice, dincolo de intrările dicționarului.
3. [00:12:34](https://www.youtube.com/watch?v=z5CUTiubzwU&t=754s) Aplicăm responsabilitatea unică: separăm politica de remapare de succesiunea mai largă care aplică reguli multiple.
4. [00:12:46](https://www.youtube.com/watch?v=z5CUTiubzwU&t=766s) Extragerea ei ca altă funcție ar necesita transmiterea și a dependenței sale de date. [00:12:54](https://www.youtube.com/watch?v=z5CUTiubzwU&t=774s) Contextul dependent de dicționar trebuie oricum să ajungă explicit la acea funcție.
5. [00:13:02](https://www.youtube.com/watch?v=z5CUTiubzwU&t=782s) În schimb, grupăm transformarea și contextul ei ca pe un **serviciu**, astfel încât să călătorească împreună.
6. [00:13:13](https://www.youtube.com/watch?v=z5CUTiubzwU&t=793s) Punem această abstractizare la nivel de clasă. Altfel fiecare apel al funcției ar cere din nou argumentele sale de configurație.
7. [00:13:26](https://www.youtube.com/watch?v=z5CUTiubzwU&t=806s) Contrastăm pachetul de configurație existent la nivel de funcție cu un serviciu. [00:13:33](https://www.youtube.com/watch?v=z5CUTiubzwU&t=813s) Serviciul este o abstractizare la nivel de clasă.
8. [00:13:40](https://www.youtube.com/watch?v=z5CUTiubzwU&t=820s) Lăsăm intrările directe ca parametri de metodă și păstrăm configurația stabilă într-un **câmp** al clasei, adică stocare aparținând instanței.

[Extracția serviciului salvată, conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/94f768be6e21ab0eb6f9121df54161d291f040cc/examples/dependencies_simplified/code/Code.cs) implementează politica de dicționar, nu toate regulile ipotetice de prefix/sufix. Formă minimală:

```csharp
public class RemapNameService
{
    private readonly Dictionary<string, string> _map;
    public RemapNameService(Dictionary<string, string> map) => _map = map;
    public string RemapName(string name) => _map.GetValueOrDefault(name, name);
}
```

**Încapsularea** ascunde modul în care serviciul își obține răspunsul: apelanții cer remaparea, în loc să se ocupe singuri de reprezentare. Singura intrare directă rămasă este numele pentru apelul curent.

## Construim un serviciu de remapare a numelor

[00:13:57](https://www.youtube.com/watch?v=z5CUTiubzwU&t=837s) Creăm clasa și o numim după modul în care este folosită. [00:14:01](https://www.youtube.com/watch?v=z5CUTiubzwU&t=841s) Acțiunea sa principală este `RemapName`; [00:14:11](https://www.youtube.com/watch?v=z5CUTiubzwU&t=851s) prin urmare `RemapNameService` îi descrie rolul.

1. [00:14:16](https://www.youtube.com/watch?v=z5CUTiubzwU&t=856s) Stocăm configurația într-un câmp privat. **private** împiedică codul exterior să ajungă în acel membru.
2. [00:14:29](https://www.youtube.com/watch?v=z5CUTiubzwU&t=869s) Un câmp neprivat este posibil în principiu, dar consecințele sale de design sunt amânate intenționat.
3. [00:14:32](https://www.youtube.com/watch?v=z5CUTiubzwU&t=872s) Inițializăm câmpul printr-un **constructor**, operația folosită la crearea unei instanțe.
4. [00:14:39](https://www.youtube.com/watch?v=z5CUTiubzwU&t=879s) Expunem o metodă publică pentru acțiunea numită de serviciu.
5. [00:14:50](https://www.youtube.com/watch?v=z5CUTiubzwU&t=890s) Dăm acelei metode ca parametri doar dependențele sale directe de intrare.
6. [00:14:53](https://www.youtube.com/watch?v=z5CUTiubzwU&t=893s) Citim configurația din câmp; apelantul nu o mai furnizează la fiecare apel.
7. [00:15:00](https://www.youtube.com/watch?v=z5CUTiubzwU&t=900s) Transmitem serviciul însuși mai departe, în locul dicționarului.
8. [00:15:10](https://www.youtube.com/watch?v=z5CUTiubzwU&t=910s) Serviciul poate acum schimba ce date deține, fără a-i forța pe apelanți să urmeze acea schimbare de reprezentare.

[Serviciul de remapare a numelor salvat](https://github.com/AntonC9018/uniCourse_csharp/blob/94f768be6e21ab0eb6f9121df54161d291f040cc/examples/dependencies_simplified/code/Code.cs) are această interfață și utilizare simplificate:

```csharp
public class RemapNameService
{
    private readonly Dictionary<string, string> _nameRemap;
    public RemapNameService(Dictionary<string, string> nameRemap)
        => _nameRemap = nameRemap;

    public string RemapName(string itemName)
        => _nameRemap.GetValueOrDefault(itemName, itemName);
}

var remapper = new RemapNameService(
    new Dictionary<string, string> { ["Anton"] = "Mark" });
string replacement = remapper.RemapName("Anton"); // Mark
```

[00:15:22](https://www.youtube.com/watch?v=z5CUTiubzwU&t=922s) Un serviciu seamănă cu un pachet de configurație cu o operație atașată: transmitem întreaga unitate ca pe o singură dependență. [00:15:23](https://www.youtube.com/watch?v=z5CUTiubzwU&t=923s) Păstrarea acelei operații legate de propria configurație este potrivită, pentru că nimic altceva nu are nevoie de forma internă a acelei configurații.

[00:15:46](https://www.youtube.com/watch?v=z5CUTiubzwU&t=946s) Deocamdată, un `minPriceCutOff` numeric rămas poate sta ca număr simplu. Înfășurarea fiecărei reguli rămase într-un serviciu privat nu este necesară la acest pas.

## Ascundem reprezentările și extragem regula de preț

[00:16:08](https://www.youtube.com/watch?v=z5CUTiubzwU&t=968s) Citim valoarea curentă a numelui înainte de a o transmite operației de remapare. [00:16:26](https://www.youtube.com/watch?v=z5CUTiubzwU&t=986s) Apelantul ar trebui să vadă un nume și o operație de remapare, nu faptul că operația este susținută de un dicționar. [00:16:50](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1010s) Locurile sale de apel nu mai menționează configurația de remapare.

1. [00:16:55](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1015s) Aplicăm același model de serviciu celorlalte reguli.
2. [00:17:09](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1029s) Operația de preț are nevoie de două setări: minimul și maximul.
3. [00:17:12](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1032s) Îi numim metoda booleană `AllowsPrice`, pentru că se citește ca o întrebare. Numele clasei unui serviciu nu trebuie să coincidă cu fiecare nume de metodă.
4. [00:17:34](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1054s) Înlocuim a doua verificare inline de preț cu apelul la serviciu, folosind `item.Price`.
5. [00:17:59](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1079s) Păstrăm ambele limite de preț în interiorul metodei serviciului.
6. [00:18:14](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1094s) Un apelant nu mai trebuie să cunoască acele setări interne; instanța le transportă.
7. [00:18:22](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1102s) Desfacem vechiul pachet unic în abstractizări de servicii separate, pe măsură ce se schimbă codul.
8. [00:18:28](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1108s) Creăm și configurăm explicit serviciul de preț.
9. [00:18:39](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1119s) Înlocuim dependența de configurație brută la limita de procesare cu serviciul de preț și elementul curent.
10. [00:18:48](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1128s) Două servicii și un pachet de configurație partajat rămas pot coexista. Nu toate valorile au nevoie de servicii separate.
11. [00:18:59](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1139s) Alegem un serviciu suplimentar când încapsularea ambelor dependențe și a operației este utilă; [00:19:19](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1159s) apelanții depind atunci doar de acea abstractizare.

[Starea cu servicii salvată](https://github.com/AntonC9018/uniCourse_csharp/blob/94f768be6e21ab0eb6f9121df54161d291f040cc/examples/dependencies_simplified/code/Code.cs) stochează setările numerice de preț în constructorul serviciului de preț, în loc să îi transmită un dicționar de nume. Aceasta este implementarea inspectată după editările de configurare în evoluție. Regulă simplificată:

```csharp
public class PriceCutoffService
{
    private readonly float _min, _max;
    public PriceCutoffService(float min, float max)
    {
        _min = min;
        _max = max;
    }

    public bool AllowsPrice(float price) => price > _min && price < _max;
}

// În operația pe element:
if (!config.priceCutoff.AllowsPrice(item.Price)) return null;
var name = config.nameRemap.RemapName(item.Name).ToUpper();
```

Verificarea salvată exclude prețurile egale cu oricare dintre limite. **readonly** împiedică reatribuirea câmpurilor după construcție. Astfel, dependențele sunt mai ușor de urmărit; nu înseamnă că orice configurație posibilă trebuie să devină un serviciu.

## Transformăm procesorul de elemente într-un serviciu

[00:19:41](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1181s) Întreaga operație pe un singur element merită acum devenită un serviciu, pentru că încă preia configurația direct. O formulare reconciliată numește această operație `PriceSite`; tipul salvat este `ProcessItemService`.

1. [00:19:58](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1198s) Dăm configurația noului serviciu procesor și mutăm vechea operație inline în metoda sa principală.
2. [00:20:16](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1216s) Un underscore pe un nume privat de configurație este o convenție de denumire, nu o cerință de corectitudine.
3. [00:20:22](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1222s) Eliminăm vechiul ajutor care prelua configurația, odată ce operația lui trăiește în serviciu.
4. [00:20:29](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1229s) Transmitem serviciul în locul unui obiect de configurație. [00:20:30](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1230s) Este aceeași schimbare de limită: serviciul își deține contextul.
5. [00:20:38](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1238s) Nu îi mai transmitem configurația la fiecare invocare.
6. [00:20:44](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1244s) Dependența directă trebuie în continuare să ajungă la consumatorul ei direct; `item` rămâne parametru de metodă.
7. [00:20:53](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1253s) Creăm serviciul și îl furnizăm în locul fostului argument de configurație.
8. [00:21:00](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1260s) Alte operații ajutor pot primi același tratament; alternativ, transmitem o funcție apelabilă produsă din acel context.
9. [00:21:11](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1271s) O astfel de extragere are sens doar după ce ajutorul are o singură sarcină coerentă.
10. [00:21:23](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1283s) Urmarea responsabilității unice prin fiecare ajutor rămas ar presupune și mai multe extrageri de servicii; lecția descrie acea continuare, fără a o cere acum în întregime.

[Serviciul procesor salvat](https://github.com/AntonC9018/uniCourse_csharp/blob/5280fbba5831ce2b52a5717595e422580537cd7e/examples/dependencies_simplified/code/Code.cs) conține regulile stabilite. Limită și parcurgere simplificate:

```csharp
public class ProcessItemService
{
    private readonly ProcessItemConfig _config;
    public ProcessItemService(ProcessItemConfig config) => _config = config;

    public Item? Process(Item item)
    {
        if (!_config.priceCutoff.AllowsPrice(item.Price)) return null;
        if (_config.ignoredNames.Contains(item.Name)) return null;
        string name = _config.nameRemap.RemapName(item.Name).ToUpper();
        return new Item { Name = name, Price = item.Price };
    }
}

// În parcurgere:
var newItem = processItemService.Process(item);
if (newItem != null) answer.Add(newItem);
```

**Serviciul** stochează contextul o singură dată; metoda sa acceptă **intrarea directă** pentru fiecare apel. Underscore-ul este doar un ajutor vizual de denumire. Eliminarea cablării redundante a ajutoarelor nu elimină dependența însăși.

## Trecem comportamentul printr-un delegat

[00:21:33](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1293s) C# poate transmite contextul unei operații printr-un **delegat**, o valoare apelabilă cu semnătură declarată. [00:21:37](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1297s) și [00:21:45](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1305s) Comparați furnizarea unei lambda cu furnizarea unui serviciu. O **lambda** este sintaxă pentru o funcție anonimă; un delegat este valoarea apelabilă tipizată căreia unei asemenea funcții i se poate atribui.

1. [00:21:55](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1315s) Declarăm un delegat dând prototipul funcției: ce primește și ce returnează.
2. [00:22:01](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1321s) Numim elementul apelabil pe un singur element `ProcessItemDelegate`. Întoarcerea sa `Item?` păstrează cazul fără rezultat.
3. [00:22:18](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1338s) Acesta este un contract de comportament, nu o dependență de o anumită clasă concretă de servicii.
4. [00:22:21](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1341s) Acceptăm și invocăm delegatul în locul serviciului.
5. [00:22:31](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1351s) Declarația poate trăi într-un fișier separat.
6. [00:22:41](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1361s) Furnizăm fie un **grup de metode**, adică numele unei metode compatibile, fie o lambda inline.
7. [00:22:52](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1372s) Un ajutor **static** aparține tipului său; în interiorul lambda apelăm `Helper.ProcessItem` cu elementul și configurația.
8. [00:23:02](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1382s) și [00:23:14](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1394s) Astfel se poate obține aceeași operație configurată fără a scrie o clasă care să enumere fiecare dependență și să le primească prin constructor.
9. [00:23:21](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1401s) Funcția anonimă depinde de o variabilă locală din jur.
10. [00:23:35](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1415s) Un serviciu scris manual ar avea nevoie de acea valoare drept parametru de constructor.
11. [00:23:46](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1426s) **Captura** permite lambda să acceseze variabila locală prin stocare generată de compilator.

Niciun commit inspectat nu păstrează întregul experiment tranzitoriu cu delegatul. [Funcția configurată, conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/9968dcccb5b84a1622eb0e2f93b4b2b3de182146/examples/dependencies_simplified/code/Code.cs) și [versiunea cu servicii](https://github.com/AntonC9018/uniCourse_csharp/blob/5280fbba5831ce2b52a5717595e422580537cd7e/examples/dependencies_simplified/code/Code.cs) arată cele două limite adaptate. Echivalent simplificat al delegatului, cu un ajutor compatibil ilustrativ `Transform`:

```csharp
public delegate Item? ProcessItemDelegate(Item item);

var config = new ProcessItemConfig(5, 50,
    new Dictionary<string, string>(), new HashSet<string>());
ProcessItemDelegate transform = item => Transform(item, config);

foreach (var item in items)
{
    var output = transform(item);
    if (output != null) answer.Add(output);
}

// O metodă compatibilă de serviciu poate fi furnizată și ca grup de metode:
// ProcessItemDelegate transform = processor.Process;
```

`Transform` de mai sus este un ajutor compatibil schematic: parametrii săi sunt elementul și configurația, iar întoarcerea este `Item?`. Un delegat este un tip C#, nu o declarație de interfață C# în sens literal; „interfață pentru comportament” îi descrie rolul. Captura evită retransmiterea scrisă manual prin constructor, dar nu garantează că nu are loc nicio alocare de closure.

<details>
<summary>Unde este dependența de configurație a lambda după ce parametrul dispare din consumator?</summary>

Ea rămâne în contextul capturat al elementului apelabil. Bucla consumatoare vede semnătura delegatului; lambda are în continuare nevoie de configurația capturată pentru a-și efectua operația.

</details>

## Încercăm o altă implementare de remapare

[00:23:59](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1439s) Comportamentul devine o întrebare de design atunci când mai multe implementări sunt utile. Remaperul curent folosește căutare în dicționar.

1. [00:24:07](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1447s) Considerăm o altă strategie: eliminarea unui prefix când un nume potrivit nu are mapare directă, în loc de a ne baza doar pe intrările dicționarului.
2. [00:24:17](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1457s) Scriem o verificare a prefixului și întoarcem sufixul de după prefix când acesta se potrivește.
3. [00:24:28](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1468s) Folosim `StartsWith` și o felie de șir; altfel întoarcem numele neschimbat. Numim clasa după acest comportament.
4. [00:24:44](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1484s) Procesorul ar trebui să poată primi o dependență diferită de acea unică clasă concretă de dicționar.
5. [00:25:01](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1501s) Furnizăm implementarea candidată care elimină prefixul.
6. [00:25:15](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1515s) Înlocuirea directă nu compilează.
7. [00:25:25](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1525s) Cele două clase sunt tipuri incompatibile, deși operația lor utilă are aceeași formă.
8. [00:25:32](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1532s) Ambele expun operația pe care o dorește consumatorul. [00:25:45](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1545s) El folosește doar `RemapName`, nu toate detaliile vreuneia dintre clase.
9. [00:25:49](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1549s) În această etapă intermediară dependența este încă legată de remaperul concret, deci alternativele nu pot fi încă interschimbate.

[Versiunea polimorfă salvată, conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/7001d912d07b50d6516b1206d5b36bd03c54d43b/examples/dependencies_simplified/code/Code.cs) conține implementarea cu prefix după corectarea interfeței. Nu stabilește că atribuirea incompatibilă anterioară a compilat. Serviciul ei de prefix este o politică separată, fără o căutare inițială în dicționar. Operație simplificată:

```csharp
public string RemapName(string itemName)
{
    if (itemName.StartsWith(_removedPrefix))
        return itemName[_removedPrefix.Length..];
    return itemName;
}
```

O **felie** selectează o parte dintr-un șir; aceasta începe la lungimea prefixului și continuă până la final. O valoare de rezervă neschimbată păstrează numele fără acel prefix. Doar identitatea scrierii numelui metodei nu face clase C# fără legătură atribuibile unul la tipul concret al celuilalt.

## Folosim o interfață pentru Strategy

[00:26:43](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1603s) Eliminăm dependența puternică prin **polimorfism**, permițând ca un apel să ajungă la o implementare selectată. [00:26:57](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1617s) și [00:26:58](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1618s) Consumatorul nu trebuie să cunoască acțiunea internă, dacă operația furnizată respectă contractul său. [00:27:04](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1624s) Susținerea alternativelor viitoare este exact motivul.

[00:27:22](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1642s) Aici o **interfață**, o declarație a operațiilor necesare, permite acel polimorfism. O reuniune etichetată va fi cealaltă opțiune.

1. [00:27:47](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1667s) Declarăm interfața cu semnătura operației.
2. [00:28:05](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1685s) Lăsăm deoparte corpul metodei; implementăm interfața în fiecare clasă concretă permisă.
3. [00:28:18](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1698s) Schimbăm tipul dependenței consumatorului de la remaperul concret la interfață.
4. [00:28:28](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1708s) Păstrăm vechea implementare sau furnizăm cea nouă. Dispatch-ul de la runtime ajunge la metoda obiectului efectiv furnizat.
5. [00:28:45](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1725s) Acesta este **șablonul Strategy**: alegerea comportamentului prin transmiterea sau stocarea unei implementări.
6. [00:29:09](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1749s) Strategia furnizată dictează ce acțiune se execută.

[Starea Strategy salvată](https://github.com/AntonC9018/uniCourse_csharp/blob/7001d912d07b50d6516b1206d5b36bd03c54d43b/examples/dependencies_simplified/code/Code.cs) oferă consumatorului acest contract. Declarație și apel simplificate:

```csharp
public interface IRemapNameService
{
    string RemapName(string itemName);
}

// Fiecare clasă concretă declară: : IRemapNameService
IRemapNameService strategy = new RemapNameService_RemovePrefix("A");
string output = strategy.RemapName("Anton"); // nton
```

O **implementare** furnizează corpul promis de interfață. Declarația interfeței conține doar prototipul. Consumatorul se bazează pe acea operație, deci înlocuirea implementării nu trebuie să schimbe logica proprie a consumatorului. Strategy funcționează indiferent dacă implementarea sosește ca argument de metodă sau este stocată într-un câmp.

## Alegem o ierarhie deschisă sau închisă

[00:29:24](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1764s) Versiunea bazată pe interfață are un contract plus implementările sale. [00:29:25](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1765s) Un consumator poate folosi o clasă predefinită sau poate crea una complet nouă.

1. [00:29:39](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1779s) O implementare scrisă de un utilizator poate face orice, chiar poate întoarce intrarea neschimbată.
2. [00:30:03](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1803s) Consumatorul inițial o apelează în continuare prin aceeași interfață, deși autorul inițial nu a știut niciodată de clasa nouă.
3. [00:30:23](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1823s) Unele proiecte interzic această extindere. [00:30:42](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1842s) Această ierarhie este **deschisă** tocmai pentru că consumatorii pot adăuga implementări.
4. [00:30:48](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1848s) O **ierarhie închisă** enumeră în schimb fiecare alternativă permisă.
5. [00:30:56](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1856s) Odată reprezentate explicit acele alternative, această versiune poate renunța la interfață.

[Starea cu interfață deschisă](https://github.com/AntonC9018/uniCourse_csharp/blob/7001d912d07b50d6516b1206d5b36bd03c54d43b/examples/dependencies_simplified/code/Code.cs) permite această strategie externă simplificată:

```csharp
public class IdentityRemapper : IRemapNameService
{
    public string RemapName(string itemName) => itemName;
}
IRemapNameService choice = new IdentityRemapper();
```

Acea clasă este o implementare nouă ilustrativă, nu una salvată de lecție. Un contract deschis permite implementări proprii, dar nu poate garanta că politica lor corespunde remapării intenționate de apelant. Reprezentarea următoare restrânge tipurile posibile de implementare.

## Construim și dirijăm o reuniune etichetată

[00:31:03](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1863s) Creăm reuniunea, numită `RemapNameStringUnion` în descrierea reconciliată și `RemapNameService_TaggedUnion` în codul salvat. O **reuniune etichetată** reprezintă exact una dintre alternativele unei mulțimi fixe de **variante**, adică alternativele suportate.

1. [00:31:11](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1871s) Îi dăm un constructor pentru fiecare variantă și stocare pentru implementarea selectată. O proiectare primitivă ar putea avea câmpuri separate; versiunea salvată folosește un singur câmp de tip `object`.
2. [00:31:21](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1881s) Adăugăm **dispatch**, care direcționează apelul către varianta efectiv stocată.
3. [00:31:38](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1898s) Tratăm prima variantă de remapare.
4. [00:31:57](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1917s) Tratăm a doua.
5. [00:32:05](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1925s) Tratăm ramura rămasă ca stare imposibilă, presupunând că construirea a furnizat una dintre variantele suportate, diferite de null.
6. [00:32:22](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1942s) Considerăm înlocuirea dispatch-ului tipizat cu **dynamic**, selectarea membrului amânată până la runtime. Această alternativă tranzitorie lasă consumatorul din jur neschimbat.
7. [00:32:33](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1953s) Reuniunea transportă și retransmite alternativele; nu definește o politică suplimentară de remapare.

[Implementarea reuniunii etichetate salvată](https://github.com/AntonC9018/uniCourse_csharp/blob/9cabfb706383195f1b3b3dade007d2272c62f899/examples/dependencies_simplified/code/Code.cs) folosește un `struct` și tipare de tip. Încapsularea anterioară într-o clasă și editarea cu `dynamic` sunt stări din video, nu declarații salvate exact. Dispatch salvat simplificat:

```csharp
public readonly struct RemapNameService_TaggedUnion
{
    private readonly object _impl;
    public RemapNameService_TaggedUnion(RemapNameService mapper)
        => _impl = mapper;
    public RemapNameService_TaggedUnion(RemapNameService_RemovePrefix prefix)
        => _impl = prefix;

    public string RemapName(string name)
    {
        if (_impl is RemapNameService a) return a.RemapName(name);
        if (_impl is RemapNameService_RemovePrefix b) return b.RemapName(name);
        throw new InvalidOperationException("Invalid union state");
    }
}

// Alternativă tranzitorie din înregistrare, nu implementarea salvată:
// return ((dynamic)_impl).RemapName(name);
```

Un **tipar de tip**, precum `is RemapNameService a`, verifică tipul și introduce o variabilă cu acel tip. Tipul de la runtime acționează aici ca etichetă; nu există o etichetă întreagă explicită. Exemplul aruncă o excepție în ramura de rezervă; codul salvat apelează `Debug.Fail` și întoarce null. Niciuna dintre ramurile de rezervă nu creează o variantă nouă validă.

Argumentul stării imposibile este valabil doar pentru o construire corectă și populată. Argumentele null și, odată ce acesta este un `struct`, valoarea sa implicită pot lăsa nicio variantă utilizabilă. Lecția nu adaugă validări complete pentru aceste cazuri.

## Comparăm alternativele brute ale dependenței

[00:32:45](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1965s) Cu **funcții pure**, care își calculează răspunsul din intrări fără efecte secundare externe, contextul ar putea fi furnizat direct, în loc de a fi împachetat în obiecte de serviciu.

1. [00:32:55](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1975s) Comparați cu câmpuri sau parametri opționali separați, fiecare verificat pentru prezență. [00:33:13](https://www.youtube.com/watch?v=z5CUTiubzwU&t=1993s) Aceasta este și o formă frecventă în C#.
2. [00:33:22](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2002s) Ambele stiluri pot susține procesorul din jur.
3. [00:33:31](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2011s) Reuniunea reprezintă o singură variantă de dependență aleasă, niciodată mai multe simultan.
4. [00:33:36](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2016s) Recuperăm dependența de prefix cu o **conversie**, adică o conversie explicită înapoi la tipul cunoscut stocat la construire.
5. [00:33:55](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2035s) Repetăm conversia și dispatch-ul pentru varianta cu dicționar.
6. [00:33:58](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2038s) Fiecare variantă are nevoie de conversii asemănătoare, principalul dezavantaj al acestei forme primitive.
7. [00:34:11](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2051s) O reprezentare care presupune o singură dependență stocată nu acomodează în mod natural o implementare care are nevoie de mai multe valori de dependență.
8. [00:34:18](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2058s) Un tip trebuie repetat la construire, la detectare și la conversie, ceea ce face schimbările fragile.
9. [00:34:24](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2064s) O altă clasă dependentă ar putea elimina o parte din repetare, dar lecția vede puțin beneficiu și lasă acea extindere deoparte.
10. [00:34:31](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2071s) Codul scris manual trebuie să detecteze ce tip a fost stocat. [00:34:40](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2080s) Această povară aparține reprezentării sale manuale de reuniune, nu decurge automat din nefolosirea interfețelor de serviciu.

Acestea sunt experimente tranzitorii cu context brut. [Reuniunea de servicii salvată, conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/9cabfb706383195f1b3b3dade007d2272c62f899/examples/dependencies_simplified/code/Code.cs) arată alternativele fixe după ce contextul lor a fost încapsulat. Forme brute simplificate lângă acea referință:

```csharp
// Separate optional arguments: the order also defines priority if both exist.
static string Remap(string name, Dictionary<string, string>? map, string? prefix)
{
    if (map != null) return map.GetValueOrDefault(name, name);
    if (prefix != null && name.StartsWith(prefix)) return name[prefix.Length..];
    return name;
}

// Schematic stored-dependency dispatch:
if (storedDependency is string)
{
    var prefix = (string)storedDependency;
    return name.StartsWith(prefix) ? name[prefix.Length..] : name;
}
if (storedDependency is Dictionary<string, string>)
{
    var map = (Dictionary<string, string>)storedDependency;
    return map.GetValueOrDefault(name, name);
}
```

Acestea sunt fragmente alternative, nu o singură funcție combinată. Cu argumente opționale separate, „ambele prezente” și „niciuna prezentă” au nevoie de semantică definită. O reuniune urmărește să exprime o alegere de tipul unu-din, nu acele combinații suplimentare. Convertirea unei dependențe brute îl face pe consumator să își cunoască din nou reprezentarea.

## Raționăm despre tipuri valoare și alocări

[00:34:47](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2087s) Încapsularea poate deveni un **struct**, adică un tip valoare, în locul unei **clase**, adică un tip referință. O referință indică un obiect; o valoare ocupă stocarea care o conține.

1. [00:34:59](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2099s) Motivația este evitarea unei alocări separate a încapsulării pe **heap**, stocarea gestionată a obiectelor, și păstrarea eventuală a unei valori locale pe **stivă**, stocare locală execuției.
2. [00:35:12](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2112s) Obiectele-dependență de tip referință există totuși pe heap.
3. [00:35:18](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2118s) Convertirea unei referințe existente de serviciu la `object` nu face boxing și nu dublează serviciul; acesta este deja un obiect de tip referință.
4. [00:35:33](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2133s) O dependență de tip valoare stocată ca `object` ar fi **supusă boxing-ului**, copiată într-o reprezentare de obiect gestionat.
5. [00:35:45](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2145s) Considerăm o reuniune tipizată separată pentru dependențele de intrare, în loc de a pune o dependență de tip valoare în `object`.
6. [00:35:51](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2151s) Construim împreună acel context de dependențe, transmițând valoarea număr/numărătoare și un null explicit pentru o dependență nefolosită. Astfel evităm adunarea câmpurilor sale în pași separați.
7. [00:36:09](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2169s) Păstrarea unui context de valoare tipizat poate evita un obiect suplimentar de încapsulare a contextului.
8. [00:36:26](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2186s) Costul principal de design rămâne: reuniunea trebuie să cunoască fiecare implementare posibilă.
9. [00:36:39](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2199s) Cu alternative închise fixe și context la momentul construirii, aceasta poate reduce alocările și îmbunătăți viteza. Acestea sunt motivații, nu rezultate de benchmark măsurate.

[Reuniunea de tip valoare salvată](https://github.com/AntonC9018/uniCourse_csharp/blob/9cabfb706383195f1b3b3dade007d2272c62f899/examples/dependencies_simplified/code/Code.cs) are un câmp `object` care ține o referință la un serviciu deja alocat. Următorul exemplu simplificat separă trei conversii care nu trebuie confundate:

```csharp
var mapper = new RemapNameService(new Dictionary<string, string>());
object reference = mapper; // Reference conversion: no boxing of the service.

object boxedNumber = 3; // Boxing a value-type dependency.

var choice = new RemapNameService_TaggedUnion(mapper);
object boxedChoice = choice; // Boxing the struct wrapper itself.
```

Explicația „fără boxing” privește dependența stocată de tip referință, nu conversia reuniunii-`struct` însăși la `object`. Un `struct` nu garantează stocarea pe stivă: un câmp `struct` într-un obiect de pe heap este stocat acolo, iar boxing-ul creează un obiect pe heap.

Constructorul de context de intrare este o schiță tranzitorie, nu un commit exact verificat. Aceeași [stare de reuniune, conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/9cabfb706383195f1b3b3dade007d2272c62f899/examples/dependencies_simplified/code/Code.cs) furnizează context pentru această reconstrucție schematică la [00:35:51](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2151s):

```csharp
// Illustrative typed context, not a verbatim video declaration:
public readonly record struct InputDependencies(
    int Count, Dictionary<string, string>? Map, string? Prefix);

var context = new InputDependencies(Count: 2, Map: nameMap, Prefix: null);
```

Referințele stocate indică totuși date existente pe heap. Evitarea unui obiect de încapsulare nu elimină acele obiecte. O reuniune `struct` implicită poate avea un câmp null, deci presupunerile despre construirea validă trebuie ținute separate de toate valorile reprezentabile.

<details>
<summary>Preziceți dacă `object value = choice` face boxing reuniunii când `choice` este un `struct`.</summary>

Da. Reuniunea este un tip valoare, iar conversia la `object` aplică boxing. Referința de serviciu ținută în interiorul acelei valori încasate rămâne o referință către serviciul inițial; acel serviciu nu este din nou supus boxing-ului.

</details>

## Restaurăm serviciile în alegerea închisă

[00:36:58](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2218s) Revenim de la contextul de dependențe brut la obiectele de serviciu. [00:37:05](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2225s) Este mai ușor de citit, pentru că fiecare serviciu își încapsulează propriile dependențe.

1. [00:37:14](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2234s) Păstrăm responsabilitatea unică a reuniunii: enumeră și transportă implementările permise, retransmițându-le operațiile, în loc de a adăuga o politică de remapare.
2. [00:37:27](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2247s) Transmitem o reuniune inițializată cu serviciul de mapare selectat, în locul valorilor de mapare individuale.
3. [00:37:45](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2265s) O clasă personalizată fără legătură a unui utilizator nu poate fi adăugată acelei reuniuni existente: numai variantele declarate în constructori sunt suportate.
4. [00:37:49](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2269s) Comparați cu reuniunea logică la nivel de tip din TypeScript, adică o valoare poate fi un tip sau celălalt.
5. [00:38:04](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2284s) Versiunea de C# folosită în această înregistrare nu are o declarație de reuniune echivalentă încorporată.
6. [00:38:14](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2294s) Folosim o reprezentare `struct`, în loc de a aloca altă încapsulare de clasă în jurul unor servicii deja alocate.

[Versiunea salvată cu alegerea închisă](https://github.com/AntonC9018/uniCourse_csharp/blob/9cabfb706383195f1b3b3dade007d2272c62f899/examples/dependencies_simplified/code/Code.cs) și [versiunea ulterioară cu interfața restaurată](https://github.com/AntonC9018/uniCourse_csharp/blob/9acccee8e1a31518a29a0afc6e52a06bfa2e6d1c/examples/dependencies_simplified/code/Code.cs) disting cele două reprezentări. Utilizare simplificată și comparația TypeScript din înregistrare:

```csharp
var mapper = new RemapNameService(
    new Dictionary<string, string> { ["Anton"] = "Mark" });
var choice = new RemapNameService_TaggedUnion(mapper);
string name = choice.RemapName("Anton");
```

```typescript
// Type-level comparison from the recording:
type NameMapper = DictionaryMapper | PrefixMapper;
```

Linia TypeScript ilustrează relația de tipul unu-din; nu este cod salvat în commitul C#. Comparația dintre limbaje descrie sfera înregistrării, nu o afirmație despre viitoarele versiuni de C#. În commitul cu interfața restaurată, procesorul depinde din nou de `IRemapNameService`, în timp ce tipul reuniune rămâne în fișier.

## Separăm controlul injecției și inversarea

[00:38:26](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2306s) Trecem la injecția automată a dependențelor, începând cu ce înseamnă însăși injecția.

1. [00:38:44](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2324s) Simplificăm procesorul de elemente: eliminăm stratul său combinat de configurație și dăm fiecărei dependențe propriul câmp și propriul argument de constructor.
2. [00:39:03](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2343s) **Injecția dependențelor** furnizează dependențele unui obiect la crearea lui, cel mai adesea prin constructor; și alte metode de livrare sunt posibile.
3. [00:39:19](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2359s) **Inversarea controlului** înseamnă că serviciul nu își construiește intern propriile dependențe.
4. [00:39:27](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2367s) Remaperul serviciului, sau un repository/client ipotetic, sosește din exterior.
5. [00:39:39](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2379s) Codul care folosește serviciul decide ce configurație și ce dependențe furnizează.
6. [00:39:51](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2391s) Injecția este actul de a le transmite, împreună cu acel control exterior asupra creării lor.
7. [00:40:04](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2404s) **Inversarea dependențelor** privește în schimb tipul declarat al dependenței: folosim o abstractizare, aici o interfață, în locul unei singure implementări concrete.
8. [00:40:15](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2415s) Un serviciu de nivel înalt legat de o clasă concretă de nivel inferior depinde de acea alegere de implementare.
9. [00:40:33](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2433s) Declarăm dependența față de contractul de nivel înalt; consumatorul nu trebuie să știe cum funcționează implementarea de nivel inferior.
10. [00:40:45](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2445s) Implementările de nivel inferior declară că implementează contractul și îi furnizează operațiile.

[Starea salvată cu injecție prin constructor](https://github.com/AntonC9018/uniCourse_csharp/blob/53502e1527899ae9acf128bbbf3b0388948ec39a/examples/dependencies_simplified/code/Code.cs) are această formă simplificată:

```csharp
public class ProcessItemService
{
    private readonly PriceCutoffService _priceCutoff;
    private readonly IRemapNameService _nameRemap;
    private readonly HashSet<string> _ignoredNames;

    public ProcessItemService(
        PriceCutoffService priceCutoff,
        IRemapNameService nameRemap,
        HashSet<string> ignoredNames)
    {
        _priceCutoff = priceCutoff;
        _nameRemap = nameRemap;
        _ignoredNames = ignoredNames;
    }

    // Process(Item item) uses those fields.
}
```

Aceasta demonstrează injecția chiar și fără un container. Folosește și inversarea dependențelor pentru dependența de remapare a numelui, pentru că aceasta este declarată ca interfață. Serviciul de preț rămâne concret; doar injecția prin constructor nu inversează automat fiecare dependență.

<details>
<summary>Ce principiu se schimbă când înlocuiți un parametru de constructor concret cu o interfață?</summary>

Se schimbă contractul dependenței: inversarea dependențelor. Furnizarea valorii sale din exterior este injecția dependențelor; lăsarea construirii și selecției în seama codului exterior este inversarea controlului. Acestea sunt decizii înrudite, dar distincte.

</details>

## Configurăm o colecție, apoi construim un container

[00:40:59](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2459s) **Container IoC**, **container DI** și **container Dependency Injection** se referă aici la același mecanism de asamblare. [00:41:17](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2477s) Crearea manuală presupune construirea fiecărei dependențe și alimentarea constructorilor în ordinea corectă.

[00:41:37](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2497s) Dăm containerului înregistrările sale o singură dată, apoi îl lăsăm să asambleze serviciile și dependențele lor înregistrate. Descrierea introductivă despre „crearea dependențelor lipsă” înseamnă crearea implementărilor înregistrate necesare, nu rezolvarea unor tipuri neînregistrate arbitrare și nici inventarea unor setări primitive.

1. [00:41:58](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2518s) C#/.NET oferă o bibliotecă pentru asta; începem aducând acea bibliotecă în proiect.
2. [00:42:02](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2522s) Adăugăm `Microsoft.Extensions.DependencyInjection`. [00:42:03](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2523s) Referința sa de pachet aparține unui `ItemGroup` de proiect.
3. [00:42:08](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2528s) Mai întâi configurăm, apoi construim: lista de definiții devine un furnizor utilizabil.
4. [00:42:17](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2537s) Terminem înregistrările înainte de construire, pentru că lista este o configurație mutabilă.
5. [00:42:27](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2547s) Comparați cu un **builder**, care transformă un model mutabil într-un obiect finit utilizabil. [00:42:31](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2551s) Colecția este modelul mutabil; containerul este rezultatul.
6. [00:43:13](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2593s) Modelul finit furnizează servicii și dependențele lor; [00:43:15](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2595s) reiteră tocmai acest scop.
7. [00:43:18](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2598s) Creăm o **colecție de servicii**, lista mutabilă de înregistrări.
8. [00:43:22](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2602s) Implementarea ei este o listă de descriptori de servicii, astfel încât definițiile pot fi adăugate.
9. [00:43:31](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2611s) Un **descriptor de serviciu** înregistrează cum va fi furnizat un tip de serviciu, inclusiv o fabrică de creare în înregistrarea bazată pe fabrică demonstrată.
10. [00:43:41](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2621s) O **fabrică**, o funcție care creează o instanță, produce un obiect concret real, nu o declarație de interfață.
11. [00:43:43](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2623s) Descriptorul înregistrează și tipul de serviciu/interfață cerut.
12. [00:43:44](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2624s) Împreună, acestea spun ce implementare furnizează un tip cerut.
13. [00:43:51](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2631s) Adăugăm înregistrări cât timp colecția este mutabilă, înainte de construirea furnizorului.

[Adăugarea pachetului DI salvată](https://github.com/AntonC9018/uniCourse_csharp/blob/53502e1527899ae9acf128bbbf3b0388948ec39a/examples/dependencies_simplified/tests/tests.csproj) stabilește schimbarea de instalare; nu este un commit de adăugare a exercițiului. Referință de proiect simplificată:

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.Extensions.DependencyInjection"
                    Version="9.0.11" />
</ItemGroup>
```

Versiunea de mai sus este cea a proiectului salvat, nu o cerință de a reproduce fiecare experiment al lecției cu exact acea versiune. [Codul final de înregistrare](https://github.com/AntonC9018/uniCourse_csharp/blob/d80428c0f252c626af6927783c16b777dc7f1425/examples/dependencies_simplified/tests/Tests.cs) ilustrează limita dintre colecție și furnizor. Formă minimală:

```csharp
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
// Add service definitions here.
var serviceProvider = services.BuildServiceProvider();
```

**Furnizorul de servicii** este obiectul de rezolvare construit. Construirea preia configurația de înregistrări pentru acel furnizor; modificarea ulterioară a colecției nu actualizează un furnizor deja construit. „Finit” nu înseamnă că obiectele de servicii rezolvate trebuie să fie ele însele imutabile. Descriptorii pot reprezenta un tip de implementare, o fabrică sau o instanță; lecția descrie inițial forma cu fabrică.

## Înregistrăm fabrici și instanțe

[00:44:39](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2679s) Folosim **metode de extensie**, operații de înregistrare apelabile pe colecție, precum `AddSingleton`. O înregistrare **singleton** reutilizează o instanță în interiorul unui furnizor.

[00:44:41](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2681s) `AddScoped` este menționat pentru studiere ulterioară, după cum este necesar. O **durată de viață scoped** reutilizează o instanță în interiorul unui **scope**, adică unui context de execuție; proiectarea detaliată a duratelor de viață nu este predată aici.

1. [00:44:43](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2683s) Înregistrăm tipul concret de serviciu, dacă acel serviciu nu are o interfață separată.
2. [00:44:51](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2691s) Eliminăm cablarea redundantă de la locul de creare, pe măsură ce obiectul devine o dependență furnizată de container.
3. [00:44:53](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2693s) **Argumentul de tip generic**, tipul dintre paranteze ascuțite, trebuie să corespundă tipului concret cerut de consumatori.
4. [00:44:59](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2699s) Facem ca delegatul de înregistrare să inițializeze și să întoarcă acel serviciu.
5. [00:45:09](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2709s) O lambda cu corp de expresie întoarce valoarea expresiei sale; poate înlocui un bloc cu un `return` explicit.
6. [00:45:15](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2715s) Sintaxa mai scurtă compilează echivalent.
7. [00:45:52](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2752s) și [00:46:07](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2767s) Ignorăm argumentul de furnizor al fabricii când nu este necesar.
8. [00:46:12](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2772s) Furnizăm funcția containerului pentru crearea ulterioară, dacă serviciul este cerut.
9. [00:46:24](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2784s) Alternativ, creeăm serviciul imediat și înregistrăm acea instanță.

[Înregistrarea finală din teste](https://github.com/AntonC9018/uniCourse_csharp/blob/d80428c0f252c626af6927783c16b777dc7f1425/examples/dependencies_simplified/tests/Tests.cs) și [forma anterioară a constructorului, conexă](https://github.com/AntonC9018/uniCourse_csharp/blob/53502e1527899ae9acf128bbbf3b0388948ec39a/examples/dependencies_simplified/code/Code.cs) susțin comparația; exemplul folosește constructorul anterior cu două numere. Alternative simplificate:

```csharp
services.AddSingleton<PriceCutoffService>(sp =>
{
    _ = sp;
    return new PriceCutoffService(5, 50);
});

// Equivalent alternative, rather than a second registration:
services.AddSingleton<PriceCutoffService>(
    _ => new PriceCutoffService(5, 50));

// Eager alternative:
services.AddSingleton(new PriceCutoffService(5, 50));
```

Constructorul final în stil Options primește în schimb `PriceCutoffConfig`. Acestea sunt alegeri distincte, nu instrucțiuni de a înregistra același serviciu de trei ori. Într-o lambda cu corp de bloc, un `return` este în continuare necesar pentru o valoare returnată. Este trecerea la sintaxa cu corp de expresie, nu doar inferența tipurilor, care elimină `return`-ul scris. Underscore-ul este un nume de parametru nefolosit în această lambda cu un singur parametru; `_ = sp` ignoră explicit valoarea.

1. [00:46:39](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2799s) `AddSingleton<Interface, Implementation>()` înregistrează o implementare sub o cheie de interfață.
2. [00:47:04](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2824s) Constructorii care cer acea interfață primesc implementarea ei înregistrată.
3. [00:47:10](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2830s) Obiectul întoars de fabrică trebuie să fie compatibil cu tipul cerut respectiv.
4. [00:47:21](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2841s) O lambda este opțională: folosim un delegat numit compatibil sau un obiect deja creat.
5. [00:47:42](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2862s) Un remaper cu dicționar are nevoie doar de dicționarul său, deci poate fi creat înainte de înregistrare.
6. [00:48:03](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2883s) Înregistrăm instanța remaperului și trecem la dependența de tip colecție.

Același [test final](https://github.com/AntonC9018/uniCourse_csharp/blob/d80428c0f252c626af6927783c16b777dc7f1425/examples/dependencies_simplified/tests/Tests.cs) înregistrează remaperul prin fabrică; forma cu instanța creată imediat este un echivalent simplificat demonstrat în timpul înregistrării:

```csharp
// Type-based registration requires resolvable constructor dependencies:
services.AddSingleton<IRemapNameService, RemapNameService>();

// Eager alternative, when the dictionary is supplied here:
var mapper = new RemapNameService(
    new Dictionary<string, string> { ["Anton"] = "Mark" });
services.AddSingleton<IRemapNameService>(mapper);

// A separate dependency if ignored names should be provider-resolved:
services.AddSingleton(new HashSet<string> { "ignored" });
```

Din nou, alegeți o singură formă de înregistrare pentru remaper. Înregistrați explicit cheia de interfață atunci când aceasta este cea cerută de consumator; înregistrarea unei clase concrete doar sub tipul ei nu stabilește maparea interfeței.

Colecția de după remaper este `HashSet<string>`-ul cu numele ignorate din constructorul salvat al consumatorului, nu un `IServiceCollection` injectat în procesor. Formularea din transcriere este ambiguă; [constructorul salvat](https://github.com/AntonC9018/uniCourse_csharp/blob/53502e1527899ae9acf128bbbf3b0388948ec39a/examples/dependencies_simplified/code/Code.cs) identifică dependența reală, ca în exemplul de înregistrare a mulțimii de mai sus.

## Construim furnizorul și creăm ținta

[00:48:10](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2890s) Odată configurate dependențele sale, fie înregistrăm și consumatorul, fie îl construim dinamic. Înregistrarea demonstrează mai întâi a doua variantă.

1. [00:48:34](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2914s) Adunăm toate înregistrările de dependențe necesare țintei.
2. [00:48:51](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2931s) Apelăm `BuildServiceProvider()` pentru a crea containerul.
3. [00:49:01](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2941s) Rezultatul furnizează `IServiceProvider`, numit convențional `serviceProvider`. Este același tip de furnizor transmis în metodele de fabrică.
4. [00:49:15](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2955s) Un serviciu înregistrat poate fi rezolvat direct din el.
5. [00:49:23](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2963s) Folosim `ActivatorUtilities.CreateInstance` cu tipul de țintă dorit pentru creare dinamică.
6. [00:49:40](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2980s) și [00:49:42](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2982s) `GetRequiredService` ar eșua pentru acea țintă în această etapă, pentru că ținta însăși nu este înregistrată.
7. [00:49:49](https://www.youtube.com/watch?v=z5CUTiubzwU&t=2989s) `CreateInstance` construiește o țintă neînregistrată, obținând în schimb dependențele sale de constructor din furnizor.
8. [00:50:08](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3008s) Transmitem furnizorul plus orice argumente de constructor care nu sunt disponibile din înregistrări.
9. [00:50:32](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3032s) Furnizăm inline colecția rămasă cu numele ignorate.
10. [00:50:42](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3042s) Transmiterea unei colecții existente funcționează și ea; alegeți implementarea de remapare compatibilă cu numele așteptate stabilite.

[Constructorul salvat, conex](https://github.com/AntonC9018/uniCourse_csharp/blob/53502e1527899ae9acf128bbbf3b0388948ec39a/examples/dependencies_simplified/code/Code.cs) și [cabvarea finală din teste](https://github.com/AntonC9018/uniCourse_csharp/blob/d80428c0f252c626af6927783c16b777dc7f1425/examples/dependencies_simplified/tests/Tests.cs) stabilesc argumentele necesare. Testul final înregistrează și ținta; nu este editarea exactă anterioară cu ținta neînregistrată. Formă anterioară simplificată, după înregistrarea serviciilor de preț și de remapare:

```csharp
using var serviceProvider = services.BuildServiceProvider();

var processor = ActivatorUtilities.CreateInstance<ProcessItemService>(
    serviceProvider, new HashSet<string> { "ignored" });
var answer = Helper.ProcessItems(items, processor);
await Verify(answer);
```

Colecția rămasă este date de apartenență pentru numele ignorate, adică un `HashSet<string>`, așa cum identifică constructorul salvat. Nu este un dicționar de servicii, în ciuda acelei etichete dintr-o descriere reconciliată. `CreateInstance` nu poate inventa setări lipsă arbitrare; furnizați explicit ceea ce nu poate fi rezolvat din furnizor.

[00:50:56](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3056s) Păstrăm corectura: **testul nu s-a stricat în realitate**. Înregistrarea dependențelor a fost suficientă, iar transmiterea manuală a dependențelor deja disponibile din furnizor a fost inutilă. Trecerea între remaparea prin dicționar și eliminarea prefixului este o alegere de comportament separată și poate schimba numele așteptate.

Același [constructor final al consumatorului](https://github.com/AntonC9018/uniCourse_csharp/blob/d80428c0f252c626af6927783c16b777dc7f1425/examples/dependencies_simplified/code/Code.cs) permite această alternativă simplificată la [00:50:56](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3056s):

```csharp
// Before building the provider:
services.AddSingleton(new HashSet<string> { "ignored" });

// After building, with every constructor dependency registered:
var processor =
    ActivatorUtilities.CreateInstance<ProcessItemService>(serviceProvider);
```

Aceasta este o configurație alternativă, nu o instrucțiune de a adăuga o înregistrare la un furnizor deja construit. Mai întâi înregistrăm, apoi construim, apoi rezolvăm sau construim.

<details>
<summary>De ce poate `CreateInstance` să construiască o țintă pentru care `GetRequiredService` eșuează?</summary>

`GetRequiredService` caută o cheie de serviciu înregistrată. `CreateInstance` construiește ținta concretă cerută folosind dependențe rezolvate din furnizor plus argumentele rămase explicite. Ținta poate fi neînregistrată; dependențele trebuie totuși să fie disponibile.

</details>

## Lăsăm containerul să asambleze graful

[00:51:07](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3067s) Un **graf de dependențe** descrie ce obiecte au nevoie de care altele. Într-un arbore simplu, un serviciu are nevoie de două dependențe, iar acelea au nevoie doar de configurație; asamblarea automată poate oferi puțin față de apelurile manuale de constructor.

[00:51:18](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3078s) O configurație mai dificilă este A având nevoie de B și C, în timp ce B are nevoie și de C. Ordinea de creare și partajarea devin mai puțin evidente. [00:51:29](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3089s) Containerul gestionează asamblarea fără apeluri de constructor imbricate explicite prin toate obiectele dependente.

[00:51:49](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3109s) Înregistrăm consumatorul prin `AddSingleton` și îl obținem din furnizor. [Testul final salvat](https://github.com/AntonC9018/uniCourse_csharp/blob/d80428c0f252c626af6927783c16b777dc7f1425/examples/dependencies_simplified/tests/Tests.cs) folosește această formă simplificată:

```csharp
services.AddSingleton<ProcessItemService>(sp =>
    ActivatorUtilities.CreateInstance<ProcessItemService>(
        sp, new HashSet<string> { "ignored" }));

using var serviceProvider = services.BuildServiceProvider();
var processor = serviceProvider.GetRequiredService<ProcessItemService>();
var answer = Helper.ProcessItems(items, processor);
```

Ținta rezolvată din furnizor este acum înregistrată, spre deosebire de demonstrația anterioară doar cu `CreateInstance`. Fabrica furnizează în continuare explicit mulțimea neînregistrată de numere ignorate. Asamblarea centrală elimină cablarea repetată, iar consumatorul continuă să declare ce are nevoie în realitate.

## Înregistrăm o configurație în stil Options

[00:52:17](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3137s) Introducem **Options**, grupând setările înrudite ale serviciului într-un tip de configurație cu nume. Acest prim exemplu înregistrează o instanță de configurație obișnuită; API-ul scoped `IOptionsSnapshot` urmează.

1. [00:52:24](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3144s) Creăm o clasă care descrie parametrii serviciului.
2. [00:52:36](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3156s) Mutăm minimul și maximul în ea, stocăm acel obiect în serviciu și actualizăm accesările.
3. [00:52:49](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3169s) Tipul de configurație poate fi el însuși un serviciu înregistrat: construim instanța și o înregistrăm.
4. [00:53:00](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3180s) Înregistrăm configurația și serviciul fără a cabla manual fiecare legătură. Tipul din constructorul serviciului comunică ce configurație are nevoie; implementarea consumatoare decide cum o folosește.
5. [00:53:19](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3199s) Înregistrările pot trăi în altă parte a programului.
6. [00:53:34](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3214s) Verificarea prezentată confirmă că obiectul de configurație a fost păstrat și folosit la construirea serviciului de preț. [00:53:37](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3217s) Efectul observat este folosirea acelui obiect anterior stocat.

[Serviciul de preț în stil Options salvat](https://github.com/AntonC9018/uniCourse_csharp/blob/d80428c0f252c626af6927783c16b777dc7f1425/examples/dependencies_simplified/code/Code.cs) și [înregistrările sale](https://github.com/AntonC9018/uniCourse_csharp/blob/d80428c0f252c626af6927783c16b777dc7f1425/examples/dependencies_simplified/tests/Tests.cs) dau această versiune simplificată:

```csharp
public class PriceCutoffConfig
{
    public required float MinPrice { get; set; }
    public required float MaxPrice { get; set; }
}

public class PriceCutoffService
{
    private readonly PriceCutoffConfig _config;
    public PriceCutoffService(PriceCutoffConfig config) => _config = config;
    public bool AllowsPrice(float price)
        => price > _config.MinPrice && price < _config.MaxPrice;
}

var config = new PriceCutoffConfig { MinPrice = 5, MaxPrice = 50 };
services.AddSingleton(config);
services.AddSingleton<PriceCutoffService>();
```

O **proprietate** expune accesul prin `get` și `set`; acestea sunt accesorii din clasa de configurație. Proprietățile sale `required` trebuie inițializate la creare. **Inițializatorul** furnizează valori între acolade. Câmpul privat `readonly` păstrează aceeași referință de configurație, dar proprietățile configurabile ale configurației pot fi în continuare schimbate.

**IOptions<T>** este un înveliș tipizat prin care un consumator accesează opțiunile. Obiectul de configurație nu devine automat un asemenea înveliș. Acest pas demonstrează că configurația poate participa la injecția prin constructor ca dependență tipizată obișnuită.

## Suprascriem opțiunile într-un scope de pipeline

[00:53:43](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3223s) Aruncați o privire la aplicația exemplu însoțitoare. Aplicația salvată se află în folderul de exemplu `lab5`. Un **pipeline** este o succesiune de pași de procesare; aici citește elemente, le transformă și scrie rezultatul.

1. [00:53:51](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3231s) Creăm pipeline-ul și alegem o configurație pentru fiecare context.
2. [00:54:04](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3244s) Motivăm setările **scoped**: execuții diferite pot folosi valori alese diferite, în locul unei singure configurații fixe pentru întregul program.
3. [00:54:12](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3252s) Folosim **IOptionsSnapshot<T>**, un accesor de opțiuni ale cărui valori sunt asociate unui scope și memorate în cache în el, în loc de a ține direct o valoare globală de configurație.
4. [00:54:27](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3267s) Stabilim din nou configurația pentru un context de invocare nou.
5. [00:54:40](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3280s) Codul exterior poate obține obiectul configurabil și îi poate seta valorile înainte de folosire.
6. [00:55:04](https://www.youtube.com/watch?v=z5CUTiubzwU&t=3304s) Operația vede apoi valoarea nouă în locul valorii implicite inițiale, permițând o suprascriere programatică din mers.

[Aplicația de pipeline salvată](https://github.com/AntonC9018/uniCourse_csharp/blob/d80428c0f252c626af6927783c16b777dc7f1425/examples/lab5_example/Program.cs) creează un nou scope asincron pentru fiecare `Run`. [Builderul salvat](https://github.com/AntonC9018/uniCourse_csharp/blob/7f31c6acd0e759a957c51c40ddb9031b1399d17d/examples/lab5_example/Orchestration/OrchestrationBuilder.cs) rezolvă instantaneul de opțiuni, invocă callbackul de configurare pe valoarea lui și ulterior creează cititorul prin fabrica sa. Versiune simplificată a acelui context partajat:

```csharp
services.AddOptions<RandomReaderFactoryOptions>()
    .Configure(options => options.GeneratedCount = 3);

await using var serviceProvider = services.BuildServiceProvider();
await using var scope = serviceProvider.CreateAsyncScope();

var snapshot = scope.ServiceProvider
    .GetRequiredService<IOptionsSnapshot<RandomReaderFactoryOptions>>();
snapshot.Value.GeneratedCount = 10;

// O fabrică de cititoare care folosește opțiunile acestui scope vede valoarea actualizată.
```

**async** și **await** susțin finalizarea asincronă; `await using` elimină furnizorul și scope-ul prin suportul lor de eliminare asincronă. Un **scope** este un context de execuție, iar **durata de viață scoped** înseamnă reutilizare în interiorul lui. Apelantul builderului alege opțiunile înainte de crearea cititorului, în timp ce consumatorul primește opțiunile prin container.

Doar durata de viață scoped nu reîncarcă și nu modifică configurația. `IOptionsSnapshot` memorează în cache o valoare de opțiuni într-un scope; nu recalculează un obiect nou la fiecare citire în același scope. Demonstrația alege o valoare nouă pentru un context, în loc de a implementa infrastructură de reîncărcare a fișierului de configurare. [Fabricile de citire/scriere salvate, conexe](https://github.com/AntonC9018/uniCourse_csharp/blob/306f4dda13a4582f96ba2d76c3c130728a064df4/examples/lab5_example/ReaderWriter/Factories.cs) separă setările pentru operații diferite. Fabrica de citire aleatoare își citește opțiunile de scope când creează cititorul; extras simplificat:

```csharp
var count = _count.Value.GeneratedCount;
return new RandomReader(count, _generators);
```

Prin urmare, execuțiile independente ale pipeline-ului nu trebuie să împartă o singură alegere globală mutabilă.

<details>
<summary>Instantaneul lui Verify înseamnă același lucru ca IOptionsSnapshot?</summary>

Nu. Verify compară ieșirea reală cu o bază de test aprobată. IOptionsSnapshot furnizează setări de aplicație asociate unui scope. Unul verifică comportamentul; celălalt furnizează configurație pentru execuție.

</details>

Lecția rămâne la un exemplu de dependențe deliberat mic. Amână scenariul realist mai dificil, proiectarea câmpurilor de configurație neprivate, conversia completă a tuturor ajutoarelor în servicii și studiul detaliat al lui `AddScoped`. Experimentează cu o reuniune închisă scrisă manual, dar nu validează exhaustiv stările sale și nu măsoară afirmațiile despre alocări. Introduce opțiuni scoped fără a preda un sistem complet de reîncărcare a configurației și folosește o bibliotecă DI în loc de a construi un container.

Pentru exerciții suplimentare opționale, **nepredate ca sarcină de implementare în video**: [laboratorul de container DI](../../../labs/3_advanced/01_di_container.md) cere un registru, ajutoare de creare, validarea grafului de dependențe și suport opțional pentru durate de viață. Acea asignare este distinctă de stările de cod salvate ale acestei lecții.

## Istoricul modificărilor de cod

Stările imutabile inspectate de mai jos urmează progresia salvată, cea mai recentă la final. Ele identifică repere salvate, nu fiecare tastă. Experimentele cu delegatul și cu contextul de dependențe brut dintre aceste repere sunt legate mai sus ca stări din video.

- [Șablon](https://github.com/AntonC9018/uniCourse_csharp/commit/5253379090ffec2086654d0a6d1957e0dbd81637) — Clase goale de ajutor/test, cu pachetele de testare și Verify pregătite.
- [Video 1](https://github.com/AntonC9018/uniCourse_csharp/commit/2e88085eed097ef2a1e1345951fa37665caf0756) — Procesare inline cu limite furnizate de apelant, mapare și un instantaneu de ieșire aprobat.
- [Dependență de configurație adăugată](https://github.com/AntonC9018/uniCourse_csharp/commit/407acfc8643e7a38c4031696e2eaa858541a46e4) — Procesarea extrasă a unui element nullable, cu setări explicite și retransmiterea numelor ignorate.
- [Struct de configurație](https://github.com/AntonC9018/uniCourse_csharp/commit/9968dcccb5b84a1622eb0e2f93b4b2b3de182146) — Patru setări grupate în `ProcessItemConfig`.
- [Serviciu](https://github.com/AntonC9018/uniCourse_csharp/commit/94f768be6e21ab0eb6f9121df54161d291f040cc) — Remaparea și verificările de preț încapsulate în servicii configurate.
- [Serviciu de procesare a elementului](https://github.com/AntonC9018/uniCourse_csharp/commit/5280fbba5831ce2b52a5717595e422580537cd7e) — Procesorul pe un singur element devine o operație la nivel de clasă, cu context stocat.
- [Polimorfism](https://github.com/AntonC9018/uniCourse_csharp/commit/7001d912d07b50d6516b1206d5b36bd03c54d43b) — `IRemapNameService` permite strategiile cu dicționar și cu eliminarea prefixului.
- [Reuniune etichetată](https://github.com/AntonC9018/uniCourse_csharp/commit/9cabfb706383195f1b3b3dade007d2272c62f899) — O încapsulare `struct` enumeră și face dispatch alternativelor închise de remapare.
- [Interfață restaurată](https://github.com/AntonC9018/uniCourse_csharp/commit/9acccee8e1a31518a29a0afc6e52a06bfa2e6d1c) — Consumatorul folosește din nou interfața deschisă, păstrând exemplul reuniunii.
- [Furnizor de servicii](https://github.com/AntonC9018/uniCourse_csharp/commit/53502e1527899ae9acf128bbbf3b0388948ec39a) — Dependențele de constructor sunt separate și se adaugă `Microsoft.Extensions.DependencyInjection`.
- [Starea finală a lecției](https://github.com/AntonC9018/uniCourse_csharp/commit/d80428c0f252c626af6927783c16b777dc7f1425) — Configurația de preț este înregistrată ca obiect, iar procesorul este creat prin cablarea furnizorului.

Reamintire compactă lângă acea istorie, simplificată mai degrabă decât ca stare exactă a unui singur commit:

```csharp
ProcessItem(item, config);          // Function receives its context.
processor.Process(item);           // Service stores its context.
transform(item);                   // Delegate carries callable behaviour.
provider.GetRequiredService<ProcessItemService>(); // Registered target.
```
