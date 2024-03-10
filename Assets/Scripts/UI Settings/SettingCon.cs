using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingCon : MonoBehaviour
{

    public Slider soundslider, musicslider;
    public AudioSource soundSource, musicSource;
    // Start is called before the first frame update
    void Start()
    {
        soundslider.value = PlayerPrefs.GetFloat("sv");
        musicslider.value = PlayerPrefs.GetFloat("mv");
        soundSource.volume = soundslider.value;
        musicSource.volume = musicslider.value;
    }

    private void OnEnable()
    {
      //  soundslider.value=PlayerPrefs.GetFloat("sv");
      //  musicslider.value = PlayerPrefs.GetFloat("mv");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSound()
    {
        soundSource.volume = soundslider.value;
        PlayerPrefs.SetFloat("sv", soundslider.value);
    }
    public void ChangeMusic()
    {
        musicSource.volume = musicslider.value;
        PlayerPrefs.SetFloat("mv", musicslider.value);
    }
}
