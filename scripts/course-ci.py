"""Validate this course using the selection in course-ci.json."""

import argparse
import json
from pathlib import Path
import sys

REPO = Path(__file__).resolve().parent.parent
sys.path.insert(0, str(REPO / "course_maintenance"))

from maintenance.pipeline import run as run_maintenance
from publishing.ci import (
    run_compat_suite,
    run_publishing_check_stage,
    run_site_build_stage,
)
from publishing.projection import validate_out_location


def load_config():
    config = json.loads((REPO / "course-ci.json").read_text(encoding="utf-8"))
    if set(config) != {"maintenance_paths", "run_compatibility_tests"}:
        raise ValueError("expected maintenance_paths and run_compatibility_tests")
    paths = config["maintenance_paths"]
    if not isinstance(paths, list) or not paths:
        raise ValueError("maintenance_paths must be a nonempty list")
    targets = []
    for raw in paths:
        if not isinstance(raw, str) or not raw or Path(raw).is_absolute():
            raise ValueError("maintenance paths must be relative to the repository")
        target = (REPO / raw).resolve()
        if (not target.is_relative_to(REPO) or target == REPO
                or target.is_relative_to(REPO / "course_maintenance")):
            raise ValueError(f"maintenance path must select course documents: {raw}")
        if not target.exists():
            raise ValueError(f"maintenance path does not exist: {raw}")
        targets.append(target)
    if not isinstance(config["run_compatibility_tests"], bool):
        raise ValueError("run_compatibility_tests must be a boolean")
    return targets, config["run_compatibility_tests"]


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--out", help="site build directory outside the repository")
    args = parser.parse_args()
    try:
        targets, compatibility = load_config()
        if args.out:
            problem = validate_out_location(REPO, Path(args.out).resolve())
            if problem:
                raise ValueError(problem)
    except (OSError, ValueError, TypeError) as exc:
        print(f"error: course-ci.json / CI arguments: {exc}", file=sys.stderr)
        return 2

    print("ci: maintenance targets: " + ", ".join(
        str(path.relative_to(REPO)) for path in targets), flush=True)
    code = run_maintenance(targets, root=REPO, check_only=True, quiet=False)
    if code:
        return code
    code = run_publishing_check_stage(REPO)
    if code:
        return code
    if compatibility:
        code = run_compat_suite()
        if code:
            return code
    else:
        print("ci: pipeline compatibility tests disabled by course-ci.json")
    return run_site_build_stage(REPO, args.out)


if __name__ == "__main__":
    sys.exit(main())
