using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        //transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // Y축을 기준으로 회전합니다.
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime); // Z축을 기준으로 회전합니다.
        // transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime); // X축을 기준으로 회전합니다.
    }
}
