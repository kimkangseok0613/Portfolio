using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Wander,
        Chase,
        Attack,
        Dead
    }
    [Header("공격")]
    public float attackDistance = 2f;
    public float attackDamage = 10f;
    public float attackDelay = 1.5f;

    private float attackTimer;

    [Header("현재 상태")]
    public State currentState = State.Wander;



    [Header("이동 속도")]
    public float wanderSpeed = 1.5f;
    public float chaseSpeed = 4f;



    [Header("플레이어 감지")]
    public float detectDistance = 10f;



    [Header("랜덤 이동")]
    public float wanderRadius = 5f;
    public float wanderWaitTime = 3f;



    private Transform player;
    private Animator animator;



    private Vector3 targetPosition;
    private float waitTimer;



    void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");


        if (playerObject != null)
        {
            player = playerObject.transform;
        }


        animator = GetComponent<Animator>();


        SetRandomDestination();
    }



    void Update()
    {
        // 죽은 상태면 아무것도 안함
        if (currentState == State.Dead)
            return;



        if (player == null)
            return;



        float distance =
            Vector3.Distance(
                transform.position,
                player.position
            );



        if (currentState != State.Dead)
        {
            if (distance <= attackDistance)
            {
                currentState = State.Attack;
            }
            else if (distance <= detectDistance)
            {
                currentState = State.Chase;
            }
            else
            {
                currentState = State.Wander;
            }
        }



        switch (currentState)
        {
            case State.Wander:
                Wander();
                break;


            case State.Chase:
                Chase();
                break;


            case State.Attack:
                Attack();
                break;
        }
    }

    void Attack()
    {
        animator.SetBool("isRun", false);


        Vector3 direction =
            (player.position - transform.position)
            .normalized;


        direction.y = 0;


        LookDirection(direction);



        attackTimer += Time.deltaTime;


        if (attackTimer >= attackDelay)
        {
            attackTimer = 0;


            PlayerHealth hp =
                player.GetComponent<PlayerHealth>();


            if (hp != null)
            {
                hp.TakeDamage(attackDamage);
            }


            Debug.Log("적 공격!");
        }
    }

    // -------------------------
    // 랜덤 이동
    // -------------------------

    void Wander()
    {
        animator.SetBool("isRun", false);



        Vector3 direction =
            (targetPosition - transform.position)
            .normalized;



        transform.position +=
            direction *
            wanderSpeed *
            Time.deltaTime;



        LookDirection(direction);



        float distance =
            Vector3.Distance(
                transform.position,
                targetPosition
            );



        if (distance < 0.5f)
        {
            waitTimer += Time.deltaTime;


            if (waitTimer >= wanderWaitTime)
            {
                waitTimer = 0;

                SetRandomDestination();
            }
        }
    }




    void SetRandomDestination()
    {
        Vector3 random =
            Random.insideUnitSphere *
            wanderRadius;


        random.y = 0;


        targetPosition =
            transform.position + random;
    }





    // -------------------------
    // 추적
    // -------------------------

    void Chase()
    {
        animator.SetBool("isRun", true);



        Vector3 direction =
            (player.position -
             transform.position)
             .normalized;



        direction.y = 0;



        transform.position +=
            direction *
            chaseSpeed *
            Time.deltaTime;



        LookDirection(direction);
    }





    // -------------------------
    // 사망 처리
    // -------------------------

    public void Die()
    {
        currentState = State.Dead;


        animator.SetBool("isRun", false);


        // Death Trigger 실행
        animator.SetTrigger("Die");



        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }





    // 방향 바라보기

    void LookDirection(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;



        Quaternion rotation =
            Quaternion.LookRotation(direction);



        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                rotation,
                Time.deltaTime * 5f
            );
    }





    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            detectDistance
        );


        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(
            transform.position,
            wanderRadius
        );
    }
}