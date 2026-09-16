#!/usr/bin/env bash
# Post-deploy smoke tests for the C# course site (CI-11).
#
# Verifies at the public Pages URL (default below, override with BASE_URL):
#   1. root redirect -> /ru/labs/basic/install/
#   2. representative Russian route (root lesson)
#   3. another representative Russian route (design labs)
#   4. Pagefind search (page markup references pagefind + index JS reachable)
#
# Bounded retry: Pages deployments propagate with delay, so every check
# retries with a fixed bound (SMOKE_RETRIES x SMOKE_SLEEP_S, defaults
# 30x10s = ~5min). Fails (exit 1) on first check that never passes.
# No network side-effects besides GET requests; safe to re-run.
#
# Usage:
#   BASE_URL=https://AntonC9018.github.io/uniCourse_csharp \
#     bash scripts/smoke-test.sh
#   SMOKE_RETRIES=5 SMOKE_SLEEP_S=2 bash scripts/smoke-test.sh  # faster local
#
# Local validation against a built dist/:
#   python3 -m http.server 8080 --directory /tmp/site-out/dist &
#   BASE_URL=http://127.0.0.1:8080/uniCourse_csharp \
#     SMOKE_RETRIES=3 SMOKE_SLEEP_S=1 bash scripts/smoke-test.sh
set -euo pipefail

BASE_URL="${BASE_URL:-https://AntonC9018.github.io/uniCourse_csharp}"
RETRIES="${SMOKE_RETRIES:-30}"
SLEEP_S="${SMOKE_SLEEP_S:-10}"

BASE_URL="${BASE_URL%/}"
PASS=0
FAIL=0

log() { printf '%s\n' "$*"; }
ok() { PASS=$((PASS + 1)); log "PASS: $*"; }
fail() { FAIL=$((FAIL + 1)); log "FAIL: $*"; return 1; }

# fetch <url> -> prints body to stdout; returns nonzero on HTTP error.
fetch() {
  curl -fsSL --retry 0 --max-time 30 --user-agent "course-smoke/1.0" "$1"
}

# check_contains <label> <url> <expected-substring>
# Retries up to $RETRIES; passes when body contains the substring.
check_contains() {
  local label="$1" url="$2" needle="$3" attempt=1 body=""
  while [ "$attempt" -le "$RETRIES" ]; do
    if body="$(fetch "$url" 2>/dev/null)"; then
      if printf '%s' "$body" | grep -qF "$needle"; then
        ok "$label ($url contains $(printf '%s' "$needle" | head -c 60))"
        return 0
      fi
      log "attempt $attempt/$RETRIES: $label reachable but missing expected content; retrying in ${SLEEP_S}s"
    else
      log "attempt $attempt/$RETRIES: $label not reachable ($url); retrying in ${SLEEP_S}s"
    fi
    attempt=$((attempt + 1))
    [ "$attempt" -le "$RETRIES" ] && sleep "$SLEEP_S"
  done
  fail "$label ($url never contained $(printf '%s' "$needle" | head -c 60) after $RETRIES attempts)"
}

# check_status200 <label> <url>  (binary-safe, e.g. images)
check_status200() {
  local label="$1" url="$2" attempt=1 code=""
  while [ "$attempt" -le "$RETRIES" ]; do
    code="$(curl -s -o /dev/null -w '%{http_code}' --max-time 30 --user-agent "course-smoke/1.0" "$url" 2>/dev/null || true)"
    if [ "$code" = "200" ]; then
      ok "$label ($url -> 200)"
      return 0
    fi
    log "attempt $attempt/$RETRIES: $label got HTTP $code ($url); retrying in ${SLEEP_S}s"
    attempt=$((attempt + 1))
    [ "$attempt" -le "$RETRIES" ] && sleep "$SLEEP_S"
  done
  fail "$label ($url never returned 200 after $RETRIES attempts; last: $code)"
}

log "smoke: BASE_URL=$BASE_URL retries=$RETRIES sleep=${SLEEP_S}s"

# 1. Root redirect: / follows to the default Russian lab. Astro emits a
# redirect page, so assert the served root references the target slug.
check_contains "root redirect" \
  "$BASE_URL/" \
  "/ru/labs/basic/install/" || true

# 2. Representative Russian route (root lesson).
check_contains "ru route (install)" \
  "$BASE_URL/ru/labs/basic/install/" \
  "Установка .NET" || true

# 3. Representative Russian route (design labs).
check_contains "ru route (field-mask)" \
  "$BASE_URL/ru/labs/design/field-mask/" \
  "Field Mask" || true

# 4. Search: representative page references Pagefind (proves the
# pagefind:true build + indexed routes are served).
check_contains "search markup (pagefind)" \
  "$BASE_URL/ru/labs/basic/install/" \
  "pagefind" || true

# 4b. Search index JS reachable. Astro Starlight/Pagefind emits the index
# under pagefind/ (observed in dist/: pagefind/pagefind.js); older layouts
# use _pagefind/. Accept either (observed path first to avoid wasting the
# retry budget on the legacy path).
PAGEFIND_OK=0
for p in "pagefind/pagefind.js" "_pagefind/pagefind.js"; do
  if check_status200 "search index ($p)" "$BASE_URL/$p"; then
    PAGEFIND_OK=1
    break
  fi
done
[ "$PAGEFIND_OK" = "1" ] || true

log "smoke: $PASS passed, $FAIL failed"
[ "$FAIL" -eq 0 ]
