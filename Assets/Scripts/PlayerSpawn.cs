using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    private void Start()
    {
        
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation; 
    }
}
