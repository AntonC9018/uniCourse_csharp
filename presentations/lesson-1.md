---
theme: default
title: 'Understanding code through abstractions'
info: 'Software design lessons with C# examples'
layout: cover
class: cover
colorSchema: light
aspectRatio: 16/9
canvasWidth: 1280
fonts:
  sans: Arial
  mono: Consolas
  provider: none
drawings:
  persist: false
transition: none
mdc: false
---

# Understanding code through abstractions

Software design · C#

<!--
Basic C# is assumed. Read the concept, then use the examples to explain it. Sections spanning several slides share their original teaching-time allocation.
-->

---

# Start with the ordinary cases

```csharp
Between(15, 10, 20); // true: clearly inside
Between(5, 10, 20);  // false: below the interval
Between(25, 10, 20); // false: above the interval
```

The operation checks whether a value is between two bounds. Here is the implementation used in these examples:

```csharp
static bool Between(int value, int min, int max)
{
    if (min > max)
        throw new ArgumentException("Bounds are reversed.");

    bool result = value >= min && value <= max;
    return result;
}
```

Both endpoints are included. Reversed bounds cause an exception.

<!--
Teaching notes · 2 minutes

"Fifteen is inside this interval; five and twenty-five are outside. The implementation checks both bounds and combines the answers. It also rejects reversed bounds. We can now see exactly what this version does.

But someone calling the function may only see its name and arguments. Would that tell them which endpoints are included? Keep that question in mind as we look at the next calls."
-->

---

# Make argument roles and endpoint behavior clear

**Interface:** how other code interacts with an operation.
**Implementation:** the code that carries it out.

<div class="grid grid-cols-2 gap-6">

<div>

Unclear roles:

```csharp
Between(10, 10, 20); // Is the lower bound included?
Between(20, 10, 20); // Is the upper bound included?
Between(15, 20, 10); // What about reversed bounds?
```

In `Between(10, 10, 20)`, the repeated numbers do not explain their roles. Named arguments make those roles visible:

```csharp
Between(value: 10, min: 10, max: 20);
```

</div>

<div>

Named constants can also explain what the bounds mean in the application:

```csharp
const int MinimumAllowedTemperature = 10;
const int MaximumAllowedTemperature = 20;
int measuredTemperature = 10;

bool allowed = Between(
    value: measuredTemperature,
    min: MinimumAllowedTemperature,
    max: MaximumAllowedTemperature);
```

</div>

</div>

Argument names explain each parameter's role. Constant names explain why these particular values are used. Neither yet says whether the endpoints are included.

The signature tells us how to call it, but not all of its behavior.

<!--
Teaching notes · 2 minutes

"The first two tens have different jobs: one is the measurement, and one is the lower bound. Named arguments let us see that without looking up the parameter order. The constants add application meaning: these are the permitted temperatures, not arbitrary numbers.

We saw the implementation, so we know that equality is accepted. But the name Between alone leaves that unclear. The next change will communicate it at the call site too.

An interface is how we interact with something. That includes function names and parameters; it does not only mean a C# interface declaration."
-->

---

# State the contract, and make the name help

**Contract:** what callers must satisfy and what they may rely on.

```csharp
BetweenInclusive(value: 10, minInclusive: 10, maxInclusive: 20); // true
BetweenInclusive(value: 20, minInclusive: 10, maxInclusive: 20); // true
BetweenInclusive(value: 25, minInclusive: 10, maxInclusive: 20); // false
```

For this function, the contract is:

- The bounds must be ordered: `minInclusive <= maxInclusive`.
- Both endpoints are included.
- The function returns whether the value is inside that interval.
- It changes no external state.

```csharp
bool BetweenInclusive(int value, int minInclusive, int maxInclusive);
```

<!--
Teaching notes · 3 minutes

"A contract tells us what we must supply and what we can expect back. Here the argument names identify the value and both inclusive bounds. Ten and twenty are included, while twenty-five is outside. The bounds must be ordered; the next slide explains how a function checks that requirement."

