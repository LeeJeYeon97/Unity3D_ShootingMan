using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Util : MonoBehaviour
{
    /// <summary>
    /// ������Ʈ�� �ڽ��� ã���ִ� �Լ�
    /// </summary>
    /// <typeparam name="T">������Ʈ Ÿ��</typeparam>
    /// <param name="go">�θ� ������Ʈ</param>
    /// <param name="name">ã�� �̸��� ������ �̸��� �������ʰ� Ÿ�Կ��� ����</param>
    /// <param name="recursive">��������� ã�� ������? true => �ڽ��� �ڽı���, false => ���� �ڽĸ�</param>
    /// <returns></returns>
    /// 

    // GameObject ������ FindChild
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

        if (recursive == false) // ���� �ڽĵ鸸 ã��
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                // ���� �ڽĵ� ã��
                Transform transform = go.transform.GetChild(i);

                // �̸��� ã�� ������Ʈ�� ã���� ����
                if (string.IsNullOrEmpty(name) | transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else // ��� �ڽ� ã��
        {
            // ���ڷ� �Ѿ�� Type(T)�� �������ִ� ������Ʈ�� ã��
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                // �̸��� ����ְų� ã�� �̸��̸� ����
                if (string.IsNullOrEmpty(name) | component.name == name)
                    return component;
            }
        }
        return null;
    }
}