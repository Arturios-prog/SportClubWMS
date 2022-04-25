using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SportClubAPI.Models;
using SportClubWMS.Shared;

namespace SportClubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ISportGoodRepository _sportGoodRepository;
        private readonly ICustomerSportGoodRepository _customerSportGoodRepository;


        public CustomerController(
            ICustomerRepository customerRepository,
            ISportGoodRepository sportGoodRepository,
            ICustomerSportGoodRepository customerSportGoodRepository)
        {
            _customerRepository = customerRepository;
            _sportGoodRepository = sportGoodRepository;
            _customerSportGoodRepository = customerSportGoodRepository;
        }

        // GET: api/<controller>/includegoods
        [HttpGet("includegoods")]
        public IActionResult GetCustomersWithGoods()
        {
            return Ok(_customerRepository.GetAllCustomers(true));
        }

        // GET: api/<controller>/5/count
        [HttpGet("{id}/count")]
        public IActionResult GetCustomerGoodsCount(int id)
        {
            return Ok(_customerRepository.GetCustomerGoodsCount(id));
        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetAllCustomersWithoutGoods()
        {
            return Ok(_customerRepository.GetAllCustomers(false));
        }

        // GET: api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult GetCustomerByIdWithoutGoods(int id)
        {
            var foundCustomer = _customerRepository.GetCustomerById(id, false);
            if (foundCustomer != null)
                return Ok(foundCustomer);
            else return NotFound();
        }

        // GET: api/<controller>/5/includegoods
        [HttpGet("{id}/includegoods")]
        public IActionResult GetCustomerByIdWithGoods(int id)
        {
            var foundCustomer = _customerRepository.GetCustomerById(id, true);
            if (foundCustomer != null)
                return Ok(foundCustomer);
            else return NotFound();
        }

        // GET: api/<controller>/csgoods
        [HttpGet("csgoods")]
        public IActionResult GetAllCustomerSportGoods()
        {
            return Ok(_customerSportGoodRepository.GetAllCustomerSportGoods());
        }

        // GET: api/<controller>/5/onlygoods
        [HttpGet("{id}/onlygoods")]
        public IActionResult GetCustomerGoodsById(int id)
        {
            if (id < 1)
                return BadRequest();
            if (_customerRepository.GetCustomerById(id, false) == null)
                return NotFound();
            return Ok(_customerSportGoodRepository.GetAllCustomerSportGoodsById(id));
        }
        // POST: api/<controller>
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest();
            if (customer.FirstName == string.Empty || customer.SecondName == string.Empty)
                ModelState.AddModelError("Name/SecondName", "The name or second Name should contain a value.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCustomer = _customerRepository.AddCustomer(customer);
            return Created("Customer", createdCustomer);
        }

        // PUT: api/<controller>
        [HttpPut]
        public IActionResult UpdateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest();

            if (customer.FirstName == string.Empty || customer.SecondName == string.Empty)
            {
                ModelState.AddModelError("Name/SecondName", "The name or second name should contain a value");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerToUpdate = _customerRepository.GetCustomerById(customer.Id, true);

            if (customerToUpdate == null)
                return NotFound();
            _customerRepository.UpdateCustomer(customerToUpdate);
            return NoContent();
        }

        // PUT: api/<controller>/customer(5)_good(1)_add(4)
        [HttpPut("customer({customerId})_good({sportGoodId})_add({quantity})")]
        public IActionResult AddSportGoodToCustomer(int customerId, int sportGoodId, uint quantity)
        {
            if (customerId < 1 || sportGoodId < 1 || quantity < 1)
            {
                ModelState.AddModelError("Input", "Invalid input");
                return BadRequest(ModelState);
            }

            var foundCustomer = _customerRepository.GetCustomerById(customerId, true);
            var foundSportGood = _sportGoodRepository.GetSportGoodById(sportGoodId, false);
            var foundCustomerSportGood = _customerSportGoodRepository.GetCustomerSportGoodById(customerId, sportGoodId);

            if (foundCustomer == null || foundSportGood == null)
            {
                ModelState.AddModelError("Not found", "Object not found");
                return NotFound(ModelState);
            }

            if (quantity > foundSportGood.Quantity)
            {
                ModelState.AddModelError("Quantity", $"You can't take a quantity ({quantity}) that is bigger " +
                    $"than a total quantity of goods ({foundSportGood.Quantity}).");
                return BadRequest(ModelState);
            }

            _sportGoodRepository.UpdateQuantitySportGood(sportGoodId, quantity, Operands.SUB);

            if (foundCustomerSportGood == null)
            {
                var csg = new CustomerSportGood
                {
                    SportGoodId = sportGoodId,
                    CustomerId = customerId,
                    Quantity = quantity,
                    Customer = foundCustomer
                };
                _customerSportGoodRepository.AddCustomerSportGood(csg);
                foundCustomer.SportGoods.Add(foundSportGood);
                foundCustomer.CustomerSportGoods.Add(csg);
                foundSportGood.Customers.Add(foundCustomer);
            }
            else
            {
                _customerSportGoodRepository.UpdateCustomerSportGoodQuantity(sportGoodId, quantity, Operands.SUM);
            }

            _customerRepository.UpdateCustomer(foundCustomer);
            return NoContent();
        }
        // PUT: api/<controller>/customer(5)_good(1)_remove(4)
        [HttpPut("customer({customerId})_good({sportGoodId})_remove({quantity})")]
        public IActionResult RemoveSportGoodFromCustomer(int customerId, int sportGoodId, uint quantity)
        {
            if (customerId < 1 || sportGoodId < 1 || quantity < 1)
            {
                ModelState.AddModelError("Input", "Invalid input");
                return BadRequest(ModelState);
            }

            var foundCustomer = _customerRepository.GetCustomerById(customerId, true);
            var foundSportGood = _sportGoodRepository.GetSportGoodById(sportGoodId, false);
            var foundCustomerSportGood = _customerSportGoodRepository.GetCustomerSportGoodById(customerId, sportGoodId);

            if (foundCustomer == null || foundSportGood == null
                || foundCustomerSportGood == null)
            {
                ModelState.AddModelError("Not found", "Object not found");
                return NotFound(ModelState);
            }

            if (quantity > foundCustomerSportGood.Quantity)
            {
                ModelState.AddModelError("Quantity", $"You can't take a quantity ({quantity}) that is bigger " +
                    $"than a quantity of goods for a customer ({foundCustomerSportGood.Quantity}).");
                return BadRequest(ModelState);
            }

            _sportGoodRepository.UpdateQuantitySportGood(sportGoodId, quantity, Operands.SUM);

            if (quantity == foundCustomerSportGood.Quantity)
            {
                _customerSportGoodRepository.DeleteCustomerSportGood(customerId, sportGoodId);
                foundCustomer.SportGoods.Remove(foundSportGood);
                foundCustomer.CustomerSportGoods.Remove(foundCustomerSportGood);
                foundSportGood.Customers.Remove(foundCustomer);
            }
            else
                _customerSportGoodRepository.UpdateCustomerSportGoodQuantity(sportGoodId, quantity, Operands.SUB);

            _customerRepository.UpdateCustomer(foundCustomer);
            return NoContent();
        }

        // DELETE: api/<controller>/5/2
        [HttpDelete("{customerId}/{sportGoodId}")]
        public IActionResult DeleteCustomerSportGood(int customerId, int sportGoodId)
        {
            var foundCsg = _customerSportGoodRepository.GetCustomerSportGoodById(customerId, sportGoodId);
            var foundSportGood = _sportGoodRepository.GetSportGoodById(sportGoodId, false);
            var foundCustomer = _customerRepository.GetCustomerById(customerId, true);

            if (foundCsg == null || foundSportGood == null || foundCustomer == null)
                return NotFound();

            _customerSportGoodRepository.DeleteCustomerSportGood(customerId, sportGoodId);
            foundCustomer.SportGoods.Remove(foundSportGood);
            _customerRepository.UpdateCustomer(foundCustomer);

            return NoContent();
        }

        // DELETE: api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("Input", "InvalidInput");
                return BadRequest(ModelState);
            }
            var customerToDelete = _customerRepository.GetCustomerById(id, true);
            if (customerToDelete == null)
                return NotFound();

            _customerRepository.DeleteCustomer(id);
            return NoContent();
        }

    }
}
