using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;

    public Transform playerBody;

    public static bool canLook = true;

    float xRotation = 0f;


    void Start()
    {
        Cursor.lockState =
            CursorLockMode.Locked;

        Cursor.visible = false;
    }



    void Update()
    {
        // 옵션창 열려있으면 카메라 정지
        if (!canLook)
        {
            return;
        }


        // 감도 실시간 적용
        mouseSensitivity =
            PlayerPrefs.GetFloat(
                "MouseSensitivity",
                200f
            );



        float mouseX =
            Input.GetAxis("Mouse X")
            *
            mouseSensitivity
            *
            Time.deltaTime;



        float mouseY =
            Input.GetAxis("Mouse Y")
            *
            mouseSensitivity
            *
            Time.deltaTime;



        xRotation -= mouseY;


        xRotation =
            Mathf.Clamp(
                xRotation,
                -90f,
                90f
            );



        transform.localRotation =
            Quaternion.Euler(
                xRotation,
                0f,
                0f
            );



        playerBody.Rotate(
            Vector3.up * mouseX
        );
    }
}