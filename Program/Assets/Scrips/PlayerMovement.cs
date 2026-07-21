using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;

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


    void Awake()
    {
        controller = GetComponent<CharacterController>();
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
            {
                doubleJumpUI.Hide();
            }
        }


        if (skillCooldownTimer > 0)
        {
            skillCooldownTimer -= Time.deltaTime;
        }


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
            {
                speedSkillActive = false;
            }
        }


        if (jumpPadCooldownTimer > 0)
        {
            jumpPadCooldownTimer -= Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.Z) &&
            jumpPadCooldownTimer <= 0)
        {
            CreateJumpPad();
            jumpPadCooldownTimer = jumpPadCooldownTime;
        }


        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");


        float currentSpeed = moveSpeed;


        if (speedSkillActive)
        {
            currentSpeed = skillSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }


        Vector3 move =
            transform.right * x +
            transform.forward * z;


        controller.Move(
            move.normalized *
            currentSpeed *
            Time.deltaTime
        );


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                velocity.y =
                    Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else if (canAirJump)
            {
                velocity.y =
                    Mathf.Sqrt(jumpHeight * -2f * gravity);

                canAirJump = false;

                if (doubleJumpUI != null)
                {
                    doubleJumpUI.Hide();
                }
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
        {
            doubleJumpUI.Show();
        }
    }
}