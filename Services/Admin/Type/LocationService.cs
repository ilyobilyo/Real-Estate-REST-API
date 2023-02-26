using AutoMapper;
using bgbrokersapi.Data;
using bgbrokersapi.Data.Models.OfferLocation;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;
using Microsoft.EntityFrameworkCore;

namespace bgbrokersapi.Services.Admin.Type
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public LocationService(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<bool> CityExist(int? id)
        {
            return await dbContext.Cities.AnyAsync(x => x.Id == id);
        }

        public async Task<CityResponseModel> CreateCity(TypeInputModel model)
        {
            var responseModel = new CityResponseModel();

            if (await CityExist(model.Name))
            {
                responseModel.Status = StatusCodes.Status400BadRequest;
                responseModel.Message = "City already exist!";
                return responseModel;
            }

            var city = new City
            {
                Name = model.Name,
            };

            try
            {
                await dbContext.AddAsync(city);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                responseModel.Status = StatusCodes.Status400BadRequest;
                responseModel.Message = "Unexpected error. Try again!";
            }

            responseModel.Status = StatusCodes.Status201Created;
            responseModel.Message = "Creation is successful";
            responseModel.City = mapper.Map<CityModel>(city);

            return responseModel;
        }

        public async Task<HoodResponseModel> CreateHood(HoodInputModel model)
        {
            var responseModel = new HoodResponseModel();

            if (await HoodExist(model))
            {
                responseModel.Status = StatusCodes.Status400BadRequest;
                responseModel.Message = "Hood already exist!";
                return responseModel;
            }

            var hood = new Hood
            {
                Name = model.Name,
                CityId = model.CityId
            };

            try
            {
                await dbContext.AddAsync(hood);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                responseModel.Status = StatusCodes.Status400BadRequest;
                responseModel.Message = "Unexpected error. Try again!";
            }

            responseModel.Status = StatusCodes.Status201Created;
            responseModel.Message = "Creation is successful";
            responseModel.Hood = mapper.Map<HoodModel>(hood);

            return responseModel;
        }

        public async Task<ResponseModel> DeleteHood(int id)
        {
            var response = new ResponseModel();

            try
            {
                var hoodToDelete = await GetHoodById(id);
                dbContext.Remove(hoodToDelete);
                await dbContext.SaveChangesAsync();
            }
            catch (ArgumentException e)
            {
                response.Status = StatusCodes.Status404NotFound;
                response.Message = e.Message;

                return response;
            }
            catch (Exception e)
            {
                response.Status = StatusCodes.Status400BadRequest;
                response.Message = e.Message;

                return response;
            }

            response.Status = StatusCodes.Status202Accepted;
            response.Message = "Successful delete";

            return response;
        }

        public async Task<IEnumerable<City>> GetAllCities()
        {
            return await dbContext.Cities
                .Include(x => x.Hoods)
                .ToListAsync();
        }

        public async Task<IEnumerable<Hood>> GetAllHoods()
        {
            return await dbContext.Hoods
                .Include(x => x.City)
                .ToListAsync();
        }

        public async Task<City> GetCityById(int? id)
        {
            var city = await dbContext
                .Cities
                .Include(x => x.Hoods)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (city == null)
            {
                throw new ArgumentException("City is not found");
            }

            return city;
        }

        public async Task<Hood> GetHoodById(int? id)
        {
            var hood = await dbContext
                .Hoods
                .Include(x => x.City)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (hood == null)
            {
                throw new ArgumentException("Hood is not found");
            }

            return hood;
        }

        public async Task<bool> HoodExist(int? id)
        {
            return await dbContext.Hoods.AnyAsync(x => x.Id == id);
        }

        private async Task<bool> CityExist(string name)
        {
            return await dbContext.Cities.AnyAsync(x => x.Name == name);
        }

        private async Task<bool> HoodExist(HoodInputModel model)
        {
            return await dbContext.Hoods.AnyAsync(x => x.CityId == model.CityId && x.Name == model.Name);
        }
    }
}
