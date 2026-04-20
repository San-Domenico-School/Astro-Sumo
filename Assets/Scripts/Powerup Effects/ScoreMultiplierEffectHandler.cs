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
        if (data.powerUpName.Equals("Score Multiplier") && scoreHandler != null)
        {
            scoreHandler.scoreMultiplier = data.scoreMultiplier;
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

