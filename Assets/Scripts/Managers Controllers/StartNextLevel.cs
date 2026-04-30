using UnityEngine;

public class StartNextLevel : MonoBehaviour
{
    private PlayerMovement[] allPlayers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         allPlayers = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);

        // This loop freezes all player controls (including yours)
        foreach (PlayerMovement player in allPlayers)
        {
            player.transform.position = player.GetComponent<PlayerMovement>().originalSpawnPosition;
            player.GetComponent<Rigidbody>().useGravity = true;
        }
    }

}
