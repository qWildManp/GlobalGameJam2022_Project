using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMangerInLevel: MonoBehaviour
{
    public GameObject levelMenuUI;
    public GameObject healthBarUI;
    public GameObject shieldBarUI;
    public PlayerStatus playerStatus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHealthUI();
        ChangeShieldUI();
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

    private void ChangeHealthUI()
    {
        float currentHealth = playerStatus.health;
        float maxHealth = playerStatus.maxHealth;
        float currentRatio = currentHealth / maxHealth;

        healthBarUI.GetComponent<Image>().fillAmount = currentRatio;
    }

    private void ChangeShieldUI()
    {
        float currentShiled = playerStatus.shield;
        float maxShiled = 3;
        float currentRatio = currentShiled / maxShiled;

        shieldBarUI.GetComponent<Image>().fillAmount = currentRatio;
    }
}
