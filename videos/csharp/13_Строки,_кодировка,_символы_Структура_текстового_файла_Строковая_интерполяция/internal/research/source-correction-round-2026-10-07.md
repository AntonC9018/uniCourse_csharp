# Source-correction round of 2026-10-07 (run 248)

A translator flagged two possible factual problems in the English note. The
round ran as run 248, codex/gpt-6.1-sol, session
`01a1033a-6c99-7ca0-8730-2cc554dc9` (the same rollout as run 234), 2026-10-07
15:20 UTC. The database also created a dedicated verification order
`translation-source-wo-5bf68461…` for this round; see
`record-contradictions-and-open-items.md`.

- **Both proposals targeted redaction placeholders, not file content.** The
  supplied copy contained "The [redacted] Plane (BMP)" and "The final lesson
  boundary is [redacted] and message construction". Reading the actual file
  showed zero `[redacted]` occurrences and a byte-identical match with
  immutable commit `accd55e6a1d8349548cd48e7a1768ecf4dfaf94f`. No correction was
  needed for either.
- **BMP already correct:** both occurrences read "Basic Multilingual Plane",
  corroborated by the Unicode glossary
  (https://www.unicode.org/glossary/#basic_multilingual_plane).
- **Scope-wording proposal rejected:** the translator proposed narrowing the
  closing boundary to "string concatenation". Evidence against: transcript
  626–649 s covers UTF-16/`char`; 767–876 s concatenation; 876–891 s `ToString`
  and `WriteLine`; ledger TP-051/052 and TP-062–072 span all three. The broader
  "basic representation and message construction" boundary stays.
- **Outcome:** no files changed; code, citations, attribution, and app-owned
  animation regions preserved. A second verifier run 249
  (session `ses_ee90b709fffe75lGUbE3R4DnbE`) re-approved the note after this
  round.
