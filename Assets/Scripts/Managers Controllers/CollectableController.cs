/*******************************************************************
* This is attached to each Collectable Prefab
*
* It determines how long that collectable remains in the scene
* and provides rotation animation capabilities
*
* Bruce Gustin
* Jan 2, 2026
*******************************************************************/

using System.Collections;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    [Range(3, 30)]
    [SerializeField] private float timeInScene;
    [SerializeField] private GameObject AVPrefab;

    [Tooltip("Use 0.00 to 1.0 in each axis")]
    [SerializeField] private Vector3 rotation;
    
    // Start the Coroutine at initialization
    void Start()
    {
        StartCoroutine("PowerUpTimeout");
    }

    // Gives the object a rotation animation
    void Update()
    {
        transform.Rotate(rotation);
    }

    public void DestroyCollectable()
    {
        StopCoroutine("PowerUpTimeout");
        HandlePickup();
    }

    //Destroys the game object after the alloted time as passed.
    IEnumerator PowerUpTimeout()
    {
        yield return new WaitForSeconds(timeInScene);
        Destroy(gameObject);
    }

    private void HandlePickup()
    {
        // Instantiate AVPrefab
        if (AVPrefab != null)
        {
            // Create the sound in the scene at the current object's position
            Instantiate(AVPrefab, transform.position, Quaternion.identity);
        }

        // Final Cleanup (This will always run)
        Destroy(gameObject);
    }
}
