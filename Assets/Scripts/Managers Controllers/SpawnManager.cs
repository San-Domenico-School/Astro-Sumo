/**********************************************************************
* This script is attached to the Spawn Manager
*
* It spawns collectables into the scene.  
* 
* Bruce Gustin
* Jan 4, 2026
**********************************************************************/

using System.Linq; // Required for OrderBy and Take
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Objects to Spawn")]
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private GameObject[] scoreable;
    [SerializeField] private GameObject enemy;
    private GameObject[] spawnablePowerups;
   
    [Header("Spawn Attributes")]
    [Range(10, 60)]
    [SerializeField] private int powerupRate;   
        
    [Range(10, 60)]
    [SerializeField] private int scoreableRate;   
    [SerializeField] private int maxPowerupTypes;
   

    [Header("Maximum Enemies Per Wave")]
    [SerializeField] private float maxWave;
    private int currentWave;

    [HideInInspector]
    public static int enemyCount;


    void Start()
    {
        if (powerups.Length <= maxPowerupTypes)
        {
            spawnablePowerups = powerups;
        }
        else
        {
            // Shuffle the array randomly and take the first 'maxPowerupTypes' elements
            spawnablePowerups = powerups
                .OrderBy(x => Random.value) 
                .Take(maxPowerupTypes)
                .ToArray();
        }
    }

    // Spawns called per fram
    void Update()
    {
        SpawnPowerup();
        SpawnScoreable();
        SpawnEnemy();
    }
    
    // Spawns a specific powerup at a specific position at powerupRate
    void SpawnPowerup()
    {
        float spawnThreshold = powerupRate / 60f * Time.deltaTime;
        if(Random.value < spawnThreshold && spawnablePowerups.Length > 0)
        {
            int choiceIndex = Random.Range(0, spawnablePowerups.Length); 
            Vector3 position = SpawnLocation();
            Quaternion prefabRotation = spawnablePowerups[choiceIndex].transform.rotation;
            Instantiate(spawnablePowerups[choiceIndex], position, prefabRotation); 
        }
    }
    
    // Spawns a specific scoreable at a specific position at scoreableRate
    void SpawnScoreable()
    { 
        float spawnThreshold = scoreableRate / 60f * Time.deltaTime;
        if(Random.value < spawnThreshold && scoreable.Length > 0)
        {
            int choiceIndex = Random.Range(0, scoreable.Length); 
            Vector3 position = SpawnLocation();
            Quaternion prefabRotation = scoreable[choiceIndex].transform.rotation;
            Instantiate(scoreable[choiceIndex], position, prefabRotation); 
        }
    }

    // Spawns a wave of enemies when there are no enemies present.
    // Wave grows each cycle up to the maximum wave
    void SpawnEnemy()
    {
        if(enemyCount == 0 && enemy != null)
        {
            // Spawn a wave of enemies
            for(int i = 0; i < currentWave; i++)
            {
                Vector3 position = SpawnLocation();
                Instantiate(enemy, position, transform.rotation);
                enemyCount++;
            }
            
            // Set wave size, limited to max wave
            if (currentWave < maxWave) 
            {
                currentWave++;
            }
        }
    }

    // Returns a location on the Island
    private Vector3 SpawnLocation()
    {
        // Sets random position on island
        float xPos = Random.Range(-26.0f, 26.0f); 
        float zPos = Random.Range(-13.75f, 6.0f); 
        Vector3 position = new Vector3(xPos, 0, zPos);

        return position;
    }
}
