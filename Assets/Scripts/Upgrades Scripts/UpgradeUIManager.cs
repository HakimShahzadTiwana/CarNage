using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradeUIManager : MonoBehaviour
{
    public TextMeshProUGUI playerCoins, infiniteCost, intangibleCost, multiplierCost;
    public Button InfiniteBtn, MultiplierBtn, intangibleBtn;
    public GameObject[] InfiniteLevelBar, MultiplierLevelBar, IntangibleLevelBar;
    private int currentCoins, infiniteLevel, multiplierLevel, intangibleLevel;
    private int maxlevel=4;
    public AudioClip buttonClick;
    private bool isMuted;
    private AudioSource backgroundMusic;
    private AudioSource UIAudio;

    private void Start()
    {
        backgroundMusic = Camera.main.GetComponent<AudioSource>();
        backgroundMusic.time = PlayerPrefs.GetFloat(PlayerControl.MUSIC_TIME, 0f);
        GetMuteStatus();
        UIAudio = GameObject.Find("UpgradeUIManager").GetComponent<AudioSource>();
        infiniteLevel = PlayerPrefs.GetInt(PlayerControl.INFINITE_LEVEL, 0);
        intangibleLevel = PlayerPrefs.GetInt(PlayerControl.INTANGIBLE_LEVEL, 0);
        multiplierLevel = PlayerPrefs.GetInt(PlayerControl.MULTIPLIER_LEVEL, 0);

        LoadUpgradeBars();

        currentCoins = PlayerPrefs.GetInt(PlayerControl.PLAYERCOINS, 0);
        UpdateCoins(currentCoins);

        infiniteCost.text = "x " + calculateCost(infiniteLevel).ToString();
        intangibleCost.text = "x " + calculateCost(intangibleLevel).ToString();
        multiplierCost.text = "x " + calculateCost(multiplierLevel).ToString(); 
    }


    void LoadUpgradeBars() {
        for (int i = 0; i < 5; i++)
        {
            if (i <= infiniteLevel)
            {
                InfiniteLevelBar[i].SetActive(true);
            }
            if (i <= multiplierLevel)
            {
                MultiplierLevelBar[i].SetActive(true);
            }
            if (i <= intangibleLevel)
            {
                IntangibleLevelBar[i].SetActive(true);
            }

        }
    }
    public void OnInfiniteUpgButtonClicked() {

        if (infiniteLevel == maxlevel)
        {
            InfiniteBtn.interactable = false;
            infiniteCost.text = "MAX";
        }
        else
        {

            if (currentCoins >= calculateCost(infiniteLevel))
            {
                if (infiniteLevel != 0)
                {
                    PlayerPrefs.SetInt(PlayerControl.INFINITE_LEVEL, infiniteLevel + 1);
                }
                else
                {
                    PlayerPrefs.SetInt(PlayerControl.INFINITE_LEVEL, 1);

                }
                currentCoins -= calculateCost(infiniteLevel);
                PlayerPrefs.SetInt(PlayerControl.PLAYERCOINS, currentCoins);
                UpdateCoins(currentCoins);
                infiniteLevel++;
                InfiniteLevelBar[infiniteLevel].SetActive(true);
                infiniteCost.text = "x " + calculateCost(infiniteLevel).ToString();
                PlayButtonClick();

            }
        }

    }
    public void OnIntangibleUpgButtonClicked()
    {
        if (intangibleLevel == maxlevel)
        {
            intangibleBtn.interactable = false;
            intangibleCost.text = "MAX";
        }
        else
        {
            if (currentCoins >= calculateCost(intangibleLevel))
            {
                if (intangibleLevel != 0)
                {
                    PlayerPrefs.SetInt(PlayerControl.INTANGIBLE_LEVEL, intangibleLevel + 1);
                }
                else
                {
                    PlayerPrefs.SetInt(PlayerControl.INTANGIBLE_LEVEL, 1);

                }
                currentCoins -= calculateCost(intangibleLevel);
                PlayerPrefs.SetInt(PlayerControl.PLAYERCOINS, currentCoins);
                UpdateCoins(currentCoins);
                intangibleLevel++;
                IntangibleLevelBar[intangibleLevel].SetActive(true);
                intangibleCost.text = "x " + calculateCost(intangibleLevel).ToString();
                PlayButtonClick();

            }
        }
    }
    public void OnMultiplierUpgButtonClicked()
    {
        if (multiplierLevel == maxlevel)
        {
            MultiplierBtn.interactable = false;
            multiplierCost.text = "MAX";
        }
        else
        {
            if (currentCoins >= calculateCost(multiplierLevel))
            {
                if (multiplierLevel != 0)
                {
                    PlayerPrefs.SetInt(PlayerControl.MULTIPLIER_LEVEL, multiplierLevel + 1);
                }
                else
                {
                    PlayerPrefs.SetInt(PlayerControl.MULTIPLIER_LEVEL, 1);

                }
                currentCoins -= calculateCost(multiplierLevel);
                PlayerPrefs.SetInt(PlayerControl.PLAYERCOINS, currentCoins);
                UpdateCoins(currentCoins);
                multiplierLevel++;
                MultiplierLevelBar[multiplierLevel].SetActive(true);
                multiplierCost.text = "x " + calculateCost(multiplierLevel).ToString();
                PlayButtonClick();

            }
        }
    }
    public void onBackButtonClicked() {
        PlayButtonClick();
        SaveMusicTime();
        SceneManager.LoadScene("StartScene");
    }

    void UpdateCoins(int coins) {
        playerCoins.text = coins.ToString();
    }
    int calculateCost(int level) {
        return ((level * 200) + ((level-1)*300) + ((level + 1) * 100))+500;
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

