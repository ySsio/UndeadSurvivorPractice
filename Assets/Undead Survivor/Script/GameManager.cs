using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // static 변수는 클래스에서 바로 부를 수 있다!! (메모리에 올림?)
    // ex) GameManager.instance.player 같은 식으로 다른 클래스에서 호출 가능
    public static GameManager instance;

    public float gameTime;
    public float maxGameTime;

    public Player player;
    public PoolManager pool;
    public Spawner spawner;
    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

}
