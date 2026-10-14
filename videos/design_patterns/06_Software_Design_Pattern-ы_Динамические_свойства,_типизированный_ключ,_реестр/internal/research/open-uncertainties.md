# Open items the record does not close

These remain source discrepancies or evidence limits; the note’s wording follows the recording where noted. Re-check the ledger and transcript before changing the note.

## TP-336 contradicts the note, and the contradiction was accepted rather than fixed

- Ledger TP-336 (3999319–4012880 ms, ≈01:06:39): "A key holding the name Anton is stored under the
  index-zero key with a null value, showing which concrete key object is used."
- `notes.en.md` at 01:06:39: "the name key has numeric identity zero and stores the example name «Антон»
  under that identity. Zero is the key's number; the stored name is not null."
- `factual_verification` run 489 recorded this explicitly and accepted the note: the ledger phrasing is odd,
  the note flags the discrepancy, "acceptable". No finding was raised, so the ledger text still carries the
  wrong claim.
- **Unresolved:** the ledger was never corrected. A future note pass using the ledger text alone could reintroduce "null value"; consult the transcript and current note together.

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

The note labels the parameterless `Execute()` greeting step, generic-variance stage, numeric key-registry allocator, and registry introspection implementation as reconstructions. This dossier does not treat a worker's historical search as exhaustive proof that no matching state exists. If a matching commit is identified in `examples/property_dict/` or `examples/pipeline_library/`, cite it only for the state it actually contains. See `coverage-decisions.md`.

## The `false_start` / `caption_noise` windows overlap between segments

S-032 (`1729–1734 false_start`) sits inside S-033 (`1729–1737 caption_noise`); segment windows overlap
throughout the run-403 accounting. The exclusion ranges themselves are usable; the segment ids are not a
partition. See `caption-disposition-map.md`.
- **Unresolved:** whether the overlap is an accounting artefact or genuinely two overlapping windows.
