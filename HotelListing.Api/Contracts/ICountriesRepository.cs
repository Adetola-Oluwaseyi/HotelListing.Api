﻿using HotelListing.Api.Data;

namespace HotelListing.Api.Contracts
{
    public interface ICountriesRepository : IGenericRepository<Country>
    {
        Task<Country> GetById(int id);

    }
}
