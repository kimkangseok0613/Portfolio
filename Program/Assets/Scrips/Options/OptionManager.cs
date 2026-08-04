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

    [Header("Sound")]
    public Slider soundSlider;
    public TMP_InputField soundInput;

    public float defaultSoundVolume = 100f;

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

        // 저장된 소리 크기 불러오기
        float soundVolume =
            PlayerPrefs.GetFloat(
                "SoundVolume",
                defaultSoundVolume
            );


        soundSlider.minValue = 0f;
        soundSlider.maxValue = 100f;


        soundSlider.value = soundVolume;


        soundInput.text =
            soundVolume.ToString("0");


        // 슬라이더 이벤트
        soundSlider.onValueChanged.AddListener(
            ChangeSoundSlider
        );


        // 입력 이벤트
        soundInput.onEndEdit.AddListener(
            ChangeSoundInput
        );


        // 실제 게임 볼륨 적용
        AudioListener.volume =
            soundVolume / 100f;
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

    void ChangeSoundSlider(float value)
    {
        PlayerPrefs.SetFloat(
            "SoundVolume",
            value
        );

        PlayerPrefs.Save();


        AudioListener.volume =
            value / 100f;


        soundInput.text =
            value.ToString("0");
    }

    void ChangeSoundInput(string value)
    {
        float number;


        if (float.TryParse(value, out number))
        {
            number =
                Mathf.Clamp(
                    number,
                    0f,
                    100f
                );


            soundSlider.value =
                number;


            PlayerPrefs.SetFloat(
                "SoundVolume",
                number
            );


            PlayerPrefs.Save();


            AudioListener.volume =
                number / 100f;


            soundInput.text =
                number.ToString("0");
        }
        else
        {
            soundInput.text =
                soundSlider.value.ToString("0");
        }
    }
}