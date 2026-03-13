namespace Application.DTOs.ForumRoom;

public class ForumPostDto
{
    public Guid Id { get; set; }
    public Guid ForumRoomId { get; set; }
    public string PostContent { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? UserDisplayName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsLiked { get; set; }
    public bool IsDisliked { get; set; }
    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }
    public int ViewCount { get; set; }
}
