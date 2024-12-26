using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CustomizationUIManager : MonoBehaviour
{
    public GameObject playerBody;
    public static string PLAYER_COLOR = "PLAYERCOLOR";

    public AudioClip buttonClick;
    private bool isMuted;
    private AudioSource backgroundMusic;
    private AudioSource UIAudio;
    public Material[] playerColor;

    private void Start()
    {
        playerBody.GetComponent<MeshRenderer>().material = playerColor[PlayerPrefs.GetInt(PLAYER_COLOR, 0)];
        backgroundMusic = Camera.main.GetComponent<AudioSource>();
        backgroundMusic.time = PlayerPrefs.GetFloat(PlayerControl.MUSIC_TIME, 0f);
        GetMuteStatus();
        UIAudio = GameObject.Find("CustomizationUIManager").GetComponent<AudioSource>();
    }

    public void oncolorButtonPressed(Material material)
    {
        PlayButtonClick();
        playerBody.GetComponent<MeshRenderer>().material = material;
    }
    public void setIndexOnButtonClicked(int colorIndex)
    {
        PlayerPrefs.SetInt(PLAYER_COLOR, colorIndex);
    }
    public void onCloseButtonClicked(){
        PlayButtonClick();
        SaveMusicTime();
        SceneManager.LoadScene("StartScene"); 
    }


    void GetMuteStatus()
    {
        int muteStatus = PlayerPrefs.GetInt(PlayerControl.IS_MUTED, 0);
        if (muteStatus == 0)
        {
            isMuted = false;
        }
        else
        {
            isMuted = true;
            backgroundMusic.mute = true;

        }
    }
    void PlayButtonClick()
    {
        if (!isMuted)
        {
            UIAudio.PlayOneShot(buttonClick);
        }
    }
    void SaveMusicTime()
    {
        PlayerPrefs.SetFloat(PlayerControl.MUSIC_TIME, backgroundMusic.time);
    }

}
