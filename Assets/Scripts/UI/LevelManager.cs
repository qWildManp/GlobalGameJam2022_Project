using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update


        void Start()
        {
            LoadLevel();
        }

        private void LoadLevel()
        {
            //上次退出游戏时保存的游戏关卡ID，如果第一次进入默认为1
            int levelId = PlayerPrefs.GetInt("level", 1);
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i + 1 > levelId)
                {
                    //没通关的关卡
                    transform.GetChild(i).GetComponent<LevelItem>().Init(i + 1, true);
                }
                else
                {
                    //通关的关卡
                    transform.GetChild(i).GetComponent<LevelItem>().Init(i + 1, false);
                }
            }
        }

}
