# The array example's element values are never spoken; the note uses placeholders

- The recording names only the count. At 13:38–13:42 (t=818.9–822.1s):
  "возьмём что у нас четыре значения в этом списке", and at 13:45 (t=825s):
  "ещё один кусочек памяти который указывает сколько значений здесь". No individual
  value is spoken anywhere in that segment.
- The note at commit `1d3c1d4` nevertheless drew concrete cells
  `values: [ 7 ][ 5 ][ 4 ][ 3 ]  length: 4` and folded those invented values into the
  base-sum block (7 + 5 + 4 + 3 + 8 = 27).
- `factual_verification`, run 46, opencode session `ses_f092fc46affecGAcYVE1HgIHq3`,
  finding F-003 (`unsupported_claim`, ledger entry TP-055): the transcript establishes
  only that the example has four values, the specific values are never spoken, and they
  conflict with the node values used later in the same note (5, 5, 9, 3, 8). Evidence:
  transcript 811590–833590 ms and 1390880–1404159 ms.
- `factual_fixing`, run 47, opencode session `ses_f092c0fddffe3zUovPuIN8GK41`: replaced
  both array sketches with placeholders `[ a ][ b ][ c ][ d ]` / `length: 4`, and added
  the caption that the recording names only the count of values. It also removed the
  invented values from the sum block (finding F-002, see
  `recording-running-sum-values-are-5-5-9-3-8.md`).
- The regenerated note (english_authoring run 82, codex session
  `01a0f822-b255-75c3-b3e0-5c00c37a60cc`, commit `7b9b21f`) keeps that discipline: the
  four-value sketch is introduced as "represented below with placeholder values, not an
  exact transcription of the screen", and the last-index sketch used for the bounds
  discussion (15:47–16:00) is drawn for eight values as `[ a ][ b ][ c ][ d ][ e ][ f ]
  [ g ][ h ]` with offsets 0…7. Its verifier (run 83, session
  `ses_f07cd50a2ffeRzzArvfZsiGAba`) accepted both sketches on the grounds that the
  placeholder framing makes no factual claim.

Retained rule: illustrative numbers in a sketch must never be attributable to the
recording. The invented 7/5/4/3 leaked into an arithmetic claim and produced a wrong
total.

Re-checked 2026-10-11 against `internal/transcript.md` in this folder.