For this tiny operation, the contract is almost the same length as the implementation. That makes the contract easy to inspect; it does not demonstrate a large reduction in complexity. Two inline comparisons can be entirely reasonable.

Other contract dimensions belong with examples that need them. We will demonstrate result order with deduplication and required call order with parser setup. Resource ownership is not needed for this integer example.
-->

---

# Validate an input or assert an established assumption

<div class="grid grid-cols-2 gap-6">

<div>

A boundary that promises to reject invalid bounds:

```csharp
if (minInclusive > maxInclusive)
    throw new ArgumentException("Bounds are reversed.");
```

</div>

<div>

A helper whose caller already establishes the condition:

```csharp
Debug.Assert(minInclusive <= maxInclusive);
```

</div>

</div>

A public entry point often validates. An internal helper can assert an assumption already established by its callers.

`Debug.Assert` checks an assumption while debugging; it does not repair the arguments. In a normal Release build, this check is omitted. If the function promises to reject reversed bounds at runtime, keep the `throw` check.

<!--
Teaching notes · 3 minutes

"Suppose min is twenty and max is ten. An assertion can draw our attention to that mistake while debugging. It does not swap the numbers, and in a normal Release build the assertion call is absent.

If our function promises to throw for reversed bounds, we need the explicit validation shown above. If every caller has already established that the bounds are ordered, an internal helper can use an assertion to catch a programming mistake instead of repeating runtime validation.

Being private is not itself a guarantee. We must be able to point to where the arguments were checked or constructed correctly. A short guard can stay right here; extracting it into another function would not necessarily make it clearer."

[Sources]
- https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.debug.assert
-->

---

# A useful abstraction can hide a substantial operation

**Abstraction:** a view or interface that exposes selected properties and operations while allowing us to disregard other details.

```csharp
RemoveDuplicates(new[] { "Ann", "Bob", "Ann" });
// ["Ann", "Bob"]

RemoveDuplicates(new[] { "Ann", "Bob" });
// ["Ann", "Bob"]
```

Contract: retain each distinct name once, in first-occurrence order. Compare names ordinally. Leave the input unchanged.

What does the caller no longer have to implement?

<!--
Teaching notes · 3 minutes

Assume a non-null input containing non-null strings. Explain ordinal comparison through one additional example: ["Ann", "ann"] retains both.

The caller needs the meaning of duplicate removal, not a choice of data structure or a traversal algorithm. We can now show a function whose contract is substantially simpler than its implementation.

A one-use function can still be worthwhile. Reuse is one benefit of abstraction, not its definition.
-->

---

# The caller and implementation work at different levels

**Abstraction level:** how much implementation detail a piece of code works with. A higher-level operation lets its caller work without handling its lower-level steps.

<div class="grid grid-cols-2 gap-6">

<div>

The caller:

```csharp
var uniqueNames = RemoveDuplicates(names);
```

The caller asks for distinct names.

</div>

<div>

One implementation:

```csharp
static IReadOnlyList<string> RemoveDuplicates(
    IReadOnlyList<string> names)
{
    var seen = new HashSet<string>(
        StringComparer.Ordinal);
    var unique = new List<string>();
    foreach (var name in names)
    {
        if (seen.Add(name))
            unique.Add(name);
    }
    IReadOnlyList<string> result = unique;
    return result;
}
```

The implementation manages a set, traversal, and accumulation.

</div>

</div>

<!--
Teaching notes · 4 minutes

Walk through ["Bob", "Ann", "Bob"]. The result is ["Bob", "Ann"]. Sorting first would violate the stated order guarantee.

Explain only the relevant operation of HashSet.Add: it reports whether this is a new member. The set implementation itself hides further details. Abstraction levels are relative.

The read-only input view expresses that this operation reads the collection. Explain the type as a view supporting reading and counting, without introducing iterator implementation or ownership theory here. A new result list is produced.

The final local variable follows the instructor's debugging convention: the result can be inspected before return. A simple one-line method can remain an expression-bodied method.
-->

