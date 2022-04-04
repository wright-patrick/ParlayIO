using ParlayIO.Domain;
using System.Windows.Media;

namespace ParlayIO.Client.Models
{
    public class ConnectedUser : User
    {
        public SolidColorBrush AvatarColor { get; set; }

        public ConnectedUser(User user)
        {
            Username = user.Username;
            UID = user.UID;
            Initials = user.Initials;
            Red = user.Red;
            Green = user.Green;
            Blue = user.Blue;
            Color c = Color.FromRgb(user.Red, user.Green, user.Blue);
            AvatarColor = new SolidColorBrush(c);
        }
    }
}
