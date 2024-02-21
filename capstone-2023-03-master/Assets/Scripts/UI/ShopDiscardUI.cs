using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DataStructs;


//�������� ī�� ������ UI
public class ShopDiscardUI : BaseUI
{
    [SerializeField]
    public TMP_Text disCostText; //���ź��
    [SerializeField]
    public TMP_Text playerMoneyText; //�÷��̾� ��
    [SerializeField]
    public LibraryUI libraryUI;


    private void Awake()
    {
        libraryUI.Init(LibraryMode.ShopDiscard); //�������� ���̺귯�� UI�� ���� ���� ���� �ʱ�ȭ
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

    public void UpdateUI()
    {
        disCostText.text = ShopData.Instance.DiscardCost.ToString();
        playerMoneyText.text = PlayerData.Instance.Money.ToString();
    }

}
