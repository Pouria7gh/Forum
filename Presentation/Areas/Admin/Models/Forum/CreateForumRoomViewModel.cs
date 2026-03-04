using System.ComponentModel.DataAnnotations;

namespace Presentation.Areas.Admin.Models.Forum;

public class CreateForumRoomViewModel
{
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(maximumLength: 256, MinimumLength = 2, ErrorMessage = "{0} length must be between {2} and {1}.")]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(maximumLength: 256, MinimumLength = 2, ErrorMessage = "{0} length must be between {2} and {1}.")]
    public string? Subtitle { get; set; }
    
    [StringLength(maximumLength: 2048, MinimumLength = 2, ErrorMessage = "{0} length must be between {2} and {1}.")]
    public string? Rules { get; set; }
    
    [StringLength(maximumLength: 2048, MinimumLength = 2, ErrorMessage = "{0} length must be between {2} and {1}.")]
    public string? Description { get; set; }
    public bool IsClosed { get; set; } = false;
    public bool IsPinned { get; set; } = false;
}
