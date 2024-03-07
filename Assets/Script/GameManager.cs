using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime;  // 게임시간
    public float maxGameTime = 20f;
    public bool isLive;     // 게임 실행 중인지

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kills;
    public int exp;
    public int[] MaxExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public Player player;
    public PoolManager poolManager;
    public LevelUpWindow levelUpWindow;

    


    private void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;
        // 임시 스크립트 (첫번째 무기 지급)

        levelUpWindow.Select(0);

        isLive = true;
    }

    private void Update()
    {
        if (!isLive)
            return;
        gameTime += Time.deltaTime;

        if (gameTime >= maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
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
        Time.timeScale = 0;     // 유니티의 시간 속도 (배율). 기본값은 1.
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;     // 유니티의 시간 속도 (배율). 기본값은 1.
    }
}
