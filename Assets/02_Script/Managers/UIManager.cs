using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager
{
    int _order = 10;

    // 가장 마지막에 띄운 팝업이 먼저 삭제되어야하기때문에 Stack을 이용
    // GameObject를 제네릭으로 넘겨주면 Popup 정보를 알기 애매하기때문에
    // UI_Popup컴포넌트를 값으로 넘겨준다.
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    public UI_Scene _sceneUI = null;

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Canvas 안에 Canvas가 있을 때 부모가 어떤 값을 가지던 자신의 값을 가지는 것
        canvas.overrideSorting = true;

        if (sort) // 팝업 UI
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else // 팝업이 아닌 일반 UI
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        // 이름을 넣어주지않으면 T로 넘어온 값의 이름을 사용
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
        // Stack에 아무것도 없을 때 리턴
        if (_popupStack.Count == 0)
            return;

        // 마지막에 있는 것을 찾는것
        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed");
        }

        ClosePopupUI();
    }
    public void ClosePopupUI()
    {
        // Stack에 아무것도 없을 때 리턴
        if (_popupStack.Count == 0)
            return;

        // 가장 최근에 띄운 Popup뽑아오기
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