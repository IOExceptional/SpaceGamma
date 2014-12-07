using UnityEngine;

public class DamageController : MonoBehaviour
{
    /// <summary>
    /// Health points of the hull
    /// </summary>
    public int HullStrength = 1000;

    /// <summary>
    /// Actual health points of the shield
    /// </summary>
    public int ShieldStrength = 1000;

    public bool IsPlayer = false;

    private int _shieldStrength = 0;

    /// <summary>
    /// Measured in milliseconds?
    /// </summary>
    public int ShieldRegenTime = 1000;

    private float shieldTimer;
    private bool shieldTimerActive;

    AudioClip _shieldSound;
    AudioClip _hullSound;

    AudioClip _takingDamageSound;
    AudioClip _shieldDownSound;
    AudioClip _hullBreachedSound;


    void Start()
    {
        shieldTimer = 0;
        shieldTimerActive = false;

        _shieldStrength = ShieldStrength;

        LoadSounds();
    }

    void LoadSounds()
    {
        _shieldSound = Resources.Load<AudioClip>("Sounds/Sounds/shield-noise");
        _hullSound = Resources.Load<AudioClip>("Sounds/Sounds/hull-noise");
        _takingDamageSound = Resources.Load<AudioClip>("Sounds/Voices/taking-damage");
        _shieldDownSound = Resources.Load<AudioClip>("Sounds/Voices/shields-down");
        _hullBreachedSound = Resources.Load<AudioClip>("Sounds/Voices/hull-breached");
    }

    void Update()
    {
        if(shieldTimerActive && shieldTimer >= 0)
        {
            shieldTimer -= Time.deltaTime;
        }

        if(shieldTimer <= 0)
        {
            shieldTimerActive = false;
            _shieldStrength = ShieldStrength;
        }
    }

    internal void Hit(GunController controller, Vector3 hitPosition)
    {
        int laserStrength = controller.Strength;

        if (_shieldStrength > 0)
        {
            AudioSource.PlayClipAtPoint(_shieldSound, Camera.main.transform.position);
            if (_shieldStrength <= laserStrength)
            {
                if (IsPlayer)
                {
                    AudioSource.PlayClipAtPoint(_shieldDownSound, Camera.main.transform.position);
                }
                _shieldStrength = 0;
            }
            else
            {
                _shieldStrength -= laserStrength;
            }

            if(!shieldTimerActive)
            {
                shieldTimer = ShieldRegenTime;
                shieldTimerActive = true;
                if (IsPlayer)
                {
                    AudioSource.PlayClipAtPoint(_takingDamageSound, Camera.main.transform.position);
                }
            }

            laserStrength -= _shieldStrength;
        }

        if (laserStrength > 0 && HullStrength > 0)
        {
            AudioSource.PlayClipAtPoint(_hullSound, Camera.main.transform.position);
            laserStrength -= HullStrength;

            if (HullStrength <= laserStrength)
            {
                //DEAD MAYN
                HullStrength = 0;
            }
            else if (HullStrength <= 100)
            {
                if (IsPlayer)
                {
                    AudioSource.PlayClipAtPoint(_hullBreachedSound, Camera.main.transform.position);
                }
            }
        }

        DeathCheck();

    }

    private void DeathCheck()
    {
        if (HullStrength <= 0)
        {
            Destruct();
        }
    }

    private void Destruct()
    {
        Destroy(transform.root.gameObject);
    }
}