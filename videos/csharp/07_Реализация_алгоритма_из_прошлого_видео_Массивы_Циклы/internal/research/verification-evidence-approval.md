# What the verifier actually inspected before approving (evidence inventory)

Verdict: approved, zero findings, full coverage of TP-001…TP-100, for the note at
commit bac4adf51ab0a57b4841bb11edc236e8f6fc1687 (factual_verification, run 176,
opencode session ses_f022c5cc5ffemvQYAgHcZGI7y2).

Evidence the verifier inspected directly:

- `git ls-tree` of this folder at bac4adf5 — proves "no committed program source"
  (see no-committed-program-source-and-citation-pins.md).
- `git show bac4adf5:labs/1_basic/05_functions.md` and `:10_basic.md` — the note's
  "related practice with argument copies and result storage" characterization.
- The transcript blob at the pinned commit 4814706ae2fafbc797829239a33afc2eeefa29e8
  — resolved, 40 hex, content matched the claims checked (14→19 slip, `i < length`
  wording, `.length` naming, 700/8000 addresses, declaration order).
- Anchor-by-anchor coverage walk of all 100 ledger entries against note headings.

Non-findings deliberately not raised (recorded so a future reviewer does not
re-litigate them):

- TP-036 (IDE suggestion wording) — accepted despite ledger/transcript ambiguity; see
  open-uncertainty-ide-array-creation-suggestion.md.
- TP-038 ledger says "return type, then static, then the name"; the transcript at
  08:17–08:22 (t=497–502s, "пишем int до этого Статик ... потом идёт имя") puts
  `static` before `int`, which is what the note states. Both describe the same code
  (`static int SumNumbers`), so the wording mismatch is not a factual defect.
- The empty-array sum (no reads, sum stays 0) is derived from the reconstructed guard
  code; no empty-array run is claimed, and the note labels it as a derivation.

Downstream: the factual_fixing pass (2026-10-02T18:17:15Z) recorded "clean factual
verdict needs no fixing" — no worker run, no file changed; the output hashes of
notes.en.md (07a0488c…) and internal/teaching-points.json (f5af809f…) in the fixing
and authoring results are identical.
