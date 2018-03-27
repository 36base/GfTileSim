using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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