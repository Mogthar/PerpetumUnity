using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement parameters")]
    [SerializeField] float movementSpeed = 1.0f;
    [SerializeField] float rotationSpeed = 1.0f;
    [SerializeField] float speedDecayTime = 0.25f;
    [SerializeField] float jumpHeight = 1.0f;

    [Header("Technical parameters")]
    [SerializeField] float groundSphereRadius = 0.4f;
    [SerializeField] float groundSphereOffsetFromBottom = 0.3f;

    // internal variables
    Vector3 movementDirection;
    Vector2 rotationIncrement;
    [SerializeField] bool isGrounded = true;
    bool isJumping = false;

    // component references
    Rigidbody rigidBody;
    Camera playerCamera;
    CapsuleCollider capsuleCollider;

    // start methods
    private void Awake() 
    {
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerCamera = Camera.main;    
    }

    void Start()
    {
        
    }

    // update methods
    void Update()
    {
        CheckGround();
        RotatePlayer();
        RotateCamera();   
    }

    private void FixedUpdate() 
    {
        MovePlayer();
        Jump(); 
    }

    // movement related methods
    void CheckGround()
    {
        Vector3 spherePosition = transform.position + Vector3.down * (capsuleCollider.height / 2 - groundSphereOffsetFromBottom);
        if(Physics.CheckSphere(spherePosition, groundSphereRadius, LayerMask.GetMask("Geometry")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    
    void MovePlayer()
    {
        if(isGrounded)
        {
            Vector3 targetVelocity = movementSpeed * transform.TransformDirection(movementDirection);
            Vector3 deltaVelocity = targetVelocity - rigidBody.velocity;
            rigidBody.AddForce((Time.fixedDeltaTime / speedDecayTime) * deltaVelocity, ForceMode.VelocityChange);
        }
        else
        {
            // add gravity
            rigidBody.AddForce(rigidBody.mass * 9.81f * Vector3.down, ForceMode.Force);
        }
    }

    void Jump()
    {
        if(isJumping && isGrounded)
        {
            rigidBody.AddForce(Vector3.up * Mathf.Sqrt(2f * 9.81f * jumpHeight) * rigidBody.mass, ForceMode.Impulse);
            isJumping = false;
        }
    }

    void RotatePlayer()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        
        // rotate the player about Y
        float deltaRotationY = rotationSpeed * rotationIncrement.x;
        Vector3 deltaRotationVectorY = new Vector3 (0, deltaRotationY, 0);
        Quaternion newRotationY = Quaternion.Euler(currentRotation + deltaRotationVectorY);
        transform.rotation = newRotationY;
    }

    void RotateCamera()
    {
        //rotate the camera about X
        float deltaRotationX = -rotationSpeed * rotationIncrement.y;
        Vector3 currentRotation = playerCamera.transform.rotation.eulerAngles;
        float normalizedCurrentRotationX = currentRotation.x;
        if (normalizedCurrentRotationX > 180)
        {
            normalizedCurrentRotationX -= 360.0f;
        }
        float newRotationAngleX = Mathf.Clamp(deltaRotationX + normalizedCurrentRotationX, -90.0f, 90.0f);
        Quaternion newRotationX = Quaternion.Euler(new Vector3(newRotationAngleX, currentRotation.y, currentRotation.z));
        playerCamera.transform.rotation = newRotationX;
    }

    // input handling
    void OnMove(InputValue input)
    {
        Vector2 inputVector = input.Get<Vector2>();
        movementDirection.x = inputVector.x;
        movementDirection.z = inputVector.y; 
    }

    void OnLook(InputValue input)
    {
        rotationIncrement = input.Get<Vector2>();
    }

    void OnJump()
    {
        Debug.Log(Vector3.down * (capsuleCollider.height / 2 - groundSphereOffsetFromBottom));
        if(isGrounded)
        {
            isJumping = true;
        }
    }

}
