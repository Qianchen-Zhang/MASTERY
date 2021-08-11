using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    public  Camera miniCam;

    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 jump;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;

    public float friction = 0.1f;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        miniCam = GameObject.Find("minimapCamera").GetComponent<Camera>();
       
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
       
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void Jump(Vector3 _jump)
    {
        jump = _jump;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
        
    }


    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

    }

    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        
    }

    private void PerformJump()
    {
        if (jump.magnitude > 0)
        {
            jump -= transform.forward * Time.deltaTime * friction;
        }
    }
}