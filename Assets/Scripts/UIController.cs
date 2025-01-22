using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public ActualGM gm;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text hiScoreText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private Sprite[] livesSprites;
    [SerializeField] private Image livesImg;

    [SerializeField] private TMP_Text GOscore;
    [SerializeField] private TMP_Text GOHiScore;
    [SerializeField] private TMP_Text GOKilled;

    private bool paused;
    [SerializeField] private GameObject pauseScreen;
 //   [SerializeField] private TMP_Text GOCobia;

    void Update()
    {
        livesText.text = gm.lives.ToString();
        livesImg.sprite = livesSprites[gm.lives];

        scoreText.text = "Score\n" + gm.score.ToString("00000");
        hiScoreText.text = "Hi-score\n" + gm.hiScore.ToString("00000");


        GOscore.text = "Final score\n" + gm.score.ToString("00000");
        if (gm.isHighscore)
        {
            GOHiScore.text = "New Highscore!";
        }
        else
        {
            GOHiScore.text = "";
        }
        GOKilled.text = "Scared off\n" + gm.invadersKilled.ToString() + "\n Invaders";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc pressed");
            if (!gm.gameOver)
                Pause();
        }
    }

    public void Pause()
    {
        if (!paused)
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            paused = true;
        }         
    }

    public void GoToMenu()
    {
        Unpause();
        SceneManager.LoadScene("Menu");
    }

    public void Unpause()
    {
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(false);
        paused = false;
    }
}
