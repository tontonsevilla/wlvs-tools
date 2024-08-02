using Microsoft.Win32.SafeHandles;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace WLVSTools.Web.WebInfrastructure.General
{
    public static class Impersonator
    {
        // constants from winbase.h
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_LOGON_NETWORK = 3;
        public const int LOGON32_LOGON_BATCH = 4;
        public const int LOGON32_LOGON_SERVICE = 5;
        public const int LOGON32_LOGON_UNLOCK = 7;
        public const int LOGON32_LOGON_NETWORK_CLEARTEXT = 8;
        public const int LOGON32_LOGON_NEW_CREDENTIALS = 9;

        public const int LOGON32_PROVIDER_DEFAULT = 0;
        public const int LOGON32_PROVIDER_WINNT35 = 1;
        public const int LOGON32_PROVIDER_WINNT40 = 2;
        public const int LOGON32_PROVIDER_WINNT50 = 3;

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool LogonUser(
            String Username, 
            String Domain, 
            String Password, 
            int LogonType, 
            int LogonProvider, 
            out SafeAccessTokenHandle Token);


        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        /*
        public static WindowsImpersonationContext LogOn(string userName, string password)
        {
            return LogOn(userName, password, "");
        }

        public static WindowsImpersonationContext LogOn(string userName, string password, string domain)
        {
            WindowsIdentity tempWindowsIdentity;
            WindowsImpersonationContext impersonationContext;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_NEW_CREDENTIALS,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return impersonationContext;
                        }
                    }
                }
                else
                {
                    var win32 = new Win32Exception(Marshal.GetLastWin32Error());
                    //throw new Exception(string.Format("{0}, Domain:{1}, User:{2}, Password:{3}",
                    //    win32.Message, domain, userName, password));
                    throw new Exception(win32.Message);
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return null; // Failed to impersonate
        }

        public static bool LogOff(WindowsImpersonationContext context)
        {
            bool result = false;
            try
            {
                if (context != null)
                {
                    context.Undo();
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
        */
    }
}
