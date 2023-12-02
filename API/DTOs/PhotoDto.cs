namespace API.DTOs
{
    public class PhotoDto : BaseDto
    {
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }
}