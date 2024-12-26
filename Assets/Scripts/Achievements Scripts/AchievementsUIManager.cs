using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using TMPro;
using System;
public class AchievementsUIManager : MonoBehaviour
{
    public TextMeshProUGUI txtTankAch, txtCoinAch, txtBulletAch, txtRocketAch, txtScoreAch, txtPowerAch;
    public TextMeshProUGUI txtTankAchBtn, txtRocketAchBtn, txtBulletAchBtn, txtScoreAchBtn, txtPowerAchBtn, txtCoinAchBtn;
    public TextMeshProUGUI playerCoins;
    public GameObject[] starTankAch, starCoinAch, starBulletAch, starRocketAch, starScoreAch, starPowerAch;
    public GameObject btnTankAch, btnCoinAch, btnBulltAch, btnRocketAch, btnScoreAch, btnPowerAch;

    public GameObject DailyChallengesWindow;
    public GameObject btnC1, btnC2, btnC3, completeC1, completeC2, completeC3;
    public TextMeshProUGUI txtC1, txtC2, txtC3, txtC1Prog, txtC2Prog, txtC3Prog;


    public AudioClip buttonClick;

    private AudioSource backgroundMusic;
    private AudioSource UIAudio;
    private bool isMuted;


    private void Start()
    {
        backgroundMusic = Camera.main.GetComponent<AudioSource>();
        backgroundMusic.time = PlayerPrefs.GetFloat(PlayerControl.MUSIC_TIME, 0f);
        UIAudio = GameObject.Find("AchievementsUIManager").GetComponent<AudioSource>();
        GetMuteStatus();
        HandleChallenges();
        RefreshAchievements();
        HandleBulletAchievement();
        HandleTankAchievement();
        HandleRocketAchievement();
        HandleScoreAchievement();
        HandleCoinAchievement();
        HandlePowerAchievement();
        UpdateCoins();

    }

    private void RefreshAchievements(){
        Achievements.AddBulletsFired(0);
        Achievements.AddCoinsCollected(0);
        Achievements.AddPointsScored(0);
        Achievements.AddPowerGained(0);
        Achievements.AddRocketsLaunched(0);
        Achievements.AddTanksDestroyed(0);
    }

    private void HandleChallenges()
    {
       
        string[] challenges = Challenges.LoadChallengeDetails();
        txtC1.text = challenges[0];
        txtC2.text = challenges[1];
        txtC3.text = challenges[2];

        int c1Prog = Challenges.ChallengeProgress(PlayerPrefs.GetString(Challenges.CHALLENGE_1_TYPE));
        int c1Count =PlayerPrefs.GetInt(Challenges.CHALLENGE_1_COUNT);
        if (c1Prog < c1Count)
        {
            txtC1Prog.text = c1Prog.ToString();
        }
        else {
            completeC1.SetActive(true);
            
            txtC1Prog.text = "";
            if (PlayerPrefs.GetInt(Challenges.CHALLENGE_1_STATUS)==0)
            {
                btnC1.SetActive(true);
            }
        }

        int c2Prog = Challenges.ChallengeProgress(PlayerPrefs.GetString(Challenges.CHALLENGE_2_TYPE));
        int c2Count = PlayerPrefs.GetInt(Challenges.CHALLENGE_2_COUNT);
        if (c2Prog < c2Count)
        {   
            txtC2Prog.text = c2Prog.ToString();
        }
        else
        {
            completeC2.SetActive(true);
            txtC2Prog.text = "";
            if (PlayerPrefs.GetInt(Challenges.CHALLENGE_2_STATUS,0)==0)
            {
                btnC2.SetActive(true);  
            }
        }

        int c3Prog = Challenges.ChallengeProgress(PlayerPrefs.GetString(Challenges.CHALLENGE_3_TYPE));
        int c3Count = PlayerPrefs.GetInt(Challenges.CHALLENGE_3_COUNT);
        if (c3Prog < c3Count)
        {
            txtC3Prog.text = c3Prog.ToString();
        }
        else
        {
            completeC3.SetActive(true);           
            txtC3Prog.text = "";
            if (PlayerPrefs.GetInt(Challenges.CHALLENGE_3_STATUS,0)==0)
            {
                btnC3.SetActive(true);
            }
            
        }


       

    }
  

