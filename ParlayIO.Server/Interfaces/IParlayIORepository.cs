using ParlayIO.Domain;
using System.Collections.Generic;

namespace ParlayIO.Server.Interfaces
{
    public interface IParlayIORepository
    {
        void Add(User user);
        void Remove(User user);
        bool Add(Message message);
        void Remove(Message message);
        Message PopFirstMessage();
        List<User> GetUsers();
        List<Message> GetMessages();
    }
}