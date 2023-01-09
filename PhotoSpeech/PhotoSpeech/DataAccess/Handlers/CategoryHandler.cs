using PhotoSpeech.DataAccess.Handlers.Interfaces;
using PhotoSpeech.DataAccess.Models;

namespace PhotoSpeech.DataAccess.Handlers
{
    public class CategoryHandler : ICategoryHandler
    {
        private SqlDataAccess _db;

        public CategoryHandler()
        {
            _db = new SqlDataAccess();
        }
        public async Task<List<Category>> GetAllCategories()
        {
            var sql = $"SELECT * FROM [dbo].[Categories]";

            var categories = await _db.LoadData<Category>(sql);


            return categories;
        }
    }
}
