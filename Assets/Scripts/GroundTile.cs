using System.Collections;
using UnityEngine;

public class GroundTile : MonoBehaviour
{

    GroundSpawner groundSpawner;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject obstaclePrefab;

    private void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }
    IEnumerator DelayDisable() 
    {
        yield return new WaitForSeconds(2);
        obstaclePrefab.SetActive(false);
        coinPrefab.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile(true);
        StartCoroutine(DelayDisable());
    
    }

    public void SpawnObstacle()
    {
        // Choose a random point to spawn the obstacle
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        // Spawn the obstace at the position
        //Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
        GameObject obstaclePrefab = ObjectPooler.SharedInstance.GetPooledObject("Obstacle");
        if (obstaclePrefab != null)
        {
            obstaclePrefab.transform.position = spawnPoint.position;
            obstaclePrefab.transform.rotation = Quaternion.identity;
            obstaclePrefab.SetActive(true);
        }


    }


    public void SpawnCoins()
    {
        int coinsToSpawn = 10;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            //GameObject temp = Instantiate(coinPrefab, transform);
            GameObject coinPrefab = ObjectPooler.SharedInstance.GetPooledObject("Coin");
            if (coinPrefab != null)
            {
                coinPrefab.SetActive(true);
                //coinPrefab.transform.parent = transform;

            }
            coinPrefab.transform.position = GetRandomPointInCollider(GetComponent<Collider>());

        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}