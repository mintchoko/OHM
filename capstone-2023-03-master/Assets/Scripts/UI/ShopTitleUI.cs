using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopTitleUI : MonoBehaviour
{
    [SerializeField]
    public Button CardShopButton; //카드 상점 들어가기
    [SerializeField]
    public Button CardDiscardButton; //카드 제거 들어가기
    [SerializeField]
    public Button ExitButton; //나가기


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
