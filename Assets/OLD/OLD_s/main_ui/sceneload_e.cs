using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneLoad_e : MonoBehaviour
{
    public Slider progressbar;
    public TMP_Text loadtext;
    public bool check;

    private void Start()
    {
        StartCoroutine(loadscene());
    }

    IEnumerator loadscene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("PlayScenes");
        operation.allowSceneActivation = false;
        check = operation.allowSceneActivation;

        while (!operation.isDone)
        {
            yield return null;
            if (progressbar.value < 1f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }
            else
            {
                loadtext.text = "Touch to continue";
            }

            if (progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                SceneManager.LoadScene("PlayScenes");
                operation.allowSceneActivation = true;
                check = operation.allowSceneActivation;

            }
        }
    }

}
