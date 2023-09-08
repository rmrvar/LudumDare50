namespace Ai
{
    public class Argument<T>
    {
        private bool _isDirect;
        private string _key;
        private T _value;

        private Argument()
        {
        }

        public static Argument<T> FromKey(string name)
        {
            return new Argument<T>()
            {
                _isDirect = false,
                _key = name
            };
        }

        public static Argument<T> FromValue(T value)
        {
            return new Argument<T>()
            {
                _isDirect = true,
                _value = value
            };
        }

        public T Get(Context context)
        {
            if (_isDirect)
            {
                return _value;
            }
            else
            {
                return context.Get<T>(_key);
            }
        }
    }
}