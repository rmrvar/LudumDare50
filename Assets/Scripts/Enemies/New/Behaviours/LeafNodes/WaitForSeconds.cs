namespace Ai
{
    public class WaitForSeconds : LeafNode
    {
        private const string TIME_ELAPSED = "TimeElapsed";

        private Argument<float> _secondsToWaitFor;

        public WaitForSeconds(Argument<float> secondsToWaitFor)
        {
            _secondsToWaitFor = secondsToWaitFor;
        }

        public override void Start(Context context)
        {
            base.Start(context);

            context.SetNodeValue<float>(this, TIME_ELAPSED, 0);
        }

        public override void Reset(Context context)
        {
            base.Reset(context);

            context.UnsetNodeValue(this, TIME_ELAPSED);
        }

        public override void Process(float dt, Context context)
        {
            base.Process(dt, context);

            var timeElapsed = context.GetNodeValue<float>(this, TIME_ELAPSED);
            timeElapsed += dt;
            context.SetNodeValue(this, TIME_ELAPSED, timeElapsed);

            if (timeElapsed >= _secondsToWaitFor.Get(context))
            {
                OnCompleted(State.SUCCESS, context);
            }
        }
    }
}