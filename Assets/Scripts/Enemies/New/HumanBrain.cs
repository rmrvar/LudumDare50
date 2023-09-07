using UnityEngine;

public class HumanBrain : Brain
{
    protected override void Update()
    {
        base.Update();

        var offsetDir = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
          ).normalized;
        Pawn.ForceMove(offsetDir);
    }
}
