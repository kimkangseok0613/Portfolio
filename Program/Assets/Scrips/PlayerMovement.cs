using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;

    [Header("Crouch")]
    public float crouchSpeed = 2.5f;
    public float crouchHeight = 1f;
    public float speedSmooth = 8f;

    public Transform playerCamera;
    public float crouchCameraOffset = 0.5f;
    public float crouchSmooth = 10f;

    public float skillSpeed = 12f;
    public float skillDuration = 5f;
    public float skillCooldownTime = 9f;

    public GameObject jumpPadPrefab;
    public float jumpPadDistance = 3f;
    public float jumpPadCooldownTime = 120f;

    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.25f;
    public LayerMask groundMask;

    public DoubleJumpUI doubleJumpUI;

    private CharacterController controller;
    private Vector3 velocity;

    private bool isGrounded;
    private bool canAirJump;

    private bool speedSkillActive;
    private float skillTimer;
    private float skillCooldownTimer;

    private float jumpPadCooldownTimer;

    private bool isCrouching;

    private float currentSpeed;

    private float standHeight;
    private Vector3 standCenter;
    private Vector3 cameraStandPos;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        standHeight = controller.height;
        standCenter = controller.center;

        if (playerCamera != null)
            cameraStandPos = playerCamera.localPosition;

        currentSpeed = moveSpeed;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask
        );

        if (isGrounded && velocity.y <= 0)
        {
            canAirJump = false;

            if (doubleJumpUI != null)
                doubleJumpUI.Hide();
        }

        // 스피드 스킬
        if (skillCooldownTimer > 0)
            skillCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q) &&
            !speedSkillActive &&
            skillCooldownTimer <= 0)
        {
            speedSkillActive = true;
            skillTimer = skillDuration;
            skillCooldownTimer = skillCooldownTime;
        }

        if (speedSkillActive)
        {
            skillTimer -= Time.deltaTime;

            if (skillTimer <= 0)
                speedSkillActive = false;
        }

        // 점프패드
        if (jumpPadCooldownTimer > 0)
            jumpPadCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z) &&
            jumpPadCooldownTimer <= 0)
        {
            CreateJumpPad();
            jumpPadCooldownTimer = jumpPadCooldownTime;
        }

        //========================
        // 웅크리기
        //========================
        isCrouching = Input.GetKey(KeyCode.LeftControl);

        if (isCrouching)
        {
            controller.height = crouchHeight;

            controller.center = new Vector3(
                standCenter.x,
                standCenter.y - (standHeight - crouchHeight) * 0.5f,
                standCenter.z
            );
        }
        else
        {
            controller.height = standHeight;
            controller.center = standCenter;
        }

        // 카메라 이동
        if (playerCamera != null)
        {
            Vector3 targetPos = cameraStandPos;

            if (isCrouching)
                targetPos += Vector3.down * crouchCameraOffset;

            playerCamera.localPosition = Vector3.Lerp(
                playerCamera.localPosition,
                targetPos,
                crouchSmooth * Time.deltaTime
            );
        }

        //========================
        // 이동
        //========================
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        float targetSpeed = moveSpeed;

        if (isCrouching)
        {
            targetSpeed = crouchSpeed;
        }
        else if (speedSkillActive)
        {
            targetSpeed = skillSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            targetSpeed = sprintSpeed;
        }

        currentSpeed = Mathf.Lerp(
            currentSpeed,
            targetSpeed,
            speedSmooth * Time.deltaTime
        );

        Vector3 move =
            transform.right * x +
            transform.forward * z;

        controller.Move(
            move.normalized *
            currentSpeed *
            Time.deltaTime
        );

        //========================
        // 중력
        //========================
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //========================
        // 점프
        //========================
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded && !isCrouching)
            {
                velocity.y =
                    Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else if (canAirJump && !isCrouching)
            {
                velocity.y =
                    Mathf.Sqrt(jumpHeight * -2f * gravity);

                canAirJump = false;

                if (doubleJumpUI != null)
                    doubleJumpUI.Hide();
            }
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(
            velocity *
            Time.deltaTime
        );
    }

    void CreateJumpPad()
    {
        Vector3 spawnPosition =
            transform.position +
            transform.forward *
            jumpPadDistance;

        RaycastHit hit;

        if (Physics.Raycast(
            spawnPosition + Vector3.up * 5f,
            Vector3.down,
            out hit,
            10f))
        {
            spawnPosition.y = hit.point.y;
        }

        Instantiate(
            jumpPadPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }

    public void JumpPadLaunch(float power)
    {
        velocity.y = power;
        canAirJump = true;

        if (doubleJumpUI != null)
            doubleJumpUI.Show();
    }
}