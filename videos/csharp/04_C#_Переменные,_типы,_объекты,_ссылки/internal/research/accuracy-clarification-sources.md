# Accuracy clarifications sit outside the lesson, and their source set

- The note separates what the video taught from five accuracy terms that only appear in
  the clarifications: immutability, garbage collection, string interning, pointer /
  address-of notation, and boxing + overload. The closing paragraph repeats the split:
  "Garbage collection, literal reuse, pointer rules, and overload selection above are
  accuracy clarifications, not additional implementation procedures taught in the video."
  Source: the lesson-boundary paragraph and accuracy-clarification block authored in
  `english_authoring` run 171 (session `01a0fd99-65c8-7181-9ecd-98a8c0cc72a8`).
- The exact pages run 171 opened (from its rollout plus the `worker_tool_events`
  `source_opened` rows), and what each backs in the note:
  - `https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types`
    — the value-type term definition.
  - `https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/reference-types`
    — the reference-type term, cited in "Replace only the reference in a" next to the
    C# string reference.
  - `https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/fundamentals`
    — the garbage-collection term (reclaiming unused objects and moving survivors while
    updating references).
  - `https://learn.microsoft.com/en-us/dotnet/api/system.string.intern?view=net-10.0`
    — the string-interning term and the note's "literal reuse may affect allocation
    timing, but it does not change the assignment result".
  - `https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/pointer-related-operators`
    — the pointer / `&` term, specifically that C#'s address-of operator is not a general
    way to obtain a managed string reference.
  - `https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing`
    — the boxing term, cited in the final accuracy clarification.
  - `https://learn.microsoft.com/en-us/dotnet/api/system.console.writeline?view=net-10.0`
    — the `Console.WriteLine` integer overload, cited in the same paragraph as
    `.../system.console.writeline#system-console-writeline(system-int32)`.
- The recording itself flags the printing mechanism as deferred: TP-034 (00:09:33) and
  TP-047 (00:12:30) are `scope_boundary` entries, and TP-049/TP-050 describe integer
  printing as "converts it into a dynamic object" then "its type is inspected".
- The note's wording keeps those two ledger entries honest: "This is the recording's
  introductory account, not a requirement that every integer output call first boxes its
  argument." That sentence is what the boxing/overload clarification corrects, and after
  the `dotnet/dotnet` excerpt was removed (commit `344a53b`) the Microsoft Learn links
  became the clarification's only support.
- Coverage mapping note: all 51 ledger entries are mapped to note anchors in
  `internal/teaching-points.json`, with `TP-049` and `TP-050` both anchored at
  `#printing-and-the-lessons-boundaries`; run 171's read-back check confirmed 51 IDs in
  order and every anchor resolving to a heading.
