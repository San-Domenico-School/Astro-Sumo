using UnityEngine;

public class BaseCollectible : MonoBehaviour
{
    [Header("Score Settings")]
    public int scoreValue = 10;

    [Header("Effects")]
    public AudioClip collectSound;
    public GameObject collectEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Grab score manager and apply score
        ScoreManager scoreManager = FindAnyObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddScore(scoreValue);
        }

        // Visual effect
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }

        // Audio effect
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        Destroy(gameObject);
    }
}