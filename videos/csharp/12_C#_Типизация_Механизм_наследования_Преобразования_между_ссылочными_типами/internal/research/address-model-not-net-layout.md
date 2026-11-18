# The address model is the recording's model, not .NET layout

The note is explicit that the numeric sketches are explanatory, and this was a *carried-forward*
correction rather than a new invention. `english_authoring` run 231 (codex, session
`01a1032f-3b9e-7cf1-8921-aae71f223b64`) named the three corrections it preserved from the
pre-existing note:

- a cast preserves the instance reference (it changes the static type of the expression, not the
  object's runtime type);
- the postfix `!` operator suppresses a nullable warning and **adds no runtime guarantee**;
- the memory addresses, the hidden type link, and base-fields-first ordering are the recording's
  explanatory model, "not a portable specification of .NET layout."

`notes.en.md` carries this in its "Reading the examples" paragraph and repeats the framing at each
sensitive point, e.g. the `0x6000` field-start address is called "the lesson's simplified account
of compiling a fixed field access", and the hypothetical two-base address `5` is called "an
illustrative shifted position, not a numerical result of a legal C# cast."

Provenance reason: the accepted plan classified this folder as `manual_edits=false`, and the
worker's `MANAGED NOTE PROVENANCE` message stated that generated corrections in the existing note
are authoring inputs to preserve, not a manual-overlap conflict. So these framings survive
re-authoring by policy, not by coincidence.

Verifier corroboration (run 236, session `ses_ee91effecffeUy9bFs8k3GeASF`): the note labels printed
results as recording output ("These are results from the recording, rather than a new execution of
this reconstructed snippet"), which is why no `execution_result` evidence exists for any value.
