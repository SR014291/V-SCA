A WinAppDriverWindowsObjectHandler package helps in Handling Windows Automation using WinAppDriver

Usage:

1. Include the namespace "WinAppDriverWindowsObjectHandler"
Eg: using WinAppDriverWindowsObjectHandler;

2. Create an object for WindowsObjectHandler class.
Eg: WindowsObjectHandler winobjHandler = new WindowsObjectHandler();

Process: 
1. WinAppDriver on new instance of Windows Application
	a. launchWinAppDriver:							Launches WinAppDriver
	b. initializeWinAppDriver:							Initializes WinAppDriver and launches Windows application

2. WinAppDriver on already running instance of Windows Application
	a. launchWinAppDriver:							Launches WinAppDriver
	b. getWindowHandle:								Get Window Handler of an Application which is already launched
	c. initializeWinAppDriverOnRunningApp:		Initializes WinAppDriver on already running App