# The one verified factual defect: `length` vs `intLen`

Established by the `factual_verification` work order (run 182, opencode/space-bunny-free,
session `ses_f0163436fffekvtRwrzG13iMxw`), which returned `changes_requested` with exactly
one finding (F-001, `false_claim`, ledger entry TP-135).

- The note's "Add an interval and see the duplication" snippet printed
  `const int length = thirdIntervalEnd - thirdIntervalStart + 1;` and used `length` in the
  age-8–10 branch, then stated "Those identifiers belong to the cited experimental
  function".
- The cited blob is `repository_blob AntonC9018/uniCourse_csharp @
  b333fe75bfe90c7d2c965c1a57f477497887e5e2`, path
  `examples/tests_example/code/Helper.cs`. Its real line is
  `const int intLen = thirdIntervalEnd - thirdIntervalStart + 1;`. `length` does not exist
  in that file, so the "identifiers belong to that function" claim was contradicted by the
  inspected contents.
- The other identifiers in the same snippet — `firstAgeCoef`, `secondAgeCoef`,
  `_3to7intervalCoef`, and the tail coefficient `4` — do match the committed file and were
  left untouched. Only the renamed constant was wrong.
- The `factual_fixing` work order (run 183, session `ses_f0161fb8cffevh6PEeOj7XJx8i`)
  applied the suggested fix: `length` → `intLen` in both the declaration and its single use
  in `notes.en.md`. No surrounding prose was reworded.

## Where the fix landed

- Run 183 reported its own commit attempt failed: `fatal: Unable to create
  '.../uniCourse_csharp/.git/index.lock': Read-only file system`.
- The identical 2-line change is present in commit `7d858a22bbd351d858d1f563ee0410f38b122271`
  ("Correct course video 9DBoMknXbVQ", author `anton-agent-9018`, 2026-10-03 15:51 +0300,
  `Course-Maintenance-Run: 20260929T222206Z-f69d5b84`). The pipeline committed the edit, not
  the worker.
- The note at the verified English commit `6bd23cf30f17df99bc8ed719aa202ce6c36b1026` still
  carried `const int length`; the note at HEAD carries `const int intLen`.

## Everything else in the note survived verification

Run 182 explicitly re-checked and accepted, among others:

- `tests.csproj` at b333fe75 carries `<OutputType>Exe</OutputType>` plus
  `<ProjectReference Include="..\code\code.csproj" />`, matching the note's "committed
  manual test project contains the reference and its then-current executable output
  setting".
- `code.csproj` is minimal; `Directory.Build.props` is `net9.0` / `ImplicitUsings` /
  `Nullable`; the committed final `Tests.cs` still has an unused `System.Diagnostics`
  import and no `Using Include="Xunit"`.
- The final test project's package versions match the sample: Microsoft.NET.Test.Sdk
  17.10.0, xunit 2.9.0, xunit.runner.visualstudio 2.8.2; the sample's stated omissions
  (runner asset metadata, coverage collector) are accurate.
- All 234 ledger entries were reported covered, and every note anchor resolves to a
  heading.
