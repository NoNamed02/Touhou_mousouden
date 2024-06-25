using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneLoad : MonoBehaviour
{
    public Slider progressbar;
    public TMP_Text loadtext;

    private void Start()
    {
        StartCoroutine(loadscene());
    }

    IEnumerator loadscene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("PlayScenes");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if (progressbar.value < 1f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }
            else
            {
                loadtext.text = "Welcome to Touhou!";
                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }
            }
        }
    }
}
