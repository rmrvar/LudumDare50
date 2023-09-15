using Ai;
using UnityEngine;

public class SkeletalMageBrain : ComputerBrain
{
    [SerializeField] private LayerMask _collisionLayerMask = default;

    //private static BehaviourTree _skeletalMageBehaviourTree =
    //    new BehaviourTree(
    //        new Repeat(3,
    //            new Sequence(
    //                //new RandomNumber(
    //                //    "input1",
    //                //    Argument<float>.FromValue(1),
    //                //    Argument<float>.FromValue(10)
    //                //  ),
    //                //new RandomNumber(
    //                //    "input2",
    //                //    Argument<float>.FromValue(1),
    //                //    Argument<float>.FromValue(10)
    //                //  ),
    //                new Add(
    //                    "output1",
    //                    new[] {
    //                        //Argument<float>.FromKey("input1"),
    //                        //Argument<float>.FromKey("input2")
    //                        Argument<float>.FromValue(1),
    //                        Argument<float>.FromValue(3),
    //                      }
    //                  ),
    //                new Multiply(
    //                    "output2",
    //                    new[] {
    //                        Argument<float>.FromKey("output1"),
    //                        Argument<float>.FromValue(10)
    //                      }
    //                  ),
    //                //new PrintArgumentValue<float>(Argument<float>.FromKey("input1")),
    //                //new PrintArgumentValue<float>(Argument<float>.FromKey("input2")),
    //                new PrintArgumentValue<float>(Argument<float>.FromKey("output2")),
    //                new RandomNumber(
    //                    "wait_for",
    //                    Argument<float>.FromValue(1),
    //                    Argument<float>.FromValue(3)
    //                  ),
    //                new WaitForSeconds(Argument<float>.FromKey("wait_for"))
    //              )
    //          )
    //      );

    //private static BehaviourTree _skeletalMageBehaviourTree =
    //    new BehaviourTree(
    //        new Repeat(3,
    //            new Sequence(
    //                new Parallel(
    //                    new WaitForSeconds(Argument<float>.FromValue(1.5F)),
    //                    new WaitForSeconds(Argument<float>.FromValue(1))
    //                  ),
    //                new PrintArgumentValue<string>(Argument<string>.FromValue("Done with parallel stuff!!!"))
    //            )
    //          )
    //      );


    private static ArgumentFromKey<Transform> _transformArg
        = Argument.FromKey<Transform>("Transform");
    private static ArgumentFromKey<LayerMask> _feelerLayerMaskArg
        = Argument.FromKey<LayerMask>("FeelerLayerMask");
    private static ArgumentFromKey<float> _feelerDistanceArg
        = Argument.FromKey<float>("FeelerDistance");
    private static ArgumentFromKey<float[]> _feelerOutputBufferArg
        = Argument.FromKey<float[]>("FeelerOutputBuffer");
    private static ArgumentFromKey<Transform> _targetTransformArg
        = Argument.FromKey<Transform>("TargetTransform");
    private static ArgumentFromKey<Vector2> _desiredDirectionArg
        = Argument.FromKey<Vector2>("DesiredDirection");

    private static BehaviourTree _skeletalMageBehaviourTree =
        new BehaviourTree(
            new Parallel(
                new DetectObstacles(
                    _transformArg,
                    _feelerLayerMaskArg,
                    _feelerDistanceArg,
                    _feelerOutputBufferArg
                  ),
                new Pathfind(
                    _transformArg,
                    _targetTransformArg,
                    _desiredDirectionArg
                  ),
                //new MoveTo(
                //    _transformArg,
                //    _targetTransformArg
                //  )
                new Move(
                    _transformArg,
                    _desiredDirectionArg,
                    _feelerOutputBufferArg
                  )
              )
          );

    protected override void Awake()
    {
        base.Awake();

        _behaviourTree = _skeletalMageBehaviourTree;
        _transformArg.Set(transform, _context);
        _targetTransformArg.Set(
            GameObject.FindGameObjectWithTag("Player").transform,
            _context
          );
        _feelerLayerMaskArg.Set(_collisionLayerMask, _context);
        _feelerDistanceArg.Set(1, _context);
        _feelerOutputBufferArg.Set(new float[8], _context);
    }

    private void OnDrawGizmosSelected()
    {
        if (_context == null)
        {
            return;
        }

        var directions = DetectObstacles.Directions;
        var feelerOutputBuffer = _feelerOutputBufferArg.Get(_context);

        Debug.DrawRay(transform.position, directions[0] * -feelerOutputBuffer[0], Color.red);
        Debug.DrawRay(transform.position, directions[1] * -feelerOutputBuffer[1], Color.red);
        Debug.DrawRay(transform.position, directions[2] * -feelerOutputBuffer[2], Color.red);
        Debug.DrawRay(transform.position, directions[3] * -feelerOutputBuffer[3], Color.red);
        Debug.DrawRay(transform.position, directions[4] * -feelerOutputBuffer[4], Color.red);
        Debug.DrawRay(transform.position, directions[5] * -feelerOutputBuffer[5], Color.red);
        Debug.DrawRay(transform.position, directions[6] * -feelerOutputBuffer[6], Color.red);
        Debug.DrawRay(transform.position, directions[7] * -feelerOutputBuffer[7], Color.red);

        var desiredDirection = _desiredDirectionArg.Get(_context);

        Debug.DrawRay(transform.position, desiredDirection, Color.blue);
    }
}
