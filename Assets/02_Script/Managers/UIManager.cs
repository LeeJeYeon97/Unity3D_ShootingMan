using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager
{
    int _order = 10;

    // ���� �������� ��� �˾��� ���� �����Ǿ���ϱ⶧���� Stack�� �̿�
    // GameObject�� ���׸����� �Ѱ��ָ� Popup ������ �˱� �ָ��ϱ⶧����
    // UI_Popup������Ʈ�� ������ �Ѱ��ش�.
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    public UI_Scene _sceneUI = null;

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Canvas �ȿ� Canvas�� ���� �� �θ� � ���� ������ �ڽ��� ���� ������ ��
        canvas.overrideSorting = true;

        if (sort) // �˾� UI
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else // �˾��� �ƴ� �Ϲ� UI
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        // �̸��� �־����������� T�� �Ѿ�� ���� �̸��� ���
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject prefab = Resources.Load<GameObject>($"UI/Popup/{name}");
        GameObject go = UnityEngine.Object.Instantiate(prefab);


        T popup = go.GetOrAddComponent<T>();
        _popupStack.Push(popup);

        GameObject root = GameObject.Find("@UI_Root");

        if (root == null)
            root = new GameObject { name = "@UI_Root" };

        go.transform.SetParent(root.transform);

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        // Stack�� �ƹ��͵� ���� �� ����
        if (_popupStack.Count == 0)
            return;

        // �������� �ִ� ���� ã�°�
        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed");
        }

        ClosePopupUI();
    }
    public void ClosePopupUI()
    {
        // Stack�� �ƹ��͵� ���� �� ����
        if (_popupStack.Count == 0)
            return;

        // ���� �ֱٿ� ��� Popup�̾ƿ���
        UI_Popup popup = _popupStack.Pop();
        UnityEngine.Object.Destroy(popup.gameObject);
        //popup.gameObject.SetActive(false);
        popup = null;
        _order--;

    }
    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GameObject.Instantiate<T>(Resources.Load<T>($"UI/Scene/{name}")) as GameObject;

        T sceneUI = go.GetOrAddComponent<T>();
        _sceneUI = sceneUI;

        GameObject root = GameObject.Find("@UI_Root");

        if (root == null)
            root = new GameObject { name = "@UI_Root" };

        go.transform.SetParent(root.transform);

        return sceneUI;
    }
}