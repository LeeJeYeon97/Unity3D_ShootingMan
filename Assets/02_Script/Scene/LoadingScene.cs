using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : BaseScene
{

    private UI_Loading loadingUI;
    public Slider progressBar;

    protected override void Init()
    {
        GameObject obj = Resources.Load<GameObject>("UI/Scene/UI_Scene_Loading");
        loadingUI = Instantiate(obj).GetComponent<UI_Loading>();
    }
    void Start()
    {
        progressBar = loadingUI.slider;
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("Game");

        // ���� �ε� �Ϸ�Ǿ��� �� �ٷ� �� ��ȯ�� �� ������ üũ
        // 90%������ Load
        ao.allowSceneActivation = false;

        //float timer = 0f;

        while (!ao.isDone)
        {
            yield return null;

            if (ao.progress < 0.9f)
            {
                Debug.Log("test");
                progressBar.value = ao.progress;
            }
            else
            {
                yield break;
                //timer += Time.unscaledDeltaTime;
            }
        }
    }
}
