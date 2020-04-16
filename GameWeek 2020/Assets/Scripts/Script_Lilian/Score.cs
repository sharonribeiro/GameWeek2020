using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class Score : MonoBehaviour
{
    [SerializeField] private Sprite m_flag = null;

    [Header("Preferences")] [SerializeField]
    private List<Collectible> m_likes = new List<Collectible>();

    [SerializeField] private List<Collectible> m_dontLikes = new List<Collectible>();

    [Header("Score settings")] [SerializeField]
    private float m_countryScore = 40.0f;

    [SerializeField] private float m_scoreObjective = 50.0f;
    [SerializeField] private float m_pointsToLoose = 1.0f;
    [SerializeField] private float m_timeToWait = 1.0f;
    [SerializeField] private float m_scoreGain = 5.0f;

    [SerializeField] private Image m_flagIconTarget;
    [SerializeField] private Image m_gaugeObj;
    [SerializeField] private Image m_gaugeCurrent;

    public float GetScore()
    {
        return m_countryScore;
    }

    public float GetObjective()
    {
        return m_scoreObjective;
    }

    private void Start()
    {
        StartCoroutine(nameof(ScoreDecrease));
        m_flagIconTarget.sprite = m_flag;
        m_gaugeObj.fillAmount = m_scoreObjective * 0.01f;
    }

    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        m_gaugeCurrent.fillAmount = GetScore() * 0.01f;
    }

    IEnumerator ScoreDecrease()
    {
        WaitForSecondsRealtime timeToWait = new WaitForSecondsRealtime(m_timeToWait);
        
        if (m_countryScore > 0)
            m_countryScore -= m_pointsToLoose;

        if (m_countryScore < 0)
            m_countryScore = 0;

        yield return timeToWait;
        yield return StartCoroutine(nameof(ScoreDecrease));
    }

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Collectible>();

        if (!other.isTrigger)
            return;
            
        if (!item)
            return;
        
        foreach (var like in m_likes)
        {
            if (item.name != like.name)
                continue;

            m_countryScore += m_scoreGain;
        }

        foreach (var dontLike in m_dontLikes)
        {
            if (item.name != dontLike.name)
                continue;

            if (m_countryScore > 0)
                m_countryScore -= m_pointsToLoose;

            if (m_countryScore < 0)
                m_countryScore = 0;
        }
        
        Destroy(other.gameObject);
    }
}
