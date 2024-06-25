using UnityEngine;

public class e_bullet_test : MonoBehaviour
{
    public GameObject bulletPrefab; // 생성할 총알 프리팹
    public float fixedRotationSpeed = 10f; // 일정한 회전 속도
    private float rotationSpeed = 10f;
    private float bulletAngle = 0f;

    Vector3 unit;

    void Update()
    {
        // 회전 속도를 고정된 값으로 설정
        rotationSpeed = fixedRotationSpeed;

        // 회전 각도를 업데이트합니다.
        bulletAngle += 1f;

        if(Time.frameCount % 40 == 0)
        {
            Vector3 bulletDirection = Quaternion.Euler(0, 0, bulletAngle) * transform.up;
            Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(Vector3.forward, bulletDirection));
        }
    }
}
