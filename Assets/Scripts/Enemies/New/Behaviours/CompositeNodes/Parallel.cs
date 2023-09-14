namespace Ai
{
    public class Parallel : CompositeNode
    {
        private const string _CHILDREN_COMPLETED = "ChildrenCompleted";

        public Parallel(params Node[] children) : base(children)
        {
        }

        protected override void StartSelf(Context context)
        {
            context.SetNodeValue(this, _CHILDREN_COMPLETED, 0);
        }

        protected override void StartChildren(Context context)
        {
            foreach (var child in Children)
            {
                child.Start(context);
            }
        }

        protected override void ResetSelf(Context context)
        {
            context.UnsetNodeValue(this, _CHILDREN_COMPLETED);
        }

        protected override void OnChildCompleted(Node node, State state, Context context)
        {
            base.OnChildCompleted(node, state, context);

            var amountCompleted = context.GetNodeValue<int>(
                this,
                _CHILDREN_COMPLETED
              );
            context.SetNodeValue(
                this,
                _CHILDREN_COMPLETED,
                ++amountCompleted
              );

            if (state != State.SUCCESS && amountCompleted >= Children.Length)
            {
                OnCompleted(State.FAILURE, context);
                return;
            }

            foreach (var child in Children)
            {
                if (child != node)
                {
                    child.Abort(context);
                }
            }
            OnCompleted(State.SUCCESS, context);
        }
    }
}