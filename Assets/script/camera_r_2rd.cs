using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_r_2rd : MonoBehaviour
{
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;

        // Set the camera to cover the left half of the screen
        rect.width = 0.5f; // Left half of the screen
        rect.height = 1.0f; // Full height of the screen
        rect.x = 0; // Start from the left side
        rect.y = 0; // Start from the bottom

        camera.rect = rect;
        OnPreCull();
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);
}
