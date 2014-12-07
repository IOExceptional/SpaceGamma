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

    private int _currentShieldStrength = 0;

    /// <summary>
    /// Measured in milliseconds?
    /// </summary>
    public int ShieldRegenTime = 1000;

    public int CurrentShieldStrength
    {
        get
        {
            return _currentShieldStrength;
        }
    }

    private float shieldTimer;
    private bool shieldTimerActive;

    AudioClip _shieldSound;
    AudioClip _hullSound;

    AudioClip _takingDamageSound;
    AudioClip _shieldDownSound;
    AudioClip _hullBreachedSound;
    AudioClip _shieldsRecharged;
    AudioClip _enemyShieldsDown;


    void Start()
    {
        shieldTimer = 0;
        shieldTimerActive = false;

        _currentShieldStrength = ShieldStrength;

        LoadSounds();
    }

    void LoadSounds()
    {
        _shieldSound = Resources.Load<AudioClip>("Sounds/Sounds/shield-noise");
        _hullSound = Resources.Load<AudioClip>("Sounds/Sounds/hull-noise");
        _enemyShieldsDown = Resources.Load<AudioClip>("Sounds/Voices/enemy-shields-down");
        _enemyShieldsRecharged = Resources.Load<AudioClip>("Sounds/Voices/enemy-shields-recharged");
        if (IsPlayer)
        {
            _takingDamageSound = Resources.Load<AudioClip>("Sounds/Voices/taking-damage");
            _shieldDownSound = Resources.Load<AudioClip>("Sounds/Voices/shields-down");
            _hullBreachedSound = Resources.Load<AudioClip>("Sounds/Voices/hull-breached");
            _shieldsRecharged = Resources.Load<AudioClip>("Sounds/Voices/shields-recharged");
        }
    }

    void Update()
    {
        if (shieldTimerActive && shieldTimer >= 0)
        {
            shieldTimer -= Time.deltaTime;
        }

        if (shieldTimer <= 0 && shieldTimerActive)
        {
            shieldTimerActive = false;
            _currentShieldStrength = ShieldStrength;

            Debug.Log(string.Format("Shields recharged"));

            if (IsPlayer && _shieldsRecharged.isReadyToPlay)
            {
                AudioSource.PlayClipAtPoint(_shieldsRecharged, Camera.main.transform.position);
            }
            if(!IsPlayer && _enemyShieldsRecharged.isReadyToPlay)
            {
                AudioSource.PlayClipAtPoint(_enemyShieldsRecharged, Camera.main.transform.position);
            }

        }
    }

    internal void Hit(GunController controller, Vector3 hitPosition)
    {
        int laserStrength = controller.Strength;

        Debug.Log(string.Format("LS: {0}, SS: {1}, HS: {2}", laserStrength, _currentShieldStrength, HullStrength));

        HandleShield(hitPosition, ref laserStrength);

        HandleHull(hitPosition, ref laserStrength);

        DeathCheck();
    }

    private void HandleHull(Vector3 hitPosition, ref int laserStrength)
    {
        if (laserStrength > 0 && HullStrength > 0)
        {
            if (_hullSound.isReadyToPlay)
            {
                AudioSource.PlayClipAtPoint(_hullSound, hitPosition);
            }
            
            HullStrength -= laserStrength;
            
            if (HullStrength <= 100)
            {
                if (IsPlayer && _hullBreachedSound.isReadyToPlay)
                {
                    AudioSource.PlayClipAtPoint(_hullBreachedSound, Camera.main.transform.position);
                }
            }
        }
    }

    private void HandleShield(Vector3 hitPosition, ref int laserStrength)
    {
        if (!shieldTimerActive && _currentShieldStrength > 0)
        {
            int laserDiff = Mathf.Clamp(laserStrength - _currentShieldStrength, 0, int.MaxValue);
            
            if (_shieldSound.isReadyToPlay)
            {
                AudioSource.PlayClipAtPoint(_shieldSound, hitPosition);
            }

            if (_currentShieldStrength <= laserStrength)
            {
                if (IsPlayer && _shieldDownSound.isReadyToPlay)
                {
                    AudioSource.PlayClipAtPoint(_shieldDownSound, Camera.main.transform.position);
                }

                _currentShieldStrength = 0;
            }
            else
            {
                _currentShieldStrength -= laserStrength;
            }

            laserStrength = laserDiff;

            if(_currentShieldStrength == 0)
            {
                shieldTimerActive = true;
                shieldTimer = ShieldRegenTime;
                if (IsPlayer && _takingDamageSound.isReadyToPlay)
                {
                    AudioSource.PlayClipAtPoint(_takingDamageSound, Camera.main.transform.position);
                }
                if(!IsPlayer && _enemyShieldsDown.isReadyToPlay)
                {
                    AudioSource.PlayClipAtPoint(_enemyShieldsDown, Camera.main.transform.position);
                }
            }
        }
    }

    private void DeathCheck()
    {
        Debug.Log("Checking for death");
        if (HullStrength <= 0)
        {
            Debug.Log("Checking for death");
            Destruct();
        }
    }

    private void Destruct()
    {
        Destroy(transform.root.gameObject);
    }

    public AudioClip _enemyShieldsRecharged { get; set; }
}