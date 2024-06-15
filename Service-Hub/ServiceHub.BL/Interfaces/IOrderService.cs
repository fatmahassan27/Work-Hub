namespace ServiceHub.BL.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(int wokerId, int userId);

    }
}
