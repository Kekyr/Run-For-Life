using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject[] PlatformPrefabs;
    [SerializeField] private Transform player;

    private List<GameObject> activePlatforms = new List<GameObject>();
    private float spawnPosition = 0;
    private float platformLength=21;
    private int startCountOfPlatforms = 6;
    
    private void Start()
    {
        for (var i = 0; i < startCountOfPlatforms; i++)
        {
            SpawnPlatform(Random.Range(0, PlatformPrefabs.Length));
        }
    }

    private void Update()
    {
        if ((player.position.z -platformLength)> (spawnPosition - (startCountOfPlatforms * platformLength)))
        {
            SpawnPlatform(Random.Range(0, PlatformPrefabs.Length));
            DeletePlatform();
        }
    }
    

    private void SpawnPlatform(int platformIndex)
    {
        GameObject nextPlatform=Instantiate(PlatformPrefabs[platformIndex], transform.forward * spawnPosition, transform.rotation);
        activePlatforms.Add(nextPlatform);
        spawnPosition += platformLength;
    }

    private void DeletePlatform()
    {
        Destroy(activePlatforms[0]);
        activePlatforms.RemoveAt(0);
    }
    
}
