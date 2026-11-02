# "Two conditions" vs "two violations" in the validation tests

- Ledger entry TP-139 (1532310–1539430 ms, `#interpret-the-validation-tests-and-limits`) states the test demonstrates inputs 2, 3, 1 "satisfying two of the three postconditions."
- The transcript at t=1532s–1545s says only "я получу два условия" / "я получу только одно условие" ("I get two conditions" / "only one condition") for the runs (2,3,1) and (2,5,4), and "ни одно условие не будет" for valid input. Whether "условие" here means a violated rule or a satisfied one is not decidable from the ASR alone.
- The note resolves the ambiguity toward violations: "Two messages in the first run mean two violations, not two satisfied clauses," with (2,3,1) breaking `B >= 2 * A` and `C > A + B`, and (2,5,4) breaking only `C > A + B`. The reading is consistent with the surrounding narration, where each rule violation prints a message before the accumulator version.
- Verifier decision (run 141 / session `ses_f065bf2acffeZV0OzYZjByxp91`): the note's interpretation is a reasonable reading of ambiguous audio given that rules are violations; accepted without a finding.
- Traceability: anchored at `#interpret-the-validation-tests-and-limits`; evidence inspected `internal/transcript.md` t=1532s–1548s plus TP-139.

## Open point

- The exact lesson sentence is not recoverable from the transcript. If a frame of the demo run is ever captured, use it to decide between "two violated rules" and "two satisfied clauses"; the note currently commits to the former.
