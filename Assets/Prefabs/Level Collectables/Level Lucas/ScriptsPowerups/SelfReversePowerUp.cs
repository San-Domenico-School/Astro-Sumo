using UnityEngine;
using System.Collections;

public class SelfReversePowerUp : MonoBehaviour
{
    public float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement snagger = other.GetComponent<PlayerMovement>();

        if (snagger != null)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            StartCoroutine(TempReverse(snagger));

            Debug.Log(snagger.name + " reversed their own controls!");
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