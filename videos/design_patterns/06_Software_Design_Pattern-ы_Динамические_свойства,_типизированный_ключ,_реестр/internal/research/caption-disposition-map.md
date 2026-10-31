# Cue dispositions: what the ledger deliberately excluded from teaching

Work order kind: `ledger`, worker run 403, session `ses_ee32aff6fffeqwyCWpaModxFlr`
(`COMPLETENESS PROMPT (segment-accounting worker)`). The 62 segments account for every caption cue with a
disposition; the non-`teaching` ranges below are the spans that carry no teaching point.

Ranges are **cue indices into `internal/transcript.vtt`** (3315 cues, 00:00:00.320 → 01:19:58.440), not
seconds. The accounting record contains no cue-to-time map; the mapping has to be read off the VTT. Cue
indices do roughly 1.45 s each, so cue *n* is near `n × 1.45` seconds — close enough to locate a span, never
close enough to quote as a timestamp.

| segment | cues (exclusive end) | disposition |
|---|---|---|
| S-002 | 99–108 | repetition |
| S-003 | 132–133 | caption_noise |
| S-005 | 235–243 | false_start |
| S-006 | 321–329 | repetition |
| S-007 | 364–365 | false_start |
| S-007 | 366–384 | repetition |
| S-012 | 638–639 | off_topic |
| S-013 | 686–693 | repetition |
| S-014 | 723–737 | wait |
| S-016 | 828–835 | off_topic |
| S-021 | 1101–1102 | caption_noise |
| S-022 | 1167–1168 | caption_noise |
| S-023 | 1209–1210 | caption_noise |
| S-023 | 1229–1230 | caption_noise |
| S-025 | 1316–1319 | caption_noise |
| S-026 | 1352–1353 | caption_noise |
| S-027 | 1416–1417 | caption_noise |
| S-029 | 1525–1527 | caption_noise |
| S-029 | 1549–1550 | caption_noise |
| S-030 | 1591–1592 | caption_noise |
| S-030 | 1616–1617 | false_start |
| S-031 | 1651–1654 | caption_noise |
| S-031 | 1668–1680 | false_start |
| S-032 | 1729–1734 | false_start |
| S-033 | 1729–1737 | caption_noise |
| S-034 | 1828–1829 | caption_noise |
| S-038 | 2021–2022 | repetition |
| S-046 | 2436–2438 | caption_noise |
| S-047 | 2538–2544 | navigation |
| S-052 | 2755–2756 | caption_noise |
| S-056 | 2982–2985 | caption_noise |
| S-056 | 2985–2988 | caption_noise |
| S-057 | 3024–3027 | false_start |
| S-058 | 3085–3086 | caption_noise |
| S-061 | 3253–3256 | caption_noise |
| S-062 | 3294–3295 | caption_noise |

Totals: 91 `teaching` ranges, 21 `caption_noise`, 6 `false_start`, 5 `repetition`, 2 `off_topic`,
1 `wait`, 1 `navigation`.

## Two things this map does *not* cover

- **The 00:05:58 garbling is inside a `teaching` range, not a noise range.** Cue 253 (00:05:58.469) falls in
  S-005's `243–276 teaching` span, so the accounting passes it as teaching. The defect is inside the *text*
  of ledger point TP-034, whose sentence merges a true clause with nonsense; the note handles it by content,
  not by disposition. Do not expect the disposition map to flag it.
- **The recorded segment boundaries overlap.** Adjacent segments share cue boundaries throughout
  (S-001 ends at 60 and S-002 starts at 54), and S-032 (`1729–1734 false_start`) sits inside S-033
  (`1729–1737 caption_noise`). Segment ids are labels for overlapping windows, not a partition; use the
  ranges, not the ids, when excluding content.

Useful reading: `caption_noise` marks a caption defect rather than a speaker lapse, so nothing teachable is
lost by skipping those spans. S-033's `1729–1737` noise lands at ≈00:40:32–00:40:43, inside the
contravariance explanation where the note's 00:40:30 anchor sits.