
using ParlayIO.Domain;
using System.Windows.Media;

namespace ParlayIO.Client.Models
{
    public class ClientSideMessage : Message
    {
        public SolidColorBrush AvatarColor { get; set; }
        public ClientSideMessage(Message message)
        {
            Content = message.Content;
            SendDate = message.SendDate;
            SentBy = message.SentBy;
            Initials = message.Initials;
            UID = message.UID;
            SentByUser = message.SentByUser;

            if(message.SentByUser != null)
            {
                Color c = Color.FromRgb(SentByUser.Red, SentByUser.Green, SentByUser.Blue);
                AvatarColor = new SolidColorBrush(c);
            }
        }
    }
}
