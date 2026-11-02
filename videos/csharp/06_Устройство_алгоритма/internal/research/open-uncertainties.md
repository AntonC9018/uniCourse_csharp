# Open uncertainties and unresolved items

- Ledger TP-092 still carries the impossible wording. The entry reads "…so 5 plus 5
  gives 10 and then 10 plus 4 gives 19 for the following step", although the recording
  says nine and both verification passes now treat 9 as correct: run 46 raised it as a
  note error and run 83 judged the note right and the ledger garbled. No work order in
  the record rewrites ledger text, so the audited ledger and the note disagree on a
  literal fact. Status: unresolved. Either the ledger sentence needs correcting or the
  note should keep an explicit marker that this entry is a known garble. Recorded by
  re-reading `internal/teaching-points.json` on 2026-10-11.
- Listing-padding wording (TP-062, 947839–957910 ms). The note says the recording
  "adjusts the spacing around the first and last cells" so the end of the listing is
  easier to distinguish. The transcript in that window is truncated mid-sentence —
  "У первой ячейки отступ … но у последней ячейки будет отступ потому что чтобы доб…" —
  and cannot prove an indent before the first cell, only that the last cell gets extra
  offset. Run 83 accepted the note's coverage of TP-062 without raising a finding.
  Re-checked 2026-10-11 against `internal/transcript.md`.
- "The example's result is eleven" (note section "Drawing an algorithm as memory cells";
  TP-123, 1819070–1842269 ms). The transcript literally reads "ответ будет здесь 11",
  but the lecturer never states the values of a and b, so 11 is an illustrative result
  rather than a computed one. Run 83 confirmed the literal reading. Low risk; the note
  presents it as the example's result, not as an arithmetic derivation.
- Ledger localization gap: 19 of the 126 teaching-point entries (TP-044 … TP-063, the
  file-splitting and array sections) still hold Russian text while the rest are English.
  The note is English throughout and was approved as covering every entry, so this is a
  ledger artifact gap rather than a note defect. Re-checked 2026-10-11 against
  `internal/teaching-points.json`.
- No external evidence exists in the record: runs 46, 47, 82 and 83 have no
  `web_query` or `opened_url` tool events, and the note cites only YouTube timestamps
  and the two relative lab links. Nothing in these files comes from web research; the
  re-checks above are local transcript/ledger/git reads.
