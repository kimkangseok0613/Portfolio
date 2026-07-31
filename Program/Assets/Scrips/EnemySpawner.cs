using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform player;

    [Header("생성 거리")]
    public float spawnRadius = 10f;

    [Header("생성 높이")]
    public float spawnHeight = 1f;

    [Header("재생성 시간")]
    public float respawnDelay = 2f;


    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");

            if (p != null)
                player = p.transform;
        }

        SpawnEnemy();
    }


    public void SpawnEnemy()
    {
        if (player == null)
        {
            Debug.LogError("Player 없음");
            return;
        }


        // 플레이어 주변 랜덤 X,Z
        Vector3 randomPosition =
            player.position +
            new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0,
                Random.Range(-spawnRadius, spawnRadius)
            );


        // 높이 고정
        randomPosition.y = spawnHeight;


        Instantiate(
            enemyPrefab,
            randomPosition,
            Quaternion.identity
        );
    }


    public void RespawnEnemy()
    {
        StartCoroutine(RespawnCoroutine());
    }


    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        SpawnEnemy();
    }
}