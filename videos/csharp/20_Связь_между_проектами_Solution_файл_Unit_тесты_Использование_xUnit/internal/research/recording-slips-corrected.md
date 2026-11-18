# Slips in the recording that the note deliberately corrects

Mostly established by the `english_authoring` work order (run 158, codex/gpt-6.1-sol,
session `01a0fc19-11bc-7612-a9fa-541b3373178d`), which pinned the two immutable commits
first and then wrote the narrative around the differences between what was said and what
was committed. Run 182 (`factual_verification`) independently re-derived or accepted most of
these.

- **Second-year coefficient spoken as 7, corrected to 8.** The first spoken estimate gives
  ~22 for age 2; the implementation and the later explicit correction use 8, giving 23.
  Evidence: transcript moment [00:09:17]
  (https://www.youtube.com/watch?v=9DBoMknXbVQ&t=557s) and the committed row
  `[InlineData(2, 23)]` at `e4b890a75d5e014ab00227c27ff6a35965acb9a1:examples/tests_example/tests/Tests.cs`.
- **The 3–7 block is five years, not seven.** Years 3,4,5,6,7 at coefficient 3 contribute
  15; the seven-year total is `15 + 8 + 5 * 3 = 38`. The spoken 21 and provisional 44 are
  not a consistent derivation of the final rule, and 44 is the rule's answer at age 10, not
  age 7. Evidence: [00:10:43] (…&t=643s), [00:10:52] (…&t=652s), and committed rows
  `(7, 38)` / `(15, 54)`.
- **Verbal "maximum" is corrected to a minimum.** The block contribution is capped by the
  smaller of the full block length and the reached depth (`Math.Min`). Evidence: ledger /
  note passage at [00:23:50] (…&t=1430s).
- **"The spoken two is the coefficient, not a length of two."** The open-ended group has no
  infinity marker, so the committed config uses a very large finite length (99999) with
  coefficient 2. Evidence: [00:35:23] (…&t=2123s) and the committed row
  `new(Length: 99999, Coefficient: 2)`.
- **Abbreviated wording around 40 must not become a test row.** The note forbids reading
  "input 40 expects output 40" from the recording; no such InlineData row exists (the rows
  are 0, 1, 2, 3, 4, 6, 7, 8, 15). Evidence: [00:42:45] (…&t=2565s) and the committed
  `Tests.cs`.
- **"Exactly one expectation fails" is wrong — and the note is right, the ledger is wrong.**
  Changing the tail coefficient to 8 makes *both* age 8 and age 15 disagree: `15 - 8 + 1 = 8`
  gives `38 + 8*8 = 102` instead of 54, and age 8 also reaches the interval. Ledger TP-212
  states "exactly one expectation fail"; the note contradicts it deliberately. Run 182
  re-derived the arithmetic and declined to file a finding: "The note contradicts the ledger
  but the arithmetic supports the note … That's a defensible correction (ledger is derived
  from transcript)." Evidence: [00:43:32] (…&t=2612s).
- **The spoken failure description reverses expected and actual** in the manual check
  demonstration. The note keeps only the 23/24 mismatch as evidence and says the recording
  "does not show both disagreeing values clearly". Evidence: [00:19:44] (…&t=1184s).
- **The interval term is `age - start + 1`, not subtraction in the opposite order.** The
  note states explicitly that the recording's abbreviated verbal formula should not be read
  as reversed subtraction. Evidence: [00:29:04] (…&t=1744s), ledger TP-139.
- **The decomposition is 15 for the first year plus 8 for the second**, irrespective of the
  recording's reversed phrasing. Evidence: [00:26:56] (…&t=1616s).
- **"Snapshot tests are particularly valuable" is the instructor's judgment.** The note
  records the claim as a personal usefulness judgment, "not a universal ranking of test
  techniques", so the section's enthusiasm does not read as fact. Evidence: [00:44:41]
  (…&t=2681s).
- **`Directory.Build.targets` is a described alternative, not the committed layout.** The
  instructor's account of injecting test dependencies via a conventional shared import is
  presented as an alternative setup, explicitly separated from the package references that
  are actually committed. Evidence: [00:37:24] (…&t=2244s) and
  `e4b890a75d5e014ab00227c27ff6a35965acb9a1:examples/tests_example/tests/tests.csproj`.

## Why this matters for future maintenance

The note's credibility rests on separating three layers: what was *said* (transcript
moment), what is *committed* (immutable blob), and what is *reconstructed* (labelled as
such). Any future edit that merges these layers — e.g. quoting a spoken intermediate value
as if it were committed — recreates the class of defect that F-001 caught.