---

# Changing notation is not the same as hiding implementation

Same value, different notation:

```csharp
int a = 255;
int b = 0xFF;
```

<div class="grid grid-cols-2 gap-6">

<div>

A higher-level operation:

```csharp
var unique = RemoveDuplicates(names);
```

</div>

<div>

A lower-level step inside it:

```csharp
if (seen.Add(name))
    unique.Add(name);
```

</div>

</div>

A useful level lets the caller disregard details. Another name alone may add very little.

<!--
Teaching notes · 2 minutes

There is no universal numbering of abstraction levels. This call is higher-level relative to the set-and-loop implementation. The set is itself an abstraction over storage and lookup mechanisms.

BetweenInclusive may express an intention slightly differently from two comparisons, but its implementation is already simple. Do not force the same magnitude of benefit onto every function.

Compilation is another familiar connection: source-language operations are implemented through lower-level instructions. A translation between representations does not necessarily go up a level.

[Sources]
- https://www.nationalacademies.org/read/11106/chapter/6
-->

---

# Names can express meaning in the system

**Semantic meaning** is what a value or operation means in the system: what it represents, measures, or tells us.

<div class="grid grid-cols-2 gap-6">

<div>

Vague names:

```csharp
int right = 18;
bool indicator = true;
```

</div>

<div>

Names with application meaning:

```csharp
int rightSensorTemperature = 18;
bool temperatureWithinAllowedInterval = true;
```

</div>

</div>

What does `right` measure? What does the indicator tell us?

`Right` can be a meaningful physical position in a collection of sensor readings. In another context, it could just be a vague label. `TemperatureWithinAllowedInterval` states an application fact. Both are stored using ordinary C# types, but their names communicate different knowledge.

A name communicates intent; it does not enforce that intent. A field named `temperatureWithinAllowedInterval` must still be computed correctly.

<!--
Teaching notes · 2 minutes

"An int tells us what kind of value we can store. It does not tell us whether eighteen is a temperature, a count, or a position. The name supplies some of that meaning.

Right is not automatically a bad name. In a Readings object with Left, Center, and Right sensors, the physical positions may be perfectly clear. Outside that context, rightSensorTemperature tells us more.

The boolean names make a different distinction. Indicator tells us very little about what true means. TemperatureWithinAllowedInterval states the fact being reported. That name knows about our application's temperature rule. We should put the code that establishes that fact where that rule belongs.

Giving something a more descriptive name does not by itself create a higher abstraction level. It helps us understand what the existing value means."
-->

---

# Normalize the interval's representation

**Normalization:** convert equivalent inputs to one consistent representation.

For integer intervals, store an inclusive start and an exclusive end. Boundary choices affect construction only.

<div class="grid grid-cols-2 gap-6">

<div>

```csharp
var interval = Interval.Create(
    min: 10, max: 20,
    minBoundary: Boundary.Inclusive,
    maxBoundary: Boundary.Exclusive);
// Stored as [10, 20).

interval.Contains(15); // true
interval.Contains(25); // false
interval.Contains(10); // true
interval.Contains(20); // false
```

</div>

<div>

These inputs all represent the same integers and store the same bounds:

| Input interval | Normalized storage |
| --- | --- |
| [10, 20) | [10, 20) |
| [10, 19] | [10, 20) |
| (9, 20) | [10, 20) |
| (9, 19] | [10, 20) |

</div>

</div>

<!--
Teaching notes · 4 minutes

"Inclusive means the endpoint is allowed. Exclusive means it is not. Each row here contains the integers ten through nineteen.

For an excluded start, we move forward to the next integer. For an included end, we move forward to the first integer outside the interval. Once we do that, the object no longer needs to remember the boundary flags.

This works because we are dealing with integers. Adding one would not preserve the meaning of a floating-point interval."
-->

---

# An invariant keeps the normalized bounds valid

**Invariant:** a condition the type establishes when constructed and preserves through its operations.

Here, `StartInclusive <= EndExclusive`. Equal bounds represent an empty interval.

