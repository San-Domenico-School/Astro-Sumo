/**********************************************************************
* Effect Handler — attached to PowerUp Scoring child prefab on Player
*
* Listens for power-up events and temporarily multiplies all points
* earned from scoreables by data.scoreMultiplier for data.duration
* seconds, then resets the multiplier back to 1.
*
* PowerUpData asset setup:
*   powerUpName     → "Score Multiplier"
*   scoreMultiplier → desired multiplier (e.g. 2.0 for double points)
*   duration        → active time in seconds (e.g. 30)
*
* Zo Nijjar
* April 2026
**********************************************************************/
using UnityEngine;

public class ScoreMultiplier_zo : MonoBehaviour
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
