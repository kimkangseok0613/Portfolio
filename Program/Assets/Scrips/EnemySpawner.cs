using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("적 프리팹 (Zombie1)")]
    public GameObject enemyPrefab;

    [Header("플레이어")]
    public Transform player;

    [Header("생성 거리")]
    public float minSpawnDistance = 12f;
    public float maxSpawnDistance = 18f;

    [Header("생성 높이")]
    public float spawnHeight = 1f;

    [Header("생성 간격")]
    public float spawnDelay = 2f;

    [Header("최대 적 수")]
    public int maxEnemyCount = 7;

    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");

            if (p != null)
                player = p.transform;
        }

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            // 게임 종료 시 생성 중지
            if (GameManager.Instance != null &&
                GameManager.Instance.IsGameEnded)
            {
                yield break;
            }

            // 죽은 적 제거
            enemies.RemoveAll(enemy => enemy == null);

            // 최대 수 이하일 때만 생성
            if (enemies.Count < maxEnemyCount)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy Prefab이 없습니다.");
            return;
        }

        if (player == null)
            return;

        Vector2 randomDir = Random.insideUnitCircle.normalized;

        float distance =
            Random.Range(minSpawnDistance, maxSpawnDistance);

        Vector3 spawnPosition =
            player.position +
            new Vector3(randomDir.x, 0, randomDir.y) * distance;

        spawnPosition.y = spawnHeight;

        GameObject enemy =
            Instantiate(
                enemyPrefab,
                spawnPosition,
                Quaternion.identity);

        enemies.Add(enemy);
    }
}