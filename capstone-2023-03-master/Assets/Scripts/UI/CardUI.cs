using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DataStructs;
using UnityEngine.EventSystems;
using System;
using System.Collections;



//ī�� UI�� ĵ������ �����Ƿ�(�ִ� ĵ���� ���� ���� UI��) UIManager�� ȣ���ϸ� �ȵ� ����
public class CardUI : BaseUI, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    //UI�� ����� ī�� ��ü
    public CardStruct Card;

    //ī�� UI�� �̹���, �ؽ�Ʈ ������Ʈ��
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private TMP_Text costText;
    [SerializeField]
    private TMP_Text descriptionText;

    private float scaleOnHover = 1.1f;
    private Vector3 originalScale = Vector3.one;

    public Action<CardUI> OnCardClicked; //ī�� UI Ŭ���� ����� �Լ���. �ٸ� UI���� ���⿡ ����� �� ����
    public Action<CardUI> OnCardEntered; //ī�� UI�� ���콺�� ���� �� ����� �Լ���. �ٸ� UI���� ���⿡ ����� �� ����
    public Action<CardUI> OnCardExited; //ī�� UI���� ���콺�� ���� �� �Լ���. �ٸ� UI���� ���⿡ ����� �� ����


    public void OnPointerDown(PointerEventData eventData) //ī�� Ŭ�� �� ����
    {
        OnCardClicked?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData) //ī�� ���콺 �ø� �� ����
    {
        OnCardEntered?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData) //ī�� ���콺 Ż�� �� ����
    {

        OnCardExited?.Invoke(this); 
    }



    //���ڷ� ���� ī���� �����͸� UI�� �����ִ� �Լ�
    public void ShowCardData(CardStruct showCard)
    {
        Card = showCard;
        nameText.text = Card.name;
        descriptionText.text = Card.description;
        costText.text =  Card.cost == 99 ? "X" : Card.cost.ToString();


        switch(Card.rarity)
        {
            case 0:
                nameText.color = Color.white; break;
            case 1:
                nameText.color = Color.magenta; break;
            case 2:
                nameText.color = Color.yellow; break;
        }

        //ī�忡 �Ӽ��� ����� �̹��� ��������
        if (Card.attribute != null && Card.attribute != "") image.sprite = GameData.Instance.SpriteDic[Card.attribute];
        else
        {  
            //���Ӽ��� ���� �ٸ� �ʵ�� �̹��� ���� 
        }

    }

    public void CardBig() //�ش� ī�� UI Ȯ��
    {
        StopAllCoroutines(); // ���� ���� ���� ��� �ڷ�ƾ�� ����ϴ�.
        StartCoroutine(ChangeScale(Vector3.one * 1.1f)); // ���ο� ũ��� õõ�� ��ȯ�ϴ� �ڷ�ƾ�� �����մϴ�.
    }

    public void CardSmall() //�ش� ī�� UI ���
    {
        StopAllCoroutines(); // ���� ���� ���� ��� �ڷ�ƾ�� ����ϴ�.
        StartCoroutine(ChangeScale(Vector3.one)); // ���ο� ũ��� õõ�� ��ȯ�ϴ� �ڷ�ƾ�� �����մϴ�.
    }

    private IEnumerator ChangeScale(Vector3 targetScale) // �־��� ũ��� õõ�� ��ȯ
    {
        while (transform.localScale != targetScale) // �־��� ũ�Ⱑ �� ������
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 0.5f);
            yield return null;
        }
    }

}
