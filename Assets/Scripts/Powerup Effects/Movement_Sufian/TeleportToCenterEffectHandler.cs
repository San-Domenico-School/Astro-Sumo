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
public class TeleportToCenterEffectHandler : MonoBehaviour
{
      // Declare fields as needed    
      // Shown only as an example   
        		


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
          if (data.powerUpName.Equals("Teleport To Center"))
          {
            transform.parent.position = new Vector3(0f, 0.5f, -4.75f);
            Debug.Log("Teleported");
          }
      }

// Is called when the effect ends
    private void RemoveEffect(PowerUpData data)
    {
        // Nothing needed here
    }
}
/*

*/
