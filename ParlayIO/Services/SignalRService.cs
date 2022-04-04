using Microsoft.AspNetCore.SignalR.Client;
using ParlayIO.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParleyIOClient.Services
{
    public class SignalRService
    {
        private readonly HubConnection _connection;

        public event Action<Message> MessageReceived;
        public event Action<List<User>> ConnectReceived;
        public event Action<User> DisconnectReceived;
        public event Action<List<Message>> GetMessagesReceived;
        public event Action<Message> PopFirstMessageReceived;

        public SignalRService(HubConnection connection)
        {
            _connection = connection;
            _connection.On<Message>("ReceiveMessage", (message) => MessageReceived?.Invoke(message));
            _connection.On<List<User>>("ReceiveConnect", (users) => ConnectReceived?.Invoke(users));
            _connection.On<User>("ReceiveDisconnect", (user) => DisconnectReceived?.Invoke(user));
            _connection.On<List<Message>>("ReceiveGetMessages", (messages) => GetMessagesReceived?.Invoke(messages));
            _connection.On<Message>("ReceivePopFirstMessage", (message) => PopFirstMessageReceived?.Invoke(message));
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        public async Task SendMessage(Message message)
        {
            await _connection.SendAsync("SendMessage", message);
        }

        public async Task SendConnect(User user)
        {
            await _connection.SendAsync("SendConnect", user);
        }

        public async Task SendDisconnect(User user)
        {
            await _connection.SendAsync("SendDisconnect", user);
        }
    }
}
