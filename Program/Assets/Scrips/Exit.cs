using UnityEngine;

public class Exit : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
