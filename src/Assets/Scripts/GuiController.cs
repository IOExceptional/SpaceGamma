using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    public DamageController damage;
    public GameObject shieldMeter;
    public GameObject hullMeter;

    private Text _shieldText;
    private Text _hullText;

    void Start()
    {
        _shieldText = shieldMeter.GetComponent<Text>();
        _hullText = hullMeter.GetComponent<Text>();
    }

    void Update()
    {
        _shieldText.text = damage.CurrentShieldStrength.ToString();
        _hullText.text = damage.HullStrength.ToString();
    }
}
