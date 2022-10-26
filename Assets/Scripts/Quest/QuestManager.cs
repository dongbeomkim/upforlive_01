using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//����Ʈ �ѹ��� ������ ��ũ��Ʈ
public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;
    public static QuestManager Instance { get { return instance; } }

    //����Ʈ ���̵�
    public int questId;
    public int questActionIndex;

    public int killCount = 0;
    public static System.Action CheckKillCount;

    public int bosskillCount = 0;
    public static System.Action BossKillCount;

    public int checkObject = 0;
    public static System.Action CObject;

    //����Ʈ �����͸� �ҷ��� ����Ʈ
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
            DontDestroyOnLoad(this.gameObject);     // ���� ����Ǵ��� ���� ������Ʈ�� ������� �ʰ� ���ִ� �Լ�
        }
        else
        {
            // ���� Gamemanager�� ������ �����ƴ�.
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Initialize()
    {
        //���� ������ �� �����ũ�� ����Ʈ �ҷ�����
        //questList�� ����ϱ� ���� �ʱ�ȭ ���ְ�
        questList = new Dictionary<int, QuestData>();

        //�� �Լ��� ���ؼ� ������
        GenerateData();

        inventory = FindObjectOfType<Inventory>();

        ////��� ų�� üũ�� ��������Ʈ
        //CheckKillCount = () => { checkkillcount(); };
        //
        ////���� ų üũ�� ��������Ʈ
        //BossKillCount = () => { BossskillCount(); };
        //
        //CObject = () => { CheckObject(); };
    }

    //�ʱ�ȭ�� questList�� ����ϱ� ���� �Լ�
    private void GenerateData()
    {
        //����Ʈ�� Add ��ɾ�� ���ϴ� ���� ����
        //10 - ����Ʈ ���� �ѹ�, ����Ʈ����Ÿ�� ����Ʈ ��� ���õ� ���ǽ� ��ȣ�� �޾ƿ�?
        questList.Add(10, new QuestData("������ �Ʒ�", new int[] { 1000 }));

        questList.Add(20, new QuestData("������ ����", new int[] { 2000 }));

        questList.Add(30, new QuestData("������?", new int[] { 2000 }));

        questList.Add(40, new QuestData("ó�� �� ����, �׸��� ������", new int[] { 2000 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        //���� ��ũ Ÿ��
        if (id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        //��ȭ �Ϸ� �� ���� ����Ʈ
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
    //        Debug.Log("���� ����� �����ϴ�.");
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