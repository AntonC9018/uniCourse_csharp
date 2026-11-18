# The isValid flag's initial value: ledger says false, note says true

- Ledger entry TP-134 (1479550–1484830 ms, `#report-every-violated-rule`) records the teaching point as "introduce an isValid flag initialized to false at the start of the validation function."
- The note instead teaches `bool isValid = true;` and explains why in the same section: starting at false and only ever assigning false in the failure branches would reject every object, including one that satisfies all rules; the final valid-input run announces no violation.
- The transcript cannot settle this on its own: the ASR of the corresponding lines (t≈1479s–1485s) is garbled — "флаг изначально Down Valet в начале функции" ("isValid" mis-transcribed as "Down Valet"). The on-screen code, not the audio, is the only direct evidence, and it is not captured in `internal/`.
- Verifier decision (run 141 / session `ses_f065bf2acffeZV0OzYZjByxp91`, `factual_verification`): the ledger wording is inaccurate, but the note's claim is supported by the recorded behavior, so this is a ledger-text defect, not a note defect; no finding raised, verdict stayed `approved`.
- Traceability: the note's wording is anchored to `#report-every-violated-rule`; evidence inspected was `internal/transcript.md` t=1479s–t=1512s plus the ledger entry TP-134.

## Residual uncertainty (unresolved)

- No frame or committed source shows the actual `isValid = true` statement in the instructor's editor. If a future evidence pass captures that frame, this is the claim it would confirm or refute.
