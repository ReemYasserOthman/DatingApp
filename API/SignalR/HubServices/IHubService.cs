using API.Entities;

namespace API.SignalR.HubServices
{
    public interface IHubService
    {
         public Task SendMessage(string groupName , Message message );
         string GetGroupName(string caller, string other);
         //Task<Group> AddToGroup(string groupName, string connectionId, string username);
         //Task<Group> RemoveConnectionFromGroup(string connectionId);
    }
}