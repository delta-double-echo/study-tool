using System;
using System.Collections.Generic;
using System.Text;

namespace StudyTool.Core.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public bool FlaggedForImprovement { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<TallyEvent> TallyEvents { get; set; } = new List<TallyEvent>();
    }
}
