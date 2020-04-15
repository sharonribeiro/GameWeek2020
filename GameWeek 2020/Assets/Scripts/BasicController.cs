using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    private Rigidbody RB = null;
    public float speed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 NextPos = new Vector3();
        
        if (Input.GetKey(KeyCode.W))
        {
            NextPos += transform.forward * (Time.deltaTime * speed);
            // RB.MovePosition(transform.position + transform.forward * (Time.deltaTime * speed));
        }

        if (Input.GetKey(KeyCode.S))
        {
            NextPos += -transform.forward * (Time.deltaTime * speed);
            // RB.MovePosition(transform.position - transform.forward * (Time.deltaTime * speed));
        }

        if (Input.GetKey(KeyCode.A))
        {
            NextPos += -transform.right * (Time.deltaTime * speed);
            // RB.MovePosition(transform.position - transform.right * (Time.deltaTime * speed));
        }

        if (Input.GetKey(KeyCode.D))
        {
            NextPos += transform.right * (Time.deltaTime * speed);
            // RB.MovePosition(transform.position + transform.right * (Time.deltaTime * speed));
        }
        
        RB.MovePosition(transform.position + NextPos);
    }
}
