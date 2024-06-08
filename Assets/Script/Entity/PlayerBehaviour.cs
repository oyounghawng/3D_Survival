using System;
using System.Collections;
using UnityEngine;

public class PlayerBehaviour : EntityBehaviour
{
    private InputHandler inputHandler;
    private Vector2 moveDirection;
    private UI_Conditions conditions;
    private Animator animator;

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

    [Header("AttackState")]
    [SerializeField] private float frontRange;
    [SerializeField] private LayerMask damagableLayer;
    private Camera cam;
    private bool isAttack;

    [Header("Gizmos")]
    [SerializeField] private bool debugMode;

    protected override void Awake()
    {
        base.Awake();
        inputHandler = GetComponent<InputHandler>();
        cameraContainer = Util.FindChild(gameObject, "CameraContainer");
        animator = Util.FindChild<Animator>(gameObject);
        cam = Camera.main;
    }

    private void Start()
    {
        inputHandler.OnMoveEvent += GetMoveEvent;
        inputHandler.OnRunEvent += GetRunEvent;
        inputHandler.OnJumpEvent += Jump;
        inputHandler.OnLookEvent += GetLookEvent;
        inputHandler.OnAttackEvent += GetAttackEvent;
        Cursor.lockState = CursorLockMode.Locked;
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

    private void GetAttackEvent()
    {
        if(!isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        animator.SetBool("Attack", true);
        isAttack = true;
        yield return new WaitForSeconds(0.1f);
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, frontRange, damagableLayer))
        {
            if (hit.transform.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                damagable.Damaged(attack);
            }
        }
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(1f);
        isAttack = false;
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

    protected override void Move(Vector2 direction)
    {
        base.Move(direction);

        if (isMove)
        {
            animator.SetFloat("Speed", curSpeed);
            animator.SetFloat("Direction", direction.x);
        }
        else
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("Direction", 0);
        }

        if (isMove && !IsStaminaEnough(0.1f))
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
        StartCoroutine(JumpAnim());
        UseStamina(jumpStamina);
    }

    private IEnumerator JumpAnim()
    {
        animator.SetBool("Jump", true);
        yield return new WaitForSeconds(0.3f);
        body.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Jump", false);
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
        Vector3 frontRay = transform.forward * frontRange;
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