using UnityEngine;

public class WeaponSetting : MonoBehaviour
{
    [Header("카메라 기준 위치")]
    public Vector3 position;


    [Header("카메라 기준 회전")]
    public Vector3 rotation;


    [Header("총 크기")]
    public Vector3 scale = Vector3.one;



    void Awake()
    {
        transform.localPosition = position;

        transform.localEulerAngles = rotation;

        transform.localScale = scale;
    }
}