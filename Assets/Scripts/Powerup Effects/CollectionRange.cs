/**********************************************************************
* Effect Handler — attached to PowerUp Scoring child prefab on Player
*
* Temporarily expands the player's scoring trigger collider radius by
* data.collectionRange (used as a multiplier, e.g. 1.5 = 50% larger)
* for data.duration seconds, then restores the original radius.
*
* Requires a SphereCollider set to Is Trigger on the same GameObject
* as PlayerScoreHandler. If none is found the effect is skipped safely.
*
* PowerUpData asset setup:
*   powerUpName     → "Collection Range"
*   collectionRange → radius multiplier (e.g. 1.5 for 50% larger)
*   duration        → active time in seconds (e.g. 10)
*
* Zo Nijjar
* April 2026
**********************************************************************/
using UnityEngine;

public class CollectionRange : MonoBehaviour
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
