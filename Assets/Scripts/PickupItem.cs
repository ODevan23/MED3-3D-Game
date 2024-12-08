using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Tooltip("The maximum distance the player can pick up the item from")]
    public float pickupRange = 3.0f;

    private Transform playerCamera;

    [Tooltip("Reference to the MonsterSpawner script")]
    public MonsterSpawner monsterSpawner; // Reference to the MonsterSpawner

    private void Start()
    {
        // Get the player's main camera
        playerCamera = Camera.main.transform;
    }

    private void Update()
    {
        // Check if the player presses E
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    private void TryPickup()
    {
        // Perform a raycast forward from the camera
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            // Check if the raycast hit this object
            if (hit.collider.gameObject == gameObject)
            {
                Pickup();
            }
        }
    }

    private void Pickup()
    {
        // Destroy the object (make it disappear)
        Destroy(gameObject);

        // Spawn monsters when the item is picked up
        if (monsterSpawner != null)
        {
            monsterSpawner.SpawnMonsters();
        }
        else
        {
            Debug.LogError("MonsterSpawner not assigned in PickupItem script!");
        }
    }
}
