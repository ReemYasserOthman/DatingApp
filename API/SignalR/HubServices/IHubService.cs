using API.Entities;

namespace API.SignalR.HubServices
{
    public interface IHubService
    {
         public Task SendMessage(string groupName , Message message );
         string GetGroupName(string caller, string other);
         
    }
}