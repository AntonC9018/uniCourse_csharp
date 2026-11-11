# Claims the final verification actually checked, and against what

Each item below was re-derived from the recording during the verification pass
over the approved English commit; the evidence named is what the check used.

- `a.cpp` is never named in the transcript. The note therefore attributes the
  name to its own samples ("the samples call it `a.cpp`"). Evidence: transcript
  grep for `a cpp` / `A.cpp` / "имя файла" / "назовём" returned only generic
  phrases about `git add` at 00:04:23. Treated as self-referential and
  illustrative, not as a false claim — but the name is the note's, not the
  recording's. Same for `other.exe`, labelled "an illustrative reproduction".
- `main.exe` is supported, but only through an ASR mangling: transcript
  `[00:11:03.040](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=663s)` reads
  "main XC", read as `main.exe`. Worth remembering if the executable name is ever
  questioned.
- `d.txt` is supported: transcript `[00:22:17.760](…t=1337s)` and
  `[00:22:21.350](…t=1341s)` mention "d.tx"/"d.txt", and the note's claim that
  the transcript identifies it as a text file rests on that spelling.
- The size contrast is verbatim from the recording: source 104 bytes, executable
  about 60 KB, transcript
  `[00:10:14.279](https://www.youtube.com/watch?v=fcxFAW1EE_A&t=614s)` and
  `[00:10:17.839](…t=617s)`. The note attributes the added size to included
  library functions and marks that as an explanation of this example rather than
  a universal account — a hedge the recording itself invites.
- Author identity is independent of a GitHub account: TP-028 (361479-372670 ms)
  states the commit email need not match a GitHub account email, and TP-029
  (372680-392070 ms) states the address is entered without an ownership check.
  The note's aside "It does not mean a setting for every user of the computer"
  for `--global` is technically true but not stated in the transcript (TP-030
  covers only that `--global` widens the scope beyond the current repository);
  it was accepted because nothing contradicts it.
- Commit authorship survives pushing under a different login: TP-114 (1843880
  ms) records that the GitHub author differs from the repository owner, and
  TP-115 separates authorization from recorded authorship. Consistent with the
  local author configuration and with the recording.
- `git restore .` semantics depend on whether the deletion was committed.
  Unstaged deletion: the files come back from the staging area, which still holds
  the committed contents (TP-066). After the deletion is staged and committed,
  the same command does not bring them back (TP-068). The note's
  default-behaviour wording matches real Git, and the ledger carries both halves.
- Step order for visiting the older state was checked against the timestamps it
  cites: log and copy the identifier, press `q` to leave the pager, then
  checkout. The ordering claim in the note matches the transcript.
- `git push --set-upstream origin master` is a reconstruction, not a quoted
  command; the recording says the branch has no upstream and that it must be
  associated with the remote branch. The verifier accepted it as a
  representation, not as transcript text.
- Plain `git diff` compares working files with their staged versions; because the
  edit had not been staged, the staged copy still matched the last commit, which
  is why the diff showed the change. The transcript passage the note cites for
  the two-line removal was checked and matches.
- First push fails for a missing upstream (TP-110, 1744799 ms). The pull request
  that later merges `experiment` into `master` yields the transferred commit plus
  an extra merge commit (TP-127, 2079359 ms), and switching back to local
  `master` does not fetch those commits (TP-128, 2101240 ms); a `git pull` is
  required. The squash alternative is discussed but not used.
