using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float m_timeInMinute = 5.0f;
    [SerializeField] private float m_timeInSeconds = 0;
    [SerializeField] private Score[] m_countryScores = new Score[3];
    public Canvas nextLevel = null;
    
    private void Start()
    {
        if(nextLevel)
            nextLevel.gameObject.SetActive(false);
        
        m_timeInSeconds = m_timeInMinute * 60.0f;
        StartCoroutine(nameof(StartTimer));
    }

    private void TimesUp()
    {
        int i = CalculateScore();
        Debug.Log("Times up with score : " + i);
        
        if(nextLevel)
            nextLevel.gameObject.SetActive(true);
    }

    private int CalculateScore()
    {
        int score = 0;

        foreach (var countryScore in m_countryScores)
        {
            if (countryScore.GetScore() >= countryScore.GetObjective())
                ++score;
        }
        
        return score;
    }

    IEnumerator StartTimer()
    {
        WaitForSecondsRealtime timeToWait = new WaitForSecondsRealtime(1);
        m_timeInSeconds -= 1.0f;

        yield return timeToWait;
        
        if (m_timeInSeconds > 0)
            yield return StartCoroutine(nameof(StartTimer));
        else
        {
            TimesUp();
            yield return null;
        }
    }
}
