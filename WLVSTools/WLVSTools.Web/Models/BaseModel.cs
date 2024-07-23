using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using WLVSTools.Web.Models.Common;

namespace WLVSTools.Web.Models
{
    public class BaseModel
    {
        public DateTime? RunStartDateTime { get; set; } = DateTime.Now;
        public DateTime? RunEndDateTime { get; set; }
        public double TotalRunTimeInSeconds
        {
            get
            {
                return RunEndDateTime.HasValue ? (RunEndDateTime.Value - RunStartDateTime.Value).TotalSeconds : 0;
            }
        }

        public List<SelectListItem> CountryList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem("Please select", ""),
                    new SelectListItem("United States", "United States"),
                    new SelectListItem("Canada", "Canada"),
                    new SelectListItem("United Kingdom", "United Kingdom"),
                    new SelectListItem("Australia", "Australia"),
                    new SelectListItem("Brazil", "Brazil"),
                    new SelectListItem("France", "France"),
                    new SelectListItem("Germany", "Germany"),
                    new SelectListItem("Italy", "Italy"),
                    new SelectListItem("Spain", "Spain")
                }.OrderBy(m => m.Value).ToList();
            }
        }

        public IWebHostEnvironment? WebHostEnvironment { get; set; }
        public List<SelectListItem> StateList
        {
            get
            {
                if (WebHostEnvironment == null)
                {
                    return new List<SelectListItem>();
                }

                var json = File.ReadAllText($"{WebHostEnvironment.WebRootPath}/json/us-state-list.json");
                var usStateList = JsonSerializer.Deserialize<List<CountryState>>(json);
                var usListItem = usStateList.Select(item => new SelectListItem($"{item.Name} ({item.Abbreviation})", item.Abbreviation)).ToList();

                usListItem.Insert(0, new SelectListItem("Please select", ""));

                return usListItem;
            }
        }
    }

    public class ApiBaseModel
    {
        public DateTime? RunStartDateTime { get; set; } = DateTime.Now;
        public DateTime? RunEndDateTime { get; set; }
        public double TotalRunTimeInSeconds
        {
            get
            {
                return RunEndDateTime.HasValue ? (RunEndDateTime.Value - RunStartDateTime.Value).TotalSeconds : 0;
            }
        }
    }
}
