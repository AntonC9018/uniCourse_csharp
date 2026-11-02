# Recording slip: the sum is first announced as 14, then corrected to 19

- At 11:53–11:57 (t=713–717s) the recording says the call is replaced by the sum
  "это будет 14" and immediately self-corrects: "это будет 14 буде 19 то есть этот
  выв..." (transcript.md lines 539–541, pinned at
  4814706ae2fafbc797829239a33afc2eeefa29e8).
- 6 + 8 + 5 = 19; 14 matches no partial sum of these values, so the intended value
  is 19 and the word 14 is a spoken slip.
- The note records the slip verbatim at [00:11:48]
  (https://www.youtube.com/watch?v=VhA2OupAYRc&t=708s): "The recording momentarily
  says 14, then corrects the total to 19: `6 + 8 + 5 = 19`." Downstream anchors
  (trace step 10, the returned-value section, and the `foreach` closing sample
  printing 19) all rely on the corrected value.
- Both workers saw this independently: the author captured the slip while writing the
  trace section (run 175, codex session
  01a0fdc9-40c2-72e1-98e2-e7872b17ba4d); the verifier re-checked it against transcript
  lines 713–719 and accepted it (run 176, opencode session
  ses_f022c5cc5ffemvQYAgHcZGI7y2, factual_verification).
