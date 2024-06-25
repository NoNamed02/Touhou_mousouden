using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider load;

    public string scence_name;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(scence_name);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                load.value = Mathf.Lerp(load.value, op.progress, timer);
                if (load.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                load.value = Mathf.Lerp(load.value, 1f, timer);
                if (load.value == 1.0f)
                {
                    op.allowSceneActivation = true;
                    BGMmanager.Instance.Sound.Stop();
                    yield break;
                }
            }
        }
    }
}