using Microsoft.EntityFrameworkCore;
using StudyTool.Core.Interfaces;
using StudyTool.Core.Models;

namespace StudyTool.Data.Services;

public class CardService(AppDbContext db) : ICardService
{
    public async Task<IEnumerable<Card>> GetAllAsync(Guid? groupId = null, bool flaggedOnly = false)
    {
        var query = db.Cards
            .Include(c => c.Group)
            .Include(c => c.TallyEvents)
            .AsQueryable();

        if (groupId.HasValue)
            query = query.Where(c => c.GroupId == groupId.Value);

        if (flaggedOnly)
            query = query.Where(c => c.FlaggedForImprovement);

        return await query
            .OrderBy(c => c.Group.Name)
            .ThenBy(c => c.Title)
            .ToListAsync();
    }

    public async Task<Card?> GetByIdAsync(Guid id)
    {
        return await db.Cards
            .Include(c => c.Group)
            .Include(c => c.TallyEvents)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Card> CreateAsync(Card card, string userId)
    {
        card.Id = Guid.NewGuid();
        card.CreatedBy = userId;
        card.CreatedAt = DateTime.UtcNow;
        card.UpdatedAt = DateTime.UtcNow;
        db.Cards.Add(card);
        await db.SaveChangesAsync();
        return card;
    }

    public async Task<Card> UpdateAsync(Card card)
    {
        card.UpdatedAt = DateTime.UtcNow;
        db.Cards.Update(card);
        await db.SaveChangesAsync();
        return card;
    }

    public async Task DeleteAsync(Guid id)
    {
        var card = await db.Cards.FindAsync(id);
        if (card is not null)
        {
            db.Cards.Remove(card);
            await db.SaveChangesAsync();
        }
    }

    public async Task RecordTallyAsync(Guid cardId, string userId)
    {
        var tally = new TallyEvent
        {
            Id = Guid.NewGuid(),
            CardId = cardId,
            UserId = userId,
            OccurredAt = DateTime.UtcNow
        };
        db.TallyEvents.Add(tally);
        await db.SaveChangesAsync();
    }
}