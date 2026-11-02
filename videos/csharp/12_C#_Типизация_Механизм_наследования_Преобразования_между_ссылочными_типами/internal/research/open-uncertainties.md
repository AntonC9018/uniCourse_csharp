# Open uncertainties

- **No visual or executable evidence exists.** The write assignment excluded
  `internal/frames/`, `internal/images/`, `internal/scenes/`, and no frame, scene, or animation
  asset was inspected by either worker. The note leans heavily on the recording's memory drawings;
  nothing in the record can reproduce them.
- **Every printed value is transcription, not execution.** No order re-ran any snippet. Values such
  as `Class`, `10`, `11`, `12`, `18`, `20`, `True` rest on the recording and the transcript alone.
- **TP-104 still contradicts the transcript.** The ledger entry is unchanged in this folder. Both
  orders approved the note as-is; nothing repaired the ledger.
- **The fixer produced no recorded reasoning.** `factual_fixing` (`wo-cffe7999…`, completed
  2026-10-07T15:03:00) has **no worker run** in `worker_runs`; its single attempt
  (`work_order_attempts` 180) completed in ~20 s with "clean factual verdict needs no fixing" and
  identical output hashes. Any belief that a fixer re-examined the 4000/99000 or `88.000`
  questions is unsupported — those were settled by the author and the verifier only.
- **Part of the author's reasoning is unrecoverable.** The stored session transcript for run 231
  clips the assistant's note drafts, so the reasoning behind the sections between the quoted
  drafts (notably "Store unrelated instances in `object`", "Run the incompatible cast",
  "Distinguish siblings from their common base", "Reach the same base type by two routes") is not
  in the record. Nothing above was reconstructed from those gaps.
- **Coverage was asserted, then checked.** Run 236 asserted that all 184 anchors map to note
  sections. Re-checked 2026-10-11 against `internal/teaching-points.json` and `notes.en.md` in
  this checkout: all 184 `englishCoverage.anchor` values resolve to one of 20 headings, 20 of them
  distinct. No missed teaching point.
