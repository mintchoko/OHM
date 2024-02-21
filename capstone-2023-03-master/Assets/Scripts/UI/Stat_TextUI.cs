using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stat_TextUI : MonoBehaviour
{
    public BattleData StatData;
    TextMeshProUGUI TextUI;

    void Start()
    {
        TextUI = GetComponentInChildren<TextMeshProUGUI>();
        StatData = GameObject.Find("BattleData").GetComponent<BattleData>();

    }

    void Update()
    {
        StatData = GameObject.Find("BattleData").GetComponent<BattleData>();
        UpdateStatText();
    }

    void UpdateStatText()
    {
        string Text;
        Text = "�� " + StatData.Str.ToString() + "\n���� " + StatData.Int.ToString();
        TextUI.text = Text;
    }
}
