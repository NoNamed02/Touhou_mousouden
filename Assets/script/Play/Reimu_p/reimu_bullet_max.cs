using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReimuBulletMax : MonoBehaviour // 유도
{
    [SerializeField] private int B_speed = 10;
    [SerializeField] private int des_point = 7;
    private int des_time = 7;
    

    private Transform target;

    void Awake()
    {
        FindClosestTarget();
    }

    void Update()
    {
        des_time++;
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.Translate(Vector2.up * B_speed * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Translate(Vector2.up * B_speed * Time.deltaTime, Space.Self);
        }

        if (gameObject.transform.position.y >= des_point)
        {
            Destroy(gameObject);
        }
        else if(des_time >= 1000)
            Destroy(gameObject);
    }

    private void FindClosestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("boss");

        float closestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = enemy;
            }
        }

        foreach (GameObject boss in bosses)
        {
            float distance = Vector2.Distance(transform.position, boss.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = boss;
            }
        }

        if (closestObject != null)
        {
            target = closestObject.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("boss"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("boss"))
        {
            Destroy(gameObject);
        }
    }
}
