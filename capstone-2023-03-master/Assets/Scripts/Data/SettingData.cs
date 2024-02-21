using DataStructs;
using LitJson;
using System.IO;
using UnityEngine;

public class SettingData : Singleton<SettingData>
{
    public SettingStruct Setting { get; set; } = new SettingStruct();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        LoadSettingData();
    }

    private void OnApplicationQuit()
    {
        SaveSettingData(); //�� �� ������ ����
    }

    private void SaveSettingData() //���� ���� ��, ���� ������ ����.
    {
        string jsonData = JsonMapper.ToJson(Setting);
        string filePath = Path.Combine(Application.persistentDataPath, "Setting.json");
        File.WriteAllText(filePath, jsonData);
    }

    public void LoadSettingData()
    {
        Debug.Log("Setting Data Load");
        string filePath = Path.Combine(Application.persistentDataPath, "Setting.json");

        if (File.Exists(filePath)) //�ι�° ���� ���ĺ��ʹ� ����� ���� �����͸� �����´�.
        {
            string dataAsJson = File.ReadAllText(filePath);
            Setting = JsonUtility.FromJson<SettingStruct>(dataAsJson);
        }
        else //ó�� ������ ������ ���� �⺻ ��������.
        {
            Setting.tutorial = true;
            Setting.bgm = 50;
            Setting.effect = 50;
        }
    }
}
