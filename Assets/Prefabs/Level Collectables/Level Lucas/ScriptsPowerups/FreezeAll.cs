using UnityEngine;
using System.Collections;

public class FreezePowerUp : MonoBehaviour
{
    // Removed: private Vector3 originalScale;
    private PlayerMovement playerMovement;
    private PlayerMovement[] allPlayers;

    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void ApplyEffect(PowerUpData data)
    {
        // This prevents the player from stretching for EVERY power-up
        if (data.powerUpName.Equals("FreezeAll"))
        {
            allPlayers = Object.FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);
            // This loop freezes all player controls (including yours)
            foreach (PlayerMovement player in allPlayers)
            {
                player.isFrozen = data.freezeNearbyPlayers;
                Debug.Log("Power-Up Applied: Opponents frozen");
            }

            // This unfreezes your controls
            playerMovement.isFrozen = !data.freezeNearbyPlayers;
        }
    }

    private void RemoveEffect(PowerUpData data)
    {
        foreach (PlayerMovement player in allPlayers)
        {
            player.isFrozen = !data.freezeNearbyPlayers;
            Debug.Log("Power-Up Removed: Opponents freed");
        }
    }
}