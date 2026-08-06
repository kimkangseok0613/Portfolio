using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("게임 시간")]
    public float gameTime = 180f;

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text killText;

    public GameObject gameOverPanel;
    public TMP_Text resultText;

    private int killCount;
    private bool gameEnded;

    public bool IsGameEnded => gameEnded;

    private float totalTime;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        totalTime = gameTime;

        Time.timeScale = 1;

        gameOverPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UpdateKillUI();
    }

    void Update()
    {
        if (gameEnded)
            return;

        gameTime -= Time.deltaTime;

        if (gameTime < 0)
            gameTime = 0;

        int min = Mathf.FloorToInt(gameTime / 60);
        int sec = Mathf.FloorToInt(gameTime % 60);

        timerText.text = $"{min:00}:{sec:00}";

        if (gameTime <= 0)
        {
            EndGame();
        }
    }

    public void AddKill()
    {
        killCount++;
        UpdateKillUI();
    }

    void UpdateKillUI()
    {
        if (killText != null)
            killText.text = $"Kill : {killCount}";
    }

    public void EndGame()
    {
        if (gameEnded)
            return;

        gameEnded = true;

        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        CameraLook.canLook = false;
        GunShoot.canShoot = false;

        gameOverPanel.SetActive(true);

        float survive = totalTime - gameTime;

        int min = Mathf.FloorToInt(survive / 60);
        int sec = Mathf.FloorToInt(survive % 60);

        resultText.text =
            $"Game Over\n\nTime Survived : {min:00}:{sec:00}\nKills : {killCount}";
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}