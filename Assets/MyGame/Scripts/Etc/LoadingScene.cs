using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// 지금은 사용안함.
/// 씬로드 시 로딩슬라이더 표현 및 비동기로드씬 진행.
/// 대부분의 로드가 Awake함수에서 리소스 로드및 생성에서 이루어지기 때문,
/// 실제 로드진행상황을 알기 힘들기 때문
/// </summary>
public class LoadingScene : MonoBehaviour
{
    public Slider slider;

    bool load = false;
    AsyncOperation async_operation;

    public Text loadText;

    private void Update()
    {
        loadText.color 
            = new Color(loadText.color.r, loadText.color.g, loadText.color.b, Mathf.PingPong(Time.time, 1));

        if (!load)
        {
            load = true;
            StartCoroutine(StartLoad(1));
        }
    }

    public IEnumerator StartLoad(int scene)
    {
        async_operation = SceneManager.LoadSceneAsync(scene);

        async_operation.allowSceneActivation = false;

        while (async_operation.progress < 0.9f)
        {
            //Debug.Log(async_operation.progress);
            slider.value = async_operation.progress;
            yield return null;
        }
        slider.value = 1f;
        async_operation.allowSceneActivation = true;
    }
}