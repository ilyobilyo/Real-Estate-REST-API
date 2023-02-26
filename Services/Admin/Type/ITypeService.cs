using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;

namespace bgbrokersapi.Services.Admin.Type
{
    public interface ITypeService
    {
        Task<TypeResponseModel> Create<T>(TypeInputModel model) where T : Data.Models.Abstract.Type, new();
        Task<bool> TypeExist<T>(string name) where T : Data.Models.Abstract.Type;
        Task<bool?> TypeExist<T>(int? id) where T : Data.Models.Abstract.Type;

        Task<T> GetById<T>(int? id) where T : class;
        Task<TypeResponseModel> UpdateType<T>(int id, TypeInputModel model) where T : Data.Models.Abstract.Type;
        Task<ResponseModel> DeleteType<T>(int id) where T : class;
        Task<IEnumerable<T>> GetAll<T>() where T : class;

    }
}
