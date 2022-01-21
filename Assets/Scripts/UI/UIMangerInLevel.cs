using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMangerInLevel: MonoBehaviour
{
    public GameObject levelMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        PlayerPrefs.SetInt("levelUI", 0);
        SceneManager.LoadScene(0);
    }

    public void BackToLevelSelect()
    {
        PlayerPrefs.SetInt("levelUI", 1);
        SceneManager.LoadScene(0);
    }
    public void ShowLevelMenu()
    {
        levelMenuUI.SetActive(true);
    }
    public void CloseLevelMenu()
    {
        levelMenuUI.SetActive(false);
    }
    public void FinishLevel()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            int new_level_id = PlayerPrefs.GetInt("level") + 1;
            PlayerPrefs.SetInt("level", new_level_id);
        }
        else
        {
            PlayerPrefs.SetInt("level", 2);
        }
    }
}
