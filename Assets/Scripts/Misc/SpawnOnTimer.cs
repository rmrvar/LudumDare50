using System.Collections;
using UnityEngine;

public class SpawnOnTimer : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs = default;

    [SerializeField] private float _minSpawnTime = 1;
    [SerializeField] private float _maxSpawnTime = 5;
    [SerializeField] private float _minSpawnDistance = 10;
    [SerializeField] private float _maxSpawnDistance = 20;

    [SerializeField] private Vector2 _bounds = default;

    private Coroutine _spawningBehaviour;

    private void OnEnable()
    {
        _spawningBehaviour = StartCoroutine(SpawningBehaviour());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawningBehaviour);
    }

    private IEnumerator SpawningBehaviour()
    {
        var playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        while (true)
        {
            var spawnDistance = Mathf.Lerp(_minSpawnDistance, _maxSpawnDistance, Random.value);
            var spawnOffset = Random.insideUnitCircle.normalized * spawnDistance;

            var spawnPosition = playerTransform.position + (Vector3) spawnOffset;
            if (spawnPosition.x < transform.position.x - _bounds.x * 0.5F
            ||  spawnPosition.x > transform.position.x + _bounds.x * 0.5F
            ||  spawnPosition.y < transform.position.y - _bounds.y * 0.5F
            ||  spawnPosition.y > transform.position.y + _bounds.y * 0.5F)
            {
                Debug.Log("Skipped spawning because out of bounds!");
                continue;
            }

            var randomIndex = Random.Range(0, _enemyPrefabs.Length);
            var randomEnemy = _enemyPrefabs[randomIndex];

            Instantiate(randomEnemy, spawnPosition, Quaternion.identity);

            var cooldown = Mathf.Lerp(_minSpawnTime, _maxSpawnTime, Random.value);
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, _bounds);
    }
}
