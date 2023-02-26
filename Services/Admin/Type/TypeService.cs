using AutoMapper;
using bgbrokersapi.Data;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;
using Microsoft.EntityFrameworkCore;

namespace bgbrokersapi.Services.Admin.Type
{
    public class TypeService : ITypeService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public TypeService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<TypeResponseModel> Create<T>(TypeInputModel model) where T : Data.Models.Abstract.Type, new()
        {
            var responseModel = new TypeResponseModel();

            if (await TypeExist<T>(model.Name))
            {
                responseModel.Status = StatusCodes.Status400BadRequest;
                responseModel.Message = "Type already exist!";
                return responseModel;
            }

            var type = new T()
            {
                Name = model.Name,
            };

            try
            {
                await dbContext.AddAsync(type);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                responseModel.Status = StatusCodes.Status400BadRequest;
                responseModel.Message = "Unexpected error. Try again!";
            }

            responseModel.Status = StatusCodes.Status201Created;
            responseModel.Message = "Creation is successful";
            responseModel.Type = mapper.Map<TypeModel>(type);

            return responseModel;
        }

        public async Task<ResponseModel> DeleteType<T>(int id) where T : class
        {
            var response = new ResponseModel();

            try
            {
                var constructionToDelete = await GetById<T>(id);
                dbContext.Remove(constructionToDelete);
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

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById<T>(int? id) where T : class
        {
            var type = await dbContext.Set<T>().FindAsync(id);

            if (type == null)
            {
                throw new ArgumentException("Type is not found");
            }

            return type;
        }

        public async Task<bool> TypeExist<T>(string name) where T : Data.Models.Abstract.Type
        {
            return await dbContext.Set<T>().AnyAsync(x => x.Name == name);
        }

        public async Task<bool?> TypeExist<T>(int? id) where T : Data.Models.Abstract.Type
        {
            if (id == null)
            {
                return null;
            }

            return await dbContext.Set<T>().AnyAsync(x => x.Id == id);
        }

        public async Task<TypeResponseModel> UpdateType<T>(int id, TypeInputModel model) where T : Data.Models.Abstract.Type
        {
            var response = new TypeResponseModel();

            try
            {
                var typeToUpdate = await GetById<T>(id);

                typeToUpdate.Name = model.Name;

                var type = mapper.Map<TypeModel>(typeToUpdate);

                response.Status = StatusCodes.Status202Accepted;
                response.Message = "Type is updated successful";
                response.Type = type;

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
    }
}
