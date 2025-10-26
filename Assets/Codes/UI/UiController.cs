using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;

    private int scoreValue = 0;

    [Header("Hearts")]
    [SerializeField] private Image[] heartSprites;

    void Awake() => instance = this;

    void Update()
    {
        if (Time.time > 1)
        {
            timerText.text = Time.time.ToString("#,#");
        }
    }

    public void AddScore()
    {
        scoreValue++;
        scoreText.text = scoreValue.ToString();
    }

    public void DisableHeart(int index)
    {
        heartSprites[index].gameObject.SetActive(false);
    }

    public void CheckGameOver()
    {

    }

    public void EnableGameOverButton()
    {
        gameOverPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
