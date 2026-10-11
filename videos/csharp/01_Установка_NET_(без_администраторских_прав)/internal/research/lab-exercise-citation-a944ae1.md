# Lab-exercise citation pinned at a944ae1 — disputed, then verified

- At the approved commit the note cited the related course exercise four times
  as an immutable reference:
  `https://github.com/AntonC9018/uniCourse_csharp/blob/a944ae1fd13128aa386007b0681b6019d2b8b97e/labs/1_basic/01_install.md`
  — in the sections "Construct a minimal project", "Executable project edit",
  "Generate the project from a template", and "Recorded code trail". Each time
  the note explicitly disclaims that the exercise is not an exact snapshot of
  the instructor's on-screen project files.
- `factual_verification` attempt 2 (run 12, session
  `ses_f1bb13c2affe2l9ku7Gg36qjK6`) raised four `bad_citation` findings: the
  citation "cannot be reopened in trusted inventory", so the pinned state
  "cannot be verified", verdict `human_decision_required`. That run was
  instructed not to read `.git`; its result was discarded as malformed
  (`result_invalid`), so its findings never entered the record's verdict.
- The retry prompt changed: attempt 3 (run 13) was explicitly allowed
  read-only Git commands. Run 13 verified the citation locally and approved:
  - `git cat-file -t a944ae1fd13128aa386007b0681b6019d2b8b97e` → `commit`
  - `git show a944ae1…:labs/1_basic/01_install.md` returns the lab
    ("Установка .NET" task list: install .NET, make `dotnet` available, create
    `Program.cs` + `.csproj`, `dotnet new console`, `dotnet run`)
  - `git log --oneline --all -- labs/1_basic/01_install.md` lists `a944ae1`
- Resolution in the repository: `baa405d` (2026-09-28, "Address course PR
  review feedback") dropped the four pinned external links. The current note
  links the lab relatively from the intro
  (`../../../labs/1_basic/01_install.md`) and the "Recorded code trail"
  section keeps only per-moment video links.
- Reusable lesson: "the trusted inventory cannot reopen this blob" is not
  evidence that a citation is broken. Verify with read-only Git
  (`cat-file`/`show`/`log`) before raising a bad-citation finding; the
  verifier prompt itself warns never to infer a broken link when the local
  checkout lacks the commit.
