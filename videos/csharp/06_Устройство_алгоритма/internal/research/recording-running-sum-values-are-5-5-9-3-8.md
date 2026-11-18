# Running-sum example: the recorded values are 5, 5, 9, 3, 8 (total 30)

Recorded speech, `internal/transcript.md` (unchanged since commit `c1fc3e3` "Acquire
course transcripts"; ledger `transcriptSha256` `d30951993fada094948cf201a47c663fab21fa1a6fc35a146c7e0b32291eb949`):

- 22:34–22:38 (t=1354s): "добавляем следующий элемент то есть 5 … 5 это будет 10" —
  the first two values are 5 and 5, giving 10.
- 23:02–23:06 (t=1382.8–1385.7s): "добавляем к этому дев получается 19" — the value
  added next is spoken "дев" (nine), and 10 + 9 = 19.
- 23:23–23:26 (t=1403.7s): "19 + 3 - это будет 22. 22 + 8 - это будет 30" — the
  remaining values are 3 and 8 and the recording states the final sum is 30.

So the drawn node values are 5, 5, 9, 3, 8 and the total is 30. The recording never
says "ten plus four".

ASR hazard that produced the error: "дев" is a clipped "девять". Read as a digit it is
easily rendered as 4, which makes the step arithmetically impossible (10 + 4 = 14) and
contradicts the note's own continuation (19 + 3 = 22, 22 + 8 = 30).

How the record handled it:

- `factual_verification`, run 46, opencode session `ses_f092fc46affecGAcYVE1HgIHq3`,
  finding F-001 (`false_claim`, ledger entry TP-092): the note at commit `1d3c1d4` said
  "5 plus 5 gives 10, and then 10 plus 4 gives 19 for the following step". Evidence
  cited: transcript 1341110–1360259 ms, video timestamp t=1385s
  (<https://www.youtube.com/watch?v=C6plSGSYuyc&t=1385s>), transcript 1390880–1404159 ms.
  Suggested fix: "then 10 plus 9 gives 19".
- `factual_fixing`, run 47, opencode session `ses_f092c0fddffe3zUovPuIN8GK41`: applied
  that single-line edit to `notes.en.md`.
- The same run's finding F-002 (`false_claim`, TP-087) covered the illustrative base-sum
  block "0 + 7 + 5 + 4 + 3 + 8" ending at 27, which contradicted both the recording and
  the note's own later paragraph; it was realigned to
  `0 + 5 + 5 + 9 + 3 + 8`, steps 0 → 5 → 10 → 19 → 22 → 30, and labelled a simplified
  sketch of the base-sum walkthrough.

The regenerated note (english_authoring, run 82, codex session
`01a0f822-b255-75c3-b3e0-5c00c37a60cc`, committed as `7b9b21f`) did not copy the ledger
sentence; it re-derived the sequence from the speech and states the disambiguation in
the text: "The saved transcript then identifies the following value as nine, producing
nineteen at [00:23:02]. Use nine at this step: ten plus four would be fourteen, not
nineteen.", followed by

```text
elements:       5, 5, 9, 3, 8
running sums:   0 -> 5 -> 10 -> 19 -> 22 -> 30
```

The verifier of that commit (factual_verification, run 83, opencode session
`ses_f07cd50a2ffeRzzArvfZsiGAba`) re-read 1320–1392 s, concluded the note is correct,
called the ledger's "10 plus 4" the garbled side, and approved with no findings.

Re-checked 2026-10-11 against `internal/transcript.md` in this folder: the quoted
segments are present verbatim. The current `notes.en.md` sha256 `c7817ad3…` equals the
output hash that the english_authoring order recorded for run 82, so nothing later
edited this passage.
