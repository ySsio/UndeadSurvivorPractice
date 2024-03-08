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
        icon = GetComponentsInChildren<Image>()[1]; // 0 ��°�� �θ�(this)
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

        // ������ ������ ���� ���� ���� ���� ���� �ٸ�.. ��
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
                // ������ 0�� �� (���Ⱑ ������ ��) => ���� ���� (AddComponent)
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
                // ������ 0�� �� (�� ������ ��) => ���� ���� (AddComponent)
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

        // �ִ� ������ ������ ��� (damages �迭�� ������� ��� ����;)
        if (level == data.damages.Length)
        {
            // ��ư�� ��ȣ�ۿ� ����
            GetComponent<Button>().interactable = false;
        }

    }

}
