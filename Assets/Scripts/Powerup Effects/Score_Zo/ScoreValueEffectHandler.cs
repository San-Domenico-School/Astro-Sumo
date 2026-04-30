using UnityEngine;

public class ScoreValueEffectHandler : MonoBehaviour
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
        if (data.powerUpName.Equals("Score Value") && data.scoreValue > 0 && scoreHandler != null)
        {
            scoreHandler.AddScore((int)(data.scoreValue * scoreHandler.scoreMultiplier));
            Debug.Log($"Power-Up Applied: +{data.scoreValue} points!");
        }
    }

    private void RemoveEffect(PowerUpData data) { }
}

