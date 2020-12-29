@echo off

set scriptDir=\\GASVCT01\APPS\Installation
set targetDir=APPS\Installation
set targetServer=GASVCT01

set assemblyName=GA.BDC.Console.PAPFeedback.TaskExecutor.exe
set binFolder=GA\BDC\Console\PAPFeedback\TaskExecutor\bin


mode con cols=132

rem cscript.exe //H:CScript

xcopy %assemblyName% \\%targetServer%\%targetDir%\%binFolder%\ /D /R /Y

xcopy *.dll \\%targetServer%\%targetDir%\%binFolder%\ /D /R /Y
xcopy *.pdb \\%targetServer%\%targetDir%\%binFolder%\ /D /R /Y


pause


