using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform m_target;
    [SerializeField]
    private float m_speed = 0.125f;
    [SerializeField]
    private Vector3 m_offset;

    void FixedUpdate()
    {
        Vector3 smoothPos = Vector3.Slerp(transform.position, m_target.position + m_offset, m_speed);
        transform.position = smoothPos;
    }
}
