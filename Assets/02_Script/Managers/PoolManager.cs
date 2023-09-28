using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolManager
{
    Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();
    
    private int poolSize;

    GameObject[] prefabs;
    string[] prefabNames;
    int prefabLength;

    public void Init()
    {
        poolSize = 100;

        prefabLength = Enum.GetValues(typeof(Define.PoolList)).Length;
        prefabNames = Enum.GetNames(typeof(Define.PoolList));

        prefabs = new GameObject[prefabLength];

        // 프리팹 가져오기
        for (int i = 0; i < prefabLength; i++)
        {
            prefabs[i] = Resources.Load<GameObject>($"PoolObjects/{prefabNames[i]}");            
        }

        CreatePool();
    }

    private void CreatePool()
    {
        GameObject poolRoot = new GameObject("Pool_Root");

        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject root = new GameObject($"{prefabNames[i]}_Pool");
            root.transform.SetParent(poolRoot.transform);

            List<GameObject> list = new List<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = UnityEngine.Object.Instantiate(prefabs[i], root.transform);
                obj.SetActive(false);
                list.Add(obj);
            }
            pools.Add(prefabNames[i], list);
        }
    }

    public GameObject GetPool(string name)
    {
        if (!pools.ContainsKey(name)) return null;
        
        for(int i = 0; i < pools[name].Count; i++)
        {
            if (pools[name][i] == true)
                return pools[name][i];
        }

        // 없으면 새로 만들어서 넣기
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
