using PhotoSpeech.DataAccess.Models;

namespace PhotoSpeech.DataAccess.Handlers.Interfaces
{
    public interface ICategoryHandler
    {
        Task<List<Category>> GetAllCategories();
    }
}
