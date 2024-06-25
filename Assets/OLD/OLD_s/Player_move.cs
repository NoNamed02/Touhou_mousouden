using UnityEngine;
using System.Collections;

public class Player_move : MonoBehaviour
{
    
    public GameObject P_bullet;
    public GameObject Player_spwan;
    public Transform fire_position;
    public Transform fire_position1;
    public Transform fire_position2;
    [SerializeField] private int speed = 10;
    //[SerializeField]private int invincibility =
    private int iter = 0;
    public int level = 1;
    public int type = 0;
    public float damageDuration = 1.0f;
    public bl_Joystick js; // 조이스틱 오브젝트

    public float HP = 100.0f; // 체력
    [SerializeField] private int hit_check = 0;
    private SpriteRenderer spriteRenderer;
    
    private bool spwan_check;

    private Animator animator;

    //public GameManager manager;
    void Awake() {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
        spwan_check = true;
        
    }
    void Update()
    {
        AudioSource die = GetComponent<AudioSource>();
        if(spwan_check == true){
            StartCoroutine(respwan());
            spwan_check = false;
        }
        /*****************************조이스틱 구현*************************************/
        Vector3 dir = new Vector3(js.Horizontal, js.Vertical, 0);
        dir.Normalize();
        transform.position += dir * speed * Time.deltaTime; // 조이스틱 이동
        if (dir.x > 0) // right
        {
            animator.SetBool("IsMovingRight", true);
            animator.SetBool("IsMovingLeft", false);
        }
        else if (dir.x < 0) //left
        {
            animator.SetBool("IsMovingRight", false);
            animator.SetBool("IsMovingLeft", true);
        }
        else // idle
        {
            animator.SetBool("IsMovingRight", false);
            animator.SetBool("IsMovingLeft", false);
        }


        transform.rotation = Quaternion.identity;
        //**********************************************************************************************************************************************************//
        Vector3 worldpos = Camera.main.WorldToViewportPoint(transform.position);
        if (worldpos.x < 0f) worldpos.x = 0f;
        if (worldpos.y < 0f) worldpos.y = 0f;
        if (worldpos.x > 1f) worldpos.x = 1f;
        if (worldpos.y > 1f) worldpos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(worldpos);

        //*********************************************키보드*************************************************************************************************************//
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDir = (Vector3.up * v) + (Vector3.right * h);

        if (moveDir.magnitude > 1f)
        {
            moveDir = moveDir.normalized; // 입력 벡터를 정규화하여 길이가 1인 벡터로 만듦
        }

        transform.Translate(moveDir * speed * Time.deltaTime);
        iter++;
        if (iter % 5 == 0)
        {
            for (int i = 0; i < level; i++)
            {
                if (type == 0)
                {
                    //Instantiate(P_bullet, fire_position.position + new Vector3(((1.0f - level) / 2.0f + i) * 2.0f, 0.0f, 0.0f), Quaternion.identity);
                    Instantiate(P_bullet, fire_position.position, Quaternion.identity);
                    if(level >= 10){
                        Instantiate(P_bullet, fire_position1.position, Quaternion.Euler(0f, 0f, 20f));
                    }
                    if(level >= 20){
                        Instantiate(P_bullet, fire_position2.position, Quaternion.Euler(0f, 0f, -20f));
                    }
                }
                else if (type == 1) //집속
                {
                    Instantiate(P_bullet, fire_position.position, Quaternion.identity);
                    if(level >= 10){
                        Instantiate(P_bullet, fire_position1.position, Quaternion.identity);
                    }
                    if(level >= 20){
                        Instantiate(P_bullet, fire_position2.position, Quaternion.identity);
                    }
                    //Instantiate(P_bullet, fire_position.position, Quaternion.Euler(0f, 0f, 1.0f - level / 2.0f + i + 0.0f));
                }
            }
        }
        if(HP <= 0){
            die.Play();
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("item"))
        {
            level += 1;
            if (level >= 30)
            { // 레벨 MAX
                level = 30;
            }
        }

        if (hit_check == 0 && collision.gameObject.CompareTag("e_bullet"))
        {
            StartCoroutine(HandleDamage());
        }
    }

    
    private IEnumerator HandleDamage()
    {
        HP -= 20;
        hit_check = 1;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(damageDuration);
        spriteRenderer.color = Color.white;
        hit_check = 0;
    }

    private IEnumerator respwan()
    {
        hit_check = 1;
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(4f);
        spriteRenderer.color = Color.white;
        hit_check = 0;
    }

    public void ResetHPBar()
    {
        GameObject hpBarObject = GameObject.FindGameObjectWithTag("HPBar");
        if (hpBarObject != null)
        {
            HPBar hpBar = hpBarObject.GetComponent<HPBar>();
            if (hpBar != null)
            {
                //hpBar.playerMove = this;
                hpBar.slider.maxValue = HP;
                hpBar.slider.value = HP;
            }
        }
    }

    /*
    public void Resetlife()
    {
        GameObject GameM = GameObject.FindGameObjectWithTag("GameM");
        if (GameM != null)
        {
            GameManager gameManager = GameM.GetComponent<GameManager>();
            if (gameManager != null)
            {
                gameManager.LIFE = manager.LIFE; // 현재 라이프 값을 가져옴
            }
        }
    }
    */

    /*
    public void Resetlife()
    {
        GameObject GameM = GameObject.FindGameObjectWithTag("GameM");
        if (GameM != null)
        {
            GameManager gameManager = GameM.GetComponent<GameManager>();
            if (gameManager != null)
            {
                gameManager.LIFE = manager.LIFE; // 현재 라이프 값을 가져옴
            }
        }
    }
    */
}
