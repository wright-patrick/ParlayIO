using System;

namespace ParlayIO.Domain
{
    public class Message
    {
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        public string SentBy { get; set; }
        public string Initials { get; set; }
        public string UID { get; set; }
        public User SentByUser { get; set; }

        public Message() { }
        public Message(string content, DateTime sendDate, string sentBy, string initials, string uID, User sentByUser)
        {
            Content = content;
            SendDate = sendDate;
            SentBy = sentBy;
            Initials = initials;
            UID = uID;
            SentByUser = sentByUser;
        }
    }
}
