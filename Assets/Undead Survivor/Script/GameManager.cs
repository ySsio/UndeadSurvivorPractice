using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // static ������ Ŭ�������� �ٷ� �θ� �� �ִ�!! (�޸𸮿� �ø�?)
    // ex) GameManager.instance.player ���� ������ �ٸ� Ŭ�������� ȣ�� ����
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
