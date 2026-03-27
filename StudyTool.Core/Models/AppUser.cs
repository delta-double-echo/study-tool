using System;
using System.Collections.Generic;
using System.Text;

namespace StudyTool.Core.Models
{
    public class AppUser
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}
