﻿using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            this._context = context;
        }
        public bool CountryExists(int id)
        {
            return this._context.Countries.Any(c => c.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            this._context.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            this._context.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCounties()
        {
            return this._context.Countries.OrderBy(c => c.Id).ToList();
        }

        public Country GetCountry(int id)
        {
            return this._context.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return this._context.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromCountry(int countryId)
        {
            return this._context.Owners.Where(o => o.Country.Id == countryId).ToList();
        }

        public bool Save()
        {
            var saved = this._context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool UpdateCountry(Country country)
        {
            this._context.Update(country);
            return Save();
        }
    }
}
