using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenemanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        int setWidth = 1920;
        int setHeight = 1200;
        Screen.orientation = ScreenOrientation.Portrait;
        int deviceW = Screen.width;
        int deviceH = Screen.height;

        Screen.SetResolution(setWidth, (int)((float)deviceH / deviceW * setWidth), true);
        StartCoroutine(AdjustCamera(setWidth, setHeight, deviceW, deviceH));
    }

    IEnumerator AdjustCamera(int setWidth, int setHeight, int deviceW, int deviceH)
    {
        // Wait for the end of the frame to ensure the resolution is applied
        yield return new WaitForEndOfFrame();

        float targetAspect = (float)setWidth / setHeight;
        float windowAspect = (float)deviceW / deviceH;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = Camera.main.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            Camera.main.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = Camera.main.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            Camera.main.rect = rect;
        }
    }

    void Start()
    {
        // This section is no longer needed, as it's handled in Awake and the coroutine
    }

    // Update is called once per frame
    void Update()
    {

    }
}