<div class="grid grid-cols-2 gap-6">

<div>

```csharp
public readonly struct Interval
{
    public long StartInclusive { get; }
    public long EndExclusive { get; }

    private Interval(
        long startInclusive, long endExclusive)
    {
        StartInclusive = startInclusive;
        EndExclusive = endExclusive;
    }
    // Factory and Contains follow.
}
```

</div>

<div>

After rejecting reversed input bounds and invalid enum values, the factory normalizes them:

```csharp
long startInclusive = (long)min
    + (minBoundary == Boundary.Exclusive ? 1 : 0);
long endExclusive = (long)max
    + (maxBoundary == Boundary.Inclusive ? 1 : 0);

// Store all empty intervals as [0, 0).
if (startInclusive >= endExclusive)
    return default;

var result = new Interval(startInclusive, endExclusive);
return result;
```

</div>

</div>

<!--
Teaching notes · 4 minutes

"The factory checks the input and converts it once. Every later operation can rely on ordered, normalized bounds. Invalid construction fails here, close to the cause, instead of allowing a bad value to travel through the program.

The public input is int. Internally we use long so an inclusive int.MaxValue can become an exclusive endpoint one greater without overflowing. The cast happens before addition.

An interval with no integers in it becomes the empty interval [0,0). That also makes default construction a valid empty interval. The complete factory, including validation, is in the reference implementation.

The readonly struct keeps these values together without requiring a separate heap object for each interval. Its get-only properties prevent callers from changing one bound independently."
-->

---

# Encapsulation keeps bound interpretation inside the interval

**Encapsulation:** a type keeps its representation private and exposes the operations callers are allowed to use.

<div class="grid grid-cols-2 gap-6">

<div>

Add this method inside `Interval`:

```diff
+ public bool Contains(int value)
+ {
+     bool result = value >= StartInclusive
+         && value < EndExclusive;
+     return result;
+ }
```

</div>

<div>

Callers use the operation:

```csharp
bool allowed = interval.Contains(reading);
```

</div>

</div>

`Contains` uses the same two comparisons for every interval. It never checks the original boundary choices.

<!--
Teaching notes · 4 minutes

"For [10,20), fifteen passes both comparisons. Twenty fails the second. We use the same method even if the caller originally supplied (9,19]. Construction has already converted that input.

Any function receiving an Interval can rely on its bounds being valid. It does not repeat their validation or reinterpret boundary flags. That knowledge belongs in Interval.

The plus signs mark the added code. The temporary result lets us inspect the answer at a breakpoint before returning."
-->

---

# Local reasoning means using the contracts of your dependencies

**Local reasoning:** understand a piece of code using its own logic and the contracts of what it uses.

```csharp
var unique = RemoveDuplicates(names);
ShowNames(unique);
```

For input `["Bob", "Ann", "Bob"]`, the display receives `["Bob", "Ann"]`.

To understand this code, we need to know:

- What `RemoveDuplicates` returns and whether it changes its input.
- What `ShowNames` does with the result.

We do not need the duplicate-removal algorithm or the display implementation.

<!--
Teaching notes · 3 minutes

Keep this explanation focused. Do not undermine it with a separate debate about whether a trivial wrapper should exist. We already have an operation whose contract hides substantial implementation.

Dependencies still exist. The goal is bounded knowledge, not never opening another file. If ShowNames sorts its input in place without saying so, callers cannot reason from the stated contract. That would be a contract problem to repair.
-->

---

# A caller should not have to list all the fields

```csharp
record struct Readings(int Left, int Center, int Right);
```

<div class="grid grid-cols-2 gap-6">

<div>

Inside the screen update:

```csharp
bool allowed = interval.Contains(readings.Left)
    && interval.Contains(readings.Center)
    && interval.Contains(readings.Right);
SetIndicator(allowed);
```

</div>

<div>

Inside alarm handling:

```csharp
bool allowed = interval.Contains(readings.Left)
    && interval.Contains(readings.Center)
    && interval.Contains(readings.Right);
if (!allowed)
    SoundAlarm();
```

