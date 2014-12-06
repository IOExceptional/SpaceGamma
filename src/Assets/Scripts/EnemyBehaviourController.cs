using UnityEngine;
using System.Collections;

public class EnemyBehaviourController : MonoBehaviour
{

    Transform[] waypoints;
    int currentPoint = 0;

    string state = "patrol";
    float speed = 10;
    float chaseDist = 15;
    float gravity = 15;

    private CharacterController controller;
    private Transform player;
    private ParticleEmitter alert;

    void Start()
    {
        alert = GameObject.Find("alert").particleEmitter;
        player = GameObject.FindWithTag("Player").transform;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        var playerDist = Vector3.Distance(player.position, transform.position); //vector 3.dist finds distances between things

        if (playerDist <= chaseDist)
        {
            state = "chase";
        }
        else
        {
            state = "patrol";
        }

        if (state == "patrol")
        {
            alert.emit = false;
            renderer.material.color = Color.green;

            if (currentPoint < waypoints.Length)
            {
                Mover(waypoints[currentPoint].position);
            }

        }
        else if (state == "chase")
        {
            alert.emit = true;
            renderer.material.color = Color.red;
            Mover(player.position);
        }
    }

    void Mover(Vector3 target)
    {
        Vector3 diffVector = target - transform.position;

        Vector3 movement = new Vector3();

        if (diffVector.magnitude > 1)
        {
            movement = (diffVector.normalized * speed);//without this it will run toward something and slow down when it gets close.    

        }
        else
        {
            currentPoint = (currentPoint + 1) % waypoints.Length;

        }
        controller.Move(movement * Time.deltaTime);

        transform.LookAt(target);

    }
}
