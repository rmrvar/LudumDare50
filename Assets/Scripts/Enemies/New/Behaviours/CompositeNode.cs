using UnityEngine;

namespace Ai
{
    public abstract class CompositeNode : Node
    {
        public CompositeNode(params Node[] children) : base()
        {
            Children = children;

            foreach (var child in Children)
            {
                child.Completed += (state, context) =>
                    OnChildCompleted(child, state, context);
            }
        }

        public Node[] Children { get; private set; }

        #region ---- Start ----
        public sealed override void Start(Context context)
        {
            base.Start(context);

            StartSelf(context);
            StartChildren(context);
        }

        protected virtual void StartSelf(Context context)
        {
        }

        protected abstract void StartChildren(Context context);
        #endregion

        #region ---- Stop ----
        public sealed override void Stop(Context context)
        {
            base.Stop(context);

            StopSelf(context);
            StopChildren(context);
        }

        protected virtual void StopSelf(Context context)
        {
        }

        protected virtual void StopChildren(Context context)
        {
            foreach (var child in Children)
            {
                child.Stop(context);
            }
        }
        #endregion

        #region ---- Reset ----
        public sealed override void Reset(Context context)
        {
            base.Reset(context);

            ResetSelf(context);
            ResetChildren(context);
        }

        protected virtual void ResetSelf(Context context)
        {
        }

        protected virtual void ResetChildren(Context context)
        {
            foreach (var child in Children)
            {
                child.Reset(context);
            }
        }
        #endregion

        protected virtual void OnChildCompleted(Node node, State state, Context context)
        {
            Debug.Assert(state != State.RUNNING, $"Completed Child {node} is still running!");

            Debug.Log($"Context {context.Id} node {_nodeId} has had child complete with state {state}.");
        }
    }
}
