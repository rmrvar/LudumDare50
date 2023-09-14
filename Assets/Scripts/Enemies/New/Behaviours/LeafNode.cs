using UnityEngine;

namespace Ai
{
    public abstract class LeafNode : Node
    {
        public override void Start(Context context)
        {
            base.Start(context);

            context.StartProcessingNode(this);
        }

        public override void Abort(Context context)
        {
            base.Abort(context);

            context.StopProcessingNode(this);
        }

        // TODO: Add a Process function that is called every FixedUpdate.
        public virtual void Process(float dt, Context context)
        {
            Debug.Log($"Context {context.Id} node {_nodeId} in progress!");
        }

        protected override void OnCompleted(State state, Context context)
        {
            context.StopProcessingNode(this);

            base.OnCompleted(state, context);
        }
    }
}
