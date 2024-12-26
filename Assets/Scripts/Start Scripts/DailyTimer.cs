using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DailyTimer
{
    private DateTime expiryTime;

    public DailyTimer()
    {
        if (!ReadTimestamp("timer"))
        {
            ScheduleTimer();
            onTimer();
        }
        if (DateTime.Now > expiryTime)
        {
            ScheduleTimer();
            onTimer();
        }
    }


    void ScheduleTimer()
    {
        expiryTime = DateTime.Now.AddDays(1.0);
        WriteTimestamp("timer");
    }
    private bool ReadTimestamp(string key)
    {
        long tmp = Convert.ToInt64(PlayerPrefs.GetString(key, "0"));
        if (tmp == 0)
        {
            return false;
        }
        expiryTime = DateTime.FromBinary(tmp);
        return true;
    }

    private void WriteTimestamp(string key)
    {
        PlayerPrefs.SetString(key, expiryTime.ToBinary().ToString());
    }

    void onTimer()
    {
        Challenges.GenerateChallenges();
        PlayerPrefs.SetInt(Challenges.DAILY_TANKS_DESTROYED, 0);
        PlayerPrefs.SetInt(Challenges.DAILY_POINTS_SCORED, 0);
        PlayerPrefs.SetInt(Challenges.DAILY_COINS_COLLECTED, 0);
        PlayerPrefs.SetInt(Challenges.CHALLENGE_1_STATUS, 0);
        PlayerPrefs.SetInt(Challenges.CHALLENGE_2_STATUS, 0);
        PlayerPrefs.SetInt(Challenges.CHALLENGE_3_STATUS, 0);

    }




}
