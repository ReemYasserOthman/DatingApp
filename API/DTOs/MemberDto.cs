namespace API.DTOs
{
    public class MemberDto
    {
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PhotoUrl { get; set; }
    public string KnowAs { get; set; }
    public string Gender { get; set; }
    public string LokingFor { get; set; }
    public string Introudction { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int Age { get; set; }
    public DateTime Created { get; set; } 
    public DateTime LastActive { get; set; } 
    public List<PhotoDto> Photos { get; set; } 
    }
}