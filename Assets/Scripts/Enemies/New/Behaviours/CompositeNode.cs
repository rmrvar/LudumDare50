using UnityEngine;

namespace Ai
{
    public abstract class CompositeNode : Node
    {
        public CompositeNode(params Node[] children)
        {
            Children = children;
        }

        public Node[] Children { get; private set; }

        public override void Start(Context context)
        {
            base.Start(context);

            // Listen to the children's events.
            if (context.GetNodeValue<int>(this, Node.Key.TIMES_STARTED) > 1)
            {
                return;
            }
            foreach (var child in Children)
            {
                child.Completed += (state, context) =>
                    OnChildCompleted(child, state, context);
            }
        }

        public override void Reset(Context context)
        {
            base.Reset(context);

            foreach (var child in Children)
            {
                child.Reset(context);
            }
        }

        protected virtual void SetNextChildIndex(Context context, int index)
        {
            context.SetNodeValue(this, Key.NEXT_CHILD_INDEX, index);
        }

        protected virtual int GetNextChildIndex(Context context, int index)
        {
            return context.GetNodeValue<int>(this, Key.NEXT_CHILD_INDEX);
        }

        protected virtual void OnChildCompleted(Node node, State state, Context context)
        {
            Debug.Assert(state != State.RUNNING, $"Completed Child {node} is still running!");
        }
    }
}
