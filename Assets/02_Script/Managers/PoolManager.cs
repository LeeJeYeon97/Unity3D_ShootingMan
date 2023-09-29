using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolManager
{
    // ������Ʈ�� ��� ��ųʸ�
    private Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();
    
    // Ǯ ������
    private int poolSize;

    // Resources.Load�� ������ ������ ���
    private GameObject[] prefabs;

    // Ű ������ ����� ������ �̸�
    private string[] prefabNames;
    private int prefabLength;

    public void Init()
    {
        poolSize = 100;

        // System.Enum�� Ȱ���� Enum�� ���� ����
        prefabLength = Enum.GetValues(typeof(Define.PoolList)).Length;
        prefabNames = Enum.GetNames(typeof(Define.PoolList));

        prefabs = new GameObject[prefabLength];

        // ������ ��������
        for (int i = 0; i < prefabLength; i++)
        {
            prefabs[i] = Resources.Load<GameObject>($"PoolObjects/{prefabNames[i]}");            
        }

        // ������Ʈ Ǯ ����
        CreatePool();
    }

    private void CreatePool()
    {
        // ���̾��Ű���� �ֻ��� �θ�� �����ϱ� ���� ���� ������Ʈ
        // ���̾��Ű ������
        GameObject poolRoot = new GameObject("Pool_Root");

        for (int i = 0; i < prefabs.Length; i++)
        {
            // �� ������Ʈ ���������� ���� ������Ʈ
            // ���̾��Ű ������
            GameObject root = new GameObject($"{prefabNames[i]}_Pool");
            root.transform.SetParent(poolRoot.transform);

            // ��ųʸ��� Value������ �� ����Ʈ
            List<GameObject> list = new List<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                // ������Ʈ ����
                GameObject obj = UnityEngine.Object.Instantiate(prefabs[i], root.transform);
                // ������ ���ӿ�����Ʈ Active����
                obj.SetActive(false);
                list.Add(obj);
            }

            // ��ųʸ��� �߰�
            pools.Add(prefabNames[i], list);
        }
    }

    public GameObject GetPool(string name)
    {
        if (!pools.ContainsKey(name))
        {
            Debug.Log("Pools No Key!");
            return null;
        }
            
        // ��ųʸ����� Key���� �̿��� ����Ʈ�� Ž��
        // Active�� �����ִ� ������Ʈ�� ������ �����ͼ� ����
        for(int i = 0; i < pools[name].Count; i++)
        {
            if (pools[name][i].activeSelf == true)
                continue;

            return pools[name][i];
        }

        // ������ ���� ���� �ֱ�
        int index = 0;
        for(int i = 0; i < prefabNames.Length; i++)
        {
            if (name == prefabNames[i])
                index = i;
        }
        GameObject root = GameObject.Find($"{name}_Pool");
        GameObject obj = UnityEngine.Object.Instantiate(prefabs[index],root.transform);
        obj.SetActive(false);

        pools[name].Add(obj);

        return obj;
    }
}
