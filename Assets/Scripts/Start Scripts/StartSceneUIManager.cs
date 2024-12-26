using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class StartSceneUIManager : MonoBehaviour
{

    public TextMeshProUGUI txtCoins, txtHighScore;
    public GameObject helpScreen,unmuteBtn;
    public AudioClip buttonClick;
    private bool isMuted;

    private AudioSource backgroundMusic;
    private AudioSource UIAudio;
    private DailyTimer timer;



    private void Start()
    {
       
        backgroundMusic = Camera.main.GetComponent<AudioSource>();
        backgroundMusic.time = PlayerPrefs.GetFloat(PlayerControl.MUSIC_TIME, 0f);
        GetMuteStatus();
        UIAudio = GameObject.Find("StartSceneUIManager").GetComponent<AudioSource>();
        timer = new DailyTimer();
        txtCoins.text = PlayerPrefs.GetInt(PlayerControl.PLAYERCOINS, 0).ToString();
        txtHighScore.text =PlayerPrefs.GetInt(PlayerControl.PLAYERHIGHSCORE, 0).ToString();

    }

    public void onPlayButtonClicked() {
        if (!isMuted) {
            UIAudio.PlayOneShot(buttonClick);
        }
        SaveMusicTime();
        SceneManager.LoadScene("GameScene");
        
    }
    public void onCustomizeButtonClicked() {
        if (!isMuted)
        {
            UIAudio.PlayOneShot(buttonClick);
        }
        SaveMusicTime();
        SceneManager.LoadScene("CustomizationScene");
    }
    public void onAchievementButtonClicked() {
        if (!isMuted)
        {
            UIAudio.PlayOneShot(buttonClick);
        }
        SaveMusicTime();
        SceneManager.LoadScene("AchievementScene");
    }
    public void onUpgradesButtonClicked()
    {
        if (!isMuted)
        {
            UIAudio.PlayOneShot(buttonClick);
        }
        SaveMusicTime();
        SceneManager.LoadScene("UpgradeScene");
    }
    public void onMuteButtonClicked()
    {
        if (!isMuted)
        {
            isMuted = true;
            backgroundMusic.mute = true;
            PlayerPrefs.SetInt(PlayerControl.IS_MUTED, 1);
            unmuteBtn.SetActive(true);

        }
        else
        {
            isMuted = false;
            backgroundMusic.mute = false;
            PlayerPrefs.SetInt(PlayerControl.IS_MUTED, 0);
            unmuteBtn.SetActive(false);
        }
    }
    public void onHelpButtonClicked() {
        helpScreen.SetActive(true);
    }
    public void onHelpCloseButtonClicked()
    {
        helpScreen.SetActive(false);
    }





    void GetMuteStatus() {
        int muteStatus = PlayerPrefs.GetInt(PlayerControl.IS_MUTED, 0);
        if (muteStatus == 0)
        {
            isMuted = false;
        }
        else
        {
            isMuted = true;
            backgroundMusic.mute = true;
            unmuteBtn.SetActive(true);

        }
    }
    void SaveMusicTime() {
        PlayerPrefs.SetFloat(PlayerControl.MUSIC_TIME, backgroundMusic.time);
    }

}
