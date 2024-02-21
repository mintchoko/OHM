using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyStatUI : MonoBehaviour
{
    public EnemyData StatData;
    public int EnemyNum;
    TextMeshProUGUI TextUI;
    string Text;
    string CurrentText;
    // Start is called before the first frame update
    void Start()
    {
        TextUI = GetComponentInChildren<TextMeshProUGUI>();
        StatData = GameObject.Find("EnemyData").GetComponent<EnemyData>();
    }

    // Update is called once per frame
    void Update()
    {
        StatData = GameObject.Find("EnemyData").GetComponent<EnemyData>();
        UpdateText();
    }

    public void init(int num)
    {
        EnemyNum = num-1;
    }

    public void UpdateText() // 
    {
        Text = "";
        if (StatData.Fire[EnemyNum] > 0)
        {
            Text += "화상 " + StatData.Fire[EnemyNum].ToString() + "\n";
        }
        if (StatData.Ice[EnemyNum] > 0)
        {
            Text += "동상 " + StatData.Ice[EnemyNum].ToString();
        }
        TextUI.text = Text;
    }
}
