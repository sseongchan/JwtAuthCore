namespace JwtAuthSample.Modules.User.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int? EmployeeId { get; set; }
        public int? DepartmentId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string UseYn { get; set; } = "Y";
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Dn { get; set; } = string.Empty;
    }

    public class UserCreateRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int? EmployeeId { get; set; }
        public int? DepartmentId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Dn { get; set; } = string.Empty;
    }
}
