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

        public virtual void Process(float dt, Context context)
        {
            Debug.Log($"{_nodeId} in progress!");
        }

        protected override void OnCompleted(State state, Context context)
        {
            context.StopProcessingNode(this);

            base.OnCompleted(state, context);
        }
    }
}
