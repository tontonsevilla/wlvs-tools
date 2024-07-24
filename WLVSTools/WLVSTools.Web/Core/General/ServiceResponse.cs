namespace WLVSTools.Web.Core.General
{
    public class ServiceResponse<T>
        where T : class
    {
        public ServiceResponse()
        {
            ErrorMessages = new List<string>();
        }

        public T? Model { get; private set; }
        public bool HasData { get; private set; }
        
        public void AddModel(T model)
        {
            if (model != null)
            {
                Model = model;
                HasData = true;
            }
        }

        public List<string> ErrorMessages { get; private set; }
        
        public void AddErrorMessage(string message)
        {
            ErrorMessages.Add(message);
        }

        public void AddErrorMessages(IEnumerable<string> errorMessages)
        {
            ErrorMessages.AddRange(errorMessages);
        }

        public bool HasError
        {
            get
            {
                return ErrorMessages.Count > 0;
            }
        }
    }
}
