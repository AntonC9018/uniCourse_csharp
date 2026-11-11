# No committed copy of the demonstration program exists

- Both the verifier and the author looked for the recording's demo program in the
  repository and did not find it. The note therefore states it plainly in its lesson
  boundary: "No exact committed copy of the recording's demonstration program was
  identified in the inspected checkout. The samples below are simplified
  reconstructions, cited to their video states; the lab example is explicitly labeled
  related. Output comments are predictions from the shown statements, not claims of a new
  local execution."
- Because no code citation could be immutable, the note's code-history trail
  ("Recorded code history") is built from timestamped video states in construction order
  rather than from commits, and it says so in its opening line.
- Evidence that the search was real, not assumed: run 171 inspected
  `labs/1_basic/04_variables.md` and its `git_history` (recorded in `worker_tool_events`
  for run 171) before concluding the lab was only related practice; run 172 checked
  `git ls-tree 9db31a0b... --name-only labs/1_basic/04_variables.md` and the repository
  remotes.
- The lab is the closest committed analogue and is cited as *related practice*, not as the
  taught state: `labs/1_basic/04_variables.md` at
  `52a1e27639e0b78319180bee2b1aa492667d8488` (AntonC9018/uniCourse_csharp) holds an
  exercise with the identical copy rule, verified by both run 172
  (`git show 52a1e276...:labs/1_basic/04_variables.md`) and run 185:
  `string a = "1"; string b = a; a = "2"; Console.WriteLine(a); // 2` then `Console.WriteLine(b); // 1`
  (lines 168-172, with the Russian answer key at 177-193 explaining that `a = "2"`
  overwrites the reference, not the object).
- Citation-shape decision worth reusing: the note links the lab by repository-relative
  path (`../../../labs/1_basic/04_variables.md`), not by an immutable blob, because the
  commit that added the exercise is irrelevant to the lesson's taught state. The
  immutable blob at `52a1e276` appears only where the note quotes the lab's code as
  related evidence.
- The lab also contains material deliberately outside this lesson's scope — for example
  `string a = 5;` at line 195, "Присваивание `int` к `string`" — which is why the note
  adds "It also includes topics beyond this recording" to the practice paragraph and does
  not treat the lab as a coverage source.
- Reuse note: the lab link resolves from this folder's depth
  (`videos/csharp/<lesson>/` → `labs/1_basic/`), a pattern other lessons can copy.
