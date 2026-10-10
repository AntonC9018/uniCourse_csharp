# No committed code source exists for this lesson's taught states

- The English note's code samples are reconstructions of on-screen **video states**, not repository snapshots. Reason: the owned checkout contains no file holding the taught Base/Derived, Animal/Penguin/Sparrow, or SwimModule/FlyModule code. Found by the author (`english_authoring`, run 266, session `01a116fd-9c21-7ef0-83bd-06e4eb196b39`), which read `notes.en.md` (absent), its git history (empty), `internal/transcript.md`, `All.sln`, and the repo tree.
- Corroborating evidence in the artifacts themselves: every one of the 67 ledger entries in `internal/teaching-points.json` (commit `558ca24b582da5e7b7b03429dd3c1f9e51768258`) carries only a `transcript_range` evidence item — zero `repository_blob` entries. The note states this explicitly at the top of its sample section and in the code-history trail: timestamp links identify video states, not commit snapshots.
- Consequences worth remembering for future passes on this video:
  - Do not "fix" the samples by adding commit-pinned blob links; no commit contains the cited state.
  - The trail deliberately avoids padding with an exercise-adding commit that is irrelevant to the taught state.
  - The only repository-relative links in the note are the two lab files (see `sources-and-citations.md`).
- Fresh recheck 2026-10-10: `examples/` in the checkout holds builder, dependencies, dependencies_simplified, field_masks, lab5_example, pipeline, pipeline_library, property_dict, tests_example — none contains the lesson's inheritance/composition sketches, confirming the author's finding still holds.
- The verifier (`factual_verification`, run 268, session `ses_ee5516302ffe93jSw3EOe9MfZC`) ran `git rev-parse` on the verified commit, `git show --stat` on it, and `ls labs/1_basic/`; it raised no citation finding, so no pinned-revision citation was ever required or supplied for this note.
