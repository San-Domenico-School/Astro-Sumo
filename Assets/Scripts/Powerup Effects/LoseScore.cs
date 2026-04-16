/**********************************************************************
* Effect Handler — attached to PowerUp Scoring child prefab on Player
*
* Listens for the OnPowerUpApplied event. When a "Lose Score" item is
* picked up, instantly deducts data.loseScore points from the player's
* team score. Score is clamped so it cannot drop below zero.
*
* PowerUpData asset setup:
*   powerUpName → "Lose Score"
*   loseScore   → points to deduct (e.g. 20)
*   duration    → any value (effect is instant; timer is ignored)
*
* Zo Nijjar
* April 2026
**********************************************************************/
using UnityEngine;

public class LoseScore : MonoBehaviour
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
