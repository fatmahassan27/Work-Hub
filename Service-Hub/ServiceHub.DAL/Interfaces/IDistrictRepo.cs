using ServiceHub.DAL.Entities;
using ServiceHub.DAL.GenericRepository;

namespace ServiceHub.DAL.Interfaces
{
    public interface IDistrictRepo: IGenericRepo<District>
	{
        Task<IEnumerable<District>> GetAllDistrictsByCityId(int cityId);
        //add
    }
}
