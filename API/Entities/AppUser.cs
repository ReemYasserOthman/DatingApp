using API.Extinsions;
namespace API.Entities;


public class AppUser
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string KnowAs { get; set; }
    public string Gender { get; set; }
    public string LokingFor { get; set; }
    public string Introudction { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public List<Photo> Photos { get; set; } = new List<Photo>();

    // public int GetAge()
    // {
    //    return DateOfBirth.CalculateAge();
    // }

}

