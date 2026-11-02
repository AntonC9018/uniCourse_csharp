# Unresolved at the time of writing

## 1. TP-119's wrong text is still in the committed ledger — unresolved

`internal/teaching-points.json` still states that a failed `int.TryParse` "returns the variable's prior value". The note
contradicts it correctly, but the fixing work order `wo-1964b204…` made no edit (no worker session; evidence
`clean factual verdict needs no fixing`), so nothing has repaired the ledger entry. The only write path that could is a later
`ledger` or `factual_fixing` pass; the `english_authoring` write assignment does not include the ledger's point text.
Detail and evidence: `tryparse-failure-and-ledger-tp119.md`.

## 2. Two timestamp links in the brace/binding section sit ~13–23 s after the narration — unresolved

Fresh check, 2026-10-10, comparing `notes.en.md` against `internal/transcript.md`:

| note claim | note link | transcript narration |
|---|---|---|
| "the hidden number is made fixed rather than generated anew" (`notes.en.md:323`) | 00:13:11 (`t=791s`) | "Давайте я тут скажу, что R фиксированный…" at 00:12:48 (`t=768s`) |
| "The recorded tests show both messages for 50, but only the second for a different number" (`:325`) | 00:13:15 (`t=795s`) | "если я даю 50, выполнились обе инструкции" 00:12:55–00:13:02 (`t=775s`–`t=782s`) |

The very next link in the same block, 00:13:28 (`t=808s`) for the "always use braces" recommendation, matches its narration
(00:13:26–00:13:34). Spot checks of other sections (45 s `Next(100)`/`maxValue`, 93 s extracting the comparison function,
638 s "== needs both sides", 1042 s wrapping into `while (true)`, 1331 s the definite-assignment compile error, 1468 s the
zero on failed parse) all align, so the drift looks local to this paragraph.

It is unresolved because the transcript cannot establish what was on screen: the author may have anchored to the moment the
fixed value appeared in the editor rather than to the spoken sentence. Nothing in the record decides between the two readings.

Why neither automated pass caught it: the author's read-back validation (run 332) checked that each `[HH:MM:SS]` label equals
the `t=` seconds in its own URL, and the verifier's approval (run 334) checked anchor existence and claim support — neither
aligned note links against transcript cues. A dense cue list also defeats mechanical proximity checks; only reading the two
texts against each other exposes it.

## 3. "Zero on failure" is overload-specific — noted, not a defect

The note's claim is scoped to `int.TryParse(string, out int)`, which is correct. Newer `IFormatProvider` /
`INumberBase`-based overloads on the same Microsoft page document "an undefined value on failure" instead of zero (checked
2026-10-10). Nothing in this note is wrong; keep the overload scoping if the paragraph is ever retold or generalised.

## 4. No exact-state source exists for the video's code — structural, unlikely to change

Nothing in `uniCourse_csharp` holds the guessing-game code, so lab citations cannot be promoted from "related" to "exact"
without first committing that program somewhere in the repository (fresh check 2026-10-10:
`git log --all -S 'Next(100)'` matches only the note's own commit `a8cb360`). Until then, ten citations stay labelled related at
`19d6c57ba17ef64ef9c65d22808e2c3f682768c2`; see `immutable-citation-basis.md` for the section-level map.