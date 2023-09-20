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

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = this._context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var categoryEntity = this._context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            var pokemonCategory = new PokemonCategory()
            {
                Category = categoryEntity,
                Pokemon = pokemon
            };
            this._context.Add(pokemon);
            this._context.Add(pokemonOwner);
            this._context.Add(pokemonCategory);
            return this.Save();
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

        public bool UpdatePokemon(Pokemon pokemon)
        {
            this._context.Update(pokemon);
            return Save();
        }

        public bool Save()
        {
            var saved = this._context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DeletePokemon(Pokemon pokemon)
        {
            this._context.Remove(pokemon);
            return Save();
        }
    }
}
