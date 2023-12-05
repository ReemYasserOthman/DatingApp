using API.DTOs;
using API.Entities;
using API.SignalR.HubServices;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class HubService : IHubService
    {
        private readonly IHubContext<MessageHub> _messageHub;
        private readonly IMapper _mapper;

        public HubService( IHubContext<MessageHub> messageHub,IMapper mapper)
        {          
            _messageHub = messageHub;
            _mapper = mapper;
        }

        public async Task SendMessage(string groupName, Message message)
        {
            await _messageHub.Clients.Group(groupName).SendAsync("NewMessage",
              _mapper.Map<MessageDto>(message));
        }

         public string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
      
    }
}