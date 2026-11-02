# Evidence base for lIKqnGtzQrA

Identity of the material the note was written against, and the immutable
repository states that back its code claims.

## Fixed points

- Verified note commit: `a5cf0acb25721ccb13e61bae10940b93342e9bf5`
  ("Update course video lIKqnGtzQrA", 2026-10-10). The verification work-order result embeds a report whose `verifiedCommit` is this SHA
  (`wo-c868791b14e940a86d06c7f5b2ced6250ca074181a716d00a06e7eb9cd560370`, run 488).
- Base revision the author read from: `130a82b36ba18c9c3e359087f5d00737e1cf904f`
  (2026-10-08). Run 476 tool events carry this `git_revision` for recorded file operations. The verification work-order result embeds the verdict report with this verified commit SHA. The current Git object resolves to the note update commit.
- Transcript identity: `transcriptSha256 = 1d51c1de678cb1dd798d856c5dfd4372a2f5b39ea6784402bca9d7f4a360b9f1`
  in `internal/teaching-points.json`, matching the `canonical` hash recorded in
  the work-order `observations` for every work order on this folder.
- Blob ids read by run 476 (`english_authoring`, session
  `01a12266-b500-7681-99df-98e5688a3667`) at that revision:
  `examples/builder/Program.cs` `ec3a3299…`, `examples/builder/pipeline.csproj`
  `0f9911d4…`, `labs/2_design/03_builder.md` `015171c7…`, `AGENTS.md`
  `f62095fa…`, `internal/transcript.md` `5dfb95bf…`, `internal/video.json`
  `40460e39…`.

## Source states the note cites (oldest first)

The six states below were read by the author at their exact 40-character ids.
Five of them were also re-read independently by the verifier (run 488, session
`ses_edacf1d89ffeA31y69e5sP3bnq`); the sixth, `c6d7b18`, the verifier accepted
without opening it. Reuse these links verbatim — they are the only code evidence
in this folder.

| Role in the lesson | Commit | Subject line (`git log -1`) |
| --- | --- | --- |
| working reference-based conversion and scope defaults | `c79a3523fbee1e5fc4916628888cf269c26a0f67` | Builder 1 (2025-09-18) |
| typed IDs and optional lookup, migration unfinished | `dcfd6225dc8e41c7cbdc4e86a79a7bf0b5c1f869` | Broken id code (2025-09-25) |
| one `Model` contract plus shared extensions | `abda83011ba294a41288fa5aa2469ba0b51c1f8d` | Interface for config methods (2025-09-25) |
| concrete configuration methods return the receiver | `1b854ac5ffdf93de5bd0410f39849f7889a1d280` | Fluent builder (2025-09-25) |
| alternative contracts and final-creation roles, unfinished | `ff2cc72527a8a97cf7b756d006ce5d6d8ac9724f` | Abstract builder (2025-09-26) |
| restores the reference-based example; current tip of the file | `c6d7b18cfbd1b0b8607b9434feb38dce0aae681f` | Revert to working builder (2025-09-28) |

- Link shape: `https://github.com/AntonC9018/uniCourse_csharp/blob/<sha>/examples/builder/Program.cs`.
  The note uses these for every code section; a reader must be able to reach the
  exact bytes the claim describes.
- `c6d7b18` is the last commit touching `examples/builder/Program.cs`; nothing in
  the demonstration's file has changed since 2025-09-28. Checked 2026-10-10 with
  `git log -- examples/builder/Program.cs`.
- `examples/builder/pipeline.csproj` targets `net9.0` with `Nullable=enable` and
  `TreatWarningsAsErrors=true`. That is why the committed program opens with
  `#pragma warning disable CS8321 // Local function is declared but never used`
  — the example suppresses a warning rather than restructuring around it, and
  the note's nullability remarks (`Home?`, `!`) are compiler-enforced rather
  than stylistic. Checked 2026-10-10 at `c6d7b18`.
- Companion practice material the author read: `labs/2_design/03_builder.md`
  (last touched by `a944ae1`), linked from the note as
  `../../../labs/2_design/03_builder.md`.

## Output identities recorded for this folder

- `internal/teaching-points.json` published as `80ee9252…`
  (`work_orders.wo-ba7e1987e…` and `wo-9c206a5d…`). The committed file's sha256
  still equals that value, i.e. the full 384-entry ledger is what shipped.
- `notes.en.md` published as `9b56139f…` (same two work orders).
- **Artifact-shape caveat:** what run 476 actually wrote through
  `write_file` was a *coverage map only* — `{"coverage":[{"id","anchor"}]}`, blob
  `efd13b43…` (then re-written twice, `77c40164…`, `62194725…`). The published
  ledger carries `schemaVersion`, `courseVideoId`, `transcriptSha256` and 384
  full `teachingPoints` with `englishCoverage`, none of which run 476 wrote. The
  authoring worker's own report therefore describes a different artifact from the
  one committed. Anyone diffing the two must expect this.
- The verifier and the fixing pass both recorded empty `outputHashes`, i.e. the
  fixing pass changed nothing: `factual_fixing` completed with evidence
  "clean factual verdict needs no fixing" and `fixingRecords: []`.