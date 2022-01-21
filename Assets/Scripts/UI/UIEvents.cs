using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    [SerializeField] GameObject levelSelectUI;
    [SerializeField] GameObject mainMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int showLevelUI = PlayerPrefs.GetInt("levelUI",0);
        if (showLevelUI == 1)
        {
            ShowSelectLevel();
        }
        else
        {
            BackToMenu();
        }
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("levelUI", 1);
    }
    public void ShowSelectLevel()
    {
        levelSelectUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }
    public void BackToMenu()
    {
        PlayerPrefs.SetInt("levelUI", 0);
        levelSelectUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void ExitGame()
    {
    //if UNITY_EDITOR
        EditorApplication.isPlaying = false;
   //else
        Application.Quit();
    }
}
