using bgbrokersapi.Data.Models.OfferLocation;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;

namespace bgbrokersapi.Services.Admin.Type
{
    public interface ILocationService
    {
        Task<CityResponseModel> CreateCity(TypeInputModel model);
        Task<HoodResponseModel> CreateHood(HoodInputModel model);
        Task<Hood> GetHoodById(int? id);
        Task<City> GetCityById(int? id);
        Task<ResponseModel> DeleteHood(int id);
        Task<IEnumerable<Hood>> GetAllHoods();
        Task<IEnumerable<City>> GetAllCities();
        Task<bool> CityExist(int? id);
        Task<bool> HoodExist(int? id);

    }
}
