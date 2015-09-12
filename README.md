# SwitchvoxAPI
SwitchvoxAPI is a C# library that abstracts away the complexity of interfacing with the Digium Switchvox XML API.

All [Methods](http://developers.digium.com/switchvox/wiki/index.php/WebService_methods) supported by Switchvox are represented as objects derived from a common class, `RequestMethod`. All `RequestMethod` objects are stored under a namespace path matching the method names used in the actual API Requests (e.g. [`switchvox.extensions.getInfo`](http://developers.digium.com/switchvox/wiki/index.php/Switchvox.extensions.getInfo)). This makes it easy to cross reference between existing documentation and SwitchvoxAPI.

```c#
var request = new SwitchvoxRequest("https://phones.mycoolsite.com", "username", "password");
var response = request.Execute(new Switchvox.Extensions.GetInfo());
```

If you wish to access the [User API](http://developers.digium.com/switchvox/wiki/index.php/WebService_methods#User_Section), you will need to pass a user's extension details to `SwitchvoxRequest`. Otherwise, enter a set of admin credentials.

Methods that contain a number of optional parameters (such as [`switchvox.call`](http://developers.digium.com/switchvox/wiki/index.php/Switchvox.call)) can also be invoked with as many or few arguments as you like:

```c#
request.Execute(new Switchvox.Call("12345678", "100", "1000");
```
  
```c#
request.Execute(new Switchvox.Call("12345678", "100", "1000", timeout: 100, callerIdName: "Mom");
```
  
  `SwitchvoxResponse` objects primarily serve as loose wrappers around an underlying `XDocument`, implementing a number of helper methods that wrap common LINQ queries including retrieving elements with a certain tag, or retrieving certain tag attributes.
  
```c#
var name = response.GetAttribute("extension", "first_name")
```

Due to the sheer scale of Digium's Switchvox API, only the most commonly used API Methods have been implemented (read: methods I have had to use). Methods that are not yet implemented are defined with the default accessibility (internal) and so will not mess up IntelliSense when including SwitchvoxAPI in projects.

If you wish to implement an undefined Method, three things have to be done:

1. The Method object must derive from `RequestMethod`
2. The Method constructor must pass the [API Method Name](http://developers.digium.com/switchvox/wiki/index.php/WebService_methods) to the base constructor
3. A call to method `SetXml()` must be made, passing the XML to use for the Method (typically an `XElement`, `List<XElement>` or `null`)

```c#
public class DoSomething : RequestMethod
{
    public DoSomething : base("switchvox.doSomething")
    {
        SetXml(null);
    }
}
```

Existing Methods such as `Switchvox.Extensions.GetInfo` or `Switchvox.CurrentCalls.GetList` can also be referred to see how Methods are defined. If you do implement a Method that is not yet defined, feel free to request merging your additions with SwitchvoxAPI to bring it a little bit closer to completion!
