using UnityEngine;


public class item_spawn : MonoBehaviour
{
    public GameObject item;
    public GameObject enemy;
    public float XX;
    public float YY;
    public int item_s_check = 0;
    // Start is called before the first frame update
    void Start()
    {
        XX = enemy.transform.position.x;
        YY = enemy.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (item_s_check == 0)
        {
            Instantiate(item, new UnityEngine.Vector3(XX, YY, 0.0f), Quaternion.identity);
            Destroy(gameObject);
            item_s_check = 1;
        }
    }
}
