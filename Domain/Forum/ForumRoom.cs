using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Forum
{
    public class ForumRoom : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string? Rules { get; set; }
        public string? Description { get; set; }
        
        #region Management

        public bool IsClosed { get; set; }
        public bool IsPinned { get; set; }

        #endregion

        #region Navigation

        public virtual List<ForumPost>? Posts { get; set; }

        #endregion
        public bool IsDisabled { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt {  get; set; } = DateTime.UtcNow;
    }
}
