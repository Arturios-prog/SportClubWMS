using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using SportClubWMS.Services;
using SportClubWMS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubWMS.Components
{
    public partial class DialogEditCustomerPage
    {
        [Parameter]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = new Customer();
        public SportGood SportGood { get; set; } = new SportGood();

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }
        [Parameter]
        public RadzenDataGrid<Customer> dataGrid { get; set; }

        [Inject]
        public DialogService? DialogService { get; set; }

        public List<SportGood> SportGoods { get; set; } = new List<SportGood>();
        public List<string> SportGoodNames { get; set; } = new List<string>();
        public List<string> SportGoodNamesToPick { get; set; } = new List<string>();
        public List<string> CustomerSportGoodNames { get; set; } = new List<string>();
        public Dictionary<int, uint> TempQuantities { get; set; } = new Dictionary<int, uint>();
        
        //Max Quantity of Good that user can take
        public Dictionary<int, uint> MaxQuantities { get; set; } = new Dictionary<int, uint>();
        public List<int> AddedCsgs { get; set; } = new List<int>();
        public Dictionary<int, uint> TempQuantitiesOfDeletedCsgs { get; set; } = new Dictionary<int, uint>();
        [Inject]
        public ICustomerDataService CustomerDataService { get; set; }
        [Inject]
        public ISportGoodDataService SportGoodDataService { get; set; }
        [Inject]
        public DialogService Dialog { get; set; }
        [Inject]
        public IRefreshService RefreshService { get; set; }

        RadzenDataGrid<CustomerSportGood> csgGrid;
        RadzenDropDown<SubscribeStatus> dropDownStatus;
        RadzenNumeric<uint> plusNumeric;

        CustomerSportGood? csgToInsert { get; set; }


        protected override async Task OnInitializedAsync()
        {
            if (CustomerId == 0)
            {
                Customer = new Customer { SubscribeStatus = 0, Gender = 0, RegistrationDate = DateTime.Now };
            }
            else
            {
                Customer = await CustomerDataService.GetCustomerById(CustomerId, true);
            }

            //Some logic for finding out which goods customer already has
            SportGoods = (await SportGoodDataService.GetAllSportGoods(false)).ToList();
            TempQuantities = new Dictionary<int, uint>();
            foreach (var sportGood in SportGoods)
            {
                SportGoodNames.Add(sportGood.Name);
                TempQuantities.Add(sportGood.Id, 0);
                MaxQuantities.Add(sportGood.Id, sportGood.Quantity);
            }

            RepickNames();

            //Saving all the quantities in a temporary List
            foreach (var customerSportGood in Customer.CustomerSportGoods)
            {
                if (TempQuantities.ContainsKey(customerSportGood.SportGoodId))
                {
                    TempQuantities[customerSportGood.SportGoodId] = customerSportGood.Quantity;
                }
                else TempQuantities.Add(customerSportGood.SportGoodId, customerSportGood.Quantity);

                Console.WriteLine($"Added TempQuantity with SportGoodId {customerSportGood.SportGoodId}, and quantity {customerSportGood.Quantity}");

            }
        }

        //Determines a list of sport goods that client doesn't have yet
        private void RepickNames()
        {
            CustomerSportGoodNames = new List<string>();
            foreach (var customerSportGood in Customer.CustomerSportGoods)
            {
                CustomerSportGoodNames.Add(customerSportGood.SportGoodName);
            }
            SportGoodNamesToPick = SportGoodNames.Except(CustomerSportGoodNames).ToList();
        }

        private void AddTempQuantity(CustomerSportGood csg)
        {
            TempQuantities.Add(csg.SportGoodId, 0);
            Console.WriteLine($"Added TempQuantity with SportGoodId {csg.SportGoodId}, and quantity {csg.Quantity}");
        }

        //Determines whether customer's subscribe status matches to an applied quantity and changes it's status if needed
        private void CheckSubscribe(CustomerSportGood csg)
        {
            if (csg.Quantity > 1)
                dropDownStatus.Disabled = true;

            else if (Customer.SubscribeStatus.Equals(SubscribeStatus.SubscribeGeneral) && csg.Quantity > 1)
            {
                dropDownStatus.Value = SubscribeStatus.SubscribePlus;
                dropDownStatus.Disabled = true;

            }
            else
            {
                dropDownStatus.Disabled = false;
            }
        }

        //Makes it so that quantity of sport goods updates based on whether client took them or not
        private async Task UpdateSportGoodQuantityOnSave(CustomerSportGood csg)
        {
            if (TempQuantities[csg.SportGoodId] > csg.Quantity)
            {
                Console.WriteLine($"Added {TempQuantities[csg.SportGoodId] - csg.Quantity} to SportGood with id {csg.SportGoodId}");
                await SportGoodDataService.UpdateQuantitySportGood(csg.SportGoodId, TempQuantities[csg.SportGoodId] - csg.Quantity, false);
            }


            if (TempQuantities[csg.SportGoodId] < csg.Quantity)
            {
                Console.WriteLine($"Removed {csg.Quantity - TempQuantities[csg.SportGoodId]} from SportGood with id {csg.SportGoodId}");
                await SportGoodDataService.UpdateQuantitySportGood(csg.SportGoodId, csg.Quantity - TempQuantities[csg.SportGoodId], true);
            }
            else
                Console.WriteLine("Did Nothing");
        }

        async Task InsertRow()
        {
            SportGood = await SportGoodDataService.GetSportGoodByName(SportGoodNamesToPick.FirstOrDefault().Replace(" ", ""), false);
            if (SportGood != null)
                csgToInsert = new CustomerSportGood { SportGoodId = SportGood.Id };

            else csgToInsert = new CustomerSportGood();
            await csgGrid.InsertRow(csgToInsert);
        }

        async Task DeleteRow(CustomerSportGood customerSportGood)
        {
            if (customerSportGood == csgToInsert)
            {
                csgToInsert = null;
            }

            if (Customer.CustomerSportGoods.Contains(customerSportGood))
            {

                if (Customer.Id != 0)
                {
                    var foundCustomer = await CustomerDataService.GetCustomerById(CustomerId, true);
                    var foundCsg = foundCustomer.CustomerSportGoods.FirstOrDefault(csg => csg.SportGoodId == customerSportGood.SportGoodId);
                    if (foundCustomer.CustomerSportGoods.Where(csg => csg.Quantity == customerSportGood.Quantity && csg.SportGoodId == customerSportGood.SportGoodId).FirstOrDefault() != null)
                    {

                        TempQuantitiesOfDeletedCsgs.Add(customerSportGood.SportGoodId, customerSportGood.Quantity);

                    }
                    else if (foundCsg != null)
                    {
                        TempQuantitiesOfDeletedCsgs.Add(foundCsg.SportGoodId, foundCsg.Quantity);
                    }
                }
                Customer.CustomerSportGoods.Remove(customerSportGood);
                RepickNames();
                StateHasChanged();
                await csgGrid.Reload();
            }
            else
            {
                csgGrid.CancelEditRow(customerSportGood);
            }
        }

        void OnCreateRow(CustomerSportGood csg)
        {
            Customer.CustomerSportGoods.Add(csg);

            RepickNames();
        }

        async Task SaveRow(CustomerSportGood? csg)
        {
            if (csg == csgToInsert)
            {
                csgToInsert = null;
            }

            await csgGrid.UpdateRow(csg);
            SportGood = await SportGoodDataService.GetSportGoodByName(csg.SportGoodName.Replace(" ", ""), false);

            if (csg.SportGoodId != SportGood.Id)
            {
                Console.WriteLine($"csg SportGoodId != SportGood.Id");
                //If Updating an existing customer
                if (Customer.Id != 0)
                {
                    var foundCustomer = await CustomerDataService.GetCustomerById(CustomerId, true);
                    var foundCsg = foundCustomer.CustomerSportGoods.FirstOrDefault(customerSportGood => customerSportGood.Id == csg.Id);

                    if (csg.SportGoodId != 0 && foundCsg != null)
                    {
                        TempQuantitiesOfDeletedCsgs.Add(csg.SportGoodId, foundCsg.Quantity);

                    }
                }
                
                csg.SportGoodId = SportGood.Id;
                if (TempQuantities.ContainsKey(csg.SportGoodId) && TempQuantitiesOfDeletedCsgs.ContainsKey(csg.SportGoodId))
                {
                    Console.WriteLine("Found a new csg that was already deleted");
                    TempQuantitiesOfDeletedCsgs.Remove(csg.SportGoodId);
                }

            }


            if (!TempQuantities.ContainsKey(csg.SportGoodId))
                AddTempQuantity(csg);

            RepickNames();
        }

        async Task EditRow(CustomerSportGood csg)
        {
            await csgGrid.EditRow(csg);
        }

        void CancelEdit(CustomerSportGood csg)
        {
            if (csg == csgToInsert)
            {
                csgToInsert = null;
            }

            csgGrid.CancelEditRow(csg);

        }

        private void OnUpdateRow(CustomerSportGood csg)
        {
            if (csg == csgToInsert)
            {
                csgToInsert = null;
            }

            RepickNames();
        }

        private void OnEditRow(CustomerSportGood csg)
        {
            CheckSubscribe(csg);
        }


        public void HandleInvalidSubmit()
        {

        }

        protected async Task HandleValidSubmit()
        {
            if (Customer.Id == 0)
            {
                await CustomerDataService.AddCustomer(Customer);
                foreach (var customerSportGood in Customer.CustomerSportGoods)
                {
                    await UpdateSportGoodQuantityOnSave(customerSportGood);
                }
            }
            else
            {
                await CustomerDataService.UpdateCustomer(Customer);
                foreach (var customerSportGood in Customer.CustomerSportGoods)
                {
                    await UpdateSportGoodQuantityOnSave(customerSportGood);
                }
                foreach (var good in TempQuantitiesOfDeletedCsgs)
                {
                    await SportGoodDataService.UpdateQuantitySportGood(good.Key, good.Value, false);
                }
            }

            RefreshService.CallRequestRefresh();
            Dialog.Close();
            StateHasChanged();
        }

        public void Close()
        {
            Dialog.Close();
        }
    }
}
