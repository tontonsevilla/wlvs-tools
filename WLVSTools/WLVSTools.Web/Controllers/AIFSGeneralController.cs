using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using System.Security.Principal;
using WLVSTools.Web.Models.AIFS.General;
using WLVSTools.Web.WebInfrastructure.General;

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
                SafeAccessTokenHandle safeAccessTokenHandle = null;
                var loginSuccessful = Impersonator.LogonUser(
                    @"USERNAME HERE", 
                    "DOMAIN HERE", 
                    "PASSWORD HERE", 
                    Impersonator.LOGON32_LOGON_NEW_CREDENTIALS, 
                    Impersonator.LOGON32_PROVIDER_DEFAULT, 
                    out safeAccessTokenHandle);

                if (loginSuccessful && safeAccessTokenHandle != null) 
                {
                    WindowsIdentity.RunImpersonated(safeAccessTokenHandle, () =>
                    {
                        FileInfo fileInfo = new FileInfo(model.Path);

                        if (fileInfo.Exists)
                        {
                            model.DirectoryFiles.Add(fileInfo.FullName);
                        }
                    });
                }
            }

            model.RunEndDateTime = DateTime.Now;

            ViewBag.TotalRunTime = model.TotalRunTimeInSeconds;

            return View(model);
        }
    }
}
