using UnityEngine;

public class BurnEffectHandler : MonoBehaviour
{
      // Declare fields as needed    
      // Shown only as an example   
private Vector3 originalScale;        		

// Needed if you need to grab additional components from the player
// such as the rigidbody shown
void Awake()
{
	originalScale = GetComponentInParent<Transform>().localScale;
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
          if (data.powerUpName.Equals("Noodle"))
          {
              // Grabs the new scale from the PowerUpData file
              // and applies it to the parent object
              transform.parent.localScale = data.scale;
              Debug.Log("Power-Up Applied: I'm a noodle!");
          }
      }

// Is called when the effect ends
      private void RemoveEffect(PowerUpData data) 
      {
          // Sets the scale back to where it was
          transform.parent.localScale = originalScale;
          Debug.Log("Power-Up Expired: Back to normal size.");
      }
}
