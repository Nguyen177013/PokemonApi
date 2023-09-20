using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            this._context = context;
        }
        public ICollection<Review> GetReviewByReviewer(int reviewerId)
        {
            return this._context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public Reviewer GetReviewer(int id)
        {
            return this._context.Reviewers.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return this._context.Reviewers.OrderBy(r => r.Id).ToList();
        }

        public bool ReviewerExists(int id)
        {
            return this._context.Reviewers.Any(r => r.Id == id);
        }
    }
}
