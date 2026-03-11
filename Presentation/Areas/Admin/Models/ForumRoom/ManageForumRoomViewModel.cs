namespace Forum.Web.Areas.Admin.Models.ForumRoom;

public class ManageForumRoomViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsClosed { get; set; }
}