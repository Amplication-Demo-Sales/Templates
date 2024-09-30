using CrmManagement.Core.Enums;

namespace CrmManagement.APIs.Dtos;

public class UserUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public RoleEnum? Role { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Username { get; set; }
}
