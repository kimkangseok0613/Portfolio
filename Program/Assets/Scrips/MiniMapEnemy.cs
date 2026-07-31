using UnityEngine;
using UnityEngine.UI;

public class MiniMapEnemy : MonoBehaviour
{
    public GameObject enemyDotPrefab;

    private RectTransform icon;
    private Transform player;

    public float mapScale = 1f;


    void Start()
    {

        // Player 찾기
        GameObject p =
            GameObject.FindGameObjectWithTag("Player");

        if (p != null)
            player = p.transform;


        // 빨간 점 생성
        GameObject dot =
            Instantiate(enemyDotPrefab);


        icon =
            dot.GetComponent<RectTransform>();
    }



    void Update()
    {
        if (player == null || icon == null)
            return;


        Vector3 offset =
            transform.position -
            player.position;


        Vector2 mapPosition =
            new Vector2(
                offset.x,
                offset.z
            );


        icon.anchoredPosition =
            mapPosition * mapScale;
    }
}