namespace API.DTOs
{
    public class UserApiDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Email { get; set; } = null!;
    }
}
