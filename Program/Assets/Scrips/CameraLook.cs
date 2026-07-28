using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;

    public Transform playerBody;


    [Header("Sensitivity UI")]
    public Slider sensitivitySlider;
    public TMP_InputField sensitivityInput;


    public float minSensitivity = 50f;
    public float maxSensitivity = 500f;


    public static bool canLook = true;


    private float xRotation = 0f;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;


        RefreshSensitivity();



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





    // Slider 조절
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






    // 숫자 직접 입력
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
            // 잘못 입력하면 원래 값 복구
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


        transform.localRotation =
            Quaternion.Euler(
                xRotation,
                0f,
                0f
            );


        if (playerBody != null)
        {
            playerBody.Rotate(
                Vector3.up * mouseX
            );
        }
    }
}
