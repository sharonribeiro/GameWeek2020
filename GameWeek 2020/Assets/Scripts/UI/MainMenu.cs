using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string m_sceneToLoad;
    [SerializeField]
    private GameObject m_mainPanel;
    [SerializeField]
    private GameObject m_soundPanel;
    [SerializeField]
    private GameObject m_creditsPanel;

    void Start()
    {
        m_mainPanel.SetActive(true);
        m_soundPanel.SetActive(false);
        m_creditsPanel.SetActive(false);
    }

    public void PlayScene()
    {
        SceneManager.LoadScene(m_sceneToLoad);
    }

    public void Exit()
    {
        if (Application.isPlaying)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    public void OpenSoundPanel()
    {
        m_mainPanel.SetActive(false);
        m_soundPanel.SetActive(true);
    }

    public void OpenCreditsPanel()
    {
        m_mainPanel.SetActive(false);
        m_creditsPanel.SetActive(true);
    }

    public void CloseSoundPanel()
    {
        m_soundPanel.SetActive(false);
        OpenMainPanel();
    }

    public void ChangeMusicVolume(float p_value)
    {
        AudioManager instance = AudioManager.instance;
        instance.SetVolume(instance.m_ambientMusicAudioSource, p_value);
    }

    public void ChangeFXVolume(float p_value)
    {
        AudioManager instance = AudioManager.instance;
        instance.SetVolume(instance.m_oneShotAudioSource, p_value);
        instance.SetVolume(instance.m_loopAudioSource, p_value);
    }

    public void CloseCreditsPanel()
    {
        m_creditsPanel.SetActive(false);
        OpenMainPanel();
    }

    void OpenMainPanel()
    {
        m_mainPanel.SetActive(true);
    }
}
