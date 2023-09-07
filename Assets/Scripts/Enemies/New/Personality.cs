using UnityEngine;

public class Personality : ScriptableObject
{
    [field: SerializeField] public float FleeThreshold { get; private set; }
}
