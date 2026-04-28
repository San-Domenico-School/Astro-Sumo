using UnityEngine;

public class CollectionRangeEffectsHandler : MonoBehaviour
{
    private PlayerScoreHandler scoreHandler;
    private SphereCollider scoreCollider;
    private float originalRadius;

    void Awake()
    {
        scoreHandler = GetComponentInParent<PlayerScoreHandler>();
        if (scoreHandler == null) return;

        // Find the trigger SphereCollider used for scoreable detection
        foreach (SphereCollider col in scoreHandler.GetComponents<SphereCollider>())
        {
            if (col.isTrigger) { scoreCollider = col; break; }
        }

        // Fallback: grab any SphereCollider on the score handler object
        if (scoreCollider == null)
            scoreCollider = scoreHandler.GetComponent<SphereCollider>();

        if (scoreCollider != null)
            originalRadius = scoreCollider.radius;
    }

    void OnEnable()
{
    // Find the specific handler on THIS player
    PlayerPowerupHandler handler = GetComponentInParent<PlayerPowerupHandler>();
    
    if (handler != null)
    {
        handler.OnPowerUpApplied += ApplyEffect;
        handler.OnPowerUpExpired += RemoveEffect;
    }
}

void OnDisable()
{
    // Clean up using the same logic
    PlayerPowerupHandler handler = GetComponentInParent<PlayerPowerupHandler>();
    
    if (handler != null)
    {
        handler.OnPowerUpApplied -= ApplyEffect;
        handler.OnPowerUpExpired -= RemoveEffect;
    }
}

    private void ApplyEffect(PowerUpData data)
    {
        if (data.powerUpName.Equals("Collection Range") && scoreCollider != null && data.collectionRange > 0f)
        {
            scoreCollider.radius = originalRadius * data.collectionRange;
            Debug.Log($"Power-Up Applied: Collection radius x{data.collectionRange}!");
        }
    }

    private void RemoveEffect(PowerUpData data)
    {
        if (data.powerUpName.Equals("Collection Range") && scoreCollider != null)
        {
            scoreCollider.radius = originalRadius;
            Debug.Log("Power-Up Expired: Collection radius restored.");
        }
    }
}
