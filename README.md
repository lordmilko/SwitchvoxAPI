# SwitchvoxAPI
SwitchvoxAPI is a C# library that abstracts away the complexity of interfacing with the Digium Switchvox XML API.

All [methods](http://developers.digium.com/switchvox/wiki/index.php/WebService_methods) supported by Switchvox are represented as objects derived from a common class, `RequestMethod`. All `RequestMethod` objects are stored under a namespace path matching the method names used in the actual API Requests (e.g. [`switchvox.extensions.getInfo`](http://developers.digium.com/switchvox/wiki/index.php/Switchvox.extensions.getInfo)). This makes it easy to cross reference between existing documentation and SwitchvoxAPI.

  ```c#
    var request = new SwitchvoxRequest();
    var response = request.Execute(new Switchvox.Extensions.GetInfo());
  ```
Methods that contain a number of optional parameters (such as [`switchvox.call`](http://developers.digium.com/switchvox/wiki/index.php/Switchvox.call)) can also be invoked with as many or few arguments as you like:

  ```c#
    request.Execute(new Switchvox.Call("12345678", "100", "1000");
  ```
  
  ```c#
    request.Execute(new Switchvox.Call("12345678", "100", "1000", timeout: 100, callerIdName: "Mom");
  ```
  
  `SwitchvoxResponse` objects primarily serve as loose wrappers around an underlying `XDocument`, implementing a number of helpful methods that wrap common LINQ queries including retrieving elements with a certain tag, or retrieving certain tag attributes.
  
  ```c#
    var name = response.GetAttribute("extension", "first_name")
  ```
