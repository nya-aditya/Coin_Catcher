using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject bombPrefab;

    public float spawnInterval = 1.2f;
    public float bombChance = 0.25f; // 25% chance of spawning a bomb

    public float minX = -7f;
    public float maxX = 7f;
    public float spawnY = 5.5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 1f, spawnInterval);
    }

    void SpawnObject()
    {
        // Stop spawning when game is over
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
        {
            return;
        }

        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        GameObject prefabToSpawn = coinPrefab;
        if (Random.value < bombChance && bombPrefab != null)
        {
            prefabToSpawn = bombPrefab;
        }

        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
