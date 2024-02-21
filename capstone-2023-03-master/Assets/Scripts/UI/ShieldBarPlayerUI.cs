using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBarPlayerUI : MonoBehaviour
{

    public BattleData ShieldData;
    public bool HasAnimationWhenHealthChanges = true;
    public float AnimationDuration = 0.1f;

    TextMeshProUGUI TextUI;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        TextUI = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();
        ShieldData = GameObject.Find("BattleData").GetComponent<BattleData>();
    }

    // Update is called once per frame
    void Update()
    {
        ShieldData = GameObject.Find("BattleData").GetComponent<BattleData>();
        ChangeShieldText();
    }

    void ChangeShieldText() // Text�� �ؽ�Ʈ ������ Shield�� �ٲ��ִ� �Լ�
    {
        TextUI.text = ShieldData.Shield.ToString();
        if (ShieldData.Shield == 0)
        {
            image.fillAmount = 0;
        }
        else
        {
            image.fillAmount = 1;
        }
    }
}
