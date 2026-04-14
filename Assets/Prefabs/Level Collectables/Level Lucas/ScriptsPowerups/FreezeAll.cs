using UnityEngine;
using System.Collections;

public class FreezePowerUp : MonoBehaviour
{
    public float freezeDuration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement snagger = other.GetComponent<PlayerMovement>();

        if (snagger != null)
        {
            // Hide the powerup so it looks picked up
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            PlayerMovement[] allPlayers = Object.FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);

            foreach (PlayerMovement p in allPlayers)
            {
                if (p != snagger) StartCoroutine(TempFreeze(p));
            }

            Debug.Log(snagger.name + " froze everyone else!");
            // Destroy after the longest possible duration
            Destroy(gameObject, freezeDuration + 0.1f);
        }
    }

    IEnumerator TempFreeze(PlayerMovement p)
    {
        p.isFrozen = true;
        yield return new WaitForSeconds(freezeDuration);
        p.isFrozen = false;
    }
}