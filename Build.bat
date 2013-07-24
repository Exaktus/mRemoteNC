@echo off

set RAR="C:\Program Files\WinRAR\WinRAR.exe"
set VCVARSALL="%ProgramFiles(x86)%\Microsoft Visual Studio 10.0\VC\vcvarsall.bat"
set DEVENV="devenv.exe"
set ISS="%ProgramFiles(x86)%\Innoj Setup 5\iscc.exe"

call %VCVARSALL% x86

rmdir /s /q "%~dp0\mRemoteV2\bin" > nul 2>&1
rmdir /s /q "%~dp0\mRemoteV2\obj" > nul 2>&1

if exist "%~dp0\mRemoteV1\bin" goto ERROR_RMDIR
if exist "%~dp0\mRemoteV1\obj" goto ERROR_RMDIR
goto NOERROR_RMDIR

:ERROR_RMDIR
echo.
echo Could not clean output directories.
echo.
echo Build process failed.
echo.
goto END

:NOERROR_RMDIR

echo Building release version...
%DEVENV% "%~dp0\mRemoteV2.sln" /build "Release"

echo Building portable version...
%DEVENV% "%~dp0\mRemoteV2.sln" /build "Release Portable"

mkdir "%~dp0\Release" > nul 2>&1
del /f /q "%~dp0\Release\*.*" > nul 2>&1

if not exist %ISS% goto ERROR_ISS
goto NOERROR_ISS

:ERROR_ISS
echo.
echo Inno Setup not found.
echo.
echo Build process failed.
echo.
goto END

:NOERROR_ISS

%ISS% "%~dp0\Installer\mRemoteNC.iss"

for /F %%a in ('dir /b .\Release\mRemoteNC-Installer-*.exe') do set FileName=%%~na
set PortableZipName=%FileName:Installer=Portable%.zip
set ReleaseZipName=%FileName:-Installer=%.zip
set InstallerZipName=%FileName%.zip
set InstallerReleaseName=%FileName%.exe
set AioName=%FileName%-AIO.zip

echo Creating portable ZIP file...
del /f /q "%~dp0\Release\%PortableZipName%" > nul 2>&1
%RAR% a -ibck -m5 -r -ep1 -afzip -inul "%~dp0\Release\%PortableZipName%" "%~dp0\mRemoteV2\bin\Release Portable\*.*"
%RAR% a -ibck -m5 -r -ep1 -afzip -inul "%~dp0\Release\%PortableZipName%" "%~dp0\Installer\Dependencies\*.*"

echo Creating release ZIP file...
del /f /q "%~dp0\Release\%ReleaseZipName%" > nul 2>&1
%RAR% a -ibck -m5 -r -ep1 -afzip -inul "%~dp0\Release\%ReleaseZipName%" "%~dp0\mRemoteV2\bin\Release\*.*"
%RAR% a -ibck -m5 -r -ep1 -afzip -inul "%~dp0\Release\%ReleaseZipName%" "%~dp0\Installer\Dependencies\*.*"

echo Creating installer ZIP file...
del /f /q "%~dp0\Release\%InstallerZipName%" > nul 2>&1
%RAR% a -ibck -m5 -r -ep1 -afzip -inul "%~dp0\Release\%InstallerZipName%" "%~dp0.\Release\%InstallerReleaseName%"

echo Creating AIO ZIP file...
del /f /q "%~dp0\Release\%AioName%" > nul 2>&1
%RAR% a -ibck -m5 -r -ep1 -afzip -inul "%~dp0\Release\%AioName%" "%~dp0\Release\%PortableZipName%"
%RAR% a -ibck -m5 -r -ep1 -afzip -inul "%~dp0\Release\%AioName%" "%~dp0\Release\%InstallerReleaseName%"

copy "%~dp0\Release\%AioName%" "%~dp0\Release\mRemoteNC-AIO-Latest.zip"
copy "%~dp0\Release\%PortableZipName%" "%~dp0\Release\mRemoteNC-Portable-Latest.zip"
copy "%~dp0\Release\%InstallerReleaseName%" "%~dp0\Release\mRemoteNC-Installer-Latest.exe"

echo.
echo Finished!
echo.

:END
pause