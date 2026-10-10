# The ownership-history list stores the outgoing owner

Correction the authoring runs drew out of the saved code rather than the narration (`english_authoring`, runs 475 and 499, same two sessions as `immutable-source-citations.md`).

- What was wrong: loose narration invites reading `previousOwners` as a list of current/recent owners, i.e. adding the *new* owner.
- Proof: `Functions.ChangeOwner` at `d6e14fd023172f33faba0c4e0be4e8ea62b0ec82`:
  `if (dog.Owner != null) previousOwners.Add(dog.Owner); dog.Owner = newOwner;`
  The value appended is the owner being replaced, and only when one exists.
- Consequences the note states, all derivable from that snippet plus the committed five-step default:
  - starting ownerless, changing to Anton stores nothing;
  - changing Anton → Alex appends Anton;
  - changing Alex → null appends Alex and leaves the current owner null;
  - the recorded run at 00:17:59–00:20:07 (`transcript.md`, t=1179 / 1199 / 1207) prints previous owners first, then the dog, so "Alex/Lucy" is owner Alex with a dog renamed to Lucy — not a dog named Alex.
- The committed default order is `ChangeOwner(Anton)` → `ChangeName(Lucy)` → `ChangeOwner(Alex)` → `PrintOwners` → `PrintDog`, which is why the note says the second owner-change object sits at index 2 of `Pipeline.Steps`.
