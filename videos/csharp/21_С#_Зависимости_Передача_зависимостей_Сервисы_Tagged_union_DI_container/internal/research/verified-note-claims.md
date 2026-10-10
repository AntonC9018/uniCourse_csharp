# Note claims checked and confirmed against cited sources

English note for z5CUTiubzwU at commit d2fe7a0b66de1de959f1068df6f964b764cdb250,
approved by factual_verification runs 188 (wo-914412982f…, session
ses_f0144b586ffeDXBzj3s9ltRGwS) and 191 (wo-4aaa638a…, session
ses_f012bff3bffezlUgesA6cHWpQv). Each item re-verified 2026-10-10 in the local
checkout (same revisions as https://github.com/AntonC9018/uniCourse_csharp).

- Approved output snapshot: Tests.Test1.verified.txt at 2e88085e… contains exactly
  MARK/40.0, FORK/26.0, LOWER-CASE/40.0 — the "approved result" the note shows.
- Price cutoffs are exclusive at both ends: the saved processing rejects
  `price <= minPriceCutoff` and `price >= maxPriceCutoff` (Code.cs at 407acfc8…),
  so cutoff values themselves are excluded.
- Tagged-union fallback: the in-video sample throws, while the saved
  `RemapNameService_TaggedUnion` calls `Debug.Fail("Impossible")` and returns
  null (Code.cs at 9cabfb70…, lines 55–62). The note's wording distinguishes the
  two deliberately.
- The union is a `readonly struct` whose tag is the runtime type used in type
  patterns (`is RemapNameService a`), with no explicit integer tag; the note
  states this explicitly.
- The note's caveat that "impossible state only holds for valid populated
  construction" (null arguments and default(struct) yield no usable variant) is
  consistent with the saved code, which adds no validation.
- Final DI wiring at d80428c0…: `services.AddSingleton(priceCutoffConfig)` (line
  50), `AddSingleton<PriceCutoffService>()`, mapper registered by factory,
  `ActivatorUtilities.CreateInstance<ProcessItemService>` with the ignored-names
  HashSet, and `GetRequiredService<ProcessItemService>()` — matches the note's
  "registrations" and "final test" snippets.
- Options-style config: `public sealed class PriceCutoffConfig` with
  `required float MinPrice/MaxPrice` (Code.cs at d80428c0…, line 135 ff.);
  `PriceCutoffService(PriceCutoffConfig config)` (lines 142–144).
- DI package version: `Microsoft.Extensions.DependencyInjection` 9.0.11 in
  tests.csproj at 53502e15…, as the note claims.
- lab5 IOptionsSnapshot behaviour: OrchestrationBuilder.cs resolves
  `IOptionsSnapshot<FormatFileOptions>` and `<RandomReaderFactoryOptions>` per
  operation; Factories.cs reads `_count.Value.GeneratedCount` when creating the
  random reader; Program.cs creates a fresh `CreateAsyncScope()` per Run.
- Note-only caveat (no separate claim): "Scoped lifetime alone does not reload or
  mutate configuration — IOptionsSnapshot caches an options value within a scope"
  and "Verify snapshot ≠ IOptionsSnapshot" (Verify compares behaviour,
  IOptionsSnapshot supplies configuration). Both are consistent with the cited
  lab5 sources and are wording choices, not testable repo facts.
- Coverage: the audited ledger has 277 entries (TP-001…TP-277, internal/
  teaching-points.json) and both approving verifiers carried full 277-entry
  coverage with no missed teaching points.
