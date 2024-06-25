using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawn : MonoBehaviour
{
    public GameObject enemy;
    public int i = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        i++;
        if (i % 1000 == 0)
        {
            Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
        }
    }
}
