using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UI_MenuButton : MonoBehaviour
{

    public Button MenuPanel, Inventory, ReturnGameButton, GameOverButton;
    GameObject menuPanel;
    GameObject inventory;

    private void Awake()
    {
        menuPanel = GameObject.Find("PauseUI");
        inventory = GameObject.Find("Inventory");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuPanel.activeSelf)
            {
                menuPanel.SetActive(false);
            }
            else
            {
                menuPanel.SetActive(true);
            }
            
        }


        if(Input.GetKeyDown(KeyCode.I))
        {
            if(inventory.activeSelf)
            {
                inventory.SetActive(false);
            }
            else
            {
                inventory.SetActive(true);
            }
        }

        
        
    }

    void Start()
    {
        MenuPanel.onClick.AddListener(OnMenuPanel);
        Inventory.onClick.AddListener(OnInventory);

        ReturnGameButton.onClick.AddListener(OffMenuPanel);
        GameOverButton.onClick.AddListener(GameExit);
    }

    public void OnMenuPanel()
    {
        menuPanel.SetActive(true);
    }

    public void OnInventory()
    {
        inventory.SetActive(true);
    }


    

    public void OffMenuPanel()
    {
        menuPanel.SetActive(false);
    }

    void GameExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
