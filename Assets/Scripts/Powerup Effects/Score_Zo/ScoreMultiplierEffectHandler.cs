using UnityEngine;

public class ScoreMultiplierEffectHandler : MonoBehaviour
{
    private PlayerScoreHandler scoreHandler;

    void Awake()
    {
        scoreHandler = GetComponentInParent<PlayerScoreHandler>();
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
        if (data.powerUpName.Equals("Score Multiplier") && scoreHandler != null)
        {
            scoreHandler.scoreMultiplier *= data.scoreMultiplier;
            Debug.Log($"Power-Up Applied: {data.scoreMultiplier}x score multiplier active!");
        }
    }

    private void RemoveEffect(PowerUpData data)
    {
        if (data.powerUpName.Equals("Score Multiplier") && scoreHandler != null)
        {
            scoreHandler.scoreMultiplier = 1f;
            Debug.Log("Power-Up Expired: Score multiplier reset to 1.");
        }
    }
}

