# Memory-layout drawing is illustrative, not a layout guarantee

- Ledger TP-049 (520519–534829 ms, `#an-out-parameter-writes-into-the-caller`) records the explanation that local variables are laid out one after another — for example at addresses 10000 and then 10008 — because each occupies its own cells.
- The verifier checked the spoken numbers against the transcript: at t=651s–663s the instructor says variable B's cell "создалась по адресу 10.4" ("was created at address 10.4") while describing uninitialized references, and at t=520s–534s the drawing uses the 10000/10008 pair.
- The note (t=520s and t=534s links) deliberately widens the claim into a disclaimer: these numbers "illustrate separate locations, not guaranteed int sizes or a fixed runtime layout," and the next location "changes according to the space allocated, and the direction of layout is an implementation detail."
- Why the wording matters: taken literally, the video's drawing could be read as promising that `int` occupies 8 bytes or that locals grow upward. The note's framing keeps the teaching point (separate storage, addresses exist) while dropping the false precision.
- Traceability: run 141 / session `ses_f065bf2acffeZV0OzYZjByxp91` inspected `internal/transcript.md` t=520s, t=534s, t=651s–663s against TP-049 and approved the note as written.
