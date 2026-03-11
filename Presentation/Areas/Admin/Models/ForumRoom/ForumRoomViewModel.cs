namespace Forum.Web.Areas.Admin.Models.ForumRoom
{
    public class ForumRoomViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string? Rules { get; set; }
        public string? Description { get; set; }
        public bool IsClosed { get; set; }
        public bool IsPinned { get; set; }
    }
}
