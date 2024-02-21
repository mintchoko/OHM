using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

//Event System���� �߻���ų �̺�Ʈ�� �����ϴ� �������̽��� �� ������ ����
//UI�� ButtonEvents ������Ʈ�� ���� ��, �ش� UI ��ü�� ��ưó�� ����ϵ��� ��.
//�׸��� ���콺�� ���� �� �� �ƴ϶�, �ö��� ��, ����� �� � ���� �ٸ� ������ �ϵ��� ��.
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
