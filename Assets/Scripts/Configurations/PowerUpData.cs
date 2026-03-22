/*******************************************************************
* This is not attached to any GameObject
*
* This script defines a ScriptableObject Data Container for Power-Ups. 
* It serves as a modular template that stores configuration data for 
* various gameplay modifiers, including combat physics, movement boosts, 
* scoring mechanics, and defensive/offensive abilities. 
*
* By creating assets from this script, designers can define unique 
* power-up behaviors (e.g., Speed Boost, Shield, Point Multiplier) 
* that can be referenced and applied to players at runtime.
* 
* Bruce Gustin
* Jan 2, 2026
*******************************************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "New PowerupController", menuName = "PowerUps/PowerUp Data")]
public class PowerUpData : ScriptableObject
{
    [Header("General Information")]
        public string powerUpName;                // Name of powerup
        public Color colorIndicator;              // Color of light around player indicating powerup
        public float duration;                    // Duration of effect
        public Vector3 scale;                     // Change in Player size

    [Header("Core Combat/Push Mechanics")]
        public float pulseRadius = 0.5f;          // Size of collider on player
        public float massIncrease = 0f;           // Harder to be pushed yourself
        public float massDecrease = 0f;           // easier to be pushed yourself
        public float pushRange = 0f;              // Increase push distance/radius

    [Header("Movement")]
        public float speedMultiplier = 1f;        // Move faster
        public float stun = 1f;                   // Stops movement
        public float sizeIncreaseMultiplier = 1f; // Makes you larger
        public bool teleportToCenter = false;     // Immediately move you to center of field

    [Header("Scoring")]
        public int scoreValue = 0;                // Instant points on pickup
        public float scoreMultiplier = 1f;        // Multiply points from scoreables
        public float loseScore = 0f;              // Instant point loss
        public float collectionRange = 0f;        // Larger collection radius

    [Header("Defense")]
        public bool ghostMode = false;            // Makes you transparent
        public bool burn = false;                 // Instant 
        public bool anchorMode = false;           // Can't be moved (but can't move either)
        public int reverseAttack = 0;             // Multi-hit shield

    [Header("Offense")]
        public bool freezeNearbyPlayers = false;  // Slow/freeze nearby enemies
        public float slowAll = 0f;                // Stun hit targets
        public bool reverseControls = false;      // Reverse enemy controls nearby
        public bool reverseYourControls = false;  // Reverse your controls nearby
}
