# The "41-hex bad citation" findings were false — the note's pins were already canonical

Established by two independent factual_fixing passes that rejected the preceding
factual_verification's requested changes (video z5CUTiubzwU, English note at
commit d2fe7a0b66de1de959f1068df6f964b764cdb250).

- The first factual_verification (work order wo-faba319f0e…, run 164, session
  ses_f03a14d77ffel1D6ytiiSlRQnM) returned five `bad_citation` findings
  (F-001…F-005): that notes.en.md's immutable template/Options/interface/trail
  links embed 41-hex-digit revisions that Git silently resolves to the wrong
  commit, and demanded re-pinning.
- factual_fixing run 165 (wo-682f2a7f…, session ses_f02723cadffe1L5Upqzr3q4PIa)
  blocked: scanning every `blob/` and `commit/` reference in notes.en.md returns
  13 distinct revisions, all exactly 40 hex characters (2e88085e…, 306f4dda…,
  407acfc8…, 52533790…, 5280fbbb…, 53502e15…, 7001d912…, 7f31c6ac…, 94f768be…,
  9968dcccb…, 9acccee8…, 9cabfb70…, d80428c0…), each a valid commit object whose
  contents match the state the note claims.
- factual_fixing run 184 (wo-c1e2ac3b…, session ses_f01616574ffeI5MZeZst7o0Y6Z)
  independently reached the same conclusion: all 13 hashes are 40-hex canonical
  and "git log --reverse -- examples/dependencies_simplified shows exactly the 11
  commits used by the note's history trail, in the order and with the states the
  trail describes, so re-pinning to shorter values would break otherwise-correct
  citations."
- F-001 rebuttal: 5253379090ffec2086654d0a6d1957e0dbd81637 *is* the empty starting
  state — `git show` yields `public static class Helper { }` and
  `public sealed class Tests { }`; its tests.csproj already carries 4
  Verify-related references. The "Process item service" commit the finding
  demanded is a completely different file state.
- F-002 rebuttal: the trail's Template entry already points at 5253379090… (the
  pre-DI template), not at 53502e15… ("Service provider").
- F-003 rebuttal: the "open interface state" link points at
  7001d912d07b50d6516b1206d5b36bd03c54d43b ("Polymorphism"), whose Code.cs
  declares `public interface IRemapNameService` (line 55) plus both
  implementing services — exactly what the note's IdentityRemapper sample needs.
- F-004 rebuttal: the Options-section citations point at
  d80428c0f252c626af6927783c16b777dc7f1425, whose Code.cs contains
  PriceCutoffConfig (line 135) and PriceCutoffService(PriceCutoffConfig)
  (lines 142–144) and whose Tests.cs contains
  `services.AddSingleton(priceCutoffConfig)` (line 50) — precisely the cited
  target state.
- F-005 rebuttal: the "overlong" examples the finding listed (…cd7e, …f899, …1c,
  …ec39a) are 4-hex suffixes/prefixes of valid 40-hex values, i.e. misreads of
  correctly formed hashes; no overlong revision exists in the file.
- Git quirk behind the confusion (exercised in both fixer sessions): `git
  rev-parse` accepts a 41-hex string and resolves it to the 40-hex commit, while
  a GitHub blob/commit URL with the same 41-hex string would 404 — which is why
  the pipeline's canonical-SHA rule exists, and why an inflated string is a real
  hazard even though local Git tolerates it.

Likely origin of the false findings (recorded, but not fully provable):

- The work orders' own "exact English commit" strings were themselves overlong:
  run 164 was told to verify at `5280fbba…537cd7e4` (41 chars) and run 184 to fix
  at `407acfc8…a46e43` (41); run 188 was told `7001d912…43b5` (41), while run
  191's was the canonical `d2fe7a0b…`.
- Run 188 observed that its prompt-rendered copy of the note text carried
  perturbed SHAs (families like d80428c0…14259–14250 and 94f768be…cc9–cc1) while
  the on-disk file was clean, and attributed the difference to the
  redaction/rendering layer; run 164 recorded zero file reads (no file_read tool
  events for the run), so its findings were composed without inspecting the note
  on disk.
- Resolution: the later factual_verification passes (run 188,
  wo-914412982f…, session ses_f0144b586ffeDXBzj3s9ltRGwS; and run 191,
  wo-4aaa638a…, session ses_f012bff3bffezlUgesA6cHWpQv) verified the on-disk note
  at d2fe7a0b… and approved with zero findings; both closing factual_fixing
  orders recorded "clean factual verdict needs no fixing". The note was never
  edited for citations.

Re-verified 2026-10-10 against the local checkout: all 13 revisions in the
current notes.en.md are 40-hex and each cited state was spot-checked
(empty template at 52533790…, interface at 7001d912…, PriceCutoffConfig and
AddSingleton at d80428c0…). Same conclusions.
