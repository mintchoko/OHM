using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatusText : MonoBehaviour
{
    public TMP_Text DisplayText; // Unity Inspector에서 연결할 Text UI 오브젝트

    void Start()
    {
        PrintGameStart();
    }

    void PrintGameStart()
    {
        StartCoroutine(ShowText("Game Start"));
    }

    public void PrintTurn(string name) 
    {
        Debug.Log("PrintTurn");
        SoundManager.Instance.Play("Sounds/TurnChange");
        string text = name + "'s Turn!";
        StartCoroutine(ShowText(text));
    }

    public void PrintGameResult(bool Turn) 
    {
        Debug.Log("PrintGameResult");
        string text = "Game Winner : ";
        if (Turn) {
            text += "Player 1";
        } else {
            text += "Player 2";
        }
        DisplayText.text = text;
        SoundManager.Instance.Play("Sounds/GameResult");
    }
    IEnumerator ShowText(string text)
    {
        DisplayText.text = text; // 표시할 텍스트 설정

        yield return new WaitForSeconds(2); // 2초 대기

        DisplayText.text = ""; // 텍스트 숨기기
    }

    void Update()
    {
        
    }
}
