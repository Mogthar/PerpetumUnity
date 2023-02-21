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

        // give the object its initial velocity
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = movementDirection * movementSpeed;

        // check if end is about to be reached and reflect the velocity if yes
        Reflect();
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        Reflect();
    }

    void Reflect()
    {
        distanceToEnd = Vector3.Distance(transform.position, end.position);
        float distanceIncrement = movementSpeed * Time.fixedDeltaTime;
        if(distanceIncrement > distanceToEnd)
        {
            distanceIncrement -= distanceToEnd;

            // swap end and beginning
            Transform startPlaceholder = start;
            start = end;
            end = startPlaceholder;
            movementDirection = Vector3.Normalize(end.position - start.position);

            // place the body where it should be after reflection and reverse its velocity
            rigidBody.MovePosition(start.position + distanceIncrement * movementDirection);
            rigidBody.velocity = movementDirection * movementSpeed;
        }
    }


}
