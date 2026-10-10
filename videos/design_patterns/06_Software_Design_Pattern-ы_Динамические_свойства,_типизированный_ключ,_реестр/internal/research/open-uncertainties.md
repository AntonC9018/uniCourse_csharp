# Open items the record does not close

Marked unresolved. Each names what is still open, and where the evidence stops.

## TP-336 contradicts the note, and the contradiction was accepted rather than fixed

- Ledger TP-336 (3999319–4012880 ms, ≈01:06:39): "A key holding the name Anton is stored under the
  index-zero key with a null value, showing which concrete key object is used."
- `notes.en.md` at 01:06:39: "the name key has numeric identity zero and stores the example name «Антон»
  under that identity. Zero is the key's number; the stored name is not null."
- `factual_verification` run 489 recorded this explicitly and accepted the note: the ledger phrasing is odd,
  the note flags the discrepancy, "acceptable". No finding was raised, so the ledger text still carries the
  wrong claim.
- **Unresolved:** the ledger was never corrected. Anyone regenerating this folder from the ledger will
  reintroduce "null value".

## TP-314 keeps the recording's `const`/struct confusion

- See `corrected-record-claims.md`. The note corrects it; TP-314 and TP-315 still read as the recording
  spoke them.
- **Unresolved:** the ledger wording is the only place a downstream worker will see the claim.

## TP-349 states the opposite of what the presenter says

- TP-349 says the registry increments its counter *after* returning. At `t=4126s`–`t=4129s` the presenter
  says the counter is incremented *before* returning; the note follows the recording.
- **Unresolved:** the ledger is uncorrected, and this one is more dangerous than the other two because a
  reader who trusts the ledger could reproduce the wrong allocator and believe the code is right.

## TP-042 has a zero-duration evidence range

- TP-042 (`Суть примера: есть сами шаги и есть механизм прохождения по ним, они разделены.`) carries
  `startMilliseconds == endMilliseconds == 427510`. The verifier saw this and still covered the point at
  the pipeline section, treating it as harmless.
- **Unresolved:** a zero-length transcript range cannot be checked the way every other point can be.

## No saved repository state for four on-screen stages

Established by run 486 and carried into the note as explicit reconstructions: the parameterless `Execute()`
greeting step, the generic-variance stage, the numeric key-registry allocator, and the registry
introspection implementation. See `coverage-decisions.md`.
- **Unresolved:** if a matching commit ever appears in `examples/property_dict/` or
  `examples/pipeline_library/`, those four reconstructions should be replaced by real citations.

## The `false_start` / `caption_noise` windows overlap between segments

S-032 (`1729–1734 false_start`) sits inside S-033 (`1729–1737 caption_noise`); segment windows overlap
throughout the run-403 accounting. The exclusion ranges themselves are usable; the segment ids are not a
partition. See `caption-disposition-map.md`.
- **Unresolved:** whether the overlap is an accounting artefact or genuinely two overlapping windows.

## An intermediate `notes.en.md` blob is recorded but not recoverable

Every `notes.en.md` and `teaching-points.json` tool event of run 486 carries a blob hash that does not match
the published file (`b78c83555a921267be81d436eff4ef3293958ed0f0eaa03796f885b8232c94a9` and
`18b42933f8ad04d70ec22150cd0a2bd851ae0acc03ad1febbe8b40949f922354` respectively), while the work order's
`outputHashes` and the committed tree both record `d8f9da58…` and `4155bb7b…`. The only visible change
between the two states is a glossary rewrite in the run's tool calls: the "Key and dictionary" and
"Boxing, flyweight, pool, and ECS" bullets were string-replaced by "Key, array, and dictionary" and
"Object, value type, reference type, and boxing".
- **Unresolved:** which text the intermediate hashes actually cover is not determinable from the record.
  Treat `d8f9da58…` and `4155bb7b…` as the only states worth citing.

## Not verifiable from the record

- No worker in this folder compiled or executed any C# sample. Every "compiles / fails at runtime" claim in
  the note is transcript-backed, never reproduced locally. Reproducing them would need an out-of-band
  check.
- `worker_tool_events` preserved exactly one `source_opened` row for this folder (the Microsoft `in` page);
  the other three Microsoft pages appear only in the run-486 rollout's web call arguments. The links are in
  the note and were reviewed by run 489, but three of them have no independent open event in the database.

## Fresh research (not a worker finding)

- Checked 2026-10-10, https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const :
  "Constants can be numbers, Boolean values, strings, or a null reference… the only possible values for
  constants of reference types are strings and a null reference." This confirms the note's claim that C#
  constants extend beyond the recording's three spoken types, and that a constructed struct instance can
  never be one.