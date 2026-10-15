# Record contradictions and open items

- **Run 248 attribution:** the manifest and `worker_runs` list run 248 under
  the english_authoring order (`wo-5916fb5a…`), and its structured_result
  carries that workOrderId — but the run's own prompt and result are the
  ENGLISH AUTHOR SOURCE CORRECTION round of 2026-10-07. Treat the prompt text
  as authoritative for what the run did.
- **Separate correction-only order:** the database also holds
  `translation-source-wo-5bf68461…` (translation_source_correction), which
  carries only verifier run 249. The manifest does not list it.
- **Shared session:** runs 234 (2026-10-03) and 248 (2026-10-07) share one
  engine session id `01a1033a-6c99-7ca0-8730-2cc554dc9`; the same Codex
  rollout file was continued four days later. Reasoning from the two rounds is
  interleaved in one transcript.
- **Unknown 2026-10-03 verification outcome:** the first factual_verification
  (`wo-daf36ddf…`) and factual_fixing (`wo-a2c5cc10…`) orders were superseded
  on 2026-10-07 with `result` NULL and no stored session transcript. What they
  found is not recoverable from the record.
- **No-op fixing order:** the 2026-10-07 factual_fixing (`wo-0136fc20…`)
  expected writes to both assigned files yet recorded output hashes identical
  to the authoring run's. No content change is attributable to it.
- **Uncommitted translations in the working tree:** `notes.ru.md` and
  `notes.ro.md` exist but are untracked (`??` in git status) — post-translation
  state from the 2026-10-07 translation order that was never committed in this
  checkout. The verified English state remains commit
  `accd55e6a1d8349548cd48e7a1768ecf4dfaf94f`.
- **Truncated verifier dump:** ~8,176 characters of run 237's reasoning are
  clipped between the "Space, decimal 32" discussion and the final verdict;
  see `verification-round-2026-10-07.md`.
