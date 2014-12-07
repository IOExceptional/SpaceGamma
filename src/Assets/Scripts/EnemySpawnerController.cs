using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public float SpawnTimer = 15f;

    public GameObject objectToSpawn;

    private float timer = 0;

    void Start()
    {

    }

    void Update()
    {
        if(timer > 0)
        {
            timer = Mathf.Clamp(timer - Time.deltaTime, 0, SpawnTimer);
        }
        else
        {
            SpawnEntity();
            timer = SpawnTimer;
        }
    }

    void SpawnEntity()
    {
        Instantiate(objectToSpawn, transform.position, new Quaternion());
    }
}
