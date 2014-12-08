using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public float SpawnTimer = 2f;
    public float InitialSpawnTimer = 0f;

    public GameObject objectToSpawn;

    public Transform[] spawnPoints;

    void Start()
    {
        InvokeRepeating("SpawnEntity", InitialSpawnTimer, SpawnTimer);
    }

    void Update()
    {
    }

    void SpawnEntity()
    {
        int spawnPointIndex = -1;
        Transform spawnPoint = null;

        do
        {
            spawnPointIndex = Random.Range(0, spawnPoints.Length);
            spawnPoint = spawnPoints[spawnPointIndex];

        }
        while (spawnPointIndex < 0 && spawnPoint == null);

        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);;

        //EnemyBehaviourController controller = obj.GetComponent<EnemyBehaviourController>();


        //for(int i = 0; i < _rand.Next(3, 20); i++)
        //{
        //    Vector3 position = new Vector3(_rand.Next(-2000, 2000), _rand.Next(-2000, 2000), _rand.Next(-2000, 2000));

        //    controller.AddWayPoint(position);
        //}
    }
}
