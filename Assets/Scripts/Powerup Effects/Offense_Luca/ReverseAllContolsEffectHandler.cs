using UnityEngine;

public class ReverseAllContolsEffectHandler : MonoBehaviour
{
      // Declare fields as needed    
      // Shown only as an example   
private PlayerMovement playerMovement;
private PlayerMovement[] allPlayers; 
private int teamID;   		

// Needed if you need to grab additional components from the player
// such as the rigidbody shown
void Awake()
{
	playerMovement = GetComponentInParent<PlayerMovement>();
}

    // Called when this object becomes enabled and active
    // We subscribe to the global power-up events here
void Start()
{
    teamID = GetComponentInParent<PlayerMovement>().teamID;
}
void OnEnable()
{
    // Find the specific handler on THIS player
    PlayerPowerupHandler handler = GetComponentInParent<PlayerPowerupHandler>();
    
    if (handler != null)
    {
        handler.OnPowerUpApplied += ApplyEffect;
        handler.OnPowerUpExpired += RemoveEffect;
    }
}

void OnDisable()
{
    // Clean up using the same logic
    PlayerPowerupHandler handler = GetComponentInParent<PlayerPowerupHandler>();
    
    if (handler != null)
    {
        handler.OnPowerUpApplied -= ApplyEffect;
        handler.OnPowerUpExpired -= RemoveEffect;
    }
}

// Is called when the effect begins
// The PowerUpData parameter contains all configuration values
private void ApplyEffect(PowerUpData data) 
{
    // This prevents the player from stretching for EVERY power-up
    if (data.powerUpName.Equals("Reverse All Controls"))
    {
        allPlayers = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);

        // This loop freezes all player controls (including yours)
        foreach (PlayerMovement player in allPlayers)
        {
                int playerTeamID = player.teamID;
                if(!(playerTeamID == teamID))
                    player.controlsReversed = data.reverseControls;
        }
        
        //playerMovement.controlsReversed = !data.reverseControls;
        Debug.Log("Power-Up Applied: Reverse controls for all opponents!");

    }
}

// Is called when the effect ends
    private void RemoveEffect(PowerUpData data) 
    {
        if (data.powerUpName.Equals("Reverse All Controls"))
        {
            foreach (PlayerMovement player in allPlayers)
            {
                player.controlsReversed = !data.reverseControls;
            }
            Debug.Log("Power-Up Expired: Back to normal controls.");
        }
    }
}