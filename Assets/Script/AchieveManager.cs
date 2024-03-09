using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;

    public GameObject uiNotice;
    WaitForSecondsRealtime wait; // WaitForSeconds�� ���ӽð��̶� Ÿ�ӽ������� 0���� �ϸ� ���� ���ߴµ�, Realtime�� ���� �ð����� �ð��� �����.

    enum Achieve
    {
        UnlockPotato,
        UnlockBean,
    }

    Achieve[] achieves;

    private void Awake()
    {
        achieves = (Achieve[]) Enum.GetValues(typeof(Achieve)); // enum�� ��� ������ ���� �迭�� �޾ƿ�
        wait = new WaitForSecondsRealtime(5);
        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    private void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach(Achieve achieve in achieves)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }

    private void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        for (int index=0; index<lockCharacter.Length; index++)
        {
            string achieveName = achieves[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achieveName) == 1;
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

    private void LateUpdate()
    {
        foreach (Achieve achieve in achieves)
        {
            CheckAchieve(achieve);
        }
    }

    void CheckAchieve(Achieve _achieve)
    {
        bool isAchieve = false;
        switch (_achieve)
        {
            case Achieve.UnlockPotato:
                // �� 10���� �̻� óġ �� �ر�
                isAchieve = GameManager.instance.kills >= 10;
                break;
            case Achieve.UnlockBean:
                // ���� ������ �ر� (���� �ð��� �ִ� �ð��� ������ ���)
                isAchieve = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }

        // ���� �رݵ��� �ʾҴ� �����̾���, �̹��� ������ �����ߴٸ�
        if (isAchieve && PlayerPrefs.GetInt(_achieve.ToString())==0)
        {
            // ĳ���� �ر��ϰ�
            PlayerPrefs.SetInt(_achieve.ToString(), 1);
            // � �ȳ� ui�� ��� ������ ����
            for(int index=0; index<uiNotice.transform.childCount; index++)
            {
                // ���ڷ� ���� _achieve�� enum ��(int)�� index�� �����ϸ� �� index�� �ش��ϴ� �ڽ� ���ӿ�����Ʈ�� Ȱ��ȭ�ؼ� �ȳ������� ��� ����.
                bool isActive = (index == (int)_achieve);
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }

            // ĳ���� �ر� ui�� Ȱ��ȭ�ϰ� 5�ʵڿ� �ٽ� ��Ȱ��ȭ
            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        yield return wait;  // 5�� ��ٸ� �� �ȳ�â ��Ȱ��ȭ
        uiNotice.SetActive(false);
    }
}
