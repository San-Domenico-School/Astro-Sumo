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
        AudioSource audioSource = GetComponent<AudioSource>();
        ParticleSystem particle = GetComponent<ParticleSystem>();

        float audioDuration = 0f;
        float particleDuration = 0f;

        if(audioSource != null)
        {
            audioSource.Play();
            audioDuration = audioSource.clip.length;
        }

        if(particle != null)
        {
            particle.Play();
            particleDuration = particle.main.duration;
        }

        // Calculate the longest wait time based on what actually exists
        float waitTime = Mathf.Max(audioDuration, particleDuration);

        // If both were null, waitTime is 0, and it destroys immediately
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
