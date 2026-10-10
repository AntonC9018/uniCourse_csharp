# Coverage map, scope boundaries and terminology decisions

## 124 points collapse into 18 anchors

`internal/teaching-points.json` (built by the app from the coverage map the author wrote in run 332) holds 124 teaching points,
124 `englishCoverage` entries with status `covered`, and 18 distinct note anchors. Kinds present: 54 explanation, 27 fact,
20 reason, 8 code_change, 6 test_result, 5 edge_case, 4 scope_boundary.

The author grouped points by contiguous transcript ranges rather than one-per-heading; heaviest anchors:

- `#an-if-reads-a-bool-expression` — 12 points
- `#combine-checks-with-short-circuit-operators` — 11
- `#use-break-and-continue-inside-the-guessing-loop` — 10
- `#evaluate-comparisons-and-store-bool-values` — 9
- `#move-the-exit-test-into-the-loop-condition` — 9
- `#a-failed-parse-is-not-a-guess-of-zero` — 2 (TP-118 and TP-119; see `tryparse-failure-and-ledger-tp119.md`)

This grouping is why the note is ~19 narrative sections rather than 124 micro-paragraphs, and why one anchor legitimately
carries a contradiction between two ledger entries.

## `for` is announced but never developed

- Recording, [00:00:00](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=0s) – [00:00:09](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=9s):
  "Flow control, то есть if, while, for." Ledger TP-001 (run 305, `ses_ee50c0c77ffeBpXguGj2G8xGCS`, segment S-001) records the
  lesson scope as "flow control in C# (if, while, for) plus bools", and no ledger point after the opening covers `for`.
- Decision (`notes.en.md:296`): the note's Scope paragraph states that `for` "is named in the opening" but "does not develop
  `for` syntax", and that functions, variables and console input are supporting tools rather than subjects. Without that
  sentence a reader could reasonably expect a `for` section that the recording never delivers.

## First use before explanation, flagged rather than hidden

Ledger TP-006 (run 305, segment S-001, 62310–70690 ms) records that the very first `if` already relies on `bool` and `if`
"and the lesson defers the detailed explanation of both to later". The note keeps the video's order and marks the deferral at
`00:01:02` (62 s) instead of silently reordering. The same pattern is stated for the loop: ending an infinite loop is
introduced only after the loop itself.

## "Fixed" is disambiguated explicitly

The instructor announces at [00:12:48](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=768s) that `r` will be "фиксированный"
so it is not regenerated each time and the same input can be checked. `notes.en.md:323` renders it as "the hidden number is
made fixed rather than generated anew" and adds "it does not introduce the C# `fixed` keyword" — a one-clause guard against
the English word pulling in the unrelated keyword.

## Reconstruction labelling is global, not per-sample

Rather than qualifying every fenced block, the note declares once, before the first section, that samples are "simplified
reconstructions of the timestamped video states, with English messages and descriptive names", and that inspected lab sources
are "cited as related examples, not exact saved states". That declaration is the basis for the ten `19d6c57…` links
(see `immutable-citation-basis.md`).

## Artefact identity at approval time

Recorded output hashes and a re-check on 2026-10-10 agree, so the approved note and ledger are still the ones on disk:

- `notes.en.md` → `bad0091f7bc99b270c4e675142d1312ea0b2a1f4e8a85f43e4d037608a34a988`
- `internal/teaching-points.json` → `8df5822cf96952d254196fb70cdf74d1e0671c3f5e704f2190e6f4e068b7fbf6`
- `internal/transcript.md` → `4706a270ca7e802863380e1d79da8ed17298319f5078742250cdd18e8eeaa184`
- ledger `transcriptSha256` (`b8f48db…`) equals the canonical VTT hash recorded for this video.