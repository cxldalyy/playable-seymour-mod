@echo off
setlocal enabledelayedexpansion

:: Settings
set "PATCH_DIR=patches"
set "ORIG_DIR=original"
set "OUT_DIR=efl\x"

echo [+] FFX Seymour EFL Mods Installer
echo ---------------------------------------

for /R "%PATCH_DIR%" %%p in (*.xdelta) do (
    set "PATCH_PATH=%%p"
    
    set "REL_PATH=!PATCH_PATH:%CD%\%PATCH_DIR%\=!"
    
    set "DEST_FILE=!REL_PATH:.xdelta=!"
    
    if exist "%ORIG_DIR%\!DEST_FILE!" (
        echo [+] Installing: !DEST_FILE!
        
        for %%I in ("%OUT_DIR%\!DEST_FILE!") do if not exist "%%~dpI" mkdir "%%~dpI"
        
        .\xdelta3.exe -d -s "%ORIG_DIR%\!DEST_FILE!" "%%p" "%OUT_DIR%\!DEST_FILE!"
    ) else (
        echo [!] SKIP: Original file not found for !DEST_FILE!
    )
)

echo ---------------------------------------
echo [+] Finished
pause