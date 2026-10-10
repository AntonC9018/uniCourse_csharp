# Open uncertainties and contradictions in the record

- Unresolved: which pipeline layer appended the trailing digits to commit
  strings. The work orders' "exact commit" values were overlong for runs 164
  (…cd7e4), 184 (…46e43) and 188 (…43b5) but canonical for run 191 (d2fe7a0b…),
  and run 188 saw a prompt-rendered note whose SHAs were perturbed the same way.
  The record proves the note file itself never contained the overlong strings
  (two independent scans at two different checkouts), but it does not show where
  the mutation entered.
- Gap: run 164's session has zero file-read tool events, so its F-001…F-005
  findings were composed without ever inspecting notes.en.md on disk; its prompt
  is not stored in the record, so whether it verified against a perturbed
  rendering cannot be settled from the available sources.
- Self-reverted edit: run 184 applied one edit (re-pinning line 526's "open
  interface state" from 9cabfb70… to 9acccee8…), then discovered the real line
  already cites 7001d912… (which does declare IRemapNameService) and stopped; the
  file_write tool event for that attempt is recorded with outcome `error`. Net
  effect: no change, and both closing factual_fixing orders recorded
  "clean factual verdict needs no fixing".
- History is squashed: this folder's git history is only c1fc3e3 "Acquire course
  transcripts" and d2fe7a0 "Update course video z5CUTiubzwU", so the note's
  pre-d2fe7a0 states cannot be inspected here. "The note always had canonical
  40-hex SHAs" rests on the two fixers' scans (at d2fe7a0b… and 8942c624…, same
  blob 1ccaafd4…) plus two approvals — not on full retained history.
- Recorded orders with no stored session: the superseded factual_fixing work
  orders of 2026-10-01 and 2026-10-02T11:21, and the two completed fixing orders
  (wo-42e75e78…, wo-07409bcb…) that closed with "needs no fixing". Nothing about
  their reasoning is recoverable; nothing contradicts the conclusion above.
- Earlier attempt under different names: run 165's first working hypothesis
  (strip the first character from each hash) was immediately dropped after its
  own measurement showed the file already had 40-hex values — worth remembering
  only as a caution that plausible-looking "fix" patterns here were dead ends.
