# Statements in the recording / teaching-point ledger that the note corrects

Author work order `english_authoring`, run 486, session `01a124af-39b4-73c3-bf34-02efbd5f1ca9`.
Each item names what the record actually says, what the note says instead, and the evidence.

## `const` vs "a key cannot be a struct" — transcript 01:02:01–01:02:12 (`t=3721s`–`t=3732s`)

- Recording: «потому что ключ - это структура, а конст в cшарфе могут быть только инты и строки и флоты.
  Примитивные типы.»
- Ledger point TP-314 (3721440–3730799 ms) restates it as "A key cannot be a struct because C# consts are
  limited to primitive types such as int, string and float." TP-315 adds the correct half.
- Note (`notes.en.md`, the 01:02:01 paragraph): the blocker is **`const`**, not using a struct — an
  initialized user-defined key struct cannot be a compile-time constant, and a `readonly struct` remains a
  valid key representation. It also states that C# constants include more than the three types spoken.
- Evidence: the Microsoft `const` reference the author opened at this stage.

## "Fat struct" is a design nickname, not the C# `struct` type — around 00:23:40–00:24:05

- Captions render the term inconsistently as "F-struct" / "Fract".
- Note (00:23:40 paragraph) uses the canonical name and adds that the saved repository example is a
  `sealed class`, so "struct" is naming, not a requirement.
- Evidence: `examples/property_dict/FatStruct.cs` at `d5af1fb372be56a453e3b139990013c28e50198c`
  (see `pinned-revisions.md`), re-opened and confirmed by `factual_verification` run 489.

## «ноль» is not `null` — transcript 01:03:14 (`t=3794s`)

- Caption says "ноль" (zero). The note states the example sets the erroneous value back to numeric zero so
  it compiles, and that `null` is not a valid replacement for a non-nullable integer.
- Related but not identical: at 01:06:39 (`t=3999s`) the note says the name key has *numeric identity*
  zero and stores the name under it — "Zero is the key's number; the stored name is not null." The ledger's
  TP-336 for that moment instead reads "stored under the index-zero key with a null value". The two records
  disagree; see `open-uncertainties.md`.

## Garbled caption at 00:05:58 (`t=358s`)

- Ledger TP-034 (358469–362870 ms) reads: "Если слово контекст задано только снаружи, на нём нельзя строить
  несовместимые со свободным рынком рабочие места в коде." The first clause is correct; everything from
  "нельзя строить…" is a garbled merge that does not belong to the lesson.
- Note (00:05:36 paragraph) says the caption is garbled and recovers only the real point — externally
  supplied context — instead of inventing a rule from the noise.
- Evidence: `internal/transcript.md` at 00:05:58 (`t=358s`) actually says
  «…стратегии нужен какой-то контекст для функционирования, который может быть задан только изне[вне]» —
  exactly the "supplied only from outside" point, with no rule about jobs or markets.
- Note for future repairs: this span is dispositioned `teaching`, not `caption_noise`, in the ledger
  accounting, so the noise map will not flag it (see `caption-disposition-map.md`).

## Compiler wording vs runtime deferral at 00:30:23 (`t=1823s`)

- The narration's compiler wording in the broad-base version is easy to read as "the compiler rejects this".
- Ledger segment S-025 (`ledger` worker run 393, session `ses_ee32cdc4fffeIDSa5cXp455ZT5`) records both
  halves: a single pipeline cannot mix `MathContext` and `GridContext` steps (1823240–1846630 ms), and
  because the cast to `GridContext` happens *inside* the step, the mixed pipeline fails at runtime rather
  than at the call site (1846630–1854590 ms).
- Note (00:30:02 paragraph) states the runtime reading explicitly and warns against the static reading.

## "Increment after returning" is the ledger's reading, not the recording's — TP-349

- Ledger TP-349 (4129110 ms, 01:08:49): "Only after returning does the registry increment its counter, so
  every subsequent call yields a different key."
- Recording says the opposite: at 01:08:46–01:08:49 (`t=4126s`–`t=4129s`) «Но перед этим counter увеличит,
  чтобы последующее возвращение давало вам новый…» — *before* returning, the counter is incremented.
- Note (01:08:40 and 01:08:49 paragraphs) matches the recording: preserve the current value locally,
  increment, return the saved identity; "Nothing executes after `return` in this method".
- This is a ledger statement the note silently contradicts; it is listed again in `open-uncertainties.md`.

## Ledger wording "casts them to time" — TP-257 (2994270–2995580 ms)

- Ledger says the Spud step prints the Date and Sum constants "and casts them to time, because these values
  arrived as object".
- The note does not repeat that: it describes the sum step exposing `Sum`/`Date` constants, reading the
  current value as `object`, and casting to the required type.
- This one was judged, not corrected: `factual_verification` run 489 recorded the ledger phrasing as an
  odd transcription and judged the note's reading reasonable, with no finding raised.

## The related lab outline contradicts the recording — `labs/2_design/04_properties.md:32`

- The lab bullet list says: «Использование integer ключа и реестра (нет в видео)» — "use an integer key and a
  registry (not in the video)". The lab's own task 4 (lines 63–70) then asks for exactly that centralized
  key registry.
- The recording demonstrates the registry from 01:08:02 (`t=4082s`) through the end (ledger TP-345 onward,
  note sections "A registry allocates stable key identities" … "Restrict construction to protect the key
  contract").
- Note (line 31) links the lab as the related exercise **and** states the contradiction, deferring to the
  recording for lesson coverage.
- Evidence: `labs/2_design/04_properties.md` at the working-tree revision read by run 489
  (`grep` on `registry|реестр|integer|целочисл`), and the transcript at `t=4082s`.