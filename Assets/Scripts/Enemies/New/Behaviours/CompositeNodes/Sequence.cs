namespace Ai
{
    public class Sequence : CompositeNode
    {
        public Sequence(params Node[] children) : base(children)
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

            if (state == State.FAILURE)
            {
                OnCompleted(State.FAILURE, context);
            }

            var childIndex = context.GetNodeValue<int>(this, Key.NEXT_CHILD_INDEX);
            if (childIndex >= Children.Length)
            {
                OnCompleted(State.SUCCESS, context);
            }
            else
            {
                SetNextChildIndex(context, childIndex + 1);
                Children[childIndex].Start(context);
            }
        }
    }
}