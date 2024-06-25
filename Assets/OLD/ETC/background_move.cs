using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_move : MonoBehaviour
{
    [SerializeField] private GameObject background_obj;
    [SerializeField] private float moveSpeed = 5f; // y값을 내릴 속도
    // Start is called before the first frame update
    private bool check = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y -= moveSpeed * Time.deltaTime;
        transform.position = currentPosition;

        if (gameObject.transform.position.y <= -8 && check == false)
        {
            check = true;
            Vector3 spawnPosition = new Vector3(-7.5f, 60, 10);
            Instantiate(background_obj, spawnPosition, Quaternion.identity);
        }
        else if(gameObject.transform.position.y < -60){
            
            Destroy(gameObject);
        }
    }
}
