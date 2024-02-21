using DataStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardSelectUI : MonoBehaviour
{
    [SerializeField]
    GameObject rewardView; //���� â ī�� ���� ��

    [SerializeField]
    TMP_Text rewardText; //���� â �ȳ���

    [SerializeField]
    Button discardButton;

    List<CardStruct> rewardCards = new List<CardStruct>(); //�������� ���� ������ ī�� �����͵�

    private Action CloseAction; //â ���� �� �߻��ϴ� �̺�Ʈ


    private void OnDisable()
    {
        CloseAction?.Invoke();
    }

    public void SetCloseCallback(Action CloseCallback)
    {
        CloseAction = CloseCallback;
    }

    private void ShowReward()
    {
        //���� ī����� UI�� ǥ��
        for (int i = 0; i < rewardCards.Count; i++)
        {
            CardUI cardUI = AssetLoader.Instance.Instantiate("Prefabs/UI/CardUI", rewardView.transform).GetComponent<CardUI>();
            cardUI.ShowCardData(rewardCards[i]); //���� ���� ī�带 ������.
            cardUI.OnCardClicked += (cardUI) => //ī�� UI Ŭ�� �� �ش� �̺�Ʈ �ߵ�
            {
                PlayerData.Instance.Deck.Add(cardUI.Card); //ī�� Ŭ�� �� �ش� UI�� ī�带 ���� �߰�
                UIManager.Instance.HideUI("CardSelectUI"); //â �ݱ�
            };
            cardUI.OnCardEntered += (cardUI) => { cardUI.CardBig(); }; //ī�忡 ���콺 �� �� �ش� ī�� Ȯ�� �����ϵ��� ���.
            cardUI.OnCardExited += (cardUI) => { cardUI.CardSmall(); }; //ī�忡�� ���콺 ���� �� �ش� ī�� ��� �����ϵ��� ���.
        }
    }

    //���� �������� ���� ��� ȣ��
    public void BattleReward()
    {

        rewardText.text = "�������� �¸��ϼ̽��ϴ�!\r\n�������� ī�带 �� �� ����������.";

        //������ �������� ���� ����, ��ų ī����� ����. �׸��� ��� ���� �� ������.
        List<CardStruct> rewardCardsPool = GameData.Instance.CardList
             .Where(card => (card.type == "Attack" || card.type == "Skill")
                 && card.attribute != "Normal" //�⺻ ī��� �ȳ����� ����
             ).ToList();
        List<CardStruct> rarity0Cards = rewardCardsPool.Where(card => card.rarity == 0).ToList();
        List<CardStruct> rarity1Cards = rewardCardsPool.Where(card => card.rarity == 1).ToList();
        List<CardStruct> rarity2Cards = rewardCardsPool.Where(card => card.rarity == 2).ToList();

        for (int i = 0; i < 3; i++)
        {
            float random = Random.Range(0f, 1f);

            //Ȯ���� ���� �븻, ����, ����ũ ī��Ǯ �߿��� �� ī�带 ��� ���� ī��� ����
            if (random < 0.63f)
            {
                int index = Random.Range(0, rarity0Cards.Count);
                rewardCards.Add(rarity0Cards[index]);
                rarity0Cards.RemoveAt(index); //�ѹ� ���� ī�尡 �ٽ� ������ �ʵ��� ����
            }
            else if (random < 0.95f)
            {
                int index = Random.Range(0, rarity1Cards.Count);
                rewardCards.Add(rarity1Cards[index]);
                rarity1Cards.RemoveAt(index);
            }
            else
            {
                int index = Random.Range(0, rarity2Cards.Count);
                rewardCards.Add(rarity2Cards[index]);
                rarity2Cards.RemoveAt(index);
            }
        }

        //���� ī����� UI�� ǥ��
        ShowReward();
    }

    //�������� ���� ����
    public void BossBattleReward()
    {
        rewardText.text = "�������� �������� �¸��ϼ̽��ϴ�!\r\n�������� ����� ī�带 �� �� ����������.";

        //������ �������� ���� ī����� ����. ���� �������δ� ���� �̻��� ī�常 ����
        List<CardStruct> rewardCardsPool = GameData.Instance.CardList
             .Where(card => (card.type == "Attack" || card.type == "Skill")
                 && card.attribute != "Normal" //�⺻ ī��� �ȳ����� ����
            ).ToList();
        List<CardStruct> rarity1Cards = rewardCardsPool.Where(card => card.rarity == 1).ToList();
        List<CardStruct> rarity2Cards = rewardCardsPool.Where(card => card.rarity == 2).ToList();

        for (int i = 0; i < 3; i++)
        {
            float random = Random.Range(0f, 1f);

            //Ȯ���� ���� ����, ����ũ ī��Ǯ �߿��� �� ī�带 ��� ���� ī��� ����
            if (random < 0.63f)
            {
                int index = Random.Range(0, rarity1Cards.Count);
                rewardCards.Add(rarity1Cards[index]);
                rarity1Cards.RemoveAt(index); //�ѹ� ���� ī�尡 �ٽ� ������ �ʵ��� ����
            }
            else
            {
                int index = Random.Range(0, rarity2Cards.Count);
                rewardCards.Add(rarity2Cards[index]);
                rarity2Cards.RemoveAt(index); //�ѹ� ���� ī�尡 �ٽ� ������ �ʵ��� ����
            }
        }

        ShowReward();
    }

    //���� �� ī�� ����
    public void NegoReward(int index)
    {
        rewardText.text = "���� �����߽��ϴ�!\r\n��� ��û�ڰ� �շ��մϴ�.";

        discardButton.gameObject.SetActive(false); //���� �ÿ��� �ݵ�� ī�� ���ϱ�


        List<CardStruct> rewardCardsPool;

        //Ÿ���� ����� ī�� ��, ���ʹ� �ɺ��� �ε����� ���� ī�带 �����´�.
        switch (index)
        {
            //�Ϲ� ����̸� ī���� Ÿ���� ��� ī����� ī����� ��������, '���������� �´�' ��� ī�带 ������. 3���������� mob3 Ÿ���� ī��鸸 ������.
            //���÷�, 3���������� mob3 Ÿ���� ī��鸸 �������� �ȴ�.
            case 0: 
                rewardCardsPool = GameData.Instance.CardList
                    .Where(card => card.type == $"Mob{StageManager.Instance.Stage}")
                    .ToList();
                break;
            //�� ���� ���, index�� ���� Theme�� ��ȣ�� ����. Theme�� �ش��ϴ� Enum�� �ؽ�Ʈ(���� ��� index�� 1�̸� Define.ThemeType.Pirate�� ����)
            //�� ��������, ���� ���������� ��ȣ�� ���ϸ�, ���� �׸��� 1���������� ��� Pirate1 �̶�� ���ڿ��� ����� ����.
            //card�� type�� Pirate1�� ī����� �����ͼ� Ǯ�� �߰��Ѵ�.
            default:
                rewardCardsPool = GameData.Instance.CardList
                    .Where(card => card.type == $"{StageManager.Instance.Theme}{StageManager.Instance.Stage}")
                    .ToList();
                break;
        }

        rewardCards.Add(rewardCardsPool[Random.Range(0, rewardCardsPool.Count)]);

        ShowReward();
    }

    //���� ���� �� ī�� ����
    public void BossNegoReward()
    {
        rewardText.text = "������ ũ������� �����Ͽ����ϴ�!\r\n������ ���ᰡ �Ǿ� �� ���Դϴ�.";

        discardButton.gameObject.SetActive(false); //���� �ÿ��� �ݵ�� ī�� ���ϱ�

        //Ÿ���� ������ ī�� ��, ���ʹ��� �ε�����°�� ī�带 �����´�.
        rewardCards.Add(GameData.Instance.CardList.Where(card => card.type == $"{StageManager.Instance.Theme}Boss").ElementAtOrDefault(0));
        ShowReward();
    }

    //�̺�Ʈ ī�� ȹ�� �������� ���� ��� ȣ��
    public void EventReward()
    {

        rewardText.text = "��û�ڵ��� �����Դϴ�!\r\n�������� ī�带 �� �� ����������.";

        //������ �������� ���� ����, ��ų ī����� ����. �׸��� ��� ���� �� ������.
        List<CardStruct> rewardCardsPool = GameData.Instance.CardList
             .Where(card => (card.type == "Attack" || card.type == "Skill")
                 && card.attribute != "Normal" //�⺻ ī��� �ȳ����� ����
             ).ToList();
        List<CardStruct> rarity0Cards = rewardCardsPool.Where(card => card.rarity == 0).ToList();
        List<CardStruct> rarity1Cards = rewardCardsPool.Where(card => card.rarity == 1).ToList();
        List<CardStruct> rarity2Cards = rewardCardsPool.Where(card => card.rarity == 2).ToList();

        for (int i = 0; i < 3; i++)
        {
            float random = Random.Range(0f, 1f);

            //Ȯ���� ���� �븻, ����, ����ũ ī��Ǯ �߿��� �� ī�带 ��� ���� ī��� ����
            if (random < 0.63f)
            {
                int index = Random.Range(0, rarity0Cards.Count);
                rewardCards.Add(rarity0Cards[index]);
                rarity0Cards.RemoveAt(index); //�ѹ� ���� ī�尡 �ٽ� ������ �ʵ��� ����
            }
            else if (random < 0.95f)
            {
                int index = Random.Range(0, rarity1Cards.Count);
                rewardCards.Add(rarity1Cards[index]);
                rarity1Cards.RemoveAt(index);
            }
            else
            {
                int index = Random.Range(0, rarity2Cards.Count);
                rewardCards.Add(rarity2Cards[index]);
                rarity2Cards.RemoveAt(index);
            }
        }

        //���� ī����� UI�� ǥ��
        ShowReward();
    }

    //�̺�Ʈ ���� ī�� ȹ��
    public void PartnerReward()
    {
        //���� ����ġ
        rewardText.text = "���ᰡ ũ������� �շ��Ͽ����ϴ�!\r\n������ ���ᰡ �Ǿ� �� ���Դϴ�.";

        discardButton.gameObject.SetActive(false); //���� �ÿ��� �ݵ�� ī�� ���ϱ�

        //Ÿ���� ��Ʈ���� ī�� ��, ���� ���������� �´� ī�带 ��������.
        rewardCards.Add(GameData.Instance.CardList.Where(card => card.type == $"Partner{StageManager.Instance.Stage}").ElementAtOrDefault(0));

        //���� ī�� ȹ���Ѱɷ� ����
        PlayerData.Instance.HasPartner[StageManager.Instance.Stage - 1] = true;

        ShowReward();
    }


    //���� �� �� ī�� ����
    public void LevelUpReward()
    {

        rewardText.text = "ä�� ������ ����Ͽ����ϴ�!\r\n��û�ڵ��� ������ ���� ī�带 �����Խ��ϴ�.";

        discardButton.gameObject.SetActive(false); //�ݵ�� ī�� ���ϱ�

        //type == "��û��" �� ���
        List<CardStruct> viewerCards = GameData.Instance.CardList.Where(card => card.type == "Viewer").ToList();

        List<int> rewardCardsIndexes = Define.GenerateRandomNumbers(0, viewerCards.Count, 3); // viewercards �߿� �������� �ε��� 3�� �̱�
        for(int i = 0; i < rewardCardsIndexes.Count; i++) 
        {
            rewardCards.Add(viewerCards[rewardCardsIndexes[i]]); //3���� �ε����� �ش��ϴ� ��û�� ī����� ���� ī����� �߰�
        }

        ShowReward();
    }

    //������ ��ư Ŭ��
    public void ExitButtonClick()
    {
        UIManager.Instance.HideUI("CardSelectUI");
    }
}
