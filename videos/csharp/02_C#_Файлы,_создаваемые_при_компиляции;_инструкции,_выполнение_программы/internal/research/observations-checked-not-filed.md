# Verification observations checked and deliberately left unfiled

Source: factual_verification rounds of 2026-10-01 (run 59, session `ses_f09215a79ffeRPGWwqDuiuOYah`) and 2026-10-02 (run 166, session `ses_f0270ab37ffeNEemmbImM0bfVo`).

- `bin/debug` vs `bin/Debug/net9.0`: the note text and ledger TP-010 use lowercase "bin/debug" while the on-screen path is `bin/Debug/net9.0`. Treated as casing shorthand, not a false claim, because the ledger itself carries the lowercase form. The regenerated note resolves the ambiguity in wording: it shows the real path and states the lowercase form is shorthand for it.
- "The three instructions of `Hello`" (t=588s): consistent with the three number prints inside the `Hello` body; the count there is accurate and was left untouched.
- The calls-and-returns rule item "the process walks back out until nothing remains" is not in the ledger but follows from TP-040/TP-041; not filed as unsupported.
- `Console.ReadKey()` appearing in the project lab was checked against `labs/1_basic/02_project.md` in the checkout before being relied on as evidence for the executable-launch points.
- Root `.gitignore` contents (`bin`, `obj`, `/test`) were confirmed by direct `git show` at each pinned revision, so the "related repository example" wording holds for the ignore-file citation.
- Final approval re-verified anchor-level coverage of the trickier points: TP-002 (top-level statements) under `#source-and-project-files`, TP-014 (startup code / scope) under `#startup-code-and-the-lessons-scope`, TP-021 (the delay) under `#making-a-delay-visible`, TP-041 (nested calls) at its anchor — all present in the approved commit with no findings.
