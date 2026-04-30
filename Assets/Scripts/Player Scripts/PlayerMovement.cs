/*******************************************************************
* This is attached to the Player
*
* Handles physics-based movement using the Unity Input System. 
* Manages player interactions, including knockback force on collisions 
* with other players and dynamic linear damping (friction) when 
* interacting with "Edge" trigger zones.
* 
* Bruce Gustin
* Jan 2, 2026v1.1 Mar 26, 2026v1.2
*******************************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    private Rigidbody playerRB;
    
    private Vector3 moveDirection;
    public Vector3 originalSpawnPosition {get; private set;}

    // Fields effeced by powerups
    [HideInInspector]
    public float moveMagnitude, linearDamping, appliedForce;
    [HideInInspector]
    public bool controlsReversed, isFrozen; 
    [HideInInspector]
    public int teamID;
    private bool isAtEdge;
   
    // Initializes physics references and sets default movement and physics property values.
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        originalSpawnPosition = transform.position;
        playerRB = GetComponent<Rigidbody>();
        teamID = GetComponent<PlayerScoreHandler>().teamID;
        moveMagnitude = 250;
        linearDamping = 0.5f;
    }

    // Updates physics-based movement and checks global scores 
    // at a fixed interval for physical consistency.
    void FixedUpdate()
    {
        Move();
    }

    // Callback method triggered by the Player Input component. 
    // Sends 2D input into SetMoveDirection for creation of a 3D vector.
    public void OnInputAction(InputAction.CallbackContext ctx) => SetMoveDirection(ctx.ReadValue<Vector2>());


    // Normalizes the input vector to ensure consistent movement speed 
    // regardless of diagonal input directions.
    private void SetMoveDirection(Vector2 value)
    {
        // Causes inputs to flip if flip is true
        int powerUpInfluence = controlsReversed ? -1 : 1;

        // Causes inputs to freeze if freeze is true
        if(isFrozen) powerUpInfluence = 0;

        float right = value.x * powerUpInfluence;
        float forward = value.y * powerUpInfluence;
        moveDirection = (new Vector3(right, 0, forward)).normalized;
    }

    // Applies continuous force to the Rigidbody based on input. 
    // Also handles destruction of the player if they fall below the map threshold.
    private void Move()
    {
        // anchors player
        if(isFrozen)
        {
            playerRB.linearDamping = 1000;
        }       
        else if(!isAtEdge)
        {
            playerRB.linearDamping = linearDamping;
        }

        //Adds force to player
        playerRB.AddForce(moveDirection * moveMagnitude * Time.deltaTime);
        if(transform.position.y < -10)
        {
            playerRB.useGravity = false;
            
            // Puts the player under the scene to simulate falling out of the arena, then respawns them after a short delay
            transform.position = new Vector3(0, -10f, -3.4f);
        }
    }

    // Detects collisions with other players to calculate and apply 
    // an impulse-based knockback force away from the point of impact.
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Rigidbody otherRB = collision.gameObject.GetComponent<Rigidbody>();
            
            // Calculate the direction from ME to the OTHER player
            Vector3 directionAway = (collision.gameObject.transform.position - transform.position).normalized;
            
            // Use my current power
            float myPower = moveMagnitude * appliedForce * Time.deltaTime;
            
            // Apply force in the direction of the impact
            otherRB.AddForce(directionAway * myPower, ForceMode.Impulse);
        }
    }

    // Increases linear damping immediately when entering an "Edge" zone 
    // to simulate high friction or braking
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Edge"))
        {
            isAtEdge = true;
            playerRB.linearDamping = 12.5f;
        }
    }

    // Gradually reduces linear damping while staying within the "Edge" zone, 
    // allowing the player to slowly regain mobility.
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Edge"))
        {
            if(playerRB.linearDamping > linearDamping)
            {
                playerRB.linearDamping *= .975f;
            }
        }
    }
    // Resets linear damping to default values upon exiting the "Edge" zone.
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Edge"))
        {
            isAtEdge = false;
            playerRB.linearDamping = linearDamping;
        }
    }
}
    