# Sources worth reusing

Immutable code stages — see `example-code-stages.md` for what each one establishes:

- <https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs>
- <https://github.com/AntonC9018/uniCourse_csharp/blob/80eddb6786130b733401932cadfb35f5c939d8f4/examples/field_masks/Program.cs>
- <https://github.com/AntonC9018/uniCourse_csharp/blob/6b312db7c3e158e27048d8921f426054c4a1b2b0/examples/field_masks/Program.cs>
- <https://github.com/AntonC9018/uniCourse_csharp/pull/1> — merged PR "Field mask" whose three
  commits are exactly the stages above; the lab points at it as the staged code for the video.

Documentation the note cites, opened and checked by `english_authoring` run 355
(`01a11cb8-550c-7070-aabc-06ecd5a60dab`) on 2026-10-08:

- `required` members — <https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/required>
  Basis for saying `required` obliges initialization but does not make `null` impossible.
- `struct` / value types — <https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct>
  Basis for the note's refusal to claim structs never reach the heap.
- `with` expressions — <https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/with-expression>
  Basis for correcting TP-142; see `recording-ambiguities.md`.
- `Debug.Fail` — <https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.debug.fail?view=net-9.0>
  Basis for correcting TP-119 and for the `DEBUG`-conditional statement.
- Assertion handling — <https://learn.microsoft.com/en-us/visualstudio/debugger/assertions-in-managed-code?view=visualstudio>
  Replaced a `source.dot.net` link to the .NET `Debug.cs` implementation that the author first
  drafted; the docs page is the stable one to keep.

- `Debug.Assert` — <https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.debug.assert>
  was read by run 355 but is no longer cited in the note; keep it only if the assertion branch is
  re-explained.
- Checked by me on 2026-10-10: the five documentation URLs and the PR link each returned HTTP 200.
  The three blob URLs were not re-fetched; they were resolved locally with `git show`.
