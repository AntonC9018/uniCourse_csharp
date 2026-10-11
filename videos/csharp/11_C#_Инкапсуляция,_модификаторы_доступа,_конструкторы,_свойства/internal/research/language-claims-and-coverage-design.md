# C# claims the author relied on, and their support

Each item names where the support lives. "Fresh check" items were verified outside the worker
record and are marked with the check date and source URL; they were **not** worker findings.

- `required` cannot be kept on a private field, because required members must be at least as
  visible as their containing type. Worker support: transcript 07:51 (the recording states it
  outright) and ledger TP-034; `factual_verification` (run 201, session
  `ses_f011f16fbffepriwnMBs4FyAwl`) additionally cited the compiler rule by name.
  Fresh check 2026-10-11:
  https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required ("Required
  members must be at least as visible as their containing type") and error text CS9032 at
  https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/property-declaration-errors
- Assigning a `required` field inside a constructor does not by itself discharge the
  required-member obligation; the compiler still demands caller initialization (ledger TP-050,
  11:00–11:05). The note therefore removes `required` from `Attack` at that stage rather than
  claiming the constructor satisfies it.
- `Debug.Assert` is `[Conditional("DEBUG")]`: the call exists only when the `DEBUG` symbol is
  defined, so assertions do not reject in a normal Release build. Worker support: notes.en.md
  wording around 03:42. Fresh check 2026-10-11: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.debug.assert
- A constructor has no return type and no return expression; `new` yields the object reference.
  Worker support: recording 27:19/27:27 and the note's clarification paragraph in the last section.
- Private access is granted per **type**, not per instance: a `Stat` method may touch private state
  of another `Stat` object. Worker support: the note's 05:25 section (recording makes the same point)
  and the copy-constructor explanation — the copy constructor can read the other object's private
  health because it is declared in the same type.
- Once any constructor is declared, the compiler no longer supplies the implicit parameterless one;
  hence the explicit `public Stat()` overload. Worker support: notes.en.md 12:27 paragraph.
- A bodyless auto-property `set;` stores the value and nothing else — replacing a checked or
  clamping setter with it silently drops the custom logic. Worker support: notes.en.md 19:16–20:07
  paragraphs and the details block at 19:5x.
- Private access boundaries and accessor visibilities are independent choices: `public int Health
  { get; private set; }` exposes reads inside the type only for writes, and the property's
  accessibility governs its accessors unless an accessor is more restrictive. Worker support:
  notes.en.md 19:12–19:2x paragraphs.

# Coverage design decision

- `english_authoring` (session `01a0fed9-8b01-76a1-9002-457ce42b9c61`) mapped all 131 recorded
  teaching points onto 21 note sections and stored the mapping in
  `internal/teaching-points.json`, with each point anchored to the heading that carries it.
  Reason: the ledger is per-claim with timestamps, the note is narrative; the anchor column is what
  lets a reviewer check "every claim has a home" mechanically.
- The mapping was then machine-checked inside the authoring session: 131 ids present once and in
  order, every anchor resolving to a heading in the final note, and every displayed timestamp equal
  to its `t=` parameter (152 links). `factual_verification` (run 201) re-checked anchor ranges for a
  sample of points (TP-034, TP-051, TP-054, TP-058, TP-067, TP-072, TP-089, TP-107, TP-117, TP-125,
  TP-131) and found each note anchor inside the ledger's millisecond range.
- Ledger shape worth reusing: 131 points, kinds {explanation 61, fact 26, reason 18, code_change 15,
  edge_case 6, test_result 2, scope_boundary 2, correction 1}; 130 backed by `transcript_range`
  evidence, exactly 1 backed by a `repository_blob`.
