# Authoring decisions and citations (run 234, 2026-10-03)

english_authoring, run 234, codex/gpt-6.1-sol, session
`01a1033a-6c99-7ca0-8730-2cc554dc9`, work order `wo-5916fb5a…`.

- **No manual edits:** `notes.en.md` was absent and its git history empty at
  authoring time, so the provenance classification (`manual_edits=false`) was
  consistent; the author wrote a fresh narrative with no conflict to preserve.
- **No committed lesson source for the taught code states:** snippets are
  presented as simplified reconstructions beside linked video-state
  timestamps, and the note's "Code history trail" records stable video-state
  links instead of commit links. Rationale per the citation contract: a commit
  that merely adds an exercise is not evidence for a lesson's installation or
  code states, and the note says so explicitly ("no matching committed lesson
  source was available in the owned checkout").
- **Lab citation:** the [console input lab](../../../labs/1_basic/08_input.md)
  is cited as related earlier practice, read at commit
  `19d6c57ba17ef64ef9c65d22808e2c3f682768c2`, and labeled "a separate lesson,
  not an exercise demonstrated in this recording" — the page identifies the lab
  document, not a taught code state.
- **Stated scope boundaries:** t=223 s — detailed truncation rules deferred;
  t=554 s — internal algorithms of Unicode encodings omitted.
- **Web sources actually fetched during authoring** (recorded as tool calls in
  the session): learn.microsoft.com char and string reference-types pages, the
  C# interpolated-strings token reference, unicode.org BOM FAQ,
  learn.microsoft.com `Encoding.ASCII` and `Console.WriteLine` API pages, and
  the Microsoft Support page on inserting ASCII/Unicode symbols (the Alt-code
  background).
- **Author self-checks before returning:** all 72 coverage anchors resolve to
  note headings; all 115 timestamp links are internally consistent with their
  displayed times; no path under `internal/frames|images|scenes` is cited;
  `<details>` question blocks balanced; the file starts with the content
  heading because the AI disclaimer is app-prepended, not authored.
