using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioMixer mixer;

    private void Start()
    {
        SetMaster(GameManager.instance.MasterVol);
        SetLevel(GameManager.instance.MusicVol);
        SetSFX(GameManager.instance.SFXVol);
    }
    
    public void SetMaster(float sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        GameManager.instance.MasterVol = sliderValue;
    }
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        GameManager.instance.MusicVol = sliderValue;
    }

    public void SetSFX(float sliderValue)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
        GameManager.instance.SFXVol = sliderValue;
    }
}
