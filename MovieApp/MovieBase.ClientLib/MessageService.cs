using Microsoft.AspNetCore.SignalR.Client;
using MovieBase.Common.Interfaces;
using System.Data.Common;
using System.Diagnostics;

namespace MovieBase.ClientLib;
public class MessageService : IMessageService
{
    private HubConnection? _connection;

    public event Action<string> OnMessage = _ => { };

    public async Task<bool> Connect()
    {
        try
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7267/messages")
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string>("message", s =>
            {
                OnMessage?.Invoke(s);
            });

            await _connection.StartAsync();
            return true;
        }
        catch (Exception ex)
        {
            Trace.TraceError($"{ex}");
            return false;
        }
    }

    public Task Disconnect() => _connection!.StopAsync();

    public async Task<bool> SendMessage(string message)
    {
        try
        {
            await _connection!.SendAsync("clientMessage", $"From message service: {message}");
            return true;
        }
        catch (Exception ex)
        {
            Trace.TraceError($"{ex}");
            return false;
        }
    }
}
