# The lab is an extension list, not a second implementation to describe

`labs/2_design/01_field_mask.md` is committed at the current `HEAD` as blob `6abe848bf5cadc30b9ad6288887240567af4e3ad` (checked 2026-10-14). The retained run-355 tool record does not contain a lab-file read event, so attribute the lab content to this current Git check rather than to that worker. The note’s relative lab link resolves from this video folder to the course-root lab.

- The lab is written in Russian and has no English equivalent. The note therefore links it as
  practice material and does not restate its task text in English.
- The lab's core assignment is six tasks: a domain model with at least five fields including
  `int, string, float, enum`; a database abstraction; a field-mask class "first as bool"; a
  single-field search method; a static printing function driven by a mask; and testing it. The
  recording implements the mask, the printing function and the difference/any/all queries, but no
  database abstraction and no search method.
- Everything else is under "Дальнейшие задания (как минимум 3 на выбор)": a JSON REST API on
  ASP.NET Core, merge-by-mask, copy-by-mask, xunit tests, three mask-combining methods, and
  "Переделайте маску полей на основе битов". The note's decision is to present the database, REST
  API, copy, merge and unit-test items as exercises that the recording does not teach, rather than
  as part of the lesson.
- The lab links "Поэтапный код из видео" to PR #1 and says to look at its commits. That is the same
  three-commit history the note cites; the lab itself is the reason those commits exist in that
  shape. See `example-code-stages.md`.
- Checked by me on 2026-10-10, not by any worker: the lab still lists the bit rewrite as an
  *optional* exercise, even though the recording does implement the bitwise representation. A
  learner who follows the lab order therefore rebuilds the video's final stage as homework. If the
  note ever explains the lab, this mismatch is the thing to mention.
