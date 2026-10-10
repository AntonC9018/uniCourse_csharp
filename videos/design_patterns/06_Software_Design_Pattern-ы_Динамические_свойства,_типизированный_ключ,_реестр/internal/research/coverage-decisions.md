# Coverage and teaching decisions that shaped the note

Author work order `english_authoring`, run 486, session `01a124af-39b4-73c3-bf34-02efbd5f1ca9`;
`factual_verification` run 489, session `ses_edacf1d94ffeQhza95Gx57z9Oh`.

## "Related code" labelling convention

- Every repository example the note cites is a *different saved example* from the on-screen state, not a
  copy of it. The note therefore labels each citation **related code** and states the distinction in its
  Scope paragraph: "inspected repository versions are explicitly labeled **related code**, because they are
  different saved examples rather than exact copies of every on-screen state."
- Consequence for maintainers: a reader must not treat the pinned blobs as "the code from the video". The
  reconstructions in the note are the video's states; the blobs are the nearest saved analogues.

## Four on-screen stages have no saved repository counterpart

Run 486 searched the history of every candidate file and recorded, in the note, that no exact saved commit
was located for these stages. They are written as reconstructions:

- the parameterless `Execute()` greeting step (`notes.en.md`, 00:16:32 paragraph) — the closest saved
  analogue at `a386c9c8…` keeps an explicit context and puts configuration in step fields;
- the generic-variance stage (00:40:06 section) — `d5af1fb…/Subclassing.cs` shows the interface idea with
  no `in` modifier;
- the numeric key registry allocator (01:08:02 section) — no saved numeric-registry state exists;
  `9e0565fa…/Program.cs` only shows the manual `Key<T>` construction that registration replaces;
- the registry introspection implementation (01:13:48 section) — no saved introspection implementation;
  `9e0565fa…/Program.cs` supplies the entries such a loop would inspect.

## Explicit scope boundary

The Scope paragraph (line 33) rules out anything the recording only gestures at: a full game, ECS,
a flyweight pool, atomic key allocation, and specialized per-context registries are named as design
boundaries, not as frameworks the lesson builds.

## Execution results are the recording's, not re-run

The Scope paragraph states that reported compilation and output results come from the recording. No worker
in this folder compiled or executed any sample; every "compiles / fails at runtime" statement is a
transcript-backed claim. Anyone extending the note must not present those as locally reproduced results.

## Coverage map shape

- 396 ledger points were mapped onto 33 note headings as contiguous id blocks, so `TP-001..TP-014` sits under
  `#traversal-and-a-depth-aware-strategy`, `TP-225..TP-237` under `#replace-static-fields-with-dynamic-storage`,
  and so on through `TP-380..TP-396` under `#string-names-can-become-the-keys` /
  `#restrict-construction-to-protect-the-key-contract`.
- The author verified before publishing that every generated anchor resolves to an actual heading in the
  rendered note, that the `HH:MM:SS` label on each link equals the `t=` seconds in its URL, and that each
  code sample sits beside its citation.
- Run 489 independently confirmed that all 396 ledger ids are covered and that the note's headings match the
  anchor list.

## Lab framing

The lab is linked as the related exercise (relative path `../../../labs/2_design/04_properties.md` from the
video folder, verified by run 489 to resolve to the repository root). The note keeps the lab link and the
recorded contradiction side by side rather than dropping the lab or rewriting it.