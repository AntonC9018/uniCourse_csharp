# What the recording's two-string copy example actually says

- The transcribed program is four lines, recoverable from the audio in
  `internal/transcript.md` (ASR spelling kept as recorded; the captions hallucinate
  "Hell"/"Hello" inside the first declaration):
  1. `[00:06:42]` «сделать а и сделать … при», then `[00:07:00]` «вот эту строку которой
     мы Прив» — `string a = "привет";`
  2. `[00:07:23]` «создастся новый объект представляющий собой строку Hello 1 по
     какому-то другому адресу … будет создаться новая ячейка … в эту ячейку будет
     вписан адрес этого объекта» — `string b = "Hello 1";`
  3. `[00:07:49]` «Что значит B равно А … в ячейку с именем б вписывается то что в
     ячейке с именем а» — `b = a;`
  4. `[00:08:16]` «следующая строчка а равно Hello 2» — `a = "Hello 2";`
     then printing `b` yields the text `a` held when `b = a` ran, i.e. `привет`.
- The audited ledger (`internal/teaching-points.json`) agrees on the literals:
  TP-026 puts `"привет"` in `a` on line 1, TP-028 puts `"Hello 1"` in `b` on line 2,
  TP-029/TP-031 cover `b = a` and `a = "Hello 2"`.
- The ledger disagrees with the transcript on two details, and the record never resolved them:
  - TP-024 states the lesson "writes a = 2 followed by b = a", i.e. the reversed order.
    The transcript has `b = a` at 00:06:02 and the reassignment of `a` only at 00:08:16;
    the "2" in TP-024 matches the ASR fragment «А равен 2» at 00:06:11, which is the
    instructor's early mention of the later `a = "Hello 2"` line, not its position.
  - TP-033 states that printing `b` "still shows 'Hello 1'", which cannot hold if
    TP-026/TP-028 are right — after `b = a`, `b` holds `a`'s reference, so `"Hello 1"`
    (the object `b` originally pointed at) is the one text `b` can no longer print.
  - Run 185 noticed exactly this ("the ledger says a=2 then b=a … TP-033 says printing b
    still shows 'Hello 1' … a genuine discrepancy") and explicitly decided not to raise a
    `source_conflict`, judging the note's reconstruction label sufficient
    ("I'll approve"). Its final verdict was `changes_requested` for the unrelated
    `bad_citation` F-001, with coverage complete and no finding on the ordering.
    Mark unresolved.
- The pre-existing note (read by the author at the start of run 171 from
  `videos/csharp/04_.../notes.en.md` at `8dda92a46ccfa0d6c50fff6b435e9ad02b744b7f`) mixed
  these up: its string code would print `привет` while its explanation said `Hello 1`.
- The author's resolution, still standing in `notes.en.md`: keep the model consistent by
  fixing the sample's initialization, and hedge the remaining doubt explicitly —
  "The result is the original text that `a` held when `b = a` ran. If that original text
  is `"Hello 1"`, the result is `Hello 1`. With the explicit `"привет"` initialization
  above, it is `привет`. The destination's initial `"Hello 1"` cannot survive its
  overwrite by `b = a`." (source: `english_authoring` run 171, stated as "make each
  walkthrough consistent and label reconstructed code clearly").
- The analogous integer walkthrough has the same "recording state vs. reconstruction"
  split: the transcript names `A = 607` at 00:12:04 (TP-045) and `204` at 00:10:05
  (TP-042), and the note keeps both as explicit reconstructions with a prediction question
  about assigning 607 before the copy.
