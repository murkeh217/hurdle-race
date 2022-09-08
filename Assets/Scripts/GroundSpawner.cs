using System.Collections;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{

    [SerializeField] GameObject groundTile;
    [SerializeField] Vector3 nextSpawnPoint;

    public void SpawnTile(bool spawnItems)
    {
        //GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        groundTile = ObjectPooler.SharedInstance.GetPooledObject("GroundTile");
        if (groundTile != null)
        {
            groundTile.transform.position = nextSpawnPoint;
            groundTile.transform.rotation = Quaternion.identity;
            groundTile.SetActive(true);
        }

        nextSpawnPoint = groundTile.transform.GetChild(1).transform.position;

        if (spawnItems)
        {
            groundTile.GetComponent<GroundTile>().SpawnObstacle();
            groundTile.GetComponent<GroundTile>().SpawnCoins();
        }  
    }

    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            if (i < 2)
            {
                SpawnTile(false);
            }
            else
            {
                SpawnTile(true);
            }
        }
    }
}