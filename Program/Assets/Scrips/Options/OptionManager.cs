using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject optionPanel;

    public Slider sensitivitySlider;

    public InputField sensitivityInput;

    void Start()
    {
        // 게임 시작할 때 항상 감도 200으로 초기화
        PlayerPrefs.SetFloat("MouseSensitivity", 200f);

        sensitivitySlider.value = 200f;
        sensitivityInput.text = "200";

        sensitivitySlider.onValueChanged.AddListener(ChangeSlider);
        sensitivityInput.onEndEdit.AddListener(ChangeInput);

        optionPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool open = !optionPanel.activeSelf;

            optionPanel.SetActive(open);

            Cursor.visible = open;

            Cursor.lockState = open ?
                CursorLockMode.None :
                CursorLockMode.Locked;

            CameraLook.canLook = !open;
        }
    }

    void ChangeSlider(float value)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", value);

        sensitivityInput.text = value.ToString("0");
    }

    void ChangeInput(string value)
    {
        float number;

        if (float.TryParse(value, out number))
        {
            number = Mathf.Clamp(number, 50f, 500f);

            sensitivitySlider.value = number;

            PlayerPrefs.SetFloat("MouseSensitivity", number);

            sensitivityInput.text = number.ToString("0");
        }
    }
}