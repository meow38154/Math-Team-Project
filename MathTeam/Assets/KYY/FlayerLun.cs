using UnityEngine;
using UnityEngine.InputSystem;

public class FlayerLun : MonoBehaviour
{
    [SerializeField] private StaminaUI staminaUI;

    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float currentStamina = 100f;

    [SerializeField] private float useSpeed = 30f;
    [SerializeField] private float recoverSpeed = 20f;

    private void Start()
    {
        staminaUI.SetGauge(currentStamina, maxStamina);
    }

    private void Update()
    {
        if (Keyboard.current.shiftKey.isPressed)
        {
            currentStamina -= useSpeed * Time.deltaTime;
        }
        else
        {
            currentStamina += recoverSpeed * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

        staminaUI.SetGauge(currentStamina, maxStamina);
    }
}