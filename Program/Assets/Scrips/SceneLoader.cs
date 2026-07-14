using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadChooseCharacterScene()
    {
        SceneManager.LoadScene("ChooseCharacter");
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
