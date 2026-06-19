using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Generator Settings")]
    public GameObject anchorPrefab;
    public Transform player;

    [Header("Spawn Boundaries")]
    public float spawnDistanceAhead = 20f;

    [Tooltip("Horizontal gap between anchors")]
    public float minXSpacing = 5f;
    public float maxXSpacing = 9f;

    [Tooltip("The lowest height an anchor can spawn (Keep this well above your ground Y)")]
    public float minAnchorY = 2f;

    [Tooltip("The highest height an anchor can spawn")]
    public float maxAnchorY = 6f;

    private float lastSpawnX;

    void Start()
    {
        // Start spawning anchors slightly ahead of the player's initial position
        lastSpawnX = player.position.x + 6f;

        // Pre-generate a safe starting runway of anchors
        for (int i = 0; i < 5; i++)
        {
            SpawnAnchor();
        }
    }

    void Update()
    {
        // Continuous generation tracking the player's horizontal progress
        if (player.position.x > lastSpawnX - spawnDistanceAhead)
        {
            SpawnAnchor();
        }
    }

    void SpawnAnchor()
    {
        // Calculate relative random spacing to ensure gaps are always jumpable
        float randomXGap = Random.Range(minXSpacing, maxXSpacing);
        float randomYHeight = Random.Range(minAnchorY, maxAnchorY);

        Vector3 spawnPosition = new Vector3(lastSpawnX + randomXGap, randomYHeight, 0f);

        // Instantiate the anchor as a child of the generator to keep the Hierarchy clean
        Instantiate(anchorPrefab, spawnPosition, Quaternion.identity, transform);

        // Advance the generation marker
        lastSpawnX += randomXGap;
    }
}