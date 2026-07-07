using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private bool invert;

    private void Reset()
    {
        slider = GetComponent<Slider>();
    }

    public void SetGauge(float current, float max)
    {
        if (max <= 0f)
        {
            SetGaugeee(0f);
            return;
        }

        SetGaugeee(current / max);
    }

    public void SetGaugeee(float value)
    {
        value = Mathf.Clamp01(value);

        if (invert)
            value = 1f - value;

        if (slider != null)
            slider.value = value;
    }
}
