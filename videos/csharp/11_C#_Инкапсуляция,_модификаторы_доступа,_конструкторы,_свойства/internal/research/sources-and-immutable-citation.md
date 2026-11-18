# Source availability and the one immutable citation

- Finding (`english_authoring`, session `01a0fed9-8b01-76a1-9002-457ce42b9c61`): the combat
  example the video builds (class `Stat`, fields `Attack`/`Health`) has **no committed source in
  this repository**. The checkout was searched (file listing, git history of the note path, the
  labs tree); `labs/1_basic/16_method.md` and `labs/1_basic/17_encapsulation.md` exist, but neither
  contains the combat walkthrough. Consequence recorded in notes.en.md: every code block is labelled
  a "simplified reconstruction of the linked video state … rather than an exact committed snapshot",
  with a YouTube timestamp per state, and a note that the stages are alternatives and successive
  edits, not blocks to paste into one program.
- Decision: cite the **recording** for the code states and keep the lab as the only immutable repo
  citation, explicitly identified as "related evidence", not as the combat source.
- The lab citation was pinned to an immutable revision:
  https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/17_encapsulation.md
  Reason: the authoring worker read the file through the repository-history tool at that exact
  commit, and `factual_verification` (run 201, session `ses_f011f16fbffepriwnMBs4FyAwl`) re-read the
  same blob via `git show 19d6c57…:labs/1_basic/17_encapsulation.md`, so the quoted content is
  checkable byte-for-byte. A moveable `HEAD`-based link could drift away from what the note quotes.
- The ledger carries the same pin: TP-107 is the only teaching point with `repository_blob`
  evidence (commit `19d6c57ba17ef64ef9c65d22808e2c3f682768c2`, path
  `labs/1_basic/17_encapsulation.md`); all other 130 points are backed by `transcript_range`
  evidence only.
- Caveat captured with the citation: the committed lab snippet declares its backing field
  `public string? _nameInclude;` and asserts `value != ""` in the setter. The note quotes it as
  supporting property syntax and warns that the public backing field "should not be copied when the
  aim is preventing bypass of the property".

# Reusable source links

- Video: https://www.youtube.com/watch?v=IESVXL16OBA (playlist
  `PL4sUOB8DjVlVVw9Yx_tUO7fRPDYeaACXD`, index 11). All 152 timestamp links in the note resolve to
  this recording; each displayed time was checked equal to its `t=` parameter.
- Lab (pinned): https://github.com/AntonC9018/uniCourse_csharp/blob/19d6c57ba17ef64ef9c65d22808e2c3f682768c2/labs/1_basic/17_encapsulation.md
- Commit carrying this video's note and ledger: `c687bea2350d00b1da4884b774a42da6a962822d`
  (contains `…/11_C#_…/notes.en.md` and `…/internal/teaching-points.json`).
- Transcript-integrity anchor: `internal/teaching-points.json` records
  `transcriptSha256 = 514250ad7f99819bc29e4c5f2df89305f471d944799b378cc520310429ff9a5e`, which equals
  the sha256 of `internal/transcript.vtt` in this folder — the ledger and the retained captions are
  the same input, so ledger timestamps cannot have drifted onto different audio.
