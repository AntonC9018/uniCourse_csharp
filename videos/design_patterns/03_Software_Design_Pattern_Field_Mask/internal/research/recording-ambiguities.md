# Places where the recording (or its audited ledger) says something the note had to qualify

Every item here comes from `internal/teaching-points.json` and `internal/transcript.md` for this
folder, was handled in `english_authoring` run 355 (`01a11cb8-550c-7070-aabc-06ecd5a60dab`), and was
reviewed in `factual_verification` run 358 (`ses_ee33c4d5bffeBeVWOxKI5l69zk`).

## The recording names an API that does not exist (TP-119)

- Ledger entry: "Debug.Assert.Fail performs an unconditional assert that is always violated and is
  used together with the message you write" (1596120–1598398 ms, anchor
  `#enforce-the-field-index-contract`).
- There is no such member. The recording at 00:26:33–00:26:39 mentions the two calls separately, and
  the auto-caption renders them as "debugфайл" and "debug acert"; the string "Assert.Fail" does not
  occur anywhere in the transcript.
- The note states the correct form: "`Debug.Fail(message)` is the unconditional diagnostic; the API
  is not `Debug.Assert.Fail`", citing the Microsoft `Debug.Fail` reference. The verifier grepped the
  transcript, found the two separate mentions, and concluded the note corrects a transcription
  artifact rather than contradicting the lesson.
- Reusable lesson: ledger entries transcribed from Russian auto-captions can garble .NET member
  names. Check an API name against the transcript *and* the docs before repeating it.

## The ledger overstates what a release build removes (TP-112)

- Ledger entry: "A production build contains no assert checks at all, which only exist in a debug
  build."
- The note narrows it: calls to `Debug.Assert`/`Debug.Fail` are normally absent from a release build
  *because they are conditional on `DEBUG`*, and "production has no assertions of any kind" would be
  too broad. That is exactly what the `Debug.Fail` reference documents.

## `with` expressions are not forbidden inside a function (TP-142)

- Ledger entry: "The with-expression form cannot be used here because this code sits inside a
  function rather than in an object initializer" (1806990–1812470 ms, anchor
  `#replace-repeated-checks-with-loops`).
- What the recording actually says, at 00:30:14–00:30:21: setting a known field to false has an
  equivalent `with` form; then, about *the printer* specifically, "И с принтом мы так сделать не
  можем, потому что у нас это функция" — we cannot do that with the printer, because it is a
  function. The prohibition is on selecting a member by a runtime index, not on `with` inside a
  method.
- The note follows the recording rather than the ledger: "The important limitation is selecting a
  member by a runtime index … A `with` expression is legal inside a function; function placement is
  not a language prohibition", with the Microsoft `with`-expression reference.
- The verifier considered raising this as a `source_conflict` requiring a human decision — two
  trusted sources disagreeing — and rejected it, because the note's side is verifiable and the
  ledger entry is a loose transcription of the speaker. Same decision for TP-119.
- **Open item, unresolved.** The ledger entry's millisecond range (00:30:07–00:30:12) and the note's
  `[00:30:06]` anchor both sit on the *named-arguments* passage, while the utterance the entry
  paraphrases starts around 00:30:14–00:30:16. Checked by me on 2026-10-10 against
  `internal/transcript.md`; not raised by any worker. The substance is covered correctly, but if the
  ledger is ever repaired the anchor should move to 00:30:16.

## The four-bit complement diagram mixes two conventions (TP-167)

- At 00:34:16–00:34:29 the recording walks a value "1 01", says `V` is the unit shifted into the
  position of the first name, and states the negation of `V` is "0 1 1 1".
- Under the convention the committed code actually uses (`v = 1 << index`, first name at index 0),
  `V` is `0001` and its complement is `1110`; `0111` is the complement of `1000`, i.e. a one-bit mask
  drawn with the selected bit on the left. The in-source comment table in `6b312db`
  (`_value = 1 1 0 1`, `v = 1 0 0 0`) uses that left-to-right convention.
- The note keeps the lecturer's numbers and states the convention explicitly: "The video's four-bit
  diagram shows `0111` as a complement; that corresponds to a one-bit mask `1000`, with the selected
  bit drawn on the left. It is not the complement of position-zero `0001`." The verifier confirmed
  the "0 1 1 1" utterance in the transcript at t=2068s and accepted this reading.

## Two narrated runs of the same demonstration disagree

- At 00:11:02 the first run leaves only the surname; at 00:13:34 the later run leaves only the first
  name ("сейчас у нас останется только first name, если мы сделали всё правильно"). These are two
  `test_result` ledger entries, TP-047 and TP-057, and both remain in the folder's
  `teaching-points.json`.
- The speaker's own explanation at 00:11:05–00:11:20 is self-contradictory: he justifies the surname
  surviving because it is "нужно печатать, но не сказано, что не надо печатать" (requested and not
  forbidden) and then says of the same field that it *is* also marked not to print. TP-047 records
  the incoherent half: "only the last name is printed, because it is the only field that is both
  requested for printing and forbidden by the second mask".
- The note resolves both problems by rewording rather than picking a winner: the surviving field is
  "requested and **not forbidden**", and the later run is reported separately as a later result
  rather than as a correction of the first.

## A name mismatch inside the committed example that must survive into the note

- The enum member is `ClientField.BelongToGroups` while the domain-model field is
  `Client.BelongsToGroups`, in all three stages. The note preserves both spellings in the same
  sample instead of normalising them, because normalising would make the sample differ from the
  cited blob. The verifier checked this explicitly.
