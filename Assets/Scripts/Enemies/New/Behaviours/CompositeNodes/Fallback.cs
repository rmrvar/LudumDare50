namespace Ai
{
    public class Fallback : CompositeNode
    {
        public Fallback(params Node[] children) : base(children)
        {
        }

        protected override void StartSelf(Context context)
        {
            context.SetNodeValue(this, Node.Key.NEXT_CHILD_INDEX, 1);
        }

        protected override void StartChildren(Context context)
        {
            Children[0].Start(context);
        }

        protected override void ResetSelf(Context context)
        {
            context.UnsetNodeValue(this, Node.Key.NEXT_CHILD_INDEX);
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
                context.SetNodeValue(
                    this,
                    Node.Key.NEXT_CHILD_INDEX,
                    childIndex + 1
                  );
                Children[childIndex].Start(context);
            }
        }
    }
}
