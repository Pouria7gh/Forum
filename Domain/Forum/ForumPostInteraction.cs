using Domain.User;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Forum
{
    public class ForumPostInteraction : BaseEntity
    {
        [Display(Name = "کاربر")]
        public Guid? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [Display(Name = "کاربر")]
        public virtual AppUser? User { get; set; }

        public Guid ForumPostId { get; set; }
        [ForeignKey(nameof(ForumPostId))]
        public virtual ForumPost ForumPost { get; set; } = default!;

        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(2048)]
        public string? Description { get; set; }

        [Display(Name = "غیر فعال")]
        [DefaultValue(false)]
        public bool IsDisabled { get; set; } = false;

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "تاریخ ویرایش")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
