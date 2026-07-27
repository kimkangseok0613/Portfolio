using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        // 시작할 때 카메라 참조를 1번만 가져옵니다 (매 프레임 찾기 방지)
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        if (mainCameraTransform == null) return;

        // 카메라의 바라보는 방향(Rotation)을 그대로 따라하도록 설정
        transform.rotation = mainCameraTransform.rotation;
    }
}