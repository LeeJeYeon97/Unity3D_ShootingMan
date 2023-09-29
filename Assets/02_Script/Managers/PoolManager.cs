using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolManager
{
    // 오브젝트를 담는 딕셔너리
    private Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();
    
    // 풀 사이즈
    private int poolSize;

    // Resources.Load로 가져온 프리팹 목록
    private GameObject[] prefabs;

    // 키 값으로 사용할 프리팹 이름
    private string[] prefabNames;
    private int prefabLength;

    public void Init()
    {
        poolSize = 100;

        // System.Enum을 활용해 Enum값 정보 추출
        prefabLength = Enum.GetValues(typeof(Define.PoolList)).Length;
        prefabNames = Enum.GetNames(typeof(Define.PoolList));

        prefabs = new GameObject[prefabLength];

        // 프리팹 가져오기
        for (int i = 0; i < prefabLength; i++)
        {
            prefabs[i] = Resources.Load<GameObject>($"PoolObjects/{prefabNames[i]}");            
        }

        // 오브젝트 풀 생성
        CreatePool();
    }

    private void CreatePool()
    {
        // 하이어라키에서 최상위 부모로 지정하기 위한 게임 오브젝트
        // 하이어라키 정리용
        GameObject poolRoot = new GameObject("Pool_Root");

        for (int i = 0; i < prefabs.Length; i++)
        {
            // 각 오브젝트 종류마다의 게임 오브젝트
            // 하이어라키 정리용
            GameObject root = new GameObject($"{prefabNames[i]}_Pool");
            root.transform.SetParent(poolRoot.transform);

            // 딕셔너리의 Value값으로 들어갈 리스트
            List<GameObject> list = new List<GameObject>();

            for (int j = 0; j < poolSize; j++)
            {
                // 오브젝트 생성
                GameObject obj = UnityEngine.Object.Instantiate(prefabs[i], root.transform);
                // 생성한 게임오브젝트 Active끄기
                obj.SetActive(false);
                list.Add(obj);
            }

            // 딕셔너리에 추가
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
            
        // 딕셔너리에서 Key값을 이용해 리스트를 탐색
        // Active가 꺼져있는 오브젝트가 있으면 가져와서 리턴
        for(int i = 0; i < pools[name].Count; i++)
        {
            if (pools[name][i].activeSelf == true)
                continue;

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
