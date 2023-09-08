using Ai;

public class SkeletalMageBrain : ComputerBrain
{
    private static BehaviourTree _skeletalMageBehaviourTree =
        new BehaviourTree(
            new Repeat(3,
                new Sequence(
                    //new RandomNumber(
                    //    "input1",
                    //    Argument<float>.FromValue(1),
                    //    Argument<float>.FromValue(10)
                    //  ),
                    //new RandomNumber(
                    //    "input2",
                    //    Argument<float>.FromValue(1),
                    //    Argument<float>.FromValue(10)
                    //  ),
                    new Add(
                        "output1",
                        new[] {
                            //Argument<float>.FromKey("input1"),
                            //Argument<float>.FromKey("input2")
                            Argument<float>.FromValue(1),
                            Argument<float>.FromValue(3),
                          }
                      ),
                    new Multiply(
                        "output2",
                        new[] {
                            Argument<float>.FromKey("output1"),
                            Argument<float>.FromValue(10)
                          }
                      ),
                    new RandomNumber(
                        "wait_for",
                        Argument<float>.FromValue(1),
                        Argument<float>.FromValue(3)
                      ),
                    new WaitForSeconds(Argument<float>.FromKey("wait_for"))
                  )
              )
          );

    protected override void Awake()
    {
        base.Awake();

        _behaviourTree = _skeletalMageBehaviourTree;
    }
}
