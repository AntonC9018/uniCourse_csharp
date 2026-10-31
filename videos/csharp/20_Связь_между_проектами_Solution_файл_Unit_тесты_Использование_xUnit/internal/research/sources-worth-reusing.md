# Source links worth reusing for this video

Video: `9DBoMknXbVQ` — "Связь между проектами. Solution файл. Unit тесты. Использование xUnit."
Position 20 in playlist `PL4sUOB8DjVlVVw9Yx_tUO7fRPDYeaACXD`, position 7 in
`PL4sUOB8DjVlXKTknA5IW75BGwcTw6PU3r`. The same video is also mirrored as
`videos/design_patterns/07_…` via a shared-reference repair, so citations into the
`uniCourse_csharp` repository should always name the `videos/csharp/20_…` folder.

## Immutable repository checkpoints

- https://github.com/AntonC9018/uniCourse_csharp/commit/b333fe75bfe90c7d2c965c1a57f477497887e5e2
  — manual `Debug.Assert` checks, `OutputType Exe` test project, branch-based helper with
  the age 8–10 / coefficient-4 experiment, later zero-origin `GetHumanAge_New` loop and the
  `Interval` / `Config` record structs.
- https://github.com/AntonC9018/uniCourse_csharp/commit/e4b890a75d5e014ab00227c27ff6a35965acb9a1
  — test project becomes a library with xUnit package references; one `[Theory]` with nine
  `[InlineData]` rows and `Assert.Equal`.

Useful blobs at those revisions:

- `examples/tests_example/code/Helper.cs` — the file that settles identifier questions
  (`intLen`, not `length`; `firstAgeCoef`; `secondAgeCoef`; `_3to7intervalCoef`).
- `examples/tests_example/tests/Tests.cs` — the authoritative nine expected pairs and the
  configuration `Length 1/15, 1/8, 5/3, 99999/2`.
- `examples/tests_example/tests/tests.csproj` — both states: `OutputType Exe` at b333fe75,
  package references only at e4b890a7.
- `examples/tests_example/code/code.csproj`, `examples/tests_example/Directory.Build.props`,
  `All.sln` (repository root, holds the two `tests_example` entries).

## Folder evidence

- `internal/video.json` — upload id, etags, caption id, canonical VTT / cue / spoken-text
  hashes and playlist memberships.
- `internal/transcript.md` and `internal/transcript.vtt` — the spoken record behind every
  "recording says" correction in the note.
- `internal/teaching-points.json` — coverage only: `TP-001`…`TP-234` mapped to the note's 29
  heading anchors.

## Note revisions

- `6bd23cf30f17df99bc8ed719aa202ce6c36b1026` — "Update course video 9DBoMknXbVQ": the
  English note and ledger as first committed, still containing `const int length`.
- `7d858a22bbd351d858d1f563ee0410f38b122271` — "Correct course video 9DBoMknXbVQ": the
  `length` → `intLen` fix and nothing else.
