using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopTitleUI : MonoBehaviour
{
    [SerializeField]
    public Button CardShopButton; //ī�� ���� ����
    [SerializeField]
    public Button CardDiscardButton; //ī�� ���� ����
    [SerializeField]
    public Button ExitButton; //������


    public void ShopPurchaseClick()
    {
        UIManager.Instance.ShowUI("ShopPurchaseUI");
    }

    public void ShopDiscardClick()
    {
        UIManager.Instance.ShowUI("ShopDiscardUI");
    }

    public void ExitClick()
    {
        UIManager.Instance.HideUI(name);
    }    

}
