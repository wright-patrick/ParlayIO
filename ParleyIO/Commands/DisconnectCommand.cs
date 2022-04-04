using ParlayIO.Client.Core.ViewModels;
using ParlayIO.Domain;
using ParleyIOClient.Services;
using System;
using System.Windows.Input;

namespace ParlayIO.Client.Commands
{
    public class DisconnectCommand : ICommand
    {
        private readonly MainViewModel _viewModel;
        private readonly SignalRService _chatService;

        public DisconnectCommand(MainViewModel connectViewModel, SignalRService chatService)
        {
            _viewModel = connectViewModel;
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
                if(_viewModel.User == null)
                {
                    return; // User hasn't connected yet.  Just close the application.
                }

                await _chatService.SendDisconnect(new User()
                {
                    Username = _viewModel.Username,
                    UID = _viewModel.User.UID,
                    Initials = _viewModel.User.Initials,
                    Red = _viewModel.User.Red,
                    Green = _viewModel.User.Green,
                    Blue = _viewModel.User.Blue

                });
                _viewModel.Disconnect();
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to disconnect.");
            }
        }
    }  
}
