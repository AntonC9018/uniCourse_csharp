# Two verification attempts filed citation defects against links that are not in the note

Both attempts ended `malformed` (`error_class=result_invalid`), and both had raised the same
class of `bad_citation` finding before failing.

- Run 159 (`factual_verification`, session `ses_f03d57084ffeVOsJtyR61uS4G5`) and run 163
  (`factual_verification`, session `ses_f03a519dbfferIUIgZ7tYAjo2k`) each reported
  *systematic* bad citations: "ten links use invented 41-character revisions" such as
  `e4b890a75d5e014ab00227c27ff6a35965acb9a10 … a19` and
  `b333fe75bfe90c7d2c965c1a57f477497887e5e20 … e29`, asserting they 404 on GitHub.
- Those strings do not occur anywhere in the note at the verified commit
  `6bd23cf30f17df99bc8ed719aa202ce6c36b1026`. Checked 2026-10-11:
  `git show 6bd23cf…:…/notes.en.md` contains 33 hexadecimal runs, every one exactly 40
  characters long, with only the two canonical values
  `b333fe75bfe90c7d2c965c1a57f477497887e5e2` and
  `e4b890a75d5e014ab00227c27ff6a35965acb9a1`.
- Run 163's supporting evidence was a `curl -s -o /dev/null -w '%{http_code}'` against a
  blob URL *it constructed* with an overlong revision, which returned `404`. A 404 on a URL
  absent from the note does not establish a defect in the note. The only recorded web
  attempt in the run ledger is `webfetch` of
  `https://api.github.com/repos/AntonC9018/uniCourse_csharp/git/commits/<overlong>`, whose
  outcome is `error` (tool_event_id 975) — so remote behaviour was never established from a
  successful response.
- Run 159 had already discarded the malformed-hash finding once for lack of reopenable
  evidence ("Drop F-004 (malformed hash) since unverifiable via trusted inventory"), yet the
  hypothesis resurfaced in later attempts.

## The successful run briefly walked into the same trap

Run 182 (`ses_f0163436fffekvtRwrzG13iMxw`) started from the same reading — "the note cites
many blob URLs with commits like …5e29 … a19" — and then ran
`git rev-parse --verify <41-char>^{commit}`. Git silently resolves an overlong object name
to the 40-character commit, which the run read as confirmation that those links exist.
After actually inspecting the note's content it raised **no** citation finding. The lesson:
`git rev-parse` accepting an overlong revision is not evidence that the document contains
it, and the deterministic citation checks only report that a pinned path resolves, not
what the note says.

## Likely seed of the pattern

The work order itself supplied the overlong revision: the verifier was told to verify at
`b333fe75bfe90c7d2c965c1a57f477497887e5e21` (41 characters, resolving to `…5e2`), and the
trusted inventory commit was `e4b890a75d5e014ab00227c27ff6a35965acb9a13`. A model that
pattern-matches "the app pins revisions this way" can then generate the same shape for the
note. Supplying the canonical 40-character value in the prompt is the cheap defence.
