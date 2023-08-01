using UnityEngine;

// The singular purpose of this class is to clear the trail renderer before a projectile is respawned.
public class ClearTrailRenderer : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer = default;

    private void OnDisable()
    {
        _trailRenderer?.Clear();
    }
}
