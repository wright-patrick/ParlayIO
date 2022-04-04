using Microsoft.AspNetCore.SignalR.Client;
using ParlayIO.Client.Commands;
using ParlayIO.Client.Models;
using ParlayIO.Domain;
using ParleyIOClient.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ParlayIO.Client.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public string Username { get; set; }


        private bool _isConnected;
        private SignalRService _chatService;

        private bool _isConnectViewVisible;
        public bool IsConnectViewVisible
        {
            get => _isConnectViewVisible;
            set => SetField(ref _isConnectViewVisible, value);
        }

        private bool _isChatViewVisible;
        public bool IsChatViewVisible
        {
            get => _isChatViewVisible;
            set => SetField(ref _isChatViewVisible, value);
        }

        private ConnectViewModel _connectViewModel;
        public ConnectViewModel ConnectViewModel
        {
            get => _connectViewModel;
            set => SetField(ref _connectViewModel, value);
        }


        private ChatRoomViewModel _chatRoomViewModel;
        public ChatRoomViewModel ChatRoomViewModel
        {
            get => _chatRoomViewModel;
            set => SetField(ref _chatRoomViewModel, value);
        }

        private ConnectedUser _user;
        public ConnectedUser User
        {
            get => _user;
            set => SetField(ref _user, value);
        }

        public MainViewModel()
        {
            _isConnected = false;
            IsChatViewVisible = false;
            IsConnectViewVisible = true;

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://parelyio.azurewebsites.net/chat")
                .Build();

            SignalRService chatService = new SignalRService(connection);

            chatService.Connect().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    Console.WriteLine("Unable to connect to chat hub.");
                }
            });

            ConnectViewModel = new ConnectViewModel(chatService);
            ChatRoomViewModel = new ChatRoomViewModel(this, chatService);

            DisconnectCommand = new DisconnectCommand(this, chatService);
            chatService.ConnectReceived += ChatService_ConnectReceived;
        }

        public ICommand DisconnectCommand { get; private set; }

        private void ChatService_ConnectReceived(List<User> obj)
        {
            if (_isConnected)
            {
                ChatRoomViewModel.RefreshUsers(obj);
            }
            else
            {
                foreach(User user in obj)
                {
                    if(user.UID == ConnectViewModel.SessionGuid)
                    {
                        User = new ConnectedUser(user);
                        break;
                    }
                }

                if(User == null)
                {
                    Console.WriteLine("Session uID not found.  Connection Rejected");
                }
                else
                {
                    _isConnected = true;
                    ChatRoomViewModel.OnConnected(User);
                    ChatRoomViewModel.RefreshUsers(obj);
                    IsConnectViewVisible = false;
                    IsChatViewVisible = true;
                }
            }
        }
        internal void Disconnect()
        {
            _isConnected = false;
            IsChatViewVisible = false;
            IsConnectViewVisible = true;
        }
    }
}
