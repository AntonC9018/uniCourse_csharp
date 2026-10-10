# Ledger entries TP-088 … TP-123 are in Russian

**Status: unresolved.** The audited English teaching-point ledger committed for
this folder is not entirely English, and nothing in the recorded pipeline
flagged it.

## What the ledger contains

- `internal/teaching-points.json` at `a5cf0acb` holds 384 entries, `TP-001` …
  `TP-384`.
- 36 consecutive entries, `TP-088` through `TP-123`, carry Russian `text`. They
  span 1 177 520 ms – 1 472 740 ms of the transcript and land in three note
  sections: `#consider-a-configuration-delegate`,
  `#store-configuration-in-mutable-models`,
  `#keep-data-open-and-put-contracts-on-configuration`.
- Every one of the 36 is marked `englishCoverage.status = "covered"`.
- `notes.en.md` itself contains no Cyrillic, so the effect is on the audit
  artifact, not on the student-facing text.

## Provenance

- Extraction worker run **376**, session `ses_ee33117f3ffeeycfo2V6n4Qx2d`, work
  order `wo-17f4886…` (`ledger`), emitted segments `S-017`–`S-020` entirely in
  Russian; those four segments are exactly `TP-088`–`TP-123`.
- No other extraction run contributed Russian teaching-point text. Runs 373 and
  383 contain Cyrillic only inside `nonTeaching.detail`, quoting Russian caption
  fragments such as "Долный Джон", which is correct usage.

## Why nothing caught it

- `english_authoring` run 476 received the ledger, mapped all entries to anchors,
  and marked all of them covered.
- `factual_verification` run 488 returned `verdict: approved`, `findings: []`,
  `fixingRecords: []` with `coverage` listing `TP-001` … `TP-384` inclusive —
  including the 36 Russian ids.
- `factual_fixing` then completed with "clean factual verdict needs no fixing".

## Why it matters

- The verifier's coverage claim is a list of ids, not a language check, so an
  English-only audit trail can be silently mixed. Any later reviewer comparing
  the note against "the audited English ledger" for this folder will be reading
  Russian source text for one contiguous five-minute stretch of the lesson
  (19:37 – 24:32).
- If a downstream process re-authors or re-verifies from this ledger, it inherits
  the defect without an error surface.

## Suggested follow-up (not performed)

Re-extract `S-017`–`S-020` in English and re-publish the ledger, or record an
explicit exemption. Nothing in the current pipeline will surface this on its own.