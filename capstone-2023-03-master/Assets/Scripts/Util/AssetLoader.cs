using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//���߿� ����ȭ�� �� �� �� �ֱ� ������ �ϴ� �� Ŭ������ �ѹ� ���ؼ� �ҷ����� �Ѵ�
//����/�������� �ҷ��� �� ��� ����
public class AssetLoader : Singleton<AssetLoader>
{
    private Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    //���� �ҷ�����
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    //������ ���� ���� �迭�� �ҷ�����
    public T[] LoadAll<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path);
    }



    //�������� (�Ⱥҷ������� �ҷ�����) �����ؼ� ��ȯ
    //AssetLoader.Instance.Instantiate("Prefabs/BossDoor"); ������ ���
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab;

        if (!cache.ContainsKey(path))
        {
            cache[path] = Load<GameObject>(path);
        }

        prefab = cache[path];

        GameObject go = Object.Instantiate(prefab, parent);

        go.name = prefab.name;

        return go;
    }

    //�ٸ� ����
    public GameObject Instantiate(string path, Vector3 postion, Quaternion rotation, Transform parent = null)
    {
        GameObject go = Instantiate(path, parent);

        go.transform.position = postion;
        go.transform.rotation = rotation;

        return go;
    }

    //���� ������Ʈ �ı�
    public void Destroy(GameObject go)
    {
        Object.Destroy(go);
    }


}
