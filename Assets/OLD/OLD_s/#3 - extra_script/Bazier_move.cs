using UnityEngine;

public class Bazier_move : MonoBehaviour
{
    public Transform[] waypoints;
    public GameObject objectToMove; // 이동시킬 오브젝트

    private void Update()
    {
        MoveObjectAlongBazier();
    }

    private void MoveObjectAlongBazier()
    {
        float t = Mathf.PingPong(Time.time * 0.5f, 1); // 시간에 따라 t 값 변화

        Vector3 newPos = CalculateBazierPosition(t);
        objectToMove.transform.position = newPos;
    }

    private Vector3 CalculateBazierPosition(float t)
    {
        Vector3 position = Mathf.Pow(1 - t, 3) * waypoints[0].position +
                           3 * Mathf.Pow(1 - t, 2) * t * waypoints[1].position +
                           3 * (1 - t) * Mathf.Pow(t, 2) * waypoints[2].position +
                           Mathf.Pow(t, 3) * waypoints[3].position;
        Gizmos.DrawSphere(position, 0.05f);

        return position;
    }
}
