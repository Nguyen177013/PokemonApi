using PokemonReviewApp.Models.Base;

namespace PokemonReviewApp.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }

        public ICollection<PokemonCategory> PokemonCategories { get; set; }
    }
}
