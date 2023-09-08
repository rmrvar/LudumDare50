using System;
using UnityEngine;

namespace Ai
{
    public abstract class Node
    {
        public static class Key
        {
            public static string STATE = "State";
            public static string TIMES_STARTED = "TimesStarted";
            public static string TIMES_COMPLETED = "TimesCompleted";
            public static string NEXT_CHILD_INDEX = "NextChildIndex";
        }

        public enum State
        {
            NOT_STARTED,
            SUCCESS,
            RUNNING,
            FAILURE
        }

        private static int _nodeCount = 0;

        protected int _nodeId;

        public Node()
        {
            ++_nodeCount;
            _nodeId = _nodeCount;
        }

        public virtual void Start(Context context)
        {
            Debug.Log($"{_nodeId} started!");

            context.SetNodeValue(this, Key.STATE, State.RUNNING);

            if (context.HasNodeValue(this, Key.TIMES_STARTED))
            {
                var timesStarted = context.GetNodeValue<int>(
                    this,
                    Key.TIMES_STARTED
                  );
                context.SetNodeValue(
                    this,
                    Key.TIMES_STARTED,
                    timesStarted + 1
                  );
            }
            else
            {
                context.SetNodeValue(
                    this,
                    Key.TIMES_STARTED,
                    1
                  );
                context.SetNodeValue(
                    this,
                    Key.TIMES_COMPLETED,
                    0
                  );
            }
        }

        public virtual void Reset(Context context)
        {
            Debug.Log($"{_nodeId} reset!");

            context.SetNodeValue(this, Key.STATE, State.NOT_STARTED);
        }

        public event Action<State, Context> Completed;
        protected virtual void OnCompleted(State state, Context context)
        {
            Debug.Log($"{_nodeId} completed with state {state}!");

            // State will be set each tick in the Process method for LeafNodes and by callback for Composite/DecoratorNodes.
            context.SetNodeValue(this, Key.STATE, state);

            var timesCompleted = context.GetNodeValue<int>(
                this,
                Key.TIMES_COMPLETED
              );
            context.SetNodeValue(this, Key.TIMES_COMPLETED, timesCompleted + 1);

            Completed?.Invoke(state, context);
        }
    }
}
