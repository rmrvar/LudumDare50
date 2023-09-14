using UnityEngine;

namespace Ai
{
    public class MoveTo : LeafNode
    {
        private Argument.In<Transform> _transformArg;
        private Argument.In<Transform> _targetTransformArg;
        private Argument.In<Vector3> _targetPositionArg;
        private Argument.In<float> _stopDistanceArg;

        public MoveTo(
            Argument.In<Transform> transform,
            Argument.In<Transform> targetTransform,
            Argument.In<float> stopDistance = null
          )
        {
            _transformArg = transform;
            _targetTransformArg = targetTransform;
            _stopDistanceArg = stopDistance;
        }

        public MoveTo(
            Argument.In<Transform> transform,
            Argument.In<Vector3> targetPosition,
            Argument.In<float> stopDistance = null
          )
        {
            _transformArg = transform;
            _targetPositionArg = targetPosition;
            _stopDistanceArg = stopDistance;
        }

        public override void Process(float dt, Context context)
        {
            base.Process(dt, context);

            var transform = _transformArg.Get(context);
            var destination = (
                   _targetTransformArg?.Get(context)?.position
                ?? _targetPositionArg ?.Get(context)
              ).Value;
            var stopDistance = _stopDistanceArg?.Get(context) ?? 0.05F;

            var fromTo = destination - transform.position;
            var direction = fromTo.normalized;
            var distance = fromTo.magnitude;
            var offset = 2 * dt;

            var isCompleted = false;
            if (distance - offset < stopDistance)
            {
                offset = distance - stopDistance;
                isCompleted = true;
            }
            transform.position += direction * offset;

            if (isCompleted)
            {
                OnCompleted(State.SUCCESS, context);
            }
        }
    }
}