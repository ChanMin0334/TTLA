using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    SFX_EnterBoss,
    SFX_PickupMoney
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    // 여러 BGM을 Inspector에서 할당
    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    public float bgmVolume { get; set; } = 1f;
    public float sfxVolume { get; set; } = 1f;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            bgmSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            bgmSource.loop = true;
            sfxSource.playOnAwake = false; // SFX는 자동 재생하지 않음
            SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트 등록
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제
        }
    }

    // 씬이 로드될 때마다 호출되는 메서드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"씬 로드됨: {scene.name}");

        switch(scene.name)
        {
            case "IntroScene":
                break;
            case "MainScene":
                PlayBGM(BGMName.BGM_01);
                break;
            case "Level1_Forest":
                PlayBGM(BGMName.BGM_02); 
                break;
            default:
                break;
        }
    }

    // 인덱스로 BGM 재생
    public void PlayBGM(BGMName soundName)
    {
        int index = (int)soundName;

        if (index < 0 || index >= bgmClips.Length) return;
        if (bgmSource.clip == bgmClips[index]) return;

        bgmSource.clip = bgmClips[index];
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(SFX_Name name)
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
            case SFX_Name.SFX_EnterBoss:
                clip = sfxClips[6];
                break;
            case SFX_Name.SFX_PickupMoney:
                clip = sfxClips[7];
                break;
            case SFX_Name.SFX_ButtonClick:
                clip = sfxClips[8];
                break;
            default:
                Debug.LogWarning("SFX clip not found for: " + name);
                return;

        }

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void SetSFXVolume(Slider slider)
    {
        sfxVolume = slider.value;
        sfxSource.volume = sfxVolume;
    }

    public void SetBGMVolume(Slider slider)
    {
        bgmVolume = slider.value;
        bgmSource.volume = bgmVolume;
    }

    public void SetSliderValues(Slider sfxSlider, Slider bgmSlider)
    {
        sfxSlider.value = sfxVolume;
        bgmSlider.value = bgmVolume;
    }
}
