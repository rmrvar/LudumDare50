namespace Ai
{
    public class Add : LeafNode
    {
        private string _outputTo;
        private Argument<float>[] _arguments;

        public Add(string outputTo, Argument<float>[] arguments)
        {
            _outputTo = outputTo;
            _arguments = arguments;
        }

        public override void Process(float dt, Context context)
        {
            base.Process(dt, context);

            float sum = 0;
            foreach (var arg in _arguments)
            {
                sum += arg.Get(context);
            }
            context.Set(_outputTo, sum);

            OnCompleted(State.SUCCESS, context);
        }
    }
}