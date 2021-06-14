using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee: MonoBehaviour
{
    private Vector3 startPosition;

    public float patrolRange;
    public float speed;
    private bool isMovingRight;
    private float speedMod = 0;

    private bool isAgred;
    public float agroRange;
    public GameObject target;



    

    // Start is called before the first frame update
    void Start()
    {
    isMovingRight = true;
    startPosition = transform.position;
    isAgred = false;
    }

    // Update is called once per frame
    void Update()
    {
        //float speedMod = 0;
        //Debug.Log(transform.position.z);
        //Debug.Log(startPosition.z + patrolRange);
        
        Debug.Log(isAgred);
        if (speedMod <= 1)
            speedMod +=1 *Time.deltaTime ;

        if (Vector3.Distance(transform.position, target.transform.position) < agroRange)
        {
            if (!Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, Mathf.Infinity, 1 << 3))
            {
                isAgred = true;
            }
        }
        else
            isAgred = false;

        if (!isAgred)
        {
            if (isMovingRight)
            {
                if (transform.position.z <= (startPosition.z + patrolRange))
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (speed * speedMod * Time.deltaTime));

                }
                else
                {
                    speedMod = 0;
                    isMovingRight = false;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }

            }
            else
            {
                if (transform.position.z >= (startPosition.z - patrolRange))
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (speed * speedMod * Time.deltaTime));

                }
                else
                {
                    speedMod = 0;
                    isMovingRight = true;
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }
        }
        else
        {
            if (isMovingRight)
            {
                if (transform.position.z - target.transform.position.z <= 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (speed * speedMod * Time.deltaTime));

                }
                else
                {
                    speedMod = 0;
                    isMovingRight = false;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }

            }
            else
            {
                if (transform.position.z - target.transform.position.z > 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (speed * speedMod * Time.deltaTime));

                }
                else
                {
                    speedMod = 0;
                    isMovingRight = true;
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            } 
        }
    }
}
