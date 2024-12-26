using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenges 
{
    public static string  CHALLENGE_1 = "CHALLENGE_1";
    public static string CHALLENGE_2 = "CHALLENGE_2";
    public static string CHALLENGE_3 = "CHALLENGE_3";
    public static string CHALLENGE_1_COUNT = "CHALLENGE_1_COUNT";
    public static string CHALLENGE_2_COUNT = "CHALLENGE_2_COUNT";
    public static string CHALLENGE_3_COUNT = "CHALLENGE_3_COUNT";
    public static string CHALLENGE_1_TYPE = "CHALLENGE_1_TYPE";
    public static string CHALLENGE_2_TYPE = "CHALLENGE_2_TYPE";
    public static string CHALLENGE_3_TYPE = "CHALLENGE_3_TYPE";
    public static string CHALLENGE_TANK = "CHALLENGE_TANK";
    public static string CHALLENGE_SCORE = "CHALLENGE_SCORE";
    public static string CHALLENGE_SINGLE_SCORE = "CHALLENGE_SINGLE_SCORE";
    public static string CHALLENGE_COINS = "CHALLENGE_COINS";
    public static string CHALLENGE_SINGLE_COINS = "CHALLENGE_SINGLE_COINS";
    public static string DAILY_TANKS_DESTROYED = "DAILY_TANKS_DESTROYED";
    public static string DAILY_POINTS_SCORED = "DAILY_POINTS_SCORED";
    public static string DAILY_COINS_COLLECTED = "DAILY_COINS_COLLECTED";
    public static string SINGLE_POINTS_SCORED = "SINGLE_POINTS_SCORED";
    public static string SINGLE_COINS_COLLECTED = "SINGLE_COINS_COLLECTED";
    public static string CHALLENGE_1_STATUS = "CHALLENGE_1_STATUS";
    public static string CHALLENGE_2_STATUS = "CHALLENGE_2_STATUS";
    public static string CHALLENGE_3_STATUS = "CHALLENGE_3_STATUS";


    public string details;
    public int count;
    public string challengeType;

    Challenges() {
        details = "";
        count = 0;
        challengeType = "";
    }

    private static Challenges TankChallanges() {
        Challenges tank = new Challenges();
        tank.challengeType = CHALLENGE_TANK;
        tank.count = Random.Range(1, 6);
        tank.details = "Destroy " + tank.count + " tanks.";
        return tank;
    }
    private static Challenges ScoreChallanges()
    {
        Challenges score = new Challenges();
        score.challengeType = CHALLENGE_SCORE;
         int step = Random.Range(1, 5);
        score.count = 500 * step;
        score.details = "Score " + score.count + " points in total.";
        return score;
    }
    private static Challenges CoinChallanges()
    {
        Challenges coin = new Challenges();
        coin.challengeType = CHALLENGE_COINS;
        int step = Random.Range(1, 5);
        coin.count = step * 50;
        coin.details = "Collect " + coin.count + " coins in total.";
        return coin;
    }
    private static Challenges SingleScoreChallanges()
    {
        Challenges score = new Challenges();
        score.challengeType = CHALLENGE_SINGLE_SCORE;
        int step = Random.Range(1, 3);
        score.count = 500 * step;
        score.details = "Score " + score.count + " points in a single game.";
        return score;
    }
    private static Challenges SingleCoinChallanges()
    {
        Challenges coin = new Challenges();
        coin.challengeType = CHALLENGE_SINGLE_COINS;
        int step = Random.Range(1, 3);
        coin.count = step * 50;
        coin.details = "Collect " + coin.count + " coins in a single game.";
        return coin;
    }
    private static int[] UniqueRandomNumbers(int count) {
        int[] rndmNums =new int [count];
        for (int i = 0; i < count; i++) {
            rndmNums[i] = -1;
        }

        for (int i = 0; i < count; i++)
        {
            bool sameFound = false;
            int random;
            do
            {
                sameFound = false;
                random = Random.Range(0, 4);
                for (int j = 0; j < count; j++)
                {
                    if (random == rndmNums[j])
                    {
                        sameFound = true;
                        break;
                    }
                }
            } while (sameFound);

            rndmNums[i] = random;
        }
        return rndmNums;
    }
    public static void GenerateChallenges() {
        Challenges[] challenges = new Challenges[3];
        int[] randomNumbers = UniqueRandomNumbers(3);
        for (int i = 0; i < 3; i++)
        {

            switch (randomNumbers[i])
            {

                case 1:
                    challenges[i] = TankChallanges();             
                    break;
                case 2:
                    challenges[i] = ScoreChallanges();                  
                    break;
                case 3:
                    challenges[i] = CoinChallanges();                   
                    break;
                case 4:
                    challenges[i] = SingleScoreChallanges();                   
                    break;
                default:
                    challenges[i] = SingleCoinChallanges();
                    break;
            }
        }
        SaveChallenges(challenges);
    }


    public static void SaveChallenges(Challenges[] chal) {
        PlayerPrefs.SetString(CHALLENGE_1, chal[0].details);
        PlayerPrefs.SetString(CHALLENGE_1_TYPE, chal[0].challengeType);
        PlayerPrefs.SetInt(CHALLENGE_1_COUNT, chal[0].count);

        PlayerPrefs.SetString(CHALLENGE_2, chal[1].details);
        PlayerPrefs.SetString(CHALLENGE_2_TYPE, chal[1].challengeType);
        PlayerPrefs.SetInt(CHALLENGE_2_COUNT, chal[1].count);

        PlayerPrefs.SetString(CHALLENGE_3, chal[2].details);
        PlayerPrefs.SetString(CHALLENGE_3_TYPE, chal[2].challengeType);
        PlayerPrefs.SetInt(CHALLENGE_3_COUNT, chal[2].count);

    }
    public static string[] LoadChallengeDetails()
    {
        string[] details = { "", "", "" };
        details[0]= PlayerPrefs.GetString(CHALLENGE_1);
        details[1]=PlayerPrefs.GetString(CHALLENGE_2);
        details[2]=PlayerPrefs.GetString(CHALLENGE_3);

        return details;

    }

    public static int ChallengeProgress(string chalType) {

            if (chalType == CHALLENGE_TANK)
            {
                return  PlayerPrefs.GetInt(DAILY_TANKS_DESTROYED, 0);
            }
            else if (chalType == CHALLENGE_SCORE)
            {
                return   PlayerPrefs.GetInt(DAILY_POINTS_SCORED, 0);
            }
            else if (chalType == CHALLENGE_COINS)
            {
                return PlayerPrefs.GetInt(DAILY_COINS_COLLECTED, 0);
            }
            else if (chalType == CHALLENGE_SINGLE_SCORE)
            {
                return PlayerPrefs.GetInt(SINGLE_POINTS_SCORED, 0);
            }
            else if (chalType == CHALLENGE_SINGLE_COINS)
            {
                return PlayerPrefs.GetInt(SINGLE_COINS_COLLECTED, 0);
            }
        return 0;
        
   
    }


    public static void AddDailyCoins(int coins) {

        int currentCoins = PlayerPrefs.GetInt(DAILY_COINS_COLLECTED, 0);
        if (currentCoins == 0)
        {
            PlayerPrefs.SetInt(DAILY_COINS_COLLECTED, coins);
        }
        else {
            PlayerPrefs.SetInt(DAILY_COINS_COLLECTED, currentCoins + coins);
        }
  
    }
    public static void AddDailyScore(int score)
    {

        int currentScore = PlayerPrefs.GetInt(DAILY_POINTS_SCORED, 0);
        if (currentScore == 0)
        {
            PlayerPrefs.SetInt(DAILY_POINTS_SCORED, score);
        }
        else
        {
            PlayerPrefs.SetInt(DAILY_POINTS_SCORED, currentScore + score);
        }

    }
    public static void AddDailyTanksDestroyed(int tanks)
    {
      
        int currentTanks = PlayerPrefs.GetInt(DAILY_TANKS_DESTROYED, 0);
        if (currentTanks == 0)
        {
            PlayerPrefs.SetInt(DAILY_TANKS_DESTROYED, tanks);
        }
        else
        {
            PlayerPrefs.SetInt(DAILY_TANKS_DESTROYED, currentTanks + tanks);
        }

    }
    public static void SaveSingleGameCoins(int coinCount)
    {
     
        PlayerPrefs.SetInt(SINGLE_COINS_COLLECTED, coinCount);
 
    }
    public static void SaveSingleGameScore(int score)
    {
    
        PlayerPrefs.SetInt(SINGLE_POINTS_SCORED, score);
       
    }
    public static void ClearSingleGameCoins()
    {
        PlayerPrefs.SetInt(SINGLE_COINS_COLLECTED, 0);

    }
    public static void ClearSingleGameScore()
    {
        PlayerPrefs.SetInt(SINGLE_POINTS_SCORED, 0);
    }

}
