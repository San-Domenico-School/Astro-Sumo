using UnityEngine;
using System.Collections;

public class ReversePowerUp : MonoBehaviour
{
    public float duration = 5f;

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
                if (p != snagger) StartCoroutine(TempReverse(p));
            }

            Debug.Log(snagger.name + " reversed everyone else!");
            Destroy(gameObject, duration + 0.1f);
        }
    }

    IEnumerator TempReverse(PlayerMovement p)
    {
        p.controlsReversed = true;
        yield return new WaitForSeconds(duration);
        p.controlsReversed = false;
    }
}