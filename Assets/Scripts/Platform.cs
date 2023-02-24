using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float fractionalOffset;

    [SerializeField] Transform start;
    [SerializeField] Transform end;

    private float distanceToEnd;
    private Vector3 movementDirection;
    private Rigidbody rigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        // initialize the object between its start and end point (based on fractional offset variable)
        distanceToEnd = Vector3.Distance(start.position, end.position); // probably not necessary but wasnt sure about scaling
        movementDirection = Vector3.Normalize(end.position - start.position);
        transform.position = start.position + distanceToEnd * fractionalOffset * movementDirection;

        // get the rigidbody of the component
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        Move();
    }

    void Move()
    {
        distanceToEnd = Vector3.Distance(transform.position, end.position);
        float distanceIncrement = movementSpeed * Time.deltaTime;
        if(distanceIncrement < distanceToEnd)
        {
            transform.position = transform.position + movementDirection * distanceIncrement;
        }
        else
        {
            distanceIncrement -= distanceToEnd;

            // swap end and beginning
            Transform startPlaceholder = start;
            start = end;
            end = startPlaceholder;

            // reflect movement direction
            movementDirection = Vector3.Normalize(end.position - start.position);

            // place the body where it should be after reflection and reverse its velocity
            transform.position = start.position + movementDirection * distanceIncrement;
        }
    }

    public Vector3 GetMovementDirection()
    {
        return movementDirection;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    /* void FixedUpdate() 
    {
        Move();
    }

    void Move()
    {
        distanceToEnd = Vector3.Distance(transform.position, end.position);
        float distanceIncrement = movementSpeed * Time.fixedDeltaTime;
        if(distanceIncrement < distanceToEnd)
        {
            rigidBody.MovePosition(transform.position + movementDirection * distanceIncrement);
        }
        else
        {
            distanceIncrement -= distanceToEnd;

            // swap end and beginning
            Transform startPlaceholder = start;
            start = end;
            end = startPlaceholder;

            // reflect movement direction
            movementDirection = Vector3.Normalize(end.position - start.position);

            // place the body where it should be after reflection and reverse its velocity
            rigidBody.MovePosition(start.position + movementDirection * distanceIncrement);
        }
    }
    */


}
