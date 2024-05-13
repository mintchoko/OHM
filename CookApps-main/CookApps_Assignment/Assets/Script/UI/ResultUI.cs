using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private BoardUI boardUI;
    [SerializeField]
    private Button BackButton;
    [SerializeField]
    private TextMeshProUGUI ResultText;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeText();
    }

    public void ChangeText(){
        if(BattleData.Instance.end){
            if(BattleData.Instance.result){
                ResultText.text = "Player Win";
            }
            else{
                ResultText.text = "Enemy Win";
            }
        }
        else{
            ResultText.text = "Draw";
        }
    }
    public void BackClick(){
        BattleData.Instance.turn++;
        boardUI.arrange();
        gameObject.SetActive(false);
        BattleData.Instance.Reset();
    }
}
