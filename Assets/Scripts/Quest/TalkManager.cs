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

        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    private void GenerateData()
    {
        //퀘스트 대화
        talkData.Add(10 + 1000, new string[]
        { $"총괄 안드로이드 : \n 삐릭, {DataManager.instance.nowPlayer.name}.\n 지금까지 최연소로 모든 훈련과정을 통과.\n 이제 마지막 훈련이 남음.",
        "총괄 안드로이드 : \n마지막 훈련명 '탈출'.\n다시 한 번 반복함, \n마지막 훈련은 '시설에서 탈출하라'임.",
        $"{DataManager.instance.nowPlayer.name} : \n탈출 후에는?","총괄 안드로이드 : \n자연스럽게 알게 될 거라고 적혀있음.\n일반 공격키 : CTRL\n점프 : ATL\n메뉴 : ESC\n인벤토리 : I\n나머지는 스스로 알아내야함.",
        $"{DataManager.instance.nowPlayer.name} : \n이 지옥같은 곳을 탈출해서 조직의 손아귀에서 벗어나자...."});

        talkData.Add(20 + 2000, new string[] { $"{DataManager.instance.nowPlayer.name} : 책이다.",
        "??? : 이 책을 읽고 있다면 무사히 이상한 괴물들을 없애고 왔겠군....\n이 앞으로 쭉 가면 지상으로 가는 입구가 있긴 있다.",
        "??? : 하지만, 당연히 너도 느꼈겠지만 괴물들이 득실거리고 있다.\n후배의 탈출을 위해 스킬 하나와 무기를 주겠다.",
        "??? : 쉬프트 키를 누르면 대쉬를 할 수 있을 것이다.\n그리고 인벤토리를 보면 무기가 있을거야.\n마우스 우클릭으로 착용하면 된다."});

        talkData.Add(30 + 2000, new string[] { $"{DataManager.instance.nowPlayer.name} : 또 그 책이다.",
        "??? : 어때 지상으로 나온 소감은? 생각했던 모습이랑 많이 다르겠지.\n하지만 구경할 시간은 없다구",
        "??? : 지상은 방사능으로 생긴 괴물들과 인간, 동물들이 서로 싸우고 있는 형국이지.\n물론 인간들끼리도 나뉘어 있지.",
        "??? : 우선 OOO-OO이 지점으로 가서 책장에 있는 비밀 문서들을 읽어봐라.\n훈련받았으니 어딘지 알겠지?",
        "??? : 우리가 만나는건 그 다음이다.",
        "시스템 : 튜토리얼은 끝났습니다.\n고생하셨습니다."});
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
                //퀘스트 진행 중 대사가 없을 때 
                //퀘스트 맨 처음 대사를 가지고 옴
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
            //id로 대화 talkIndex로 대화의 한 문장을 리턴
            return talkData[id][talkIndex];
        }
    }
}