using ServiceHub.DAL.Entities;

namespace ServiceHub.DAL.Interfaces
{
	public interface IDistrictRepo: IGenericRepo<District>
	{
        Task<IEnumerable<District>> GetAllDistrictsByCityId(int cityId);
        //add
    }
}
