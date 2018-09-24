1oT UWP

This application was written to get first glimpse of whats possible with 1oT API. In this project, we are using RestSharp for communication with 1oT API together with basic samples. It is writen in Universal Windows Platform so in theory it could eventually run anywhere Windows does run. For LocalStorage we are using Newtonsoft.Json and for UI and UX we are using Microsoft Toolkit solutions. There is background tasks prepered for future when based on 1oT sim card activity, a push notification could be sent locally.

Features
* 1oT Blog webview 
* Settings page where you can enter your credentials
* Capability to manage SIM cards data limit, activate and deactivate them


Minimum requierments
* Windows 10, version 1803 (April 2018 Update) or later - https://support.microsoft.com/en-au/help/4028685/windows-10-get-the-update

For developing
* Visual Studio 2017 (You will also need the optional Universal Windows Platform development workload)

Dev notes
* Temporary workaround for loading Manage SIM page on not supported Windows 10 installations removes the FontFamily="Segoe MDL2 Assets" usage.
* logs are saved in the folder:  C: \ Users \ [user_name] \ AppData \ Local \ Packages \ [package_family_name] \ LocalState \ MetroLogs