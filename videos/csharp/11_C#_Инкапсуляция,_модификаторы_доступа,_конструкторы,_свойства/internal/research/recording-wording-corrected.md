# Where the recording's wording had to be corrected

Captured by `english_authoring` (session `01a0fed9-8b01-76a1-9002-457ce42b9c61`); the recorded
teaching-point ledger, the note's wording, and the transcript lines are the evidence.
Ledger ids and video timestamps refer to `internal/teaching-points.json` and
`internal/transcript.md` in this folder.

- Ledger TP-122 (25:29–25:48; 1529030–1548600 ms) says "an unqualified `data1` inside the constructor body can still
  be resolved to the object's field while the parameter `data1` refers to the temporary stack
  variable." The recording itself is looser: at 25:56 it says only that fields and parameters share
  names but "refer to different memory cells" (Data 1 as field vs Data 1 as parameter). The author
  corrected the resolution rule explicitly: unqualified `data1` resolves to the **parameter**
  because it shadows the field; `data1 = data1;` would assign the parameter to itself and leave the
  field unchanged. Wording lives in notes.en.md ("Trace allocation and constructor arguments", step
  6 and the paragraph after the step list).
- Ledger TP-120/TP-121 say the constructor is "declared with three parameters" and "receives three
  arguments" (hidden `this` + two data parameters). The recording's own count at 25:32 is the same
  ("три параметра: один скрытый … This и два параметра под входные данные Data 1, Data 2"). The note
  keeps the ledger's hidden-receiver idea but states there are **two declared parameters**, because
  the teaching point is about the hidden receiver, not about a three-parameter signature.
- The recording's remark that one could "try to return something" from the constructor was
  corrected: a C# constructor has no return type and cannot use `return expression;`; the result of
  successful `new` is the created object reference (transcript 27:19/27:27 agrees that the result of
  the expression is always the address of the new object).
- Ledger TP-130 records the object address as the spoken label "88000". The captured transcript
  text contains only "8000" (26:38, 27:01, 27:04). The note handles this by saying the later spoken
  label "88000" does not establish another allocation and that numeric labels are diagram shorthand.
  See `verification-and-open-items.md` for why this stays an open item.
- "Temporary data is erased" (end of the walkthrough) was restated as "the end of its role in the
  call" — not a promise that bytes are securely wiped or that every parameter occupies a physical
  stack cell. The storage model (dynamic memory for the object, temporary cells for parameters) is
  presented as conceptual, not as a guarantee of physical layout.
- Ledger TP-034 states `required` cannot be applied to private fields; the recording supports this
  directly at 07:51 ("required на Private полях нельзя использовать"). The note phrases it as a
  visibility rule (required members must be accessible enough for callers to initialize them) and
  adds that merely removing `required` does not grant external initializer access.

# The recording's incomplete sweep is covered by explicit hedges

- The recording replaces the checked/clamping `SetHealth` with a bodyless auto-property `set;` (auto
  properties do not regenerate the deleted validation, 19:58/20:04). The note makes the loss of the
  earlier check explicit and answers it in a details block: a private auto-property setter limits
  who may write, but an allowed internal assignment can still store a negative number.
- Assertions versus runtime validation: the recording demonstrates an assertion failure at 04:11
  (zero health breaks the living-player precondition) but never contrasts Debug/Release compilation.
  The note adds that `Debug.Assert` calls are conditional on the `DEBUG` compilation symbol and that
  assertions alone do not enforce rejection in a normal Release build.
- Negative health arithmetic: the note separates "the assertion version violates the contract and
  can stop the debugging run" from "negative integer arithmetic itself is not a C# crash", and
  presents clamping as an alternative policy rather than a correction.
