using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//�ڵ� ���� ������Ƽ ��� ����
//1. �Լ��� ����뿡 �ɸ���.
public class Room : MonoBehaviour
{

    public int Index { get; set; }
    public bool IsCleared { get; set; } = false; //�� ����

    public bool IsEntered { get; set; } = false; //�� ���Դ�

    public Define.EventType Type { get; set; }
    public RoomSymbol Symbol { get; set; } = null;

    //�����ִ� ����-�� ��ųʸ�
    public Dictionary<Define.Direction, Door> Doors { get; set; } = new Dictionary<Define.Direction, Door>((int)Define.Direction.Count);

    private void OnTriggerEnter(Collider collider)
    {
        IsEntered = true;

        if (IsCleared == false)
        { 
            //ó�� ���� �� �� �ݱ�
            ActivateDoors(false);

            //���� ���� ���̸� �׳� Ŭ���� ó��
            if(Type != Define.EventType.Enemy && Type != Define.EventType.Event)
            {
                IsCleared = true;
                ActivateDoors(true);
                StageManager.Instance.RoomClear();
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        //�ɺ��� ���ų� �ı��Ǿ��� ��, Ȥ�� ���̳� ������ �ƴ� �� �� Ŭ���� ó�� (���߿� ��ü ����)
        if (Symbol == null && IsCleared == false)
        {
            IsCleared = true;
            ActivateDoors(true);
            StageManager.Instance.RoomClear();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IsEntered = false;  
    }

    //��� ���� �ʱ�ȭ
    public void Init(Define.EventType type)
    {
        //Type ����
        Type = type;
        //Symbol ��ȯ

        int offset; 

        switch (type)
        {
            //�ɺ��� ��ȯ�� ��, ���� �������� �ε����� �����ϰ�, �ε����� ���� �ٸ� ��ȭ ����� ����, ������ ȹ�� ���� �ϰ� �� ����
            case Define.EventType.Enemy:
                Symbol = AssetLoader.Instance.Instantiate($"Prefabs/RoomSymbol/EnemySymbol", transform)
                    .AddComponent<EnemySymbol>();

                int choice = Random.Range(0, 2); //�Ϲ� ��� ��ȭ������ ���� ��� ��ȭ������
                offset = choice == 0 ? 0 : (int)StageManager.Instance.Theme;
                Symbol.Init(offset, type); 

                break;
            case Define.EventType.Rest:
                Symbol = AssetLoader.Instance.Instantiate($"Prefabs/RoomSymbol/RestSymbol", transform)
                    .AddComponent<RestSymbol>();
                Symbol.Init(Define.REST_INDEX, type);
                break;
            case Define.EventType.Shop:
                Symbol = AssetLoader.Instance.Instantiate($"Prefabs/RoomSymbol/ShopSymbol", transform)
                    .AddComponent<ShopSymbol>();
                Symbol.Init(Define.SHOP_INDEX, type);
                break;
            case Define.EventType.Event:
                Symbol = AssetLoader.Instance.Instantiate($"Prefabs/RoomSymbol/EventSymbol", transform)
                    .AddComponent<EventSymbol>();
                offset = Random.Range(1, 8);
                Symbol.Init(Define.EVENT_INDEX + offset, type);
                break;
            case Define.EventType.Boss:
                Symbol = AssetLoader.Instance.Instantiate($"Prefabs/RoomSymbol/BossSymbol", transform)
                    .AddComponent<BossSymbol>();
                Symbol.Init(Define.BOSS_INDEX + (int)StageManager.Instance.Theme - 1, type);
                break;
            default:
                return;
        }
        Symbol.transform.position = new Vector3(0, 1, 0);
    }

    //�� �濡�� Ư�� ���⿡ �ִ� ���� ������ ��ġ�� �����ϰ�, ������ Doors ��ųʸ��� �߰��Ѵ�.
    public void SetDoorsDictionary(Define.Direction direction, Room destination)
    {
        //�ϴ� ������ �� ��ųʸ��� <����-��> �߰�
        Doors[direction] = transform.Find("Doors").GetChild((int)direction).GetComponent<Door>();
        Doors[direction].gameObject.SetActive(true);

        //���� �� �߽ɿ��� ������ ���� ������ �ݴ� ����
        Vector3 oppositeVector = (transform.position - Doors[direction].transform.position) * 0.8f;
        oppositeVector.y = 0;

        //���� �������� �� ���Ͱ� ����
        Doors[direction].Destination = destination.transform.position + oppositeVector;    
    }

    //���� ���� Ȱ��ȭ/��Ȱ��ȭ
    public void ActivateDoors(bool isActivated)
    {
        foreach(KeyValuePair<Define.Direction, Door> door in Doors) 
        {
            door.Value.gameObject.SetActive(isActivated);
        }
    }

}
