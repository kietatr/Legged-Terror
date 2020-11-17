:: For expediting the git push process (on a Windows machine)
:: Usage:
::     git-push.bat "Your git commit message"

@echo off
git pull
git add -A 
git commit -m %1
git push
