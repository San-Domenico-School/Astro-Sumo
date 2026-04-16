using UnityEngine;
using System.Collections;

public class SelfReversePowerUp : MonoBehaviour
{
    private PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void ApplyEffect(PowerUpData data)
    {
        if (data.powerUpName.Equals("SelfReverse"))
        {
            playerMovement.controlsReversed = true;
            Debug.Log("Power-Up Applied: Your controls reversed");
        }
    }

    private void RemoveEffect(PowerUpData data)
    {
        playerMovement.controlsReversed = false;
        Debug.Log("Power-Up Removed: Your controls restored");
    }
}