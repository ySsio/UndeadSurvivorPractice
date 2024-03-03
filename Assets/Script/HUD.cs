using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    public void SetSliderMaxValue(int iValue)
    {
        mySlider.maxValue = iValue;
        return;
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                mySlider.value = GameManager.instance.exp;
                mySlider.maxValue = GameManager.instance.MaxExp[GameManager.instance.level];
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}",GameManager.instance.level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kills);
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Health:
                mySlider.value = GameManager.instance.health;
                mySlider.maxValue = GameManager.instance.maxHealth;
                break;
            default:
                break;
        }
    }

}
