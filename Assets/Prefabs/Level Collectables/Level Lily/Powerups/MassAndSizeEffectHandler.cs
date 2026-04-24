using UnityEngine;

public class MassAndSizeEffectHandler : MonoBehaviour
{
    [Header("Power-Up Multipliers")]
    public float massMultiplier = 3.0f;   // Triple the weight
    public float sizeMultiplier = 4.0f;   // Double the size
    // Note: Duration is handled by the PowerUpManager/Handler

    private Vector3 originalScale;
    private float originalMass;
    private Rigidbody rb;

    void Awake()
    {
        // Get the Rigidbody on the player
        rb = GetComponentInParent<Rigidbody>();
        
        // Save exactly what the player looks like at the start
        // Using transform.parent because this handler is usually a child of the player
        originalScale = transform.parent.localScale;
        
        if (rb != null)
        {
            originalMass = rb.mass;
        }
    }

    void OnEnable()
    {
        // Subscribe to your global power-up events
        PlayerPowerupHandler.OnPowerUpApplied += ApplyEffect;
        PlayerPowerupHandler.OnPowerUpExpired += RemoveEffect;
    }

    void OnDisable()
    {
        // Unsubscribe to prevent errors
        PlayerPowerupHandler.OnPowerUpApplied -= ApplyEffect;
        PlayerPowerupHandler.OnPowerUpExpired -= RemoveEffect;
    }

    private void ApplyEffect(PowerUpData data) 
    {
        // Only trigger if the power-up name matches
        if (data.powerUpName.Equals("MassIncrease"))
        {
            // 1. Double the size (or whatever multiplier you set)
            transform.parent.localScale = originalScale * sizeMultiplier;

            // 2. Increase the mass
            if (rb != null)
            {
                rb.mass = originalMass * massMultiplier;
            }

            Debug.Log("Combined Effect: Player is now Big and Heavy!");
        }
    }

    private void RemoveEffect(PowerUpData data) 
    {
        if (data.powerUpName.Equals("MassIncrease"))
        {
            // Reset to the exact original values saved in Awake
            transform.parent.localScale = originalScale;

            if (rb != null)
            {
                rb.mass = originalMass;
            }

            Debug.Log("Combined Effect: Player returned to normal.");
        }
    }
}