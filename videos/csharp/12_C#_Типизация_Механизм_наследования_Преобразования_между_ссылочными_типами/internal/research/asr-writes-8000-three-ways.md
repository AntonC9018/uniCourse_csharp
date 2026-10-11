# ASR writes the same address three ways

The recording uses dot thousands separators consistently (`4.000`, `5.000`, `6.000`, `9.000`,
`10.000`, `99.000`). For the one address of `B`'s type description the automatic transcript
renders three different spellings, only one of which is literal:

| transcript moment | ASR text | reading |
| --- | --- | --- |
| 00:23:48.919 | "знает что у типа для б будет адрес 88.000" | **88.000** — artifact |
| 00:25:35.679 | "тип по адресу 8.000" | 8.000 = 8000 |
| 00:29:32.240 | "проверяем на равенство 8000 ра 8000" | 8000 (plain) |

Both workers resolved `88.000` as a mis-transcription of `8000`, not the literal 88000:

- `factual_verification` run 236 (opencode, session `ses_ee91effecffeUy9bFs8k3GeASF`) cross-checked
  00:25:35 and 00:29:32 and concluded "note reading 8000 is consistent with 1535 (`тип по адресу
  8.000`) and 1772", adding that 88000 is implausible in a drawing whose other addresses are
  4000/5000/6000/8000/10000/99000.
- Verdict impact: none — recorded as a transcript ASR issue, not a note finding.

Resolution rule worth reusing: when the ASR gives a number that is out of scale for the drawing,
treat the dot-separated form as the canonical spelling and confirm against a later plain-digit
mention before changing the note. The note's wording "illustrated as `8000` for `B`" (cited
00:23:39) follows this rule.
