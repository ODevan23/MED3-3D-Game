using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Tooltip("Optional sound or effect on pickup")]
    public AudioClip pickupSound;
    [Tooltip("The maximum distance the player can pick up the item from")]
    public float pickupRange = 3.0f;

    private Transform playerCamera;

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
        // Play the pickup sound if available
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        // Destroy the object (make it disappear)
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the pickup range in the Scene view
        Gizmos.color = Color.yellow;
        if (playerCamera != null)
        {
            Gizmos.DrawRay(playerCamera.position, playerCamera.forward * pickupRange);
        }
    }
}
