# Claims checked against evidence

Each bullet is a claim the note makes, the evidence that settles it, and the
worker that established it. Provenance vocabulary: **author** = `english_authoring`
run 476, session `01a12266-b500-7681-99df-98e5688a3667`; **verifier** =
`factual_verification` run 488, session `ses_edacf1d89ffeA31y69e5sP3bnq`;
**ledger** = the 19 extraction/completeness runs 365–395 under work order
`wo-17f4886…`. Video timestamps are `lIKqnGtzQrA`.

## Cache correctness (the opening bug)

- The name lookup caches an **array index**, not a `Citizen`. Recording:
  "если я индекс сохраняю, надо будет ещё взять по индексу этого ситина"
  (00:06:01–00:06:09). Note sample: `byName[city.Citizens[i].Name] = i;`. The
  author preserved this as an existing correction rather than re-deriving it.
  Verifier confirmed the surrounding model shape at `c79a352`.
- Because the key selects a cached slot while the search reads the live cell, the
  failure is that replacing slot 0 with Joe makes a search for John return Joe.
  Ledger `TP-026`–`TP-038` (extraction run 373, session
  `ses_ee3316618ffeJC1GsvPXjbZ1Vz`, segment `S-005`) already records the index
  cache, the constructor-time population and the failed second search as the
  video's own reasoning, so the note is restating the lesson rather than adding
  an inference.

## Shallow versus deep protection

- `public required Citizen[] Citizens { get; init; }` stops reassignment of the
  property, not of the elements. Source shape at `c79a352`; the note keeps both
  mutation samples (`city.Citizens[0] = …`, `city.Citizens[0].Name = "Changed"`).
  **Correction preserved by the author** from an earlier pass.
- The finished `City` is immutable by convention only. The author states this in
  the note's preamble, and it is why no claim of deep immutability is made.

## Scope defaults and the printed ages

- `Scope().Citizen()` **copies** the currently specified defaults; later edits to
  the defaults model do not reach already-created citizens. Recording 00:47:15–00:47:31
  ("если мы их эйджа посмотрим, у Джона будет 18 … Джо … 32").
- Age is copied **by value**, the home **by reference**, so several staged
  citizens can still share one `MutableHome`. The recording only confirms
  propagation ("если мы посмотрим первого ситина его home и второго … то у них
  он будет у обоих задан", 00:46:46–00:46:55); the value/reference split, and
  the consequence that later default edits cannot reach an existing citizen, is
  the author's reasoning in the note's `<details>` answer.
- The demo program prints `18, 32, 32`. The verifier established this itself at
  `c79a352`: its opening inspection lists "InvalidAge = -1, mutToImmutHome
  dictionary, scope, Chell, ages 18/32/32" as the claims it checked, and the file
  it read carries the third citizen named `Chell`. Run 476 read the same program
  at `c6d7b18`, the state that actually survives in the repository.
- **Chell exists only in the repository, not in the recording.** The video's
  third citizen is described only as "ещё 32" (00:48:00–00:48:04). The note
  attributes the name to "the working source", which is the accurate split.

## Validation

- `if (mut.Address is not { } address)` at `c79a352` rejects **null only**; an
  empty string passes. The recording discusses "not specified" as covering absent
  *or* empty (00:33:51, 00:35:11). The note keeps both: intended rule versus
  implemented rule.
- The age sentinel is `MutableCitizen.InvalidAge = -1` with
  `public int Age = InvalidAge;` (`c79a352`).
- **Source defect worth remembering:** the age branch throws
  `InvalidOperationException("Name not given")` — the same text as the name
  branch. The note marks it `// source's mismatched message`. Anyone reusing
  this example as a validation demo should not copy the message.
- The recording itself concedes the state is incomplete at 00:34:42 ("если мы все
  эти ошибки проигнорим … в принципе, оно будет работать"). The note therefore
  calls the successful run a recorded development check, not proof that later
  commits compile.

## Strongly typed IDs

- `HomeId` is `int`-valued with the field named `Value`
  (`dcfd622:…`, `readonly record struct HomeId { public readonly int Value; … }`).
  Verifier confirmed this by reading that blob.
- `OptionalHomeId` encodes absence with `static readonly HomeId NoHomeId = new(-1)`
  and `IsNull => Id == NoHomeId`, and is a distinct type rather than a bare `-1`
  convention.
- `public Home Get(HomeId id) => Homes[id.Value];` performs **no bounds check**;
  `Get(OptionalHomeId)` returns `null` only for the absence marker. The author's
  wording decision: keep "not supplied" and "supplied but nonexistent" separate.
- Type safety fixes only which overload the compiler picks. Existence, range and
  ownership of the key remain unproven — the author's explicit caveat, and the
  reason the note does not present overloads as validation.
- A value-type edge case the author flagged: `new OptionalHomeId()` runs the
  declared parameterless constructor, while `default(OptionalHomeId)` and
  zero-initialized array elements bypass it and yield an ID of `0`, not `-1`.
  Author's own language-semantics reasoning; the recording does not cover it.

## Later API refactors

- At `abda830` the shared configuration methods are extension methods returning
  `void`; returning the receiver arrives only at `1b854ac`. So a `*Builder` name
  alone does not make an API fluent.
- At `ff2cc72` the concrete `Home(HomeBuilder)` and the interface
  `Home(IHomeBuilder)` disagree, and the adapter verifies with
  `Debug.Assert(home is HomeBuilder)` before casting. Verifier matched this
  against the blob and against the note's "holder for configuration and
  final-builder references".
- A configuration delegate is described in the video but never committed; the
  note labels the snippet as an alternative API sketch, not as `CityBuilder`.
- C# does not prevent capturing the builder into an outer variable, so the
  delegate style is not a runtime non-escape guarantee. Author's caveat.

## Java-versus-C# mapping the note pins down

- A Java field's non-reassignability comes from `final`; `private` only controls
  access (00:49:25–00:49:38).
- Not every `record struct` is implicitly `readonly`; the note's committed sample
  writes `readonly record struct HomeId`.
- A Java `record` is not a C# value type; the analogy is limited to generated
  equality and hashing.