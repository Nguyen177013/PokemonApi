using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            this._context = context;
        }

        public Pokemon GetPokemon(int id)
        {
            return this._context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return this._context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int id)
        {
            var review = this._context.Reviews.Where(r => r.Pokemon.Id == id);
            if (review.Count() < 0)
            {
                return 0;
            }
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return this._context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int id)
        {
            return this._context.Pokemons.Any(p => p.Id == id);
        }
    }
}