    void GetTankAchCollection() {
        if (PlayerPrefs.GetInt(Achievements.TANKS_ACH_STATUS, 0) < PlayerPrefs.GetInt(Achievements.TANKS_ACH_LEVEL))
        {
            btnTankAch.SetActive(true);
            txtTankAchBtn.text = "x " + getReward(PlayerPrefs.GetInt(Achievements.TANKS_ACH_STATUS, 0) + 1).ToString();
        }
    }
    void GetScoreAchCollection()
    {
        if (PlayerPrefs.GetInt(Achievements.POINTS_ACH_STATUS, 0) < PlayerPrefs.GetInt(Achievements.POINTS_ACH_LEVEL))
        {
            btnScoreAch.SetActive(true);
            txtScoreAchBtn.text = "x " + getReward(PlayerPrefs.GetInt(Achievements.POINTS_ACH_STATUS, 0) + 1).ToString();
        }
    }
    void GetCoinAchCollection()
    {
        if (PlayerPrefs.GetInt(Achievements.COINS_ACH_STATUS, 0) < PlayerPrefs.GetInt(Achievements.COINS_ACH_LEVEL))
        {
            btnCoinAch.SetActive(true);
            txtCoinAchBtn.text = "x " + getReward(PlayerPrefs.GetInt(Achievements.COINS_ACH_STATUS, 0) + 1).ToString();
        }
    }
    void GetBulletAchCollection()
    {
        if (PlayerPrefs.GetInt(Achievements.BULLETS_ACH_STATUS, 0) < PlayerPrefs.GetInt(Achievements.BULLETS_ACH_LEVEL))
        {
            btnBulltAch.SetActive(true);
            txtBulletAchBtn.text = "x " + getReward(PlayerPrefs.GetInt(Achievements.BULLETS_ACH_STATUS, 0) + 1).ToString();
        }
    }
    void GetRocketAchCollection()
    {
        if (PlayerPrefs.GetInt(Achievements.ROCKETS_ACH_STATUS, 0) < PlayerPrefs.GetInt(Achievements.ROCKETS_ACH_LEVEL))
        {
            btnRocketAch.SetActive(true);
            txtRocketAchBtn.text = "x " + getReward(PlayerPrefs.GetInt(Achievements.ROCKETS_ACH_STATUS, 0) + 1).ToString();
        }
    }
    void GetPowerAchCollection()
    {
        if (PlayerPrefs.GetInt(Achievements.POWERS_ACH_STATUS, 0) < PlayerPrefs.GetInt(Achievements.POWERS_ACH_LEVEL))
        {
            btnPowerAch.SetActive(true);
            txtPowerAchBtn.text = "x " + getReward(PlayerPrefs.GetInt(Achievements.POWERS_ACH_STATUS, 0) + 1).ToString();
        }
    }

