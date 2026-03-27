using System;
using System.Collections.Generic;
using System.Text;

namespace StudyTool.Core.Models
{
    public class MultipleChoiceQuestion
    {
        public string Question { get; set; } = string.Empty;
        public string CorrectAnswer { get; set; } = string.Empty;
        public List<string> Choices { get; set; } = new();
        public int CorrectIndex { get; set; }
        public string Explanation { get; set; } = string.Empty;
    }
}
