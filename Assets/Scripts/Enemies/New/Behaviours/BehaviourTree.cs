namespace Ai
{
    public class BehaviourTree
    {
        private Node _root;

        public BehaviourTree(Node root)
        {
            _root = root;
        }

        public void Process(float dt, Context context)
        {
            if (!context.HasStarted)
            {
                _root.Start(context);
                context.HasStarted = true;
            }
            else
            {
                foreach (var node in context.NodesToProcess())
                {
                    node.Process(dt, context);
                }
            }
        }
    }
}
