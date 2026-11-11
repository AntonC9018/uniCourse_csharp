# Claims the verifier confirmed, with the exact check performed

Source: `factual_verification`, run 192, session `ses_f01294d91ffevAUIrUH3p7ouiW` (work order
`wo-8c8ce67ca3d69aedfeba461889613854d8f05d4f30756a8186c2eee55589552a`), verdict `approved`, no
findings, all 123 ledger entries listed as covered.

The verifier read the transcript out of Git rather than trusting the working tree, then spot-checked
every numeric and syntactic claim in the note. Reproduce these by `git show
4814706ae2fafbc797829239a33afc2eeefa29e8:<transcript>`.

## Commits proved to exist before any claim was checked

```
git rev-parse --verify 4814706ae2fafbc797829239a33afc2eeefa29e8^{commit}
git rev-parse --verify 32fcbff7a4c933f42b333ee08ad0af22298409b4^{commit}
```
Both resolved. This was deliberate — the verifier prompt forbids inferring a broken link merely because
a commit is absent locally.

## Transcript at that commit contains the folder's files

`git ls-tree -r --name-only 4814706… -- videos/csharp/09_C*/` returned
`internal/transcript.md`, `internal/transcript.vtt`, `internal/video.json` — and nothing else. No
`notes.en.md`, no `teaching-points.json`, no frames or scenes. This is the structural reason the note
carries no code-history citations for its own construction.

## Numeric and behavioural claims

| Note claim | Transcript evidence | Verifier's note |
|---|---|---|
| Final counter output is 6 | t≈526–535 "в конце увидим шесть, потому что шесть раз был добавлен" | "\"шесть\" = six. Good." |
| Default factory = attack 1, health 10 | t≈691–698 "будет одна атака, 10 здоровья… определяет эту идею дефолтные атаки и здоровья" | "Confirms attack 1 health 10; strong 5/10. Good." |
| Strong factory = attack 5, health 10 | same range | as above |
| `public static int Main(string[] args)` sketch | t≈1471–1485, quoted in `recording-overstatements-the-note-corrects.md` | "matches transcript (returns int, public static, Main, args array)" |
| A static class cannot be instantiated | t≈238–245 "невозможно создать статический класс [объект]" | kept verbatim in the note's rejected-experiment sketch at 00:03:33 |

## Top-level / local-function claims

- Declarations after `return` are legal: checked against t≈1541 onward; the note's local-function
  section shows the declaration after the `return 0;` statement.
- Scope of the local function is inside the containing function: checked against t≈1552–1560.
- Capture semantics (TP-121) were **not** pass-through. See `open-questions-and-gaps.md`.

## Labs citations exist at the approved commit

```
git ls-tree -r --name-only 32fcbff7… -- labs/1_basic | grep -E '1[56]_'
```
returned `labs/1_basic/15_static_class.md` and `labs/1_basic/16_method.md`. Both links in the note's
intro resolve at the pinned English commit. Neither lab is a copy of the combat program — the note says
so explicitly ("related exercises, not committed copies of the combat program").

## Author self-checks worth knowing about

The author (`english_authoring`, run 190) validated the note with a scripted pass before writing it:
141 timestamp links, all with `&t=` seconds equal to the `[hh:mm:ss]` prefix; 123 coverage anchors, in
order, all matching the ledger; no `[[123]]` markers left unexpanded; no citations under
`internal/frames`, `internal/images`, or `internal/scenes`; the file starts with a content heading.
The saved file was then re-read and byte-compared against the draft.
