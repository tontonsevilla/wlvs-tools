namespace WLVSTools.Web.Core.Models.Common
{
    public class ServiceResponse<T>
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

        public string HtmlMessages
        {
            get
            {
                var htmlString = "";

                foreach (var message in _messages)
                {
                    htmlString += $"<li>{message}</li>";
                }

                if (_messages.Count > 0)
                {
                    return $"<ul>{htmlString}</ul>";
                }

                return htmlString;
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

        public void AddMessage(string message)
        {
            _messages.Add(message);
        }

        public void AddMessages(List<string> messages)
        {
            _messages.AddRange(messages);
        }
    }
}
