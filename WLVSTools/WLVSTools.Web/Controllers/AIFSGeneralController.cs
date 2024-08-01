using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
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

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool LogonUser(String Username, String Domain, String Password, int LogonType, int LogonProvider, out SafeAccessTokenHandle Token);
        
        public IActionResult NetworkDirectory(NetworkDirectory model)
        {
            model.WebHostEnvironment = _webHostEnvironment;

            if (!model.IsSubmitted)
                ModelState.Clear();

            if (model.IsSubmitted && ModelState.IsValid && Directory.Exists(model.Path))
            {
                Task.Factory.StartNew(() => {
                    var dir = new DirectoryInfo(model.Path);
                    var files = dir.EnumerateFiles("*.*", SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        //Action<string> del = f => model.DirectoryFiles.Add((string)f);
                        //BeginInvoke(del, file);
                        model.DirectoryFiles.Add(file.FullName);
                    }
                });
            }

            model.RunEndDateTime = DateTime.Now;

            ViewBag.TotalRunTime = model.TotalRunTimeInSeconds;

            return View(model);
        }
    }
}
