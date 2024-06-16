using ServiceHub.DAL.Interfaces;

namespace ServiceHub.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IJobRepo JobRepo { get; }
        ICityRepo CityRepo { get; }
        IOrderRepo OrderRepo { get; }
        IDistrictRepo DistrictRepo { get; }
        IRateRepo RateRepo { get; }
        INotificaionRepo NotificaionRepo { get; }

        Task<int> saveAsync();
    }


}
