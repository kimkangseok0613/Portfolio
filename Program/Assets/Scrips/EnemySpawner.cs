using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float respawnTime = 2f;

    public void SpawnEnemy(Vector3 position, Quaternion rotation)
    {
        StartCoroutine(Spawn(position, rotation));
    }

    IEnumerator Spawn(Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(respawnTime);

        Instantiate(enemyPrefab, position, rotation);
    }
}