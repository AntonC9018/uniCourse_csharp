# The printed values 18 and 20 are successive example states

The block example prints `FieldB` as `18` in one video state and `20` in a later one. The obvious
misreading is that the second print came from casting an unrelated `A` instance to `B`.

`english_authoring` run 231 (codex, session `01a1032f-3b9e-7cf1-8921-aae71f223b64`) recorded the
decision explicitly: "I'm also treating the printed values `18` and `20` as successive example
states, so the note won't imply that an unrelated `A` object passes a `B` cast."

The note therefore presents `18` and `20` as two states of the *same* example (the field is
assigned 18, then the example is re-run with 20), and only the *incompatible* `A`→`B` cast
(`B b1 = (A)ob;`, cited at 00:25:13) is described as throwing `InvalidCastException`.

Transcript support for the two states: 00:21:39 (value 18) and 00:22:16 (value 20),
trailing code-history entries of `notes.en.md`.
