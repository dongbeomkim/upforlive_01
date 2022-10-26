using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class SceneLoad : MonoBehaviour
{
    public static int nextScene;

    public Slider progressbar;
    public Text loadText;

    Player player;
    UI_Player uI;

    void Start()
    {
        player = FindObjectOfType<Player>();
        uI = FindObjectOfType<UI_Player>();
        StartCoroutine(LoadScene());
    }

    
    void Update()
    {
        
    }

    public static void LoadScene(int nextSceneName)
    {
        nextScene = nextSceneName;
        SceneManager.LoadScene(4);
    }

    IEnumerator LoadScene()
    {
        yield return null;

        if(player != null)
        {
            player.currentMap = nextScene;
        }
        
        if(uI != null)
        {
            uI.gameObject.SetActive(false);
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            yield return null;
            if(progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }
            else if(operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if(operation.progress >=0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if(progressbar.value >= 1f)
            {
                loadText.text = "Loading Complete....";
            }

            if(progressbar.value>=1f && operation.progress>=0.9f)
            {
                operation.allowSceneActivation=true;
                if(uI !=null)
                    uI.gameObject.SetActive(true);
            }
        }
    }
}
