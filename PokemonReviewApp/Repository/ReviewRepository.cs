using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            this._context = context;
        }

        public bool CreateReview(Review review)
        {
            this._context.Add(review);
            return this.Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            this._context.RemoveRange(reviews);
            return Save();
        }
        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public Review GetReview(int id)
        {
            return this._context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviewFromPokemon(int pokemonId)
        {
            return this._context.Reviews.Where(r => r.Pokemon.Id == pokemonId).ToList();
        }

        public ICollection<Review> GetReviews()
        {
            return this._context.Reviews.OrderBy(r => r.Id).ToList();
        }

        public bool ReviewExists(int id)
        {
            return this._context.Reviews.Any(r => r.Id == id);
        }

        public bool Save()
        {
            var saved = this._context.SaveChanges();
            return saved > 0 ? true: false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }

    }
}
