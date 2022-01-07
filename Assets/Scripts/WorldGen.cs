using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGen : MonoBehaviour
{
    public float spawnPlatformDistance = 200f;
    public float maxJumpDistance = 3f;
    public GameObject player;
    public GameObject[] platforms;

    private Vector3 previousPlatformPosition;

    private void Awake()
    {
        createPlatform(0, new Vector3(10, 0, 0));
        createPlatform(0, new Vector3(3, 0, 0));
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, previousPlatformPosition) < spawnPlatformDistance)
        {
            Vector3 newPosition = new Vector3(Random.Range(0, maxJumpDistance), 0, 0);
            createPlatform(Random.Range(0, platforms.Length), newPosition);
        }
    }

    private void createPlatform(int platformIndex, Vector3 position)
    {
        GameObject platform = platforms[platformIndex];
        Vector3 size = platform.GetComponentInChildren<Tilemap>().size;
        GameObject spawnedPlatform = Instantiate(platform, previousPlatformPosition + position, Quaternion.identity);
        previousPlatformPosition = spawnedPlatform.transform.position + size;
        previousPlatformPosition.y -= spawnedPlatform.GetComponentInChildren<Tilemap>().size.y;
    }
}
