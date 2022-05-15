﻿using Microsoft.AspNetCore.Components;
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
    public partial class DialogEditSportGoodPage
    {
        [Parameter]
        public int SportGoodId { get; set; }
        [Parameter]
        public RadzenDataGrid<SportGood> dataGrid { get; set; }

        public SportGood SportGood = new SportGood();
        [Inject]
        public ISportGoodDataService SportGoodDataService { get; set; }
        [Inject]
        public DialogService Dialog { get; set; }

        private class CategoryType
        {
            public string CategoryName { get; set; } = string.Empty;
            public SportCategory EnumValue { get; set; }
        }
        private List<CategoryType> _categories = new List<CategoryType>();

        protected override async Task OnInitializedAsync()
        {
            if (SportGoodId == 0)
            {
                SportGood = new SportGood {};
            }
            else
            {
                SportGood = await SportGoodDataService.GetSportGoodById(SportGoodId, true);
            }
            foreach (SportCategory category in (SportCategory[])Enum.GetValues(typeof(SportCategory)))
            {
                _categories.Add(new CategoryType { CategoryName = category.ToString(), EnumValue = category });
            }

        }

        public void HandleInvalidSubmit()
        {

        }

        protected async Task HandleValidSubmit()
        {
            if (SportGood.SportGoodId == 0)
            {
                await SportGoodDataService.AddSportGood(SportGood);
            }
            else
            {
                await SportGoodDataService.UpdateSportGood(SportGood);
                
            }

            Dialog.Close();
            StateHasChanged();
        }

        public void Close()
        {
            Dialog.Close();
        }

    }
}
