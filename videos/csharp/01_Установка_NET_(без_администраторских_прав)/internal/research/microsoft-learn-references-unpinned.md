# Microsoft Learn references — pinning finding raised, never resolved

- `factual_verification` attempt 1 (run 11, session
  `01a0e339-871c-7080-96e8-953a1343b982`) checked the note's illustrative
  Microsoft references against their upstream sources and returned
  `changes_requested` with finding F-001 (`bad_citation`): the note's
  illustrative policy and installer references point at live
  learn.microsoft.com pages, whose source files live in GitHub repositories on
  the **moving `main` branch**, so the citations do not fix the supporting
  documentation state as an immutable reference is supposed to.
- The citation it wanted pinned, and why: the verifier rule is "every code
  reference must be an immutable link when an immutable source exists, and the
  pinned commit must contain the cited state". Upstream sources located by the
  run (from its tool calls):
  - `MicrosoftDocs/PowerShell-Docs` →
    `reference/7.6/Microsoft.PowerShell.Security/Set-ExecutionPolicy.md`
    (source of the `Set-ExecutionPolicy` page cited beside the execution-policy
    example)
  - `dotnet/docs` → `docs/core/tools/dotnet-install-script.md` (source of the
    `dotnet-install.ps1` page cited for the installer command, `-Channel`, and
    the session-PATH behavior)
  - `dotnet/docs` → `docs/core/install/windows.md` (source of the Windows
    guidance cited for setting `PATH` and `DOTNET_ROOT`)
  - To pin a revision it queried the commits API per path, e.g.
    `https://api.github.com/repos/MicrosoftDocs/PowerShell-Docs/commits?path=reference/7.6/Microsoft.PowerShell.Security/Set-ExecutionPolicy.md&per_page=1`
- The finding did not survive into the record: run 11's result was rejected as
  malformed (its first attempt used prose evidence strings), and the final
  verifier (run 13, `ses_f19d0173effec2d4zcZefpe0C0`) approved with zero
  findings without addressing it. So F-001 is neither fixed nor refuted —
  **open uncertainty**.
- Current state (checked 2026-10-11): `notes.en.md` still cites the live Learn
  pages — set-executionpolicy, dotnet/core/tools/dotnet-install-script,
  dotnet/core/install/windows, powershell/scripting/samples/managing-current-location,
  dotnet/standard/frameworks — and `dotnet.microsoft.com/platform/support/policy`.
  No commit-pinned upstream revision exists anywhere in the note.
- Mitigating context (from the author, `english_authoring` run 10, session
  `01a0e32e-78a0-7a23-bd75-3a67186ca95a`): every one of these references
  accompanies a command the transcript does not preserve, and each is labeled
  as an illustrative example beside the recorded moment, not a transcription.
  The approved verifier accepted the live links on that basis.
- Source links worth reusing (all live as of 2026-10-11):
  - https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.security/set-executionpolicy
  - https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-install-script
  - https://learn.microsoft.com/en-us/dotnet/core/install/windows
  - https://learn.microsoft.com/en-us/powershell/scripting/samples/managing-current-location
  - https://github.com/MicrosoftDocs/PowerShell-Docs/blob/main/reference/7.6/Microsoft.PowerShell.Security/Set-ExecutionPolicy.md
  - https://github.com/dotnet/docs/blob/main/docs/core/tools/dotnet-install-script.md
