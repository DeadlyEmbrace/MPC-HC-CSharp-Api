# MPC-HC-CSharp-Api

## Install
Install using nuget
`Install-Package MPC-HC.Domain`

## Features
* Play
* Pause
* Stop
* Set volume level
* Get all info from `/variables.html`
* Open media file
* UnMute
* Mute
* ToggleMute
* Next
* Prev
* SetPosition


## Usage

As of now, this is how you create an instace of the `commandService` and send request to the web interface.

```csharp
var mpcHomeCinema = new MPCHomeCinema("http://localhost:13579");
var result = await mpcHomeCinema.PlayAsync();
if(result.ResultCode == ResultCode.Ok){
  //we are good
  Console.WriteLine($"{result.Info.FileName} is playing");
}
```


## Set up MPC-HC

You need to enable the inbuilt web interface in the options.

![MPC-HC](https://i.gyazo.com/5f56efbb32a65d42cfce24a23d5db2ab.png)

![MPC-HC options](https://i.gyazo.com/f03dbfea5ff204b30cf92a4b80921b42.png)

### That's it, just remember that MPC-HC needs to be running in order for you to retrive info from it.

## Testing

There is a low test covrage due to bad implementation of the MPC-HC web interface. (It returns no response but a 302, no matter what you throw at it.)

This is solved by running realtime integration test and checking the `/variables.html` after each request.

