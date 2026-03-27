using System;
using System.Collections.Generic;
using System.Text;

namespace StudyTool.Core.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}
