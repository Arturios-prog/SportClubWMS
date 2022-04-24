using Microsoft.EntityFrameworkCore;
using SportClubWMS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubAPI.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;
        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Customer> GetAllCustomers(bool includeGoods)
        {
            if (includeGoods)
                return _appDbContext.Customers.Include(c => c.SportGoods)
                    .Include(c => c.CustomerSportGoods).OrderBy(c => c.FirstName);
            return _appDbContext.Customers;
        }
        public Customer? GetCustomerById(int id, bool includeGoods)
        {
            if (includeGoods)
            {
                return _appDbContext.Customers.Include(s => s.SportGoods)
                    .Include(csg=> csg.CustomerSportGoods)
                    .Where(c => c.Id == id).FirstOrDefault();
            }
            return _appDbContext.Customers.Where(c => c.Id == id)
                .FirstOrDefault();
        }

        public Customer? GetCustomerByName(string name, bool includeGoods)
        {
            if (includeGoods)
            {
                return _appDbContext.Customers.Include(s => s.SportGoods)
                    .Include(csg => csg.CustomerSportGoods)
                    .Where(c => c.FirstName == name).FirstOrDefault();
            }
            return _appDbContext.Customers.Where(c => c.FirstName == name)
                .FirstOrDefault();
        }

        public int? GetCustomerGoodsCount(int customerId)
        {
            return _appDbContext.Customers.Where(c => c.Id == customerId)
                .FirstOrDefault().CustomerSportGoods.Count;
        }

        public Customer AddCustomer(Customer customer)
        {
            var addedEntity = _appDbContext.Customers.Add(customer);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public Customer? UpdateCustomer(Customer customer)
        {
            var foundCustomer = _appDbContext.Customers
                .Where(fc => fc.Id == customer.Id).FirstOrDefault();

            if (foundCustomer != null)
            {
                foundCustomer.FirstName = customer.FirstName;
                foundCustomer.SecondName = customer.SecondName;
                foundCustomer.Age = customer.Age;
                foundCustomer.Gender = customer.Gender;
                foundCustomer.CustomerSportGoods = customer.CustomerSportGoods;
                foundCustomer.SportGoods = customer.SportGoods;
                foundCustomer.Address = customer.Address;
                foundCustomer.Email = customer.Email;
                foundCustomer.SubscribeStatus = customer.SubscribeStatus;
                foundCustomer.RegistrationDate = customer.RegistrationDate;

                _appDbContext.SaveChanges();
                return foundCustomer;
            }

            return null;
        }

        public void DeleteCustomer(int id)
        {
            var foundCustomer = _appDbContext.Customers.FirstOrDefault(c => c.Id == id);

            if (foundCustomer == null) return;

            _appDbContext.Customers.Remove(foundCustomer);
            _appDbContext.SaveChanges();
        }
    }
}
