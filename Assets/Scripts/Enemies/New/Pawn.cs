using UnityEngine;

public class Pawn : MonoBehaviour
{
    private Health _health;

    private Animator _animator;
    private Rigidbody2D _rb2d;
    private Collider2D[] _colliders;

    public bool CanControl { get; private set; }

    private float _facingDirection = +1;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
        _colliders = GetComponentsInChildren<Collider2D>();
    }

    private void Start()
    {
        _health.OnKilled += OnKilled;
        _health.OnRevived += OnRevived;
    }

    private void OnDestroy()
    {
        _health.OnKilled -= OnKilled;
        _health.OnRevived -= OnRevived;
    }

    public void Move(Vector2 offsetDir, float offsetDst, float combineStrength)
    {
        Move(offsetDir * offsetDst, combineStrength);
    }

    public void Move(Vector2 offset, float combineStrength)
    {

    }

    public void ForceMove(Vector2 offsetDir, float offsetDst)
    {

    }

    public void ForceMove(Vector2 offset)
    {

    }

    public void CombineMoves()
    {

    }

    private void OnKilled(GameObject source)
    {
        CanControl = false;
    }

    private void OnRevived()
    {
        CanControl = true;
    }
}
