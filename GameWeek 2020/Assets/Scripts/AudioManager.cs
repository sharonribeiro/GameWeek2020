using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Managers
{
    [RequireComponent(typeof(AudioSource), typeof(AudioSource), typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [HideInInspector] 
        public static AudioManager instance;
        
        [Header("FX AudioSources")]
        public AudioSource m_loopAudioSource;
        public AudioSource m_oneShotAudioSource;
        
        [Header("Music AudioSources")]
        public AudioSource m_ambientMusicAudioSource;

        [Header("Music AudioClips")]
        public AudioClip m_musicClip;
        
        [Header("FX AudioClips")]
        public AudioClip m_fxAudioClip;

        private bool m_isMusicPlaying;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            SceneManager.sceneLoaded += SceneLoaded;
        }

        private void SceneLoaded(Scene p_arg0, LoadSceneMode p_arg1)
        {
            if (p_arg0.buildIndex != 0)
            {
                if (m_loopAudioSource == null || m_oneShotAudioSource == null || SceneManager.GetActiveScene().buildIndex == 0)
                {
                    return;
                }
                UpdateMusic();
            }
            else
            {
                m_ambientMusicAudioSource.clip = m_musicClip;
                m_ambientMusicAudioSource.Play();
            }
        }

        private void Update()
        {
            if (m_loopAudioSource == null || m_oneShotAudioSource == null || SceneManager.GetActiveScene().buildIndex == 0)
            {
                return;
            }
            UpdateMusic();
        }

        private void UpdateMusic()
        {
            if (m_isMusicPlaying == false)
            {
                m_isMusicPlaying = true;
                m_ambientMusicAudioSource.loop = true;
                m_ambientMusicAudioSource.clip = m_musicClip;
                SetVolume(m_ambientMusicAudioSource, m_ambientMusicAudioSource.volume);
                m_ambientMusicAudioSource.Play();
            }
        }

        private void ResetAudioSource(AudioSource p_as)
        {
            p_as.Stop();
            p_as.clip = null;
            p_as.loop = false;
        }

        public void PlaySoundOnce(AudioClip p_audioClip)
        {
            m_oneShotAudioSource.PlayOneShot(p_audioClip);
        }

        public void SetVolume(AudioSource p_audioSource, float p_volume)
        {
            p_audioSource.volume = p_volume;
        }
    }
}