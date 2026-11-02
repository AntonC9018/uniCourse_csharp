# Instructor claims that are wrong or misleading, and how `notes.en.md` corrects them

Established by the `english_authoring` work order
`wo-224fbd13b916169329f38348ccb1f8002d7101ebf10c55f5298726663213bf44`,
worker run 267 (codex session `01a116fd-bf6a-7f91-be37-aad205ba7215`,
2026-10-07). The author's own words in that rollout: "The transcript includes
several claims that need careful correction: enums retain runtime type
information, casts do not validate membership, and `Debug.Assert` does not
guarantee a crash."

Each entry below names the ledger entry that still carries the instructor's
wording, the transcript moment that proves the instructor said it, and the
correction the note actually ships.

## `return` does not require braces — corrects TP-035

- Transcript 00:07:09–00:07:16 (429–436 s): "это всё нужно фигурные скобки
  засунуть, иначе будет ошибка компиляции".
- The claim is false. `if (p == 2) return "Гитлер";` is valid C#; only a
  block-bodied function needs its outer braces.
- Note wording: "the claim that `return` itself always requires those braces
  is too broad … A normal block-bodied function still needs its outer braces."
- Source the author used: the C# selection-statement reference,
  https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements

## The enum type does not vanish at runtime — corrects TP-082, TP-083, TP-084

- Transcript 00:17:31–00:17:45 (1051–1065 s): "эта двойка будет не как двойка,
  а как тип портрет. Но по факту оно сохранится просто как двойка, потому что
  портрет — это концепт, существующий во время компиляции… В данном случае он
  используется только во время компиляции."
- .NET retains enum type information and member names; the printed name is
  resolved by runtime enum formatting, not chosen by the compiler.
- Note wording: "Keep the numeric-storage insight, but correct the claim that the
  enum type disappears at runtime: .NET retains enum type information and member
  names."
- Sources the author used: `System.Enum` type reference
  (https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum#the-systemenum-type-and-enum-constraint)
  and `Enum.ToString`
  (https://learn.microsoft.com/en-us/dotnet/api/system.enum.tostring).

## Enums do not support every integer operator directly — corrects TP-109, TP-110

- Transcript 00:21:53–00:21:56 (1313–1316 s): "инамы — это на самом деле просто
  числа, и с ними можно делать любые манипуляции, которые можно делать с
  обычными числами."
- Arbitrary integer work is possible only after conversion; multiplying two
  enum operands requires `(int)` casts first.
- Note wording: "The statement that 'every integer operation works on enums'
  needs a qualification … For example, multiplication of two enum operands
  requires converting them first."
- Source: enum operations specification
  https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/enums#206-enum-values-and-operations

## `portrait.ToString()` does not take the portrait number — corrects TP-025

- Transcript 00:05:16–00:05:25 (316–325 s) calls the custom converter
  "функцией портрет in to string".
- The built-in parameterless `int.ToString()` would produce `"2"`, not a
  portrait label, and takes no argument of that kind.
- Note wording: it is named `PortraitToString` "to distinguish it from the
  built-in parameterless `portrait.ToString()`, which would produce `"2"` for an
  integer rather than a portrait label."

## `Debug.Fail` / `Debug.Assert` do not guarantee a crash — corrects TP-039, TP-044

- Transcript 00:08:06–00:08:16 (486–496 s) and 00:09:26–00:09:40 (566–580 s):
  "Debug.Fail … компилятор не определяет, что это крашит программу";
  "в финальном билде уже … оно просто выпилится".
- Two distinct errors. (a) A diagnostic is not a guaranteed interruption —
  behaviour depends on diagnostic handling and listeners. (b) The condition is
  the `DEBUG` compilation symbol, not optimization: the calls are annotated
  `[Conditional("DEBUG")]`.
- Note wording: "these diagnostics do not universally guarantee a crash:
  behavior depends on the diagnostic handling", plus an exception alternative
  `throw new System.ArgumentOutOfRangeException(nameof(p));` that "interrupts
  execution in every build".
- Sources the author used: `Debug.Assert` and `Debug.Fail` API pages.
- Independently re-checked 2026-10-10 (not a worker finding, my own check):
  every `Debug.Assert` overload carries `[System.Diagnostics.Conditional("DEBUG")]`
  and the remarks state "By default, the Debug.Assert method works only in debug
  builds", with the Abort button terminating the app and Ignore continuing —
  https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.debug.assert?view=net-10.0

## A cast performs no membership validation — corrects TP-092

- Transcript ~00:18:35–00:18:37 (1115–1117 s).
- The cast converts a number to the enum type; it never checks the member list,
  so an unnamed value such as `(Portrait)123` compiles and stores fine.
- Note wording: "This conversion performs no **membership validation** … A number
  absent from that list can still be stored." This is the pivot the note uses
  to explain why the `default` branch cannot be removed.
- Source: enum conversions section
  https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum#conversions

## Printing an enum prints a name only when one is declared — qualifies TP-075–TP-077

- Transcript 00:15:58–00:16:10 (958–970 s): "он найдёт в Инами имя константы с
  этим значением и напечатает это имя как строку."
- Unqualified, the claim is false for an unnamed value; `Enum.ToString` emits the
  number then.
- Note wording: "when its value has a declared name, the output normally uses
  that member name rather than the underlying number", citing `Enum.ToString`
  documentation, which "describes name-based output and numeric output when no
  matching name exists".

## The rejected `Color.White` assignment is a type mismatch, not an iteration rule — corrects TP-067

- Transcript 00:13:35–00:13:41 (815–821 s): "если я скажу color.white, это даёт
  ошибку уже. Мы теперь можем присвоить только из иумерации портрета".
- The ledger renders this as "only values coming from the enum iteration are
  accepted"; no iteration is involved.
- Note wording: "More precisely, it is the cross-type assignment or call that
  fails; `Color.White` itself is a valid color member. No iteration requirement
  is involved."

## A missing case does not always crash — qualifies TP-074

- The demonstrated program uses `Debug.Fail` plus `return ""`, so with `DEBUG`
  absent the missing enum case yields an empty label rather than a crash.
- Note wording: "With the video's debug-diagnostic-plus-empty-return form, the
  outcome is diagnostic-dependent and can become an empty label when `DEBUG` is
  absent. With the exception alternative, the missing case interrupts execution
  in every build."

## The equality check does not crash — qualifies TP-095

- Ledger TP-095 says the program "crashes at the later equality check".
- Note wording: "the assignment succeeds and failure appears later when the
  value reaches the programmed check/failure path. Equality itself does not
  crash: it produces a Boolean result."

## Implicit enum-to-int extraction must not be generalized — qualifies TP-106

- Note wording: "do not generalize that to an implicit conversion of every enum
  expression to `int`. Ordinary numeric extraction here uses `(int)`."