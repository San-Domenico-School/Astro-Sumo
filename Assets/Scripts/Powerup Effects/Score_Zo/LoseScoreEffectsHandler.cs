using UnityEngine;

public class LoseScoreEffectsHandler : MonoBehaviour
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
        if (data.powerUpName.Equals("Lose Score") && data.loseScore > 0 && scoreHandler != null)
        {
            scoreHandler.SubtractScore((int)data.loseScore);
            Debug.Log($"Power-Up Applied: -{data.loseScore} point penalty!");
        }
    }

    private void RemoveEffect(PowerUpData data) { }
}
