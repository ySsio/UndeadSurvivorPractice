using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI levelUI;

    float time;
    int level;


    void Update()
    {
        time = GameManager.instance.gameTime;
        timerUI.text = string.Format("{0:00}",Mathf.FloorToInt(time/60)) + " : " + string.Format("{0:00}",Mathf.FloorToInt(time)%60);

        level = GameManager.instance.spawner.level;
        levelUI.text = $"Lv. {level+1}";
    }
}
