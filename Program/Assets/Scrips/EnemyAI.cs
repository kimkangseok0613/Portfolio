using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Wander,
        Chase
    }

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


        // 처음 랜덤 목적지 설정
        SetRandomDestination();
    }



    void Update()
    {
        Debug.Log("Enemy AI 실행");

        if (player == null)
            return;


        float distance =
            Vector3.Distance(
                transform.position,
                player.position
            );


        // 아직 추적 전일 때만 감지
        if (currentState == State.Wander)
        {
            if (distance <= detectDistance)
            {
                currentState = State.Chase;
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
    // 플레이어 추적
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




    // Scene에서 감지거리 표시
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