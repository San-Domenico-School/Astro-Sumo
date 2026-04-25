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

public class ReverseAttackHandler : MonoBehaviour
{
      // Declare fields as needed    
      // Shown only as an example   
private Rigidbody playerRB;  
private PlayerMovement playerMovement; 
private float originalMass; 
private float originalAppliedForce;  
private float originalMoveMagnitude;       

// Needed if you need to grab additional components from the player
// such as the rigidbody shown
void Awake()
{
    playerMovement = GetComponentInParent<PlayerMovement>();
    playerRB = GetComponentInParent<Rigidbody>();
    originalMass = playerRB.mass;
    originalAppliedForce = playerMovement.appliedForce;
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
          if (data.powerUpName.Equals("ReverseAttack"))
          {
              // Make your player very heavy but still move normal
              playerRB.mass *= data.massIncrease;
              playerMovement.moveMagnitude *= data.speedMultiplier;

              // Change Applied Force to the opponent
              playerMovement.appliedForce = data.reverseAttack;

              Debug.Log("Power-Up Applied: Reversed Attack");
          }
      }

// Is called when the effect ends
    private void RemoveEffect(PowerUpData data) 
    {
        if (data.powerUpName.Equals("ReverseAttack"))
        {
          // Set your player to orginal mass
            playerRB.mass = originalMass;
            playerMovement.moveMagnitude = originalMoveMagnitude;
            playerMovement.appliedForce = originalAppliedForce;
        }
          // Reverse Applied Force
          Debug.Log("Power-Up Expired: Reversed Attack");
    }
}