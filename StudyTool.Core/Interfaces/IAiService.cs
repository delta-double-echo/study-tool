using StudyTool.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyTool.Core.Interfaces
{
    public interface IAiService
    {
        Task<MultipleChoiceQuestion> GenerateChoicesAsync(string question, string answer);
    }
}
