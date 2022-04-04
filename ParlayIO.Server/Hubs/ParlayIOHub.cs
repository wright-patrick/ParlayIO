using Microsoft.AspNetCore.SignalR;
using ParlayIO.Domain;
using ParlayIO.Server.Interfaces;
using System.Threading.Tasks;

namespace ParlayIO.Server.Hubs
{
    public class ParlayIOHub : Hub
    {
        private IParlayIORepository _repo;
        public ParlayIOHub()
        {
            _repo = (IParlayIORepository) Startup._serviceProvider.GetService(typeof(IParlayIORepository));
        }

        public async Task SendMessage(Message message)
        {
            bool isCapacity = _repo.Add(message);
            await Clients.All.SendAsync("ReceiveMessage", message);
            if (isCapacity)
            {
                await Clients.All.SendAsync("ReceivePopFirstMessage", _repo.PopFirstMessage());
            }
        }

        public async Task SendConnect(User user)
        {
            _repo.Add(user);
            await Clients.All.SendAsync("ReceiveConnect", _repo.GetUsers());
            await Clients.Caller.SendAsync("ReceiveGetMessages", _repo.GetMessages());
        }

        public async Task SendDisconnect(User user)
        {
            _repo.Remove(user);
            await Clients.All.SendAsync("ReceiveDisconnect", user);
        }
    }
}