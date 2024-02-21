using DataStructs;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;


//�������� ī�� ��� UI
public class ShopPurchaseUI : MonoBehaviour
{
    [SerializeField]
    public TMP_Text reCostText; //���Ѻ��
    [SerializeField]
    public TMP_Text playerMoneyText; //�÷��̾� ��
    [SerializeField]
    public GameObject shopCards; //ī�� ������

    List<CardStruct> showedCardList;

    void Awake()
    {
        UpdateUI();
    }

    private void OnEnable()
    {
        UpdateUI();
        PlayerData.Instance.OnDataChange += UpdateUI; //���Ȱ��� ��ȭ �� UI ����
        ShopData.Instance.OnDataChange += UpdateUI; //���� �����Ϳ� ��ȭ �� UI ����
    }
    private void OnDisable()
    {
        PlayerData.Instance.OnDataChange -= UpdateUI;
        ShopData.Instance.OnDataChange -= UpdateUI;
    }

    //UI ���ΰ�ħ
    public void UpdateUI()
    {
        reCostText.text = ShopData.Instance.RerollCost.ToString();
        playerMoneyText.text = PlayerData.Instance.Money.ToString();
        ShowShopCards();
    }

    //ǥ������ ī�� ����
    private void ClearCards()
    {
        for (int i = 0; i < shopCards.transform.childCount; i++)
        {
            AssetLoader.Instance.Destroy(shopCards.transform.GetChild(i).gameObject);
        }
    }


    public void ShowShopCards()
    {

        ClearCards();

        showedCardList = ShopData.Instance.ShopCardsList;

        //CardUI �������� ��������, shopCards�� ���� ������Ʈ��� �߰�
        //���� �ؽ�Ʈ�� ������ cardUI �Ʒ��� �ؽ�Ʈ�� ���

        for(int i = 0; i < showedCardList.Count; i++)
        {
            CardUI cardUI = AssetLoader.Instance.Instantiate("Prefabs/UI/CardUI", shopCards.transform).GetComponent<CardUI>();
            cardUI.ShowCardData(showedCardList[i]); //���� ī�� UI ǥ��

            int price; //ī���� ����� ���� ������ ��ȯ�մϴ�.

            switch (showedCardList[i].rarity)
            {
                case 0:
                    price = Random.Range(8, 11) * 5; break; //40, 45, 50
                case 1:
                    price = Random.Range(13, 16) * 5; break; //65, 70, 75
                case 2:
                    price = Random.Range(24, 30) * 5; break; //120, 125... 145, 150
                default:
                    price = 99999; break;
            }

            //���� �ؽ�Ʈ �����ͼ� ����
            TMP_Text priceText = cardUI.transform.Find("Base/PriceText").GetComponent<TextMeshProUGUI>();
            priceText.gameObject.SetActive(true);   
            priceText.text = $"{price}��";

            //���콺 �̺�Ʈ ���
            cardUI.OnCardClicked += (cardUI) =>
            {
                // �÷��̾ ����� ���� ������ �ִ��� Ȯ��
                if (PlayerData.Instance.Money >= price)
                {
                    PlayerData.Instance.Money -= price; // ���� ����
                    AssetLoader.Instance.Destroy(cardUI.gameObject); //ī�� UI ����

                    PlayerData.Instance.Deck.Add(cardUI.Card); // ī�带 ���� �߰�
                    PlayerData.Instance.DeckChanged(); //�� ���� �˷��� UI ���ΰ�ħ�ϵ���!

                    ShopData.Instance.ShopCardsList.Remove(cardUI.Card);  //�������� ī�� ����
                    ShopData.Instance.DataChanged(); //���� ������ ���� �˷��� UI ���ΰ�ħ�ϵ���!
                }
                else
                {
                    Debug.Log("���� �����մϴ�.");
                }
            };
            cardUI.OnCardEntered += (cardUI) => { cardUI.CardBig(); };
            cardUI.OnCardExited += (cardUI) => { cardUI.CardSmall(); };
        }
    }

    public void RerollClick()
    {
        // �÷��̾ �����ϱ� ����� ���� ������ �ִ��� Ȯ��
        int rerollCost = ShopData.Instance.RerollCost;
        if (PlayerData.Instance.Money >= rerollCost)
        {
            PlayerData.Instance.Money -= rerollCost; // ���� ����
            ShopData.Instance.RerollCost += 25; //���Ѻ�� 25 ����
            ShopData.Instance.InitShopCardList(); //���� ī�� ����Ʈ �ʱ�ȭ
            ShopData.Instance.DataChanged(); //���� ������ ���� �˷��� UI ���ΰ�ħ�ϵ���!
        }
        else
        {
            Debug.Log("���� �����մϴ�.");
        }
    }    

    public void ExitClick()
    {
        UIManager.Instance.HideUI(name);
    }

}