</div>

</div>

Adding a sensor means remembering every list of fields.

<!--
Teaching notes · 3 minutes

These three values have distinct physical positions, but this operation applies identically to all three. The screen and alarm should not each enumerate the representation.

A new field can leave both fragments compiling while neither checks it. Ask students to identify the duplicated knowledge rather than simply count repeated lines.

Do not introduce arrays, indexing, LINQ, or iterator abstractions to solve this particular problem. A named operation is enough for the first lesson.
-->

---

# Put the field-by-field operation in one place

```csharp
static bool AllWithin(Readings readings, Interval interval)
{
    bool result = interval.Contains(readings.Left)
        && interval.Contains(readings.Center)
        && interval.Contains(readings.Right);
    return result;
}
```

<div class="grid grid-cols-2 gap-6">

<div>

The screen:

```csharp
bool allowed = AllWithin(readings, interval);
SetIndicator(allowed);
```

</div>

<div>

The alarm:

```csharp
if (!AllWithin(readings, interval))
    SoundAlarm();
```

</div>

</div>

The helper owns the enumeration of fields. Both callers express the operation.

<!--
Teaching notes · 3 minutes

Keep the fixed struct. We did not need a new container or an iterator to demonstrate the improvement. The helper could live with the Readings type or a closely related operations module; its physical location is less important than owning this representation-dependent operation.

Adding a sensor still requires updating AllWithin. That responsibility is centralized, not magically eliminated. For an existing collection of equivalent readings, an ordinary loop could implement the same operation. The collection representation is a separate decision.

Same field type alone is not a justification: CustomerId, Age, and RetryCount are all integers but do not form one meaningful set of measurements.
-->

---

# Another call can help, or just make you look elsewhere

<div class="grid grid-cols-2 gap-6">

<div>

Already understandable:

```csharp
if (min > max)
    throw new ArgumentException("Bounds are reversed.");
```

</div>

<div>

An additional call:

```csharp
CheckArguments(value, min, max);
```

</div>

</div>

Its implementation:

```csharp
static void CheckArguments(int value, int min, int max)
{
    if (min > max)
        throw new ArgumentException("Bounds are reversed.");
}
```

The helper hides a clear check and even receives an unused parameter.

<!--
Teaching notes · 3 minutes

Define indirection as reaching an operation through another name or call. It can hide complexity and centralize knowledge, but it can also add navigation and a new contract without a useful gain.

Contrast with AllWithin from the previous slide: that function removed field-layout knowledge from multiple callers. Here the abstraction has not earned its cost. A focused, named bounds-validation method could be justified if several operations share a substantial rule. Neither number of parameters nor number of lines decides by itself.
-->

---

# Show the workflow without its subordinate algorithm

<div class="grid grid-cols-2 gap-6">

<div>

Before:

```csharp
var names = ReadNames(source);
var seen = new HashSet<string>(StringComparer.Ordinal);
var unique = new List<string>();
foreach (var name in names)
{
    if (seen.Add(name))
        unique.Add(name);
}
WriteNames(destination, unique);
```

</div>

<div>

After extracting the implementation shown earlier:

```csharp
var names = ReadNames(source);
var unique = RemoveDuplicates(names);
WriteNames(destination, unique);
```

</div>

</div>

The workflow expresses reading, duplicate removal, and writing.

<!--
Teaching notes · 4 minutes

Keep both versions visible during discussion. The requirement and the algorithm have not changed. The knowledge needed to read the workflow has changed.

A workflow-level guard can stay direct: if names.Count == 0, return. That decision can belong to the workflow. Extracting EmptyInputCheck merely so every line is a function call would miss the point.

The criterion is a consistent level in the main story, not a syntactic ban on conditions. Normalization code such as Trim and ToLowerInvariant can similarly be appropriate inside NormalizeName but distracting if pasted into otherwise high-level import orchestration.
-->

---

# Make the necessary input visible

