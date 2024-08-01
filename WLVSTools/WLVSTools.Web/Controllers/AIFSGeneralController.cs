using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.Models.AIFS.General;

namespace WLVSTools.Web.Controllers
{
    [Area("AIFS"), Route("AIFS/General/[action]")]
    public class AIFSGeneralController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AIFSGeneralController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult NetworkDirectory(NetworkDirectory model)
        {
            model.WebHostEnvironment = _webHostEnvironment;

            if (!model.IsSubmitted)
                ModelState.Clear();

            if (model.IsSubmitted && ModelState.IsValid)
            {
                FileInfo fileInfo = new FileInfo(model.Path);

                if (fileInfo.Exists)
                {
                    model.DirectoryFiles.Add(fileInfo.FullName);
                }
            }

            model.RunEndDateTime = DateTime.Now;

            ViewBag.TotalRunTime = model.TotalRunTimeInSeconds;

            return View(model);
        }
    }
}
