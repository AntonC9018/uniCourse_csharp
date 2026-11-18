# Factual corrections to the recording

All entries were established by the author while writing `notes.en.md`
(english_authoring, run 234, codex/gpt-6.1-sol, session
`01a1033a-6c99-7ca0-8730-2cc554dc9`, work order `wo-5916fb5a…`, 2026-10-03) and
re-checked one by one by the verifier (factual_verification, run 237,
opencode/space-bunny-free, session `ses_ee91effe4ffefYRbO63sb4md8N`, work order
`wo-0931c8d9…`, 2026-10-07), which approved the note at commit
`accd55e6a1d8349548cd48e7a1768ecf4dfaf94f` with zero findings.

- **Number-base slip, TP-040 (transcript 453,190–475,240 ms):** the recording
  says character `e` was stored as the number 45 and then says the dump is
  hexadecimal. `0x45` is 69 and equals uppercase `E`; lowercase `e` is `0x65` =
  101. The note carries an explicit "Number-base correction" at t=453s so the
  45 label is not read as decimal. Verifier confirmed the hexadecimal context
  in the transcript before accepting.
- **ASCII entry count, TP-045 (transcript 521,829–533,630 ms):** the recording
  says the table holds "only the 127 most frequently used characters". ASCII
  has 128 entries numbered 0–127. The note states the correction at t=253s and
  cites Microsoft's `Encoding.ASCII` documentation for the range.
- **Encoding of C# strings and `char`, TP-045 area:** the recording's broad
  wording implies ASCII as the general encoding. C# strings and `char` use
  Unicode UTF-16 in memory; ASCII is only the smaller shared set in these
  examples. Corrected in the same t=253s passage.
- **BOM expansion:** the recording says "byte order Mask". BOM expands to Byte
  Order Mark. Corrected at t=496s, citing Unicode's BOM FAQ
  (https://www.unicode.org/faq/utf_bom.html#BOM); the note also clarifies that
  UTF-8 has no byte-order choice, so there the BOM acts as an encoding
  signature.
- **TP-030 misleading wording (transcript 329,000–338,909 ms):** "printing it
  reveals the code rather than the symbol" is wrong — a `char` assigned `'H'`
  still prints as `H`; exposing 72 requires an integer expression
  (`(int)ch`). Corrected at t=329s.
- **TP-014 cast wording (transcript 127,000–140,000 ms):** the cast keeps the
  same number 72 but in the wider integer type; it does not preserve the same
  storage size. The note says exactly that at t=127s instead of "only changes
  how the value is typed".
- **Word wrap, TP-060 (transcript 740,750–749,949 ms):** the recording says a
  wrapped source line reaches the final string object as-is. Word wrap only
  folds the editor display; it inserts no newline, and an ordinary C# string
  literal cannot contain an unescaped physical newline. Corrected at t=740s
  with the C# interpolation reference linked.

Structural note: `internal/teaching-points.json` preserves the recording's
original claim texts (e.g. TP-040 "stored on disk as the number 45"); only the
note carries the corrections, each flagged inline. Do not retro-edit the ledger
to match the note.
