using UnityEngine;

public class ReversePowerUp : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerMovement[] allPlayers;

    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void ApplyEffect(PowerUpData data)
    {
        if (data.powerUpName.Equals("ReverseAll"))
        {
            allPlayers = Object.FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);
            foreach (PlayerMovement player in allPlayers)
            {
                player.controlsReversed = data.reverseControls;
                Debug.Log("Power-Up Applied: Opponents reversed");
            }

            // Un-reverses your own controls
            playerMovement.controlsReversed = !data.reverseControls;
        }
    }

    private void RemoveEffect(PowerUpData data)
    {
        foreach (PlayerMovement player in allPlayers)
        {
            player.controlsReversed = !data.reverseControls;
            Debug.Log("Power-Up Removed: Opponents un-reversed");
        }
    }
}