# Reusable sources for this video

- `f5e1f7f8460be5474c50b2c3fbda201e4769f8af` — "Update course video t2T88IiiP48". The exact
  English commit both completed orders verified. Its `outputHashes` (recorded in the work-order
  results) match the working tree byte for byte, re-checked 2026-10-11 with `sha256sum`:
  - `internal/teaching-points.json` = `ad112aaba1a86dcc58bca8660c95e549549bfc2895537ce4cdddc009a7f34d8`
  - `notes.en.md` = `b3f43ec4af5e9231af21c7b3c7581748ce23998d3ae7a66067bc89244b8ac088`
- `8942c62400146e6b483dd17587b0671271527cde` — "Acquire course transcripts". Source of
  `internal/transcript.md`, `internal/transcript.vtt`, `internal/video.json`.
- Microsoft Learn pages opened by the author (run 231) and cited in the note:
  <https://learn.microsoft.com/en-us/dotnet/api/system.object.gettype>,
  <https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving>,
  <https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance>,
  <https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions>.
  Checked 2026-10-11 only for existence/identity; contents not re-verified against the note.
- `labs/1_basic/18_interface.md` — the only file outside the folder that names this upload
  ("Видео (динамическая типизация)", line 10). The note does **not** cite it; it is the candidate
  link if a related lesson page is ever wanted. Its adding commit is irrelevant to the taught state.

## Gotcha when locating the folder at a commit

`git ls-tree -r --name-only <commit> | grep -i t2T88IiiP48` returns nothing, because paths under
`videos/csharp/` carry the Russian folder name, never the upload id — and non-ASCII paths are
print-quoted by default. Both facts combined made `factual_verification` run 236 briefly conclude
that the note and transcript did not exist at the verified commit. Grep on the Cyrillic folder
prefix (e.g. `12_C`) or use `git log -- <path>` instead.
