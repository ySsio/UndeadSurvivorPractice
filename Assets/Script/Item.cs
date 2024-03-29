using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1]; // 0 번째는 부모(this)
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>(); // Level, Name, Desc
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
    }

    private void OnEnable()
    {
        textLevel.text = "Lv. " + (level + 1);

        // 아이템 종류에 따라 설명에 들어가는 인자 수가 다름.. ㅠ
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;
            case ItemData.ItemType.Heal:
                textDesc.text = data.itemDesc;
                break;

        }
    }


    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                // 레벨이 0일 때 (무기가 없었을 때) => 새로 생성 (AddComponent)
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);          // Weapon.Init()
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    Debug.Log(gameObject.name);
                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }

                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                // 레벨이 0일 때 (기어가 없었을 때) => 새로 생성 (AddComponent)
                if (level==0)
                {
                    GameObject newWeapon = new GameObject();
                    gear = newWeapon.AddComponent<Gear>();
                    gear.Init(data);            // Gear.Init()
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }

        ++level;

        // 최대 레벨에 도달한 경우 (damages 배열의 사이즈로 계산 ㅋㅋ;)
        if (level == data.damages.Length)
        {
            // 버튼의 상호작용 제거
            GetComponent<Button>().interactable = false;
        }

    }

}
