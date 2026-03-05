namespace Forum.Web.Areas.Admin.Models.Forum
{
    public class ForumRoomViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string? Rules { get; set; }
        public string? Description { get; set; }
        public bool IsClosed { get; set; }
        public bool IsPinned { get; set; }
    }
}
