using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    LineRenderer lineRenderer;
    
    public float LaserLength = 100;

    public int Strength = 50;

    private AudioClip _laserSound;

    // Use this for initialization
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        lineRenderer.enabled = false;

        LoadSounds();
    }

    void LoadSounds()
    {
        _laserSound = Resources.Load<AudioClip>("Sounds/Sounds/laser-gun");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            StopCoroutine("PlayerFireLaser");
            StartCoroutine("PlayerFireLaser");
        }
    }

    IEnumerator PlayerFireLaser()
    {
        lineRenderer.enabled = true;

        while(Input.GetButton("Fire1"))
        {
            FireLaser();

            yield return null;
        }

        lineRenderer.enabled = false;
    }

    void FireLaser()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        Vector3 endPoint = ray.GetPoint(LaserLength);

        if (Physics.Raycast(ray, out hit, 100) && !hit.collider.collider.isTrigger)
        {
            endPoint = hit.point;
        }

        if(hit.collider != null)
        {
            DamageController controller = hit.collider.GetComponentInParent<DamageController>();

            if (controller != null)
            {
                controller.Hit(this, hit.collider.transform.position);
            }
        }

        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, endPoint);

        AudioSource.PlayClipAtPoint(_laserSound, ray.origin);
    }
}