    void HandleTankAchievement()
    {
        if (PlayerPrefs.GetInt(Achievements.TANKS_ACH_LEVEL) < 3)
        {
            txtTankAch.text = PlayerPrefs.GetInt(Achievements.TANKS_DESTROYED).ToString() + "/" + Achievements.tanksNeeded[PlayerPrefs.GetInt(Achievements.TANKS_ACH_LEVEL)].ToString() + " Tanks Destroyed";
        }
        else
        {
            txtTankAch.text = "Completed!";
        }
        for (int i = 0; i < 3; i++)
        {
            if (i < PlayerPrefs.GetInt(Achievements.TANKS_ACH_LEVEL, 0))
            {
                starTankAch[i].SetActive(true);
            }
        }
        GetTankAchCollection();
    }
    void HandleCoinAchievement()
    {
        if (PlayerPrefs.GetInt(Achievements.COINS_ACH_LEVEL) < 3)
        {
            txtCoinAch.text = PlayerPrefs.GetInt(Achievements.COINS_COLLECTED).ToString() + "/" + Achievements.coinsNeeded[PlayerPrefs.GetInt(Achievements.COINS_ACH_LEVEL)].ToString() + " Coins Collected";
        }
        else
        {
            txtCoinAch.text = "Completed!";
        }
        for (int i = 0; i < 3; i++)
        {
            if (i < PlayerPrefs.GetInt(Achievements.COINS_ACH_LEVEL, 0))
            {
                starCoinAch[i].SetActive(true);
            }
        }
        GetCoinAchCollection();
    }
    void HandleBulletAchievement()
    {
        if (PlayerPrefs.GetInt(Achievements.BULLETS_ACH_LEVEL) < 3)
        {
            txtBulletAch.text = PlayerPrefs.GetInt(Achievements.BULLETS_FIRED).ToString() + "/" + Achievements.bulletsNeeded[PlayerPrefs.GetInt(Achievements.BULLETS_ACH_LEVEL)].ToString() + " Bullets Fired";
        }
        else
        {
            txtBulletAch.text = "Completed!";
        }
        for (int i = 0; i < 3; i++)
        {
            if (i < PlayerPrefs.GetInt(Achievements.BULLETS_ACH_LEVEL, 0))
            {
                starBulletAch[i].SetActive(true);
            }
        }

        GetBulletAchCollection();


    }
    void HandleRocketAchievement()
    {
        if (PlayerPrefs.GetInt(Achievements.ROCKETS_ACH_LEVEL) < 3)
        {
            txtRocketAch.text = PlayerPrefs.GetInt(Achievements.ROCKETS_LAUNCHED).ToString() + "/" + Achievements.rocketsNeeded[PlayerPrefs.GetInt(Achievements.ROCKETS_ACH_LEVEL)].ToString() + " Rockets Launched";
        }
        else
        {
            txtRocketAch.text = "Completed!";
        }
        for (int i = 0; i < 3; i++)
        {
            if (i < PlayerPrefs.GetInt(Achievements.ROCKETS_ACH_LEVEL, 0))
            {
                starRocketAch[i].SetActive(true);
            }
        }

        GetRocketAchCollection();
    }
    void HandleScoreAchievement()
    {
        if (PlayerPrefs.GetInt(Achievements.POINTS_ACH_LEVEL) < 3)
        {
            txtScoreAch.text = PlayerPrefs.GetInt(Achievements.POINTS_SCORED).ToString() + "/" + Achievements.scoreNeeded[PlayerPrefs.GetInt(Achievements.POINTS_ACH_LEVEL)].ToString() + " Points Scored";
        }
        else
        {
            txtScoreAch.text = "Completed!";
        }
        for (int i = 0; i < 3; i++)
        {
            if (i < PlayerPrefs.GetInt(Achievements.POINTS_ACH_LEVEL, 0))
            {
                starScoreAch[i].SetActive(true);
            }
        }

        GetScoreAchCollection();
    }
    void HandlePowerAchievement()
    {
        if (PlayerPrefs.GetInt(Achievements.POWERS_ACH_LEVEL) < 3)
        {
            txtPowerAch.text = PlayerPrefs.GetInt(Achievements.POWERS_GAINED).ToString() + "/" + Achievements.powerNeeded[PlayerPrefs.GetInt(Achievements.POWERS_ACH_LEVEL)].ToString() + " Powerups Gained";
        }
        else
        {
            txtPowerAch.text = "Completed!";
        }
        for (int i = 0; i < 3; i++)
        {
            if (i < PlayerPrefs.GetInt(Achievements.POWERS_ACH_LEVEL, 0))
            {
                starPowerAch[i].SetActive(true);
            }
        }
        GetPowerAchCollection();     
    }

    int getReward(int level) {

        if (level == 1)
        {
            return 100;
        }
        else if (level == 2)
        {
            return 500;
        }
        else if (level == 3)
        {
            return 1000;
        }
        else {
            return 0;
        }

    }
    void AddCoins(int coins) {
        int currentCoins = PlayerPrefs.GetInt(PlayerControl.PLAYERCOINS, 0);
        if (currentCoins == 0)
        {
            PlayerPrefs.SetInt(PlayerControl.PLAYERCOINS, coins);
        }
        else {
            PlayerPrefs.SetInt(PlayerControl.PLAYERCOINS, coins + currentCoins);
        }
        UpdateCoins();
    }
    void UpdateCoins() {
        playerCoins.text = PlayerPrefs.GetInt(PlayerControl.PLAYERCOINS, 0).ToString();
    }
 


