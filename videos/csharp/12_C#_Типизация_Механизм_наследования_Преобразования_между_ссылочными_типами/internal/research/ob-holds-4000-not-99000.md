# `ob` holds 4000, not 99000

Ledger entry **TP-104** (`internal/teaching-points.json`, range 1 179 390–1 180 600 ms =
00:19:39.390–00:19:40.600) states that *"the address 99000 is what actually gets stored in the
object variable named ob."* The transcript at that exact range says the opposite:

- 00:19:32.400 — "это об может быть сохранён другой тип не 10.000 А 99.000" (`ob` could hold a
  different type, **not 10000 but 99000** — a hypothetical alternative).
- 00:19:39.080–00:19:40.600 — "в переменной названной об сохранён адрес 4.000" (the address
  **saved in `ob` is 4000**), and the object at 4000 stores its type separately.

So 99000 is a *type-description* address an `object` variable could hold; the drawing's actual
saved reference is **4000**.

Established by `english_authoring` run 231 (codex, session `01a1032f-3b9e-7cf1-8921-aae71f223b64`),
which stated: "The transcript confirms the address correction: `ob` holds the instance reference
`4000`; `99000` describes its concrete type." The note was written from the transcript, not from
the ledger entry.

`factual_verification` run 236 (opencode, session `ses_ee91effecffeUy9bFs8k3GeASF`) independently
re-read 00:19:02–00:19:40 and reached the same reading. It recorded that the note "corrects the
ledger's TP-104", matched the note to the transcript, and raised **no** finding — treating the
ledger/transcript mismatch as a ledger defect rather than a note defect.

Consequence for reuse: TP-104 (and its companion TP-102, range 1 161 690–1 179 390 ms, which
correctly says an `object` variable *can* hold 10000 or 99000) must be read as "these are
type-description addresses"; only TP-104 misattributes 99000 to the saved reference. No order in
this folder repaired the ledger entry itself — the paired `factual_fixing` order
(`wo-cffe7999…`) completed with "clean factual verdict needs no fixing" and changed no file.
