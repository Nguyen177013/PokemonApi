using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerReppository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            this._context = context;
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public Owner GetOwner(int id)
        {
            return this._context.Owners.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwners()
        {
            return this._context.Owners.OrderBy(o => o.Id).ToList();
        }

        public ICollection<Owner> GetOwnersFromPokemon(int pokemonId)
        {
            return this._context.PokemonOwners.Where(p => p.PokemonId == pokemonId).Select(o => o.Owner).ToList();
        }

        public ICollection<Pokemon> GetPokemonFromOwner(int ownerId)
        {
            return this._context.PokemonOwners.Where(p => p.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int id)
        {
            return this._context.Owners.Any(o => o.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
