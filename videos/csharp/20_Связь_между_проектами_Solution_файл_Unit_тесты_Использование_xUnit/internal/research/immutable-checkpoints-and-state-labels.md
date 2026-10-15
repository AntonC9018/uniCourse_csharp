# The two immutable checkpoints, and why every other code state is labelled "later related state"

Decision by the `english_authoring` work order (run 158, session
`01a0fc19-11bc-7612-a9fa-541b3373178d`), which resolved exactly eight blobs through
`git_read_file` before drafting, then reused the same two revisions for the entire note.

- The note carries code claims on exactly two revisions:
  - `b333fe75bfe90c7d2c965c1a57f477497887e5e2` — subject "stuff", 2025-10-30 14:39:36 +0200.
  - `e4b890a75d5e014ab00227c27ff6a35965acb9a1` — subject "Add unit tests", 2025-10-30
    14:55:59 +0200.
  Every hex revision string in the note is 40 characters; there are 33 of them and only
  those two distinct values. (Checked 2026-10-11 against
  `git show 6bd23cf30f17df99bc8ed719aa202ce6c36b1026:<folder>/notes.en.md`.)
- Reason: the repository has no commit per video moment. The intermediate states the
  lesson actually builds — console templates, the printing loop, provisional arithmetic,
  the failed inclusive-start edit, the temporary `[Fact]`, and the other application's
  snapshot windows — exist only in the recording. So the note's closing "Code history
  trail" states this once, and each committed sample is labelled a "later related state",
  "related experimental state" or "related resulting state", with the reconstruction
  explicitly called simplified.
- Consequence for verification: a claim about a committed identifier or attribute is checked
  against the blob at the pinned revision (`git show <commit>:<path>`), while a claim about
  an intermediate state must be carried by a transcript range or a ledger entry instead.
  Mixing the two is what produced the F-001 defect.

## What is actually at each checkpoint

At `b333fe75…e2` (all verified with `git show` on 2026-10-11):

- `examples/tests_example/tests/Tests.cs`: top-level `CreateConfig()` returning
  `[Length 1/Coef 15, Length 1/Coef 8, Length 5/Coef 3, Length 99999/Coef 2]`, a
  `void Test(int input, int expected)` using
  `Debug.Assert(actual == expected, $"Output for {input} is {actual}, expected {expected}")`,
  and nine calls `Test(0,0)…Test(15,54)`.
- `examples/tests_example/tests/tests.csproj`: `<OutputType>Exe</OutputType>` plus
  `<ProjectReference Include="..\code\code.csproj" />`.
- `examples/tests_example/code/Helper.cs`: the branch-based helper with
  `const int intLen = thirdIntervalEnd - thirdIntervalStart + 1;`, an age 8–10 branch with
  tail coefficient 4, plus the later `GetHumanAge_New` zero-origin loop and the
  `Interval` / `Config` record structs.
- `examples/tests_example/code/code.csproj`: minimal; `Directory.Build.props`:
  `net9.0`, `ImplicitUsings`, `Nullable`.
- `All.sln` (repository root) contains the `tests_example\code` and `tests_example\tests`
  entries.

At `e4b890a7…a1`:

- `examples/tests_example/tests/Tests.cs`: `public sealed class Tests` with `[Theory]`, the
  nine `[InlineData]` rows `(0,0) (1,15) (2,23) (3,26) (4,29) (6,35) (7,38) (8,40) (15,54)`,
  and `Assert.Equal(expected, actual)`.
- `examples/tests_example/tests/tests.csproj`: no `PropertyGroup` and no `OutputType`;
  package references only — Microsoft.NET.Test.Sdk 17.10.0, xunit 2.9.0,
  xunit.runner.visualstudio 2.8.2 (with IncludeAssets/PrivateAssets), coverlet.collector
  6.0.2. The note discloses that its sample omits the runner asset metadata and the
  coverage collector.

## Reusable pattern

When a managed note needs to describe code the video shows but the repository does not
commit, pin the nearest committed state, quote it verbatim, and label it as a *related*
state rather than the shown state. This kept the note auditable and is why the verifier
could approve everything except one identifier.
