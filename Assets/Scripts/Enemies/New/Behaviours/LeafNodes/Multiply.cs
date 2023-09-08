namespace Ai
{
    public class Multiply : LeafNode
    {
        private string _outputTo;
        private Argument<float>[] _arguments;

        public Multiply(string outputTo, Argument<float>[] arguments)
        {
            _outputTo = outputTo;
            _arguments = arguments;
        }

        public override void Process(float dt, Context context)
        {
            base.Process(dt, context);

            float product = 1;
            foreach (var arg in _arguments)
            {
                product *= arg.Get(context);
            }
            context.Set(_outputTo, product);

            OnCompleted(State.SUCCESS, context);
        }
    }
}