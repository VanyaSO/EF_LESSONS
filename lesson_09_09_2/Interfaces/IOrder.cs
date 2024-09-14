using lesson_09_09_2.Models;

public interface IOrder
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<IEnumerable<Order>> GetAllOrdersByNameAsync(string name);
    Task<IEnumerable<Order>> GetAllOrdersByAddressAsync(string address);
    Task<Order> GetOrderAsync(int id);
    Task<Order> GetOrderWithOrderLinesAndBooksAsync(int id);
        
    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(Order order);
}