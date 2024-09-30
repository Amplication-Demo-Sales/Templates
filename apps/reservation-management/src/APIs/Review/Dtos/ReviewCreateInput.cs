namespace ReservationManagement.APIs.Dtos;

public class ReviewCreateInput
{
    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? Date { get; set; }

    public string? Id { get; set; }

    public int? Rating { get; set; }

    public Reservation? Reservation { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
}
