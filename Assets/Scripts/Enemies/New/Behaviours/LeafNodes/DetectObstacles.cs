using UnityEngine;

namespace Ai
{
    public class DetectObstacles : LeafNode
    {
        private Argument.In<Transform> _transformArg;
        private Argument.In<LayerMask> _rayLayerMaskArg;
        private Argument.In<float> _rayDistanceArg;
        private Argument.InOut<float[]> _outputBufferArg;

        public DetectObstacles(
            Argument.In<Transform> transform,
            Argument.In<LayerMask> rayLayerMask,
            Argument.In<float> rayDistance,
            Argument.InOut<float[]> outputBuffer
          )
        {
            _transformArg = transform;
            _rayLayerMaskArg = rayLayerMask;
            _rayDistanceArg = rayDistance;
            _outputBufferArg = outputBuffer;
        }

        public override void Process(float dt, Context context)
        {
            base.Process(dt, context);

            var transform = _transformArg.Get(context);
            var outputBuffer = _outputBufferArg.Get(context);
            var rayDistance = _rayDistanceArg.Get(context);
            int rayLayerMask = _rayLayerMaskArg.Get(context).value;

            outputBuffer[0] = TestRayDirection(
                transform.position, new Vector3( 0, +1), rayDistance, rayLayerMask);
            outputBuffer[1] = TestRayDirection(
                transform.position, new Vector3(+1, +1), rayDistance, rayLayerMask);
            outputBuffer[2] = TestRayDirection(
                transform.position, new Vector3(+1,  0), rayDistance, rayLayerMask);
            outputBuffer[3] = TestRayDirection(
                transform.position, new Vector3(+1, -1), rayDistance, rayLayerMask);
            outputBuffer[4] = TestRayDirection(
                transform.position, new Vector3( 0, -1), rayDistance, rayLayerMask);
            outputBuffer[5] = TestRayDirection(
                transform.position, new Vector3(-1, -1), rayDistance, rayLayerMask);
            outputBuffer[6] = TestRayDirection(
                transform.position, new Vector3(-1,  0), rayDistance, rayLayerMask);
            outputBuffer[7] = TestRayDirection(
                transform.position, new Vector3(-1, +1), rayDistance, rayLayerMask);
        }

        private float TestRayDirection(
            Vector2 origin,
            Vector2 direction,
            float distance,
            LayerMask layerMask
          )
        {
            // TODO: This might possibly intersect with self.
            var hit = Physics2D.Raycast(
                origin,
                direction,
                distance,
                layerMask
              );
            if (hit.transform)
            {
                return Mathf.Lerp(
                    1,
                    0,
                    Vector2.Distance(origin, hit.point) / distance
                  );
            }
            else
            {
                return 0;
            }
        }
    }
}