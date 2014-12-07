using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject TriggerObject;
    public float requestCooldown;

    private float _requestTimer = 2;
    private bool _requestSent = false;

    private float _levelLoadTimer = 2;
    private bool _requestAccepted = false;


    private AudioClip _dockingPermissionGrantedSound;
    private AudioClip _dockingPermissionRequestedSound;
    private AudioClip _autopilotDisengaged;

    // Use this for initialization
    void Start()
    {
        _dockingPermissionRequestedSound = Resources.Load<AudioClip>("Sounds/Voices/docking-permission-requested");
        _dockingPermissionGrantedSound = Resources.Load<AudioClip>("Sounds/Voices/docking-request-accepted");
        _autopilotDisengaged = Resources.Load<AudioClip>("Sounds/Voices/autopilot-disengaged");

        if(_autopilotDisengaged.isReadyToPlay)
        {
            AudioSource.PlayClipAtPoint(_autopilotDisengaged, Camera.main.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckRequestSent();

        CheckRequestAccepted();
    }

    private void CheckRequestAccepted()
    {
        if (_requestAccepted && _levelLoadTimer > 0)
        {
            _levelLoadTimer = Mathf.Clamp(_levelLoadTimer - Time.deltaTime, 0, requestCooldown);
        }

        if (_requestAccepted && _levelLoadTimer == 0)
        {
            Application.LoadLevel("menu-scene");
        }
    }

    private void CheckRequestSent()
    {
        if (_requestSent && _requestTimer > 0)
        {
            _requestTimer = Mathf.Clamp(_requestTimer - Time.deltaTime, 0, requestCooldown);
        }

        if (_requestSent && _requestTimer == 0)
        {
            if (_dockingPermissionRequestedSound.isReadyToPlay)
            {
                AudioSource.PlayClipAtPoint(_dockingPermissionGrantedSound, Camera.main.transform.position);
                _requestTimer = requestCooldown;
            }
            _requestSent = false;
            _requestAccepted = true;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject == TriggerObject)
        {
            if (!_requestSent)
            {
                if (_dockingPermissionRequestedSound.isReadyToPlay)
                {
                    AudioSource.PlayClipAtPoint(_dockingPermissionRequestedSound, Camera.main.transform.position);
                    _requestTimer = requestCooldown;
                    _requestSent = true;
                }
            }
        }
    }
}

