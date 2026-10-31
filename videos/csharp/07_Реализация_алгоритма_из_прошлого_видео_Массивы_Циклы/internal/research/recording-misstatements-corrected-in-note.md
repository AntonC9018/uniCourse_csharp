# Recording misstatements the note corrects, with the proof of the correction

Each bullet: the reconciled teaching point (ledger) text faithfully carries what the
recording said; the note explains the same moment and corrects it. The verifier
accepted every one as "note corrects the recording", not a false claim.

- **TP-024 "one cell carries a link to the next cell"** (04:58, t=298s, transcript.md
  "идёт ссылка на память до следующей ячейки"). Array elements do not store a link to
  a successor; the note states the drawing's arrows identify locations or cell
  boundaries. Factually correct for C# and consistent with the transcript's own
  "contiguous" framing.
- **TP-053 "a variable declared as int may hold references or addresses of int
  arrays"** (10:23–10:26, t=623–626s, "переменная R Она позволяет себе ссылки ...
  именно на массивы"). The recording conflates `r` (type `int[]`) with an `int`
  variable. Note correction: the caller's `r` has type `int[]`, so it can hold an
  integer-array reference; an `int` variable cannot hold that array reference.
- **TP-058 "the parameter receives a copy of the first element of the source array,
  because the parameter in the first position corresponds to the first element"**
  (11:22–11:29, t=682–689s, "в эту переменную пере копируется то что в первой
  позиции"). Note correction: the first argument is the expression `r`, whose value
  is an array reference; parameter position matches *argument* position, not
  *array-element* position. Pass by value copies the reference, not the element, and
  the elements are not copied into a second array.
- **TP-054 "a variable declared as var may only hold numbers"** (10:34, t=634s,
  "сама она будет позволять себе только числа"). Note correction: `var` infers the
  type from its initializer and is not limited to numbers; a standalone `var sum;` is
  invalid because there is no initializer to infer from.
- **TP-072/TP-075 loop boundary**: the recording says `i < length` is "меньше чем
  последняя позиция не включая последнюю позицию" (15:44, t=944s) — i.e. it excludes
  the last position, which is wrong (`length - 1 < length`). TP-072's "negation of
  `i < length` is `i >= length - 1`" is wrong for the same reason: it would stop
  before processing the last cell. Note correction: continue test `i < length`
  includes the last element at `length - 1`; the inverse stop test is `i >= length`
  (equivalently `i > length - 1`), and `i >= length - 1` is the classic off-by-one.
- **TP-024's "why indexing is a jump over zero elements"** is preserved but restated
  as offset arithmetic (start address + index × element size), which the note keeps
  inside the recording's element-slot model (addresses 700/800/801/802 are teaching
  units, not literal byte addresses).

Ledger wording kept: the final teaching-points.json still carries the recording's
original text for TP-024, TP-053, TP-054, TP-058, TP-072, TP-075 — the ledger records
what the video said; the note carries the correction. Do not "fix" the ledger text to
match the note; the pairing is intentional.

Sources: reconciled points supplied to run 175 (codex session
01a0fdc9-40c2-72e1-98e2-e7872b17ba4d, english_authoring); corrections written in
notes.en.md; each accepted by run 176 (opencode session
ses_f022c5cc5ffemvQYAgHcZGI7y2, factual_verification). Transcript quotes are from
internal/transcript.md pinned at commit 4814706ae2fafbc797829239a33afc2eeefa29e8.
