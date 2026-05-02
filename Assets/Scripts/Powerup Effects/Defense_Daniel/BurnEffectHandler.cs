/********
* This powerup takes the player out of commission for its durations
* by having it float above the scene while turning off controls
*/

using UnityEngine;

public class BurnEffectHandler : MonoBehaviour
{
      // Declare fields as needed    
      // Shown only as an example    
private Rigidbody playerRB;   
private ParticleSystem particles;
private Vector3 topPosition; 
private float originalMass;  

// Needed if you need to grab additional components from the player
// such as the rigidbody shown
void Awake()
{
    playerRB = GetComponentInParent<Rigidbody>();
    particles = GetComponentInParent<ParticleSystem>();
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
        if (data.powerUpName.Equals("Burn"))
        {
            // Removes gravity from player
            playerRB.useGravity = false;

            // Increase mass to make it harder to move
            playerRB.mass *= data.massIncrease;

            // Turns on Particle System
            if(particles != null)
            {
                particles.Play();
                Debug.Log("Particle Starts");
            }

            // Moves player off ground
            topPosition = transform.parent.position + Vector3.up * 5;
            InvokeRepeating("MovePlayerUp", 0, .1f);
            Debug.Log("Power-Up Applied: I am floating");
        }
    }

// Is called when the effect ends
      private void RemoveEffect(PowerUpData data) 
      {
        if (data.powerUpName.Equals("Burn"))
        {
            // Reinserts gravity
            CancelInvoke("MovePlayerUp");
            playerRB.useGravity = true;
            playerRB.mass = originalMass;
            if(particles != null)
            {
                particles.Stop();
            }
            Debug.Log("Power-Up Applied: I falling again");
        }
      }

      private void MovePlayerUp()
    {
        if(transform.parent.position.y < topPosition.y)
        {
            transform.parent.position += Vector3.up * .25f;
        }
    }
}
