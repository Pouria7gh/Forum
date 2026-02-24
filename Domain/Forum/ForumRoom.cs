using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Forum
{
    public class ForumRoom : BaseEntity
    {
        [Required]
        [Display(Name = "عنوان")]
        [StringLength(maximumLength:256, MinimumLength = 2, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} حرف باشد.")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "زیر عنوان")]
        [StringLength(maximumLength: 256, MinimumLength = 2, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} حرف باشد.")]
        public string? Subtitle { get; set; }


        [Display(Name = "قوانین")]
        [StringLength(maximumLength: 2048, MinimumLength = 2, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} حرف باشد.")]
        public string? Rules { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(2048)]
        public string? Description { get; set; }
        
        #region Management

        public bool IsClosed { get; set; }
        public bool IsPinned { get; set; }

        #endregion

        #region Navigation

        public virtual List<ForumPost>? Posts { get; set; }

        #endregion

        [Display(Name = "غیر فعال")]
        [DefaultValue(false)]
        public bool IsDisabled { get; set; } = false;

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "تاریخ ویرایش")]
        public DateTime UpdatedAt {  get; set; } = DateTime.UtcNow;
    }
}
