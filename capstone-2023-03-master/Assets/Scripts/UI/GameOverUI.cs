using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public void TitleClick()
    {
        SceneLoader.Instance.LoadScene("TitleScene");
    }    
}
