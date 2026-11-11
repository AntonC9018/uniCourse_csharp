# Garbled numeral captions, resolved from the trace instead

One idea: four places where the video's spoken numerals contradict the traced
execution, and how the note words each one.

- First test output (TP-013, transcript range 191000–217000 ms): the spoken
  "1, 2, 3" counts horizontal contribution 1, vertical contribution 2, total
  3. The program prints the single total `3`, not three coordinate values.
  Note: "The spoken '1, 2, 3' counts the steps and contributions; the program
  prints the single total `3`, not three coordinate values."
- `Math.Abs` on the intermediate −1 (TP-020/TP-021, transcript ranges
  339240–350919 ms and 350920–359710 ms): the note states `Math.Abs(-1)`
  gives 1 — "it does not give 0".
- Y contribution (video 00:31:51, t=1911s): a garbled caption suggesting
  3 − 3 cannot explain the traced result 2; the intended arithmetic is 3 − 1
  (note: "a garbled caption suggesting 3 − 3 cannot explain the traced result
  2").
- Second run (TP-100, 1352000–1369900 ms): the later run prints 3 and 5 — two
  distance results, not X and Y coordinates.

Established by english_authoring run 228 (codex gpt-6.1-sol, session
01a1032d-f616-7bb1-98d1-4ee452610c36), whose readback noted the transcript
"clarifies several garbled captions: the first total is 3, absolute value turns
−1 into 1, and the later run prints distances 3 and 5".

## Unresolved residue (open)

The current ledger text for TP-020 still reads "Passing the intermediate -1
through the absolute-value function turns it back into 0, so the subtraction
cell finally holds 1", and TP-021 reads "Because the function returns 0, the
same result of 1 is what gets written into the x_dist cell" — internally
inconsistent arithmetic that the note explicitly corrects. The 2026-10-03
operator ledger corrections did not touch TP-020/TP-021, and verifier run 235
(opencode space-bunny-free, session ses_ee91f0002ffegBLRSn9DtOinR5) approved
coverage at the anchor without flagging the wording. The ledger wording
contradicts the note; no recorded pass fixes it. Marked unresolved.
