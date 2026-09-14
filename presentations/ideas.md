# Lesson 1 — Understanding code through abstractions

1. Start with ordinary cases. — Example: `Between` calls for inside/below/above, then its implementation.
2. Make argument roles and endpoint behavior clear. — Example: ambiguous `Between(10, 10, 20)` → named arguments → named constants; still silent on endpoints.
3. State the contract, and let the name help. — Example: `BetweenInclusive` calls plus the 4-point contract list.
4. Validate inputs, assert established assumptions. — Example: `throw` on reversed bounds vs `Debug.Assert` in an internal helper.
5. A useful abstraction hides a substantial operation. — Example: `RemoveDuplicates` calls and its contract; question: what does the caller no longer implement?
6. Caller and implementation work at different levels. — Example: one-line caller vs the HashSet/loop implementation.
7. New notation is not a higher level. — Example: `255` vs `0xFF`, then the call vs the `seen.Add` step inside it.
8. Names carry meaning in the system. — Example: `right`/`indicator` vs `rightSensorTemperature`/`temperatureWithinAllowedInterval`.
9. Normalize to one representation. — Example: `Interval.Create` with boundary flags, plus the table of equivalent inputs.
10. An invariant keeps normalized bounds valid. — Example: the `Interval` struct plus the normalizing factory.
11. Encapsulation keeps bound interpretation inside the type. — Example: the `Contains` method, then the one-line caller.
12. Local reasoning uses contracts of dependencies. — Example: `RemoveDuplicates` + `ShowNames`; what you must vs must not know.
13. Callers shouldn't enumerate fields. — Example: screen update vs alarm handling, both listing Left/Center/Right.
14. Put the field-by-field operation in one place. — Example: `AllWithin` helper, then the two slimmed-down callers.
15. An extra call can cost more than it saves. — Example: inline guard vs `CheckArguments` wrapper and its implementation.
16. Show the workflow without its subordinate algorithm. — Example: import workflow before vs after extracting `RemoveDuplicates`.
17. Make necessary input explicit. — Example: hidden `Settings.MaximumTemperature` vs an explicit `maximumTemperature` parameter.
18. Match the parameter view to intended access. — Example: `IReadOnlyList` reader vs in-place sort with `outNames`.
19. Two references can observe one change. — Example: list owner, read-only view, `SortInPlace`, reading `view[0]`.
20. Compare what two callers must know. — Example: Caller A (lists fields) vs Caller B (`AllWithin`) plus the 4 questions.

# Lesson 2 — Deciding what code should know

1. Add reporting to a working import. — Example: read/dedup/write workflow; new requirement: Read / Removed / Written counts.
2. A step that knows the whole report is over-coupled. — Example: `RemoveDuplicatesForReport` taking `ImportStatistics`.
3. Show what the extra dependency permits. — Example: a second caller inventing a dummy report; an accidental `Reset()`.
4. Return facts about the operation itself. — Example: `DeduplicationResult` and `Deduplicate` with a usage snippet.
5. The workflow builds its own report. — Example: assembling `ImportReport` vs a second caller that just shows names.
6. Architecture is who-knows-whom. — Example: the two dependency diagrams (statistics vs result).
7. Call order is a hidden dependency. — Example: setting `parser.Separator` before/after `Parse` vs a `Parse(text, separator)` function.
8. A narrow view makes mutation precise. — Example: `IRemovalCounter` interface plus `DeduplicateAndCount`.
9. Generic reporting lets the outside interpret events. — Example: `ItemsRemoved` event with a report-writing vs a console-printing subscriber.
10. Cohesion asks what belongs together. — Example: the bounds guard moving into `Interval`.
11. Two operations can share one rule. — Example: index inserts/lookups that forget `ToLowerInvariant`.
12. One abstraction can own both sides of a rule. — Example: the `NameIndex` class with ordinal-ignore-case comparison.
13. Separate helpers can still scatter the rule. — Example: `NormalizeKey`/`Store`/`Find` plus the still-forgetful caller, vs the index.
14. Same data doesn't mean shared responsibility. — Example: `AddName` fused with file reporting vs the two split lines.
15. A concrete change tests the boundary. — Example: the `ImportReport` diff (added Destination) while `Deduplicate` stays unchanged.
16. Technical debt is a continuing cost you can point to. — Example: repeated normalize-at-every-call vs the index; interest vs repayment.
17. Refactoring preserves promised behavior. — Example: repeated field checks vs `AllWithin`, plus the same-cases checks.
18. Over-engineering is unjustified complexity. — Example: `FormatReport` + `WriteAllText` vs destination/formatter/writer factories.
19. Don't abstract for an imagined future. — Example: today's file write vs a speculative 6-parameter writer vs the actual preview need.
20. A workflow-specific wrapper can be deliberate. — Example: `DeduplicateForImport` for the import vs plain `Deduplicate` elsewhere.
21. Compare what each abstraction buys and costs. — Example: inline guard vs `AllWithin` vs the `IRemovalCounter` variant side by side.
22. Review a function that knows too much. — Example: `CleanNames` with `ImportContext` (Reset, source, clock) plus the sorting questions.
23. Return rejection facts from the operation. — Example: the `CleanResult` implementation counting empties and duplicates.
24. The caller adds workflow information. — Example: assembling `CleaningReport` (source, counts, clock) vs a caller that just shows names.
