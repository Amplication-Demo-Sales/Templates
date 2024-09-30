namespace ReservationManagement.APIs.Dtos;

public class UserCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public string? Password { get; set; }

    public List<Reservation>? Reservations { get; set; }

    public List<Review>? Reviews { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Username { get; set; }
}
