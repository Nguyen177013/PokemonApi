using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IOwnerReppository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int id);
        ICollection<Owner> GetOwnersFromPokemon(int pokemonId);
        ICollection<Pokemon> GetPokemonFromOwner(int ownerId);
        bool OwnerExists(int id);
        bool CreateOwner(Owner owner);
        bool Save();
    }
}
