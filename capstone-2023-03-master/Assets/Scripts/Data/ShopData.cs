using DataStructs;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopData : Singleton<ShopData>
{


    //�������� �Ĵ� 5���� ī�� ����Ʈ
    public List<CardStruct> ShopCardsList { get; set; } = new List<CardStruct>(5);
    
    //ī�� ���� �� ��� ���
    public int RerollCost { get; set; }
    
    //ī�� ���� �� ��� ���
    public int DiscardCost { get; set; }

    public event Action OnDataChange;

    protected override void Awake()
    {
        base.Awake();
        ClearShopData();
    }

    private void OnEnable()
    {
        StageManager.Instance.OnLevelClear += ClearShopData; //���� Ŭ���� �� �̰� �����ؼ� ���� ������ �ʱ�ȭ
    }
    private void OnDisable()
    {
        StageManager.Instance.OnLevelClear -= ClearShopData;
    }

    public void DataChanged()
    {
        OnDataChange?.Invoke(); 
    }

    //���� ī�� �ʱ�ȭ
    public void InitShopCardList()
    {

        ShopCardsList.Clear();

        //����, ��ų ī����� ����.
        List<CardStruct> shopCardsPool = GameData.Instance.CardList
             .Where(card => (card.type == "Attack" || card.type == "Skill")
                 && card.attribute != "Normal" //�⺻ ī��� �ȳ����� ����
             ).ToList();

        //5���� �������� ��������
        for (int i = 0; i < 5; i++)
        {
            int index = Random.Range(0, shopCardsPool.Count);
            ShopCardsList.Add(shopCardsPool[index]);
            shopCardsPool.RemoveAt(index); //�ѹ� ���� ī�尡 �ٽ� ������ �ʵ��� ����
        }
    }

    //���� �ٲ� ������ ���� ������ �ʱ�ȭ
    public void ClearShopData()
    {

        InitShopCardList();

        RerollCost = 50;
        DiscardCost = 75;
    }

}
