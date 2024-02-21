using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayerUI : MonoBehaviour
{

    public BattleData HealthData;

    public bool HasAnimationWhenHealthChanges = true;
    public float AnimationDuration = 0.1f;

    TextMeshProUGUI TextUI;
    Image image;


    
    // Start is called before the first frame update
    void Start()
    {
        TextUI = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();
        HealthData = GameObject.Find("BattleData").GetComponent<BattleData>();

    }

    // Update is called once per frame
    void Update()
    {
        HealthData = GameObject.Find("BattleData").GetComponent<BattleData>();
        ChangeHPText();
    }

    void ChangeHPText() // Text�� �ؽ�Ʈ ������ CurrentHealth / MaximumHealth�� �ٲ��ִ� �Լ�
    {

        if (HealthData.IsAlive)
        {
            TextUI.text = HealthData.CurrentHealth + " / " + HealthData.MaximumHealth;
        }
        else
        {
            TextUI.text = "Dead";
        }

        image.fillAmount = HealthData.CurrentHealth / HealthData.MaximumHealth;
    }

}
