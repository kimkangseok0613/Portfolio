using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionManager : MonoBehaviour
{
    public GameObject optionPanel;


    [Header("Sensitivity")]
    public Slider sensitivitySlider;
    public TMP_InputField sensitivityInput;


    public float defaultSensitivity = 200f;



    void Start()
    {
        // 게임 시작 시 무조건 정상 속도
        Time.timeScale = 1f;


        // 저장된 감도 불러오기
        float sensitivity =
            PlayerPrefs.GetFloat(
                "MouseSensitivity",
                defaultSensitivity
            );


        sensitivitySlider.minValue = 50f;
        sensitivitySlider.maxValue = 500f;


        sensitivitySlider.value = sensitivity;


        sensitivityInput.text =
            sensitivity.ToString("0");



        sensitivitySlider.onValueChanged.AddListener(
            ChangeSlider
        );


        sensitivityInput.onEndEdit.AddListener(
            ChangeInput
        );



        // 시작할 때 옵션창 닫기
        optionPanel.SetActive(false);



        // 마우스 게임 상태
        Cursor.visible = false;

        Cursor.lockState =
            CursorLockMode.Locked;


        CameraLook.canLook = true;
        GunShoot.canShoot = true;
    }





    void Update()
    {
        Debug.Log("TimeScale : " + Time.timeScale);


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOption();
        }
    }






    void ToggleOption()
    {
        bool open =
            !optionPanel.activeSelf;



        optionPanel.SetActive(open);



        if (open)
        {
            // -----------------
            // 게임 정지
            // -----------------

            Time.timeScale = 0f;


            Cursor.visible = true;

            Cursor.lockState =
                CursorLockMode.None;


            CameraLook.canLook = false;

            GunShoot.canShoot = false;
        }
        else
        {
            // -----------------
            // 게임 재개
            // -----------------

            Time.timeScale = 1f;


            Cursor.visible = false;

            Cursor.lockState =
                CursorLockMode.Locked;


            CameraLook.canLook = true;

            GunShoot.canShoot = true;
        }
    }







    void ChangeSlider(float value)
    {
        PlayerPrefs.SetFloat(
            "MouseSensitivity",
            value
        );


        PlayerPrefs.Save();



        sensitivityInput.text =
            value.ToString("0");
    }







    void ChangeInput(string value)
    {
        float number;


        if (float.TryParse(value, out number))
        {
            number =
                Mathf.Clamp(
                    number,
                    50f,
                    500f
                );


            sensitivitySlider.value =
                number;



            PlayerPrefs.SetFloat(
                "MouseSensitivity",
                number
            );


            PlayerPrefs.Save();



            sensitivityInput.text =
                number.ToString("0");
        }
        else
        {
            sensitivityInput.text =
                sensitivitySlider.value.ToString("0");
        }
    }
}