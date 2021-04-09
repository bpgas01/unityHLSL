@echo off
git add .
set /p commit="Add a commit message: "
git commit -m %commit%
git push
