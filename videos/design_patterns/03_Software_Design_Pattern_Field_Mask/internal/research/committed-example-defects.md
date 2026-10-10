# Defects the committed example actually contains

The `english_authoring` run 355 (`01a11cb8-550c-7070-aabc-06ecd5a60dab`) found these while reading
the three example stages and chose to correct them in the note rather than quote them as teaching
material. `factual_verification` run 358 (`ses_ee33c4d5bffeBeVWOxKI5l69zk`) re-derived each one from
the pinned blobs with `git show` and approved the note with no findings. The recording itself never
mentions any of them.

## `Diff` records equality as a difference for the first name

- Present in all three stages, including the current file. At `ef4630d`
  (`https://github.com/AntonC9018/uniCourse_csharp/blob/ef4630d291e20ffbcf93018cba43a53971d327a2/examples/field_masks/Program.cs`):

  ```csharp
  if (FirstNameEquals(a, b))   // true when the names are EQUAL
  {
      result.FirstName = true; // ...so this records "equal" as "different"
  }
  ```

  The last-name branch (`a.LastName != b.LastName`) and the groups branch
  (`!a.BelongsToGroups.SequenceEqual(...)`) are correct; only the first-name branch is inverted.
- The demo input makes the two clients share both names (`"Anton"`, `"Curmanschii"`) and differ
  only in `BelongsToGroups`, so the inversion is not cosmetic: `differences.FirstName` is `true`
  exactly when the first names match.
- The note's correction is a negated call, `result.FirstName = !FirstNameEquals(a, b)`, and it
  states plainly that the committed snapshots "call `FirstNameEquals` without negation … that marks
  equality as a difference and contradicts the intended diff contract".
- Execution check on 2026-10-10 (not a worker result): `Program.cs` copied out of `6b312db` into a
  scratch directory and run with the local SDK (repo csproj targets `net9.0`; only SDK
  11.0.100-preview.4 is installed here, so the project was re-targeted to `net10.0`; the C# source
  was not modified). Output: `Might be siblings`. With `!` added to the one condition, the program
  prints nothing. The committed demo therefore reports two identically-named clients as possible
  siblings.

## The indexed `AreSet` loop has a missing negation

- Only in `80eddb6786130b733401932cadfb35f5c939d8f4`
  (`https://github.com/AntonC9018/uniCourse_csharp/blob/80eddb6786130b733401932cadfb35f5c939d8f4/examples/field_masks/Program.cs`):

  ```csharp
  for (var i = ClientField.FirstField; i != ClientField.Count; i++)
      if (Get(toCheck, i))
          if (Get(mask, i))
              return false;      // returns false when a required flag IS set
  return true;                   // ...and true when the mask is missing them
  ```

  The contract is "all required flags are set", so both returns are inverted for non-empty
  requirements.
- The bitwise rewrite in `6b312db` fixes it incidentally:
  `return (mask._value & toCheck._value) == toCheck._value;`. The note says the prior boolean helper
  and the later bitwise helper both express the contract, so the defect is scoped to the middle stage.
- Execution check on 2026-10-10 (not a worker result): probing the indexed stage with
  `AreSet(diffs, require first name only)` and `AreSet(diffs, require nothing)` returns
  `False` and `True`; the same probe against `6b312db` returns `True` and `True`.

## The console message and the predicate disagree

- The call site prints "Either first or last name are different" when
  `AreSet(whereAreDifferent, { FirstName = true, LastName = true })` holds, but `AreSet` is an
  *all*-flags predicate in every stage, so the message claims "either" where the code requires
  "both". The note flags exactly this: the demonstration's message and its all-flags predicate
  should not be conflated.
- Execution check on 2026-10-10 (not a worker result): with the committed demo input the second
  message never prints, because the two last names are equal and `AreSet` requires both flags.
  A learner following the message text would not reproduce the printed output.

## What this means for later maintenance

- The note is approved and the fixing work order recorded "clean factual verdict needs no fixing",
  so none of the above is an outstanding defect *in the note*. They are properties of
  `examples/field_masks/Program.cs` that the note now describes explicitly. Anyone who edits the
  example should expect the note's three corrections to become stale, because they name the
  committed code as wrong.
- The `Might be siblings` demonstration is the most fragile claim in the note: it depends on both
  the diff polarity and on the demo clients sharing names, and it silently inverts if either changes.
