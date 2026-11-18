# Recording overstatements the note deliberately corrects

The author (`english_authoring`, run 190, session `01a0fec1-2dde-75c1-98ce-451a36f03dd7`) opened with
the assessment that the supplied ledger "contains several technical overstatements, especially about
static fields, instance receivers, and generated `Main`" and that it would "explain the demonstrated
steps and correct those claims in the note." Each correction below names what the recording actually
says, so the wording can be re-audited without re-watching the video.

All transcript timestamps are from `internal/transcript.md` for video `UVLUbfcCcG0`.
Recording text was re-read from the transcript during this capture; none of these readings is new —
they are the same evidence the author and verifier used.

## "A static class cannot have fields"

- Recording, t≈222–235: "приписал Static вы не сможете [ему] создать поля — это вам даст ошибку…
  статический класс не может иметь полей" ("a static class cannot have fields").
- The same stretch, t≈238–245, adds the separate and *correct* claim that "невозможно создать
  статический класс [объект]" — a static class cannot be instantiated. The note keeps this one at
  00:03:33 without softening it; only the field claim is corrected.
- The note (`notes.en.md` at 00:03:18) does not repeat this: "The precise rule is that it cannot contain
  **instance** fields; static fields are permitted, as the next demonstration establishes."
- Why it matters: the same recording later demonstrates a static field in that same static class
  (t≈324–331: "в статическом классе Вы можете сделать статическое поле, например…"). Repeating the
  early claim verbatim would have made the note contradict itself 5 minutes later.

## "With static fields, passing data is impossible"

- Recording, t≈425–429: "со статическими полями такое сделать невозможно."
- The note at 00:07:05: "A function can technically receive a value obtained from a static field, but
  direct global access does not force callers to reveal that dependency. The lesson's 'impossible'
  phrasing is a warning about hidden dependencies, not a language prohibition on passing data."
- Correctness basis: passing `Logic.AddedEnemies` as an argument is legal C#. The pedagogical point
  (dependencies become invisible) survives; the literal prohibition does not.

## The receiver as a "hidden first parameter"

- Recording, t≈931 and t≈1086, models the receiver as an extra first parameter.
- The note hedges twice: at 00:15:31 and 00:18:06 — "it is not a parameter you declare or pass inside
  the parentheses. The compiler supplies the instance context."
- The sharpest case is the `ReduceDamage` conversion at 00:17:01 (transcript t≈1021): the recording says
  the compiler will accept `playerStats` "первым параметром… и вторым параметром ничего не передать,
  потому что второго параметра нет." The note keeps the lesson's conceptual numbering ("the receiver is
  'first' and the damage is 'second'") but states that in the actual C# declaration `amount` is the only
  declared parameter and only argument, and that "there is no literal offset parameter to add to the
  signature."

## Naming: `CreateDefault` / `CreateStrong` / `ReduceDamage`

- The note never claims these are the recording's exact identifiers. 00:10:56 offers "names such as
  `CreateDefault` or `CreateDefaultStats`"; 00:12:40 records that "the recording experiments with a
  `Create`-prefixed name such as `CreateDef`"; 00:17:23 calls `ReduceDamage` "the illustrative method
  name."
- Grounding for `ReduceDamage` specifically: the transcript at t≈1056–1060 has the speaker say "я как
  пример сделал Reduce damage" — the note's name is taken from the recording, not invented.
- `ReduceDamage` and `CreateDef` are absent from the transcript because they are code on screen, not
  speech. The note's escape hatch is the only evidence available.

## The generated entry point

- Recording, t≈1471–1485: the compiler "создаёт класс называемый program по умолчанию и в него вставляет
  публичный статический метод который возвращает int называемый Main и он принимает параметром массив
  аргументов переданных при запуске программы."
- The note at 00:24:23 sketches exactly `public static int Main(string[] args)` but frames it as "an
  explanatory explicit equivalent, not the exact name, access level, or return type guaranteed for
  compiler-generated code."
- The note also blocks an inference the recording invites: 00:25:06 records that the speaker "explicitly
  expresses uncertainty about whether the compiler uses that arrangement [nested types] here; it is not
  evidence that your own types are automatically nested."

## Local functions are not public utility methods

- Recording, t≈1528–1560, treats the static functions written after the top-level code as living in the
  entry-point body. The note states this positively at 00:25:28: "They are not automatically public
  utility methods of `Program`."
