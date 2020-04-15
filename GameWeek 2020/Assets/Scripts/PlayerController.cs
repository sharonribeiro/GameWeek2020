using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody m_rb;
    [SerializeField]
    private float m_speed = 10.0f;
    [SerializeField]
    private float m_maxSpeed = 5.0f;
    [SerializeField]
    private GameObject m_playerObject;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetSpeed() < m_maxSpeed)
        {
            m_rb.AddForce(transform.forward * Input.GetAxis("Vertical") * m_speed);
            m_rb.AddForce(transform.right * Input.GetAxis("Horizontal") * m_speed);
        }

        m_playerObject.transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxis("Vertical") * -1.0f, Input.GetAxis("Horizontal")) * Mathf.Rad2Deg + 90.0f, 0);
    }

    float GetSpeed()
    {
        return Mathf.Abs(m_rb.velocity.x) + Mathf.Abs(m_rb.velocity.y) + Mathf.Abs(m_rb.velocity.z);
    }
}
