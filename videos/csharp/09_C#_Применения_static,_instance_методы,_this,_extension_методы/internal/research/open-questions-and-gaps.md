# Open questions, gaps, and things the record leaves unresolved

## Unresolved: TP-121 and the "captures by default all local variables" wording

The ledger entry TP-121 reads "captures by default all local variables of the enclosing function." The
transcript at t≈1573–1579 has the speaker say that a local function without `static` "будет по
умолчанию [захватывать]" — which sounds like eager capture of everything.

The note does not repeat it. At 00:26:13 it says capture "does not mean the compiler eagerly captures
every local variable whether used or not."

The verifier (`factual_verification`, run 192) considered flagging this and decided not to, reasoning:
"The ledger point is covered … The hedge is technically accurate C#. Acceptable."

**This is the one place where the note and the ledger disagree in substance, and the resolution was the
verifier's judgement call rather than an evidence-based finding.** It is defensible — C# only lifts the
locals a lambda/local function actually references — but the ledger text was never amended to match, so
a future re-verification that reads TP-121 literally could reopen this as a coverage dispute. Marked
unresolved.

The note's external support for the hedged wording is the Microsoft local-functions reference at
00:26:51, which per the author "explains … the capture restriction on static local functions." No dated
capture of that page exists in the record.

## Unresolved: the shadowing diagnostic is not reproducible from the record

The note at 00:26:31 says the recording "shows a diagnostic involving a name already visible from
`Main`" and then carefully adds that "collisions depend on the declarations and scopes involved, so the
demonstrated diagnostic is not a rule that every matching name always produces the same error." The
recording's diagnostic text is not in the transcript (it is on screen), so the note's caveat is the only
auditable version. There is no way to re-derive the exact compiler error from the evidence available.

## The first authoring turn was aborted and produced nothing

Run 190's session records a `turn_aborted` after the first evidence-gathering batch. The author then
restarted, re-read the transcript, re-read the existing note, and re-ran the web research. The batch read
of the transcript ranges t=870–1195 and t=1195–1452 happened in the aborted first turn; the surviving
turn re-read the transcript via `git_read_file` at commit `4814706` but the visible evidence-gathering
window in the surviving turn is narrower. Nothing from the aborted turn was carried forward, and no
work order in this folder's history records what the aborted turn might have concluded.

## Unexplained in the record: three generations of work orders for one note

The manifest records three `english_authoring` / `factual_verification` / `factual_fixing` triples for
this folder (2026-10-01T15:42, 2026-10-02T11:21, 2026-10-02T22:26) before the successful set
(2026-10-02T22:44 → 22:58). Every earlier triple is `superseded` with `result: null`,
`blocker_evidence: null`, and no recorded runs. Only the final `english_authoring` (run 190) and
`factual_verification` (run 192) have sessions.

The manifest offers no reason for the two earlier generations. The final `factual_fixing` order
(`wo-df0895fac84bc982ad605a7d8253bf155371f8bb10e1aa87d1f8b6b8b631833b`) records
"clean factual verdict needs no fixing" and reports the same output hashes as the authoring run —
i.e. it was a no-op. **No factual error was ever found and fixed on this note.** The whole
verify/fix cycle converged on the first generated version.

## Not explored on purpose

- **The worker rollback/restore machinery.** `worker_runs` for this folder shows `correction_used: 0`
  on runs 190 and 192. Translation runs (210, 215, 216, 2026-10-03) belong to the `ru`/`ro` round, not
  to the English author/verify/fix cycle this folder's research is about; their translation-fidelity
  findings are wording-level naturalness fixes and are recorded in the translation worker, not here.
- **`retained_reports`.** Queried for `UVLUbfcCcG0`; every hit is a batch `saved-plan` whose payload only
  restates this video as classified `new`. Nothing folder-specific about the research.
- **Frames and scene assets.** Never existed for this folder (see `citation-and-evidence-decisions.md`)
  and the author was forbidden from citing them.
