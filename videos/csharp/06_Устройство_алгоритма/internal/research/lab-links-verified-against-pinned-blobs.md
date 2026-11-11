# Lab links: two relative document paths, verified against pinned blobs

- The note cites two labs by relative path from `videos/csharp/06_Устройство_алгоритма/`:
  `../../../labs/1_basic/11_algorithms.md` and `../../../labs/1_basic/12_algorithms_code.md`.
  Both were confirmed present with `git ls-tree` / `git show` at the pinned commits in
  each pass: run 46 against `1d3c1d4d0a2d3833d42ad65a1330fce2ee1e9004`, run 83 against
  `7b9b21fffcda34536e140829129f3c581213c8a8`.
- `11_algorithms.md` is the lab that identifies this recording: its video line is
  <https://www.youtube.com/watch?v=C6plSGSYuyc&list=PL4sUOB8DjVlVVw9Yx_tUO7fRPDYeaACXD&index=5>.
  It also carries a cross-course pointer to the same exercise in the C++/ASD course:
  <https://github.com/AntonC9018/uniCourse_dataStructuresAndAlgorithms/blob/master/ru/labs/common/04_algorithm.md>
  — useful when the same algorithm task is needed in another course folder.
- `12_algorithms_code.md` belongs to a different recording
  (<https://youtu.be/VhA2OupAYRc>), which is the evidence behind the note's wording "a
  later practice assignment, not an implementation completed in this video". Its
  requirement list matches the note's "Further practice, not taught in this video"
  paragraph verbatim in substance: a function containing only the algorithm logic and no
  printing, arrays (including the output array) created in Main and passed in, the
  algorithm writing its answer into the output array, `while` and `for` versions, and
  optional contract checks via `Debug.Assert` or `ArgumentException`. It also lists the
  extra practice (count values above 5, maximum, two maxima, first n primes, Fibonacci),
  which the note repeats as optional.
- `factual_verification`, run 46, opencode session `ses_f092fc46affecGAcYVE1HgIHq3`,
  finding F-004 (`unsupported_claim`) was raised only to record that this claim had been
  checked against the pinned blob of `labs/1_basic/12_algorithms_code.md` and that no
  change was requested; the fixer (run 47, session `ses_f092c0fddffe3zUovPuIN8GK41`)
  marked it `no_change_required` and left the text intact. The later verifier (run 83)
  re-checked the same blobs and approved.
- Both links are document links, not commit citations, which is correct for exercise
  references: the lab pages are not evidence for a code state taught in this video.

Re-checked 2026-10-11 in this checkout by reading both lab files directly.
