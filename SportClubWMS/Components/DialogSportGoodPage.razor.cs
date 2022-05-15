using Microsoft.AspNetCore.Components;
using SportClubWMS.Services;
using SportClubWMS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubWMS.Components
{
    public partial class DialogSportGoodPage
    {
        [Parameter]
        public int SportGoodId { get; set; }
        public SportGood SportGood { get; set; } = new SportGood();
        [Inject]
        public ICustomerDataService CustomerDataService { get; set; }
        [Inject]
        public ISportGoodDataService SportGoodDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SportGood = await SportGoodDataService.GetSportGoodById(SportGoodId, true);
        }
    }
}
