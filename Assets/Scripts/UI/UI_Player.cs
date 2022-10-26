using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : MonoBehaviour
{

    Image hp;
    Image sm;
    TextMeshProUGUI name;
    Player player;

    static UI_Player instance;
    public static UI_Player Instance
    {
        get => instance;
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        hp = transform.GetChild(3).GetComponent<Image>();
        sm = transform.GetChild(4).GetComponent<Image>();
        name = transform.GetChild(8).GetComponent<TextMeshProUGUI>();
        name.text = DataManager.instance.nowPlayer.name;
    }

    
    void Update()
    {
        hp.fillAmount = player.PlayerHP / player.MaxHP;
        sm.fillAmount = player.PlayerSM / player.MaxSM;
    }

    
}
