using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int oldSceneIndex;

    public TalkManager talkManager;

    public QuestManager questManager;
    public QuestManager Questmanager
    {
        get => questManager;
    }

    static GameManager instance;
    public static GameManager Instance
    {
        get => instance;
    }

    public GameObject talkPanel;
    public GameObject TalkPanel
    {
        get
        {
            return talkPanel;
        }
    }

    TextMeshProUGUI talkText;
    public TextMeshProUGUI TalkText
    {
        get => talkText;
        set
        {
            talkText = value;
        }
    }

    public Button talkButton;

    Player player;
    public Player Player => player;

    public Inventory Inventory { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Init();
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public Button SaveGameButton;
    public UI_MenuButton menuButton;

    void Init()
    {
        SceneManager.sceneLoaded += OnstageStart;

        menuButton = GetComponent<UI_MenuButton>();
        SaveGameButton.onClick.AddListener(SaveGame);
        talkText = talkPanel.GetComponentInChildren<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
        Inventory = FindObjectOfType<Inventory>();
    }

    

    private void Start()
    {
        talkButton.onClick.AddListener(ExitTalkPanel);
        talkPanel.SetActive(false);
    }

    void OnstageStart(Scene arg0, LoadSceneMode arg1)
    {
        if (oldSceneIndex != arg0.buildIndex)
        {
            oldSceneIndex = arg0.buildIndex; //옛날 씬 인덱스를 지금 씬 인덱스로 변경
            
        }
    }

    void ExitTalkPanel()
    {
        talkPanel.SetActive(false);
    }

    void SaveGame()
    {
        Debug.Log("게임 저장 실험");
        DataManager.instance.SaveData();
    }
}


