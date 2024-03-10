using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime;  // ���ӽð�
    public float maxGameTime = 20f;
    public bool isLive;     // ���� ���� ������

    [Header("# Player Info")]
    public int playerID;
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kills;
    public int exp;
    public int[] MaxExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public Player player;
    public PoolManager poolManager;
    public LevelUpWindow levelUpWindow;
    public Result uiResult;
    public GameObject enemyCleaner;
    


    private void Awake()
    {
        instance = this;
    }

    public void GameStart(int _id)
    {
        playerID = _id;
        health = maxHealth;

        player.gameObject.SetActive(true);
        levelUpWindow.Select(playerID % 2);
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        
        yield return new WaitForSeconds(0.5f);  // �÷��̾� ���� �ִϸ��̼� ���⸣ ��ٸ�.
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);  // ��� ���� ��� �ִϸ��̼� ��ٸ�.

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (!isLive)
            return;
        gameTime += Time.deltaTime;

        if (gameTime >= maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive)
            return;

        ++exp;

        if (exp >= MaxExp[Mathf.Min(level, MaxExp.Length - 1)])
        {
            ++level;
            exp = 0;
            levelUpWindow.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;     // ����Ƽ�� �ð� �ӵ� (����). �⺻���� 1.
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;     // ����Ƽ�� �ð� �ӵ� (����). �⺻���� 1.
    }
}
