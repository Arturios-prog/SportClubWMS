﻿using SportClubWMS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SportClubWMS.Services
{
    public class SportGoodDataService : ISportGoodDataService
    {
        private readonly HttpClient _httpClient;
        public SportGoodDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<SportGood>?> GetAllCustomers(bool includeCustomers)
        {
            if (!includeCustomers)
                return await JsonSerializer.DeserializeAsync<IEnumerable<SportGood>>
                    (await _httpClient.GetStreamAsync($"api/sportgood"),
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            else
                return await JsonSerializer.DeserializeAsync<IEnumerable<SportGood>>
                    (await _httpClient.GetStreamAsync($"api/sportgood/includecustomers"),
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<SportGood?> GetSportGoodById(int sportGoodId, bool includeCustomers)
        {
            if (!includeCustomers)
                return await JsonSerializer.DeserializeAsync<SportGood>
                    (await _httpClient.GetStreamAsync($"api/sportgood/{sportGoodId}"),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            else
                return await JsonSerializer.DeserializeAsync<SportGood>
                    (await _httpClient.GetStreamAsync($"api/sportgood/{sportGoodId}/includecustomers"),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task DeleteSportGood(int sportGoodId)
        {
            await _httpClient.DeleteAsync($"api/sportgood/{sportGoodId}");
        }
    }
}
