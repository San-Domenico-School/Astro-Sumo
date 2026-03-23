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
    [SerializeField] private GameObject audioEffectPrefab;
    [SerializeField] private GameObject particleEffectPrefab;

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
        StartCoroutine("HandlePickup");
    }

    //Destroys the game object after the alloted time as passed.
    IEnumerator PowerUpTimeout()
    {
        yield return new WaitForSeconds(timeInScene);
        Destroy(gameObject);
    }

    IEnumerator HandlePickup()
    {
        float audioDuration = 0f;
        float particleDuration = 0f;

        // 1. Handle Audio Prefab
        if (audioEffectPrefab != null)
        {
            // Create the sound in the scene at the current object's position
            GameObject audioObj = Instantiate(audioEffectPrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = audioObj.GetComponent<AudioSource>();
            
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
                audioDuration = audioSource.clip.length;
                // Tell the spawned sound to destroy itself after it finishes
                Destroy(audioObj, audioDuration);
            }
        }

        // 2. Handle Particle Prefab
        if (particleEffectPrefab != null)
        {
            // Create the particles in the scene
            GameObject particleObj = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
            ParticleSystem particle = particleObj.GetComponent<ParticleSystem>();
            
            if (particle != null)
            {
                particle.Play();
                particleDuration = particle.main.duration;
                // Tell the spawned particles to destroy themselves after they finish
                Destroy(particleObj, particleDuration);
            }
        }

        // 3. Cleanup the original Collectable Object
        // We don't actually need to wait for the sounds/particles here 
        // because the spawned objects handle their own destruction now.
        float waitTime = Mathf.Max(audioDuration, particleDuration);

        // 4. Wait
        yield return new WaitForSeconds(waitTime);

        // 5. Final Cleanup (This will always run)
        Destroy(gameObject);
    }
}
