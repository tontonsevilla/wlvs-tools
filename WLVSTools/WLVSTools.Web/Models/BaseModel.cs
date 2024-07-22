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

        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public List<SelectListItem> StateList
        {
            get
            {
                var json = File.ReadAllText($"{WebHostEnvironment.WebRootPath}/json/us-state-list.json");
                var usStateList = JsonSerializer.Deserialize<List<CountryState>>(json);
                return usStateList.Select(item => new SelectListItem($"{item.Name} ({item.Abbreviation})", item.Abbreviation)).ToList();
            }
        }
    }
}
