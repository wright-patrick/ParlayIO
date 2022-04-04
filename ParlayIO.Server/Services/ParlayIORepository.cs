using ParlayIO.Domain;
using ParlayIO.Server.Interfaces;
using System.Collections.Generic;

namespace ParlayIO.Server.Services
{
    public  class ParlayIORepository: IParlayIORepository
    {
        public  List<User> Users = new List<User>();
        public List<Message> Messages = new List<Message>();

        public void Add(User user)
        {
            Users.Add(user);
        }

        public bool Add(Message message)
        {
            Messages.Add(message);
            return Messages.Count >= 25;
        }

        public void Remove(User user)
        {
            List<User> newUsers = new List<User>();
            foreach(User existingUser in Users)
            {
                if(existingUser.UID != user.UID)
                {
                    newUsers.Add(existingUser);
                }
            }
            Users = newUsers;
        }

        public void Remove(Message message)
        {
            Messages.Remove(message);
        }

        public List<User> GetUsers()
        {
            return Users;
        }

        public List<Message> GetMessages()
        {
            return Messages;
        }

        public Message PopFirstMessage()
        {
            Message message = Messages[0];
            Messages.RemoveAt(0);
            return message;
        }
    }
}