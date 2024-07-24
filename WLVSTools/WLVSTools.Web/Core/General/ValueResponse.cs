namespace WLVSTools.Web.Core.General
{
    public class ValueResponse<T>
        where T : class
    {
        public ValueResponse(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}
