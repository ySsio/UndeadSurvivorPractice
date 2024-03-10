using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    private AudioSource bgmPlayer;
    private AudioHighPassFilter bgmHighPassFilter;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channelCount;
    private AudioSource[] sfxPlayers;
    private int currentChannel;

    public enum Sfx {
        Dead,
        Hit,
        LevelUp = 3,
        Lose,
        Melee,
        Range = 7,
        Select,
        Win,
    }

    private void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        // # 왜 에디터에서 미리 안 만들고 스크립트로 만드냐.
        // # 배경음은 어차피 한 개라 그냥 에디터에서 만들어둬도 되는데,
        // # 효과음은 여러 개가 동시에 발생할 수 있기 때문에 런타임에서 동시에 실행되는 오디오소스의 개수가 결정됨. 따라서 스크립트로 만듦.
        // # 그럼에도 게임오브젝트는 그냥 만들어 둘 수 있지 않나? 컴포넌트만 동적으로 관리하면 되고
        // # 아니 다시 생각해보니 심지어 여기서도 일단 배열 개수는 정해져있는데

        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();  // BgmPlayer 컴포넌트에 
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmHighPassFilter = Camera.main.GetComponent<AudioHighPassFilter>();


        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channelCount];
        for (int i = 0; i < channelCount; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
            sfxPlayers[i].bypassListenerEffects = true;
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if(isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmHighPassFilter.enabled = isPlay;
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < channelCount; i++)
        {
            int loopIndex = (i + currentChannel) % channelCount;
            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            // Hit이나 Melee는 2가지 효과음 중 하나로 랜덤 지정하기 위해 아래 로직
            int rand = 0;
            if (sfx == Sfx.Hit || sfx == Sfx.Melee)
            {
                rand = Random.Range(0,2);
            }

            currentChannel = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + rand];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
