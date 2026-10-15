# No on-screen code is ever built or run; every sample is a reconstruction

- Scope boundary at 25:15 (t=1515s): "Code serves as a demonstration of the derived
  steps. There is no project build or recorded execution test." The lesson outline ends
  at translating the derived steps into code; a complete file-processing program is
  explicitly left out.
- Consequence for evidence: this folder has no `execution_result` evidence and none can
  be produced. The note states this both in its scope list ("Code serves as a
  demonstration … no project build or recorded execution test") and in its code trail.
- The verifier of the regenerated note (factual_verification, run 83, opencode session
  `ses_f07cd50a2ffeRzzArvfZsiGAba`) accepted the `goto`-based C# traversal snippet
  (`goto CheckIndex;` … `Finished: ;`) on exactly that basis: it is syntactically valid
  C# but a declared simplified reconstruction, so it carries no execution claim. The
  explanation of labels, `+=`, `values.Length` and the empty statement is presented as
  reading the reconstruction, not as a recorded run.
- Citation consequence: the note's "Recorded code trail" lists video states by timestamp
  (00:06:04 through 00:26:23) instead of repository commits, and says so explicitly:
  "The nearby samples above are simplified reconstructions; they do not claim an exact
  commit-pinned repository version." No inspected commit in this repository contains any
  of the on-screen program text, so a commit-pinned code trail would have been padding.
  Same decision as the sibling folder `07_Реализация_алгоритма_из_прошлого_видео_Массивы_Циклы`.
- Reusable citation pin: `internal/transcript.md` entered the repository in commit
  `c1fc3e3` and has not changed since. Re-checked 2026-10-11 in this checkout:
  `git log -- internal/transcript.md` returns only `c1fc3e3`; sha256 of
  `internal/transcript.md` is `b7332a91ead2920ec9b209110cacfa3c3ef08394a5736f0868e893d71ca9b2aa`
  and of `internal/transcript.vtt` is
  `d30951993fada094948cf201a47c663fab21fa1a6fc35a146c7e0b32291eb949`, which is the value
  the ledger records as `transcriptSha256`.
