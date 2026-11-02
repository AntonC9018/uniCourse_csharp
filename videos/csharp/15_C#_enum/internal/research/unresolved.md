# Unresolved in the recorded history

## The committed ledger still states the claims the note corrects — UNRESOLVED

`internal/teaching-points.json` at `d8775cfafc393b0ac88349bad484cc4bf8f1737c`
holds 127 audited entries. Several of them carry the instructor's wording
verbatim, while `notes.en.md` corrects that wording in prose at the same anchor.
Concrete disagreements, ledger text vs note text:

| Entry | Ledger text (committed) | What `notes.en.md` says |
| --- | --- | --- |
| TP-025 | "Calling `portrait.ToString()` returns a string and takes the portrait number as its argument." | Built-in `portrait.ToString()` would print `"2"`; the custom function is named `PortraitToString`. |
| TP-035 | "Return must be wrapped in curly braces, otherwise the code fails to compile." | The claim is too broad; a single-statement body is legal C#. |
| TP-046 | "`Debug.Assert` … is stripped out of the final optimized build, so it costs nothing in production." | Conditional on the `DEBUG` symbol, not on optimization; and it does not guarantee a crash. |
| TP-082 | "At run time the enum type does not exist … the enum is purely a compile-time concept in this program." | .NET retains enum type information and member names. |
| TP-115 | "If the first member has no value and the second is explicitly given 5, the second still evaluates to 6." | The member written `= 5` is 5; the next omitted member is 6. |

Further entries the note qualifies without contradicting outright: TP-014,
TP-039, TP-044, TP-052, TP-054, TP-067, TP-074, TP-075–TP-078, TP-092, TP-095,
TP-106, TP-109, TP-110.

Whether the ledger should be re-reconciled, or the note's "corrected" wording is
considered a coverage deviation, is not decided anywhere in the record.

## The verification pass asserted coverage without checking claims — UNRESOLVED

`factual_verification`, work order
`wo-37a22f14967c57c388a5c7705d3b3839e9cdf8f29e7884c7ca3755319e14c845`, worker run
269, session `ses_ee5516310ffeJ9ou2amuXjHoH5`, 2026-10-08: verdict `approved`,
zero findings, coverage `TP-001`…`TP-127`.

Its entire recorded tool use is one bash call resolving the commit and listing
`git status`. The transcript's own reasoning is "The note is thorough.
Deterministic citation checks empty. I should approve." and "The note covers all
ledger entries at claimed anchors. No repository citations (checks empty).
Approve." No claim was compared against the transcript or a documentation source,
which is why the ledger/note disagreements above survived to the committed state.

## No fixer ever ran for this folder

`factual_fixing`, work order
`wo-0057cc308251e6133ccff329d751562e4593ffaecb003dc0cb50b47dcdde8915` (the only
one that reached `completed`, 2026-10-08T08:45:07Z), records the result
"clean factual verdict needs no fixing" and has **zero** rows in
`worker_runs`. The superseded fixing and verification orders likewise have none.
There are therefore no fixer findings, blocker evidence, or fixer sources
anywhere in the record for this video — anything attributed to a "fixer" here
would be invented.

## The recorded source list under-reports what the author read — UNRESOLVED

Run 267 performed two batched documentation fetches covering seven Microsoft
Learn URLs, but `worker_tool_events` recorded only two `source_opened` rows, one
per batch (the first URL of each). The remaining five URLs are recoverable only
from the rollout transcript, not from the event log. Any future reconstruction
that trusts the event log alone will miss five of the seven sources.