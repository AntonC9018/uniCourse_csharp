# Verifier outcome and what the record leaves open

## Verifier and fixer outcome

- `factual_verification` (run 201, session `ses_f011f16fbffepriwnMBs4FyAwl`) checked the note and
  ledger against evidence and returned verdict **approved with no findings**, with explicit coverage
  of TP-001 through TP-131 (all 131 points). Its verification report names commit
  `c687bea2350d00b1da4884b774a42da6a962822d` as the verified commit — that commit is what pins the
  exact note/ledger state that was judged.
- `factual_fixing` recorded that a clean verdict needs no fixing, i.e. no corrections were applied
  to the note after verification.
- Spot checks the verifier ran and confirmed, worth keeping because they show what the evidence
  actually supports:
  - the lab quote "exposes its backing field publicly" — true at the pinned commit
    (`public string? _nameInclude;`, `Debug.Assert(value != "")`);
  - TP-034's `required`-on-private claim — consistent with the compiler rule and the recording;
  - TP-122's ambiguous field/parameter resolution — the note corrects it, see
    `recording-wording-corrected.md`;
  - simplified code blocks are labelled, so they are not presented as committed snapshots.

## Open items (unresolved)

- **TP-130 address "88000" vs the transcript.** The ledger records the object address as 88000; the
  captured transcript text contains only 8000. The note hedges it ("the later spoken address label
  '88000' does not establish another allocation") but the discrepancy between ledger and transcript
  was never resolved by anyone in the record. Confirm against the audio/frames before reusing the
  number.
- **TP-120/TP-121 "three parameters" vs the note's "two declared parameters".** The verifier judged
  the note "arguably a correction" and accepted it because the code is labelled a simplified
  reconstruction. The ledger text was not fixed, so the two records still disagree in wording.
- **Evidence types available are narrow.** 130 of 131 ledger points rest on `transcript_range`
  evidence and one on a repository blob; no frames, no execution results, no cross-repo sources were
  consulted by any worker on this video (the recorded tool activity is repository reads and git
  history only — no web sources). Claims that would need a captured frame or a run (for example the
  assertion failure behaviour at 04:11) are recorded as "the recording demonstrates its failure
  behavior rather than a universal exception guarantee".
- **No committed combat source.** The video's `Stat`/`Health` code exists only inside the recording.
  Any future note regeneration must keep the "simplified reconstruction" framing or find a source
  outside this repository; nothing in the repo can upgrade those code blocks to committed snapshots.

## Deliberately not captured

- Worker prompts, model/engine identifiers, token counts, run statuses and retries.
- Earlier `english_authoring` / `factual_verification` / `factual_fixing` work orders for this video
  that were superseded before producing a note: they have no worker runs and no recorded session
  transcript, so they carry no research to mine.
