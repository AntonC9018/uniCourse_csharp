# The pinned repository example is related, never exact

One idea: the note's only code citation is pinned at an immutable commit whose
distance formula differs from the lesson's, so it is labeled related evidence.

- Citation: `examples/property_dict/Subclassing.cs` at
  `d5af1fb372be56a453e3b139990013c28e50198c` — the canonical value resolved
  with `git rev-parse --verify --end-of-options
  'd5af1fb372be56a453e3b139990013c28e50198c^{commit}'` during verification.
- The committed `Distance` there is Euclidean:
  `Math.Sqrt(diff.X * diff.X + diff.Y * diff.Y)` returning `float`, on a
  `record struct Position`. The lesson's arithmetic is the grid-step count
  `Math.Abs(xA - xB) + Math.Abs(yA - yB)`. Different formula, different result
  type — the link cannot be presented as the recording's exact code state.
- It is cited anyway because it demonstrates the lesson's grouped-position
  idea (a function taking two `Position` values), which makes it related source context only; the ledger does not attach a repository-blob evidence item to TP-119.
- Wording decision (english_authoring run 173, session
  01a0fda3-fe08-73e1-9548-03d5b3ba657d): the code-citation contract requires
  labeling a non-exact commit as related or final, never exact. The note says
  "An inspected related repository example at commit d5af1fb" and puts a
  shortened fragment next to it.
- Confirmed twice, independently: verifier run 174 (session
  ses_f024e998bffeIkry2pe0QJw8Mf) at commit 11dbee37 —
  "Citation resolves; content matches the note's description (Euclidean, float,
  record struct Position)"; verifier run 235 (session
  ses_ee91f0002ffegBLRSn9DtOinR5) at commit
  924eaadd84275c9287747c8f62765cee5cf3a794 — "The citation fragment matches.
  Note is well-qualified."
