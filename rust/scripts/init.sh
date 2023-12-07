#!/bin/bash
set -euo pipefail

docker run --rm -v "$PWD":/usr/src/myapp -w /usr/src/myapp rust cargo init