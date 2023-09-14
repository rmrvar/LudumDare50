using System.Linq;
using UnityEngine;

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
                    var state = context.GetNodeValue<Node.State>(
                        node,
                        Node.Key.STATE
                      );
                    if (state == Node.State.ABORTED)
                    {
                        // ABORTED occurs when the Parallel node completes and cancels any still running nodes.
                        // The node was removed in the original list, so ignore it.
                        continue;
                    }

                    Debug.Assert(
                        state == Node.State.RUNNING,
                        "Behaviour tree processed node in invalid state!"
                      );

                    node.Process(dt, context);
                }
            }
        }
    }
}
