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
        GameObject obj = Instantiate(objectToSpawn, transform.position, new Quaternion()) as GameObject;

        EnemyBehaviourController controller = obj.GetComponent<EnemyBehaviourController>();

        //TODO: Add some waypoints in :)    
    }
}
