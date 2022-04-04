using ParlayIO.Client.Core.ViewModels;
using ParleyIOClient.Services;
using System;
using System.Windows.Input;

namespace ParlayIO.Client.Commands
{
    class SendMessageCommand : ICommand
    {
        private readonly ChatRoomViewModel _viewModel;
        private readonly SignalRService _chatService;

        public SendMessageCommand(ChatRoomViewModel viewModel, SignalRService chatService)
        {
            _viewModel = viewModel;
            _chatService = chatService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                _viewModel.ChatMessage.SendDate = DateTime.Now;
                _viewModel.ChatMessage.Content = _viewModel.MessageContent;
                _viewModel.ChatMessage.UID = Guid.NewGuid().ToString();
                _viewModel.ChatMessage.SentByUser = _viewModel.ConnectedUser;
                await _chatService.SendMessage(_viewModel.ChatMessage);
                _viewModel.MessageContent = "";
                
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to send message.");
            }
        }
    }
}
