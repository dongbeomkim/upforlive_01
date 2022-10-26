using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    public int QuestNumber1 = 0;

    private static TalkManager instance;
    public static TalkManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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

        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    private void GenerateData()
    {
        //����Ʈ ��ȭ
        talkData.Add(10 + 1000, new string[]
        { $"�Ѱ� �ȵ���̵� : \n �߸�, {DataManager.instance.nowPlayer.name}.\n ���ݱ��� �ֿ��ҷ� ��� �Ʒð����� ���.\n ���� ������ �Ʒ��� ����.",
        "�Ѱ� �ȵ���̵� : \n������ �Ʒø� 'Ż��'.\n�ٽ� �� �� �ݺ���, \n������ �Ʒ��� '�ü����� Ż���϶�'��.",
        $"{DataManager.instance.nowPlayer.name} : \nŻ�� �Ŀ���?","�Ѱ� �ȵ���̵� : \n�ڿ������� �˰� �� �Ŷ�� ��������.\n�Ϲ� ����Ű : CTRL\n���� : ATL\n�޴� : ESC\n�κ��丮 : I\n�������� ������ �˾Ƴ�����.",
        $"{DataManager.instance.nowPlayer.name} : \n�� �������� ���� Ż���ؼ� ������ �վƱͿ��� �����...."});

        talkData.Add(20 + 2000, new string[] { $"{DataManager.instance.nowPlayer.name} : å�̴�.",
        "??? : �� å�� �а� �ִٸ� ������ �̻��� �������� ���ְ� �԰ڱ�....\n�� ������ �� ���� �������� ���� �Ա��� �ֱ� �ִ�.",
        "??? : ������, �翬�� �ʵ� ���������� �������� ��ǰŸ��� �ִ�.\n�Ĺ��� Ż���� ���� ��ų �ϳ��� ���⸦ �ְڴ�.",
        "??? : ����Ʈ Ű�� ������ �뽬�� �� �� ���� ���̴�.\n�׸��� �κ��丮�� ���� ���Ⱑ �����ž�.\n���콺 ��Ŭ������ �����ϸ� �ȴ�."});

        talkData.Add(30 + 2000, new string[] { $"{DataManager.instance.nowPlayer.name} : �� �� å�̴�.",
        "??? : � �������� ���� �Ұ���? �����ߴ� ����̶� ���� �ٸ�����.\n������ ������ �ð��� ���ٱ�",
        "??? : ������ �������� ���� ������� �ΰ�, �������� ���� �ο�� �ִ� ��������.\n���� �ΰ��鳢���� ������ ����.",
        "??? : �켱 OOO-OO�� �������� ���� å�忡 �ִ� ��� �������� �о����.\n�Ʒù޾����� ����� �˰���?",
        "??? : �츮�� �����°� �� �����̴�.",
        "�ý��� : Ʃ�丮���� �������ϴ�.\n����ϼ̽��ϴ�."});
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                if (talkIndex == talkData[id - id % 100].Length)
                {
                    return null;
                }
                else
                {
                    return talkData[id - id % 100][talkIndex];
                }
            }
            else
            {
                //����Ʈ ���� �� ��簡 ���� �� 
                //����Ʈ �� ó�� ��縦 ������ ��
                if (talkIndex == talkData[id - id % 10].Length)
                {
                    return null;
                }
                else
                {
                    return talkData[id - id % 10][talkIndex];
                }
            }
        }

        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            //id�� ��ȭ talkIndex�� ��ȭ�� �� ������ ����
            return talkData[id][talkIndex];
        }
    }
}