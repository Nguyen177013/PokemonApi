using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            this._context = context;
        }
        public ICollection<Category> GetCategories()
        {
            return this._context.Categories.OrderBy(c => c.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return this._context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.PokemonCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
        }

        public bool CategoryExists(int id)
        {
            return this._context.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            this._context.Add(category);
            return Save();
        }

        public bool Save()
        {
            var saved = this._context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
