using System;
using System.Collections.Generic;
using System.Text;
using StudyTool.Core.Models;

namespace StudyTool.Core.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetAllAsync(string userId);
        Task<Group?> GetByIdAsync(Guid id);
        Task<Group> CreateAsync(Group group, string userId);
        Task<Group> UpdateAsync(Group group);
        Task DeleteAsync(Guid id);
    }
}
