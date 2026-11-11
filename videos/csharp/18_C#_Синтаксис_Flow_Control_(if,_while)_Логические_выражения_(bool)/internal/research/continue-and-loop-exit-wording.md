# `continue`: why the note names both the condition check and loop-body restart

## The recording and TP-099 compared here

This file examines these two descriptions; it is not a count of all ledger coverage. TP-097 and TP-122 also cover related loop-exit/continuation moments.

- Recording, [00:25:11](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1511s) – [00:25:24](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1524s):
  "Continu опять же перейдёт на первую инструкцию цикла… Continue не значит продолжи выполнять нормальным образом.
  Continu значит вернись в начало." — `continue` goes back to the *first statement of the loop body*, and explicitly is not
  "carry on normally". `internal/transcript.md` lines 1113–1123.
- Ledger entry (run 312, session `ses_ee50add39ffeKXHwokHCB02BB3`, TP-099, 1215190–1238470 ms): "continue aborts the
  current iteration of the loop, so everything that would normally run after it in the body is skipped and execution jumps
  back to the first statement of the block."

Both are the same behaviour for a top-tested loop, but they are not the same sentence, and the difference matters the moment
someone writes `while (true)`: there the "first statement of the body" is only reached *after* the constant-true condition
check, and any code placed between the `while` and the body would not be.

## The wording the note settled on

`notes.en.md:659`, timestamp [00:25:11](https://www.youtube.com/watch?v=mj9E9BhOAhU&t=1511s):

> "`continue` returns to the `while (true)` check and then the first statement of the body. It skips the later `return num`;
> it does not mean 'execute the next line.'"

This states the condition check and the body-first statement in one sentence, matching both the recording and the ledger
point instead of choosing one, and adds the negative form ("not the next line") that the instructor spelled out at 1519–1524 s.
The same distinction is carried earlier in the note at `00:18:55` (1135 s): "`continue` ends only the current iteration. In a
`while`, it goes to the condition check before the next pass."

## Sources the author consulted before drafting

From the `english_authoring` rollout (run 332, `01a11afa-c7c4-7562-b971-c72d954c858b`), the worker stated it was checking two
things first — "failed parsing writes zero, and `continue` returns to the loop's condition check" — and opened:

- <https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/jump-statements> (loop-control semantics)
- <https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0>

Only the TryParse URL survives in `worker_tool_events` (row 16 for run 332); the jump-statements URL is attested by the
rollout transcript alone. The delivered note does cite the Microsoft jump-statements page at line 466 and links the related lab example at `19d6c57…` §15. The transcript supports the specific wording; the documentation supports the language semantics.