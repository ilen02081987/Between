using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{

    public float patrolRadius;
    public float agroRadius;

    public Transform groundcheck;
    public float distance;

    private bool isMovingRight = true;

    private bool isAgred = false;
    private Transform target;

    public float speed;

    private Rigidbody _rb;

    

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.AddForce(Vector3.forward * speed * Time.deltaTime);

       
        if (!Physics.Raycast(groundcheck.position, Vector3.down, distance));
        if (isMovingRight == true)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            isMovingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            isMovingRight = false;
        }

        if (Vector3.Distance(transform.position, target.transform.position) < agroRadius)
            if (!Physics.Raycast(transform.position, (target.position - transform.position).normalized, Mathf.Infinity, 1 << 3))
            {
                transform.forward = new Vector3(target.transform.position.x, transform.position.y, 0);
                transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            }
    }

    private void FixedUpdate()
    {
        
    }
}
