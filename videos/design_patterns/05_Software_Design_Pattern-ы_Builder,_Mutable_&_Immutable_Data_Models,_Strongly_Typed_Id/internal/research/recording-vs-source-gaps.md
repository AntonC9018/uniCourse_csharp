# Where the recording and the committed source disagree

The note's most reusable editorial decision for this lesson is refusing to
present the video's described state and the repository's committed state as the
same thing. Established by **author** (run 476). The verifier (run 488) re-read
every cited commit except `c6d7b18`, so the "Program output" pair below rests on
the author's read alone; see `open-items.md` item 2.

## Type of the ID constructor

- Caption/transcript, 00:48:57–00:49:14: the speaker introduces `HomeId`; the caption text is garbled as "ID типа валы". The author interpreted the audio as “long”, but that word is not directly established by the transcript.
- Source, `dcfd622`: `public HomeId(int value) { Value = value; }` — `int`.
- Note wording: "The recording first discusses a constructor taking a long; the
  inspected later commit uses int and calls the field Value." The note shows the
  committed shape and cites the recording for the discussion, not for the type.
- Not a contradiction in the lesson; the speaker changed the type before
  committing. Worth stating because a reader following the video literally will
  not match the file.

## Program output

- Recording, 00:40:21–00:41:10: the working demonstration prints the single name
  `John`.
- Source, `c6d7b18`: three citizens and three age prints, `18, 32, 32`; the third
  is named `Chell`.
- Note wording separates them explicitly ("The working source names that third
  citizen Chell and prints ages 18, 32, 32", and "These are the video's checks,
  not newly executed tests").

## Completeness of the intermediate commits

- `dcfd622` ("Broken id code") still contains stale builder calls and
  incompatible assignments after the representation change; `ff2cc72`
  ("Abstract builder") is likewise unfinished. Both produce compiler errors.
- The note labels them "Related state" and says explicitly that they contain
  unfinished code and are development states, not successful builds.
- `c79a352` ("Builder 1") is the commit that matches the end-to-end run shown on
  screen, and is what the validation discussion quotes.

## Configurations that exist only on screen

- Delegate/lambda configuration (1130–1230 s) is described, sketched, then
  abandoned in favour of "configure a mutable model, then convert it".
- The immutable-copy fluent variant (WithName/WithHome, 1104–1123 s) is described
  as an alternative, never implemented.
- `CityFinalBuilder` / `ICityFinalBuilder` (4660 s onward) is an illustrative
  completion of a proposed role; it is not a complete class in `ff2cc72`. The
  note says so in the sentence following the snippet.

## Claims the video makes loosely

- "Flat" storage refers to cross-entity links by ID; the city still owns
  collections of objects, and nothing in the lesson implements persistence,
  deletion, or lifetime control.
- ImmutableArray and sparse sets are named as options and not implemented.
- The lesson does not aggregate validation errors across objects; it throws on
  the first problem found.

## Wording consequences to preserve on any future edit

- Keep "shallow protection" and "deep immutability" distinct everywhere.
- Keep "intended rule" and "implemented check" distinct in the validation
  section.
- Keep "recording" and "working source" distinct wherever the two diverge.
- Do not upgrade any "described in the video" mechanism into "shown by the
  committed example" without a new pinned blob that contains it.