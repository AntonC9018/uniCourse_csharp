# .NET version recommendation and support dates

- The recording selects SDK **9.0** at 03:00
  (`https://www.youtube.com/watch?v=QsO1HedgKt8&t=180s`); without the
  selection its installer would have chosen .NET 8, which the lesson says was
  also usable. The transcript preserves no exact on-screen command, so the
  authored note represented the moment with the simplified
  `.\dotnet-install.ps1 -Channel 9.0` and said so explicitly ("not preserved
  in a repository snapshot"). Source: approved `notes.en.md` at
  `91d0ec04687ebc8eb1b7883178c9e66ea22d6adf`, `factual_verification` run 13.
- `baa405d56f1d22de913fe0a16c9ddffdde7aabd4` (2026-09-28, "Address course PR
  review feedback", trailer `Course-Maintenance-Run: 20260926T232010Z-e0f9b181`)
  replaced the illustrative command with `.\dotnet-install.ps1 -Channel 10.0`
  and added a dated recommendation to the note: "For a new setup, install the
  .NET 10 SDK. As of September 2026, .NET 10 is the active LTS release,
  supported through November 14, 2028. The same policy lists .NET 9 in
  maintenance support through November 10, 2026; it is not yet out of
  support", citing
  `https://dotnet.microsoft.com/en-us/platform/support/policy`. The same commit
  set the project samples' `TargetFramework` to `net10.0`.
- Fresh check, 2026-10-11, of
  `https://dotnet.microsoft.com/platform/support/policy` (page footer: last
  updated September 8, 2026):
  - .NET 10 — released 2025-11-11, **LTS, Active**, end of support
    **2028-11-14** (matches the note verbatim). Latest patch listed: 10.0.12.
  - .NET 9 — released 2024-11-12, **STS, Maintenance**, end of support
    **2026-11-10** (matches "maintenance support through November 10, 2026").
  - .NET 8 — LTS, Maintenance, end of support **2026-11-10** (the version the
    video's installer would have defaulted to; not mentioned in the note).
- Time bomb for note maintenance: the note's "it is not yet out of support"
  for .NET 9 — and, if the note ever mentions it, .NET 8 — becomes false on
  **2026-11-10**. The claim is dated ("As of September 2026") and must be
  re-checked against the policy page then. The same page (checked 2026-10-11)
  advertises .NET 11 launching at .NET Conf, November 10–13, 2026, which will
  change the "active LTS" row again.
