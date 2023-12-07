#!/bin/bash
set -euo pipefail

docker run --rm -v "$PWD":/usr/src/myapp -w /usr/src/myapp rust cargo build --release
docker run --rm -v "$PWD":/usr/src/myapp -w /usr/src/myapp rust ./target/release/myapp