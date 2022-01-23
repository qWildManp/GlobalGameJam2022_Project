using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBar : MonoBehaviour
{
    public Queue<Color> colorBar;
    public GameObject colorBarText;
    public List<GameObject> colorBarUI;
    public GameObject currentColorUI;
    public Color currentColor;
    public bool hasChangeColor = false;
    // Start is called before the first frame update
    void Start()
    {
        colorBar = new Queue<Color>();
        for (int i = 0; i < 5; i++){
            Color newColor = GenrateRadomColor();
            colorBar.Enqueue(newColor);
        }
        currentColor = GenrateRadomColor();
    }

    // Update is called once per frame
    void Update()
    {
        ShowColorBar();
        
    }

    public void UseColor(int num)
    {
        for(int i = 0; i< num; i++)
        {
            hasChangeColor = true;
            currentColor = colorBar.Dequeue();
            Color newColor = GenrateRadomColor();
            colorBar.Enqueue(newColor);
        }
    }

    private Color GenrateRadomColor()
    {
        float rnd_num = Random.Range(0, 10);
        if(rnd_num > 4.999999f)
        {
            return Color.black;
        }
        else
        {
            return Color.white;
        }
    }

    private void ShowColorBar()
    {
        currentColorUI.GetComponent<Image>().color = currentColor;
        int i = 0;
        foreach(Color color in colorBar)
        {
            colorBarUI[i].GetComponent<Image>().color = color;
            i += 1;
        }
    }

    public void IceBallEffect()
    {
        for(int i = 0; i< 3; i++)
        {
            colorBar.Dequeue();
        }
        Queue<Color> tmpcolorBar = colorBar;
        colorBar.Clear();
        for (int i = 0; i < 5; i++)
        {
            if (i < 3)
            {
                colorBar.Enqueue(Color.black);
            }
            else{
                colorBar.Enqueue(tmpcolorBar.Dequeue());
            }
        }
        currentColor = Color.black;
    }
}
