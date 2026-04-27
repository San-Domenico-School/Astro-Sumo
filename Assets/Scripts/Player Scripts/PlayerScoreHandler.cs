/*******************************************************************
* This is attached to the Player
*
* Detects collisions with scoreable world objects. Calculates final 
* score values by applying active multipliers and broadcasts the 
* results to the GlobalEvents system to update team scores
* 
* Bruce Gustin
* Jan 2, 2026
*******************************************************************/
using UnityEngine;

public class PlayerScoreHandler : MonoBehaviour
{
    // Identifies which team this player belongs to. 
    // Used as an index when sending scores to GlobalEvents.
    [HideInInspector]
    public int teamID;
   
    // A scalar value applied to all collected points. 
    // Controlled by external power-up scripts to temporarily boost scoring.
    [HideInInspector]
    public float scoreMultiplier = 1;
    
    // Adds points to this player's team score via GlobalEvents.
    public void AddScore(int points)
    {
        GlobalEvents.SendScore(teamID, points);
    }

    // Subtracts points, clamped so team score cannot go below zero.
    public void SubtractScore(int points)
    {
        int current = GlobalEvents.TeamScores[teamID];
        int loss = Mathf.Min(points, current);
        if (loss > 0) GlobalEvents.SendScore(teamID, -loss);
    }

    // Detects contact with scoreable triggers. Retrieves point data, calculates the multiplied total,
    // and triggers a global score event before removing the object from the scene.
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Scoreable"))
        {
            // 1. GET SCORE VALUE   
            // This receives from the scoreable te points"
            ScoreableConfig scoreable = other.GetComponent<ScoreableConfig>();

            // 2. CALL THE EVENT (The "Broadcast")
            // This sends a signal to the whole game saying "Team X got points!"
            GlobalEvents.SendScore(teamID, (int) (scoreable.scoreValue * scoreMultiplier));
          
            // 3. Remove the scoreable from the world
            CollectableController collectableController = other.gameObject.GetComponent<CollectableController>();
            collectableController.DestroyCollectable();
        }
    }
}
