using UnityEngine;

public class PushRangeEffectHandler : MonoBehaviour
{
      // Declare fields as needed    
      // Shown only as an example   
 
private Rigidbody rigidbody;   
private float originalPushRange;   	

private float currentPushRange;

// Needed if you need to grab additional components from the player
// such as the rigidbody shown
void Awake()
{
	
    rigidbody = GetComponentInParent<Rigidbody>();
    currentPushRange = originalPushRange;
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
          if (data.powerUpName.Equals("IncreasePushRange"))
          {
              SphereCollider[] sphereColliders = GetComponentsInParent<SphereCollider>();
             foreach(SphereCollider sphereCollider in sphereColliders)
            {
                if (sphereCollider.isTrigger)
                {
                    sphereCollider.radius = data.pulseRadius;
                }
            }
              
            
              
              Debug.Log("Power-Up Applied: I'm a pulser radius!");
              
            
          }
      }

// Is called when the effect ends
      private void RemoveEffect(PowerUpData data)

    {
        if (data.powerUpName.Equals("IncreasePushRange"))
          {
              SphereCollider[] sphereColliders = GetComponentsInParent<SphereCollider>();
             foreach(SphereCollider sphereCollider in sphereColliders)
            {
                if (sphereCollider.isTrigger)
                {
                    sphereCollider.radius = 0.5f;
                }
            }
              
            
              
              Debug.Log("radius normal!");
              
            
          }
    }
}

