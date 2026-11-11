# Sources, pinned revisions, and where each research item comes from

## Reusable source links

- Video: `https://www.youtube.com/watch?v=fcxFAW1EE_A` ("Git").
  `internal/video.json` pins `videoEtag` `cMqOq-7SDAs4fe1AcOUz5QugOoo` and
  `captionEtag` `0bc5zktKfwf4z-hqVD8b4WOteho`; the caption id behind the
  transcript is `AUieDaYasoSPXj53KPjJyUwoQ1KvMWkOIMvqjA9RflorVJo-ReY`. Use the
  etags to detect that a re-upload or caption edit invalidates any transcript
  derived from them.
- Transcript moment pattern used throughout the note:
  `https://www.youtube.com/watch?v=fcxFAW1EE_A&t=<seconds>s`, generated so the
  rendered `[hh:mm:ss]` and `t=` value always agree. `internal/transcript.md`
  (sha256 `021699d65f5167023d2b587fd49107b6761796a937717c2ed487395f094ec08d`) is
  the deduplicated form; `internal/transcript.vtt`
  (sha256 `98006796030ac647377b4cd28b7a10a19fea80c481ecd70fd2fc3911364c3003`) is
  the canonical source, and that canonical hash matches the hash recorded for
  the video in the maintenance database.
- Lab: `labs/1_basic/03_git.md`, cited from the note as
  `../../../labs/1_basic/03_git.md`.

## Pinned immutable revisions worth reusing

- `a944ae1fd13128aa386007b0681b6019d2b8b97e` ("Topic titles for labs, numbered
  navbar, repo-ordered sections", 2026-09-16) is the last commit touching
  `labs/1_basic/03_git.md`. The lab's content is identical from that revision up
  to the verified commit, so the lab's text can be cited at a single pin without
  risk of drift — this is why the lab was the one repository source that could be
  cited safely.
- `6b1761f3b2a971fe74ffda2733cf8edd784c8dce` ("Update course video
  fcxFAW1EE_A") is the verified English commit. At it: `notes.en.md` =
  sha256 `2ed796bb6ffba2e0fd20a0c6ea0eeca8813e3e08bc55bbc63fd12301a256cc7e`,
  `internal/teaching-points.json` = sha256
  `74b3b77b7499b58fb638cde8b50fc82c4e14acb35ecbe8cd3981c0b2ec8c666d`. Both match
  the current working tree, so the note in the checkout is the verified note.
- Pre-regeneration note states, if the earlier wording is ever needed:
  `d511aecdd37844993606d09814ab9c8e19092a4f` (synchronized note) and
  `fd02d35e911e1bf65715005ccf36d6ffdee82019` (the same note with the
  case-neutral branch-flag correction). The difference between them is exactly
  the branch-flag sentence.
- No repository blob pins the demonstration project at all: no inspected commit
  contains the C++ demo. Any future "immutable citation" for this video's
  examples must therefore be a video timestamp, not a repository URL.

## Record map (what each research item was mined from)

- `branch-flag-case.md` — factual_verification pass at the earlier English
  commit (work order `wo-80fa26f4f50221b64ebb5abbf9009fcdd33f30a329d0ae991c0c15cc02884579`,
  session `ses_f0993cfd5ffeRibX5XepirLzbw`); the applied correction from the
  following fixing pass (work order `wo-0ab7eb63d480e54d59cab6d8ad18de457fd330b15ee1c932cc69dddfe1e6772b`,
  session `ses_f0961e9cdffe6pPPSL5SHf3HXs`); the blocked full regeneration and
  its evidence (work order `wo-830978305c445f92092001015d06cce3d11317e58c6b2d92b84378b3cea5f9d9`,
  session `01a0f860-db0c-7123-a010-e9af59c23f4f`); the accepted regeneration and
  approving verification (work orders
  `wo-43c06fdb49eda234dfbe4331d56e54372d3cd198b20d3a3235211b6208fb3796`,
  session `01a0fc02-597f-7f61-a5c1-e358d15bf129`, and
  `wo-edd1371ebcfc30372af3d5027b104438453db72c70d6f7c93e53ba8251aafbd5`, session
  `ses_f03f393dfffe7bxwcnbUxXxi1G`).
- `evidence-scope-decisions.md` and `verified-claims.md` — the accepted
  english_authoring session `01a0fc02-597f-7f61-a5c1-e358d15bf129` (its stated
  source paths: `internal/transcript.md`, `internal/video.json`,
  `labs/1_basic/03_git.md`) and the approving factual_verification session
  `ses_f03f393dfffe7bxwcnbUxXxi1G`.
- The one claim not from the record — the `-b` vs `-B` behaviour difference — is
  marked as a fresh check dated 2026-10-11 at
  `https://git-scm.com/docs/git-checkout`.
- Deliberately not mined: the translation and translation-fidelity passes
  (`notes.ro.md`, `notes.ru.md`). Their findings are Romanian and Russian
  phrasing and omission corrections, not research about the video.
