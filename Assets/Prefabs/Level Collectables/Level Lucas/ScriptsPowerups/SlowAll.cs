using UnityEngine;
using System.Collections;

public class SlowPowerUp : MonoBehaviour
{
    public float duration = 5f;
    [Range(0, 1)] public float speedMultiplier = 0.4f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement snagger = other.GetComponent<PlayerMovement>();

        if (snagger != null)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            PlayerMovement[] allPlayers = Object.FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);

            foreach (PlayerMovement p in allPlayers)
            {
                if (p != snagger) StartCoroutine(TempSlow(p));
            }

            Debug.Log(snagger.name + " slowed everyone else!");
            Destroy(gameObject, duration + 0.1f);
        }
    }

    IEnumerator TempSlow(PlayerMovement p)
    {
        float originalSpeed = p.moveMagnitude;
        p.moveMagnitude *= speedMultiplier;
        yield return new WaitForSeconds(duration);
        p.moveMagnitude = originalSpeed;
    }
}