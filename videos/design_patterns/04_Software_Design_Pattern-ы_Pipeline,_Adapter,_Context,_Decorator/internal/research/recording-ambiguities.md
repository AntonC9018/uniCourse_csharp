# Ambiguous narration that shaped the note's wording

All from `internal/transcript.md` (upload `X4IDL3CmV-A`); each ambiguity is resolved in `notes.en.md` with an explicit sentence rather than by silently picking one reading. Identified by the `english_authoring` runs (475 and 499).

- 00:09:19 (t=559) — while motivating the context, the narration says the function needs "this list"; in that passage the list is the previous-owners history list, not the later step list. The note states which collection is meant at that point.
- 00:10:58 (t=658) — the words "new name" appear during the ownership discussion. The note records that operation-specific inputs stay separate: the ownership step takes an owner, the rename step takes a name.
- 00:23:02 / 00:23:08 (t=1382 / 1388) — "the second owner change" identifies the second occurrence of that action kind (index 2 in the committed five-step default), not the second element of the list. The note says to choose the index from the actual list at that moment.
- 00:24:30 (t=1470) — the recording calls a step's construction-time input a "context" as well. The note keeps two names: step configuration (`Name`, `Owner`) versus the shared execution `Context`, justified by the committed `Introspect()` taking no context argument.
- 00:24:11 (t=1451) — during the printer conversion a copied step is renamed to `PrintOwners` while the discussion also mentions printing the dog. The committed design has both `PrintOwnersPipelineStep` and `PrintDogPipelineStep`; the note names both instead of merging them.
- "Pipeline" itself carries two meanings across the lesson — first the ordered sequence of operations, later the `Pipeline` object that stores and runs the steps. The note defines the second meaning where the class appears.
