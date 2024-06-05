using System;
using UnityEngine;

public class PlayerBehaviour : EntityBehaviour
{
    private InputHandler inputHandler;
    private Vector2 moveDirection;

    [Header("JumpState")]
    [SerializeField] [Range(0f, 100f)] private float jumpPower;
    [SerializeField] [Range(0f, 100f)] private float jumpRayLength;
    [SerializeField] private LayerMask groundMask;

    [Header("LookState")]
    [SerializeField] [Range(0f, 90f)] private float ViewAngle;
    [SerializeField] [Range(0f, 1f)] private float mouseSensitivity;
    [SerializeField] private GameObject cameraContainer;
    private float curXRot;
    private Vector2 lookDirection;

    [Header("Gizmos")]
    [SerializeField] private bool debugMode;

    protected override void Awake()
    {
        base.Awake();
        inputHandler = GetComponent<InputHandler>();
    }

    private void Start()
    {
        inputHandler.OnMoveEvent += GetMoveEvent;
        inputHandler.OnRunEvent += GetRunEvent;
        inputHandler.OnJumpEvent += Jump;
        inputHandler.OnLookEvent += GetLookEvent;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    #region EventCallbackMethod
    private void GetMoveEvent(Vector2 vector)
    {
        moveDirection = vector;
    }

    private void GetRunEvent(bool value)
    {
        Run(value);
    }

    private void GetLookEvent(Vector2 vector)
    {
        lookDirection = vector;
    }
    #endregion GetEvent

    private void FixedUpdate()
    {
        Move(moveDirection);
    }

    private void LateUpdate()
    {
        Look(lookDirection);
    }

    private void Jump()
    {
        if(!isGrounded())
        {
            return;
        }

        body.AddForce(transform.up * jumpPower, ForceMode.Impulse);
    }

    private bool isGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, jumpRayLength, groundMask))
        {
            return true;
        }

        return false;
    }

    private void Look(Vector2 direction)
    {
        Vector2 mouseDir = direction * mouseSensitivity;
        curXRot += mouseDir.y;
        curXRot = Mathf.Clamp(curXRot, -ViewAngle, ViewAngle);
        Vector3 xRotLook = new Vector3(-curXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDir.x, 0);
        cameraContainer.transform.localEulerAngles = xRotLook;
    }

    // DebugMode
    private void OnDrawGizmos()
    {
        Vector3 downRay = transform.up * jumpRayLength * -1f ;
        Vector3 frontRay = cameraContainer.transform.forward * (GetComponent<CapsuleCollider>().radius + 1);
        if(debugMode)
        {
            Debug.DrawRay(transform.position, downRay, Color.red);
            Debug.DrawRay(cameraContainer.transform.position, frontRay, Color.green);
        }
    }
}