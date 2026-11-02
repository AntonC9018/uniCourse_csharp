# Claims checked against evidence, and how wording was settled

Verification run 268 (session `ses_ee5516302ffe93jSw3EOe9MfZC`) checked the note at commit
`558ca24b582da5e7b7b03429dd3c1f9e51768258` and returned verdict `approved` with zero findings; the
subsequent fixing order recorded "clean factual verdict needs no fixing" and produced no fixes. The
checks below are what the record shows actually being compared.

## `new` modifier: recording says "error", compiler warns (TP-010, TP-011)

- Ledger TP-010 states: without `new` the compiler reports an error. Transcript at 00:02:07–00:02:15 backs the recording: "чтобы этой ошибки не было вы должны Дописать этот модификатор New" — the speaker calls it an error.
- The note corrects this to the actual diagnostic behavior: omitting `new` ordinarily produces a **compiler warning** (a project may treat warnings as errors), and it cites the C# reference page for the new modifier.
- Fresh recheck 2026-10-10 confirms the correction: the missing modifier is compiler warning CS0108, level 2 (`https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs0108`), and the new-modifier page states that hiding without `new` "you get a compiler warning" (`https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/new-modifier`).

## `is { }` vs `is not null`: what actually binds (TP-054, TP-055, TP-056, TP-057)

- TP-056 (explanation): `animal.Swimming is SwimModule swimming` on a field whose declared type is already `SwimModule?` performs **only a non-null check** — no runtime type test occurs, because the left-hand type is already known. The note states this and contrasts it with the general syntax that does test runtime compatibility.
- TP-057 (code_change) is garbled in the ledger: "empty braces with an is not null pattern ... puts the address stored in the field into the pattern variable" — it merges two spellings. The note separates them: `is { }` (empty property pattern) matches a non-null value and binds it; `is not null` tests without binding. It explicitly warns that the combined spelling `is not null swimming` is not valid.
- Transcript support at 00:11:56–00:12:06: the instructor offers "пустые фигурные скобочки" as the alternative and says the variable receives "тот адрес который не Нау который в этом поле хранится" — the address stored in the field, i.e. the same reference, not a copy or a converted type.
- Fresh recheck 2026-10-10: the C# patterns reference confirms "the empty property pattern `is { }` matches everything non-null, and you can use it instead of `is not null` to create a variable" (`https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns`); the fundamentals page linked by the note agrees (`https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/patterns/property-positional-patterns`, re-fetched and resolving 2026-10-10).

## capability modules are stored per animal, not selected by bird type (TP-051, TP-053)

- The note clarifies that with composition the array still holds `Animal` elements (not ability modules); capability modules live in each animal's fields. This matters because ledger TP-053 says "ability objects may be stored in a single array", which reads as if the array itself held modules.
- Also captured: a sparrow flies but does not swim, a penguin swims but does not fly, a duck does both — so capability cannot be derived from the bird type, which is why the general code reads fields instead.

## TP-023 and TP-024 assert the same rule twice

- Both ledger points state that a derived reference is usable where the base type is expected (Sparrow/Animal). The note covers them in one explanatory section; the coverage map assigns both IDs to that section's anchor. No separate wording was needed.

## TP-062: ledger claims "each bird overrides the behaviour it needs" — the sketch does not show it

- Ledger TP-062 says each bird overrides the behaviour it needs and the overridden function may be empty. The transcript at 00:13:01 does say "определение функции перековать" (override), but the on-screen inheritance sketch only declares separate `Swim`/`Fly` methods per bird — it shows duplication, not `virtual`/`override`.
- The note hedges: the override discussion is mentioned, but the sketch itself "only shows the capability duplication and does not claim its methods override anything". The verifier treated this as an acceptable hedge rather than a conflict, and kept the override sentence so the discussion is still represented.

## TP-067 (interfaces) needed a nuance to stay accurate

- The recording's argument is that interfaces only partly help because you still have to redefine the function. The note adds, explicitly marked as **not taught in the video**, that modern C# supports default interface implementations, so "interfaces can never contain reusable implementations" would be too broad. Cited to Microsoft's inheritance tutorial.

## Test results are attributed to the recording, not to this authoring pass

- TP-041/042/043 (per-animal ageing at 00:08:12–00:08:37; three penguins produce three swim calls after adding one more Penguin) are written up as results demonstrated in the recording. The note carries the sentence: these are results demonstrated in the recording, not claims that the reconstructed samples were executed during authoring — no `execution_result` evidence exists in this folder's record.
