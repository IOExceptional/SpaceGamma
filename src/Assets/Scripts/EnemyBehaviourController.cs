using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class EnemyBehaviourController : MonoBehaviour
{
    public List<Vector3> waypoints;
    public int currentPoint = 0;

    public string state = "patrol";
    public float speed = 5;
    public float chaseDist = 100;
    public float shootDist = 50;

    public float shootingCooldown = 100;

    public GameObject[] gunControllerObjects;

    private List<GunController> gunControllers;

    private CharacterController controller;
    private Transform player;
    private float shootingCooldownTimer = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        controller = GetComponent<CharacterController>();

        gunControllers = new List<GunController>();
        waypoints = new List<Vector3>();
    
        foreach(GameObject obj in gunControllerObjects)
        {
            GunController gunCtrl = obj.GetComponent<GunController>();

            if(gunCtrl != null)
            {
                gunControllers.Add(gunCtrl);
            }
        }
    }

    void Update()
    {
        if(shootingCooldownTimer > 0)
        {
            shootingCooldownTimer = Mathf.Clamp(shootingCooldownTimer - Time.deltaTime, 0, shootingCooldown);
        }

        float playerDist = Vector3.Distance(player.position, transform.position); //vector 3.dist finds distances between things

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
            if (currentPoint < waypoints.Count)
            {
                Mover(waypoints[currentPoint]);
            }

        }
        else if (state == "chase")
        {
            Mover(player.position);
            Shooter(playerDist);
        }
    }

    private void Shooter(float playerDist)
    {
        bool didShoot = false;
        if(shootingCooldownTimer == 0)
        {
            foreach(GunController ctrl in gunControllers)
            {
                if(playerDist < shootDist)
                {
                    didShoot = true;
                    ctrl.HandleShoot();
                }
            }

            if(didShoot)
            {
                shootingCooldownTimer = shootingCooldown;
            }
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
        else if(state == "patrol")
        {
            if (waypoints.Any())
            {
                currentPoint = (currentPoint + 1) % waypoints.Count;
            }

        }
        controller.Move(movement * Time.deltaTime);

        transform.LookAt(target);
    }
}
