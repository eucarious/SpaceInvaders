using TMPro;
using UnityEngine;


public class UIController : MonoBehaviour
{
    public ActualGM gm;


    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = gm.lives.ToString();
        scoreText.text = "Score\n" + gm.score.ToString("0000");
    }
}
