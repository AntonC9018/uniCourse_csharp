# The recording itself corrects the second measurement's inputs

One idea: at approximately 00:12:20 (t=740s) the instructor corrects the
tree measurement from tree–player to tree–enemy, and the note preserves that
as a factual correction, not as a refactor.

- Before: the second distance block measures tree against the player.
- After: it measures tree against the enemy, using the enemy's X and Y.
- Why it matters: this is a change of measured behavior (different input pair),
  distinct from the structural refactors (blocks, functions, position type)
  that preserve behavior. The note marks the "input-correction state" with its
  own timestamp link and a simplified before/after sample.
- Secondary teaching point kept with it: keep each entity's coordinates
  together — enemy X compares with tree X, enemy Y with tree Y; and the result
  variable's name should name the actual measurement.
- Established by english_authoring run 173 (session
  01a0fda3-fe08-73e1-9548-03d5b3ba657d) and carried into the regenerated note
  from english_authoring run 228 (session 01a1032d-f616-7bb1-98d1-4ee452610c36);
  ledger points covered at the "identify-the-common-calculation-and-repair-its-inputs"
  anchor, approved by verifier run 235.
