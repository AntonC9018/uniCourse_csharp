# examples/dependencies_simplified — milestone commit map

The note's 11-entry "Code history trail" (newest last) is exactly
`git log --reverse -- examples/dependencies_simplified` in AntonC9018/uniCourse_csharp.
Established by verifier runs 164/188/191 and both fixers; re-verified 2026-10-10
in the local checkout (mirrors https://github.com/AntonC9018/uniCourse_csharp at
the cited revisions).

| # | Trail label | Commit | Subject | Cited state contents |
|---|---|---|---|---|
| 1 | Template | `5253379090ffec2086654d0a6d1957e0dbd81637` | Template | `public static class Helper { }`, `public sealed class Tests { }`, testing + Verify packages in tests.csproj |
| 2 | Video 1 | `2e88085eed097ef2a1e1345951fa37665caf0756` | Video 1 | Inline processing; Tests.Test1.verified.txt = MARK 40.0, FORK 26.0, LOWER-CASE 40.0 |
| 3 | Added config dependency | `407acfc8643e7a38c4031696e2eaa858541a46e4` | Added config dependency | Extracted nullable item processing with min/max cutoffs, nameRemap, ignoredNames |
| 4 | Config struct | `9968dcccb5b84a1622eb0e2f93b4b2b3de182146` | Config struct | `readonly record struct ProcessItemConfig` groups four settings |
| 5 | Service | `94f768be6e21ab0eb6f9121df54161d291f040cc` | Service | RemapNameService + PriceCutoffService with `AllowsPrice`, config-driven |
| 6 | Process item service | `5280fbba5831ce2b52a5717595e422580537cd7e` | Process item service | Class-level `ProcessItemService` storing PriceCutoffService, IRemapNameService, ignoredNames |
| 7 | Polymorphism | `7001d912d07b50d6516b1206d5b36bd03c54d43b` | Polymorphism | `public interface IRemapNameService` + RemapNameService / RemapNameService_RemovePrefix |
| 8 | Tagged union | `9cabfb706383195f1b3b3dade007d2272c62f899` | Tagged union | `readonly struct RemapNameService_TaggedUnion` with object field + type patterns; Debug.Fail/return null fallback |
| 9 | Restore interface | `9acccee8e1a31518a29a0afc6e52a06bfa2e6d1c` | Restore interface | Consumer back on the interface, union example retained |
| 10 | Service provider | `53502e1527899ae9acf128bbbf3b0388948ec39a` | Service provider | Constructor dependencies separated; Microsoft.Extensions.DependencyInjection 9.0.11 added to tests.csproj |
| 11 | Final lesson state | `d80428c0f252c626af6927783c16b777dc7f1425` | Stuff | Options-style `PriceCutoffConfig` (required floats) registered as an object; processor built through provider wiring |

Two further cited commits come from the lab5_example tree:

- `7f31c6acd0e759a957c51c40ddb9031b1399d17d` ("Use ActivatorUtilities, refactor
  stuff") — cited by the note for
  examples/lab5_example/Orchestration/OrchestrationBuilder.cs, the builder that
  resolves `IOptionsSnapshot<FormatFileOptions>` /
  `IOptionsSnapshot<RandomReaderFactoryOptions>`.
- `306f4dda13a4582f96ba2d76c3c130728a064df4` ("lab 5 done") — cited for
  examples/lab5_example/ReaderWriter/Factories.cs, where the random-reader
  factory reads `_count.Value.GeneratedCount` at reader creation.
- Run 188 inspected OrchestrationBuilder.cs at 306f4dda rather than at the note's
  cited 7f31c6ac; both revisions contain the same IOptionsSnapshot wiring, so the
  note's pin stands.

Reusable link shape (all immutable):
`https://github.com/AntonC9018/uniCourse_csharp/blob/<40-hex>/examples/<path>`.
