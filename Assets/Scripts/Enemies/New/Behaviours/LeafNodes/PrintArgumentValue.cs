using UnityEngine;

namespace Ai
{
    public class PrintArgumentValue<T> : LeafNode
    {
        private Argument<T> _arg;

        public PrintArgumentValue(Argument<T> arg)
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