using Ai;

public class SkeletalMageBrain : ComputerBrain
{
    private static BehaviourTree _skeletalMageBehaviourTree =
        new BehaviourTree(
            new Repeat(3,
                new Sequence(
                    new Add(
                        "output1",
                        new[] {
                            "input1",
                            "input2"
                          }
                      ),
                    new Multiply(
                        "output2",
                        new[] {
                            "output1",
                            "input3"
                          }
                      )
                  )
              )
          );

    protected override void Awake()
    {
        base.Awake();

        _behaviourTree = _skeletalMageBehaviourTree;
    }
}
