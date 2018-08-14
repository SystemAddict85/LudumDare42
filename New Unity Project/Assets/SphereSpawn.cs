using System.Collections;
using UnityEngine;
using System.Linq;

public class SphereSpawn : Singleton<SphereSpawn> {

    public GameObject asteroidPrefab;
    public GameObject enemyPrefab;
    public GameObject greenPrefab;
    public GameObject bluePrefab;
    public GameObject redPrefab;

    private Transform[] spawnLocations;

    public static int SpawnCounter { get { return Instance.spawnCount; } set { Instance.spawnCount = value; } }

    [SerializeField]
    private int spawnCount;
    
    public int maxSpawn = 40;

    private bool readyToSpawn = true;
    [SerializeField]
    private float timeBetweenSpawns = 2.5f;

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        spawnLocations = GetComponentsInChildren<Transform>();
        spawnLocations = spawnLocations.Skip(1).ToArray();
        Debug.Log(spawnLocations.Length);
    }

    private void Update()
    {
        if(readyToSpawn && SpawnCounter < maxSpawn)
        {
            StartCoroutine(WaitToSpawn());
            SpawnRandomObject();
        }
    }

    IEnumerator WaitToSpawn()
    {
        readyToSpawn = false;
        yield return new WaitForSeconds(timeBetweenSpawns);
        readyToSpawn = true;
    }

    private void SpawnRandomObject()
    {
        int randomNum = Random.Range(1, 11);
        GameObject spawnObject;
        if(randomNum > 6)
        {
            spawnObject = asteroidPrefab;
            SpawnCounter++;
        } else if(randomNum > 1)
        {
            spawnObject = enemyPrefab;
            SpawnCounter += 6;
        } else
        {
            randomNum = Random.Range(1, 4);
            switch (randomNum)
            {
                case 1:
                    spawnObject = greenPrefab;
                    break;
                case 2:
                    spawnObject = bluePrefab;
                    break;
                case 3:
                default:
                    spawnObject = redPrefab;
                    break;
            }
        }
        if (spawnObject != null)
        {
            var spawnPos = GetSpawnPosition();

            var obj = Instantiate(spawnObject, spawnPos, transform.rotation);
            obj.transform.LookAt(PlayerManager.Player.transform);
            Debug.Log("spawned: " + obj);
        }
        
    }

    private Vector3 GetSpawnPosition()
    {
        int randomLocation = Random.Range(0, spawnLocations.Length);
        return spawnLocations[randomLocation].position;
    }

    
    
}
