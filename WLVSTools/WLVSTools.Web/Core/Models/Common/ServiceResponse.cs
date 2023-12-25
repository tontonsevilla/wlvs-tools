namespace WLVSTools.Web.Core.Models.Common
{
    public class ServiceResponse<T>
        where T : class, new()
    {
        private T? _model;
        private List<string> _messages = new List<string>();

        public T? Model
        {
            get
            {
                return _model;
            }
        }

        public List<string> Messages
        {
            get
            {
                return _messages;
            }
        }

        public bool HasData
        {
            get
            {
                return _model != null;
            }
        }

        public bool HasError
        {
            get
            {
                return _messages.Count > 0;
            }
        }

        public void AddModel(T model)
        {
            if (model != null)
            {
                _model = model;
            }
        }

        public void AddMessages(string message)
        {
            _messages.Add(message);
        }

        public void AddMessages(List<string> messages)
        {
            _messages.AddRange(messages);
        }
    }
}
