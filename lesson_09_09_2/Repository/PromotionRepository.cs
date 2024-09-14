using lesson_09_09_2.Data;
using lesson_09_09_2.Interfaces;
using lesson_09_09_2.Models;
using Microsoft.EntityFrameworkCore;

namespace lesson_09_09_2.Repository;

public class PromotionRepository : IPromotion
{
    public async Task AddPromotionAsync(Promotion promotion)
    {
        using (ApplicationContext context = Program.DbContext())
        {
            context.Promotions.Add(promotion);
            await context.SaveChangesAsync();
        }
    }
    public async Task EditPromotionAsync(Promotion promotion)
    {
        using (ApplicationContext context = Program.DbContext())
        {
            context.Promotions.Update(promotion);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeletePromotionAsync(Promotion promotion)
    {
        using (ApplicationContext context = Program.DbContext())
        {
            context.Promotions.Remove(promotion);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Promotion>> GetAllPromotionsAsync()
    {
        using (ApplicationContext context = Program.DbContext())
        {
            return await context.Promotions.ToListAsync();
        }
    }

    public async Task<Promotion> GetPromotionAsync(int id)
    {
        using (ApplicationContext context = Program.DbContext())
        {
            return await context.Promotions.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
