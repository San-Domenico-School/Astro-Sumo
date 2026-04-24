using UnityEngine;

public class FreezeAllEffectHandler : MonoBehaviour
{
      // Declare fields as needed    
      // Shown only as an example   
private PlayerMovement[] allPlayers;       		

// Needed if you need to grab additional components from the player
// such as the rigidbody shown
void Awake()
{
	//
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
          if (data.powerUpName.Equals("SlowAll"))
          {
            allPlayers = Object.FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);
            foreach (PlayerMovement player in allPlayers)
            {
                player.moveMagnitude *= data.speedMultiplier;
            }
            
            GetComponentInParent<PlayerMovement>().moveMagnitude /= data.speedMultiplier;
            Debug.Log("Power-Up Applied: Opponents slowed");
          }
      }

// Is called when the effect ends
      private void RemoveEffect(PowerUpData data) 
      {
          // Sets the scale back to where it was
          if (data.powerUpName.Equals("SlowAll"))
          {
            foreach (PlayerMovement player in allPlayers)
            {
                player.moveMagnitude = 1;
            }
            Debug.Log("Power-Up Expired: Back to normal speed.");
          }
      }
}
