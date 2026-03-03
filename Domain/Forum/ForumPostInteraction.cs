using Domain.User;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Forum
{
    public class ForumPostInteraction : BaseEntity
    {
        public Guid? UserId { get; set; }
        public virtual AppUser? User { get; set; }
        public virtual ForumPost ForumPost { get; set; } = default!;
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }
        public string? Description { get; set; }
        public bool IsDisabled { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
