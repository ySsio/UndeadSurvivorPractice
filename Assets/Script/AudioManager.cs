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
        // # �� �����Ϳ��� �̸� �� ����� ��ũ��Ʈ�� �����.
        // # ������� ������ �� ���� �׳� �����Ϳ��� �����ֵ� �Ǵµ�,
        // # ȿ������ ���� ���� ���ÿ� �߻��� �� �ֱ� ������ ��Ÿ�ӿ��� ���ÿ� ����Ǵ� ������ҽ��� ������ ������. ���� ��ũ��Ʈ�� ����.
        // # �׷����� ���ӿ�����Ʈ�� �׳� ����� �� �� ���� �ʳ�? ������Ʈ�� �������� �����ϸ� �ǰ�
        // # �ƴ� �ٽ� �����غ��� ������ ���⼭�� �ϴ� �迭 ������ �������ִµ�

        // ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();  // BgmPlayer ������Ʈ�� 
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmHighPassFilter = Camera.main.GetComponent<AudioHighPassFilter>();


        // ȿ���� �÷��̾� �ʱ�ȭ
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

            // Hit�̳� Melee�� 2���� ȿ���� �� �ϳ��� ���� �����ϱ� ���� �Ʒ� ����
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
