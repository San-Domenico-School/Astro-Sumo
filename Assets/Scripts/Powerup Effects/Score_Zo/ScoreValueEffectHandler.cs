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
        if (data.powerUpName.Equals("Score Value") && data.scoreValue > 0 && scoreHandler != null)
        {
            scoreHandler.AddScore((int)(data.scoreValue * scoreHandler.scoreMultiplier));
            Debug.Log($"Power-Up Applied: +{data.scoreValue} points!");
        }
    }

    private void RemoveEffect(PowerUpData data) { }
}

