using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    // 따라갈 플레이어
    public Transform player;


    // 카메라 높이
    public float height = 20f;



    void LateUpdate()
    {
        if (player == null)
            return;


        transform.position = new Vector3(
            player.position.x,
            height,
            player.position.z
        );
    }
}