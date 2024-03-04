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

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1]; // 0 ��°�� �θ�(this)
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        textLevel.text = "Lv. " + (level + 1);
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
