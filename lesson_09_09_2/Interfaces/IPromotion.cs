using lesson_09_09_2.Models;

namespace lesson_09_09_2.Interfaces;

public interface IPromotion
{
    Task<IEnumerable<Promotion>> GetAllPromotionsAsync();
    Task<Promotion> GetPromotionAsync(int id);

    Task AddPromotionAsync(Promotion promotion);
    Task EditPromotionAsync(Promotion promotion);
    Task DeletePromotionAsync(Promotion promotion);

}