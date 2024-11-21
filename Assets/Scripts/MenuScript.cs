using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject settingsPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SpaceInvaders");
    }

    public void SettingsToggle()
    {
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
        } 
        else
        {
            settingsPanel.SetActive(true);
        }
    }
}
