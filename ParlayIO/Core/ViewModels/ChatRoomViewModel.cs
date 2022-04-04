using ParlayIO.Client.Commands;
using ParlayIO.Client.Models;
using ParlayIO.Domain;
using ParleyIOClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ParlayIO.Client.Core.ViewModels
{
    public class ChatRoomViewModel : ViewModelBase
    {
        private ConnectedUser _connectedUser;
        public ConnectedUser ConnectedUser
        {
            get => _connectedUser;
            set => SetField(ref _connectedUser, value);
        }
    
        private ObservableCollection<ConnectedUser> _users;
        public ObservableCollection<ConnectedUser> Users
        {
            get => _users;
            set => SetField(ref _users, value);
        }

        private ObservableCollection<ClientSideMessage> _messages;
        public ObservableCollection<ClientSideMessage> Messages
        {
            get => _messages;
            set => SetField(ref _messages, value);
        }


        private Message _chatMessage;
        public Message ChatMessage
        {
            get => _chatMessage;
            set => SetField(ref _chatMessage, value);
        }

        private string _messageContent;
        public string MessageContent
        {
            get => _messageContent;
            set => SetField(ref _messageContent, value);
        }

        public ChatRoomViewModel(MainViewModel mainViewModel, SignalRService chatService)
        {
            Messages = new ObservableCollection<ClientSideMessage>();
            ChatMessage = new Message("", DateTime.Now, "", "", "", mainViewModel.User);
            SendMessageCommand = new SendMessageCommand(this, chatService);
            DisconnectCommand = new DisconnectCommand(mainViewModel, chatService);
            chatService.GetMessagesReceived += ChatService_GetMessagesReceived;
            chatService.MessageReceived += ChatService_MessageReceived;
            chatService.DisconnectReceived += ChatService_DisconnectReceived;
            chatService.PopFirstMessageReceived += ChatService_PopFirstMessageReceived;
        }

        private void ChatService_PopFirstMessageReceived(Message obj)
        {
            ObservableCollection<ClientSideMessage> newMessages = new ObservableCollection<ClientSideMessage>();
            foreach(Message message in Messages)
            {
                if(message.UID != obj.UID)
                {
                    newMessages.Add(new ClientSideMessage(message));
                }
            }

            newMessages = new ObservableCollection<ClientSideMessage>(newMessages.OrderBy(i => i.SendDate));
            Messages = newMessages;
        }

        private void ChatService_GetMessagesReceived(List<Message> obj)
        {
            Messages.Clear();
            foreach(Message message in obj)
            {
                Messages.Add(new ClientSideMessage(message));
            }
        }

        public ICommand SendMessageCommand { get; private set; }
        public ICommand DisconnectCommand { get; private set; }

        public void OnConnected(User user)
        {
            Users = new ObservableCollection<ConnectedUser>();
            ConnectedUser = new ConnectedUser(user);

            ChatMessage.SentBy = user.Username;
            ChatMessage.Initials = user.Initials;
        }
        private void ChatService_DisconnectReceived(User obj)
        {
            ObservableCollection<ConnectedUser> newUsers = new ObservableCollection<ConnectedUser>();
            foreach(User user in Users)
            {
                if(user.UID != obj.UID)
                {
                    newUsers.Add(new ConnectedUser(user));
                }
            }

            Users = newUsers;
        }

        private void ChatService_MessageReceived(Message obj)
        {
            Messages.Add(new ClientSideMessage(obj));
        }

        internal void RefreshUsers(List<User> obj)
        {
            Users.Clear();
            foreach(User user in obj)
            {
                Users.Add(new ConnectedUser(user));
            }
        }

        internal void RefreshMessages(List<Message> obj)
        {
            Messages.Clear();
            foreach(Message message in obj)
            {
                Messages.Add(new ClientSideMessage(message));
            }
        }
    }
}
