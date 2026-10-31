# Why this note has no commit-pinned code citations, and which sources are reusable

Established by the `english_authoring` work order
`wo-224fbd13b916169329f38348ccb1f8002d7101ebf10c55f5298726663213bf44`,
worker run 267 (codex session `01a116fd-bf6a-7f91-be37-aad205ba7215`).

## The bomb program has no committed source anywhere in the repository

- At the authoring checkout revision `accd55e6a1d8349548cd48e7a1768ecf4dfaf94f`
  (recorded on 21 of the run’s 24 tool events; three events have no repository revision),
  `read_file` on `videos/csharp/15_C#_enum/notes.en.md` failed and `git_history`
  for that path returned nothing. The author's words: "The checkout has no
  existing English note or committed history for it."
- `All.sln` was read at the pinned revision
  `ef4630d291e20ffbcf93018cba43a53971d327a2` through `git_read_file`. That
  solution declares exactly one project, `field_masks`
  (`field_masks\field_masks.csproj`, `{088C22F7-C8A2-45C9-8868-999DDE5AA56B}`).
  There is no bomb-defusal or enum project.
- Consequence the author accepted: every code sample is captioned
  "Reconstructed [state]" and the note states outright "No inspected committed
  source contains these exact bomb-program states, so the samples are not
  presented as commit-exact code."
- The note's closing history trail therefore uses timestamped video states, not
  commit-pinned blobs, and says so: "The inspected local history provides no
  committed source for these exact states; no unrelated exercise-adding commit is
  substituted."
- My own cross-check 2026-10-10 at `d8775cfafc393b0ac88349bad484cc4bf8f1737c`
  (not a worker finding): no tracked `.cs` file matches `Portrait` or
  `PortraitToString`, and the `enum` hits are all in unrelated
  `examples/field_masks`, `examples/lab5_example`, `examples/property_dict`.

## The deterministic citation check had nothing to check

- The verifier's prompt carried `{"schemaVersion":1,"checks":[]}` for the English
  commit: an empty list, consistent with a note that cites no repository
  revision. This is why the "bad citation" dimension of verification was
  vacuous here rather than passed.

## The enum lab is linked by path, and its commit must not be pinned

- The note links `[enum lab](../../../labs/1_basic/09_enum.md)` in the opening
  section, and lists `labs/1_basic/09_enum.md` among its source paths.
- The file's current numbered path comes from commit
  `19d6c57ba17ef64ef9c65d22808e2c3f682768c2` ("Add functions-with-parameters lab
  after variables (int/string only, pre-bool)", 2026-09-28). Inspecting it shows
  a renumbering pass (`08_enum.md` → `09_enum.md`) plus an unrelated lab
  addition. It is an exercise-adding commit, so pinning it as evidence for this
  lesson's installation or code states would be exactly the prohibited
  substitution. Linking the page itself is the correct treatment.
- Lab content (for reuse): a traffic light with three colors, a color → action
  function, an action → printed text function, a next-state function, and a check
  that the states cycle.

## Commits worth knowing

- At the authoring-time revision, `d8775cfafc393b0ac88349bad484cc4bf8f1737c` was the only commit touching this
  folder; it adds `internal/transcript.md`, `internal/transcript.vtt`,
  `internal/video.json`, `internal/teaching-points.json` and `notes.en.md`. It is
  the commit the verifier was asked to approve.
- `accd55e6a1d8349548cd48e7a1768ecf4dfaf94f` — checkout HEAD while the author
  worked; the folder had no note and no history at this revision.
- `ef4630d291e20ffbcf93018cba43a53971d327a2` — pinned revision used to inspect
  `All.sln`.
- `19d6c57ba17ef64ef9c65d22808e2c3f682768c2` — lab renumbering, irrelevant to
  this lesson's code states.

## Sources the author opened, worth reusing

Opened 2026-10-07 during run 267:

- https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
- https://learn.microsoft.com/en-us/dotnet/api/system.enum.tostring?view=net-10.0
- https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics/debug.assert?view=net-10.0
- https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics/debug.fail?view=net-10.0
- https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements
- https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements
- https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/enums

Note that these are documentation pages, not repository citations: they carry no
commit pin, and the note treats them as authority for the C# language and
runtime behaviour only.