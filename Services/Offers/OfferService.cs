using AutoMapper;
using bgbrokersapi.Data;
using bgbrokersapi.Data.Models;
using bgbrokersapi.Data.Models.Types;
using bgbrokersapi.Data.Models.User;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;
using bgbrokersapi.Services.Admin.Type;
using bgbrokersapi.Services.User;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bgbrokersapi.Services.Offers
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ITypeService typeService;
        private readonly ILocationService locationService;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public OfferService(ApplicationDbContext applicationDbContext,
            IMapper mapper,
            ITypeService typeService,
            ILocationService locationService,
            IUserService userService)
        {
            this.dbContext = applicationDbContext;
            this.mapper = mapper;
            this.typeService = typeService;
            this.locationService = locationService;
            this.userService = userService;
        }

        public async Task<OfferResponseModel> CreateOffer(OfferInputModel model, Claim createUserIdClaim)
        {
            var response = new OfferResponseModel();

            var createUser = await userService.GetById(createUserIdClaim.Value);

            var brokerExist = await userService.UserExist(model.BrokerId);

            if (!await AllTypesExists(model.ConstructionId, model.CurrencyId, model.ExpositionId, model.FurnitureId, model.HeatingId, model.OfferTypeId, model.SellTypeId))
            {
                response.Status = StatusCodes.Status404NotFound;
                response.Message = "Invalid type. Try again";

                return response;
            }

            if (!await OfferLocationIsCorrect(model.CityId, model.HoodId))
            {
                response.Status = StatusCodes.Status404NotFound;
                response.Message = "Invalid city of hood. Try again";

                return response;
            }

            if (!brokerExist)
            {
                response.Status = StatusCodes.Status404NotFound;
                response.Message = "Broker not found";

                return response;
            }

            var offer = new Offer()
            {
                Title = model.Title,
                ActualAddress = model.ActualAddress,
                BedroomsCount = model.BedroomsCount,
                BrokerId = model.BrokerId,
                CityId = model.CityId,
                ConstructionId = model.ConstructionId,
                CreateTimestamp = DateTime.Now,
                CreateUser = createUser,
                CreateUserId = createUser.Id,
                CurrencyId = model.CurrencyId,
                Description = model.Description,
                Floor = model.Floor,
                FurnitureId = model.FurnitureId,
                HasElevator = model.HasElevator,
                HasParkingPlace = model.HasParkingPlace,
                HeatingId = model.HeatingId,
                HoodId = model.HoodId,
                IsPublic = model.IsPublic,
                Name = model.Name,
                OfferTypeId = model.OfferTypeId,
                OwnerPhoneNumber = model.OwnerPhoneNumber,
                Price = model.Price,
                Priority = model.Priority,
                RoomsCount = model.RoomsCount,
                SellTypeId = model.SellTypeId,
                Squaring = model.Squaring,
                SubTitle = model.SubTitle,
                TerracesCount = model.TerracesCount,
                TotalFloors = model.TotalFloors,
                ЕxpositionId = model.ExpositionId
            };

            foreach (var imageName in model.Images)
            {
                var newImage = new Image()
                {
                    Name = imageName,
                    Offer = offer,
                };

                offer.Images.Add(newImage);
            }


            try
            {
                await dbContext.AddAsync(offer);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.Status = StatusCodes.Status400BadRequest;
                response.Message = e.Message;

                return response;
            }

            response.Status = StatusCodes.Status201Created;
            response.Message = "Creation is successful";
            response.Offer = mapper.Map<OfferModel>(offer);

            return response;
        }

        public async Task<ResponseModel> CasualDelete(Guid id)
        {
            var response = new ResponseModel();

            try
            {
                var offerToDelete = await GetById(id);

                if (!offerToDelete.IsDeleted)
                {
                    offerToDelete.IsDeleted = true;
                    await dbContext.SaveChangesAsync();
                }

            }
            catch (ArgumentException e)
            {
                response.Status = StatusCodes.Status404NotFound;
                response.Message = e.Message;

                return response;
            }

            response.Status = StatusCodes.Status202Accepted;
            response.Message = "Successful delete";

            return response;
        }

        public async Task<ResponseModel> DelteFromDb(Guid id)
        {
            var response = new ResponseModel();

            try
            {
                var offerToDelete = await GetById(id);
                dbContext.Remove(offerToDelete);
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

        public async Task<IEnumerable<Offer>> GetAllOffers()
        {
            return await dbContext.Offers
                .Include(x => x.Exposition)
                .Include(x => x.Construction)
                .Include(x => x.Currency)
                .Include(x => x.Furniture)
                .Include(x => x.Heating)
                .Include(x => x.Hood)
                .Include(x => x.OfferType)
                .Include(x => x.SellType)
                .Include(x => x.City)
                .ThenInclude(x => x.Hoods)
                .Include(x => x.Images)
                .Include(x => x.Broker)
                .ToListAsync();
        }

        public async Task<Offer> GetById(Guid id)
        {
            var offer = await dbContext
                .Offers
                .Include(x => x.Exposition)
                .Include(x => x.Construction)
                .Include(x => x.Currency)
                .Include(x => x.Furniture)
                .Include(x => x.Heating)
                .Include(x => x.Hood)
                .Include(x => x.OfferType)
                .Include(x => x.SellType)
                .Include(x => x.City)
                .ThenInclude(x => x.Hoods)
                .Include(x => x.Images)
                .Include(x => x.Broker)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (offer == null)
            {
                throw new ArgumentException("Offer is not found");
            }

            return offer;
        }

        public async Task<UpdateOfferResponseModel> UpdateOffer(Guid id, OfferInputModel model, Claim updateUserIdClaim)
        {
            var response  = new UpdateOfferResponseModel();

            try
            {
                var offerToUpdate = await GetById(id);
                var updateUser = await userService.GetById(updateUserIdClaim.Value);

                UpdateOfferProperies(offerToUpdate, model, updateUser);

                var offer = mapper.Map<OfferUpdateModel>(offerToUpdate);

                response.Status = StatusCodes.Status202Accepted;
                response.Message = "Offer is updated successful";
                response.Offer = offer;

                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.Status = StatusCodes.Status404NotFound;
                response.Message = e.Message;

                return response;
            }

            return response;
        }

        private async Task<bool> AllTypesExists(int? constructionId, int? currencyId, int? expositionId, int? furnitureId, int? heatingId, int? offerTypeId, int? sellTypeId)
        {
            bool? constructionExist = await typeService.TypeExist<Construction>(constructionId);
            bool? currencyExist = await typeService.TypeExist<Currency>(currencyId);
            bool? expositionExist = await typeService.TypeExist<Exposition>(expositionId);
            bool? furnitureExist = await typeService.TypeExist<Furniture>(furnitureId);
            bool? heatingExist = await typeService.TypeExist<Heating>(heatingId);
            bool? offerTypeExist = await typeService.TypeExist<OfferType>(offerTypeId);
            bool? sellTypeExist = await typeService.TypeExist<SellType>(sellTypeId);

            if (constructionExist == false ||
                currencyExist == false ||
                expositionExist == false ||
                furnitureExist == false ||
                heatingExist == false ||
                offerTypeExist == false ||
                sellTypeExist == false)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> OfferLocationIsCorrect(int? cityId, int? hoodId)
        {
            var city = await locationService.GetCityById(cityId);

            if (city.Hoods.Any(x => x.Id == hoodId))
            {
                return true;
            }

            return false;
        }

        private void UpdateOfferProperies(Offer offerToUpdate, OfferInputModel model, ApplicationUser updateUser)
        {
            offerToUpdate.Name = model.Name;
            offerToUpdate.Title = model.Title;
            offerToUpdate.SubTitle = model.SubTitle;
            offerToUpdate.Description = model.Description;

            offerToUpdate.Floor = model.Floor;
            offerToUpdate.Squaring = model.Squaring;
            offerToUpdate.RoomsCount = model.RoomsCount;
            offerToUpdate.TotalFloors = model.TotalFloors;
            offerToUpdate.BedroomsCount = model.BedroomsCount;
            offerToUpdate.TerracesCount = model.TerracesCount;
            offerToUpdate.HasElevator = model.HasElevator;
            offerToUpdate.HasParkingPlace = model.HasParkingPlace;

            offerToUpdate.OwnerPhoneNumber = model.OwnerPhoneNumber;
            offerToUpdate.ActualAddress = model.ActualAddress;
            offerToUpdate.BrokerId = model.BrokerId;

            offerToUpdate.IsPublic = model.IsPublic;
            offerToUpdate.Priority = model.Priority;

            offerToUpdate.Price = model.Price;
            offerToUpdate.CurrencyId = model.CurrencyId;

            offerToUpdate.CityId = model.CityId;
            offerToUpdate.HoodId = model.HoodId;

            offerToUpdate.ЕxpositionId = model.ExpositionId;
            offerToUpdate.ConstructionId = model.ConstructionId;
            offerToUpdate.FurnitureId = model.FurnitureId;
            offerToUpdate.HeatingId = model.HeatingId;
            offerToUpdate.OfferTypeId = model.OfferTypeId;
            offerToUpdate.SellTypeId = model.SellTypeId;

            offerToUpdate.UpdateTimestamp = DateTime.Now;
            offerToUpdate.UpdateUser = updateUser;
            offerToUpdate.UpdateUserId = updateUser.Id;
        }
    }
}
