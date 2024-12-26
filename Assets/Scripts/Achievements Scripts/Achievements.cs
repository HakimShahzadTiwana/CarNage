using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements 
{
    public static string TANKS_DESTROYED = "TANKS_DESTROYED";
    public static string COINS_COLLECTED = "COINS_COLLECTED";
    public static string POINTS_SCORED = "POINTS_SCORED";
    public static string BULLETS_FIRED = "BULLETS_FIRED";
    public static string ROCKETS_LAUNCHED = "ROCKETS_LAUNCHED";
    public static string POWERS_GAINED = "POWERS_GAINED";

    public static string TANKS_ACH_LEVEL = "TANKS_ACH_LEVEL";
    public static string COINS_ACH_LEVEL = "COINS_ACH_LEVEL";
    public static string POINTS_ACH_LEVEL = "POINTS_ACH_LEVEL";
    public static string BULLETS_ACH_LEVEL = "BULLETS_ACH_LEVEL";
    public static string ROCKETS_ACH_LEVEL = "ROCKETS_ACH_LEVEL";
    public static string POWERS_ACH_LEVEL = "POWERS_ACH_LEVEL";

    public static string TANKS_ACH_STATUS = "TANKS_ACH_STATUS";
    public static string COINS_ACH_STATUS = "COINS_ACH_STATUS";
    public static string BULLETS_ACH_STATUS = "BULLETS_ACH_STATUS";
    public static string POINTS_ACH_STATUS = "POINTS_ACH_STATUS";
    public static string POWERS_ACH_STATUS = "POWERS_ACH_STATUS";
    public static string ROCKETS_ACH_STATUS = "ROCKETS_ACH_STATUS";


    static int tankAchLevel=0;
    public static bool tankChanged=false;
    public static int[] tanksNeeded = { 10, 50, 200 };

    static int scoreAchLevel=0;
    public static bool scoreChanged = false;
    public static int[] scoreNeeded = { 5000, 10000, 50000 };

    static int bulletAchLevel=0;
    public static bool bulletsChanged = false;
    public static int[] bulletsNeeded = { 50, 500, 1000 };

    static int rocketAchLevel=0;
    public static bool rocketsChanged = false;
    public static int[] rocketsNeeded = { 20, 200, 500 };

    static int coinAchLevel =0;
    public static bool coinsChanged = false;
    public static int[] coinsNeeded = { 1000, 5000, 10000 };

    static int powerAchLevel =0;
    public static bool powerChanged = false;
    public static int[] powerNeeded = { 20, 75, 150 };

  

   public static void AddTanksDestroyed(int tanks) {
       int destroyedTanks = PlayerPrefs.GetInt(TANKS_DESTROYED, -1);      
        if (destroyedTanks == -1)
        {
            PlayerPrefs.SetInt(TANKS_DESTROYED, tanks);
        }
        else
        {         
            int totalTanks = destroyedTanks + tanks;
            for (int i = 0; i < 3; i++)
            {
                if (totalTanks >= tanksNeeded[i])
                {
                    tankAchLevel = i + 1;
                }
            }
            PlayerPrefs.SetInt(TANKS_ACH_LEVEL, tankAchLevel);
            PlayerPrefs.SetInt(TANKS_DESTROYED,totalTanks);
        }
   }
   public static void AddCoinsCollected(int coins)
    {
        int coinsCollected = PlayerPrefs.GetInt(COINS_COLLECTED, -1);
        if (coinsCollected == -1)
        {
            PlayerPrefs.SetInt(COINS_COLLECTED, coins);
        }
        else
        {
            int totalCoins =  coinsCollected+coins;
            for (int i = 0; i < 3; i++)
            {
                if (totalCoins >= coinsNeeded[i])
                {
                    coinAchLevel = (i + 1);
                }
            }
            PlayerPrefs.SetInt(COINS_ACH_LEVEL, coinAchLevel);
            PlayerPrefs.SetInt(COINS_COLLECTED, totalCoins);
        }
    }
   public static void AddPointsScored(int points)
    {
        int pointsScored = PlayerPrefs.GetInt(POINTS_SCORED, -1);
        if (pointsScored == -1)
        {
            PlayerPrefs.SetInt(POINTS_SCORED, points);
        }
        else
        {
            int totalPoints = pointsScored + points;
            for (int i = 0; i < 3; i++)
            {
                if (totalPoints >= scoreNeeded[i])
                {
                    scoreAchLevel = (i + 1);
                }
            }
            PlayerPrefs.SetInt(POINTS_ACH_LEVEL, scoreAchLevel);
            PlayerPrefs.SetInt(POINTS_SCORED,totalPoints);
        }
    }
   public static void AddBulletsFired(int bullets)
    {
        int bulletsFired = PlayerPrefs.GetInt(BULLETS_FIRED, -1);
        if (bulletsFired == -1)
        {
            PlayerPrefs.SetInt(BULLETS_FIRED, bullets);
        }
        else
        {
            int totalBullets = bulletsFired + bullets;
            for (int i = 0; i < 3; i++)
            {
                if (totalBullets >= bulletsNeeded[i])
                {
                    bulletAchLevel = (i + 1);
                }
            }
            PlayerPrefs.SetInt(BULLETS_ACH_LEVEL, bulletAchLevel);
            PlayerPrefs.SetInt(BULLETS_FIRED, totalBullets);
        }
    }
   public static void AddRocketsLaunched(int rockets)
    {
        int rocketsLaunched = PlayerPrefs.GetInt(ROCKETS_LAUNCHED, -1);
        if (rocketsLaunched == -1)
        {
            PlayerPrefs.SetInt(ROCKETS_LAUNCHED, rockets);
        }
        else
        {
            int totalRockets = rocketsLaunched + rockets;
            for (int i = 0; i < 3; i++)
            {
                if (totalRockets >= rocketsNeeded[i])
                {
                    rocketAchLevel = (i + 1);
                }
            }
            PlayerPrefs.SetInt(ROCKETS_ACH_LEVEL, rocketAchLevel);
            PlayerPrefs.SetInt(ROCKETS_LAUNCHED, totalRockets);
        }
    }
   public static void AddPowerGained(int power)
   {
        int powerGained = PlayerPrefs.GetInt(POWERS_GAINED, -1);
        if (powerGained == -1)
        {
            PlayerPrefs.SetInt(POWERS_GAINED, power);
        }
        else
        {
            int totalPower = powerGained + power;
            for (int i = 0; i < 3; i++)
            {
                if (totalPower >= powerNeeded[i])
                {
                    powerAchLevel = (i + 1);
                }
            }
            PlayerPrefs.SetInt(POWERS_ACH_LEVEL, powerAchLevel);
            PlayerPrefs.SetInt(POWERS_GAINED,totalPower);
        }
    }
}
