using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime; // 게임시간
    public float maxGameTime = 20f;
    
    [Header("# Player Info")]
    public int level;
    public int kills;
    public int exp;
    public int[] MaxExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public Player player;
    public PoolManager poolManager;



    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime >= maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        ++exp;

        if (exp >= MaxExp[level])
        {
            ++level;
            exp = 0;
        }
    }
}
