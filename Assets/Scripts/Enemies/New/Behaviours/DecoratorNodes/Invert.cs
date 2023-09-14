namespace Ai
{
    public class Invert : DecoratorNode
    {
        public Invert(Node child) : base(child)
        {
        }

        protected override void StartChildren(Context context)
        {
            Child.Start(context);
        }

        protected override void OnChildCompleted(Node node, State state, Context context)
        {
            base.OnChildCompleted(node, state, context);

            var newState = state == State.SUCCESS
                ? State.FAILURE
                : State.SUCCESS;
            OnCompleted(newState, context);
        }
    }
}