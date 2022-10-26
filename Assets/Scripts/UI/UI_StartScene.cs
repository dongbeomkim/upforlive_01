using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class UI_StartScene : MonoBehaviour
{
    public GameObject creat;
    public Text[] slotText;
    public Text newPlayerName;

    bool[] savefile = new bool[3];

    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            if(File.Exists(DataManager.instance.path + $"{i}"))
            {
                savefile[i] = true;
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData();
                slotText[i].text = DataManager.instance.nowPlayer.name;
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }
        DataManager.instance.DataClear();
    }

    void Update()
    {
        
    }

    public void Slot(int num)
    {
        DataManager.instance.nowSlot = num;

        if(savefile[num])
        {
            DataManager.instance.LoadData();
            GoGame();
        }
        else
        {
            Create();
        }

    }

    public void Create()
    {
        creat.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        if (!savefile[DataManager.instance.nowSlot])
        {
            DataManager.instance.nowPlayer.name = newPlayerName.text;
            DataManager.instance.SaveData();
        }
        SceneLoad.LoadScene(1);
    }

    public void GameOver()
    {
        Application.Quit();
    }
}
