namespace Ai
{
    public class Add : LeafNode
    {
        private string _writeTo;
        private string[] _readFrom;

        public Add(string writeTo, string[] readFrom)
        {
            _writeTo = writeTo;
            _readFrom = readFrom;
        }

        public override void Process(float dt, Context context)
        {
            int sum = 0;
            foreach (var key in _readFrom)
            {
                sum += context.Get<int>(key);
            }
            context.Set(_writeTo, sum);

            OnCompleted(State.SUCCESS, context);
        }
    }
}