# Claims the note refuses to make, and why

Established by the `english_authoring` work order (run 158, session
`01a0fc19-11bc-7612-a9fa-541b3373178d`). These omissions are deliberate: a verifier treats a
claim no evidence reopens as an unsupported claim, so inventing the missing detail would
have manufactured a finding.

- **The exact `dotnet` installation command and wildcard spelling for the snapshot step are
  not written down.** "The recording's result establishes that generation worked. The exact
  installation command and wildcard spelling are not preserved in these source commits, so
  this note does not invent them." (note, "Generate a solution with a tool".)
- **The other application's exact schedule test has no owned source commit.** The snapshot
  section presents the JSON → parsed representation → schedule object → JSON pipeline as a
  conceptual reconstruction and says its only source is the video state at
  [00:43:55](https://www.youtube.com/watch?v=9DBoMknXbVQ&t=2635s), "not executable code from
  that application".
- **The `dotnet sln` command sequence is labelled simplified.** The committed `All.sln` is
  described as "related output, not an installation record or an exact capture of the
  generator arguments", so the commands are presented as expressing the demonstrated edits
  rather than as recorded history.
- **Frame, diagram and scene assets are never cited.** The authoring write assignment
  covered only `internal/teaching-points.json` and `notes.en.md`; `internal/frames/`,
  `internal/images/` and `internal/scenes/` were out of scope, so no animation claim in this
  note depends on scene evidence. This is why the note has no unsupported-animation
  exposure, unlike notes that lean on captured frames.
- **The teaching-point ledger is written before the prose.** Run 158 wrote
  `internal/teaching-points.json` first (coverage only: `TP-001…TP-234` mapped to the note's
  29 heading anchors), then drafted the note against it, and finally re-checked that the
  written ledger still had exactly 234 IDs in order. The ledger-as-contract order is what
  made full coverage verifiable.
