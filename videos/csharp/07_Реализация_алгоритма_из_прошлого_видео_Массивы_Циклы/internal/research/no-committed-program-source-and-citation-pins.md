# No committed program exists; how the note's citations were pinned instead

- This folder's note was authored from scratch: the author's first read of
  notes.en.md in the checkout failed (file absent) and git history for the path was
  empty — consistent with the work order's `first_notes: true`, `output_generation: 1`
  (english_authoring, run 175, codex session
  01a0fdc9-40c2-72e1-98e2-e7872b17ba4d).
- No inspected commit holds any of the on-screen summing programs. Verified twice:
  the author's `git_read_file`/history inspection, and the verifier's
  `git ls-tree -r --name-only bac4adf5...` over this folder, which returns only
  internal/teaching-points.json, internal/transcript.md, internal/transcript.vtt,
  internal/video.json, notes.en.md. The diff 4814706→bac4adf for this folder adds
  exactly the two authored files and nothing else.
- Consequence baked into the note: every code block is labeled "Simplified video
  reconstruction, supported by the [immutable transcript][recorded-transcript], rather
  than an exact committed program" and is explicitly "not claimed as independently
  compiled or executed tests". The zero-value prints, the count-mismatch diagnostic,
  and the `foreach` result are stated as reconstruction expectations, not execution
  results.
- Transcript citation pinned at 4814706ae2fafbc797829239a33afc2eeefa29e8 ("Acquire
  course transcripts"), the commit that introduced internal/transcript.md. The author
  proved the pin matches the working copy (read the blob at that commit and compared
  it with the checkout file); the verifier independently resolved the 40-hex commit
  and confirmed the file exists at it. The transcript is byte-identical at the later
  verified note commit bac4adf51ab0a57b4841bb11edc236e8f6fc1687, so the pin needed no
  update.
- Lab links use relative document paths (`../../../labs/1_basic/...`) — no commit
  citation, per the contract that exercise links identify lab documents, not commits
  of the recorded program. The note states this explicitly. The verifier checked the
  lab content claims against the blobs at bac4adf5 (12_algorithms_code.md tasks:
  algorithm-only functions, caller-created arrays, while+for versions, optional
  assertions deferred; 05_functions.md / 10_basic.md carry the argument-copy and
  result-storage practice).
- Code-history trail decision: the note ends with a "Recorded code history" trail of
  video states (timestamped), not repository commits, because no commit contains the
  taught program text — padding with an unrelated commit would have been worse than
  the state list.
- Author self-checks recorded before completion: 100 coverage IDs in order, every
  anchor heading present, every YouTube timestamp link internally consistent, every
  code section carrying the transcript citation, no frames/images/scenes paths, no
  authored disclaimer, immutable transcript blob == working copy.
