using System.Linq;

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
                var nodesToProcess = context.NodesToProcess().ToList();
                foreach (var node in nodesToProcess)
                {
                    node.Process(dt, context);
                }
            }
        }
    }
}
