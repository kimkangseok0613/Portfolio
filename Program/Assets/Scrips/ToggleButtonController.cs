using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Toggle toggle;
    public Button button;

    void Start()
    {
        button.interactable = toggle.isOn;

        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        button.interactable = isOn;
    }
}
