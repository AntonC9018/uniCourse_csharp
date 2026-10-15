# Open items and unresolved uncertainty

- **No verification run exists after the fix.** A fixer result never carries a verdict and
  the fixed head waits for human review, so run 183's correction of F-001 has no
  independent post-fix verification record. The corrected snippet's agreement with
  `b333fe75bfe90c7d2c965c1a57f477497887e5e2:examples/tests_example/code/Helper.cs` was
  checked by the fixer itself and by this recreation, not by a fresh verifier pass.
  Unresolved: whether a re-verification was ever queued is not in the record.
- **Who committed the fix is not recorded.** Run 183 could not commit (read-only
  `.git/index.lock`); commit `7d858a22bbd351d858d1f563ee0410f38b122271` is the first commit
  containing the change, authored by the maintenance pipeline a day later. The gap between
  the worker's edit and that commit is not covered by any session transcript.
- **The ledger entry TP-212 still contradicts the note and the arithmetic.** It states
  "exactly one expectation fail"; the correct answer is that both age 8 and age 15 fail when
  the tail coefficient becomes 8. The ledger was not regenerated, so the ledger and the note
  disagree on this point. Run 182 accepted the note. Unresolved: should the ledger be
  re-derived, or is a transcript-derived ledger allowed to disagree with a verified note?
- **Remote behaviour of overlong revisions is unverified for any real link.** Every link in
  the note is 40 characters, so the "overlong SHAs 404" premise was never tested against a
  link that actually appears in the document. Run 163's only successful check was a `curl`
  on a URL it had constructed itself; its GitHub API `webfetch` ended in `error`.
- **The commit named in the verifier prompt was itself overlong.** "Verify the English note
  at exact commit b333fe75bfe90c7d2c965c1a57f477497887e5e21" is 41 characters and resolves
  to `b333fe75bfe90c7d2c965c1a57f477497887e5e2`. The prompt also told the model to copy the
  canonical 40-character value obtained via `git rev-parse`. This tension is probably what
  produced the hallucinated-citation findings, and it is worth removing at the source.
- **The `factual_fixing` work order at 2026-10-01T22:42:42 has no stored run** in the
  manifest or the database, and the two superseded `factual_verification` orders at
  2026-10-02T11:21:44 and 2026-10-02T21:50:51 are recorded only through their malformed
  runs. Their earlier findings, if any, cannot be recovered from the record — nothing here
  should be attributed to them.
