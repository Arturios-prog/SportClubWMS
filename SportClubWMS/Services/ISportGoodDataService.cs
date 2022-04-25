using SportClubWMS.Shared;

namespace SportClubWMS.Services
{
    public interface ISportGoodDataService
    {
        Task DeleteSportGood(int sportGoodId);
        Task<IEnumerable<SportGood>?> GetAllCustomers(bool includeCustomers);
        Task<SportGood?> GetSportGoodById(int sportGoodId, bool includeCustomers);
    }
}