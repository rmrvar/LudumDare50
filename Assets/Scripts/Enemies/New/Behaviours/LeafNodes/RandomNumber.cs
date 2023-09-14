using UnityEngine;

namespace Ai
{
    public class RandomNumber : LeafNode
    {
        private Argument.Out<float> _outputTo;
        private Argument.In<float> _minInclusive;
        private Argument.In<float> _maxInclusive;

        public RandomNumber(
            Argument.Out<float> outputTo,
            Argument.In<float> lowerBound,
            Argument.In<float> upperBound
          )
        {
            _outputTo = outputTo;
            _minInclusive = lowerBound;
            _maxInclusive = upperBound;
        }

        public override void Process(float dt, Context context)
        {
            base.Process(dt, context);

            _outputTo.Set(Random.Range(
                _minInclusive.Get(context),
                _maxInclusive.Get(context)
              ), context);
            OnCompleted(State.SUCCESS, context);
        }
    }
}