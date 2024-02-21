
using LitJson;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DataStructs;

//���� ���ӿ��� ����ϱ� ���� �ҷ��;��� �����͵� ����. ��ü ī��, ��ü ��ȭ ���, ��ü ��������Ʈ �̹��� ��� ��
public class GameData : Singleton<GameData>
{

    private bool isLoaded = false;

    //���� ��ο� �´� ��������Ʈ ���� ��ü�� ����� ��ųʸ�, ���� �ε��ϴ� ������ I/O ���̷���
    public Dictionary<string, Sprite> SpriteDic { get; set; } = new Dictionary<string, Sprite>();

    //ī�� ��ü�� ����� ����Ʈ
    public List<CardStruct> CardList { get; set; } = new List<CardStruct>();

    //��ȭ �α� ��ü�� ����� ����Ʈ
    public Dictionary<int, List<LineStruct>> DialogDic { get; set; } = new Dictionary<int, List<LineStruct>>();

    //���������� ���� ��ųʸ� (�ε���, ���� ����ü)
    public Dictionary<int, RewardStruct> RewardDic { get; set; } = new Dictionary<int, RewardStruct>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        LoadGameData();
    }

    public void LoadGameData()
    {
        if (isLoaded) return;
        LoadSpriteDic();
        LoadCardList();
        LoadDialogDic();
        LoadRewardDic();    
        isLoaded = true;
    }

    //��ȭ ��Ͽ��� Ư���� �� �ҷ�����
    public LineStruct GetLine(int index, int lineIndex)
    {
        if (lineIndex >= DialogDic[index].Count)
        {
            return null;
        }
        else
        {
            return DialogDic[index][lineIndex];
        }
    }


    //��������Ʈ ���� �ε�
    public void LoadSpriteDic()
    {
        Debug.Log("��������Ʈ �ε�");
        Sprite[] sprites = AssetLoader.Instance.LoadAll<Sprite>("Images");
        foreach (Sprite sprite in sprites)
        {
            SpriteDic.Add(sprite.name, sprite);
        }
    }

    //ī�� ����Ʈ �ε�
    public void LoadCardList()
    {
        Debug.Log("ī�� ����Ʈ �ε�");
        string filePath = "Data/CardLibrary";
        TextAsset jsonData = AssetLoader.Instance.Load<TextAsset>(filePath);
        if (jsonData != null)
        {
            CardList = JsonMapper.ToObject<List<CardStruct>>(jsonData.text);
        }
        else
        {
            Debug.LogError("Cannot find file at " + filePath);
        }
    }

    public void LoadRewardDic()
    {
        Debug.Log("���� ����Ʈ �ε�");
        string filePath = "Data/Reward";
        TextAsset jsonData = AssetLoader.Instance.Load<TextAsset>(filePath);
        if (jsonData != null) {
            string jsonString = jsonData.text;
            JsonData rewardData = JsonMapper.ToObject(jsonString);

            for (int i = 0; i < rewardData.Count; i++)
            {
                int index = (int)rewardData[i]["index"];

                RewardStruct reward = new RewardStruct();
                reward.money = (int)rewardData[i]["money"];
                reward.viewers = (int)rewardData[i]["viewers"];
                RewardDic.Add(index, reward);
            }
        }

    }

    //��ȭ �α� ��ü �ε�
    public void LoadDialogDic()
    {
        Debug.Log("���̾�α� ����Ʈ �ε�");
        string filePath = "Data/Dialog";
        TextAsset jsonData = AssetLoader.Instance.Load<TextAsset>(filePath);
        if (jsonData != null)
        {
            string jsonString = jsonData.text;
            JsonData dialogData = JsonMapper.ToObject(jsonString);
            for(int i = 0; i < dialogData.Count; i++)
            {
                int index = (int)dialogData[i]["index"];

                List<LineStruct> lines;
                if (!DialogDic.TryGetValue(index, out lines)) //index�� ������ �� ��� �������� �Ǵ��ϰ� index�� Ű�� ���� ����Ʈ ����.
                {
                    lines = new List<LineStruct>(); 
                    DialogDic.Add(index, lines);
                }


                LineStruct line = new LineStruct(); //��ųʸ��� ����Ʈ�� ������ �� �� ������ ��ȭ
                line.portrait = dialogData[i]["portrait"]?.ToString();
                line.name = dialogData[i]["name"]?.ToString();
                line.line = dialogData[i]["line"].ToString();

                lines.Add(line);
            }
        }
    }
}
