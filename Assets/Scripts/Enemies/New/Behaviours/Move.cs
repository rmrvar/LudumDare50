using UnityEngine;

namespace Ai
{
    public class Move : LeafNode
    {
        private Argument.In<Transform> _transformArg;
        private Argument.In<Vector2> _desiredDirectionArg;
        private Argument.In<float[]> _obstacleStrengthBufferArg;

        public Move(
            Argument.In<Transform> transform,
            Argument.In<Vector2> desiredDirection,
            Argument.In<float[]> obstacleStrengthBuffer = null
          )
        {
            _transformArg = transform;
            _obstacleStrengthBufferArg = obstacleStrengthBuffer;
            _desiredDirectionArg = desiredDirection;
        }

        public override void Process(float dt, Context context)
        {
            base.Process(dt, context);

            var strengths = _obstacleStrengthBufferArg.Get(context);

            var avgAvoidanceVector = Vector2.zero;
            for (int i = 0; i < DetectObstacles.Directions.Length; ++i)
            {
                var direction = DetectObstacles.Directions[i];
                avgAvoidanceVector += direction * -strengths[i];
            }
            avgAvoidanceVector = Vector2.ClampMagnitude(avgAvoidanceVector, 1);
            //avgAvoidanceVector.Normalize();

            var desiredHeading = _desiredDirectionArg.Get(context);
            var newHeading = avgAvoidanceVector * 0.5F + desiredHeading * 0.5F;

            var transform = _transformArg.Get(context);
            transform.position += (Vector3) newHeading * dt * 2;
        }
    }
}