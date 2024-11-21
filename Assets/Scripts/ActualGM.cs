using UnityEngine;

public class ActualGM : MonoBehaviour
{

    private Player player;
    private InvaderGrid invaderGrid;


    public string playerName;
    public int activeInvaders { get; private set; } = 0;
    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        invaderGrid = FindFirstObjectByType<InvaderGrid>();
        SetScore(score);
        SetLives(lives);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetLives(int lives)
    {
        this.lives = Mathf.Max(lives, 0);
    }
    private void SetInvaders(int invaderCount)
    {
        this.activeInvaders = invaderCount;
    }

    private void SetScore(int score)
    {
        this.score = score;
    }


    public void OnPlayerKilled(Player player)
    {
        SetLives(lives - 1);
        player.gameObject.SetActive(false);
    }
    public void OnInvaderKilled(Invader invader)
    {
        invader.gameObject.SetActive(false);
        SetInvaders(activeInvaders - 1);

        SetScore(score + invader.score);

        if (activeInvaders == 0)
        {
            Debug.Log("no more invaders. handle");
        }
    }



    public void StartActiveInvaders(int invaderCount)
    {
        SetInvaders(invaderCount);
    }


    public void OnBoundaryReached()
    {
        if (invaderGrid.gameObject.activeSelf)
        {
            invaderGrid.gameObject.SetActive(false);
            OnPlayerKilled(player);
        }
    }
}
