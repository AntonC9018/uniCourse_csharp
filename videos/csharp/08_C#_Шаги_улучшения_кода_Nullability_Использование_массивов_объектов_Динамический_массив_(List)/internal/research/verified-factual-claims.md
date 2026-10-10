# Factual claims checked against evidence (english_authoring run 187, verified by factual_verification run 189, session `ses_f0140bc29ffeQN3YIbsCLp6kmN`)

All claims were checked against the English note at commit `9baff29caa8f8477b88e9cb6d40f182bdc3bef51` and the transcript pinned at `4814706ae2fafbc797829239a33afc2eeefa29e8` (`internal/transcript.md`). Everything below was re-verified on 2026-10-10 with `git show 4814706a:…/internal/transcript.md` in the local checkout.

- First-round recorded outcome: the player ends with 7 health and each enemy with 5 (transcript `00:01:36`–`00:01:39`, `&t=96`–`99`s: "у игрока осталось семь здоровья и у врагов по пять"). Basis for the note's test result at `[00:01:23]`.
- Overshoot demonstration: player health 6 reaches -2 after the second enemy attack (`00:02:09`, `&t=129`s: "6… после атаки второго врага у игрока будет минус два здоровья"). Basis for the note's "Death means `health <= 0`, not just `health == 0`".
- Rat-spawn configuration: the player's health is raised to 50 so one loop iteration wins (`00:35:35`, `&t=2135`s). Basis for the loop-exit demonstration.
- The array holds five cells, allocated with `new Enemy[5]` ("их будет пять", `00:22:44`, `&t=1364`s).
- The last enemy is a Goblin ("для последнего Гоблин", `00:23:03`, `&t=1383`s).
- The drawn address 88000 is spoken as "адресу 88.000" (`00:26:24`, `&t=1584`s) — an instructor-drawn illustrative address, not a source-controlled value; the note states this explicitly.
- Before nullable analysis is enabled, `Player = null` (declared, unassigned address) is not an error (`00:09:48`–`00:09:51`, `&t=588`–`591`s), even though no address is stored.
- Adding `?` to a reference field produces the possible-null diagnostic (`00:10:24`–`00:10:38`, `&t=624`–`638`s).
- `Enemy3.Attack++` on an omitted, uninitialized field compiles in the demonstrated configuration but fails at runtime (`00:15:49`, `&t=949`s) — the NullReferenceException setup.
- The nullability lab at commit `19d6c57ba17ef64ef9c65d22808e2c3f682768c2` (`labs/1_basic/14_nullability.md`) really contains the cited project fragment: `<Nullable>enable</Nullable>` and `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>` (both in one `PropertyGroup` and in a second conditional group), and it explains `Nullable<int>` for value types versus `string?` not using `Nullable<string>`. Inspected via `git show 19d6c57b:labs/1_basic/14_nullability.md`.
- Final verdict for the completed verification round: approved with no findings; every ledger entry TP-001…TP-204 (204 points) was covered at its anchor, and all 21 distinct ledger anchors exist among the note's 25 headings. The paired fixing round recorded only "clean factual verdict needs no fixing" with empty `fixingRecords`.
