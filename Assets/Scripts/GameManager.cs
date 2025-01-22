using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int hiScore;

    public float MasterVol;
    public float MusicVol;
    public float SFXVol;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("SpaceInvaders");
        }
    }


    [Serializable]
    class GameData
    {
        public int score;
        public int lives;
        public string playerName;
    }


    public void Save()
    {
        // save using name

    }

    // Make a highscore tracker
}
