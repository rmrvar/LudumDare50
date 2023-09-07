using System;

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

        public virtual void Start(Context context)
        {
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
            }
        }

        public virtual void Reset(Context context)
        {
            context.SetNodeValue(this, Key.STATE, State.NOT_STARTED);
        }

        public event Action<State, Context> Completed;
        protected virtual void OnCompleted(State state, Context context)
        {
            // State will be set each tick in the Process method for LeafNodes and by callback for Composite/DecoratorNodes.
            context.SetNodeValue(this, Key.STATE, state);

            var timesCompleted = context.GetNodeValue<int>(this, Key.TIMES_COMPLETED);
            context.SetNodeValue(this, Key.TIMES_COMPLETED, timesCompleted + 1);

            Completed?.Invoke(state, context);
        }
    }
}
