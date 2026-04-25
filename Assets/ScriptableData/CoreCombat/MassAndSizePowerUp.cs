using UnityEngine;
using System.Collections;

public class MassAndSizePowerUp : MonoBehaviour
{
    [Header("Settings")]
    public float massMultiplier = 3.0f;
    public float sizeMultiplier = 2.0f;
    public float powerUpDuration = 15.0f; // Lasts 15 seconds
    public float itemExpiraton = 20.0f;   // Deletes itself if not picked up

    [Header("Visuals")]
    public float rotationSpeed = 100f;

    private bool isCollected = false;

    void Start()
    {
        // If nobody touches it, it disappears from the map
        Destroy(gameObject, itemExpiraton);
    }

    void Update()
    {
        if (!isCollected)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Stop the auto-destruct timer since we picked it up
                CancelInvoke(); 
                StartCoroutine(ApplyEffect(rb, other.transform));
            }
        }
    }

    private IEnumerator ApplyEffect(Rigidbody rb, Transform playerTransform)
    {
        isCollected = true;

        // 1. Hide the item immediately
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // 2. Save original stats and apply boost
        float originalMass = rb.mass;
        Vector3 originalScale = playerTransform.localScale;

        rb.mass *= massMultiplier;
        playerTransform.localScale *= sizeMultiplier;

        // 3. WAIT FOR 15 SECONDS
        yield return new WaitForSeconds(powerUpDuration);

        // 4. Revert everything back to normal
        if (playerTransform != null)
        {
            rb.mass = originalMass;
            playerTransform.localScale = originalScale;
        }

        // 5. Finally destroy the object
        Destroy(gameObject);
    }
}