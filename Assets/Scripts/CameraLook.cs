using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class CameraLook : MonoBehaviour
{
    public InputManager inputManager;

    public Transform cameraPivot;
    public Camera cameraObject;
    public GameObject player;

    Vector3 cameraFollowVelocity = Vector3.zero;
    Vector3 targetPosition;
    Vector3 cameraRotation;
    Quaternion targetRotation;

    [Header("Camera Speeds")]
    float cameraSmoothTime = 0.2f;
    float zoomedFOV = 35;
    float FOV = 50;
    float lookAmountVertical, lookAmountHorizontal;
    float maximumPivotAngle = 35f;
    float minimumPivotAngle = -35f;

    public void Start()
    {
        Cursor.visible = false;
    }

    public void Update()
    {
        if(Input.GetMouseButton(1))
        {
            cameraObject.fieldOfView = zoomedFOV;
        }
        else
        {
            cameraObject.fieldOfView = FOV;
        }
    }

    public void HandleAllCameraMovement()
    {
        FollowPlayer();
        RotateCamera();
    }
    private void FollowPlayer()
    {
        targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraFollowVelocity, cameraSmoothTime * Time.deltaTime);
        transform.position = targetPosition;
    }
    

    private void RotateCamera()
    {
        lookAmountVertical += (inputManager.horizontalCameraInput);
        lookAmountHorizontal += (inputManager.verticalCameraInput);
        lookAmountHorizontal = Mathf.Clamp(lookAmountHorizontal, minimumPivotAngle, maximumPivotAngle);

        cameraRotation = Vector3.zero;
        cameraRotation.y = lookAmountVertical;
        targetRotation = Quaternion.Euler(cameraRotation);
        targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, cameraSmoothTime);
        transform.rotation = targetRotation;

        cameraRotation = Vector3.zero;
        cameraRotation.x = lookAmountHorizontal;
        targetRotation = Quaternion.Euler(cameraRotation);
        targetRotation = Quaternion.Slerp(cameraPivot.localRotation, targetRotation, cameraSmoothTime);
        cameraPivot.localRotation = targetRotation;
    }
}