namespace Ai
{
    public abstract class LeafNode : Node
    {
        public override void Start(Context context)
        {
            base.Start(context);

            context.StartProcessingNode(this);
        }

        public abstract void Process(float dt, Context context);

        protected override void OnCompleted(State state, Context context)
        {
            context.StopProcessingNode(this);

            base.OnCompleted(state, context);
        }
    }
}
