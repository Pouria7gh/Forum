namespace Forum.Web.Models.ForumPost;

public class AddForumPostViewModel
{
    public string PostContent { get; set; } = string.Empty;
    public Guid ForumRoomId { get; set; }
    public Guid UserId { get; set; }
    public string? Description { get; set; }
}
