using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RoadGenerator : MonoBehaviour
{
    public static RoadGenerator instance { get; private set; }
    
    public GameObject[] PlatformPrefabs;
    private List<GameObject> _activePlatforms = new List<GameObject>();

    public float Speed = 10;
    public int maxPlatformCount = 5;
    private Vector3 _platformLength=new Vector3(0,0,21);
    private float _speed = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        ResetGenerator();
        StartLevel();
    }

    private void Update()
    {
        if (_speed == 0)
        {
            return;
        }

        foreach (GameObject platform in _activePlatforms)
        {
            platform.transform.position -= new Vector3(0, 0, _speed * Time.deltaTime);
        }

        if (_activePlatforms[0].transform.position.z < -21)
        {
            DeletePlatform();

            CreateNextPlatform();
        }
        
    }

    public void ResetGenerator()
    {
        _speed = 0;
        while (_activePlatforms.Count > 0)
        {
            DeletePlatform();
        }

        for (int i = 0; i < maxPlatformCount; i++)
        {
            CreateNextPlatform();
        }
    }
    
    public void StartLevel()
    {
        _speed = Speed;
    }

    private void DeletePlatform()
    {
        Destroy(_activePlatforms[0]);
        _activePlatforms.RemoveAt(0);
    }

    private void CreateNextPlatform()
    {
        Vector3 position = new Vector3(-0.45f,0,0);
        if (_activePlatforms.Count > 0)
        {
            position = _activePlatforms[_activePlatforms.Count - 1].transform.position + _platformLength;
        }
        GameObject gameObject=Instantiate(PlatformPrefabs[Random.Range(0, PlatformPrefabs.Length)], position, Quaternion.identity);
        _activePlatforms.Add(gameObject);
    }

    
   
    
    
    
    
}
