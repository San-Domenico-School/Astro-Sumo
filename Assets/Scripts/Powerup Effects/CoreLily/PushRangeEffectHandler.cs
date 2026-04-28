using UnityEngine;

public class PushRangeEffectHandler : MonoBehaviour
{
    // Declare fields as needed    
    // Shown only as an example     

    private SphereCollider[] sphereColliders;
    private SphereCollider sphereCollider; 
    private float originalColliderRadius;   	


    // Needed if you need to grab additional components from the player
    // such as the rigidbody shown
    void Awake()
    {
        sphereColliders = GetComponentsInParent<SphereCollider>();
        foreach(SphereCollider collider in sphereColliders)
        {
            if (collider.isTrigger)
            {
                sphereCollider = collider; 
            }
        }
        originalColliderRadius = sphereCollider.radius;
    }

    // Called when this object becomes enabled and active
    // We subscribe to the global power-up events here
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
        if (data.powerUpName.Equals("Increase Pickup Range"))
        {
            sphereCollider.radius = data.pulseRadius;  
            Debug.Log("Power-Up Applied: I can pickup at a distance");  
        }
    }

    // Is called when the effect ends
    private void RemoveEffect(PowerUpData data)
    {
        if (data.powerUpName.Equals("Increase Pickup Range"))
        {
            sphereCollider.radius = originalColliderRadius;
            Debug.Log("radius normal!");
        }

        
    }
}

