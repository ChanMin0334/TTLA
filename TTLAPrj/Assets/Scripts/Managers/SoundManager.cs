using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static SoundManager Instance { get; private set; }

    // 배경음악용 AudioSource
    private AudioSource bgmSource;
    // 효과음용 AudioSource
    private AudioSource sfxSource;

    // 효과음 클립 리스트 예시
    public AudioClip[] sfxClips;

    private void Awake()
    {
        // 싱글톤 인스턴스 할당 및 중복 방지
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // AudioSource 컴포넌트 2개 추가 (BGM, SFX)
            bgmSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            bgmSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 배경음악 재생
    public void PlayBGM(AudioClip clip, float volume = 1f)
    {
        if (bgmSource.clip != clip)
        {
            bgmSource.clip = clip;
            bgmSource.volume = volume;
            bgmSource.Play();
        }
    }

    // 효과음 재생
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    // 배경음악 정지
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // 효과음 볼륨 조절
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    // 배경음악 볼륨 조절
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }
}
