using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Security.Principal;
using WLVSTools.Web.Models.AIFS.General;
using WLVSTools.Web.WebInfrastructure.Extensions;
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
                SafeAccessTokenHandle safeAccessTokenHandle;
                var loginSuccessful = Impersonator.LogonUser(
                    model.UserName, 
                    model.DomainName,
                    model.Password,
                    Impersonator.LOGON32_LOGON_NEW_CREDENTIALS,
                    Impersonator.LOGON32_PROVIDER_DEFAULT, 
                    out safeAccessTokenHandle);
                int returnGetLastWin32Error = Marshal.GetLastWin32Error();

                if (loginSuccessful) 
                {
                    WindowsIdentity.RunImpersonated(safeAccessTokenHandle, () =>
                    {
                        if (!string.IsNullOrWhiteSpace(model.Search))
                        {
                            var filePath = $"{model.Path}\\{model.Search}";
                            FileInfo fileInfo = new FileInfo(filePath);

                            if (fileInfo.Exists)
                            {
                                model.DirectoryFiles.Add(fileInfo.FullName);
                            }
                            else
                            {
                                ModelState.AddErrorMessage($"{filePath} doesn't exists.");
                            }
                        }
                        else
                        {
                            try
                            {
                                var directoryInfo = new DirectoryInfo(model.Path);

                                directoryInfo.GetAccessControl(); // Execute a simple task to check permission grant

                                if (directoryInfo.Exists)
                                {
                                    foreach (var entry in Directory.EnumerateFileSystemEntries(model.Path))
                                    {
                                        if (Directory.Exists(entry))
                                        {
                                            model.DirectoryFiles.Add($"{entry} (Folder)");
                                        }

                                        var fileInfo = new FileInfo(entry);
                                        if (fileInfo.Exists)
                                        {
                                            model.DirectoryFiles.Add($"{entry} (File)");
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ModelState.AddErrorMessage(ex.Message);
                            }
                        }
                    });
                }
                else
                {
                    ModelState.AddErrorMessage($"LogonUser failed with error code : {returnGetLastWin32Error}");
                }
            }

            model.RunEndDateTime = DateTime.Now;

            ViewBag.TotalRunTime = model.TotalRunTimeInSeconds;

            return View(model);
        }
    }
}
