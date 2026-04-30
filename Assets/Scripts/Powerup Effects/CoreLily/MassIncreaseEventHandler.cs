/**********************************************************************
* This script is an Effect Handler attached to the Player Prefab's 
* Powerup child
*
* This component listens for global Power-Up events dispatched by the 
* PlayerPowerupHandler. It acts as a specialized "listener" that 
* modifies specific player attributes (e.g., speed, physics, scoring) 
* only when relevant PowerUpData is received.
* 
* This modular approach allows for adding new power-up behaviors 
* without modifying existing player movement or combat code which 
* would cause merge errors
*
* This example is one that has a visible effect, but is not 
* useful in the game.  It is for demonstration purposes only.
* 
* Your Name
* Date
**********************************************************************/
using UnityEngine;

public class MassIncreaseEventHandler : MonoBehaviour
{
      // Declare fields as needed    
      // Shown only as an example   
    private Vector3 originalScale;  
    private Rigidbody playerRB;   
    private float originalMass;   		

    // Needed if you need to grab additional components from the player
    // such as the rigidbody shown
    void Awake()
    {
        originalScale = transform.parent.localScale;
        playerRB = GetComponentInParent<Rigidbody>();

    }

    void Start()
    {
        originalMass = playerRB.mass;
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
          if (data.powerUpName.Equals("MassIncrease"))
          {
              // Grabs the new scale from the PowerUpData file
              // and applies it to the parent object
              transform.parent.localScale = data.scale;
              Debug.Log("Power-Up Applied: Mass increase");
              playerRB.mass = data.massIncrease;
          }
      }

// Is called when the effect ends
      private void RemoveEffect(PowerUpData data)
    {
        if (data.powerUpName.Equals("MassIncrease"))
         {
          // Sets the scale back to where it was
          transform.parent.localScale = originalScale;
          Debug.Log("Power-Up Expired: Back to normal size.");
          playerRB.mass = originalMass;
       }
    }
      
     
}
