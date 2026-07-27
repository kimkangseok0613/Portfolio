using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float mouseSensitivity = 2f; // 인스펙터에서 1 ~ 5 정도로 조정해보세요!
    public Transform playerBody;

    public static bool canLook = true;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // [인풋렉 방지] 프레임 고정 및 수직동기화 해제 (응답속도 향상)
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120; // 60~144 사이 권장

        RefreshSensitivity();
    }

    public void RefreshSensitivity()
    {
        // 기본값을 2f 정도로 설정 (기존 200f 대신)
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 2f);
    }

    void LateUpdate()
    {
        if (!canLook) return;

        // [핵심] Time.deltaTime을 빼서 인풋렉/밀림 현상 제거!
        // GetAxisRaw 값에 0.1f를 곱해 섬세하게 조절합니다.
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * 0.01f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * 0.01f;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}