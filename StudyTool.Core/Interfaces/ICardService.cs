using StudyTool.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyTool.Core.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<Card>> GetAllAsync(Guid? group = null, bool flaggedOnly = false);
        Task<Card?> GetByIdAsync(Guid id);
        Task<Card> CreateAsync(Card card, string userId);
        Task<Card> UpdateAsync(Card card);
        Task DeleteAsync(Guid id);
        Task RecordTallyAsync(Guid cardId, string userId);
    }
}
