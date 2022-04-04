using ParlayIO.Client.Core.ViewModels;
using ParlayIO.Domain;
using ParleyIOClient.Services;
using System;
using System.Windows.Input;

namespace ParlayIO.Client.Commands
{
    public class ConnectCommand : ICommand
    {
        private readonly ConnectViewModel _connectViewModel;
        private readonly SignalRService _chatService;

        public ConnectCommand(ConnectViewModel connectViewModel, SignalRService chatService)
        {
            _connectViewModel = connectViewModel;
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
                if(string.IsNullOrWhiteSpace(_connectViewModel.FirstName) || 
                    string.IsNullOrWhiteSpace(_connectViewModel.LastName) ||
                    _connectViewModel.FirstName == "Enter First Name" ||
                    _connectViewModel.LastName == "Enter Last Name")
                {
                    Console.WriteLine("First & Last Name are required fields.  Make sure to enter both a First Name & Last Name");
                    return;
                }

                var user = new User()
                {
                    Username = _connectViewModel.FirstName + " " + _connectViewModel.LastName,
                    UID = Guid.NewGuid().ToString(),
                    Initials = _connectViewModel.FirstName[0].ToString() + _connectViewModel.LastName[0].ToString(),
                    Red = _connectViewModel.Red,
                    Green = _connectViewModel.Green,
                    Blue = _connectViewModel.Blue
                };

                //Store Session uID
                _connectViewModel.SessionGuid = user.UID;

                await _chatService.SendConnect(user);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to send message.");
            }
        }
    }
}
