using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;

    public GameObject uiNotice;
    WaitForSecondsRealtime wait; // WaitForSeconds는 게임시간이라서 타임스케일을 0으로 하면 같이 멈추는데, Realtime은 실제 시간으로 시간을 계산함.

    enum Achieve
    {
        UnlockPotato,
        UnlockBean,
    }

    Achieve[] achieves;

    private void Awake()
    {
        achieves = (Achieve[]) Enum.GetValues(typeof(Achieve)); // enum의 모든 종류의 값을 배열로 받아옴
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
                // 적 10마리 이상 처치 시 해금
                isAchieve = GameManager.instance.kills >= 10;
                break;
            case Achieve.UnlockBean:
                // 생존 성공시 해금 (생존 시간이 최대 시간과 동일한 경우)
                isAchieve = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }

        // 원래 해금되지 않았던 업적이었고, 이번에 조건을 만족했다면
        if (isAchieve && PlayerPrefs.GetInt(_achieve.ToString())==0)
        {
            // 캐릭터 해금하고
            PlayerPrefs.SetInt(_achieve.ToString(), 1);
            // 어떤 안내 ui를 띄울 것인지 선택
            for(int index=0; index<uiNotice.transform.childCount; index++)
            {
                // 인자로 받은 _achieve의 enum 값(int)이 index와 동일하면 그 index에 해당하는 자식 게임오브젝트를 활성화해서 안내문구를 띄울 거임.
                bool isActive = (index == (int)_achieve);
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }

            // 캐릭터 해금 ui를 활성화하고 5초뒤에 다시 비활성화
            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        yield return wait;  // 5초 기다린 후 안내창 비활성화
        uiNotice.SetActive(false);
    }
}
