using UnityEngine;
using TMPro;

public class DoubleJumpUI : MonoBehaviour
{
    public TextMeshProUGUI text;


    void Start()
    {
        Hide();
    }


    public void Show()
    {
        text.text = "DOUBLE JUMP READY";
        text.gameObject.SetActive(true);
    }


    public void Hide()
    {
        text.gameObject.SetActive(false);
    }
}