# QuickSocket .NET Example
A simple .NET example implementation  of [QuickSocket](https://quicksocket.io). 

## Setup
In order to run this example locally you will need to use
[`qs-forward`](https://github.com/QuickSocket/qs-forward). Once installed follow these instructions:
1. Login to your QuickSocket account
2. Navigate to the "Callback" tab
3. Ensure that the Callback Mode is set to "Forward"
4. Follow the instructions at [`qs-forward`](https://github.com/QuickSocket/qs-forward) to run `qs-forward`

### .NET Application Setup
Before running the example application ensure that the generated credentials from the environment you created on the [QuickSocket Platform](https://app.quicksocket.io) have been added to `appsettings.json` under the `QuickSocket` header.


## Run the Example
1. Run the .NET solution in Visual Studio.
2. Run `qs-forward` from Command Line with:
```
qs-forward -client-id <CLIENT_ID> -client-secret <EITHER_CLIENT_SECRET> http://localhost:5321/api/receive 
```

3. Run the client with:
```
npm start
```
4. Navigate to `localhost:8080`

