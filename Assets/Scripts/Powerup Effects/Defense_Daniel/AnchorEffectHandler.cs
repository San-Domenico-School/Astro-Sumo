using UnityEngine;

public class AnchorEffectHandler : MonoBehaviour
{
      // Declare fields as needed    
      // Shown only as an example     
      private PlayerMovement playerMovement;		

// Needed if you need to grab additional components from the player
// such as the rigidbody shown
void Awake()
{
	
playerMovement = GetComponentInParent<PlayerMovement>();

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
          if (data.powerUpName.Equals("Anchor"))
          {
              
            playerMovement.isFrozen = data.anchorMode;
          }
      }

// Is called when the effect ends
      private void RemoveEffect(PowerUpData data) 
      {
          if (data.powerUpName.Equals("Anchor"))
          {
            playerMovement.isFrozen = !data.anchorMode;
          }
      }
}
