# Scope boundaries the note took, with reasons

One idea: teaching/coverage decisions where the note deliberately states a
boundary instead of a universal rule.

- `static` local function for the extracted distance helper: chosen so the
  function cannot capture the surrounding locals and therefore must receive its
  coordinates explicitly. The note frames this as the lesson's practical habit
  for such helpers, "not a universal requirement that every C# method be
  static".
- `struct Position` declared after the executable statements: this is the
  ordering rule for top-level statements in this single-file program as the
  recording has it. The note explicitly does not generalize it ("it does not
  require every struct in every project to be declared last or forbid other
  source files").
- `new Position { X = 2, Y = 3 }`: `new` here creates a struct value; the
  spelling alone does not imply a separate heap object. Memory boxes in the
  video are a teaching model of scope, argument copying, returning, and
  shared-object mutation — "exact layout, compiler optimizations, and a
  complete account of memory management are outside that model".
- `ref` demonstration (00:33:33–00:34:34, t=2013s–2074s): an advanced preview
  "probably unnecessary for the current labs", kept distinct from
  class-reference sharing (00:34:40+, t=2080s). The note explains `ref` as an
  alias addressed by the caller's cell — "a pointer-like teaching picture of a
  managed alias, not a requirement to write unsafe pointer code".
- Not taught here: `out` parameters, recursive/array/loop content in the
  follow-up labs, obstacle-aware or diagonal path finding. The scope section
  names each exclusion.
- Both english_authoring runs (173: session 01a0fda3-fe08-73e1-9548-03d5b3ba657d;
  228: session 01a1032d-f616-7bb1-98d1-4ee452610c36) and verifier run 235
  (session ses_ee91f0002ffegBLRSn9DtOinR5) carry these boundaries unchanged into
  the approved state at commit 924eaadd84275c9287747c8f62765cee5cf3a794.
