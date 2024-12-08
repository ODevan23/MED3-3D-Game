using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Tooltip("The maximum distance the player can pick up the item from")]
    public float pickupRange = 3.0f;

    private Transform playerCamera;

    [Tooltip("Reference to the MonsterSpawner script")]
    public MonsterSpawner monsterSpawner;

    private void Start()
    {
        
        playerCamera = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    private void TryPickup()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            if (hit.collider.gameObject == gameObject)
            {
                Pickup();
            }
        }
    }

    private void Pickup()
    {
        Destroy(gameObject);

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
