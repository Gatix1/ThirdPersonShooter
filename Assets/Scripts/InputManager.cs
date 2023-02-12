using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    public float verticalCameraInput, horizontalCameraInput;
    public bool isRunning;

    Vector2 cameraInput;

    private PlayerMotor motor;
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
    }

    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        onFoot.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        onFoot.Sprint.performed += i => isRunning = true;
        onFoot.Sprint.canceled += i => isRunning = false;
        onFoot.Jump.performed += i => motor.Jump();
        horizontalCameraInput = cameraInput.x;
        verticalCameraInput = -cameraInput.y;

    }

    void OnEnable()
    {
        onFoot.Enable();
    }
    void OnDisable()
    {
        onFoot.Disable();
    }
}
