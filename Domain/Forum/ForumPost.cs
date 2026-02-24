using Domain.User;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Forum
{
    public class ForumPost
    {
        [Required]
        [Display(Name = "محتوای پست")]
        [StringLength(maximumLength: 8192, MinimumLength = 2, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} حرف باشد.")]
        public string PostContent { get; set; } = string.Empty;

        #region Foreign Keys

        [Required]
        [Display(Name = "اتاق گفتگو")]
        public Guid FroumRoomId { get; set; }

        [ForeignKey(nameof(FroumRoomId))]
        [Display(Name = "اتاق گفتگو")]
        public virtual ForumRoom ForumRoom { get; set; } = default!;

        [Display(Name = "کاربر")]
        public Guid? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [Display(Name = "کاربر")]
        public virtual AppUser? User { get; set; }

        public Guid? ParentPostId { get; set; }
        public virtual ForumPost? ParentPost { get; set; }

        #endregion

        public virtual List<ForumPost>? Replies { get; set; }

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
