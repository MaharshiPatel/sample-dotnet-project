#!/usr/bin/env bash
# Run once after cloning: ./scripts/setup-hooks.sh
# Configures git to use the shared .githooks directory.
set -euo pipefail

REPO_ROOT="$(git rev-parse --show-toplevel)"
git -C "$REPO_ROOT" config core.hooksPath .githooks
chmod +x "$REPO_ROOT/.githooks/pre-commit"
echo "Git hooks configured. Pre-commit secret scan is now active."
