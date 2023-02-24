using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    [SerializeField] Platform myPlatform;
    [SerializeField] float centerOffset;

    BoxCollider myBoxCollider;

    private void Awake() 
    {
        // offset the detector
        myBoxCollider = GetComponent<BoxCollider>();
        float centerOffsetScaled = centerOffset / myPlatform.transform.localScale.y; 
        myBoxCollider.center = new Vector3(0, centerOffsetScaled, 0);
    }

    private void OnTriggerStay(Collider other) 
    {
        other.transform.SetParent(myPlatform.transform, true);
        // other.transform.localScale = new Vector3(1/ myPlatform.transform.lossyScale.x, 1/ myPlatform.transform.lossyScale.y, 1/ myPlatform.transform.lossyScale.z);
    }

    private void OnTriggerExit(Collider other) 
    {
        other.transform.SetParent(null, true);
        Rigidbody objectRigidBody;
        if(other.TryGetComponent<Rigidbody>(out objectRigidBody))
        {
            objectRigidBody.velocity += myPlatform.GetMovementDirection() * myPlatform.GetMovementSpeed();
        }
    }
}
