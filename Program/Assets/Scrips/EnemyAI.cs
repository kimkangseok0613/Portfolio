using System.Collections;
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

    private bool isAttacking;



    [Header("이동")]
    public float wanderSpeed = 1.5f;
    public float chaseSpeed = 4f;



    [Header("감지")]
    public float detectDistance = 10f;



    [Header("랜덤 이동")]
    public float wanderRadius = 5f;
    public float wanderWaitTime = 3f;



    public State currentState = State.Wander;



    private Transform player;

    private Animator animator;

    private Rigidbody rb;



    private Vector3 targetPosition;

    private float waitTimer;



    private Vector3 knockbackVelocity;
    private float knockbackTime;
    private bool isKnockback;



    void Start()
    {

        rb = GetComponent<Rigidbody>();


        GameObject obj =
            GameObject.FindGameObjectWithTag("Player");


        if (obj != null)
            player = obj.transform;



        animator = GetComponent<Animator>();


        SetRandomDestination();

    }







    void Update()
    {

        if (currentState == State.Dead)
            return;



        // 넉백 처리
        if (knockbackTime > 0)
        {

            knockbackTime -= Time.deltaTime;


            if (rb != null)
            {
                rb.MovePosition(
                    rb.position +
                    knockbackVelocity *
                    Time.deltaTime
                );
            }


            if (knockbackTime <= 0 && isKnockback)
            {
                isKnockback = false;

                animator.SetBool("Idle", false);
            }


            return;
        }


        if (player == null)
            return;



        float distance =
            Vector3.Distance(
                transform.position,
                player.position
            );





        if (currentState == State.Attack)
        {

            if (distance > attackDistance)
            {
                StopAttack();

                currentState = State.Chase;
            }

        }





        if (isAttacking)
            return;





        switch (currentState)
        {

            case State.Wander:


                if (distance <= detectDistance)
                    currentState = State.Chase;


                break;



            case State.Chase:


                if (distance <= attackDistance)
                {
                    currentState = State.Attack;
                }


                else if (distance > detectDistance)
                {
                    currentState = State.Wander;

                    SetRandomDestination();
                }


                break;




            case State.Attack:


                if (distance <= attackDistance)
                    Attack();


                break;

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

        if (isAttacking)
            return;



        animator.SetBool("isRun", false);



        Vector3 dir =
            (player.position -
             transform.position).normalized;


        dir.y = 0;


        LookDirection(dir);



        StartCoroutine(AttackRoutine());

    }







    IEnumerator AttackRoutine()
    {

        isAttacking = true;


        animator.SetBool(
            "Attack",
            true
        );



        float timer = 0;



        while (timer < attackDelay)
        {

            timer += Time.deltaTime;



            float distance =
                Vector3.Distance(
                    transform.position,
                    player.position
                );



            if (distance > attackDistance)
            {
                StopAttack();

                currentState = State.Chase;

                yield break;
            }


            yield return null;

        }




        PlayerHealth hp =
            player.GetComponent<PlayerHealth>();


        if (hp != null)
            hp.TakeDamage(attackDamage);




        StopAttack();


    }







    void StopAttack()
    {

        isAttacking = false;

        animator.SetBool(
            "Attack",
            false
        );

    }









    void Wander()
    {

        animator.SetBool(
            "isRun",
            false
        );



        Vector3 dir =
            (targetPosition -
             transform.position).normalized;



        Move(dir, wanderSpeed);



        LookDirection(dir);



        float distance =
            Vector3.Distance(
                transform.position,
                targetPosition);



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







    void Chase()
    {

        animator.SetBool(
            "isRun",
            true
        );



        Vector3 dir =
            (player.position -
             transform.position).normalized;


        dir.y = 0;



        Move(dir, chaseSpeed);



        LookDirection(dir);

    }







    void Move(Vector3 dir, float speed)
    {

        if (rb != null)
        {

            rb.MovePosition(
                rb.position +
                dir *
                speed *
                Time.deltaTime
            );

        }

    }







    void SetRandomDestination()
    {

        Vector3 random =
            Random.insideUnitSphere *
            wanderRadius;


        random.y = 0;


        targetPosition =
            transform.position +
            random;

    }






    // 총알 넉백

    public void Knockback(
    Vector3 direction,
    float power)
    {

        if (currentState == State.Dead)
            return;


        knockbackVelocity =
            direction.normalized *
            power;


        knockbackTime = 0.15f;


        // 피격 애니메이션 시작
        isKnockback = true;

        animator.SetBool("Idle", true);

    }

    public void Die()
    {

        currentState = State.Dead;


        StopAttack();


        animator.SetBool(
            "isRun",
            false
        );


        animator.SetTrigger(
            "Die"
        );


        if (rb != null)
            rb.isKinematic = true;



        Destroy(gameObject, 4f);

    }







    void LookDirection(Vector3 dir)
    {
        if (dir == Vector3.zero)
            return;


        Quaternion rot = Quaternion.LookRotation(dir);


        // X, Z 회전 제거 (기울어짐 방지)
        Vector3 euler = rot.eulerAngles;
        rot = Quaternion.Euler(
            0f,
            euler.y,
            0f
        );


        if (rb != null)
        {
            rb.MoveRotation(
                Quaternion.Slerp(
                    rb.rotation,
                    rot,
                    Time.deltaTime * 5f
                )
            );
        }
    }

}