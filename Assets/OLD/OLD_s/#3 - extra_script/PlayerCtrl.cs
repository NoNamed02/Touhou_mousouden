//using System.Collections;
//using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class PlayerCtrl : MonoBehaviour
{
    public GameObject P_bullet;
    //public Transform fire_position;
    public int speed = 10;
    private int iter = 0;
    public int level = 1;
    public int type = 0;

    //private Animator _animator;
    //public Transform myTr;
    // Start is called before the first frame update
    void Start()
    {
        //_animator = GetComponent<Animator>();
        //myTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        //**********************************************************************************************************************************************************//
        Vector3 worldpos = Camera.main.WorldToViewportPoint(transform.position);
        if (worldpos.x < 0f) worldpos.x = 0f;
        if (worldpos.y < 0f) worldpos.y = 0f;
        if (worldpos.x > 1f) worldpos.x = 1f;
        if (worldpos.y > 1f) worldpos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(worldpos);

        //_animator.SetFloat("test", 10);



        /*
        if (transform.position.x < -2.5)
        {
            transform.position = new Vector3(-2.5f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 2.5)
        {
            transform.position = new Vector3(2.5f, transform.position.y, transform.position.z);
        }
        else if (transform.position.y < -5.5)
        {
            transform.position = new Vector3(transform.position.x, -5.5f, transform.position.z);
        }
        else if (transform.position.y > 5.5)
        {
            transform.position = new Vector3(transform.position.x, 5.5f, transform.position.z);
        }
        */
        //**********************************************************************************************************************************************************//
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //_animator.SetFloat("p_hori", h);
        // Debug.Log(h.ToString());
        // Debug.Log(h.ToString());

        Vector3 moveDir = (Vector3.up * v) + (Vector3.right * h);

        if (moveDir.magnitude > 1f)
        {
            moveDir = moveDir.normalized; // 입력 벡터를 정규화하여 길이가 1인 벡터로 만듦
        }

        transform.Translate(moveDir * speed * Time.deltaTime);
        //Debug.Log("Player X = " + transform.position.x.ToString());

        if (Input.GetMouseButton(0))
        {
            iter++;
            //iter += Time.deltaTime;
            //Debug.Log(iter.ToString());
            if (iter % 20 == 0)
            {
                for (int i = 0; i < level; i++)
                {
                    if (type == 0)
                    {
                        //Instantiate(P_bullet, fire_position.position + new Vector3(((1.0f - level) / 2.0f + i) * 2.0f, 0.0f, 0.0f), Quaternion.identity);
                    }
                    else if (type == 1)
                    {
                        //Instantiate(P_bullet, fire_position.position, Quaternion.Euler(0f, 0f, 1.0f - level / 2.0f + i + 0.0f));
                    }
                }
            }
            //transform.GetComponent<SpriteRenderer>().color = Color.green;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("item"))
        {
            level += 1;
        }
    }
}