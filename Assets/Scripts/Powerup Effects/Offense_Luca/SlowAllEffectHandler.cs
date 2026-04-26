using UnityEngine;

public class SlowAllEffectHandler : MonoBehaviour
{
      // Declare fields as needed    
      // Shown only as an example   
private PlayerMovement playerMovement;
private PlayerMovement[] allPlayers;
private float originalMoveMagnitude;
    		

// Needed if you need to grab additional components from the player
// such as the rigidbody shown
void Awake()
{
	playerMovement = GetComponentInParent<PlayerMovement>();
}

void Start()
{
	originalMoveMagnitude = playerMovement.moveMagnitude;
}


// Called when this object becomes enabled and active
// We subscribe to the global power-up events here
void OnEnable()
{
PlayerPowerupHandler.OnPowerUpApplied += ApplyEffect;
PlayerPowerupHandler.OnPowerUpExpired += RemoveEffect;
}

// Called when this object is disabled or destroyed
// We must unsubscribe to prevent errors and unwanted behavior
void OnDisable()
{
PlayerPowerupHandler.OnPowerUpApplied -= ApplyEffect;
PlayerPowerupHandler.OnPowerUpExpired -= RemoveEffect;
}

// Is called when the effect begins
// The PowerUpData parameter contains all configuration values
private void ApplyEffect(PowerUpData data) 
      {
          // This prevents the player from stretching for EVERY power-up
          if (data.powerUpName.Equals("Slow All"))
          {
            allPlayers = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);

            // This loop slows all players (including yours)
            foreach (PlayerMovement player in allPlayers)
            {
                player.moveMagnitude *= data.speedMultiplier;                  
            }
 
              // Unslow your speed
            playerMovement.isFrozen = !data.freezeNearbyPlayers;
            Debug.Log("Power-Up Applied: slows opponents");

          }
      }

// Is called when the effect ends
      private void RemoveEffect(PowerUpData data) 
      {
        // Sets the speed back to where it was
        if (data.powerUpName.Equals("Slow All"))
        {
            foreach (PlayerMovement player in allPlayers)
            {
                player.moveMagnitude = originalMoveMagnitude;
            }
            Debug.Log("Power-Up Expired: Back to normal speed.");
        }
    }
}
