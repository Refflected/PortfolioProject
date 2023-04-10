using Domain.Entities.Generic;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; } = null!;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        // Navigational properties
    }
}