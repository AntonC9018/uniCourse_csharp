# The lesson's code exists as exactly three coarse commits

Established by `english_authoring` run 355 (Codex session `01a11cb8-550c-7070-aabc-06ecd5a60dab`,
read `examples/field_masks/Program.cs` history and each blob at revision `e173ad10485c`) and
re-checked by `factual_verification` run 358 (OpenCode session
`ses_ee33c4d5bffeBeVWOxKI5l69zk`, each commit re-resolved with
`git rev-parse --verify --end-of-options '<sha>^{commit}'` and re-read with `git show <sha>:examples/field_masks/Program.cs`).

- The whole worked example is one file, `examples/field_masks/Program.cs`. Nothing about it has
  ever been split into separate files, so the note cannot cite "the printer file" or "the mask
  file"; the live-edit file layout shown on screen is not preserved in any commit.
- That file has exactly three commits in its whole history, so a commit is a bundle of many
  screen edits, not a snapshot of one teaching step. Every code claim in the note therefore has to
  be pinned to a *stage*, and states the recording shows that were never committed must be labelled
  as reconstructions rather than cited.
- Stages, oldest first:

| Commit | Subject | What it establishes for the note |
| --- | --- | --- |
| `ef4630d291e20ffbcf93018cba43a53971d327a2` | `example` | boolean-per-field `ClientFieldMask` record struct; `Print` with a nullable `ClientFieldMask? mask = null` all-fields fallback; `ConfigureFilter`; per-flag `Without`; `Diff` plus `FirstNameEquals`; per-field `AreSet`; the `Might be siblings` and `Either first or last name are different` call sites |
| `80eddb6786130b733401932cadfb35f5c939d8f4` | `iterator over fields` | `enum ClientField { FirstName, LastName, BelongToGroups, Count, FirstField = FirstName }`; `Get` with an `ArgumentException` default branch and a commented `Debug.Fail` alternative; value-returning `Set`; field loops in `AreSet` and `Without` |
| `6b312db7c3e158e27048d8921f426054c4a1b2b0` | `bitwise` | `internal int _value`; `Get`/`Set` on one integer; `FirstName`/`LastName`/`BelongsToGroups` properties delegating to the indexed accessors; bitwise `AreSet` and `Without`; in-source bit tables (`_value = 1 1 0 1`, `v = 1 0 0 0`) |

- Only the third stage carries explicit enum values (`FirstName = 0, LastName = 1, BelongToGroups = 2`);
  the indexed stage relies on implicit values. A sample showing explicit `= 0, = 1, = 2` while citing
  `80eddb6` is therefore a simplification, not the committed text. The verifier examined this and
  accepted it, because the numeric result is identical and the note labels the sample simplified.
- The verifier also accepted the note's `for (field = FirstField; field < Count; field++)` loop where
  the committed `80eddb6` code uses `i != ClientField.Count`. Equivalent only because the enum is
  contiguous and zero-based; keep that assumption in mind if `ClientField` ever gains a gap.
- `Print` takes `ClientFieldMask? mask = null` and resolves absence with `mask is not { } m`, in all
  three stages. There is no committed variant using `default` as an all-fields sentinel; that
  intermediate design exists only in the recording and is marked as a reconstruction.

Not from the worker record, checked on 2026-10-10 against the checkout:

- The three commits are authored by `AntonC9018` on 2025-09-09 (`ef4630d`, `80eddb6`) and
  2025-09-10 (`6b312db`).
- `examples/field_masks/Program.cs` at `HEAD` is byte-identical to `6b312db`
  (`HEAD:examples/field_masks/Program.cs` = `6b312db:examples/field_masks/Program.cs` =
  `beed808eeb589e10005317e426d0daf8fb18530c`), so every defect described in
  `committed-example-defects.md` is still live in the current example.
- The three commits are the commits of PR #1 "Field mask" (branch `field_mask`, merged), which the
  lab file links to as "Поэтапный код из видео (смотрите коммиты)":
  <https://github.com/AntonC9018/uniCourse_csharp/pull/1>. That is why the stages are bundled.
