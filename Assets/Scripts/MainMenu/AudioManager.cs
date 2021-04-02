using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicMainMenu = null;
    [SerializeField] AudioSource musicLevel1 = null;
    [SerializeField] AudioSource musicLevel2 = null;
    [SerializeField] AudioSource musicLevel3 = null;
  
    public static AudioManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.5f);
        }

        AudioListener.volume = PlayerPrefs.GetFloat("volume");

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (!musicMainMenu.isPlaying && ActiveScene("MainMenu"))
        {
            // musicMainMenu.Play();
        }
        else if (!musicLevel1.isPlaying && ActiveScene("Level1BlockOut"))
        {
            musicLevel1.Play();
        }
        else if (!musicLevel2.isPlaying && ActiveScene("Level2BlockOut"))
        {
            musicLevel2.Play();
        }
        else if (!musicLevel3.isPlaying && ActiveScene("Level3BlockOut"))
        {
            musicLevel3.Play();
        }

        if (musicMainMenu.isPlaying && !ActiveScene("MainMenu"))
        {
            // musicMainMenu.Stop();
        }
        else if (musicLevel1.isPlaying && !ActiveScene("Level1BlockOut"))
        {
            musicLevel1.Stop();
        }
        else if (musicLevel2.isPlaying && !ActiveScene("Level2BlockOut"))
        {
            musicLevel2.Stop();
        }
        else if (musicLevel3.isPlaying && !ActiveScene("Level3BlockOut"))
        {
            musicLevel3.Stop();
        }
    }

    private bool ActiveScene(string sceneName)
    {
        return SceneManager.GetActiveScene().name.Equals(sceneName);
    }
}
