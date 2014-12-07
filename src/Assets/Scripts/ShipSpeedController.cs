using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ShipSpeedController : MonoBehaviour 
{
    public float CurrentSpeed
    {
        get
        {
            return speed;
        }
    }

    //speed stuff
    float speed;
    public int cruiseSpeed;
    float deltaSpeed;//(speed - cruisespeed)
    public int minSpeed;
    public int maxSpeed;
    float accel, decel;

    void Start()
    {
    }
 
    void FixedUpdate()
    {
        deltaSpeed = speed - cruiseSpeed;

        decel = speed - minSpeed;
        accel = maxSpeed - speed;

        if (Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey(KeyCode.W))
        {
            speed += accel * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.S))
        {
            speed -= decel * Time.fixedDeltaTime;
        }
        else if (Mathf.Abs(deltaSpeed) > .1f)
        {
            speed -= Mathf.Clamp(deltaSpeed * Mathf.Abs(deltaSpeed), -30, 100) * Time.fixedDeltaTime;
        }


        float sqrOffset = transform.GetChild(1).localPosition.sqrMagnitude;
        Vector3 offsetDir = transform.GetChild(1).localPosition.normalized;

        transform.Translate(-offsetDir * sqrOffset * 20 * Time.fixedDeltaTime);

        transform.Translate((offsetDir * sqrOffset * 50 + transform.GetChild(1).forward * speed) * Time.fixedDeltaTime, Space.World);
    }
}
