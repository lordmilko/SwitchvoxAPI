# SwitchvoxAPI

[![Build status](https://img.shields.io/appveyor/ci/lordmilko/switchvoxapi.svg)](https://ci.appveyor.com/project/lordmilko/switchvoxapi)
[![NuGet](https://img.shields.io/nuget/v/SwitchvoxAPI.svg)](https://www.nuget.org/packages/SwitchvoxAPI/)

SwitchvoxAPI is a C# library that abstracts away the complexity of interfacing with the Digium Switchvox XML API.

## Installation

### NuGet

```powershell
Install-Package SwitchvoxAPI
```

### Manual

1. Download the [latest build](https://github.com/lordmilko/SwitchvoxAPI/releases/latest)
2. Right click **SwitchvoxAPI.zip** -> **Properties**
3. On the *General* tab, under *Security* select **Unblock**
4. Unzip the file
5. Add a reference to *SwitchvoxAPI.dll* to your project, or import the *SwitchvoxAPI* module into PowerShell. Alternatively, you can run the included **SwitchvoxAPI.cmd** file to open a prompt and import the SwitchvoxAPI module for you.

## Usage

```c#
var client = new SwitchvoxClient("https://phones.mycoolsite.com", "username", "password");
var extensions = client.Extensions.GetInfo();
```

If you are incorporating SwitchvoxAPI into a website, you can store your connection details in your web.config file's AppSettings tag.

```xml
<configuration>
    <appSettings>
        <add key="ServerUrl" value="https://phones.mycoolsite.com"/>
        <add key="UserName" value="username"/>
        <add key="Password" value="password"/>
    </appSettings>
</configuration>
```
```c#
var client = new SwitchvoxClient(); //Automatically retrieves your settings from your web.config file
```

All methods implemented by SwitchvoxAPI deserialize the XML returned by Switchvox, allowing you to interface with the phone system in pure .NET.

```c#
var names = client.Extensions.GetInfo().Select(e => e.DisplayName);
```

All [Methods](http://developers.digium.com/switchvox/wiki/index.php/WebService_methods) supported by Switchvox are represented as methods in an object hierarchy similar to the method name, with `SwitchvoxClient` as the root level node. e.g. `client.Extensions.GetInfo()` for [`switchvox.extensions.getInfo`](http://developers.digium.com/switchvox/wiki/index.php/Switchvox.extensions.getInfo), `client.Call()` for [`switchvox.call`](http://developers.digium.com/switchvox/wiki/index.php/Switchvox.call), etc. This makes it easy to cross reference between existing documentation and SwitchvoxAPI.

**Note: From Switchvox 6.7.1, TLS 1.0 and 1.1 are disabled. If you are running a version of the .NET Framework that does not support TLS 1.2 by default, you must manually specify to use TLS 1.2**

```c#
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
```

If you wish to access the [User API](http://developers.digium.com/switchvox/wiki/index.php/WebService_methods#User_Section), you will need to pass a user's extension details to `SwitchvoxClient`. Otherwise, enter a set of admin credentials (with API Access Permissions).

Methods that contain a number of optional parameters (such as [`switchvox.call`](http://developers.digium.com/switchvox/wiki/index.php/Switchvox.call)) can also be invoked with as many or few arguments as you like:

```c#
client.Call("12345678", "100", "1000");
```
  
```c#
client.Call("12345678", "100", "1000", timeout: 100, callerIdName: "Mom");
```

Due to the sheer scale of Digium's Switchvox API, only the most commonly used API Methods have been implemented (read: methods I have had to use). Methods that are not yet implemented are defined with the default accessibility (internal) and so will not mess up IntelliSense when including SwitchvoxAPI in projects.

If you wish to implement an undefined Method, three things have to be done:

1. Create an object hierarchy under `SwitchvoxClient` for the method you wish to implement
2. Implement a method that constructs the XML required for the method request
3. Define one or more objects to deserialize the response against

Existing Methods such as `SwitchvoxAPI.Extensions.GetInfo` or `SwitchvoxAPI.CurrentCalls.GetList` can also be referred to to see how Methods are defined. If you do implement a Method that is not yet defined, feel free to request merging your additions with SwitchvoxAPI to bring it a little bit closer to completion!

## Supported Methods
* Switchvox
  * **Call**
  * CallLogs
      * **Search**
  * CallQueueLogs
      * **Search**
  * CallQueues
      * **GetCurrentStatus**
  * CurrentCalls
      * **GetList**
	  * **HangUp**
  * ExtensionGroups
      * **GetInfo**
      * **GetList**
  * Extensions
      * **GetInfo**
      * **Search**
  * IVR
      * GlobalVariables
          * **Add**
          * **GetList**
          * **Remove**
          * **Update**
  * Users
      * **Call**
      * CallQueues
          * **GetTodaysStatus**
      * **GetMyInfo**

## Examples

### Retrieve A Queue's Call Logs

```c#
//Get Today's Queue Status
var queueStatus = userClient.Users.CallQueues.GetTodaysStatus("1234", "5678");

//Get the account IDs of the queue
var queueAccountIds = queueStatus.Members.Select(m => m.AccountId).ToArray()

//Retrieve today's call logs
var callLogData = client.CallLogs.Search(DateTime.Today, DateTime.Today.AddDays(1), CallLogSearchCriteria.AccountId, accountIds, itemsPerPage: 10000);
```

**Note: due to what appears to be a bug in the Switchvox system, filtering by caller ID name may not return all results. If you observe executing a search with no filter returns more results that match a specified name, you may want to use LINQ to filter the results instead of filtering the request server side**

## PowerShell

_For entertainment purposes only_

All cmdlets can be invoked with either a Svx or Svox noun prefix.

If you are attempting to use these seriously, please open an issue for any troubles you may have.


The following cmdlets are currently supported. A basic usage example is included for each cmdlet. To view additional details about supported parameters, run `Get-Help <cmdlet>`

Session

| Cmdlet                     | Method                           | Example                                                |
| -------------------------- | -------------------------------- | ------------------------------------------------------ |
| Connect-SvxServer          | N/A                              | Connect-SvxServer phones.mycoolsite.com                |
| Disconnect-SvxServer       | N/A                              | Disconnect-SvxServer                                   |
| Get-SvxClient              | N/A                              | Get-SvxClient                                          |

Actions

| Cmdlet                     | Method                           | Example                                                |
| -------------------------- | -------------------------------- | ------------------------------------------------------ |
| Add-SvxIVRVariable         | ivr.globalVariables.add          | Add-SvxIVRVariable newVariable 10                      |
| Disconnect-SvxCall         | currentCalls.hangUp              | Get-SvxCall &#124; Disconnect-SvxCall # XD             |
| Invoke-SvxCall             | call                             | Get-SvxExtension 1000 &#124; Invoke-SvxCall 1234       |
| Remove-SvxIVRVariable      | ivr.globalVariables.getList      | Get-SvxIVRVariable *blah* &#124; Remove-SvxIVRVariable |
| Set-SvxIVRVariable         | ivr.globalVariables.update       | Get-SvxIVRVariable *blah* &#124; Set-SvxIVRVariable 3  |

Data

| Cmdlet                     | Method                           | Example |
| -------------------------- | -------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Get-SvxCall                | currentCalls.getList             | Get-SvxCall<br>Get-SvxCall *bob*<br>Get-SvxCall *1234*                                                                                                        |
| Get-SvxCallLog             | callLogs.search                  | Get-SvxCallLog<br>Get-SvxCallLog *bob*                                                                                                                        |
| Get-SvxCallQueueLog        | callQueueLogs.search             | Get-SvxCallQueueLog -QueueAccountID 1234<br>Get-SvxExtension -Type CallQueue &#124; Get-SvxCallQueueLog                                                       |
| Get-SvxCallQueueStatus     | callQueues.getCurrentStatus      | Get-SvxCallQueueStatus -QueueAccountID 1234<br>Get-SvxExtension -Type CallQueue &#124; Get-SvxCallQueueStatus                                                 |
| Get-SvxUserCallQueueStatus | users.callQueues.getTodaysStatus | Get-SvxUserCallQueueStatus -QueueAccountId 1234 -UserAccountId 5678<br>Get-SvxExtension -Type CallQueue &#124; Get-SvxUserCallQueueStatus -UserAccountId 5678 |
| Get-SvxExtension           | extensions.search                | Get-SvxExtension *bob*<br>Get-SvxExtension 1000                                                                                                               |
| Get-SvxExtensionGroup      | extensionGroups.getInfo          | Get-SvxExtensionGroup *blah*                                                                                                                                  |
| Get-SvxExtensionSettings   | extensions.settings.getInfo      | Get-SvxExtensionSettings                                                                                                                                      |
| Get-SvxIVRVariable         | ivr.globalVariables.getList      | Get-SvxIVRVariable *blah*                                                                                                                                     |

SwitchvoxAPI also supports GoSvox, a method of storing encrypted credentials in your PowerShell session for seamless switching between one or more severs. For more information on GoSvox, see the [equivalent documentation under PrtgAPI](https://github.com/lordmilko/PrtgAPI/wiki/Store-Credentials)
