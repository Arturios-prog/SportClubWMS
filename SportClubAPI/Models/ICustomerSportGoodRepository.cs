using SportClubWMS.Shared;

namespace SportClubAPI.Models
{
    public interface ICustomerSportGoodRepository
    {
        IEnumerable<CustomerSportGood> GetAllCustomerSportGoods();
        CustomerSportGood GetCustomerSportGoodById(int customerId, int sportGoodId);
        void UpdateCustomerSportGoodQuantity(int sportGoodId, uint quantity, string operation);
        CustomerSportGood AddCustomerSportGood(CustomerSportGood csg);
        void DeleteCustomerSportGood(int customerId, int sportGoodId);
    }
}