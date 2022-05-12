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

namespace SportClubWMS.Pages
{
    public partial class SportGoodOverview
    {
        public List<SportGood> sportGoods;

        [Inject]
        public ISportGoodDataService SportGoodDataService { get; set; }

        [Inject]
        public DialogService DialogService { get; set; }
        [Inject]
        public IRefreshService RefreshService { get; set; }

        RadzenDataGrid<SportGood> dataGrid { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadSportGoodsAsync();
        }

        private async Task LoadSportGoodsAsync()
        {
            sportGoods = (await SportGoodDataService.GetAllSportGoods(true)).ToList();
        }
        public async Task OpenCustomerInfoAsync(int id)
        {
            /*await DialogService.OpenAsync<DialogCustomerPage>($"customer {id}",
               new Dictionary<string, object>() { { "CustomerId", id } },
               new DialogOptions() { Width = "700px", Height = "570px" });
            RefreshService.RefreshRequested += RefreshMe;*/
        }

        public async Task OpenCustomerEditAsync(int id)
        {
            /*await DialogService.OpenAsync<DialogEditCustomerPage>($"customer {id}",
               new Dictionary<string, object>() {
                   { "CustomerId", id },
                   {"dataGrid", dataGrid } },
               new DialogOptions() { Width = "1000px", Height = "600px" });*/

        }

        public async Task AddNewCustomerAsync()
        {
            /*await DialogService.OpenAsync<DialogEditCustomerPage>("Add new Customer",
                new Dictionary<string, object>() { { "dataGrid", dataGrid } },
                new DialogOptions() { Width = "1000px", Height = "600px" });*/
        }

        protected async Task DeleteSportGoodAsync(int id)
        {
            var confirmResult = await DialogService.Confirm("Are you sure you want to delete this sportGood?", "Warning");
            if (confirmResult.HasValue && confirmResult.Value)
            {
                try
                {
                    var sportGood = await SportGoodDataService.GetSportGoodById(id, true);
                    if (sportGood != null)
                        await SportGoodDataService.DeleteSportGood(id);
                    
                    await LoadSportGoodsAsync();

                    StateHasChanged();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


        }

        void OnRender(DataGridRenderEventArgs<SportGood> args)
        {
            if (args.FirstRender)
            {
                StateHasChanged();
            }
        }

        private void RefreshMe()
        {
            StateHasChanged();
        }

        void OnGroupRowRender(GroupRowRenderEventArgs args)
        {
            if (args.FirstRender && args.Group.Data.Key == "Vice President, Sales")
            {
                args.Expanded = false;
            }
        }

        void OnGroupRowExpand(Group group)
        {
            Console.WriteLine($"Group row with key: {group.Data.Key} expanded");
        }

        void OnGroupRowCollapse(Group group)
        {
            Console.WriteLine($"Group row with key: {group.Data.Key} collapsed");
        }

        void OnGroup(DataGridColumnGroupEventArgs<SportGood> args)
        {
            Console.WriteLine($"DataGrid {(args.GroupDescriptor != null ? "grouped" : "ungrouped")} by {args.Column.GetGroupProperty()}");
        }
    }
}
