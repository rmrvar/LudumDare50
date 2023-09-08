using UnityEngine;

namespace Ai
{
    public class RandomNumber : LeafNode
    {
        private string _outputTo;
        private Argument<float> _minInclusive;
        private Argument<float> _maxInclusive;

        public RandomNumber(
            string outputTo,
            Argument<float> lowerBound,
            Argument<float> upperBound
          )
        {
            _outputTo = outputTo;
            _minInclusive = lowerBound;
            _maxInclusive = upperBound;
        }

        public override void Process(float dt, Context context)
        {
            base.Process(dt, context);

            context.Set(_outputTo, Random.Range(
                _minInclusive.Get(context),
                _maxInclusive.Get(context)
              ));
            OnCompleted(State.SUCCESS, context);
        }
    }
}