/**********************************************************************
* Effect Handler — attached to PowerUp Scoring child prefab on Player
*
* Listens for the OnPowerUpApplied event. When a "Score Value" power-up
* is picked up, instantly awards its scoreValue points to the player's
* team. No revert is needed because the effect is a one-time grant.
*
* PowerUpData asset setup:
*   powerUpName  → "Score Value"
*   scoreValue   → desired point amount (e.g. 10)
*   duration     → any value (effect is instant; timer is ignored)
*
* Zo Nijjar
* April 2026
**********************************************************************/
using UnityEngine;

public class ScoreValue : MonoBehaviour
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
