using UnityEngine;
using UnityEngine.Audio;

public class ActualGM : MonoBehaviour
{

    [SerializeField] private GameObject gameOverUI;
    private Player player;
    private InvaderGrid invaderGrid;
    public AudioSource audioSource;
    public AudioResource[] audioResources;
    public Bunker[] bunkers;

    public string playerName;
    public int activeInvaders { get; private set; } = 0;
    public int score { get; private set; } = 0;
    public int hiScore { get; private set; }
    public int lives { get; private set; } = 3;

    public int invadersKilled = 0;
    public bool isHighscore = false;
    public bool gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        invaderGrid = FindFirstObjectByType<InvaderGrid>();
        hiScore = GameManager.instance.hiScore;
        gameOver = false;
        audioSource.resource = audioResources[2];
        audioSource.Play();
    }

    public void NewGame()
    {
        gameOverUI.SetActive(false);
        invadersKilled = 0;
        SetScore(0);
        SetLives(3);
        gameOver = false;

        foreach (var bunker in bunkers)
        {
            bunker.ResetBunker();
        }

        NewRound();
    }
    private void NewRound()
    {
        invaderGrid.ResetInvaders();
        invaderGrid.gameObject.SetActive(true);
        
        Respawn();
    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }
    private void GameOver()
    {
        gameOver = true;
        if (score > hiScore) {
            hiScore = score;
            GameManager.instance.hiScore = score;
            isHighscore = true;
        } else { 
            isHighscore = false;
        }
        
        audioSource.resource = audioResources[0];
        audioSource.Play();
        gameOverUI.SetActive(true);
        invaderGrid.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        this.lives = Mathf.Max(lives, 0);
    }
    private void SetScore(int score)
    {
        this.score = score;
    }
    private void SetInvaders(int invaderCount)
    {
        this.activeInvaders = invaderCount;
    }



    public void OnPlayerKilled(Player player)
    {

        SetLives(lives - 1);
        player.gameObject.SetActive(false);

        if (lives > 0)
        {
            Invoke(nameof(Respawn), 1f);
        } else
        {
            GameOver();
        }
    }

    public void OnInvaderKilled(Invader invader)
    {
        invader.gameObject.SetActive(false);
        invadersKilled += 1;
        SetInvaders(activeInvaders - 1);

        SetScore(score + invader.score);

        if (activeInvaders == 0)
        {
            SetScore(score + 150);
            Invoke(nameof(NewRound), 1f);
        }
    }
    public void OnInvaderKilledNon(Invader invader)
    {
        invader.gameObject.SetActive(false);
        invadersKilled += 1;
        SetInvaders(activeInvaders - 1);
        if (activeInvaders == 0)
        {
            Invoke(nameof(NewRound), 1f);
        }
    }


    public void StartActiveInvaders(int invaderCount)
    {
        audioSource.resource = audioResources[1];
        audioSource.Play();
        SetInvaders(invaderCount);
    }


    public void OnBoundaryReached()
    {
        if (invaderGrid.gameObject.activeSelf)
        {
            invaderGrid.gameObject.SetActive(false);
            NewRound();
        }
    }
}
