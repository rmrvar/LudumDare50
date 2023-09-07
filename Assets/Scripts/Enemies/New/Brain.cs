using UnityEngine;

public class Brain : MonoBehaviour
{
    public Pawn Pawn { get; private set; }

    protected virtual void Awake()
    {
        Pawn = GetComponent<Pawn>();
    }

    protected virtual void Update()
    {
        // TODO: This does not return early in the derived functions.
        if (!Pawn.CanControl)
        {
            return;
        }
    }

    protected virtual void LateUpdate()
    {
        
    }
}
