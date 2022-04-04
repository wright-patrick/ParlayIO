using ParlayIO.Client.Commands;
using ParleyIOClient.Services;
using System.Windows.Input;
using System.Windows.Media;

namespace ParlayIO.Client.Core.ViewModels
{
    public class ConnectViewModel : ViewModelBase
    {
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetField(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetField(ref _lastName, value);
        }

        private byte _red;
        public byte Red
        {
            get => _red;
            set
            {
                SetField(ref _red, value);
                SetAvatarColor();
            }
        }

        private byte _green;
        public byte Green
        {
            get => _green;
            set
            {
                SetField(ref _green, value);
                SetAvatarColor();
            }
        }

        private byte _blue;
        public byte Blue
        {
            get => _blue;
            set
            {
                SetField(ref _blue, value);
                SetAvatarColor();
            }
        }

        private SolidColorBrush _avatarColor;
        public SolidColorBrush AvatarColor
        {
            get => _avatarColor;
            set => SetField(ref _avatarColor, value);
        }

        public string SessionGuid { get; set; }

        public ConnectViewModel(SignalRService chatService)
        {
            ConnectCommand = new ConnectCommand(this, chatService);
            Red = 0;
            Green = 0;
            Blue = 0;
        }

        public ICommand ConnectCommand { get; private set; }

        private void SetAvatarColor()
        {
            Color c = Color.FromArgb(255, Red, Green, Blue);
            AvatarColor = new SolidColorBrush(c);
        }
    }
}
