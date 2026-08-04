// CameraLook.cs

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraLook : MonoBehaviour
{
    [Header("Recoil")]

    private float currentVerticalRecoil;
    private float currentHorizontalRecoil;

    private float recoilReturnSpeed = 10f;

    private float recoilReset;

    public float mouseSensitivity = 200f;

    public Transform playerBody;


    [Header("Sensitivity UI")]
    public Slider sensitivitySlider;
    public TMP_InputField sensitivityInput;


    public float minSensitivity = 50f;
    public float maxSensitivity = 500f;


    public static bool canLook = true;


    private float xRotation = 0f;



    [Header("Zoom Settings")]
    public Camera playerCamera;

    public float normalFOV = 103f;

    public float zoomFOV = 85f;

    public float sniperZoomFOV = 60f;

    public float zoomSpeed = 10f;


    private float currentVelocity;




    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;


        RefreshSensitivity();



        if (playerCamera == null)
        {
            playerCamera = GetComponent<Camera>();
        }


        if (playerCamera != null)
        {
            normalFOV = playerCamera.fieldOfView;
        }



        // Slider 설정
        if (sensitivitySlider != null)
        {
            sensitivitySlider.minValue = minSensitivity;
            sensitivitySlider.maxValue = maxSensitivity;

            sensitivitySlider.value = mouseSensitivity;


            sensitivitySlider.onValueChanged.AddListener(
                ChangeSensitivitySlider
            );
        }



        // InputField 설정
        if (sensitivityInput != null)
        {
            sensitivityInput.text =
                mouseSensitivity.ToString();


            sensitivityInput.onEndEdit.AddListener(
                ChangeSensitivityInput
            );
        }
    }







    public void RefreshSensitivity()
    {
        mouseSensitivity =
            PlayerPrefs.GetFloat(
                "MouseSensitivity",
                200f
            );
    }



    public void AddRecoil(float vertical, float horizontal)
    {
        currentVerticalRecoil += vertical;

        currentHorizontalRecoil +=
            Random.Range(-horizontal, horizontal);
    }



    public void ChangeSensitivitySlider(float value)
    {
        mouseSensitivity = value;


        SaveSensitivity();



        if (sensitivityInput != null)
        {
            sensitivityInput.text =
                Mathf.RoundToInt(value).ToString();
        }
    }







    public void ChangeSensitivityInput(string value)
    {
        float result;


        if (float.TryParse(value, out result))
        {
            result =
                Mathf.Clamp(
                    result,
                    minSensitivity,
                    maxSensitivity
                );


            mouseSensitivity = result;


            SaveSensitivity();



            if (sensitivitySlider != null)
            {
                sensitivitySlider.value =
                    result;
            }


            sensitivityInput.text =
                Mathf.RoundToInt(result).ToString();
        }
        else
        {
            sensitivityInput.text =
                Mathf.RoundToInt(mouseSensitivity).ToString();
        }
    }







    void SaveSensitivity()
    {
        PlayerPrefs.SetFloat(
            "MouseSensitivity",
            mouseSensitivity
        );


        PlayerPrefs.Save();
    }








    void LateUpdate()
    {

        if (!canLook)
            return;



        // 마우스 회전

        float mouseX =
            Input.GetAxisRaw("Mouse X")
            * mouseSensitivity
            * 0.01f;


        float mouseY =
            Input.GetAxisRaw("Mouse Y")
            * mouseSensitivity
            * 0.01f;



        xRotation -= mouseY;


        xRotation =
            Mathf.Clamp(
                xRotation,
                -90f,
                90f
            );



        currentVerticalRecoil =
Mathf.Lerp(
    currentVerticalRecoil,
    0,
    recoilReturnSpeed * Time.deltaTime
);


        currentHorizontalRecoil =
        Mathf.Lerp(
            currentHorizontalRecoil,
            0,
            recoilReturnSpeed * Time.deltaTime
        );


        transform.localRotation =
        Quaternion.Euler(
            xRotation - currentVerticalRecoil,
            currentHorizontalRecoil,
            0
        );



        if (playerBody != null)
        {
            playerBody.Rotate(
                Vector3.up * mouseX
            );
        }



        // 줌 처리

        Zoom();
        
    }








    void Zoom()
    {
        if (playerCamera == null)
            return;


        float targetFOV = normalFOV;


        // 총을 들고 있을 때만 줌 가능
        if (WeaponManager.Instance != null &&
            WeaponManager.Instance.currentWeaponIndex != -1)
        {
            if (Input.GetMouseButton(1))
            {
                targetFOV = zoomFOV;
            }
        }


        playerCamera.fieldOfView =
            Mathf.SmoothDamp(
                playerCamera.fieldOfView,
                targetFOV,
                ref currentVelocity,
                1f / zoomSpeed
            );
    }

    public void AddRecoil(
    float vertical,
    float horizontal,
    float recovery)
    {
        currentVerticalRecoil += vertical;

        currentVerticalRecoil =
            Mathf.Clamp(
                currentVerticalRecoil,
                0,
                15f
            );


        currentHorizontalRecoil +=
            Random.Range(
                -horizontal,
                horizontal
            );


        recoilReturnSpeed =
            recovery;
    }

}