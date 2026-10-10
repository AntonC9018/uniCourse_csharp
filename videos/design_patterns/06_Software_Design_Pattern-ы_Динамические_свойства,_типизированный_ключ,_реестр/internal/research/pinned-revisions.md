# Pinned immutable revisions used by this video's note

Work order kind: `english_authoring`, worker run 486, session `01a124af-39b4-73c3-bf34-02efbd5f1ca9`
(the run's only recorded `source_opened` event is the Microsoft `in`-modifier page below).
Contents of every revision below were independently re-opened and confirmed by work order
`factual_verification`, run 489, session `ses_edacf1d94ffeQhza95Gx57z9Oh`.

Immutable link base used by the note: `https://github.com/AntonC9018/uniCourse_csharp/blob/<sha>/<path>`.
All five shas are full 40-character; the verifier confirmed each one resolves and contains the
described state.

| sha | date | subject | file | what the note relies on it for |
|---|---|---|---|---|
| `a386c9c8c77cc4f67311059663399b5b793ed47d` | 2025-10-02 | `Hierarchy` | `examples/pipeline_library/Pipeline.cs` | `IPipelineStep.Execute(Context)` + `Introspect()`, the ordered execution loop, `Context { List<Owner> PreviousOwners; required Dog Dog; }`, and step-field configuration (`ChangeNamePipelineStep.Name`). Cited as the *non-generic* loop only. |
| `ec66732a25273a9f3eaaa73866811e82f43d7853` | 2025-10-02 | `Array of props` | `examples/property_dict/Program.cs` | `object[]` layout mixing a tagged `IntStat { Type = IntStatType.Health }` with typed `ExperienceComponent` / `HealthComponent`, and `Hurricane(List<object[]>)` scanning by `is Position`. |
| `9e0565fa096e0e58867255a6cd1dd38d017a2d2c` | 2025-10-10 | `Shared context` | `examples/property_dict/Program.cs` | `GeneralContext` wrapping `private readonly Dictionary<string, object>`; `Add<T>(Key<T>, T)` with a null-forgiving `value!`; `Get<T>(Key<T>)` doing the cast; `public readonly struct Key<T>` with a *public* constructor; `static readonly Key<Dog> DogKey`; `MaliciousPipelineStep` forging `Key<MaliciousThing>` with the same `"dog"` identity. |
| `d5af1fb372be56a453e3b139990013c28e50198c` | 2025-10-02 | `Clean up examples` | `examples/property_dict/FatStruct.cs` | `sealed class FatStructEntity` with sentinels `EmptyInt = int.MinValue` / `EmptyFloat = float.MinValue`, and `DamageAll` skipping `EmptyInt`. Establishes that the "fat struct" is a `class`, not a C# `struct`. |
| `d5af1fb372be56a453e3b139990013c28e50198c` | 2025-10-02 | `Clean up examples` | `examples/property_dict/Subclassing.cs` | `character is not IMage` capability test, `IMage` / `MageTrait` with `Intelligence`, `WandPower`, `Spirit`. |
| `d5af1fb372be56a453e3b139990013c28e50198c` | 2025-10-02 | `Clean up examples` | `examples/property_dict/PropertyArray.cs` | `readonly object[] Properties`, heterogeneous contents, type-searched access, read-modify-replace. |

## Why a specific revision was needed rather than a branch

- `8384901b589550f7487d5c29fcbeffda9431425c` (`Fat struct`) was read at run 486 and deliberately **not**
  cited. `d5af1fb` is the only revision in which `Subclassing.cs` and `PropertyArray.cs` exist at all, and
  its `FatStruct.cs` is the same sentinel design with the top-level statements wrapped into
  `static class FatStructExample` — the form that makes the sentinel container independently readable as a
  citation. Pinning the earlier commit for one file alone would buy nothing.
- `examples/property_dict/Program.cs` exists in four revisions (`a386c9c8`, `ec66732a`, `8384901b`,
  `9e0565fa`) and takes a different shape in each. Each claim is pinned to the revision that actually
  contains the construct being described, not to the newest one: the `object[]` / tagged-`IntStat` reading
  needs `ec66732a`, while the typed-key `GeneralContext` only exists at `9e0565fa`.
- `examples/pipeline_library/Pipeline.cs` also exists in two revisions (`a386c9c8`, `9e0565fa`). The note
  pins `a386c9c8` for it; `9e0565fa` is cited only for `examples/property_dict/Program.cs`.

## External primary sources the note links

Opened at run 486 (`worker_tool_events.opened_url`, and the rollout's web call):

- `https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/in-generic-modifier`
  — input-only direction of the `in` type parameter and the reference-type restriction.
- `https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const`
  — basis for the `const` correction recorded in `corrected-record-claims.md`.
- `https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/null-safety/nullable-reference-types`
- `https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.add`

## Reproducibility anchors

- Folder commit: `3bd631a245b6cd7cba076435e8fdd84cc6b21a3c` ("Update course video -YG0SKqdL0U") — the only
  commit touching this folder; it carries `notes.en.md`, `internal/teaching-points.json`, the transcript
  and `internal/video.json`.
- `notes.en.md` sha256 `d8f9da58138614c1bbe418153567a5a0fdb0dca082b9e8d8d14e580ba3a6d9ba`.
- `internal/teaching-points.json` sha256 `4155bb7b22cd17393c904e8dcbbeb398cbd10b300c9eca733c3cd4548db0c36b`,
  396 points, `transcriptSha256` `c6330eabdc3a81cd17610768fa14947ab4356d5c5bbc273030619e160de69fd1`.