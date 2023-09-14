namespace Ai
{
    public static class Argument
    {
        public static ArgumentFromKey<T> FromKey<T>(string key)
        {
            return new ArgumentFromKey<T>()
            {
                Key = key
            };
        }

        public static ArgumentFromValue<T> FromValue<T>(T value)
        {
            return new ArgumentFromValue<T>()
            {
                Value = value
            };
        }

        public interface In<T>
        {
            T Get(Context context);
        }

        public interface Out<T>
        {
            void Set(T value, Context context);
        }

        public interface InOut<T>
        {
            T Get(Context context);
            void Set(T value, Context context);
        }
    }

    public sealed class ArgumentFromValue<T> : Argument.In<T>
    {
        public ArgumentFromValue()
        {
        }

        public T Value { get; set; }

        public T Get(Context context)
        {
            return Value;
        }
    }

    public sealed class ArgumentFromKey<T>
        : Argument.In<T>
        , Argument.Out<T>
        , Argument.InOut<T>
    {
        public ArgumentFromKey()
        {
        }

        public string Key { get; set; }

        public T Get(Context context)
        {
            return context.Get<T>(Key);
        }

        public void Set(T value, Context context)
        {
            context.Set(Key, value);
        }
    }
}