using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;

        SetupMission();

        SetupDefenses();

        SetupTargetOverlay();        
    }

    void Update()
    {
        UpdateDefenses();

        UpdateTargetOverlay();
    }

    #region Missions

    public GameObject MissionObject;

    void SetupMission()
    {
    }

    void UpdateMission()
    {
        //Nothing?
    }


    #endregion

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
        if (_targetText != null)
        {
            _targetText.text = target.name;
        }

        Vector3 targetOverlayPosition = _camera.WorldToScreenPoint(target.transform.position);

        guiOverlayForTarget.transform.position = targetOverlayPosition;
    }

    private void SetupTargetOverlay()
    {
        _targetText = guiOverlayForTarget.GetComponentInChildren<Text>();

        if (_targetText != null)
        {
            _targetText.text = target.name;
        }
    }

    #endregion
}
