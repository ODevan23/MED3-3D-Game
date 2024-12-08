using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public float pickupRange = 3.0f;
    private Transform playerCamera;
    public MonsterSpawner monsterSpawner;

    private void Start()
    {
        
        playerCamera = Camera.main.transform; //We get the player camera's transform for later
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //if player click "E" it tries the TryPickup method
        {
            TryPickup();
        }
    }

    private void TryPickup() //Raycasts to see if the player camera is within the ray cast
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

    private void Pickup() //with a succesful pickup it destroys the blackbox and makes the monsterspawner start
    {
        Destroy(gameObject);

        if (monsterSpawner != null)
        {
            monsterSpawner.SpawnMonsters();
        }
        else
        {
            Debug.LogError("MonsterSpawner not assigned");
        }
    }
}
