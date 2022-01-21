using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class LevelItem : MonoBehaviour
{
    /// <summary>
    /// 关卡ID
    /// </summary>
    private int LevelId;
    /// <summary>
    /// 创建按钮
    /// </summary>
    private Button btn;

    // Start is called before the first frame update
    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //Set btn interaction
    public void Init(int id, bool isLock)
    {
        LevelId = id;
        if (isLock)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }
    //Click on level btn
    private void OnClick()
    {
        SceneManager.LoadScene(LevelId);
    }

}