using System;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public float SpawnTimer = 15f;

    public GameObject objectToSpawn;

    private float timer = 0;

    private System.Random _rand;

    void Start()
    {
        _rand = new System.Random();
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


        for(int i = 0; i < _rand.Next(3, 20); i++)
        {
            Vector3 position = new Vector3(_rand.Next(-2000, 2000), _rand.Next(-2000, 2000), _rand.Next(-2000, 2000));

            controller.AddWayPoint(position);
        }
    }
}
