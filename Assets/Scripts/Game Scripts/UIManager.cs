using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI txtScore, txtCoins, txtHealth, txtBullets, txtRockets, txtPowerUp, txtGOScore, txtGOCoins,txtPauseHighscore;
    public GameObject titleScreen,gameOverScreen,inGameScreen,pauseScreen,helpScreen,MultiplierImg,IntangibleImg,InfiniteImg,muteBtn,unmuteBtn;
    public bool isGameOver=false;
    private bool isMuted;
    public Slider healthSlider;
    private AudioSource UIAudio;
    public AudioClip buttonClick;
     void Start()
     {
        isMuted = PlayerControl.isMuted;
        UIAudio = GameObject.Find("UIManager").GetComponent<AudioSource>();
        Time.timeScale = 0;
        UpdateScore(0);
        UpdateCoins(0);
        UpdateHealth(100);
        UpdatePowerUp("", Color.white);
        OnPlayButtonClicked();
     }
    public void OnPlayButtonClicked() {
        titleScreen.SetActive(false);
        inGameScreen.SetActive(true);
        //PlayButtonClickSound();
        Time.timeScale = 1;
    }
    public void DisplayGameOver(int score, int coins) {
        gameOverScreen.SetActive(true);
        inGameScreen.SetActive(false);
        txtGOScore.text = "Score : " + score.ToString();
        txtGOCoins.text = "Coins : " + coins.ToString();
        isGameOver = true;
    }
    public void OnRestartButtonClicked() {
        PlayButtonClickSound();
        SaveMusicTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void onPauseButtonClicked() {
        PlayButtonClickSound();
        pauseScreen.SetActive(true);
        txtPauseHighscore.text = PlayerPrefs.GetInt(PlayerControl.PLAYERHIGHSCORE, 0).ToString();
        inGameScreen.SetActive(false);
        if (isMuted)
        {
            unmuteBtn.SetActive(true);
            muteBtn.SetActive(false);
        }
        else {
            unmuteBtn.SetActive(false);
            muteBtn.SetActive(true);
        }
        Time.timeScale = 0;
    }
    public void onResumeButtonClicked() {
        PlayButtonClickSound();
        pauseScreen.SetActive(false);
        inGameScreen.SetActive(true);
        Time.timeScale = 1;
    }
    public void onPauseMainMenuButtonClicked() {
        PlayButtonClickSound();
        SaveMusicTime();
        SceneManager.LoadScene("StartScene");
    }
    public void onMuteBtnClicked() {
        
       
        if (!isMuted)
        {
            PlayerControl.isMuted = true;
            muteBtn.SetActive(false);
            unmuteBtn.SetActive(true);
            isMuted = true;
            PlayerPrefs.SetInt(PlayerControl.IS_MUTED, 1);
            PlayerControl.PlayerMute();
        }
        else
        {
            PlayerControl.isMuted = false;
            muteBtn.SetActive(true);
            unmuteBtn.SetActive(false);
            isMuted = false;
            PlayerPrefs.SetInt(PlayerControl.IS_MUTED, 0);
            PlayerControl.PlayerMute();
        }
    }



    public void onMainMenuButtonClicked()
    {
        SaveMusicTime();
        SceneManager.LoadScene("StartScene");
    }
    public void UpdateScore(int score) { txtScore.text = "Score : " + score.ToString(); }
    public void UpdateCoins(int coins) { txtCoins.text =  coins.ToString(); }
    public void UpdateHealth(int health) 
    { 
        txtHealth.text = health.ToString() + "%";
        healthSlider.value = health;
    }
    public void UpdateBullets(int bullets) { txtBullets.text = "x " + bullets.ToString(); }
    public void UpdateRockets(int rockets) { txtRockets.text = "x " + rockets.ToString(); }
    public void UpdatePowerUp(string powerup, Color color) 
    { 
        txtPowerUp.text = powerup; 
        txtPowerUp.color = color;
        if (powerup == PlayerControl.MULTIPLIER)
        {
            MultiplierImg.SetActive(true);
            InfiniteImg.SetActive(false);
            IntangibleImg.SetActive(false);
        }
        else if (powerup == PlayerControl.INFINITEAMMO)
        {
            MultiplierImg.SetActive(false);
            InfiniteImg.SetActive(true);
            IntangibleImg.SetActive(false);
        }
        else if (powerup == PlayerControl.INTANGIBLE)
        {
            MultiplierImg.SetActive(false);
            InfiniteImg.SetActive(false);
            IntangibleImg.SetActive(true);
        }
        else
        {
            MultiplierImg.SetActive(false);
            InfiniteImg.SetActive(false);
            IntangibleImg.SetActive(false);
        }
    }

    void PlayButtonClickSound()
    {
        if (!isMuted)
        {
            UIAudio.PlayOneShot(buttonClick);
        }
    }
    void SaveMusicTime()
    {
        PlayerPrefs.SetFloat(PlayerControl.MUSIC_TIME, Camera.main.GetComponent<AudioSource>().time);
    }


}
