using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.DTO;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IPokemonRepository _pokemonRepository;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, IReviewerRepository reviewerRepository, IPokemonRepository pokemonRepository)
        {
            this._reviewRepository = reviewRepository;
            this._mapper = mapper;
            this._reviewerRepository = reviewerRepository;
            this._pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]

        public IActionResult GetReviews()
        {
            var reviews = this._mapper.Map<List<ReviewDto>>(this._reviewRepository.GetReviews());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int reviewId)
        {
            if (!this._reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var review = this._mapper.Map<ReviewDto>(this._reviewRepository.GetReview(reviewId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }

        [HttpGet("pokemon/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewFromPokemon(int pokemonId)
        {
            var review = this._mapper.Map<List<ReviewDto>>(this._reviewRepository.GetReviewFromPokemon(pokemonId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokemonId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
            {
                return BadRequest(ModelState);
            }
            var review = this._reviewRepository.GetReviews().Where(p => p.Title.Trim().ToUpper() == reviewCreate.Title.ToUpper()).FirstOrDefault();
            if (review != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }
            var reviewMap = this._mapper.Map<Review>(reviewCreate);

            reviewMap.Pokemon = _pokemonRepository.GetPokemon(pokemonId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(pokemonId);

            if (!this._reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(422, ModelState);
            }
            return Ok("Successfully Created");
        }
    }
}
