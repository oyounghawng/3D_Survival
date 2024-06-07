using System;
using System.Collections;
using UnityEngine;

public class PlayerBehaviour : EntityBehaviour
{
    private InputHandler inputHandler;
    private Vector2 moveDirection;
    private UI_Conditions conditions;

    // ���Ŀ� ������ ���̺��� �̿��ؼ� �����ϴ���, ���� csv�� json���� �����ϴ� ��� ã�ƺ��� �ҵ�.
    [Header("JumpState")]
    [SerializeField] [Range(0f, 10f)] private float jumpPower = 5f;
    [SerializeField] [Range(0f, 2f)] private float jumpRayLength = 1.2f;
    [SerializeField] [Range(0f, 20f)] private float jumpStamina;
    [SerializeField] private LayerMask groundMask;

    [Header("LookState")]
    [SerializeField] [Range(0f, 90f)] private float ViewAngle = 50f;
    [SerializeField] [Range(0f, 1f)] private float mouseSensitivity = 0.1f;
    private GameObject cameraContainer;
    private float curXRot;
    private Vector2 lookDirection;

    private bool isRun;
    private bool isMove;
    private bool canRun = true;


    [Header("Gizmos")]
    [SerializeField] private bool debugMode;

    protected override void Awake()
    {
        base.Awake();
        inputHandler = GetComponent<InputHandler>();
        cameraContainer = Util.FindChild(gameObject, "CameraContainer");
    }

    private void Start()
    {
        inputHandler.OnMoveEvent += GetMoveEvent;
        inputHandler.OnRunEvent += GetRunEvent;
        inputHandler.OnJumpEvent += Jump;
        inputHandler.OnLookEvent += GetLookEvent;
        //Cursor.lockState = CursorLockMode.Locked;
        conditions = (Managers.UI.SceneUI as UI_HUD).conditions;
    }

    #region EventCallbackMethod
    private void GetMoveEvent(Vector2 vector)
    {
        moveDirection = vector;
        isMove = vector != Vector2.zero;
    }

    private void GetRunEvent(bool value)
    {
        if (canRun)
        {
            isRun = value;
            Run(isRun);
        }
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

    public override void Move(Vector2 direction)
    {
        base.Move(direction);

        if(isMove && !IsStaminaEnough(0.1f))
        {
            StartCoroutine(canRunFalse());
        }

        if(curSpeed == runSpeed && isMove && canRun)
        {
            conditions.Passive(ConditionType.Stamina, 10f, CalType.Substract);
            conditions.Passive(ConditionType.Hunger, 2f, CalType.Substract);
            conditions.Passive(ConditionType.Water, 2f, CalType.Substract);
        }
    }

    private IEnumerator canRunFalse()
    {
        canRun = false;
        curSpeed = moveSpeed;
        yield return new WaitForSeconds(5);
        canRun = true;
    }

    private void Jump()
    {
        if (!isGrounded() || !IsStaminaEnough(jumpStamina))
        {
            return;
        }

        body.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        UseStamina(jumpStamina);
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
        Vector3 frontRay = transform.forward * (GetComponent<CapsuleCollider>().radius + 1);
        if(debugMode)
        {
            Debug.DrawRay(transform.position, downRay, Color.red);
            Debug.DrawRay(cameraContainer.transform.position, frontRay, Color.green);
        }
    }

    private bool IsStaminaEnough(float value)
    {
        return conditions.Get(ConditionType.Stamina).isEnough(value);
    }

    private void UseStamina(float value)
    {
        conditions.Get(ConditionType.Stamina).Substract(value);
    }

}