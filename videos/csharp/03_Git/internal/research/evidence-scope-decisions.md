# Why the note cites video states instead of repository blobs

- No commit in the inspected checkout contains the demonstration project. The
  recording builds a small C++ program (`a.cpp`, `main.exe`, `other.exe`,
  `d.txt`), while the repository's `examples/` holds only C# projects
  (`builder`, `dependencies`, `field_masks`, `lab5_example`, `pipeline`,
  `pipeline_library`, `property_dict`, `tests_example`, …). The verifier
  confirmed this by listing `examples/` and found no C++ Git demo project.
- Consequence, stated in the note itself: the examples "cite stable video states
  rather than claiming an exact repository blob", and the closing
  "Code-history trail" is a chronological list of timestamped video states, not
  of commits. This is a deliberate citation policy, not an omission.
- Commands in the note are shortened reproductions of the recorded procedure.
  Paths, author values, commit messages, and the branch name `experiment` are
  declared illustrative in the note's opening.
- Where the transcript does not establish exact source text, the note uses a
  "behavior sketch" that shows only what the recording establishes: the removal
  of two console-wait lines (the exact C++ statements are unknown), the initial
  contents of `d.txt`, and the squash example. The diagrams for rebase, the pull
  request, and the branch layout are labelled symbolic, with labels described as
  descriptions rather than hashes.
- Language choice worth remembering: the video is C++ (`g++`) inside a C#
  course, and the note says so explicitly instead of translating the demo into
  C#. The vocabulary list therefore defines both Git terms and the
  compiler/executable terms the recording uses.
- Structure decision: 131 ledger points were mapped onto 22 teaching sections,
  each point anchored to the heading of its section, and every timestamp link was
  generated so the displayed `[hh:mm:ss]` and the `t=<seconds>` query match.
  The ledger's coverage array carries exactly `id` + `anchor` (plus `evidence`)
  per point — no extra fields.
- Scope boundaries the note records rather than expanding: cloning is only read
  about, never demonstrated; SSH is mentioned as an alternative that needs setup
  and is not shown; the rebase explanation explicitly assumes branches with a
  common commit and notes that the recording mentions the unrelated-history case
  as more complicated without working it through; Git's internal storage is not
  inspected.
- Lab link: the note links `../../../labs/1_basic/03_git.md`, which resolves from
  `videos/csharp/03_Git/` to the repository root's `labs/1_basic/03_git.md`. The
  verifier checked that the lab's tasks match the note's summary of it
  (initialize, ignore generated files, configure author, commit, connect with
  `git remote add` and `git push --set-upstream`).
