# LiveSplit is crashing, what can I do?

## Check the Windows Event Viewer

- Open up `eventvwr.exe`
- Go to Windows Logs > Application
- Try to find anything related to LiveSplit.exe

## Create CrashDumps for LiveSplit

There are some errors that will not be visible inside Windows Event Viewer. If you are experiencing app crashes and haven't found any clue, there is a built in solution inside Windows.

- Open up `regedit.exe`
- Add a new key 
`HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Windows Error Reporting\LocalDumps\LiveSplit.exe`.
- Add a 32-bit DWORD value named `DumpCount` with value `5`.
- Add a String value named `DumpFolder` with value `C:\CrashDumps` (you can choose any path you want).

Now when everything is set up correctly (no restart required), next time LiveSplit will crash, you will find a file called `LiveSplit.exe.xxxx.dmp` inside the folder configured above.

You will find dump files for the 5 recent crashes. Zip the most recent one and upload it to any location for further analysis.

If you want to remove automatic dump creation, just delete the key created before.

Alternatively you can use this prepared script [LiveSplit.reg](./LiveSplit.reg?raw=1)