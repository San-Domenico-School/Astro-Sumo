using UnityEngine;

public class PulseRadiusEffectHandler : MonoBehaviour
{
    public float originalRadius;
    private Rigidbody rb;
    private float currentRadius;

    // Fixed: Awake now closes its brackets properly
    void Awake()
    {
        originalRadius = transform.localScale.x;
        currentRadius = originalRadius;
        rb = GetComponent<Rigidbody>();
    }

    // Called when this object becomes enabled and active
    void OnEnable()
    {
        PlayerPowerupHandler.OnPowerUpApplied += ApplyEffect;
        PlayerPowerupHandler.OnPowerUpExpired += RemoveEffect;
    }

    // Called when this object is disabled or destroyed
    void OnDisable()
    {
        PlayerPowerupHandler.OnPowerUpApplied -= ApplyEffect;
        PlayerPowerupHandler.OnPowerUpExpired -= RemoveEffect;
    }

    // Is called when the effect begins
    private void ApplyEffect(PowerUpData data) 
    {
        if (data.powerUpName.Equals("PulseRadiusIncrease"))
        {
            Debug.Log("Power-Up Applied: I'm a noodle!");
            currentRadius = data.pulseRadius;
        }
    }

    // Is called when the effect ends
    private void RemoveEffect(PowerUpData data) 
    {
        if (data.powerUpName.Equals("PulseRadiusIncrease"))
        {
            Debug.Log("Power-Up Expired: Back to normal size.");
            currentRadius = originalRadius;
        }
    }
}