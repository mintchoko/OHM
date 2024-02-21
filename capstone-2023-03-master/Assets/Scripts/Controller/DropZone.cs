using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DataStructs;

public class DropZone : MonoBehaviour, IDropHandler
{
    GameObject CardUI;
    CardStruct Card;
    NoticeUI noticeUI;
    BattleUI battleUI;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        CardUI = eventData.pointerDrag;
        Card = CardUI.GetComponent<CardUI>().Card;
        battleUI = FindObjectOfType<BattleUI>();
        if(Card != null && (Card.cost <= BattleData.Instance.CurrentEnergy || Card.cost == 99))
        {
            Battle.UseCard(Card);
            battleUI.PlayerAttack();
            Destroy(CardUI);
        }
        else
        {
            noticeUI = FindObjectOfType<NoticeUI>();
            noticeUI.ShowNotice("에너지가 부족합니다 !");
        }
    }

}
