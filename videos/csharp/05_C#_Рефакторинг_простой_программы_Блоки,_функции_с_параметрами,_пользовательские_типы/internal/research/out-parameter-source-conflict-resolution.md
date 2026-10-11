# The `out`-parameter claim was a source conflict, resolved by ledger correction

One idea: the note once disagreed with the teaching-point ledger about how the
second `Distance` call delivers its result; the record shows how the conflict
was raised, ruled on, and folded into the ledger.

- Ledger text before the correction claimed the function "writes its computed
  distance into the out parameter it was given", that "only the value assigned
  to the out parameter survives for the caller", and that the run prints "both
  X and Y values, 3 and 5, confirming the out parameter received the computed
  value" (TP-098, TP-099, TP-100 pre-correction text).
- The 2026-10-02 note (authored in english_authoring run 173, codex
  gpt-6.1-sol, session 01a0fda3-fe08-73e1-9548-03d5b3ba657d) instead states the
  transcript establishes no `out` signature and that 3 and 5 are two separate
  distance results returned normally.
- Verifier run 174 (english-cycle factual_verification, opencode
  space-bunny-free, session ses_f024e998bffeIkry2pe0QJw8Mf, commit
  11dbee37f672ae487955c05fb6d08e47e078bc74) raised this as a `source_conflict`
  and returned `human_decision_required`, citing ledger entries TP-098/099/100
  plus transcript ranges 1326360–1345000 ms and 1352000–1369900 ms. It declined
  to invent a correction because both sides rested on the same transcript
  ranges.
- Resolution: operator ledger corrections ("kind":"ledger-corrections")
  rewrote TP-096, TP-098, TP-099, TP-100 to the return-value reading, recorded
  against english_authoring (completed 2026-10-03T19:24:41, run 228, codex
  gpt-6.1-sol, session 01a1032d-f616-7bb1-98d1-4ee452610c36) with source author
  wo-f515a884 (the 2026-10-02 authoring order).
- Supporting record check (2026-10-11, this checkout): `internal/transcript.md`
  contains no mention of an out parameter (no matches for "out", "аут", or
  "выходной параметр"), so the corrected reading stands.
- The final note also adds the definitional boundary: "`out` is the separate
  parameter mode in which the callee supplies an output through a parameter;
  that mode is not being demonstrated here."
