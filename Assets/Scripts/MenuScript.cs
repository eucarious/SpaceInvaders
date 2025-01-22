using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject creditsPanel;

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
        if (settingsPanel.activeSelf) {
            settingsPanel.SetActive(false);
        } else {
            settingsPanel.SetActive(true);
        }
    }
    public void CreditsToggle()
    {
        if (creditsPanel.activeSelf) {
            creditsPanel.SetActive(false);
        } else {
            creditsPanel.SetActive(true);
        }
    }
}
