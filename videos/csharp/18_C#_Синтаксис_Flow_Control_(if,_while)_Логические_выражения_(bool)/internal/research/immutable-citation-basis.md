# Why every code citation in this note is "related", and what the pin holds

## The pinned revision

All ten immutable code citations in `notes.en.md` resolve to a single revision:

`19d6c57ba17ef64ef9c65d22808e2c3f682768c2` — "Add functions-with-parameters lab after variables (int/string only, pre-bool)",
AntonC9018, 2026-09-28, touching `labs/1_basic/05_functions.md` … `18_interface.md`.

- 4 links point at `labs/1_basic/06_bool.md` ("bool-lab examples", "AND examples", "lazy-evaluation examples",
  "bool-comparison example"), at `notes.en.md` lines 146, 177, 219, 257. The matching lab headings include §4, “Сравнение переменных это выражение” in `06_bool.md`; “Вложенный if vs &&” belongs to `07_flow_control.md`, not `06_bool.md`.
- 6 links point at `labs/1_basic/07_flow_control.md` ("block examples", "brace-less example", "if-else examples",
  "chain example", "break-and-continue examples", "return-inside-while example"), at lines 278, 314, 341, 386, 466, 661.
- Each link text is explicitly qualified "related committed …": none claims the exact state of the guessing game.

`factual_verification` run 334 (`ses_ee4f99f43ffee9A1yKtGuJ0XCg`) resolved the revision with
`git rev-parse --verify --end-of-options` and read the blobs at that revision rather than at HEAD.

## Why "related" is the only honest label

The guessing game from the video does not exist anywhere in the repository. Fresh check 2026-10-10:
`git log --all -S 'Next(100)' -- '*.md' '*.cs'` matches exactly one commit, `a8cb360` ("Update course video mj9E9BhOAhU"),
which is the commit that added `notes.en.md` itself — i.e. the string exists only in the note, never in a lab or source file.
So no commit can carry an exact state of the video's code, and "exact" labelling would be unsupportable for any of these
references. The author (run 332) made the same call and stated it in the note's preamble: samples are "simplified
reconstructions of the timestamped video states", and inspected lab sources are "cited as related examples, not exact saved
states".

## Section-level map of the pin (fresh re-verification, 2026-10-10)

Not recorded in the worker transcript, re-derived by reading the blobs at `19d6c57…` so future edits can re-anchor precisely:

`labs/1_basic/06_bool.md`

- line 167 `bool c = a == b;`, §4 "Сравнение переменных это выражение" (66), §5 "Сложное сравнение переменных" (81) — "bool-lab examples"
- §11 "Сравнение логических значений" (163) — "bool-comparison example"
- §13 "Оператор `&&`" (192), §14–16 "Тонкости оператора `&&`" (206, 234, 263) with `bool result = A() && B();` — AND and
  lazy-evaluation examples

`labs/1_basic/07_flow_control.md`

- §4 "Сравнение переменных это выражение" (66)
- §6 "Несколько инструкций в теле `if`" (162), §7 "`if` без фигурных скобок" (177), §8 "Блок `else`" (199), §11 "`if`-`else`
  без фигурных скобок" (265) — block / brace-less / if-else examples
- §13 "Цепочка `else if`" (296) — "chain example"
- §15 "`break` и `continue`" (517) — "break-and-continue examples"
- §16 "`return` внутри `while (true)`" (574) — "return-inside-while example"

## Exercise links are repository-relative, not website URLs

`notes.en.md` line 25 points readers at the actual lab documents with `../../../labs/1_basic/06_bool.md` and
`../../../labs/1_basic/07_flow_control.md`; three levels up from `videos/csharp/<this folder>/notes.en.md` is the repository
root. The verifier confirmed this depth explicitly.

## Commit the note was verified at

`a8cb360f026ee61d22dc43a32ffa869859a93f12` — "Update course video mj9E9BhOAhU", 5 files / 8669 insertions
(`internal/teaching-points.json`, `internal/transcript.md`, `internal/transcript.vtt`, `internal/video.json`,
`notes.en.md`). This was the only lesson-content commit when the verdict was recorded. The current folder history also includes the later research dossier commit; the note approval remains bound to the lesson commit above.