using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;

        SetupDefenses();

        SetupTargetOverlay();

        SetupSpeedo();
    }

    void Update()
    {
        UpdateDefenses();

        UpdateTargetOverlay();

        UpdateSpeedo();
    }

    #region Defenses

    public DamageController damage;
    public GameObject shieldMeter;
    public GameObject hullMeter;

    private Text _shieldText;
    private Text _hullText;

    private void SetupDefenses()
    {
        _shieldText = shieldMeter.GetComponent<Text>();
        _hullText = hullMeter.GetComponent<Text>();
    }

    private void UpdateDefenses()
    {
        _shieldText.text = damage.CurrentShieldStrength.ToString();
        _hullText.text = damage.HullStrength.ToString();
    }

    #endregion

    #region Target overlay

    public GameObject target;

    public GameObject guiOverlayForTarget;

    private Text _targetText;

    private void UpdateTargetOverlay()
    {
        if(target == null)
        {
            return;
        }

        if (_targetText != null)
        {
            _targetText.text = target.name;
        }

        Vector3 targetOverlayPosition = _camera.WorldToScreenPoint(target.transform.position);

        //Huh...
        targetOverlayPosition.z = 0;

        guiOverlayForTarget.transform.position = targetOverlayPosition;
    }

    private void SetupTargetOverlay()
    {
        if (target == null)
        {
            return;
        }

        _targetText = guiOverlayForTarget.GetComponentInChildren<Text>();

        if (_targetText != null)
        {
            _targetText.text = target.name;
        }
    }

    #endregion

    #region Speedometer

    public int speedoMultiplier;

    public GameObject shipToMeasure;
    public GameObject speedValueTextObject;

    private Text _speedValueText;
    private ShipSpeedController _controller;

    void SetupSpeedo()
    {
        _controller = shipToMeasure.GetComponent<ShipSpeedController>();
        if(_controller == null)
        {
            return;
        }
        _speedValueText = speedValueTextObject.GetComponent<Text>();
        _speedValueText.text = GetSpeed();
    }

    void UpdateSpeedo()
    {
        if (_controller == null)
        {
            return;
        }

        _speedValueText.text = GetSpeed();
    }

    string GetSpeed()
    {
        int speed = ((int)_controller.CurrentSpeed) * speedoMultiplier;

        return speed.ToString();
    }

    #endregion
}
