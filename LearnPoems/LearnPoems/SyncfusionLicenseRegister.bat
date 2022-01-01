@echo on&setlocal
    setlocal enableextensions disabledelayedexpansion

	::Arguments of either PreBuild or PostBuild
	set buildType=%1
	::License key replacement file
	set sourceFile=%2
	
	::Replacement string
    set DummyKey=##SyncfusionLicense##
	set LicenseKey=%SyncfusionLicenseKey%
	
	::Replacement statement
	if NOT "%LicenseKey%" == "" (
		if "%buildType%" == "PostBuild" (
			for /f "delims=" %%i in ('type "%sourceFile%" ^& break ^> "%sourceFile%" ') do (
			set "line=%%i"
			setlocal enabledelayedexpansion
			>>"%sourceFile%" echo(!line:%LicenseKey%=%DummyKey%!
			endlocal
		))
		if "%buildType%" == "PreBuild" (
		for /f "delims=" %%i in ('type "%sourceFile%" ^& break ^> "%sourceFile%" ') do (
			set "line=%%i"
			setlocal enabledelayedexpansion
			>>"%sourceFile%" echo(!line:%DummyKey%=%LicenseKey%!
			endlocal
		))
	)