namespace Application.DTOs.ForumRoom;

public class ForumPostDto
{
    public Guid Id { get; set; }
    public string PostContent { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? UserDisplayName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public string? ParentUserDisplayName { get; set; }
    public string? ParentPostContent { get; set; }
}
