using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//������ �����Ͱ� �����ؾ���
//������ -> Json
//Json�� �ܺο� ����

//�ܺ� Json�� ������
//Json�� ������ ���·� ��ȯ
//������ �����͸� ���

public class PlayerData
{
    //�̸� ����
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
