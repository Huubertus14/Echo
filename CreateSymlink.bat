@echo off

:: BatchGotAdmin
:-------------------------------------
REM 
    IF "%PROCESSOR_ARCHITECTURE%" EQU "amd64" (
>nul 2>&1 "%SYSTEMROOT%\SysWOW64\cacls.exe" "%SYSTEMROOT%\SysWOW64\config\system"
) ELSE (
>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"
)

REM
if '%errorlevel%' NEQ '0' (
    echo Requesting administrative privileges...
    goto UACPrompt
) else ( goto gotAdmin )

:UACPrompt
    echo Set UAC = CreateObject^("Shell.Application"^) > "%temp%\getadmin.vbs"
    set params= %*
    echo UAC.ShellExecute "cmd.exe", "/c ""%~s0"" %params:"=""%", "", "runas", 1 >> "%temp%\getadmin.vbs"

    "%temp%\getadmin.vbs"
    del "%temp%\getadmin.vbs"
    exit /B

:gotAdmin
    pushd "%CD%"
    CD /D "%~dp0"

:: Creating symlink Unity project to test multiplayer easily
::
mkdir "SymLinkEcho"
:: Symlinking these important folders...
:: 
mklink /d "SymLinkEcho/Assets" "..\Assets"
mklink /d "SymLinkEcho/Packages" "..\Packages"
mklink /d "SymLinkEcho/ProjectSettings" "..\ProjectSettings"
:: To save yourself long unity reimport time, lets copy the Library dir
:: 
robocopy "Library" "SymLinkEcho/Library/" /mir /e /NFL /NDL
:: Add a symlink account ID override (otherwise networking will mess up)
::
if not exist "Echo/AccountGUID.txt" echo MySymlinkID_%computername% > "SymLinkEcho/AccountGUID.txt"
::
echo Done creating/updating symlinked project!
pause
