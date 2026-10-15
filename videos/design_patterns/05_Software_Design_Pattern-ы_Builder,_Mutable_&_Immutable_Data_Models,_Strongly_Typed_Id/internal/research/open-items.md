# Open items

## 1. Mis-attributed sentence about default interface implementations — unresolved

- `notes.en.md` carries a standalone sentence, immediately after the
  `01:03:27` paragraph on `ICitizenBuilder` and extensions: "The recording's
  broad claim that C# lacks default interface implementations needs
  qualification: modern C# supports them."
- It has no timestamp anchor, and the recording's words at 3801–3806 s are about
  **Java**: "В JV, наверное, такое не получится, потому что тут применяется идея
  extension методов" — Java lacking extension methods.
- Checked 2026-10-10 against the whole transcript: of the 86 cues that mention
  "интерфейс", none is about default interface implementations or about C#
  lacking them. The only nearby Java remark is the one above.
- The sentence's substantive correction is right in substance: C# interfaces can
  supply implementations for their function members. **Fresh check 2026-10-10**,
  C# language specification §19.1: "The interface itself may provide an
  implementation for some or all of the function members that it declares."
  <https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/interfaces>
  (marked as fresh research, not a worker finding).
- So the wording is probably right about C# but attributes it to the recording
  and to the wrong language. Whether that is an intentional paraphrase or a
  transcription slip is not recoverable from the record: run 488 returned
  `approved` with no findings and did not compare this sentence against the
  transcript.
- **Action if this note is revisited:** either drop the "recording's" attribution
  and state the C# fact directly, or rewrite it as the Java point the speaker
  actually made (Java has default interface methods, not extension methods).

## 2. One history-trail claim was accepted without inspection

- The verifier's own reasoning ends: "Note history trail claims c6d7b18 restore;
  assume fine." That entry was never opened by run 488; only the other five
  pinned commits were read.
- Author run 476 did read `c6d7b18` in full, so the claim is not unsupported —
  but the second, independent check is missing from the record.
- Closed on 2026-10-10 by this recreation: `git log -1 c6d7b18` gives
  "Revert to working builder" (2025-09-28), and `c6d7b18` is the current tip of
  `examples/builder/Program.cs`. The claim holds.

## 3. Ledger point count changed between authoring attempts

- Run 469 (`english_authoring`, session `01a11d9e-0cd8-7923-9817-1516bc896101`,
  interrupted with `rate_limited`) opened with "map all 385 points".
- The committed ledger has 384. The ledger work order completed at
  2026-10-08T18:43:29, before run 469 started at 22:24, so the prompt and the
  artifact disagree by one. Which entry was dropped, and whether it was a
  deliberate merge of two overlapping ledger entries, is not recorded.

## 4. No stored transcript for two failed authoring attempts

- Runs 405 and 469 (`english_authoring`, both `interrupted`, `error_class
  rate_limited`) retain almost nothing. Run 405 died after three seconds and its
  rollout contains no assistant message and no tool call. Run 469's rollout stops
  after five opening tool calls, with no reasoning and no findings.
- The only thing worth keeping: run 469 had already selected the same six pinned
  `examples/builder/Program.cs` commits before dying, which is why the successful
  run 476 used exactly those six.
- Run 493 (`translation`, `timeout`) is outside the author/verifier/fixer scope
  of this folder.

## 5. Evidence the author preserved but did not re-derive

- Run 476 opened with "preserve the key corrections: the name cache stores
  indices, `init` leaves array contents writable, and the scope output
  `18, 32, 32` shows ages." The earlier pass that found those errors is not in
  the retained record — the only prior verification work order for this folder
  was superseded without a run — so the original wrong wording is unknown.
  The corrected forms are recorded in `claims-and-evidence.md` with the evidence
  that supports them today.