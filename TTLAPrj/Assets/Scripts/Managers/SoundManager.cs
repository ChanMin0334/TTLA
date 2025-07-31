using UnityEngine;

public enum BGMName
{
    BGM_01,
    BGM_02,
    BGM_03,
    BGM_04,
    BossBGM,
}

public enum SFX_Name
{
    Player_Attack,
    Player_LevelUp,
    Player_Death,
    Player_Heal,
    Player_ByAttack,
    SFX_ButtonClick,
    SFX_GameOver,
    SFX_IntroSound,
    SFX_EnterBoss,
    SFX_PickupMoney
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    // 여러 BGM을 Inspector에서 할당
    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            bgmSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            bgmSource.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 인덱스로 BGM 재생
    public void PlayBGM(BGMName soundName, float volume = 1f)
    {
        int index = (int)soundName;

        if (index < 0 || index >= bgmClips.Length) return;
        if (bgmSource.clip == bgmClips[index]) return;

        bgmSource.clip = bgmClips[index];
        bgmSource.volume = volume;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(SFX_Name name, float volume = 1f)
    {
        AudioClip clip;

        switch (name)
        {
            case SFX_Name.Player_Attack:
                clip = sfxClips[0];
                break;
            case SFX_Name.Player_LevelUp:
                clip = sfxClips[1];
                break;
            case SFX_Name.Player_Death:
                clip = sfxClips[2];
                break;
            case SFX_Name.Player_Heal:
                clip = sfxClips[3];
                break;
            case SFX_Name.Player_ByAttack:
                clip = sfxClips[4];
                break;
            case SFX_Name.SFX_GameOver:
                clip = sfxClips[5];
                break;
            case SFX_Name.SFX_IntroSound:
                clip = sfxClips[6];
                break;
            case SFX_Name.SFX_EnterBoss:
                clip = sfxClips[7];
                break;
            case SFX_Name.SFX_PickupMoney:
                clip = sfxClips[8];
                break;
            case SFX_Name.SFX_ButtonClick:
                clip = sfxClips[9];
                break;
            default:
                Debug.LogWarning("SFX clip not found for: " + name);
                return;

        }

        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }
}
