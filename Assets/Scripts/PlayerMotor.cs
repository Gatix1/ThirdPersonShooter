using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 moveDirection = Vector3.zero;
    public CameraLook playerLook;
    public float speed = 5f;
    public float gravity = -9.8f;
    public bool isGrounded;
    public float runningSpeed = 6.5f;
    public float jumpHeight = 1.5f;
    public bool isEnemyColliding;

    public int health = 20;
    public int maxHealth = 20;
    InputManager inputManager;

    [Header("Camera Transform")]
    public Transform cameraHolderTransform;

    [Header("Movement Speed")]
    public float rotationSpeed = 3.5f;

    [Header("Rotation Variables")]
    Quaternion targetRotation;
    Quaternion playerRotation;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animator = GetComponent<Animator>();
    }

    private void HandleRotation()
    {
        targetRotation = Quaternion.Euler(0, cameraHolderTransform.eulerAngles.y, 0);
        playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
        transform.rotation = playerRotation;

    }
    private void OnTriggerStay(Collider collider)
    {
        if(collider.transform.tag == "Enemy")
        {
            isEnemyColliding = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if(collider.transform.tag == "Enemy")
        {
            isEnemyColliding = false;
        }
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        HandleRotation();
        playerLook.HandleAllCameraMovement();
    }

    public void ProcessMove(Vector2 input)
    {
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        if(!inputManager.isRunning)
            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        else
            controller.Move(transform.TransformDirection(moveDirection) * runningSpeed * Time.deltaTime);
        
        animator.SetFloat("Velocity",moveDirection.x);

        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
