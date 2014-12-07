using UnityEngine;
using System.Collections;
using System.Threading;

public class GunController : MonoBehaviour
{
    LineRenderer lineRenderer;
    
    public float LaserLength = 100;

    public int Strength = 50;

    public bool IsPlayer = false;

    public float shootCooldown = 0.25f;
    public float laserDisplayCooldown = 0.01f;

    private AudioClip _laserSound;
    private bool isShooting = false;
    private float shootTimer = 0;

    private bool laserDisplayActive = false;
    private float laserDisplayTimer = 0;

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
    void FixedUpdate()
    {
        CheckLaserDisplay();
        CheckIsShooting();

        if (IsPlayer)
        {
            if (Input.GetButton("Fire1"))
            {
                HandleShoot();
            }
        }
    }

    private void CheckLaserDisplay()
    {
        if(laserDisplayActive && laserDisplayTimer > 0)
        {
            laserDisplayTimer = Mathf.Clamp(laserDisplayTimer - Time.deltaTime, 0, laserDisplayCooldown);
        }

        if(laserDisplayActive && laserDisplayTimer <= 0)
        {
            laserDisplayActive = false;
            lineRenderer.enabled = false;
        }
    }

    private void CheckIsShooting()
    {
        if (isShooting && shootTimer > 0)
        {
            shootTimer = Mathf.Clamp(shootTimer - Time.deltaTime, 0, shootCooldown);
        }

        if (isShooting && shootTimer <= 0)
        {
            
            isShooting = false;
        }
    }

    public void HandleShoot()
    {
        if (!isShooting)
        {
            shootTimer = shootCooldown;
            isShooting = true;

            laserDisplayTimer = laserDisplayCooldown;
            laserDisplayActive = true;

            lineRenderer.enabled = true;

            FireLaser();
        }
    }

    public void FireLaser()
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

        if (_laserSound.isReadyToPlay)
        {
            AudioSource.PlayClipAtPoint(_laserSound, ray.origin);
        }
    }
}