<div class="grid grid-cols-2 gap-6">

<div>

Hidden input:

```csharp
bool IsAllowed(int reading)
    => reading <= Settings.MaximumTemperature;
```

</div>

<div>

Explicit input:

```csharp
bool IsAllowed(int reading, int maximumTemperature)
    => reading <= maximumTemperature;
```

</div>

</div>

```csharp
IsAllowed(25, maximumTemperature: 20); // false
IsAllowed(25, maximumTemperature: 30); // true
```

The second signature tells the caller which setting controls this result.

<!--
Teaching notes · 3 minutes

The hidden version does not mutate anything here. A hidden dependency and a side effect are different concepts.

A meaningful Interval can replace separate settings when we need an interval's semantics. Do not introduce one for an operation that only needs a maximum. Pass the information the operation needs.

Making a dependency explicit does not make it appropriate. The second lesson will examine a workflow-specific statistics parameter that is explicit but misplaced.
-->

---

# Use a parameter view that matches the intended access

Reading a collection:

```csharp
IReadOnlyList<string> RemoveDuplicates(IReadOnlyList<string> names);
```

Intentionally changing a collection:

```csharp
void SortInPlace(List<string> outNames)
    => outNames.Sort(StringComparer.Ordinal);
```

```csharp
IReadOnlyList<string> names = new List<string> { "Bob", "Ann" };
// names.Add("Eve"); // Does not compile: this view exposes no Add.
```

The signature should make the permitted access clear.

<!--
Teaching notes · 3 minutes

Mutation is a supported design choice, not automatically a defect. Use an operation name and parameter interface that communicate what can change. The outNames prefix is the instructor's naming convention for intentionally written parameters; it is not C#'s out modifier.

IReadOnlyList restricts collection mutation through this view. It does not promise deep immutability of arbitrary elements, exclusive ownership, or a snapshot that nobody else can change. Here strings avoid a second discussion about mutable elements.

The next slide gives a short concrete example of why the view and the underlying object are distinct. Do not introduce the caveat as unexplained ownership terminology.
-->

---

# Two references can observe the same change

```csharp
var owner = new List<string> { "Bob", "Ann" };
IReadOnlyList<string> readOnlyView = owner;

SortInPlace(owner);

Console.WriteLine(readOnlyView[0]); // Ann
```

The read-only view cannot sort the list, but it sees the owner's change.

A read-only parameter limits what the receiving code is offered. It does not freeze the shared object.

<!--
Teaching notes · 3 minutes

Define alias briefly: another reference to the same object. The behavior here is intentional and unsurprising once the relationship is known.

Do not use this as a warning against mutation in general. It explains the precise guarantee made by a read-only view. If stable independent data is required, the contract and representation must provide it; that is a separate requirement.

Close with the lesson's central question: what is this function allowed to use, what may it change, and which implementation details can its caller ignore?
-->

---

# Compare the knowledge required by two callers

<div class="grid grid-cols-2 gap-6">

<div>

Caller A:

```csharp
bool allowed = interval.Contains(readings.Left)
    && interval.Contains(readings.Center)
    && interval.Contains(readings.Right);
SetIndicator(allowed);
```

</div>

<div>

Caller B:

```csharp
bool allowed = AllWithin(readings, interval);
SetIndicator(allowed);
```

</div>

</div>

Explain:

1. What must A know that B does not?
2. What must the contract of `AllWithin` promise?
3. Where would adding a fourth sensor require an edit?
4. Would a wrapper around `SetIndicator(allowed)` help here?

<!--
Teaching notes · 4 minutes

Expected answers: A enumerates every field; B needs the guarantee that every reading is checked. The helper still has to be maintained when the representation grows. An extra generic wrapper around a clear SetIndicator call has no demonstrated benefit.

This exercise reinforces abstraction, contracts, and indirection using the example students just saw. There is no arithmetic trap or new domain rule to distract from the design question.

Next lesson: appropriate dependencies. A helper can hide its internals yet still know too much about a particular caller.
-->
