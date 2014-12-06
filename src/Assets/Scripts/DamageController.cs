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

    private int _shieldStrength = 0;

    /// <summary>
    /// Measured in milliseconds?
    /// </summary>
    public int ShieldRegenTime = 1000;

    private float shieldTimer;
    private bool shieldTimerActive;

    void Start()
    {
        shieldTimer = 0;
        shieldTimerActive = false;

        _shieldStrength = ShieldStrength;
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

    internal void Hit(GunController controller)
    {
        int laserStrength = controller.Strength;

        if (_shieldStrength > 0)
        {
            //Shield zap noise
            //If this is the player, play sound "taking-damage.wav"

            if (_shieldStrength <= laserStrength)
            {
                //If this is the player, play sound "shields-down.wav"
                _shieldStrength = 0;
            }
            else
            {
                _shieldStrength -= laserStrength;
            }

            if(!shieldTimerActive)
            {
                shieldTimer = ShieldRegenTime;
            }

            laserStrength -= _shieldStrength;
        }

        if (laserStrength > 0 && HullStrength > 0)
        {
            //Hull hit noise
            laserStrength -= HullStrength;

            if (HullStrength <= laserStrength)
            {
                //DEAD MAYN
                HullStrength = 0;
            }
            else if (HullStrength <= 100)
            {
                //If this is the player, play sound "hull-breached.wav"
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