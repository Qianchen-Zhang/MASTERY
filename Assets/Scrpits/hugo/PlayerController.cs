using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float mouseSensitivityX = 3f;

    [SerializeField]
    private float mouseSensitivityY = 3f;

    [SerializeField]
    private float jumpForce = 100f;

    private PlayerMotor motor;

    public GameObject lobbyWindow;



    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }


    public void LateUpdate()
    {
        Vector3 newPosition = this.transform.position;
        newPosition.y = motor.miniCam.transform.position.y;
        motor.miniCam.transform.position = newPosition;

        motor.miniCam.transform.rotation = Quaternion.Euler(90f, this.transform.eulerAngles.y, 0f);
    }

    private void Update()
    {
        if (!lobbyWindow.activeSelf)
        {
            float xMov = Input.GetAxisRaw("Horizontal");
              float zMov = Input.GetAxisRaw("Vertical");

            Vector3 moveHorizontal = transform.right * xMov;
            Vector3 moveVertical = transform.forward * zMov;

            Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

            motor.Move(velocity);

            float yRot = Input.GetAxisRaw("Mouse X");

            Vector3 rotation = new Vector3(0, yRot, 0) * mouseSensitivityX;

            motor.Rotate(rotation);

            float xRot = Input.GetAxisRaw("Mouse Y");

            float cameraRotationX = xRot * mouseSensitivityY;

            motor.RotateCamera(cameraRotationX);
        }
    }
}