using UnityEngine;

namespace Ai
{
    // TODO: Implement actual pathfinding.
    public class Pathfind : LeafNode
    {
        private Argument.In<Transform> _transformArg;
        private Argument.In<Transform> _targetTransformArg;
        private Argument.Out<Vector2> _outputDirectionArg;

        public Pathfind(
            Argument.In<Transform> transform,
            Argument.In<Transform> targetTransform,
            Argument.Out<Vector2> outputDirection
          )
        {
            _transformArg = transform;
            _targetTransformArg = targetTransform;
            _outputDirectionArg = outputDirection;
        }

        public override void Process(float dt, Context context)
        {
            base.Process(dt, context);

            var transform = _transformArg.Get(context);
            var targetTransform = _targetTransformArg.Get(context);
            var fromTo = targetTransform.position - transform.position;
            var direction = fromTo.normalized;
            _outputDirectionArg.Set(direction, context);
        }
    }
}