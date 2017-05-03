# SwitchvoxAPI
SwitchvoxAPI is a C# library that abstracts away the complexity of interfacing with the Digium Switchvox XML API.

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
  * ExtensionGroups
      * **GetInfo**
      * **GetList**
  * Extensions
      * **GetInfo**
  * IVR
      * GlobalVariables
          * **Add**
          * **GetList**
          * **Remove**
          * **Update**
  * Users
      * CallQueues
          * **GetTodaysStatus**

## Examples

### Retrieve A Queue's Call Logs

```c#
//Get Today's Queue Status
var queueStatus = userClient.Users.CallQueues.GetTodaysStatus("1234", "5678");

//Get the account IDs of the queue
var queueAccountIds = queueStatus.Members.Select(m => m.AccountId).ToArray()

//Retrieve today's call logs
var callLogData = client.CallLogs.Search(DateTime.Today, DateTime.Today.AddDays(1), CallLogMultiItemSearchData.AccountIds, accountIds, itemsPerPage: 10000);
```
