using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

//Event System에서 발생시킬 이벤트를 구독하는 인터페이스를 한 곳에서 구현
//UI에 ButtonEvents 컴포넌트를 붙일 시, 해당 UI 자체를 버튼처럼 기능하도록 함.
//그리고 마우스가 눌릴 때 뿐 아니라, 올라갔을 때, 벗어났을 때 등에 따라 다른 동작을 하도록 함.
public class ButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{

    public event Action<PointerEventData> PointerEnter;
    public event Action<PointerEventData> PointerDown;
    public event Action<PointerEventData> PointerExit;


    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter?.Invoke(eventData);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown?.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit?.Invoke(eventData);
    }
}
