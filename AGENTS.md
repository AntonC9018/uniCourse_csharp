## When asked to maintain after edits

Maintenance is handled by the `./course_maintenance`, read its `AGENTS.md`.

When edits are in, look at the latest revision hash in `revision.json`.
Create it if doesn't exist.

Single language (`ru`) only, no translation rounds.

Always preview first:
`python3 course_maintenance/maintain.py --check <paths>`
If the preview looks right, apply:
`python3 course_maintenance/maintain.py <paths>`
File args fix only that file and never trigger renames.
Dir args (e.g. `labs`) also close `NN_` numbering gaps: pass files you edited;
pass a lab dir when files were added, removed, or reordered.

If the revision json didn't exist, run maintenance on all labs.
If it did, see which files have changed since that revision,
and run maintenance on those files.

Commit once you're done.
