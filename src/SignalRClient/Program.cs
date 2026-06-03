

using Microsoft.AspNetCore.SignalR.Client;
int count = 0;
int duplicates = 0;
HashSet<string> messages = new HashSet<string>();
try
{

    var connection = new HubConnectionBuilder()
        .WithUrl("https://localhost:7111/hub", options =>
        {
            options.AccessTokenProvider = async () => await Task.Run<string>(() =>
            {
                return "";
            });
            options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.ServerSentEvents;
        })
        .Build();

    connection.On<string, string>("ReceiveMessage", (user, message) =>
    {
        Console.WriteLine($"Received message from {user}: {message}");
    });
    connection.Closed += async (error) =>
    {
        Console.WriteLine("Connection closed. Attempting to reconnect...");
        Console.WriteLine($"Error: {error?.Message}");
    };
    await connection.StartAsync();
    while (true)
    {
       await Task.Delay(1000);  
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error starting connection: {ex.Message}");
}
