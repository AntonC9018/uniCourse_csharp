# Sources and citations worth reusing for this video

Citations established by the author (`english_authoring`, run 266, session
`01a116fd-9c21-7ef0-83bd-06e4eb196b39`; its recorded web search was restricted to
`site:learn.microsoft.com`, tool event 1763). The verifier (run 268) found no citation defect.

- New modifier, warning-vs-error correction for TP-010/TP-011:
  `https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/new-modifier`
  Re-verified 2026-10-10 (resolves; states hiding without `new` produces a compiler warning).
- Compiler warning identity behind that correction:
  `https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs0108`
  Fresh source, checked 2026-10-10; confirms CS0108 is a level-2 warning, not an error.
- Empty property pattern / non-null check semantics for TP-056/TP-057:
  `https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/patterns/property-positional-patterns`
  (canonical title "Property and positional patterns"; re-fetched 2026-10-10 and resolved).
  Complementary language-reference page with the `is { }` wording quoted in `claim-checks.md`:
  `https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns`
- Inheritance/interface default-implementation nuance for TP-067:
  `https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/inheritance`

Lab links used in the note (repository-relative, pointing at the real lab documents, per the
code-citation contract that forbids substituting a published website URL):

- `../../../labs/1_basic/16_method.md` — instance methods; offered as earlier practice.
- `../../../labs/1_basic/18_interface.md` — polymorphism/interfaces; linked as optional further
  study and marked **not material taught in this video**.

The verifier confirmed both lab paths exist by listing `labs/1_basic/`.

Transcript anchors used as the de-facto source for every code state (no immutable alternative
exists; see `no-committed-code-source.md`): `internal/transcript.md`, canonical VTT sha256
`dc96357e8ddf7353724bc62813bd69dad23a8ccb3d5d7623be020295fd9e4ec6`, video `y1KNWcgUUMU`.
