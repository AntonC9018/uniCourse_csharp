# `int.TryParse` failure: the lesson, the ledger, and the correction

## What the recording actually says

At [00:24:22](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1462s) – [00:24:31](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1471s) the instructor
describes the flaw in the intermediate input helper: entering non-numeric text "по-любому вернётся значение ноль, и программа
посчитает, что был введён ноль, хоть на самом деле случилась ошибка" — a failed parse produces **zero**, and the program
mistakes it for a real guess of `0`. `internal/transcript.md` lines 1076–1083 carry the wording.

So the recording is already correct here: the defect is that the failure signal (`false`) is discarded and the out-parameter's
zero is consumed as if it were data.

## The ledger contains a garbled entry for the same seconds

Two ledger extraction runs covered this window independently, from the `ledger` work order `wo-70557c23…`:

| entry | window | text | source run | session |
|---|---|---|---|---|
| TP-118 | 1457480–1476230 ms | "the failed parse yields zero, so the program wrongly treats it as a real guess of zero" | 312 (segment S-020) | `ses_ee50add39ffeKXHwokHCB02BB3` |
| TP-119 | 1468399–1484570 ms | "When `int.TryParse` fails it **still returns the variable's prior value**, so the program treats failed input as a zero instead of reporting an error." | 315 (segment S-021) | `ses_ee50a3d62ffeu5HeR0hXyq20ID` |

TP-119 contradicts the transcript it cites, contradicts TP-118 over the same seconds, and is internally incoherent (it first
claims a prior value survives, then says the value is zero). Both entries are still in
`internal/teaching-points.json`, both marked `covered` at anchor `#a-failed-parse-is-not-a-guess-of-zero`.

## How the note and the verifier handled it

- `english_authoring` run 332 (codex, session `01a11afa-c7c4-7562-b971-c72d954c858b`) wrote the correction, not the ledger's
  wording: `notes.en.md:618` states that failure "returns `false` and writes zero to the `out` variable; it does **not**
  preserve that variable's previous value", citing
  <https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-string-system-int32@)>.
  That URL is the one web source recorded for the run in `worker_tool_events` (event 16 of 20).
- `factual_verification` run 334 (opencode, session `ses_ee4f99f43ffee9A1yKtGuJ0XCg`) noticed the divergence explicitly
  ("the ledger TP-119 says it preserves prior value… the note contradicts it… but that's a deliberate correction with
  evidence"), judged the note factually right, and approved with all 124 ledger ids in `coverage` and zero findings.
- The `factual_fixing` work order `wo-1964b204…` ran no worker session at all and recorded
  `clean factual verdict needs no fixing`, leaving both output hashes byte-identical to the authoring pass. The wrong TP-119
  text therefore still ships in the folder's own evidence file.

## Independent confirmation (fresh check, 2026-10-10)

Not a worker finding. Retrieved the linked page myself: for `TryParse(String, Int32)` the `result` parameter is documented as

> "When this method returns, contains the 32-bit signed integer value equivalent of the number contained in `s`, if the
> conversion succeeded, **or zero if the conversion failed**. … This parameter is passed uninitialized; any value
> originally supplied in `result` will be overwritten."

Source: <https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0> (page `ms.date` 2025-07-01,
fetched 2026-10-10). The note is correct; TP-119 is not.

Nuance worth preserving: "zero on failure" belongs to the `(string, out int)` / `(ReadOnlySpan<char>, out int)` /
`(ReadOnlySpan<byte>, out int)` overloads. The `IFormatProvider` and `INumberBase`/`IParsable` overloads on the same page say
only "an undefined value on failure". The note correctly scopes its statement to `int.TryParse(string, out int)`, the overload
the lesson uses; keep that scoping in any retelling.