using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//퀘스트 넘버를 관리할 스크립트
public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;
    public static QuestManager Instance { get { return instance; } }

    //퀘스트 아이디
    public int questId;
    public int questActionIndex;

    public int killCount = 0;
    public static System.Action CheckKillCount;

    public int bosskillCount = 0;
    public static System.Action BossKillCount;

    public int checkObject = 0;
    public static System.Action CObject;

    //퀘스트 데이터를 불러올 리스트
    Dictionary<int, QuestData> questList;

    int talkIndex;
    public int TalkIndex
    {
        get { return talkIndex; }
        set { talkIndex = value; }
    }

    Inventory inventory;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Initialize();
            DontDestroyOnLoad(this.gameObject);     // 씬이 변경되더라도 게임 오브젝트가 사라지기 않게 해주는 함수
        }
        else
        {
            // 씬의 Gamemanager가 여러번 생성됐다.
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Initialize()
    {
        //게임 시작할 때 어웨이크로 리스트 불러와줌
        //questList를 사용하기 위해 초기화 해주고
        questList = new Dictionary<int, QuestData>();

        //이 함수를 통해서 실행함
        GenerateData();

        inventory = FindObjectOfType<Inventory>();

        ////고블린 킬수 체크용 델리게이트
        //CheckKillCount = () => { checkkillcount(); };
        //
        ////보스 킬 체크용 델리게이트
        //BossKillCount = () => { BossskillCount(); };
        //
        //CObject = () => { CheckObject(); };
    }

    //초기화된 questList를 사용하기 위한 함수
    private void GenerateData()
    {
        //리스트에 Add 명령어로 더하는 것은 동일
        //10 - 퀘스트 고유 넘버, 퀘스트데이타는 퀘스트 명과 관련된 엔피시 번호를 받아옴?
        questList.Add(10, new QuestData("마지막 훈련", new int[] { 1000 }));

        questList.Add(20, new QuestData("지상을 향해", new int[] { 2000 }));

        questList.Add(30, new QuestData("누구지?", new int[] { 2000 }));

        questList.Add(40, new QuestData("처음 본 지상, 그리고 목적지", new int[] { 2000 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        //다음 토크 타겟
        if (id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        //대화 완료 및 다음 퀘스트
        if (questActionIndex == questList[questId].npcId.Length)
        {
            NextQuest();
        }

        QuestObject();
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    public void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void QuestObject()
    {
        if (questId == 20 && questActionIndex == 0)
        {
            GameObject obj = GameObject.Find("MechanicNpc");
            ParticleSystem particleSystem = obj.GetComponentInChildren<ParticleSystem>();
            particleSystem.Play();
            Destroy(obj, 1f);
        }
        else if (questId == 30 && questActionIndex == 0)
        {
            GameManager.Instance.Player.isDash = true;

            GameObject obj = Resources.Load("Item/BasicSmallKnife") as GameObject;
            Instantiate(obj, transform.position, Quaternion.identity);

            inventory.dashicon.color = new Color(1, 1, 1, 1);
            //GameManager.Instance.Inventory.dashicon.color = new Color(1, 1, 1, 1);

            GameObject book = GameObject.Find("Book");
            Destroy(book);
        }
        else if (questId == 40 && questActionIndex == 0)
        {
            GameObject obj = GameObject.Find("Book");
            Destroy(obj);

            SceneLoad.LoadScene(0);
        }
    }

    //public void CheckObject()
    //{
    //
    //    if (questId == 20  && questActionIndex >= 0)
    //    {
    //        GameObject obj = GameObject.Find("MechanicNpc");
    //        Destroy(obj);
    //    }
    //    else if (questId == 30 && questActionIndex == 0)
    //    {
    //        GameObject obj = GameObject.Find("MechanicNpc");
    //        Destroy(obj);
    //
    //        GameManager.Instance.Player.isDash = true;
    //        GameObject objj = GameObject.Find("Book");
    //        Destroy(objj);
    //    }
    //    else if (questId == 40 && questActionIndex == 0)
    //    {
    //        GameObject obj = GameObject.Find("MechanicNpc");
    //        Destroy(obj);
    //
    //        GameManager.Instance.Player.isDash = true;
    //        GameObject objj = GameObject.Find("Book");
    //        Destroy(objj);
    //    }
    //    else
    //    {
    //        Debug.Log("삭제 대상이 없습니다.");
    //    }
    //}
    //
    //void checkkillcount()
    //{
    //    killCount++;
    //}
    //
    //void BossskillCount()
    //{
    //    bosskillCount++;
    //}

}