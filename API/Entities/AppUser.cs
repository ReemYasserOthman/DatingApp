using API.Extinsions;
using Microsoft.AspNetCore.Identity;
namespace API.Entities;


public class AppUser : IdentityUser<int>
{
    public string KnownAs { get; set; }
    public string Gender { get; set; }
    public string LookingFor { get; set; }
    public string Introduction { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastActive { get; set; } = DateTime.Now;
    public List<Photo> Photos { get; set; } = new List<Photo>();
    public List<UserLike> LikedByUsers { get; set; }
    public List<UserLike> LikedUsers { get; set; }
    public List<Message> MessagesSend { get; set; }
    public List<Message> MessagesReceived { get; set; }
    public ICollection<AppUserRole> UserRoles { get; set; }
    
}

