using PokemonReviewApp.Models.Base;

namespace PokemonReviewApp.Models
{
    public class Owner : BaseModel
    {
        public string Name { get; set; }
        public string Gym { get; set; }

        public Country Country { get; set; }
        public ICollection<PokemonOwner> PokemonOwner
        {
            get; set;
        }
    }
}
