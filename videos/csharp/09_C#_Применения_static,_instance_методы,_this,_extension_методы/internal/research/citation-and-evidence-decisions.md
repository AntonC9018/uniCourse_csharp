# Citation and evidence decisions

## Every code sample is a reconstruction, not committed source

The author searched the checkout for the combat program and found nothing: the only files at
`4814706ae2fafbc797829239a33afc2eeefa29e8` under this folder are `internal/transcript.md`,
`internal/transcript.vtt`, `internal/video.json`. Confirmed independently during this capture with
`git ls-tree -r … 32fcbff7… | grep -iE 'combat|GameState|Enemy|Stats'` (empty).

Consequence, stated in the note at 00:00:04 and in the `Code-history trail` section: every C# snippet is
"a simplified reconstruction of its linked video state, not an exact committed program," and the
`Code-history trail` is a list of **video-state** links (oldest first), not repository commits. This is
why the section is named "Code-history trail" but contains only YouTube timestamps.

## Two different citation styles, on purpose

- **Transcript → pinned immutable revision.** The note links
  `https://github.com/AntonC9018/uniCourse_csharp/blob/4814706ae2fafbc797829239a33afc2eeefa29e8/videos/csharp/09_%23…/internal/transcript.md`
  (40-character hash, percent-encoded `#`). Needed because the transcript is an acquired artifact that a
  later `acquisition` order can rewrite; without a pin, the note's 141 per-timestamp anchors could drift
  to different cues.
- **Labs → relative repository links.** `../../../labs/1_basic/15_static_class.md` and
  `../../../labs/1_basic/16_method.md` are ordinary relative Markdown links, not pinned. Both files are
  stable in this repository (their only changes are lab renumbering/reordering, all before the note was
  authored), and they are living course content that a reader is expected to follow at the current state.

This asymmetry is defensible but is a **convention the note does not state anywhere**. If a future
convention requires immutable links for every repository reference, these two links are the ones to
change.

## Blob-level traceability

Recorded in `worker_tool_events` for run 190:

| Artifact | Blob / hash recorded |
|---|---|
| `internal/transcript.md` | `2958f6fd145d1d9c03524e95f7b7b34684bcc9826b825ebe69ce1d75feb3860f` |
| `notes.en.md` (draft) | `d6af2b220041b0ab058b7d42217fdba6f9acd0686c8651845bcd733fb83c4746` |
| `teaching-points.json` (draft) | `4f44c7d182eacae727d994de723089a4639ec7e81d1ee1217bd4d0cae7becf53` |

The transcript blob the author read is **not** the blob currently in the working tree
(`f6189ba4200fa7c31600f0aa012abbeb7d803256`), even though `internal/transcript.md` has had exactly one
commit (`4814706`) since acquisition. This is almost certainly a line-ending / normalisation
difference between the recorded acquisition hash and the stored object, not a content change. Nobody
re-verified it, and it is not worth re-litigating — but it means blob hashes in `worker_tool_events`
should not be compared against `git hash-object` of the current file.

## External sources the author opened

Only one web access is recorded in `worker_tool_events` for run 190:

- `https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements`

The author's session shows a single batch `web__run` call opening **four** Microsoft pages
(top-level statements, static classes and static class members, local functions, extension methods).
The event log captured only the first URL. The note cites exactly those four — the sets match
one-for-one:

| Cited page | Where it supports |
|---|---|
| `…/fundamentals/program-structure/top-level-statements` | the `Program`/`Main` sketch and the source-order rule at 00:24:23 and 00:24:53 |
| `…/classes-and-structs/static-classes-and-static-class-members` | the static-class restrictions (no instance fields, not instantiable) at 00:03:33 |
| `…/classes-and-structs/extension-methods` | "these are static methods invoked with instance-style syntax" at 00:23:11 |
| `…/classes-and-structs/local-functions` | "local scope, declarations after return, and the capture restriction on static local functions" at 00:26:51 |

No capture date is recorded for any of these reads, so none of them can be dated from the record. If
these citations are ever challenged, they must be re-checked against live Microsoft Learn rather than
cited to the author's session.

## One read failed, harmlessly

Run 190's first `read_file` of `notes.en.md` returned `outcome: failed` — the note did not yet exist.
The author continued and overwrote the file in the same run. No consequence.
