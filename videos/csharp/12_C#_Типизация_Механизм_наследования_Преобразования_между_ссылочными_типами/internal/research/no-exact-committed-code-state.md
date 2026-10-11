# No commit holds the lesson's code states

Nothing in the repository is an exact source for the successive classroom programs, so the note
cites video timestamps instead of repository blobs.

- `english_authoring` run 231 (codex, session `01a1032f-3b9e-7cf1-8921-aae71f223b64`) inspected
  git history for this folder's `notes.en.md` and `internal/transcript.md`, and for the candidate
  example `examples/property_dict/Subclassing.cs` (whose only relevant commit is `d5af1fb` "Clean
  up examples"). Nothing was adopted as a citation for a taught code state.
- It read the transcript at its acquisition commit `8942c62400146e6b483dd17587b0671271527cde`
  ("Acquire course transcripts") via `git_read_file` and recorded that revision as **spoken
  evidence only** — "A transcript-acquisition revision is not substituted for taught code history."
- Result: `notes.en.md` states "No exact committed program for these successive classroom states
  was located in the inspected checkout", marks every snippet a **simplified reconstruction** of
  its linked video state, and ends with a `## Code history trail` of video timestamps (newest
  last), not commits.
- `factual_verification` run 236 (session `ses_ee91effecffeUy9bFs8k3GeASF`) corroborated that the
  verified commit's tree contains no executable for the lesson; the structured citation checks
  stayed empty because the note intentionally carries no repository blob citations.

Practical consequence: for this video there is no commit-pinned URL to give for any code state.
If one is ever needed, it must be labelled "related" or "final", never "exact".
