namespace Ai
{
    public class Fallback : CompositeNode
    {
        public Fallback(params Node[] children) : base(children)
        {
        }

        public override void Start(Context context)
        {
            base.Start(context);

            SetNextChildIndex(context, 1);
            Children[0].Start(context);
        }

        public override void Reset(Context context)
        {
            context.UnsetNodeValue(this, Node.Key.NEXT_CHILD_INDEX);

            base.Reset(context);
        }

        protected override void OnChildCompleted(Node node, State state, Context context)
        {
            base.OnChildCompleted(node, state, context);

            if (state == State.SUCCESS)
            {
                OnCompleted(State.SUCCESS, context);
            }

            var childIndex = context.GetNodeValue<int>(this, Key.NEXT_CHILD_INDEX);
            if (childIndex >= Children.Length)
            {
                OnCompleted(State.FAILURE, context);
            }
            else
            {
                SetNextChildIndex(context, childIndex + 1);
                Children[childIndex].Start(context);
            }
        }
    }
}