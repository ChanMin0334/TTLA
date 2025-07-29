using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static SoundManager Instance { get; private set; }

    // ������ǿ� AudioSource
    private AudioSource bgmSource;
    // ȿ������ AudioSource
    private AudioSource sfxSource;

    // ȿ���� Ŭ�� ����Ʈ ����
    public AudioClip[] sfxClips;

    private void Awake()
    {
        // �̱��� �ν��Ͻ� �Ҵ� �� �ߺ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // AudioSource ������Ʈ 2�� �߰� (BGM, SFX)
            bgmSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            bgmSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ������� ���
    public void PlayBGM(AudioClip clip, float volume = 1f)
    {
        if (bgmSource.clip != clip)
        {
            bgmSource.clip = clip;
            bgmSource.volume = volume;
            bgmSource.Play();
        }
    }

    // ȿ���� ���
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    // ������� ����
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // ȿ���� ���� ����
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    // ������� ���� ����
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }
}
