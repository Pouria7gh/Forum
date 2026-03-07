using System.ComponentModel.DataAnnotations;

namespace Forum.Web.Models.ForumPost;

public class AddForumPostViewModel
{
    [Required]
    [StringLength(maximumLength: 8192, MinimumLength = 2, ErrorMessage = "{0} length must be between {2} and {1}.")]
    public string PostContent { get; set; } = string.Empty;
    
    [Required]
    public Guid ForumRoomId { get; set; }
    
    public Guid? ParentPostId { get; set; }

    [StringLength(maximumLength: 2048, MinimumLength = 2, ErrorMessage = "{0} length must be between {2} and {1}.")]
    public string? Description { get; set; }
}
