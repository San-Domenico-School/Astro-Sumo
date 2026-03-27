using UnityEngine;

public class FreezePowerUp : MonoBehaviour 
{
    public float freezeDuration = 3f;

    private void OnTriggerEnter(Collider other) 
    {
        // Check if the object that touched the powerup has the PlayerMovement script
        PlayerMovement snagger = other.GetComponent<PlayerMovement>();

        if (snagger != null) 
        {
            ApplyGlobalFreeze(snagger);
            Destroy(gameObject); 
        }
    }

    void ApplyGlobalFreeze(PlayerMovement theWinner) 
    {
        // Fixed syntax for finding objects
        PlayerMovement[] allPlayers = Object.FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);

        foreach (PlayerMovement p in allPlayers) 
        {
            // If this player is NOT the one who picked it up, freeze them
            if (p != theWinner) 
            {
               // p.Freeze(freezeDuration);
            }
        }
        
        Debug.Log(theWinner.name + " froze everyone else!");
    }
}