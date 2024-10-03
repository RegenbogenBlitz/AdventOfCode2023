# a bash script that builds a c program in a specific folder
#!/bin/bash
set -euo pipefail

# delete the appexe file if it exists
rm -f appexe
gcc main.c -Wall -o appexe
./appexe