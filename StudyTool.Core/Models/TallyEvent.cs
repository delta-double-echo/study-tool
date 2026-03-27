using System;
using System.Collections.Generic;
using System.Text;

namespace StudyTool.Core.Models
{
    public class TallyEvent
    {
        public Guid Id { get; set; }
        public Guid CardId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime OccurredAt { get; set; }
        public Card Card { get; set; } = null!;
    }
}
