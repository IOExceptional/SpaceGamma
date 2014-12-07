using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class ShipSpeedController : MonoBehaviour 
{
	public float Mass = 5.0f;

	public float CurrentThrust = 0.0f;

	public float ReverseThrustSetting = -7.5f;

	public float ThrustSetting = 5.0f;

	public float MaxThrust = 20;

	public float SoftSpeedLimit = 300.0f;

	public float HardSpeedLimit = 400.0f;
    public float MouseSensitivity = 1.0f;
	 
    void Start()
    {
        rigidbody.mass = Mass;
    }
 
    void FixedUpdate()
    {
		if(Input.GetKey(KeyCode.W))
		{
			CurrentThrust = ThrustSetting;
		}
		else if(Input.GetKey(KeyCode.S))
		{
			CurrentThrust = ReverseThrustSetting;
		}
		else
		{
			CurrentThrust = 0;
		}

		SpeedLimit ();
    }

	void SpeedLimit ()
	{
		float magnitude = rigidbody.velocity.magnitude;

		float force = 0;

		if (magnitude < SoftSpeedLimit) {
			force = CurrentThrust;
		}
		else {
			force = -CurrentThrust;
		}
		if (magnitude >= HardSpeedLimit) {
			force = -CurrentThrust * 2;
		}
		//Positive Z pushes ship backwards, so we flip the force
		rigidbody.AddRelativeForce (new Vector3 (0, 0, -force), ForceMode.Acceleration);
	}

	float AxisToFloat (string axisInput)
	{
		float output = 0;
		if (Input.GetAxis (axisInput) < 0) 
		{
			output -= 2;
		}
		if (Input.GetAxis (axisInput) > 0) 
		{
			output += 2;
		}

		output = Mathf.Clamp(output, -10, 10);

		return output;
	}
}
