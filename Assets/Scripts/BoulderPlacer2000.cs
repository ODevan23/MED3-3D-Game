using UnityEngine;

public class BoulderPlacer : MonoBehaviour
{
    [Tooltip("The terrain where boulders will be placed")]
    public Terrain terrain;

    [Tooltip("The boulder prefab to place randomly")]
    public GameObject boulderPrefab;

    [Tooltip("Number of boulders to place on the terrain")]
    public int numberOfBoulders = 50;

    [Tooltip("Minimum scale for the boulders")]
    public float minScale = 0.5f;

    [Tooltip("Maximum scale for the boulders")]
    public float maxScale = 2.0f;

    [Tooltip("The parent object where all boulders will be placed")]
    public GameObject boulderParent;

    private void Start()
    {
        PlaceBoulders();
    }

    private void PlaceBoulders()
    {
        if (terrain == null || boulderPrefab == null)
        {
            Debug.LogError("Terrain or boulder prefab is not set.");
            return;
        }

        // Get terrain bounds
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;
        Vector3 terrainPosition = terrain.transform.position;

        for (int i = 0; i < numberOfBoulders; i++)
        {
            // Random position on terrain
            float x = Random.Range(0, terrainSize.x) + terrainPosition.x;
            float z = Random.Range(0, terrainSize.z) + terrainPosition.z;
            float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrainPosition.y;

            // Instantiate the boulder
            GameObject boulder = Instantiate(boulderPrefab, new Vector3(x, y, z), Quaternion.identity);

            // Apply random rotation
            boulder.transform.rotation = Quaternion.Euler(
                Random.Range(0, 360),
                Random.Range(0, 360),
                Random.Range(0, 360)
            );

            // Apply random scale
            float randomScale = Random.Range(minScale, maxScale);
            boulder.transform.localScale = Vector3.one * randomScale;

            // Set the boulder's parent
            if (boulderParent != null)
            {
                boulder.transform.parent = boulderParent.transform;
            }
        }

        Debug.Log($"{numberOfBoulders} boulders placed on the terrain.");
    }
}
