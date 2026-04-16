using UnityEngine;
using System.Collections;


public class SlowPowerUp : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerMovement[] allPlayers;

    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void ApplyEffect(PowerUpData data)
    {
        if (data.powerUpName.Equals("SlowAll"))
        {
            allPlayers = Object.FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);
            foreach (PlayerMovement player in allPlayers)
            {
                player.moveMagnitude *= data.speedMultiplier;
                Debug.Log("Power-Up Applied: Opponents slowed");
            }

            // This un-slows your own controls
            playerMovement.moveMagnitude /= data.speedMultiplier;
        }
    }

    private void RemoveEffect(PowerUpData data)
    {
        foreach (PlayerMovement player in allPlayers)
        {
            player.moveMagnitude /= data.speedMultiplier;
            Debug.Log("Power-Up Removed: Opponents un-slowed");
        }
    }
}