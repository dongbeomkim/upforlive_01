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


    Inventory inven;
    public Inventory Inventory => inven;

    MainCamera mainCamera;
    public MainCamera MainCamera => mainCamera;



    private void Awake()
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
            oldSceneIndex = arg0.buildIndex; //øæ≥Ø æ¿ ¿Œµ¶Ω∫∏¶ ¡ˆ±› æ¿ ¿Œµ¶Ω∫∑Œ ∫Ø∞Ê
            
        }
    }

    void ExitTalkPanel()
    {
        talkPanel.SetActive(false);
    }

    void SaveGame()
    {
        Debug.Log("∞‘¿” ¿˙¿Â Ω««Ë");
        DataManager.instance.SaveData();
    }
}


