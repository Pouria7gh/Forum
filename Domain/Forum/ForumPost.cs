using Domain.User;

namespace Domain.Forum
{
    public class ForumPost
    {
        public string PostContent { get; set; } = string.Empty;

        #region Foreign Keys
        public Guid ForumRoomId { get; set; }
        public virtual ForumRoom ForumRoom { get; set; } = default!;
        public Guid? UserId { get; set; }
        public virtual AppUser? User { get; set; }

        public Guid? ParentPostId { get; set; }
        public virtual ForumPost? ParentPost { get; set; }

        #endregion

        public virtual List<ForumPost>? Replies { get; set; }
        public string? Description { get; set; }
        public bool IsDisabled { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
