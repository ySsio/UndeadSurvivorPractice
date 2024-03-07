using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime;  // ���ӽð�
    public float maxGameTime = 20f;
    public bool isLive;     // ���� ���� ������

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
        // �ӽ� ��ũ��Ʈ (ù��° ���� ����)

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
        Time.timeScale = 0;     // ����Ƽ�� �ð� �ӵ� (����). �⺻���� 1.
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;     // ����Ƽ�� �ð� �ӵ� (����). �⺻���� 1.
    }
}
