using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgscroll : MonoBehaviour
{
    public int Bg_speed = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Bg_speed * Time.deltaTime, Space.Self);
        if (transform.position.y < -12)
        {
            transform.position = transform.position + new Vector3(0.0f, 24.0f, 0.0f);
        }
    }
}
