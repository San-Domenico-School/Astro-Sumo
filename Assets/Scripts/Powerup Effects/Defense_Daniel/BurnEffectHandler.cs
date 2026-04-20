using UnityEngine;

public class BurnEffectHandler : MonoBehaviour
{
    private Vector3 originalScale;

    void Awake()
    {
        originalScale = GetComponentInParent<Transform>().localScale;
    }

    void OnEnable()
    {
        PlayerPowerupHandler.OnPowerUpApplied += ApplyEffect;
        PlayerPowerupHandler.OnPowerUpExpired += RemoveEffect;
    }

    void OnDisable()
    {
        PlayerPowerupHandler.OnPowerUpApplied -= ApplyEffect;
        PlayerPowerupHandler.OnPowerUpExpired -= RemoveEffect;
    }

    private void ApplyEffect(PowerUpData data)
    {
        // If the Burn power-up is applied, kill the player
        if (data.powerUpName.Equals("Burn"))
        {
            Debug.Log("Player burned!");

            // Destroy the parent object (player)
            Destroy(transform.parent.gameObject);
        }
    }

    private void RemoveEffect(PowerUpData data)
    {
        // Optional: only needed if player survives and you want reset logic
        transform.parent.localScale = originalScale;
    }
}