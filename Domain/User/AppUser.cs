namespace Domain.User
{
    public class AppUser : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHashed { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailVerified { get; set; } = false;
        public string DisplayName { get; set; } = string.Empty;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
