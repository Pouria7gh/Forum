using Domain.User;

namespace Application.DTOs.ForumRoom;

public class ForumPostDto
{
    public string PostContent { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string UserDisplayName { get; set; } = string.Empty;
}
