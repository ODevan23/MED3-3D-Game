using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Terrain[] terrains;
    public GameObject monsterPrefab;
    public int numberOfMonsters = 5;

    private bool monstersSpawned = false;

    public void SpawnMonsters()
    {
        if (!monstersSpawned)
        {
            foreach (Terrain terrain in terrains)
            {
                // Get terrain bounds
                TerrainData terrainData = terrain.terrainData;
                Vector3 terrainSize = terrainData.size;
                Vector3 terrainPosition = terrain.transform.position;

                for (int i = 0; i < numberOfMonsters; i++)
                {
                    // Random position on terrain
                    float x = Random.Range(0, terrainSize.x) + terrainPosition.x;
                    float z = Random.Range(0, terrainSize.z) + terrainPosition.z;
                    float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrainPosition.y;

                    // Instantiate the monster at random positions
                    GameObject monster = Instantiate(monsterPrefab, new Vector3(x, y, z), Quaternion.identity);

                    // Assign random rotation to the monster
                    float randomRotationY = Random.Range(0f, 360f);
                    monster.transform.rotation = Quaternion.Euler(0, randomRotationY, 0);

                    // Optionally, parent it to an empty GameObject to keep the hierarchy clean
                    monster.transform.SetParent(this.transform);
                }
            }

            monstersSpawned = true;
            Debug.Log("Monsters have been spawned on all terrains.");
        }
    }
}
