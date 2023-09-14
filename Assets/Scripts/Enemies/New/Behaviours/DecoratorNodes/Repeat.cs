using UnityEngine;

namespace Ai
{
    public class Repeat : DecoratorNode
    {
        private string STOP_REPEATING_AT = "StopRepeatingAt";

        private bool _shouldRepeatForever;
        private int _n;

        public Repeat(Node child) : base(child)
        {
            _shouldRepeatForever = true;
        }

        public Repeat(int n, Node child) : base(child)
        {
            Debug.Assert(n > 0, "Number of times to repeat should be greater than 0!");
            _shouldRepeatForever = false;
            _n = n;
        }

        protected override void StartSelf(Context context)
        {
            if (_n > 0)
            {
                int stopRepeatingAt = _n;
                if (context.HasNodeValue(Child, Node.Key.TIMES_COMPLETED))
                {
                    stopRepeatingAt += context.GetNodeValue<int>(
                        Child, Node.Key.TIMES_COMPLETED
                      );
                }

                context.SetNodeValue(
                    this,
                    STOP_REPEATING_AT,
                    stopRepeatingAt
                  );
            }
        }

        protected override void StartChildren(Context context)
        {
            Child.Start(context);
        }

        protected override void ResetSelf(Context context)
        {
            context.UnsetNodeValue(this, STOP_REPEATING_AT);
        }

        protected override void OnChildCompleted(Node node, State state, Context context)
        {
            base.OnChildCompleted(node, state, context);

            if (_shouldRepeatForever)
            {
                ResetAndRestartChild(context);
                return;
            }

            var timesCompleted = context.GetNodeValue<int>(node, Key.TIMES_COMPLETED);
            if (timesCompleted < _n)
            {
                ResetAndRestartChild(context);
                return;
            }

            OnCompleted(State.SUCCESS, context);
        }

        private void ResetAndRestartChild(Context context)
        {
            Child.Reset(context);
            Child.Start(context);
        }
    }
}