    public void onTankRewardButtonClicked() {
        btnTankAch.SetActive(false);
        AddCoins(getReward(PlayerPrefs.GetInt(Achievements.TANKS_ACH_LEVEL, 0)));
        PlayerPrefs.SetInt(Achievements.TANKS_ACH_STATUS, PlayerPrefs.GetInt(Achievements.TANKS_ACH_STATUS, 0) + 1);
        PlayButtonClick();
        GetTankAchCollection();
    }
    public void onScoreRewardButtonClicked()
    {
        btnScoreAch.SetActive(false);
        AddCoins(getReward(PlayerPrefs.GetInt(Achievements.POINTS_ACH_LEVEL, 0)));
        PlayerPrefs.SetInt(Achievements.POINTS_ACH_STATUS, PlayerPrefs.GetInt(Achievements.POINTS_ACH_STATUS, 0) + 1);
        PlayButtonClick();
        GetScoreAchCollection();
    }
    public void onCoinRewardButtonClicked()
    {
        btnCoinAch.SetActive(false);
        AddCoins(getReward(PlayerPrefs.GetInt(Achievements.COINS_ACH_LEVEL, 0)));
        PlayerPrefs.SetInt(Achievements.COINS_ACH_STATUS, PlayerPrefs.GetInt(Achievements.COINS_ACH_STATUS, 0) + 1);
        PlayButtonClick();
        GetCoinAchCollection();
    }
    public void onBulletRewardButtonClicked()
    {
        btnBulltAch.SetActive(false);
        AddCoins(getReward(PlayerPrefs.GetInt(Achievements.BULLETS_ACH_LEVEL, 0)));
        PlayerPrefs.SetInt(Achievements.BULLETS_ACH_STATUS, PlayerPrefs.GetInt(Achievements.BULLETS_ACH_STATUS, 0) + 1);
        PlayButtonClick();
        GetBulletAchCollection();
    }
    public void onRocketRewardButtonClicked()
    {
        btnRocketAch.SetActive(false);
        AddCoins(getReward(PlayerPrefs.GetInt(Achievements.ROCKETS_ACH_LEVEL, 0)));
        PlayerPrefs.SetInt(Achievements.ROCKETS_ACH_STATUS, PlayerPrefs.GetInt(Achievements.ROCKETS_ACH_STATUS, 0) + 1);
        PlayButtonClick();
        GetRocketAchCollection();
    }
    public void onPowerRewardButtonClicked()
    {
        btnPowerAch.SetActive(false);
        AddCoins(getReward(PlayerPrefs.GetInt(Achievements.POWERS_ACH_LEVEL, 0)));
        PlayerPrefs.SetInt(Achievements.POWERS_ACH_STATUS, PlayerPrefs.GetInt(Achievements.POWERS_ACH_STATUS, 0) + 1);
        PlayButtonClick();
        GetPowerAchCollection();
    }
    public void onDailyChallengesButtonClicked(){
        PlayButtonClick();
        DailyChallengesWindow.SetActive(true);
    }
    public void onCloseWindowButtonClicked() {
        PlayButtonClick();
        DailyChallengesWindow.SetActive(false);
    }
    public void onBackButtonClicked() {
        PlayButtonClick();
        SaveMusicTime();
        SceneManager.LoadScene("StartScene");
    }


    public void onC1CompletedButtonClicked()
    {
        AddCoins(50);
        PlayerPrefs.SetInt(Challenges.CHALLENGE_1_STATUS, 1);
        PlayButtonClick();
        btnC1.SetActive(false);
    }

    public void onC2CompletedButtonClicked() {
        AddCoins(50);
        PlayerPrefs.SetInt(Challenges.CHALLENGE_2_STATUS, 1);
        PlayButtonClick();
        btnC2.SetActive(false);
    }
    public void onC3CompletedButtonClicked()
    {
        AddCoins(50);
        PlayerPrefs.SetInt(Challenges.CHALLENGE_3_STATUS, 1);
        PlayButtonClick();
        btnC3.SetActive(false);
    }


    void PlayButtonClick() {
        if (!isMuted)
        {
            UIAudio.PlayOneShot(buttonClick);
        }
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
    void SaveMusicTime()
    {
        PlayerPrefs.SetFloat(PlayerControl.MUSIC_TIME, backgroundMusic.time);
    }
}





