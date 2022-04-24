using SportClubWMS.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubAPI.Models
{
    public class SportGoodRepository : ISportGoodRepository
    {
        private readonly AppDbContext _appDbContext;
        public SportGoodRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<SportGood> GetAllSportGoods(bool includeCustomers)
        {
            if (includeCustomers)
                return _appDbContext.SportGoods.Include(c => c.Customers);

            return _appDbContext.SportGoods;
        }


        public SportGood? GetSportGoodById(int id, bool includeCustomers)
        {
            if (includeCustomers)
                return _appDbContext.SportGoods.Include(c => c.Customers)
                    .Where(s => s.Id == id).FirstOrDefault();

            return _appDbContext.SportGoods
                .Where(s => s.Id == id).FirstOrDefault();
        }

        public SportGood? GetSportGoodByName(string name, bool includeCustomers)
        {
            if (includeCustomers)
                return _appDbContext.SportGoods.Include(c => c.Customers)
                    .Where(s => s.Name == name).FirstOrDefault();

            return _appDbContext.SportGoods
                .Where(s => s.Name == name).FirstOrDefault();
        }

        public SportGood AddSportGood(SportGood SportGood)
        {
            var addedEntity = _appDbContext.SportGoods.Add(SportGood);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public SportGood? UpdateSportGood(SportGood sportGood)
        {
            var foundSportGood = _appDbContext.SportGoods.FirstOrDefault(c => c.Id == sportGood.Id);

            if (foundSportGood != null)
            {
                foundSportGood.Name = sportGood.Name;
                foundSportGood.Description = sportGood.Description;
                foundSportGood.Customers = sportGood.Customers;
                foundSportGood.Category = sportGood.Category;
                foundSportGood.Quantity = sportGood.Quantity;

                _appDbContext.SaveChanges();
                return foundSportGood;
            }

            return null;
        }

        public void DeleteSportGood(int id)
        {
            var foundSportGood = _appDbContext.SportGoods.FirstOrDefault(c => c.Id == id);
            var foundCustomerSportGood = _appDbContext.CustomerSportGoods.FirstOrDefault(csg => csg.SportGoodId == id);
            if (foundSportGood == null || foundCustomerSportGood == null) return;

            _appDbContext.SportGoods.Remove(foundSportGood);
            _appDbContext.CustomerSportGoods.Remove(foundCustomerSportGood);
            _appDbContext.SaveChanges();
        }

        public void UpdateQuantitySportGood(int id, uint quantity, string operation)
        {
            var foundSportGood = _appDbContext.SportGoods.FirstOrDefault(c => c.Id == id);
            if (foundSportGood != null)
            {
                switch (operation)
                {
                    case Operands.SUM:
                        foundSportGood.Quantity += quantity;
                        break;
                    case Operands.SUB:
                        foundSportGood.Quantity -= quantity;
                        break;

                    default:
                        throw new ArgumentException("The given argument doesn't match any operands");
                }
                _appDbContext.SaveChanges();
            }
        }
    }
}

