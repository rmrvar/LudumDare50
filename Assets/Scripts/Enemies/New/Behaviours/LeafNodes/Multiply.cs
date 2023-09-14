namespace Ai
{
    public class Multiply : LeafNode
    {
        private Argument.Out<float> _outputTo;
        private Argument.In<float>[] _arguments;

        public Multiply(Argument.Out<float> outputTo, Argument.In<float>[] arguments)
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
            _outputTo.Set(product, context);

            OnCompleted(State.SUCCESS, context);
        }
    }
}