# Branch-creation flag case: unsupported claim, its fix, and what is still open

- The pre-regeneration English note asserted a flag *case* at the branch-creation
  step: "The lowercase `-b` is the branch-creation option; the following argument
  names the branch." (`videos/csharp/03_Git/notes.en.md` line 337, in the section
  "Create an independent working branch", as of commit
  `d511aecdd37844993606d09814ab9c8e19092a4f`.)
- No evidence established the case. The recorded moment renders the command as
  "Git checkout -B": `internal/transcript.md` lines 788-790,
  `[00:20:01.159](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=1201s)`; ledger
  entry TP-075 (1201159-1213870 ms, kind `fact`) says only "git checkout with a
  branch-creation flag". Verifier evidence: `transcript_range` 1201159-1213870,
  `video_timestamp` 1201s, `ledger_entry` TP-075.
- The correction was to make the prose case-neutral, not to pick a case:
  "A branch-creation flag creates the branch from the current state; the
  following argument names the branch." The code sample beside it
  (`git checkout -b`) was left untouched, so the sample and the prose no longer
  agree about the flag.
- The full-regeneration attempt was stopped precisely because it would have
  overwritten that verified correction in the same section: it would have
  rewritten "Create an independent working branch" while the manual-edit
  contract requires stopping on section overlap. The recorded blocker names the
  change window between `d511aecdd37844993606d09814ab9c8e19092a4f` and
  `fd02d35e911e1bf65715005ccf36d6ffdee82019`.
- The accepted regeneration kept the correction's *spirit* but re-states the
  case: the section is now "Start a separate line of work" and step 2 reads
  "Create and select a branch with `git checkout -b`... The `-b` option requests
  a new branch based on the current state."
- **Unresolved.** The current note therefore carries the same kind of case
  assertion that was removed earlier, at the same transcript moment, and the
  final verifier returned no finding for it. Either the transcript moment is an
  ASR misrendering of `-b`, or the instructor typed `-B`; the record does not
  decide which, and no ledger entry establishes the case.
- Fresh check (not a worker finding), 2026-10-11: the two options differ in
  behaviour — `git checkout -b <new-branch>` creates and checks out the branch
  and fails if it exists, while `-B` does the same but resets an existing branch
  to the start point (https://git-scm.com/docs/git-checkout). If the recording
  really showed `-B`, the note's wording ("requests a new branch based on the
  current state") understates the reset behaviour.
