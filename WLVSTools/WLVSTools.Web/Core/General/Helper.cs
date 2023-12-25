namespace WLVSTools.Web.Core.General
{
    public static class Helper
    {
        public static List<string> GetExceptionMessages(Exception ex)
        {
            var messages = new List<string>();

            if (ex.InnerException != null)
            {
                GetExceptionMessages(ex.InnerException);
            }

            messages.Add(ex.Message);

            return messages;
        }
    }
}
