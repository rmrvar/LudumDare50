namespace Ai
{
    public class Add : LeafNode
    {
        private Argument.Out<float> _outputTo;
        private Argument.In<float>[] _arguments;

        public Add(Argument.Out<float> outputTo, Argument.In<float>[] arguments)
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
            _outputTo.Set(sum, context);

            OnCompleted(State.SUCCESS, context);
        }
    }
}