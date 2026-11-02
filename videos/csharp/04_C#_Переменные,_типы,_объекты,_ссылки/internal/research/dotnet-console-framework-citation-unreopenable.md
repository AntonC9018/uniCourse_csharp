# The dotnet framework citation was content-correct but unreopenable

- The authored `notes.en.md` backed its boxing/overload clarification with a pinned
  repository blob: `dotnet/dotnet@b0f34d51fccc69fd334253924abd8d6853fad7aa:src/runtime/src/libraries/System.Console/src/System/Console.cs`,
  with a simplified `public static void WriteLine(int value) { Out.WriteLine(value); }`
  excerpt beside it. Source: `english_authoring` run 171 (session
  `01a0fd99-65c8-7181-9ecd-98a8c0cc72a8`) — the note is at commit
  `9db31a0b9ad0dea10fc004941e96ddccab61e142`.
- The application's own citation check labelled that blob `invalid_reference`
  (source `none`): the trusted inventory could not reopen it. Source: the
  deterministic check quoted in the `factual_verification` prompts for run 172 and run 185.
- Run 172 (`factual_verification`, session `ses_f025e2db2ffevIow8R7HMKpbIO`, run status
  `malformed` / `result_invalid`) verified the blob by hand first: `git ls-remote
  https://github.com/dotnet/dotnet` succeeded and
  `curl -sI raw.githubusercontent.com/dotnet/dotnet/b0f34d51.../...Console.cs` returned
  `HTTP/2 200`; the fetched file contained the quoted overload at lines 779-782.
- Run 185 (`factual_verification`, session `ses_f015fef4bffeNClXQIyVwQ6E79`) reached the
  same content conclusion through the GitHub blob page
  (`https://github.com/dotnet/dotnet/blob/b0f34d51.../src/runtime/src/libraries/System.Console/src/System/Console.cs`)
  and recorded it as "the note's excerpt matches (with attribute lines omitted, described
  as simplified)".
- Both verifiers ended at the same verdict: `changes_requested` with a single
  `bad_citation` finding on `notes.en.md` (F-001). Run 172's answer was rejected as
  `malformed`/`result_invalid`, so only run 185's version survived — its stored
  `structured_result` reads "the trusted inventory reports this reference as
  invalid_reference (unreopenable), so this pinned repository citation cannot be used as
  evidence", with `affectedPaths: ["notes.en.md"]` and the suggested fix to delete the
  paragraph and keep the Microsoft API link.
- Run 186 (`factual_fixing`, session `ses_f015f2952ffeovLpfc1OAQftOU`) applied exactly the
  suggested fix and nothing else: the paragraph plus its fenced `WriteLine(int)` excerpt
  were deleted, and the `Console.WriteLine` integer-overload documentation link in the
  preceding paragraph was left as the sole support. Landing commit: `344a53b`
  ("Correct course video wjsV9CvZfGI"), a 7-line deletion, the only change in that commit.
- Reusable conclusion: a citation the pipeline cannot reopen is unusable as note evidence
  even when its contents are demonstrably correct. Prefer an inventory-reopenable source
  (the Microsoft Learn API page) over a content-correct framework blob.
- Fresh re-check, 2026-10-11 (not a worker finding): the blob still resolves —
  `raw.githubusercontent.com/dotnet/dotnet/b0f34d51.../src/runtime/src/libraries/System.Console/src/System/Console.cs`
  returns HTTP 200 and still contains `public static void WriteLine(int value)` at lines
  779-782. The same commit is absent from `dotnet/runtime` (HTTP 404 at both
  `src/runtime/src/libraries/...` and `src/libraries/...`), while
  `raw.githubusercontent.com/dotnet/runtime/main/src/libraries/System.Console/src/System/Console.cs`
  resolves. So if a framework implementation citation is ever wanted again for this
  clarification, `dotnet/runtime` at a revision that exists in that repository is the
  durable choice, not `dotnet/dotnet`.
