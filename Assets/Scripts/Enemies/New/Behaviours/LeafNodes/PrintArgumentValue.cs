using UnityEngine;

namespace Ai
{
    public class PrintArgumentValue<T> : LeafNode
    {
        private Argument.In<T> _arg;

        public PrintArgumentValue(Argument.In<T> arg)
        {
            _arg = arg;
        }

        public override void Process(float dt, Context context)
        {
            base.Process(dt, context);

            Debug.Log(_arg.Get(context));
            OnCompleted(State.SUCCESS, context);
        }
    }
}