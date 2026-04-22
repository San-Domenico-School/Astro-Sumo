using UnityEngine;

public class LosescoreeffectsHandler : MonoBehaviour
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
        if (data.powerUpName.Equals("Lose Score") && data.loseScore > 0 && scoreHandler != null)
        {
            scoreHandler.SubtractScore((int)data.loseScore);
            Debug.Log($"Power-Up Applied: -{data.loseScore} point penalty!");
        }
    }

    private void RemoveEffect(PowerUpData data) { }
}
