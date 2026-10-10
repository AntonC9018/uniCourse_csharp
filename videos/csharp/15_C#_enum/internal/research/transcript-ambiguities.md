# Transcript passages whose plain reading misleads, and the disambiguation the note chose

Established by the `english_authoring` work order
`wo-224fbd13b916169329f38348ccb1f8002d7101ebf10c55f5298726663213bf44`,
worker run 267 (codex session `01a116fd-bf6a-7f91-be37-aad205ba7215`).
These are not wrong claims so much as compressed speech that a reader could take
the wrong way; each one visibly shaped the note's wording.

## The "five becomes six" rule — TP-115

- Transcript 00:22:31–00:22:38 (1351–1358 s): "а второму я задам, например,
  пять, потому что он берёт предыдущую плюс один, будет шесть."
- Read literally this says an explicitly assigned `= 5` member evaluates to 6.
  It does not: the member written `= 5` is 5, and the *next* omitted member is 6.
- Note wording: "The transcript's compressed wording about assigning 5 and
  getting 6 must not be read as changing the explicitly assigned member … The
  rule is previous member plus one, not the number of members and not a rule that
  overrides an explicit assignment."
- Source: enum member specification
  https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/enums#204-enum-members

## `Count` is a numeric continuation, not a count — TP-117–TP-123

- The sentinel equals the number of real colors only because the values are
  zero-based and consecutive.
- Note wording: "It does not inspect an array, reflect over members, or
  automatically count declarations … Gaps, duplicate backing values, a nonzero
  start, or moving Count can break that equality. Count is itself a declared enum
  member, so application code must still distinguish it from a usable color."
- The note also carries a `<details>` self-check: with `White = 5`, `Count` is no
  longer a trustworthy color count.

## "Far more operations" for string comparison — TP-014

- Transcript 00:02:38–00:02:57 (158–177 s): to compare two strings you must "сделать
  намного больше операций", checking length then each element.
- Note wording: "Do not interpret 'far more operations' as a measured result for
  every comparison; fast paths and early mismatches can reduce string-comparison
  work." The lesson's model is kept, the universal quantification is dropped.

## Spaces belong to display labels, not to member names — TP-078

- Transcript 00:16:18–00:16:32 (978–992 s): the explicit function can return a
  Russian label "с пробелом в середине", and a constant name cannot be configured
  that way.
- Note wording: "The recording's wording about spaces should be understood as a
  requirement for display labels: C# identifiers can use Cyrillic letters, but
  ordinary identifiers cannot contain spaces. A readable label such as 'Джордж
  Вашингтон' can."

## `switch` statement, not `switch` expression — TP-052

- The instructor's "это же что-то равно ифлсам" is the shape a `switch`
  *statement* replaces.
- Note wording: "The video uses a switch statement with `case` labels, not the
  separate C# feature called a switch expression", and the note stays with the
  demonstrated statement form rather than expanding into a switch-expression
  tutorial.

## Per-case braces: the scope claim is only half the rule — TP-053, TP-054

- Transcript 00:11:20–00:11:36 (680–696 s) argues braces are needed so a variable
  in one case is not in scope for the whole switch body.
- Note wording: "Without an inner block, a local declared in a switch section can
  have scope across the switch block, while definite-assignment rules still
  restrict its use. Per-case braces avoid confusing name collisions and
  visibility."

## Scope boundaries the instructor stated and the note preserved — TP-021, TP-056, TP-108

- Text conversion postponed, then implemented (00:04:32, 272 s).
- More involved switch syntax deferred (00:11:51, 711 s).
- Modular-arithmetic application deliberately skipped; the required technique is
  enum↔`int` conversion (00:21:29, 1289 s).
- The note's "Scope and practice" section restates all three rather than
  expanding into them.