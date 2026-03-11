namespace Application.DTOs.ForumRoom;

public class ForumRoomManageDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsClosed { get; set; }
}
