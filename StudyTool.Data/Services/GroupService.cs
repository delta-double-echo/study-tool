using StudyTool.Core.Interfaces;
using StudyTool.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace StudyTool.Data.Services
{
    public class GroupService(AppDbContext db) : IGroupService
    {
        public async Task<IEnumerable<Group>> GetAllAsync(string userId)
        {
            return await db.Groups
                .Where(g => g.CreatedBy == userId)
                .OrderBy(g => g.Name)
                .ToListAsync();
        }

        public async Task<Group?> GetByIdAsync(Guid id)
        {
            return await db.Groups
                .Include(g => g.Cards)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Group> CreateAsync(Group group, string userId)
        {
            group.Id = Guid.NewGuid();
            group.CreatedBy = userId;
            group.CreatedAt = DateTime.UtcNow;
            db.Groups.Add(group);
            await db.SaveChangesAsync();
            return group;
        }

        public async Task<Group> UpdateAsync(Group group)
        {
            db.Groups.Update(group);
            await db.SaveChangesAsync();
            return group;
        }

        public async Task DeleteAsync(Guid id)
        {
            var group = await db.Groups.FindAsync(id);
            if (group is not null)
            {
                db.Groups.Remove(group);
                await db.SaveChangesAsync();
            }
        }
    }
}
