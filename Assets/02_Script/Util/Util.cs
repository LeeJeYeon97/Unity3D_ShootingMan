using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Util : MonoBehaviour
{
    /// <summary>
    /// 오브젝트의 자식을 찾아주는 함수
    /// </summary>
    /// <typeparam name="T">컴포넌트 타입</typeparam>
    /// <param name="go">부모 오브젝트</param>
    /// <param name="name">찾을 이름이 없으면 이름을 비교하지않고 타입에만 의존</param>
    /// <param name="recursive">재귀적으로 찾을 것인지? true => 자식의 자식까지, false => 하위 자식만</param>
    /// <returns></returns>
    /// 

    // GameObject 전용의 FindChild
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false) // 하위 자식들만 찾기
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                // 하위 자식들 찾기
                Transform transform = go.transform.GetChild(i);

                // 이름을 찾고 컴포넌트를 찾으면 리턴
                if (string.IsNullOrEmpty(name) | transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else // 모든 자식 찾기
        {
            // 인자로 넘어온 Type(T)를 가지고있는 컴포넌트를 찾기
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                // 이름이 비어있거나 찾는 이름이면 리턴
                if (string.IsNullOrEmpty(name) | component.name == name)
                    return component;
            }
        }
        return null;
    }
}