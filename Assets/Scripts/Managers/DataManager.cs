using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//저장할 데이터가 존재해야함
//데이터 -> Json
//Json을 외부에 저장

//외부 Json을 가져옴
//Json을 데이터 형태로 변환
//가져온 데이터를 사용

public class PlayerData
{
    //이름 저장
    public string name;
}

public class DataManager : MonoBehaviour
{
    static public DataManager instance;

    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath + "/";
    }

    
    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
